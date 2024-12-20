/*
 * 카드 분배 턴이 오면 카드 풀리에서 모두에게 나눠주는거 하는중
 * 카드 풀링에서 리스트로 만들어놓을지 고민중
 * 현재 카드의 합이 이상하게 나옴 어디서 잘못된건지 파악해야함.
 * 받을지 넘길지 선택하는 창에 보유한 카드를 보여줘야함
 * 그리고 다음 턴으로 넘김.
 * 선택창에서 Hit을 햇을때 다음 Hit이 작동은 하나 card추가나 이미지가 안바뀜
 * 현재 몇점인지 / 몇번째 자리에서 진행하는지 모르겠으니까 알려주는 ui있어야함.
 * 에이스 카드일 경우 1 or 11판단하는 기능
 * Hit했을때 만약에 넘어가면 Busted잠깐 뛰어주고 넘어가기 
 * ace카드여도 +11했을때 21을 넘어간다면 안나오게 해야함
 * ace카드가 11일때 다음카드의 합이 21을 넘기게 되면 다시 11을 다시 1로 바꿔줘야함.
 * 1과 11을 선택했을때 선택창의 합도 최신화 해줘야함.
 * 블랙잭에 10도 포함시켜야함
 * 11을 눌렀는데 다시 선택차의 합이 최신화가 되지를 않음.
 * 5장보여주고 bust가 아니라면 넘어가고 bust라면 bust ui보여주고 넘어가야함.
 * selectactgionpanelscript => 한번만 적용되게 만들어줘야함 / 그리고 5장 넘으면 턴 넘어가지는것도 있어야 함
 * 그리고 1 1 에서 11을 선택하고 다음장 10이 나왔으면 1 + 11 + 10 은 21은 넘겼기때문에 11을 다시 1로 만들어줘서 1 + 1 + 10 해서 12가 되어야 하는데 그냥 10이 되버림.
 * 초과했을때 버튼 클릭을 막아야 됨.
 * 배팅 동전 중앙 말고 옆으로 좀 옮겼으면 좋겠음
 * 게임컨트롤ㄹ러 -> 컴페어함수 -> 비교시작
 * 다시 선택이 안됨
 * 칩 안사라짐
 * 결과발표 보여주기
 * 돈이 0원이면 배팅 10원 돌려받고 자동으로 넘어가야함. 
 * 돈이 -400 / 생명 -1 과 같이 차액때문에 음수가 됨 
 * 하트 구매 골드 구매 UI  
 * 배팅할때마다 배팅금액 차감
 * 돈 시스템  
 * 현재 한 라운드가 끝나면 돈과 생명력이 안보임
 * 게임오버 / 게임승리 ui
 * 승리하면 승리 배당 수령
 * 데이터 저장
 * 
 * 해야할것 =>
 * 
 * 스플릿시스템 구현
 *
 * 리팩토링 및 구조 수정 =>
 *  게임 컨트롤러에서 현재 어디 자리의 위치인지 파악한다.
 *  좌석이 꺼졌다가 다시 켜지면 init되는 enable을 사용한다.
 *  게임 윈 오버 체크를 나눠서 할 필요가 있나?
 *  save data도 이벤트로 묶거나 Action으로 간단하게 호출
*/