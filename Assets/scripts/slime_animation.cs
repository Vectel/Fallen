using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float walkSpeed = 1.5f;
    public float runSpeed = 3f;
    public float directionChangeTime = 2f;

    private Rigidbody2D rb;
    private Animator animator;

    private Vector2 movement;
    private float timer;

    private bool isRunning;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        PickNewDirection();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
            PickNewDirection();

        float speed = isRunning ? 1f : 0.5f;

        animator.SetFloat("dirX", movement.x);
        animator.SetFloat("dirY", movement.y);
        animator.SetFloat("speed", speed);
    }

    void FixedUpdate()
    {
        float moveSpeed = isRunning ? runSpeed : walkSpeed;
        rb.linearVelocity = movement * moveSpeed;
    }

    void PickNewDirection()
    {
        movement = Random.insideUnitCircle.normalized;

        isRunning = Random.value > 0.6f; // sometimes run

        timer = directionChangeTime;
    }

    // OPTIONAL hooks for later combat system

    public void Attack()
    {
        animator.SetBool("isAttacking", true);
    }

    public void Hurt()
    {
        animator.SetBool("isHurt", true);
    }

    public void Die()
    {
        animator.SetBool("isDead", true);
        rb.linearVelocity = Vector2.zero;
    }
}
