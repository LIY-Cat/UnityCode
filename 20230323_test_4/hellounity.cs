using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hellounity : MonoBehaviour
{
    //시간알아보기
    private float waitTime = 2.0f;
    private float timer = 0.0f;
    private float visualTime = 0.0f;
    //private float scrollBar = 1.0f;
    private int count = 0;

    void Test(){
        string str = "happy ";
        int num = 123;
        string message = str + num;
        Debug.Log(message);
        //return;
    }

    void arry(){
        int[] points = {83, 99, 52, 93, 15};
        int sum = 0;
        for(int p = 0; p < points.Length; p++){
            sum += points[p];
        }
        int average = sum / points.Length;
        Debug.Log("평균값" + average);
    }

    // Start is called before the first frame update
    void Start()
    {
        string str1 = "happy ";
        string str2 = "birthday";
        string message;
        message = str1 + str2;
        Debug.Log("Hello Untiy!!");
        Debug.Log(message);
        //따로 문자열을 선언하지 않고 넣기
        str1 += str2;
        Debug.Log(str1 + "다르게 선언함");
        Debug.Log("Test 함수를 콜");
        Test();
        Debug.Log("arry 함수 콜");
        arry();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > waitTime){
            visualTime = timer;
            count++;
            Debug.Log("2초 지났습니다.");
            Debug.Log(timer + "초 지났습니다. 카운트: " + count);

            //2초 삭제
            timer -= waitTime;
            //Time.timeScale = scrollBar;
        }
        
    }



}
