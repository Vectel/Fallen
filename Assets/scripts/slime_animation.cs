using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal"); // A, D
        float moveY = Input.GetAxis("Vertical");   // W, S

        // Flytta spelkaraktären
        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Hantera animationer
        UpdateAnimation(moveX, moveY);
    }

    void UpdateAnimation(float moveX, float moveY)
    {
        // Om spelaren rör sig horisontellt
        if (Mathf.Abs(moveX) > 0.1f)
        {
            animator.SetBool("isWalking", true);
            animator.SetInteger("direction", moveX > 0 ? 2 : 1); // 2 = right, 1 = left
        }
        // Om spelaren rör sig vertikalt men inte horisontellt
        else if (Mathf.Abs(moveY) > 0.1f)
        {
            animator.SetBool("isWalking", true);
            animator.SetInteger("direction", moveY > 0 ? 3 : 0); // 3 = up, 0 = down
        }
        else
        {
            // Idle
            animator.SetBool("isWalking", false);
            // direction stannar kvar, så du kan ha idle i samma riktning
        }
    }
}
