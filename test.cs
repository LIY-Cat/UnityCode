using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private float x = 3.0f;
    private float y = 5.0f;
    void vector2_pos(){
        Vector2 playerPos = new Vector2(3.0f, 4.0f);
        playerPos.x += 8.0f;
        playerPos.y += 5.0f;
        Debug.Log("플레이어 위치: " + playerPos);
    }
    // Start is called before the first frame update
    void Start()
    {
        //vector2_pos();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 posxy = new Vector2(horizontalInput * x * Time.deltaTime,verticalInput * y * Time.deltaTime);
        transform.position += posxy;
        Debug.Log(transform.position);

    }
}
