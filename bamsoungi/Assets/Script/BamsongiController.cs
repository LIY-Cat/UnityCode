using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//다른 스크립트에서 접근을 위함
namespace BamsongiControllerNamespace {
    // 밤송이 컨트롤러 클래스
    public class BamsongiController : MonoBehaviour{
        //동시성 문제
        private object lockObject = new object();
        // 코루틴 중복 실행 방지용 플래그
        private Coroutine postCollisionDelayProcessCoroutine = null;

        //시스템 잠시 딜레이
        private const float SYSTEM_DELAY = 1.0f;

        //랜덤 범위수
        private const float MIN_RAMDOM = -5.0f;
        private const float MAX_RAMDOM = 5.0f;

        // 밤송이 재설정 딜레이 시간 상수
        private const float RESET_DELAY = 0.5f;
        // 밤송이 파괴 딜레이 시간 상수
        private const float DESTROY_DELAY = 5f;

        private const string BAM_TAG_NAME = "BamsongiTag";
        private const string TAR_TAG_NAME = "targetTag";   

        // 이 밤송이가 충돌을 처리했는지 여부, 없으면 중복 실행
        private bool collisionCheck = false;
        // 오브젝트 파괴 여부
        private bool destructionCheck = false;
        // 점수를 추가해야 하는지 여부, 없으면 여러번 올라감
        private bool isScoreAdded = false;
        // 점수 추가
        private int SCORE_INCREMENT = 10;

        // 이 밤송이의 Rigidbody 컴포넌트
        private Rigidbody thisrigidbody;
        // 이 밤송이의 Collider 컴포넌트
        private Collider thisCollider;
        // 이 밤송이의 ParticleSystem 컴포넌트
        private ParticleSystem thisparticleSystem;

        // Awake에서 필요한 컴포넌트를 검색하고 할당합니다.
        void Awake(){
            thisrigidbody = GetComponent<Rigidbody>();
            Debug.Assert(thisrigidbody != null, "Rigidbody가 null값입니다.");

            thisCollider = GetComponent<Collider>();
            Debug.Assert(thisCollider != null, "Collider가 null값입니다.");

            thisparticleSystem = GetComponent<ParticleSystem>();
            Debug.Assert(thisparticleSystem != null, "ParticleSystem가 null값입니다.");
        }

        /*
        *OnCollisionEnter는 isTrigger가 false된 값을 찾는 다., Rigidbody 컴포넌트가 필요O
        *Collision other는 Rigidbody,Collider 2개다 포함
        *OnTriggerEnter는 isTrigger가 True된 값을 찾는 다., Rigidbody 컴포넌트가 필요X
        */
        // 충돌 이벤트 처리 메소드
        void OnCollisionEnter(Collision other){
            if(!other.gameObject.CompareTag(BAM_TAG_NAME)){
                HandleNonBamsongiCollision(other);
            }else if(other.gameObject.CompareTag(BAM_TAG_NAME)){
                HandleBamsongiCollision(other);
            }else{Debug.LogError("콜라이더 충돌에 문제가 있습니다.");}
        }

        // 밤송이를 특정 방향으로 발사합니다.
        public void Shoot(Vector3 dir){
                thisrigidbody.AddForce(dir);
        }

        // 밤송이가 다른 밤송이가 아닌 물체와 충돌했을 때의 처리 메소드
        private void HandleNonBamsongiCollision(Collision other) {
    	    if(collisionCheck) return; // 이미 충돌 처리된 경우

            if (other.gameObject.CompareTag(TAR_TAG_NAME)) {//타겟에 닿으면 바로 떨어지기 안됨 분석 더 필요
                thisrigidbody.velocity = Vector3.down * 2000;
                StartCoroutine(SystemDelay());
            }

            DisabledOnCollisionEnter();
    	    thisparticleSystem.Play();
            AddScore(other);
            
            StartPostCollisionDelayProcessCoroutine();

            destructionCheck = true;//파괴 여부
            collisionCheck = true;// 충돌처리 완료
        }

