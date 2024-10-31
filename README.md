# Weekly-Practice-3
 꾸준 실습 3주차

입문 주차와 비교해서 입력 받는 방식의 차이와 공통점을 비교해보세요.

입문 주차는 SendMessage를 사용해 GameObject 내의 코드에서 직접 함수 호출해 실행하는 방식(OnMove, OnLook 등등)

지금은 Invoke Unity Event를 사용해 키를 누르면 유니티의 event 기능을 사용해 Inspector에서 설정한 함수들을 호출하는 방식


CharacterManager와 Player의 역할


핵심 로직 분석 (`Move`, `CameraLook`, `IsGrounded`)


`Move`와 `CameraLook` 함수를 각각 `FixedUpdate`, `LateUpdate`에서 호출하는 이유