using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ScoreManagerNamespace{
    public class ScoreManager : MonoBehaviour{
        public static ScoreManager Instance;  // 싱글턴 인스턴스 접근 해결
        private GameObject scoreObject;
        private int score = 0;
        private Text scoreText;

        private int missTargetCount = 0;
        //잠시 테스트를 위한 변수
        private int one, two, three, four, five, six, seven, eight, nine = 0;

        private void Awake() {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        void Start(){
            scoreObject = GameObject.Find("Score");
            scoreText = scoreObject.GetComponent<Text>();
        }

        // 점수 설정 메서드
        public void SetScore(int set) {
            score = set;
        }

        // 점수를 가져오는 메서드
        public int GetScore() {
            return score;
        }

        public int GetMissTargetCount(){
            return missTargetCount;
        }

        public int GetOne() {
            return one;
        }

        public int GetTwo() {
            return two;
        }

        // 점수를 더하는 메서드
        public void AddScore(int add){
            score += add;
            if(score >= 0){
                scoreText.text = "점수: " + score.ToString() + "점";
            }
        }

        public void AddOne(){
            one++;
        }

        public void AddTwo(){
            two++;
        }

        public void AddMissTarget(){
            missTargetCount++;
        }

        // 점수를 빼는 메서드
        public void SubScore(int sub){
            score -= sub;
            if(score >= 0){
                scoreText.text = "점수: " + score.ToString() + "점";
            }
        }

    }
}