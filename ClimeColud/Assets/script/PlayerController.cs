using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour{
    Rigidbody2D rigid2D;
    Animator animator;
    float jumpForce = 680.0f;
    float walkForce = 30.0f;
    float maxWalkSpeed = 2.0f;
    private int key = 0;
    private float speedx = 0.0f;
    //private SpriteRenderer spriteRenderer;
    void Start(){
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update(){
        // 점프한다.
        if(Input.GetKeyDown(KeyCode.Space) && this.rigid2D.velocity.y == 0.0f) {
            this.animator.SetTrigger("JumpTrigger");
            this.rigid2D.AddForce(transform.up * this.jumpForce);
        }

        // 좌우 이동 & 움직이는 방향에 따라 반전
        /*
        *transform.rotation = Quaternion.Euler(0, 0, 0); 오브젝트의 회전조절(움직임에 영향을 준다.)
        *한쪽으로만 가는 문제가 발생함
        *spriteRenderer.flipX = true; 이미지 자체 반전
        *성능등에 영향을 미치는 것 같다.
        */
        key = 0;
        if(Input.GetKey(KeyCode.RightArrow)){
            //transform.rotation = Quaternion.Euler(0, 0, 0);
            //spriteRenderer.flipX = false;
            key = 1;
        }
        else if(Input.GetKey(KeyCode.LeftArrow)){
            //transform.rotation = Quaternion.Euler(0, 180, 0);
            //spriteRenderer.flipX = true;
            key = -1;
        }

        // 플레이어 속도
        //Mathf.Abs() 절대값 반환
        speedx = Mathf.Abs(this.rigid2D.velocity.x);

        // 움직이는 방향에 따라 반전
        /*
        *transform.localScale  오브젝트의 크기조절
        *transform.localScale = new Vector2(key, 1); 도 돼지만
        *transform.localScale 이게 Vector3로 되어 있다.
        */
        if(key != 0) {
            transform.localScale = new Vector3(key, 1, 1);
        }

        // 플레이어 속도에 맞춰 애니메이션 속도를 바꾼다.
        if (this.rigid2D.velocity.y == 0){
            //걷기 애니메이션 속도
            this.animator.speed = speedx / 2.0f;
        }
        else{
            //점프 애니메이션 속도
            this.animator.speed = 1.0f;
        }

        // 스피드 제한
        if(speedx < this.maxWalkSpeed) {
            /*
            *AddForce에는 여러 물리작용 모드가 있다.
            *ForceMode.Force 또는 ForceMode2D.Force: 연속적인 힘을 적용합니다. 이 옵션은 기본 값입니다.
            *ForceMode.Impulse 또는 ForceMode2D.Impulse: 순간적인 힘(충격)을 적용합니다.
            *ForceMode.Acceleration: 질량의 영향을 받지 않는 연속적인 가속도를 적용합니다.
            *ForceMode.VelocityChange: 질량의 영향을 받지 않는 순간적인 속도 변화를 적용합니다.
            */
            this.rigid2D.AddForce(transform.right * key * this.walkForce);
        }

        if(transform.position.y < -5.0) {
            SceneManager.LoadScene("GameScene");
        }
    }

    // 목표지점 도착
    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("목표지점 도착!");
        SceneManager.LoadScene("ClearScene");
    }
}
