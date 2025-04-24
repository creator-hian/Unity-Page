# Changelog

미래해양과학관 Page 패키지의 모든 주요 변경 사항이 이 파일에 기록됩니다.

형식은 [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)를 기반으로 하며,
이 프로젝트는 [Semantic Versioning](https://semver.org/spec/v2.0.0.html)을 준수합니다.

## [1.0.1] - 2025-04-24

### 변경

- Logging 패키지 의존성을 1.0.0에서 1.0.1로 업데이트
- Memory Bank 문서화 시스템 구현

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
