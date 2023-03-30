using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject circle;
    public GameObject capsule;

    float distance=0.0f;

    // Start is called before the first frame update
    void Start()
    {
        circle = GameObject.Find("Circle");
        capsule = GameObject.Find("Capsule");
        Vector2 circlev2 = circle.transform.position;
        Vector2 capsulev2 = capsule.transform.position;
        Vector2 dir = capsulev2 - circlev2;
        
        distance = Vector3.Distance(capsule.transform.position, circle.transform.position);
        
        Debug.Log("두 오브젝트간의 거리 by magnitude = " + dir.magnitude);
        Debug.Log("두 오브젝트간의 거리 by Distance = " + distance);

        Vector2 v2 = new Vector2(1, 2);
        Debug.Log("Vector2 is: " + v2);

        // convert v2 to v3
        Vector3 v3 = v2;
        Debug.Log("Vector3 is: " + v3);

        // convert v3 to new Vector3
        Debug.Log("Set v3 to (3, 4, 5)");
        v3 = new Vector3(3, 4, 5);

        // convert v3 to v2
        v2 = v3;
        Debug.Log("Vector2 is: " + v2);
    }

    // Update is called once per frame
    void Update()
    {
        circle.transform.position += new Vector3(0f, Time.deltaTime, 0f);
        
        // circle.transform.Translate(0, Time.deltaTime, 0);        
    }
}
