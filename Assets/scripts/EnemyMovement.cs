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
        PickDirection();
    }

    void Update()
    {
        changeTimer -= Time.deltaTime;

        if (changeTimer <= 0)
            PickDirection();

        animator.SetFloat("dirX", movement.x);
        animator.SetFloat("dirY", movement.y);
        animator.SetFloat("speed", movement.magnitude);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement * moveSpeed;
    }

    void PickDirection()
    {
        movement = Random.insideUnitCircle.normalized;
        changeTimer = Random.Range(1f, 3f);
    }
}
