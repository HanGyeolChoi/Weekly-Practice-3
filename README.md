# Weekly-Practice-3
 꾸준 실습 3주차
 <br>

- 입문 주차와 비교해서 입력 받는 방식의 차이와 공통점을 비교해보세요.

입문 주차는 SendMessage를 사용해 GameObject 내의 코드에서 직접 함수 호출해 실행하는 방식(OnMove, OnLook 등등)

지금은 Invoke Unity Event를 사용해 키를 누르면 유니티의 event 기능을 사용해 Inspector에서 설정한 함수들을 호출하는 방식

<br>

- CharacterManager와 Player의 역할



<br>

- 핵심 로직 분석 (`Move`, `CameraLook`, `IsGrounded`)

Move : 크기가 1인 Vector2 값을 가진 방향 벡터에 speed값을 곱해서 그 방향으로 일정한 속도로 움직이고, y의 속도 (위아래)는 그대로 놔둠

CameraLook : 마우스 좌표 이동 delta값을 이용해  x축의 이동은 cameraContainer의 y축의 회전을 만들고, y축의 이동은 cameracontainer의 x축에 대한 회전을 만드는데 위아래의
			각도는 최대,최소 값을 정해 그 사이에서만 움직이게 만듬

IsGrounded : Raycast를 이용해 현재 지점의 아랫부분에서 4지점을 정해 아래방향으로 Ray를 쏘고, 그중 하나라도 GroundLayerMask와 같은 레이어의 물체가 맞는다면 True를 반환함

<br>

`Move`와 `CameraLook` 함수를 각각 `FixedUpdate`, `LateUpdate`에서 호출하는 이유

FixedUpdate는 호출 간격이 프레임 단위가 아니라 시간 간격으로 호출되기 때문에 일정한 주기로 같은 연산을 처리해야하는 rigidbody를 이용한 Move 함수는 FixedUpdate에서 이루어 지는 것이 더 정확한 물리 계산이 가능하다.

LateUpdate는 모든 Update가 끝난 후 호출되는 함수이다.
CaemeraLook은 Move에 의해 이동된 뒤에 카메라가 그 위치에 기반해 이동되기 때문에 LateUpdate에서 호출이 되어야 한다.


<br>

</br>

- 별도의 UI 스크립트를 만드는 이유에 대해 객체지향적 관점에서 생각해보세요.



- 인터페이스의 특징에 대해 정리해보고 구현된 로직을 분석해보세요.
- 핵심 로직을 분석해보세요. (UI 스크립트 구조, `CampFire`, `DamageIndicator`)

Condition - 현재 condition의 값(curValue)와 최대 값(maxValue)의 비율에 따라 Image의 fillAmount를 변화시킴
UICondition - Singleton으로 만들어진 instance의 player의 컨디션 값들을 저장하는 클래스.
PlayerCondition - UICondtion의 값들을 직접 변화시킬 때 쓰는 메소드를 모은 클래스

Campfire - collider 영역 안에 들어온 IDamagable를 상속받은 오브젝트들을 List에 저장하고, Coroutine을 사용해 (damageRate) 초마다 그 List 안의 오브젝트에 각각 TakeDamage()함수를 호출시켜 데미지를 줌

DamageIndicator - 데미지를 입을 때 Image를 활성화시키고, 활성화가 될 때마다 alpha값을 coroutine을 사용해 점점 떨어지게 구현함