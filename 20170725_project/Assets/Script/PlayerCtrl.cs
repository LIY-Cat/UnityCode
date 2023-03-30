using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour{
    /*
    *간단하게 플레이어 조작하고 점수 올리 관련
    *Rigidbody 밀도와 관련된 메소드
    *GameObject 게임 오브젝트 메소드
    *playerRigidbody: 키 입력을 받아 움직이게 한다.
    *jumpForce: 최대 점프 높이
    *walkForce: 한 번 움직이는 거리 >> 
    ?Time.deltaTime로 움직이게 할 수 있다. 테스트 안함
    *score_object: Score라는 게임오브젝트를 찾아서 점수를 올려준다.
    *score: 실제 점수
    *[SerializeField]는 에디터 안에서 실시간으로 변경이 가능하나 코드는 바뀌지 않는다.
    TODO: 분석해서 코드 최적화
    */
        private Rigidbody playerRigidbody;
        [SerializeField] private float jumpForce = 250.0f;
        [SerializeField] private float walkForce = 5.0f;
        private GameObject scoreObject;
        private int score = 0;
        private Text scoreText;


    /*
    *게임 시작시 필요한 부분을 설정하는 함수
    *AWake() init 메소드
    *FixedUpdate() 메소드: 프레임당 업데이트 물리적인 코드 사용
    *Update() 메소드는 프로그램 로직 담당
    */
    void Awake(){

    }
    void Start(){
        playerRigidbody = GetComponent<Rigidbody>();
        scoreObject = GameObject.Find("Score");
        scoreText = scoreObject.GetComponent<Text>();
        Vector3 temp = new Vector3(0, 0, 1);
        playerRigidbody.AddForce(temp.normalized * 0.1f);
        MeasureDistance("item");
    }

    void Update(){
        HandlePlayerMovement();
        Vector3 temp = new Vector3(0, 0, 1);
        playerRigidbody.AddForce(temp.normalized * 0.1f);
        MeasureDistance("item (4)");
    }

    void FixedUpdate(){
        //Vector3 temp = new Vector3(0, 0, 1);
        //playerRigidbody.AddForce(temp.normalized * 0.1f);
        //MeasureDistance("item (4)");
    }

    /*
    *플레이어하고 아이템 거리를 보여준다. 여기서 플레이어가 움직일 때마다 거리를 콘솔창에 출력해준다.
    */
    void MeasureDistance(string name){
        float objectMD;
        GameObject objectToMeasure = GameObject.Find(name);
        GameObject objectDistance = GameObject.Find("Player");
        if(objectToMeasure != null && objectDistance != null){
            objectMD = Vector3.Distance(objectToMeasure.transform.position, objectDistance.transform.position);
            Debug.Log("플레이어하고 "+ name +"의 거리가 " + objectMD + "떨어져있습니다.");
        }else{
            Debug.LogError("오브젝트: " + name + "를(을) 못찾았습니다.");
            Debug.LogError("오브젝트: Player를(을) 못찾았습니다.");
        }
    }

    /*
    *점수등 계산 메소드
    TODO: 나중에 파일 나누기 할 것
    */
    void OnTriggerEnter(Collider other){
        score += 10;
        if(score >= 0){
            scoreText.text = "Score: " + score.ToString();
        }
        Debug.Log("이름: " + other.ToString());
        Destroy(other.gameObject);
    }
    /*
    *1) void OnTriggerEnter(Collider other) : Trigger에 들어갔을 때
    *2) void OnTriggerStay(Collider other) : Trigger안에 있을때
    *3) void OnTriggerExit(Collider other) : Trigger를 벗어 날때

    *void OnCollisionEnter(Collision other) : Collision에 들어 갔을 때
    *void OnCollisionStay(Collision other) : Collision과 충돌하고 있는 중
    *void OnCollisionExit(Collision other) : Collision과 충동에서 벗어 났을 때

    */

    /*
    *플레이어 컨트롤러 NPC등 움직임
    TODO: 나중에 파일 나누기 할 것
    */
    void HandlePlayerMovement(){
        /*
        *점프
        */
        if(Input.GetKeyDown(KeyCode.Space) && playerRigidbody.velocity.y == 0.0f){
            playerRigidbody.AddForce(transform.up * jumpForce);
        }

        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        playerRigidbody.AddForce(movement.normalized * walkForce);

        /*
        Vector3 horizontalMovement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);//A, D or <, >;
        Vector3 verticalMovement = new Vector3(0, 0, Input.GetAxisRaw("Vertical"));//W, S or ^, 아래 방향키;
        playerRigidbody.AddForce((horizontalMovement + verticalMovement).normalized * walkForce);
        */
        /*
        *이동: 앞,뒤,좌,우
        *horizontalMovement == x
        *verticalMovement == z
        *else if문을 사용하면은 누적이 안된다. >> swith문으로 하면은 더 빨리 받는 다.
        *(horizontalMovement + verticalMovement)>>.normalized<<: '-1,0,1'데이터의 값을 반환
        */
        /*Vector3 horizontalMovement = new Vector3();
        Vector3 verticalMovement = new Vector3();
        if (Input.GetKey(KeyCode.RightArrow)){
            horizontalMovement += new Vector3(1, 0, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow)){
            horizontalMovement += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow)){
            verticalMovement += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.DownArrow)){
            verticalMovement += new Vector3(0, 0, -1);
        }
        rigid.AddForce((horizontalMovement + verticalMovement).normalized * walkForce);
        */

        
        /* *디버그 작업
        *Debug.Log("Move!");
        */
    }

    /*
    *디버깅등 테스트에 사용되는 메소드
    */
    void DebuggnigTest(){

    }
    void DebuggnigTest(string d_Test){

    }
}
