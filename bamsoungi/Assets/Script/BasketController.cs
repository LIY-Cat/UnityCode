using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketController : MonoBehaviour{
    public AudioClip appleSE;
    public AudioClip bombSE;
    public AudioClip electronicSE;
    AudioSource aud;
    private const string BAM_TAG_NAME = "BamsongiTag";
    private int? option = null;
    private const int RANDOM_OPTION_MIN = 0;
    private const int RANDOM_OPTION_MAX = 3;

    private void Awake(){
        this.aud = GetComponent<AudioSource>();
        SetPos();
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == BAM_TAG_NAME){
            option = Random.Range(RANDOM_OPTION_MIN, RANDOM_OPTION_MAX);
            switch(option){
                case 0:
                    aud.PlayOneShot(appleSE);
                    Debug.Log("<color=maroon>Get 소리재생</color>");
                    break;
                case 1:
                    aud.PlayOneShot(bombSE);
                    Debug.Log("<color=darkblue>폭탄 소리재생</color>");
                    break;
                case 2:
                    aud.PlayOneShot(electronicSE);
                    Debug.Log("<color=blue>번개 소리재생</color>");
                    break;
                default :
                    Debug.Log("<color=red>바구니 옵션 애러</color>");
                    break;
            }
        }else{Debug.Log("<color=red>밤송이 태그가 이상해졌습니다?</color>");}
        
    }

    void SetPos(){
        BasketPositionNameSpace.BasketPosition.Instance.SetPosition(transform.position);
    }
}
