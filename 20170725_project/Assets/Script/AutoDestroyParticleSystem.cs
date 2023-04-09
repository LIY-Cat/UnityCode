using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyParticleSystem : MonoBehaviour{
    private ParticleSystem ex;
    

    void Start(){
        ex = GetComponent<ParticleSystem>();
        
    }

    void Update(){
        if (ex != null && !ex.IsAlive()){
            Destroy(ex.gameObject);
        }
    }
}
