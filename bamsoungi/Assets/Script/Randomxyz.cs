using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Randomxyznamespace{
    public class Randomxyz : MonoBehaviour{
        public static Randomxyz Instance;  // 싱글턴 인스턴스 접근 해결

        private float randomX = 0f;
        private float randomY = 0f;
        private float randomZ = 0f;
        private float randomForce = 0f;
        /*
        *밤송이 끼리 부딪히면은 카운트 (이미 카운트를 했으면은 카운트 x 밤송이 컨트롤러 스크립트에서 제어)
        */
        private int bamsongiWithTheSameCount = 0;

        private void Awake() {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }
        public void SetRandomX(float set){
            randomX = set;
        }
        public void SetRandomY(float set){
            randomY = set;
        }
        public void SetRandomZ(float set){
            randomZ = set;
        }
        public void SetRandomForce(float set){
            randomForce = set;
        }

        public void AddBamsongiWithTheSameCount(int add){
            bamsongiWithTheSameCount += add;
        }
        
        public float GetRandomX(){
            return randomX;
        }
        public float GetRandomY(){
            return randomY;
        }
        public float GetRandomZ(){
            return randomZ;
        }
        public float GetRandomForce(){
            return randomForce;
        }
        public int GetBamsongiWithTheSameCount(){
            return bamsongiWithTheSameCount;
        }
    }
}