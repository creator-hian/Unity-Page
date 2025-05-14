# Unity Page 시스템

> **주의사항**: 본 문서는 LLM(대규모 언어 모델)을 기반으로 작성되었으며, 실제 구현과 다소 차이가 있을 수 있습니다. 정확한 사용법은 원본 코드를 참조하시기 바랍니다.

## 개요

페이지 전환 및 UI 페이드 효과를 담당하는 시스템입니다. CanvasGroup 기반의 비동기 페이드 인/아웃, 이벤트 기반 상태 전환, 디버그 로깅 기능을 제공합니다.

## 원작성자

- @creator-hian

## 기여자

- @creator-hian

# 시스템 요구사항

Unity 6000.0 버전 이상을 요구합니다.

# 1. 사용 방법

기본적인 페이지 전환 시스템은 다음과 같이 사용할 수 있습니다:

```csharp
// 페이지 전환 예시
public class PageManager : MonoBehaviour
{
    [SerializeField] private PageView mainPage;
    [SerializeField] private PageView subPage;

    public void SwitchToMainPage()
    {
        mainPage.SetActive(true, animate: true);
        subPage.SetActive(false, animate: true);
    }

    public void SwitchToSubPage()
    {
        mainPage.SetActive(false, animate: true);
        subPage.SetActive(true, animate: true);
    }
}
```

# 2. 주요 기능

## 주요 기능 목차

[1. 핵심 컴포넌트](#핵심-컴포넌트)
[2. 확장 모듈](#확장-모듈)
[3. 디버그 로깅](#디버그-로깅)

### 핵심 컴포넌트

<details>
<summary>Click to expand</summary>
<div style="padding-left: 20px;">

Page 시스템은 다음과 같은 핵심 컴포넌트로 구성됩니다:

1. **IPage 인터페이스**

   - 페이지의 기본 동작 인터페이스
   - 페이지 활성화/비활성화, 전환 이벤트 제공
   - 페이드 애니메이션, 상태 변경 이벤트 정의

2. **PageView**

   - `IPage` 인터페이스 구현체
   - `CanvasGroup`을 활용한 페이드 인/아웃 구현
   - 비동기 전환 지원
   - 전환 시작/완료, 활성/비활성 이벤트 제공

3. **PageLogger**
   - Page 시스템 전용 중앙 집중식 로거
   - `ENABLE_PAGE_DEBUG_LOG` 심볼이 정의된 경우에만 디버그 로그 출력
   - 유지보수 및 디버깅 편의성 향상

**Usage:**

```csharp
// IPage 이벤트 활용 예시
public void InitializePage(IPage page)
{
    page.OnEnterActivate += () => Debug.Log("페이지 활성화 시작");
    page.OnExitActivate += () => Debug.Log("페이지 활성화 완료");
    page.OnEnterDeactivate += () => Debug.Log("페이지 비활성화 시작");
    page.OnExitDeactivate += () => Debug.Log("페이지 비활성화 완료");
}
```

</div>
</details>

<hr />

### 확장 모듈

<details>
<summary>Click to expand</summary>
<div style="padding-left: 20px;">

Page 시스템은 확장성을 고려하여 다양한 확장 모듈을 제공합니다:

1. **VContainer 확장**

   - VContainer 의존성 주입을 지원하는 PageView 구현
   - 표준 .NET Task를 사용하여 비동기 처리
     **계층형 ASMDEF 구조:**

```
Extensions/
├── VContainer/                   # VContainer만 필요한 확장
│   ├── FAMOZ.Extension.VContainer.asmdef
│   └── _Default/
│       └── PageView.cs           # Task 사용 구현
```

**Usage:**

```csharp
// VContainer 확장 사용 예시
builder.Register<PageView.IInjectionConfig>(c => new PageViewConfig(fadeSpeed: 2.0f));
// 페이지 구성 요소를 컨테이너에 등록
builder.RegisterComponent(pageViewComponent);
```

</div>
</details>

<hr />

### 디버그 로깅

<details>
<summary>Click to expand</summary>
<div style="padding-left: 20px;">

Page 시스템은 효율적인 디버깅을 위한 통합 로깅 시스템을 제공합니다:

1. **PageLogger**

   - 정보, 경고, 오류 메시지를 중앙 집중적으로 처리
   - 컴파일 시간 최적화를 위한 조건부 로깅

2. **PageDefineSymbolManager (에디터 전용)**
   - `ENABLE_PAGE_DEBUG_LOG` 심볼을 Unity 메뉴에서 토글하는 에디터 스크립트
   - **Tools > Page Debug Log** 메뉴를 통해 디버그 로그 활성/비활성 가능

**Usage:**

```csharp
// 로깅 예시
PageLogger.Log("페이지 초기화 완료");
PageLogger.LogWarning("페이지 전환 중 지연 발생");
PageLogger.LogError("CanvasGroup 컴포넌트를 찾을 수 없음");
```

</div>
</details>

<hr />

# 3. 기타 등등

## 폴더 및 파일 구조

```
Page/
├── Core/                    # 핵심 상수 및 설정
│   └── Const.cs             # 디버그 심볼 문자열 상수 정의
│
├── Runtime/                 # 런타임 핵심 구성 요소
│   ├── View/                # 페이지 뷰 관련 구성 요소
│   │   └── IPage.cs         # 페이지 인터페이스 정의
│   │
│   └── Logging/             # 로깅 시스템
│       └── PageLogger.cs    # 중앙 집중식 디버그 로거
│
├── Editor/                  # Unity 에디터 전용 스크립트
│   └── PageDefineSymbolManager.cs
│
└── Extensions/              # 확장 모듈
    ├── VContainer/          # VContainer 확장
    │   └── _Default/
    │       └── PageView.cs  # Task 기반 구현
```

## 종속 모듈

- com.creator-hian.logging (1.0.0) - 로깅 시스템 제공
- VContainer (선택적, Extensions/VContainer 사용 시 필요)

# 4. 변경 로그

버전별 변경 내역은 CHANGELOG.md 파일을 참조하세요.
