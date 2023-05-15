using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BamsongiGenerator : MonoBehaviour{
    public GameObject bamsongiPrefab;
    
    //랜덤값 상수
    private const int RANDOM_OPTION_MIN = 0;
    private const int RANDOM_OPTION_MAX = 5;
    private const float FORCE_MIN = 1000.0f;
    private const float FORCE_MAX = 3000.0f;

    //10번 중 꼭 2번 이상은 빗나가게 하는 변수
    private const int TOTAL_TRIES = 10;
    private const int MINIMUM_SUCCESS_TRIES = 2;
    private int missCount = 0;
    private int numberOfThrows = 0;

    //확율이 정확한지 테스트용으로 만듬
    private int totalNumberOfThrows = 0;
    private int totalMissCount = 0;

    //던지기 테스트 할 때 힘들어서 꾸욱 누르면 연속으로 발사하는 변수
    private float lastShootTime = 0f;
    private float shootDelay = 0.125f; // 값초마다 한 번씩 발사


    //랜덤 옵션
    private int? random_option = null; //int?는 null할당 위함, 0으로 초기화해서 0번 옵션이 발생하는 예외상황 방지용

    void Update(){
        if (/*Input.GetMouseButtonDown(0)*/
         Input.GetMouseButton(0) && Time.time - lastShootTime >= shootDelay
          /*true*/) {
            lastShootTime = Time.time;
            GameObject bamsongi = Instantiate(bamsongiPrefab);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log("ray.direction : " + ray.direction);
            Vector3 worldDir = ray.direction;
            Debug.Log("Before worldDir : " + worldDir);

            //꼭 10중 2번은 빗나가게하는 확율
            float probability = (MINIMUM_SUCCESS_TRIES - missCount) / (float)(TOTAL_TRIES - numberOfThrows);
            //Random.value 0.0~1.0 랜덤으로 출력
            if(Random.value < probability){
                Debug.Log("확율 : " + probability * 100 + "%");
                random_option = Random.Range(RANDOM_OPTION_MIN, RANDOM_OPTION_MAX);
                Debug.Log("<color=orange>random_option : </color>" + random_option);
                switch(random_option){
                    case 0:
                        //worldDir.z = (float)-1.0; 뒤로 발사 (float)-1.0 === -1.0f 같음
                        worldDir.z = Random.Range(0.7f, 1.0f);
                        Debug.Log("<color=orange>worldDir.z :</color>" + worldDir.z);
                        break;
                    case 1:
                        worldDir.x = Random.Range(-2.0f, 2.0f);
                        Debug.Log("<color=orange>worldDir.x :</color>" + worldDir.x);
                        break;
                    case 2:
                        worldDir.y = Random.Range(-0.2f, 0.2f);
                        Debug.Log("<color=orange>worldDir.y :</color>" + worldDir.y);
                        break;
                    case 3:
                        worldDir.z = Random.Range(0.7f, 1.0f);
                        worldDir.x = Random.Range(-2.0f, 2.0f);
                        Debug.Log("<color=orange>worldDir.z :</color>" + worldDir.z + "\n<color=orange> worldDir.x :</color>" + worldDir.x);
                        break;
                    case 4:
                        worldDir.z = Random.Range(0.7f, 1.0f);
                        worldDir.y = Random.Range(-0.2f, 0.2f);
                        Debug.Log("<color=orange>worldDir.z :</color>" + worldDir.z + "\n<color=orange> worldDir.y :</color>" + worldDir.y);
                        break;
                    case 5:
                        worldDir.x = Random.Range(-2.0f, 2.0f);
                        worldDir.y = Random.Range(-0.2f, 0.2f);
                        Debug.Log("<color=orange>worldDir.x :</color>" + worldDir.z + "\n<color=orange> worldDir.y :</color>" + worldDir.y);
                        break;
                    default:
                        Debug.Log("<color=red>random_option value Error</color>");
                        break;
                }
                //10번 다 던지기 전에 2번 빗나가면은 다시 처음부터 시작 즉 2번이상 빗나가게 된다.
                missCount++;
                totalMissCount++;
                if(missCount >= MINIMUM_SUCCESS_TRIES){
                    numberOfThrows = 0;
                    missCount = 0;
                    totalNumberOfThrows++;
                }else{
                    numberOfThrows++;
                    totalNumberOfThrows++;
                }
                //10중 딱 2번만 빗나가게 할려면은 이 코드를 사용
                /*switch(numberOfThrows){
                    case 9:
                        numberOfThrows = 0;
                        break;
                    default:
                        numberOfThrows++;
                        break;
                }*/
            }else{
                numberOfThrows++;
                totalNumberOfThrows++;
            }

            Randomxyznamespace.Randomxyz.Instance.SetRandomX(worldDir.x);
            Randomxyznamespace.Randomxyz.Instance.SetRandomY(worldDir.y);
            Randomxyznamespace.Randomxyz.Instance.SetRandomZ(worldDir.z);

            Debug.Log("<color=orange>totalNumberOfThrows : </color>" + totalNumberOfThrows
             + "\n<color=orange> totalMissCount : </color>" + totalMissCount
              + " <color=orange>BamsongiWithTheSameCount : </color>" + Randomxyznamespace.Randomxyz.Instance.GetBamsongiWithTheSameCount());
            Debug.Log("<color=orange>After worldDir : </color>" + worldDir + "\n<color=orange> worldDir.normalized : </color>" + worldDir.normalized);

            float randomForce = Random.Range(FORCE_MIN, FORCE_MAX); // 던지는 파워, 2500 이상이 면은 과녁을 뚫는 다.
            Randomxyznamespace.Randomxyz.Instance.SetRandomForce(randomForce);
            Debug.Log("<color=orange>randomForce : </color>" + randomForce);
            Debug.Log("<color=orange>worldDir.normalized*randomForce : </color>" + worldDir.normalized * randomForce);

            //bamsongi.GetComponent<BamsongiControllerNamespace.BamsongiController>().Shoot(worldDir.normalized * randomForce);
            bamsongi.GetComponent<BamsongiControllerNamespace.BamsongiController>().Shoot(new Vector3(0, 200, 2000));//테스트
        }
    }
}