# Changelog

Unity Page 패키지의 모든 주요 변경 사항이 이 파일에 기록됩니다.

형식은 [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)를 기반으로 하며,
이 프로젝트는 [Semantic Versioning](https://semver.org/spec/v2.0.0.html)을 준수합니다.

## [1.1.0] - 2025-05-14

### 변경

- 패키지 내부 구조 및 기능 개선
- 버전 업데이트 (1.0.0 → 1.1.0)
- 종속성 패키지 버전 업데이트 (1.0.0 → 1.0.1)
- 종속성 패키지 이름 변경 (`com.famoz.future-ocean.logging` → `com.creator-hian.unity.logging`)
- 로깅 시스템을 조건부 활성화 기능 추가 (Logging 패키지가 존재할 때만 확장 기능 활성화)
- `StandaloneDefineSymbolManager` 추가 - Logging 패키지 없이도 LogSymbol 관리 가능
- 조건부 컴파일 적용 - Logging 패키지 존재 여부에 따라 적절한 관리자 활성화

## [1.0.0] - 2025-04-22

### 추가

- 초기 릴리스
- 핵심 인터페이스 구현
  - `IPage` 페이지 관리 인터페이스
  - `PageView` CanvasGroup 기반 구현체
- 로깅 시스템 구현 (`PageLogger`)
- 디버그 로그 심볼 관리 시스템 추가
- Core, Runtime, Editor, Extensions 기본 구조 설정
- `.asmdef` 파일 구성 및 종속성 설정

### 종속성

- com.famoz.future-ocean.logging (1.0.1)
- VContainer (Optional, VContainer Extension 사용 시 필요)
