using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    GameObject timerText;
    GameObject pointText;
    GameObject generator;
    
    float time = 60.0f;
    float point = 0.0f;

    

    public void GetApple(){
        this.point += 100;
        Mathf.Round(this.point);
    }

    public void GetBamsonggi(){
        this.point /= 1.25f;
        Mathf.Round(this.point);
    }

    public void GetBomb(){
        this.point /= 2;
        Mathf.Round(this.point);
    }
    void Start()
    {
        this.timerText = GameObject.Find("Time");
        this.pointText = GameObject.Find("Point");
        this.generator = GameObject.Find("ItemGenerator");
    }

    void Update(){
        this.time -= Time.deltaTime;

        if(this.time < 0){
            this.time = 0;
            this.generator.GetComponent<ItemGenerator>().SetParameter(10000.0f,0.0f,0);
        }
        else if(0 <= this.time && this.time < 10 ){
            this.generator.GetComponent<ItemGenerator>().SetParameter(0.9f,-0.04f,3);
        }
        else if(10 <= this.time && this.time < 20 ){
            this.generator.GetComponent<ItemGenerator>().SetParameter(0.4f,-0.06f,6);
        }
        else if(20 <= this.time && this.time < 25 ){
            this.generator.GetComponent<ItemGenerator>().SetParameter(0.7f,-0.04f,4);
        }
        else if(25 <= this.time && this.time < 30 ){
            this.generator.GetComponent<ItemGenerator>().SetParameter(1.0f,-0.03f,2);
        }
        this.timerText.GetComponent<Text>().text = "Time: " + this.time.ToString("F1");
        this.pointText.GetComponent<Text>().text = this.point.ToString() + " point";
    }
}
