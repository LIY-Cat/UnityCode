using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
        Rigidbody rigid;//밀도 설정 테이터타입
        float jumpForce = 250.0f;
        float walkForce = 5.0f;
        GameObject scoren;
        int score = 0;
    // Start is called before the first frame update
    void Start()    
    {
        this.rigid = GetComponent<Rigidbody>();
        this.scoren = GameObject.Find("Score");
    }

    // Update is called once per frame
    void Update()
    {   
        //점프
        if(Input.GetKeyDown(KeyCode.Space) && this.rigid.velocity.y == 0.0f){
            //트랜스폼안에 힘을위로 가하는 게 있다.
            this.rigid.AddForce(transform.up * this.jumpForce);
        }
        //좌우이동
        int key = 0;
        //Debug.Log("Move!");//콘솔창에 띄운다. 디버그 작업
        if(Input.GetKey(KeyCode.RightArrow)){
            key = 1;
            this.rigid.AddForce(transform.right * key * this.walkForce);

        }else if(Input.GetKey(KeyCode.LeftArrow)){
            key = -1;
            this.rigid.AddForce(transform.right * key * this.walkForce);

        }else if(Input.GetKey(KeyCode.UpArrow)){
            key = 1;
            this.rigid.AddForce(transform.forward * key * this.walkForce);

        }else if(Input.GetKey(KeyCode.DownArrow)){
            key = -1;
            this.rigid.AddForce(transform.forward * key * this.walkForce);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("냠냠");
        score += 10;
        if(score >= 0){
            this.scoren.GetComponent<Text>().text = "Score: " + score.ToString();
        }
        Destroy(other.gameObject);
    }
}
