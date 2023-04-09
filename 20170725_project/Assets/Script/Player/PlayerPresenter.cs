/*
*PlayerPresenter.cs
*/
using UnityEngine;
using UnityEngine.UI;

public class PlayerPresenter : MonoBehaviour{
    [SerializeField] private float jumpForce = 250.0f;
    [SerializeField] private float walkForce = 5.0f;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject gaugeObject;
    [SerializeField] private Scrollbar scrollbarObject;
    private PlayerModel playerModel;
    
    private void Awake(){
        playerModel = new PlayerModel(GetComponent<Rigidbody>());
    }

    private void Start(){
        playerModel.PlayerRigidbody.AddForce(new Vector3(0, 0, 1).normalized * 0.1f);

        MeasureDistance("item");

        gaugeObject = GameObject.Find("Gauge");
        scrollbarObject = GameObject.Find("Scrollbar").GetComponent<Scrollbar>();
    }

    private void Update(){
        HandlePlayerMovement();
        playerModel.PlayerRigidbody.AddForce(new Vector3(0, 0, 1).normalized * 0.1f);

        MeasureDistance("item (4)");
    }

    private void OnTriggerEnter(Collider other){
        playerModel.AddScore(10);
        if (playerModel.Score >= 0){
            scoreText.text = "Score: " + playerModel.Score.ToString();
            gaugeObject.GetComponent<Image>().fillAmount += 0.1f;
            scrollbarObject.size += 0.1f;
        }
        Debug.Log("Name: " + other.ToString());
        Destroy(other.gameObject);
    }

    private void HandlePlayerMovement(){
        if (Input.GetKeyDown(KeyCode.Space) && playerModel.PlayerRigidbody.velocity.y == 0.0f){
            playerModel.Jump(jumpForce);
        }

        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        playerModel.Move(movement, walkForce);
    }

    private void MeasureDistance(string name){
        float objectMD;
        GameObject objectToMeasure = GameObject.Find(name);
        GameObject objectDistance = GameObject.Find("Player");
        
        if (objectToMeasure != null && objectDistance != null){
            objectMD = Vector3.Distance(objectToMeasure.transform.position, objectDistance.transform.position);
            Debug.Log("The distance between the player and " + name + " is " + objectMD + ".");
        }else{
            Debug.LogError("Object: " + name + " not found.");
            Debug.LogError("Object: Player not found");
        }
    }
}