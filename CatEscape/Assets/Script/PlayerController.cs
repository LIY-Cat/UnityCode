using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip arrowSE;
    AudioSource aud;

    // Start is called before the first frame update
    void Start()
    {
        this.aud = GetComponent<AudioSource>();
    }
    public void LButtonDown()
    {
        transform.Translate(-3, 0, 0);
    }

    public void RButtonDown()
    {
        transform.Translate(3, 0, 0);
    }

    void Update()
    {
        // 왼쪽 화살표가 눌렸을 때
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Translate(-3, 0, 0); // 왼쪽으로 「3」 움직인다
        }

        // 오른쪽 화살표가 눌렸을 때
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Translate(3, 0, 0); // 오른쪽으로 「3」 움직인다
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        this.aud.PlayOneShot(this.arrowSE);
        Debug.Log("Arrow Collision!");
        
        // 감독 스크립트에 플레이어와 화살이 충돌했다고 전달한다 
        GameObject director = GameObject.Find("GameDirector");
        director.GetComponent<GameDirector>().DecreaseHp();                    

        // 충돌했다면 화살을 지운다
        Destroy(other.gameObject);      
    }
}
