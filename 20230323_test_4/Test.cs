using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private float ix = 3.0f;
    private float iy = 5.0f;
    private Vector2 posxy;
    
    void vector2_pos(){
        Vector2 playerPos = new Vector2(3.0f, 4.0f);
        playerPos.x += 8.0f;
        playerPos.y += 5.0f;
        Debug.Log("플레이어 위치: " + playerPos);
    }
    // Start is called before the first frame update
    void Start()
    {
        vector2_pos();
        posxy = new Vector2(ix, iy);
    }

    // Update is called once per frame
    void Update()
    {
        posxy.x += ix;
        posxy.y += iy;
        Debug.Log(posxy);

    }
}
