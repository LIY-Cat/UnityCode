using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    /*
    *간단하게 플레이어 조작하고 점수 올리 관련
    TODO: 'this.' << delete (rigid,add_score,walkForce,jumpForce)
    */
        private Rigidbody rigid;//밀도
        private float jumpForce = 250.0f;//점프 높이
        private float walkForce = 5.0f;//한번에 이동 할 거리
        private GameObject add_score;/*점수 올리기 */
        private int score;//초기 점수

    /*
    * *게임 시작시 필요한 부분을 설정하는 함수
    */
    void Start()
    {
        Start_setUp();
        rigid = GetComponent<Rigidbody>();
        add_score = GameObject.Find("Score");
    }

    // Update is called once per frame
    void Update()
    {
        Player_controler();
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("냠냠");
        score += 10;
        if(score >= 0){
            add_score.GetComponent<Text>().text = "Score: " + score.ToString();
        }
        Destroy(other.gameObject);
    }

    void Player_controler(){
        //점프
        if(Input.GetKeyDown(KeyCode.Space) && rigid.velocity.y == 0.0f){
            //트랜스폼안에 힘을위로 가하는 게 있다.
            rigid.AddForce(transform.up * jumpForce);
        }
        //좌우이동
        int key = 0;
        //Debug.Log("Move!");//콘솔창에 띄운다. 디버그 작업
        if(Input.GetKey(KeyCode.RightArrow)){
            key = 1;
            rigid.AddForce(transform.right * key * walkForce);

        }else if(Input.GetKey(KeyCode.LeftArrow)){
            key = -1;
            rigid.AddForce(transform.right * key * walkForce);

        }else if(Input.GetKey(KeyCode.UpArrow)){
            key = 1;
            rigid.AddForce(transform.forward * key * walkForce);

        }else if(Input.GetKey(KeyCode.DownArrow)){
            key = -1;
            rigid.AddForce(transform.forward * key * walkForce);
        }
    }

    /*
    *초기 셋팅
    */
    void Start_setUp(){
        score = 0;
    }
}
