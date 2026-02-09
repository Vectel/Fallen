using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal"); // A, D
        float moveY = Input.GetAxis("Vertical");   // W, S
        
        // Skapa en rörelseriktning
        Vector2 moveDirection = new Vector2(moveX, 0).normalized; // Endast horisontell rörelse

        // Flytta spelkaraktären
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
}

