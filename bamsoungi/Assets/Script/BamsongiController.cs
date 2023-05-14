using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BamsongiControllerNamespace {
    public class BamsongiController : MonoBehaviour{
        //코루틴 2번 실행 방지
        private bool isCoroutineRunning = false;
        //점수 2번 올라기 방지, 팅기기 구현쪽
        private bool secondCollision = false;
        private bool shouldAddScore = false;

        //밤송이 충돌로 인한 점수누락 계산을 편하기 위해 만들어진 변수
        public bool isFlying = false;
        private bool hitTarget = false;
        private BamsongiController lastBamsongiHit = null;

        public void Shoot(Vector3 dir){
            Rigidbody rb = GetComponent<Rigidbody>();
            if(rb != null){
                isFlying = true;
                rb.AddForce(dir);
            }else{
                Debug.LogError("Rigidbody를 찾을 수 없습니다.");
            }
        }

        //OnCollisionEnter는 isTrigger가 false된 값을 찾는 다., Rigidbody 컴포넌트가 필요O
        //Collision other는 Rigidbody,Collider 2개다 포함
        //OnTriggerEnter는 isTrigger가 True된 값을 찾는 다., Rigidbody 컴포넌트가 필요X
        void OnCollisionEnter(Collision other){
            Rigidbody rb = GetComponent<Rigidbody>();
            ParticleSystem ps = GetComponent<ParticleSystem>();
            Collider collider = GetComponent<Collider>();

            Debug.Log("<color=red>" + other.gameObject.name + "</color>");

            if(rb!= null && ps!= null && collider!= null){
                if(other.gameObject.name != "BamsongiPrefab(Clone)"){
                    if(!secondCollision){
                        isFlying = false;
                        rb.isKinematic = true;
                        collider.isTrigger = true;
                        ps.Play();
                        if(!shouldAddScore && other.gameObject.name == "target" && !hitTarget){
                            ScoreManagerNamespace.ScoreManager.Instance.AddScore(10);
                            shouldAddScore = true;
                            hitTarget = true;
                        }else{
                            Debug.Log("점수 2번 올라가기 방지 " + shouldAddScore.ToString() +"\n 그리고 target 이외에 점수 올라가기 방지");
                        }
                        if(!isCoroutineRunning){
                            StartCoroutine(Ro(5f));
                            isCoroutineRunning = true;
                        }else{
                            Debug.Log("<color=red>코루틴이 실행중 입니다.</color>");
                            //Debug.LogError("<color=red>코루틴이 실행중 입니다.</color>\n 그리고 secondCollision 값을 확인해 주세요." + shouldAddScore.ToString());
                        }
                        //Invoke("ResetKinematic", 5f);
                        secondCollision = true;
                    }else{
                        Debug.Log("<color=red>이미 충돌처리 되었습니다.</color>");
                    }
                }else if(other.gameObject.name == "BamsongiPrefab(Clone)"){
                    //누락된 밤송이가 다시 타켓에 부딛힐때 점수 올라가기 방지
                    shouldAddScore = true;
                    Debug.Log("<color=red>밤송이끼리 부딛힘</color>");
                    BamsongiController otherBamsongi = other.gameObject.GetComponent<BamsongiController>();
                    if(otherBamsongi != null && otherBamsongi != lastBamsongiHit && isFlying && otherBamsongi.isFlying){
                        if(!hitTarget){
                            isFlying = false; //날아가는 밤송이와 돌아가는 밤송이만 체크
                            hitTarget = true;
                            Randomxyznamespace.Randomxyz.Instance.AddBamsongiWithTheSameCount(1);
                        }
                        lastBamsongiHit = otherBamsongi;
                    }
                }else{
                    Debug.LogError("Error");
                }
            }else{
                Debug.LogError("Rigidbody, ParticleSystem 또는 Collider 컴포넌트를 찾을 수 없습니다.");
            }
        }

        IEnumerator Ro(float delay){
            Debug.Log("<color=red>코루틴 동작 시작!!</color>");
            // 첫 번째 작업: ResetKinematic
            yield return new WaitForSeconds(delay);  // 지연
            StartCoroutine(ResetKinematic());
            

            // 두 번째 작업: ObjectDestroy
            yield return new WaitForSeconds(delay);  // 지연
            ObjectDestroy();

            if(isCoroutineRunning){
                Debug.Log("<color=red>코루틴이 안정적으로 종료 되었습니다. </color>" + isCoroutineRunning.ToString());
            }else{
                Debug.LogError("코루틴이 안정적으로 종료하는 데 문제가 발생 했습니다.");
                isCoroutineRunning = true;
            }
        }

        IEnumerator ResetKinematic() {
            Rigidbody rb = GetComponent<Rigidbody>();
            Collider collider = GetComponent<Collider>();
            
            if(rb!= null && collider!= null){
                Debug.Log("<color=red>ResetKinematic 처리</color>");
                rb.isKinematic = false;
                collider.isTrigger = false;
                isFlying = true;
                lastBamsongiHit = null;
                /*
                TODO: 카메라가 보는 반대 방향으로 날라오기
                */
                rb.AddForce(new Vector3(Randomxyznamespace.Randomxyz.Instance.GetRandomX(),
                 Randomxyznamespace.Randomxyz.Instance.GetRandomY(),
                  -1*Randomxyznamespace.Randomxyz.Instance.GetRandomZ())
                  *Randomxyznamespace.Randomxyz.Instance.GetRandomForce());
                  yield return new WaitForSeconds(0.5f);
                secondCollision = false;
                //Invoke("ObjectDestroy", 5f);
            }else{
                Debug.LogError("Rigidbody 또는 Collider 컴포넌트를 찾을 수 없습니다.");
            }
        }

        void ObjectDestroy() {
            Rigidbody rb = GetComponent<Rigidbody>();
            if(rb!= null){
                Debug.Log("<color=red>ObjectDestroy 처리</color>");
                Destroy(rb.gameObject);
            }else{
                Debug.LogError("Rigidbody를 찾을 수 없습니다.");
            }
        }
    }
}