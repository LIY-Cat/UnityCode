using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private GameObject scoreObject;
    private int score = 0;
    private Text scoreText;

    void Start(){
        scoreObject = GameObject.Find("Score");
        scoreText = scoreObject.GetComponent<Text>();
    }

    void OnTriggerEnter(Collider other){
        score += 10;
        if(score >= 0){
            scoreText.text = "점수: " + score.ToString() + "점";
        }
    }
}
