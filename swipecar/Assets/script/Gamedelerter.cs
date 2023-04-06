using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // UI를 사용하는 데 필요하다
using 
//GameDirector
public class Gamedelerter : MonoBehaviour{
    GameObject car;
    GameObject flag;
    GameObject distance;
    
    // Start is called before the first frame update
    void Start(){
        this.car = GameObject.Find("car");
        this.flag = GameObject.Find("flag");
        this.distance = GameObject.Find("Text");        
    }

    // Update is called once per frame
    void Update(){
        Dis();
    }

    void Dis(){
        float length = this.flag.transform.position.x - this.car.transform.position.x;
        this.distance.GetComponent<Text>().text = "목표지점 " + length.ToString("F2") + "m";
        Debug.Log(length);
        Gameover(length);
    }

    void Gameover(float d_length){
        if(d_length <= 0.0 || d_length >= 30.0){
            Debug.Log(d_length);
            editorapplication.exit(0)
        }
    }
}
