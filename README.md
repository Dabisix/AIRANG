# 아이랑(AiRang)
<img src="https://user-images.githubusercontent.com/33210124/202468753-2d77b037-ba22-4aef-bf91-7a1c641beca1.png" width="100%"><br>

## ✨ [UCC]() 


## ✨ Overview
아이의 상상력을 최대로! 아이랑 함께하는 AR 동화책

## 👀 아이랑(AiRang) 서비스 화면


## ✨ 주요 기능
- 회원 관련
	- 회원가입(이메일 중복 검사, 형식 검사)
	- 로그인
	- 로그아웃
	- 내 정보 수정
	- 즐겨찾기, 방금 읽은 동화 목록 제공
- 카테고리별 동화 목록 제공
	- 전체 동화
	- 조회수 기반 인기 동화
	- AR 서비스 지원 동화
- 동화 추천
	- 즐겨찾기 한 동화와 유사한 동화 목록 제공
	- 방금 읽은 동화와 유사한 동화 목록 제공
- 동화 검색 : 제목 키워드 검색
- 동화
	- 언어 지원 : 한글, 영어
	- 나레이션 지원
		- 기본
		- 부모 : 목소리 녹음 기능
	- 즐겨찾기
	- 체크포인트 기록
	- 내용 구현
		- AR
		- 그림(애니메이션&모델&환경 배치)
- 아이랑 한 컷
	- AR
	- 화면 캡쳐

## 🖥️ 개발 환경


**Management Tool**
- 형상 관리 : Gitlab
- 이슈 관리 : Jira
- 커뮤니케이션 : Mattermost, Webex, Notion
- 디자인 : Figma, Adobe Illustrator, Adobe  Photoshop, Adobe  After Effect, Blender, AutoDesk Maya

**🐳 Backend**
- Spring boot `3.0.0`
- JPA

**🦊 Frontend**
- Unity `2021.3.11f`
    - ARCore XR Plugin `4.2.6`
    - AR Foundation `4.2.6`
    - Recorder `3.0.3`
- Android Studio
- Vue 3

**🗂️ DB**
 - MySql

**🌐 Server**
- AWS EC2
- Docker `20.10.21`

**🔨 IDE**
- Unity
- IntelliJ

## 💫 시스템 아키텍처

<img src="https://user-images.githubusercontent.com/33210124/202470394-6b4e3e8a-1230-4b5d-b01a-3516de614a27.png" alt="시스템 아키텍처" width="80%">


## ✨ 기술 특이점
- AR을 활용한 3D 모델 동화책과의 상호작용 구현
- 3D 모델과 애니메이션을 활용하여, 몰입감있는 동화책 구현

## 💭 요구사항 정의 및 기능 명세


<img width="100%" alt="요구사항 정의 및 기능 명세" src="https://user-images.githubusercontent.com/33210124/202905788-4de73d40-b1b9-4ecd-a25d-39e32eea33ca.png"><br>
[요구사항 정의 및 기능 명세](https://airang.notion.site/dece2c92728a47aaabd5b2131261857d)

## 🎨 화면 설계서


<img width="100%" alt="화면설계서" src="https://user-images.githubusercontent.com/33210124/202905871-ba6c12c0-819b-4a19-9313-784fb9d625bd.png"><br>
[화면 설계서](https://airang.notion.site/d82e71af605943bebbe9c56e7680af2c)

## ✨컨벤션
[Conventions](https://airang.notion.site/Conventions-1a96caa3160b4881a5a0843be1b76f1f)

## ✨ ER Diagram

<img width="100%" alt="erd" src="https://user-images.githubusercontent.com/33210124/202905948-469fc27e-ea25-476b-a424-68a93c53ae57.png)" ><br>
[ER Diagram](https://airang.notion.site/E-R-Diagram-74b7eccd41c8428387125c19a1c601da)


## 💞 아이랑(AiRang)을 개발한 오라이(ALL I) 팀원을 소개합니다!

|**[육다빈](https://github.com/Dabisix)**|**[박서은](https://github.com/seoeun98)**|**[우상욱](https://github.com/YeoUlFox)**|**[김수빈](https://github.com/kimsubni)**|**[이화연](https://github.com/LeeHwayeon)**|**[정수빈](https://github.com/Soobin07)** |
| :---------------------------------------------------------------------------------------------------------------------------: | :---------------------------------------------------------------------------------------------------------------------------: | :---------------------------------------------------------------------------------------------------------------------------: | :---------------------------------------------------------------------------------------------------------------------------: | :---------------------------------------------------------------------------------------------------------------------------: | :---------------------------------------------------------------------------------------------------------------------------: |
| <img src="https://avatars.githubusercontent.com/u/80896077?v=4" width="800"> | <img src="https://avatars.githubusercontent.com/u/105689406?v=4" width="800"> | <img src="https://avatars.githubusercontent.com/u/41969902?v=4" width="800"> | <img src="https://avatars.githubusercontent.com/u/81076792?v=4" width="800"> | <img src="https://avatars.githubusercontent.com/u/33210124?v=4" width="800"> | <img src="https://avatars.githubusercontent.com/u/45987276?v=4" width="800"> |
|Leader & Backend|Backend|Frontend|Frontend|Frontend|Frontend|

## 😃 팀원 역할

- **육다빈**
  - 데이터 전처리, 동화 관련 DB설계 및 API 구현
- **박서은**
  - 배포, 유저&나레이션 관련 DB 설계 및 API 구현, 동화
- **우상욱**
  - 어플리케이션 시스템 설계, UI 기능 구현, AR
- **김수빈**
  - 디자인, AR, 동화, UCC
- **이화연**
  - AR, 동화, 서비스 웹 페이지
- **정수빈**
  - AR, 동화, 아이랑 한컷
