using System;

namespace Page
{
    /// <summary>
    /// 페이지의 동작을 정의하는 인터페이스
    /// </summary>
    /// <remarks>
    /// 구현체는 다음 기능을 제공해야 합니다:
    /// <list type="bullet">
    /// <item>페이드 인/아웃을 이용한 시각적 전환 효과</item>
    /// <item>페이지 활성화 상태에 따른 UI 요소 제어</item>
    /// <item>전환 과정의 이벤트 시스템 제공</item>
    /// </list>
    /// </remarks>
    public interface IPage
    {
        public bool IsActive { get; }

        /// <summary>
        /// 페이지의 활성/비활성 상태를 설정
        /// </summary>
        /// <param name="active">페이지 활성화 여부</param>
        /// <param name="animate">애니메이션 사용 여부 (기본값: false)</param>
        /// <exception cref="MissingComponentException">필수 컴포넌트 누락 시 발생</exception>
        void SetActive(bool active, bool animate = false);

        /// <summary>
        /// 페이지 활성화 과정 시작 시 발생하는 이벤트
        /// </summary>
        /// <remarks>
        /// 페이드 인 애니메이션 시작 직전에 호출됩니다.
        /// 활성화 초기화 작업에 사용할 수 있습니다.
        /// </remarks>
        event Action OnEnterActivate;

        /// <summary>
        /// 페이지 활성화 과정 완료 시 발생하는 이벤트
        /// </summary>
        /// <remarks>
        /// 페이드 인 애니메이션 종료 후 호출됩니다.
        /// 활성화 완료 후 처리 작업에 사용할 수 있습니다.
        /// </remarks>
        event Action OnExitActivate;

        /// <summary>
        /// 페이지 비활성화 과정 시작 시 발생하는 이벤트
        /// </summary>
        /// <remarks>
        /// 페이드 아웃 애니메이션 시작 직전에 호출됩니다.
        /// 비활성화 준비 작업에 사용할 수 있습니다.
        /// </remarks>
        event Action OnEnterDeactivate;

        /// <summary>
        /// 페이지 비활성화 과정 완료 시 발생하는 이벤트
        /// </summary>
        /// <remarks>
        /// 페이드 아웃 애니메이션 종료 후 호출됩니다.
        /// 최종 정리 작업에 사용할 수 있습니다.
        /// </remarks>
        event Action OnExitDeactivate;

        /// <summary>
        /// 페이지 전환 시작 시 발생하는 이벤트
        /// </summary>
        /// <remarks>
        /// 페이지 전환 애니메이션이 시작될 때 호출됩니다.
        /// UI 갱신 또는 관련 로직 처리에 사용할 수 있습니다.
        /// </remarks>
        event Action OnStartPageTransition;

        /// <summary>
        /// 페이지 전환 종료 시 발생하는 이벤트
        /// </summary>
        /// <remarks>
        /// 페이지 전환 애니메이션이 완전히 종료될 때 호출됩니다.
        /// 최종 상태 동기화 또는 후처리 작업에 사용할 수 있습니다.
        /// </remarks>
        event Action OnEndPageTransition;
    }
}
