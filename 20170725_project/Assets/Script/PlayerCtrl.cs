using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    /*
    *간단하게 플레이어 조작하고 점수 올리 관련
    *Rigidbody 밀도와 관련된 메소드
    *GameObject 게임 오브젝트 메소드
    *rigid: 키 입력을 받아 움직이게 한다.
    *jumpForce: 최대 점프 높이
    *walkForce: 한 번 움직이는 거리
    *score_object: Score라는 게임오브젝트를 찾아서 점수를 올려준다.
    *score: 실제 점수
    TODO: 분석해서 코드 최적화
    */
        private Rigidbody rigid;
        private float jumpForce = 250.0f;
        private float walkForce = 5.0f;
        private GameObject scoreObject;
        private int score = 0;
        private Text scoreText;

    /*
    * *게임 시작시 필요한 부분을 설정하는 함수
    */
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        scoreObject = GameObject.Find("Score");
        scoreText = scoreObject.GetComponent<Text>();
    }

    void Update()
    {
        Player_controler();
    }

    /*void FixedUpdate(){
        Player_controler();
    }*/
    /*
    *점수등 계산 메소드
    TODO: 나중에 파일 나누기 할 것
    */
    void OnTriggerEnter(Collider other)
    {
        score += 10;
        if(score >= 0){
            scoreText.text = "Score: " + score.ToString();
        }
        Debug.Log("이름: " + other.ToString());
        Destroy(other.gameObject);
    }

    /*
    *플레이어 컨트롤러 NPC등 움직임
    TODO: 나중에 파일 나누기 할 것
    */
    void Player_controler(){
        /*
        *점프
        */
        if(Input.GetKeyDown(KeyCode.Space) && rigid.velocity.y == 0.0f){
            rigid.AddForce(transform.up * jumpForce * Time.deltaTime);
        }
        Vector3 horizontalMovement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);//A, D or <, >
        Vector3 verticalMovement = new Vector3(0, 0, Input.GetAxisRaw("Vertical"));//W, S or ^, 아래 방향키

        rigid.AddForce((horizontalMovement + verticalMovement).normalized * walkForce * Time.deltaTime);
        
        /*
        *이동: 앞,뒤,좌,우
        *horizontalMovement == x
        *verticalMovement == z
        *else if문을 사용하면은 누적이 안된다.
        *Time.deltaTime 프레임 드랍시 밀리는 거 방지
        *(horizontalMovement + verticalMovement).normalized: 1의 값을 반환
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
        rigid.AddForce((horizontalMovement + verticalMovement).normalized * walkForce * Time.deltaTime);
        */

        
        /* *디버그 작업
        *Debug.Log("Move!");
        */
    }
}
