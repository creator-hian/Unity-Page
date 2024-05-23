# FAMOZ Base Package

## 개요

> 해당 Pacakge의 기본적인 내용을 작성해주세요. 실제 작성시에 이 Callout은 제거해 주세요.

## 원작성자

- 작성자 이름 기재

## 기여자

# 시스템 요구사항

> 시스템의 요구사항을 기재해 주세요. 실제 작성시에 이 Callout은 제거해 주세요.

Unity 2021.3 버전 이상을 요구합니다.<br>

# 설치

## manifest.json 파일의 scopedRegistries 수정을 통한 패키지 연동

- 프로젝트 폴더/Packages 폴더 내부의 manifest.json 파일에 해당 구문을 추가하고 저장하면, UnityEditor에서 해당 패키지를 인식하고 UI상에 표현합니다.<br>

```json
{
  "scopedRegistries": [
    {
      "name": "FAMOZ",
      "url": "http://famoz.iptime.org:20090/",
      "scopes": [
        "com.famoz"
      ]
    }
  ],
  "dependencies": {
  }
}
```

# 1. 사용 방법

> 해당 Package를 유효하게 사용하기 위한 방법을 기재해주세요. 가급적이면 간단한 예시가 좋습니다.<br>
> 실제 작성시에 이 Callout은 제거해 주세요.

# 2. 주요 기능

## 주요 기능 목차

> 주요 기능들의 목차를 링크와 함께 작성해주세요.
> <br>실제 작성시에 이 Callout은 제거해 주세요.

[1. 예제](#예제)


### 예제

> 해당 기능의 설명을 간략히 기재해 주세요.

<details>
<summary>Click to expand</summary>
<div style="padding-left: 20px;">

> 해당 기능의 세부 설명을 기재해 주세요.

**Usage:**

> 해당 기능의 사용 예시를 작성해주세요. 코드 블럭과 함께 기재헤 주세요.
```csharp

```

</div>
</details>

<hr />

# 3. 기타 등등

## 종속 모듈

> 해당 Package를 사용하는데 반드시 필요한 Package를 기재해주세요.

- 없습니다.

# 4. 변경 로그

> 각 버전별로 구분하여 기재해 주세요.

## ver 1.0.0

<details>
<summary>Click to expand</summary>
<div style="padding-left: 20px;">

### 변경일

- yyyy-MM-dd

### 기여자

- 작성자 이름 (이메일주소)

### 변경사항
</div>
</details>