        // 밤송이가 다른 밤송이와 충돌했을 때의 처리 메소드
        private void HandleBamsongiCollision(Collision other) {
            destructionCheck = false;//파괴 여부
            //thisparticleSystem.Play();
        }

        IEnumerator SystemDelay(){yield return new WaitForSeconds(SYSTEM_DELAY);}

        // 이 밤송이를 처리 후 대기 상태로 만드는 코루틴
        IEnumerator PostCollisionDelayProcess(float delay){
            yield return new WaitForSeconds(delay);
            try{
                lock (lockObject) {
                    postCollisionDelayProcessCoroutine = StartCoroutine(RredyShoot("random"));
                }
            }catch(System.Exception e){Debug.LogException(e);
            }finally{postCollisionDelayProcessCoroutine = null;}

            yield return new WaitForSeconds(delay);
            try{
                lock (lockObject) {
                    if(destructionCheck){ObjectDestroy();}
                    else{destructionCheck = true;}
                }
            }catch(System.Exception e){Debug.LogException(e);
            }finally{postCollisionDelayProcessCoroutine = null;}
            
            postCollisionDelayProcessCoroutine = null;
        }

        // 되돌아오는 코루틴, 다른 방향으로 다시 발사하는 코루틴
        IEnumerator RredyShoot(string option) {
            ActivateOnCollisionEnter();
            if(option == "backward") {
                ShootSetBamsongi("backward");
            }else{
                ShootSetBamsongi("random");
            }
            //안 기달려 주면은 바로 찰싹 붙는 다.
            yield return new WaitForSeconds(RESET_DELAY);
            lock (lockObject) {collisionCheck = false;}
            //Invoke("ObjectDestroy", 5f);
        }

        private void ShootSetBamsongi(string option){
            if(option == "backward"){// 던져진 밤송이를 뒤로 돌아오게 만든다.
                Shoot(new Vector3(
                Randomxyznamespace.Randomxyz.Instance.GetRandomX(),
                 Randomxyznamespace.Randomxyz.Instance.GetRandomY(), 
                 -1 * Randomxyznamespace.Randomxyz.Instance.GetRandomZ()
                 )
                 * Randomxyznamespace.Randomxyz.Instance.GetRandomForce()
                );

            }else{// 던져진 밤송이를 다시 한번더 랜덤하게 튕긴다.
                Shoot(new Vector3(
                Random.Range(MIN_RAMDOM, MAX_RAMDOM),
                 Random.Range(MIN_RAMDOM, MAX_RAMDOM),
                  Random.Range(MIN_RAMDOM, MAX_RAMDOM)
                  )
                   * Randomxyznamespace.Randomxyz.Instance.GetRandomForce()
                );
            }
        }

        //유의점: 변경시 바로 적용이 된다.
        private void ActivateOnCollisionEnter(){
            thisrigidbody.isKinematic = false;
            thisCollider.isTrigger = false;
        }

        private void DisabledOnCollisionEnter(){
            thisrigidbody.isKinematic = true;
            thisCollider.isTrigger = true;
        }

        private void AddScore(Collision other){
            if(!isScoreAdded && other.gameObject.CompareTag(TAR_TAG_NAME)){
                ScoreManagerNamespace.ScoreManager.Instance.AddScore(SCORE_INCREMENT);
                isScoreAdded = true;
            }
        }

        // 코루틴 시작 메소드
        private void StartPostCollisionDelayProcessCoroutine(){
            //코루틴 중첩실행 방지, 성능 최적화
            if(postCollisionDelayProcessCoroutine == null){
                postCollisionDelayProcessCoroutine = StartCoroutine(PostCollisionDelayProcess(DESTROY_DELAY));
            }
        }

        // 코루틴 중지 메소드
        private void StopPostCollisionDelayProcessCoroutine(){
            if(postCollisionDelayProcessCoroutine != null){
                StopCoroutine(postCollisionDelayProcessCoroutine);
                postCollisionDelayProcessCoroutine = null;
            }
        }

        // 이 밤송이를 파괴하는 메소드
        private void ObjectDestroy() {
                Destroy(thisrigidbody.gameObject);
        }
    }
}