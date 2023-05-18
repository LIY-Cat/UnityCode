using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BasketPositionNameSpace{
    public class BasketPosition : MonoBehaviour{
        public static BasketPosition Instance;
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

        public void SetPositionX(float x){
            this.x = x;
        }
        public void SetPositionY(float y){
            this.y = y;
        }
        public void SetPositionZ(float z){
            this.z = z;
        }

        //테스트중...
        public void GetPostitonAll(out float x, out float y, out float z){
            x = this.x; y = this.y; z = this.z;
        }

        public Vector3 GetPositionAll(){
            Vector3 position;
            position.x = x; position.y = y; position.z = z;
            return position;
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