using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ItemGenerator : MonoBehaviour{
    public GameObject applePrefab;
    public GameObject bamsongiPrefab;
    public GameObject bombPrefab;

    float span = 1.0f;
    float delta = 0;
    int ratio = 2; // bomb : 20%, bamsongi 20%, apple : 60% 생성
    int ratioB = 4;
    float speed = -0.03f;

    public void SetParameter(float span, float speed, int ratio){
        this.span = span;
        this.speed = speed;
        this.ratio = ratio;
    }

    void Update(){
        this.delta += Time.deltaTime;
        if(this.delta > this.span){
            GameObject item;
            int dice = Random.Range(1,11);
            this.delta = 0;

            if(dice <= this.ratio){
                item = Instantiate(bombPrefab) as GameObject;
            }else if(dice <= ratioB){
                item = Instantiate(bamsongiPrefab) as GameObject;
            }else{
                item = Instantiate(applePrefab) as GameObject;
            }

            float x = Random.Range(-1,2);
            float z = Random.Range(-1,2);
            item.transform.position = new Vector3(x, 4, z);
            item.GetComponent<ItemController>().dropSpeed = this.speed;
        }
    }
}
