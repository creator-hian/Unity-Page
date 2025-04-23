using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Page
{
    /// <summary>
    /// CanvasGroup을 활용한 페이지 전환 시스템의 구체적 구현체
    /// </summary>
    /// <remarks>
    /// 주요 동작 원리:
    /// <list type="number">
    /// <item>페이드 인/아웃 시 알파 값 변화를 통한 시각적 효과 구현</item>
    /// <item>CancellationTokenSource를 이용한 비동기 작업 취소 관리</item>
    /// <item>VContainer 의존성 주입을 통한 설정 파라미터 제어</item>
    /// </list>
    /// </remarks>
    [RequireComponent(typeof(CanvasGroup))]
    public class PageView : MonoBehaviour, IPage
    {
        /// <summary>
        /// 의존성 주입 설정 인터페이스
        /// </summary>
        public interface IInjectionConfig
        {
            /// <summary>기본 페이드 속도 대비 배율 (1 = 100% 속도)</summary>
            float FadeSpeedMultiplier { get; }
        }

        [SerializeField]
        [Tooltip("유니티 인스펙터에서 설정하는 기본 페이드 속도 배율")]
        protected float fadeSpeedMultiplier = 1f;

        /// <summary>페이지 활성화 시작 이벤트</summary>
        public event Action OnEnterActivate;

        /// <summary>페이지 활성화 완료 이벤트</summary>
        public event Action OnExitActivate;

        /// <summary>페이지 비활성화 시작 이벤트</summary>
        public event Action OnEnterDeactivate;

        /// <summary>페이지 비활성화 완료 이벤트</summary>
        public event Action OnExitDeactivate;

        /// <summary>페이지 전환 시작 이벤트</summary>
        public event Action OnStartPageTransition;

        /// <summary>페이지 전환 종료 이벤트</summary>
        public event Action OnEndPageTransition;

        private CanvasGroup _canvasGroup;
        private CancellationTokenSource _fadeTokenSource;

        public bool IsActive { get; private set; } = false;

        /// <summary>
        /// 의존성 주입 구성 메서드
        /// </summary>
        /// <param name="injectionConfig">외부 주입 설정 객체</param>
        /// <remarks>
        /// 인스펙터에서 설정한 값보다 주입된 설정이 우선 적용됨
        /// </remarks>
        [Inject]
        protected void Construct(IInjectionConfig injectionConfig)
        {
            PageLogger.Log(
                $"DI 구성 업데이트 완료 - 페이드 속도 배율: {this.fadeSpeedMultiplier}배 적용"
            );

            this.fadeSpeedMultiplier = injectionConfig.FadeSpeedMultiplier;

            if (null == _canvasGroup && !TryGetComponent(out _canvasGroup))
            {
                PageLogger.LogError("CanvasGroup not found");
            }
        }

        /// <summary>
        /// CanvasGroup 컴포넌트 초기화
        /// </summary>
        private void Awake()
        {
            if (null == _canvasGroup && !TryGetComponent(out _canvasGroup))
            {
                PageLogger.LogError("CanvasGroup not found");
            }
        }

        /// <summary>
        /// 페이드 전환 작업 취소 및 리소스 정리
        /// </summary>
        /// <remarks>
        /// 기존 진행 중인 페이드 애니메이션 즉시 중단
        /// </remarks>
        private void CancelCurrentFade()
        {
            if (_fadeTokenSource != null)
            {
                _fadeTokenSource.Cancel();
                _fadeTokenSource.Dispose();
                _fadeTokenSource = null;
            }
        }

        /// <summary>
        /// 페이지 활성/비활성 상태 전환 메서드
        /// </summary>
        /// <param name="active">활성화 여부</param>
        /// <param name="animate">애니메이션 사용 여부</param>
        /// <exception cref="MissingComponentException">CanvasGroup 컴포넌트 누락 시 발생</exception>
        public void SetActive(bool active, bool animate = false)
        {
            CancelCurrentFade();

            if (!animate)
            {
                OnStartPageTransition?.Invoke();

                if (active)
                {
                    OnEnterActivate?.Invoke();
                }
                else
                {
                    OnEnterDeactivate?.Invoke();
                }

                if (_canvasGroup)
                {
                    _canvasGroup.alpha = active ? 1 : 0;
                    _canvasGroup.interactable = active;
                    _canvasGroup.blocksRaycasts = active;
                }

                IsActive = active;

                if (active)
                {
                    OnExitActivate?.Invoke();
                }
                else
                {
                    OnExitDeactivate?.Invoke();
                }

                OnEndPageTransition?.Invoke();
                return;
            }

            _fadeTokenSource = new CancellationTokenSource();
            _ = HandleFadeTransition(active);
        }

        /// <summary>
        /// 페이드 전환 비동기 처리 핸들러
        /// </summary>
        /// <param name="active">전환 방향 (true: FadeIn, false: FadeOut)</param>
        /// <returns>비동기 작업 태스크</returns>
        private async Task HandleFadeTransition(bool active)
        {
            OnStartPageTransition?.Invoke();
            try
            {
                if (active)
                {
                    OnEnterActivate?.Invoke();
                    await FadeIn(_fadeTokenSource.Token);
                }
                else
                {
                    OnEnterDeactivate?.Invoke();
                    await FadeOut(_fadeTokenSource.Token);
                }
            }
            catch (OperationCanceledException)
            {
                PageLogger.LogWarning($"{(active ? "FadeIn" : "FadeOut")} was canceled");
            }
            catch (Exception ex)
            {
                PageLogger.LogError($"Error during fade transition: {ex}");
                _canvasGroup.alpha = active ? 0 : 1;
            }
            finally
            {
                if (active)
                {
                    OnExitActivate?.Invoke();
                }
                else
                {
                    OnExitDeactivate?.Invoke();
                }

                if (_fadeTokenSource != null)
                {
                    _fadeTokenSource.Dispose();
                    _fadeTokenSource = null;
                }
                OnEndPageTransition?.Invoke();
            }
        }

        /// <summary>
        /// 페이드 아웃 애니메이션 실행
        /// </summary>
        /// <param name="cancellationToken">작업 취소 토큰</param>
        /// <returns>애니메이션 완료 태스크</returns>
        /// <exception cref="OperationCanceledException">작업 취소 시 발생</exception>
        private async Task FadeOut(CancellationToken cancellationToken)
        {
            if (_canvasGroup == null)
            {
                PageLogger.LogError("CanvasGroup not found");
                return;
            }

            float currentAlpha = _canvasGroup.alpha;
            while (currentAlpha > 0)
            {
                currentAlpha -= Time.deltaTime * fadeSpeedMultiplier;
                _canvasGroup.alpha = Mathf.Max(0, currentAlpha);
                await Task.Yield();
                cancellationToken.ThrowIfCancellationRequested();
            }
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        /// <summary>
        /// 페이드 인 애니메이션 실행
        /// </summary>
        /// <param name="cancellationToken">작업 취소 토큰</param>
        /// <returns>애니메이션 완료 태스크</returns>
        /// <exception cref="OperationCanceledException">작업 취소 시 발생</exception>
        private async Task FadeIn(CancellationToken cancellationToken)
        {
            if (_canvasGroup == null)
            {
                PageLogger.LogError("CanvasGroup not found");
                return;
            }

            float currentAlpha = _canvasGroup.alpha;
            while (currentAlpha < 1)
            {
                currentAlpha += Time.deltaTime * fadeSpeedMultiplier;
                _canvasGroup.alpha = Mathf.Min(1, currentAlpha);
                await Task.Yield();
                cancellationToken.ThrowIfCancellationRequested();
            }
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        /// <summary>
        /// 객체 파괴 시 리소스 정리
        /// </summary>
        /// <remarks>
        /// 진행 중인 모든 페이드 애니메이션 중단
        /// </remarks>
        private void OnDestroy()
        {
            CancelCurrentFade();
        }
    }
}
