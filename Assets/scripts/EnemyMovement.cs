using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    private float changeTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        PickNewDirection();
    }

    void Update()
    {
        changeTimer -= Time.deltaTime;

        if (changeTimer <= 0)
            PickNewDirection();

        UpdateAnimation(movement.x, movement.y);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement * moveSpeed;
    }

    void PickNewDirection()
    {
        movement = Random.insideUnitCircle.normalized;
        changeTimer = Random.Range(1f, 3f);
    }

    void UpdateAnimation(float moveX, float moveY)
    {
        if (Mathf.Abs(moveX) > 0.1f)
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
