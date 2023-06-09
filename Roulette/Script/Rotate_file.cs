using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_file : MonoBehaviour{
    private float rotSpeed = 0;  // 회전 속도  

    void Start(){

    }

    void Update(){
        // 클릭하면 회전 속도를 설정한다
        if (Input.GetMouseButtonDown(0)){
            rotSpeed = 10;
        }

        // 회전 속도만큼 룰렛을 회전시킨다
        transform.Rotate(0, 0, rotSpeed);
        if(rotSpeed <= 10){
            rotSpeed *= 0.988f;
        }else if(rotSpeed <= 0){
            rotSpeed *= 0.0f;
        }
        //Debug.Log(rotSpeed);
    }
}