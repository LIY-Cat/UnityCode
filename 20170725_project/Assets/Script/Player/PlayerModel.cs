/*
*PlayerModel.cs
*/
using UnityEngine;

public class PlayerModel{
    private Rigidbody playerRigidbody;
    private int score = 0;
    public Rigidbody PlayerRigidbody => playerRigidbody;
    public int Score => score;

    public PlayerModel(Rigidbody playerRigidbody){
        this.playerRigidbody = playerRigidbody;
    }

    public void AddScore(int scoreToAdd){
        score += scoreToAdd;
    }

    public void Jump(float jumpForce){
        if (Input.GetKeyDown(KeyCode.Space) && playerRigidbody.velocity.y == 0.0f){
            this.playerRigidbody.AddForce(playerRigidbody.transform.up * jumpForce);
        }
    }


    public void Move(Vector3 movement, float walkForce){
        playerRigidbody.AddForce(movement.normalized * walkForce);
    }
}