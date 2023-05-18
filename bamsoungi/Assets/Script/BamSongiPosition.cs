using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BamSongiPositionNameSpace{
    public class BamSongiPosition : MonoBehaviour{
        public static BamSongiPosition Instance;
        private float x = 0.0f;
        private float y = 0.0f;
        private float z = 0.0f;

        private void Awake() {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        public void SetPosition(float x, float y, float z){
            this.x = x; this.y = y; this.z = z;
        }

        public void SetPosition(Vector3 position){
            x = position.x; y = position.y; z = position.z;
        }

        public float GetPositionX(){
            return x;
        }

        public float GetPositionY(){
            return y;
        }

        public float GetPositionZ(){
            return z;
        }
    }
}