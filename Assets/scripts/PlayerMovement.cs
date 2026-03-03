using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement = movement.normalized;

        UpdateAnimation(movement.x, movement.y);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement * moveSpeed;
    }

    void UpdateAnimation(float moveX, float moveY)
    {
        if (Mathf.Abs(moveX) > 0.01f)
        {
            animator.SetBool("isWalking", true);
            animator.SetInteger("direction", moveX > 0 ? 2 : 1);
        }
        else if (Mathf.Abs(moveY) > 0.1f)
        {
            animator.SetBool("isWalking", true);
            animator.SetInteger("direction", 0);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}
