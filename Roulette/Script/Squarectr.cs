using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squarectr : MonoBehaviour{

    private GameObject square;
    private float speed = 3.0f;
    //private float rotation;

    void Start(){
        square = GameObject.Find("Square");
    }

    void Update(){
        Handle();
    }

    void Handle(){
        float move = Input.GetAxisRaw("Horizontal");
        float move2 = Input.GetAxisRaw("Vertical");
        square.transform.Translate(move * speed, move2 * speed, 0);

            //square.transform.Translate(rotation * Time.deltaTime, 0, 0);
    }
}
