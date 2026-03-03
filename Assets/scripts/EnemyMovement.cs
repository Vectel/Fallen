using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Speeds")]
    public float walkSpeed = 2f;
    public float runSpeed = 4f;

    [Header("AI")]
    public float directionChangeTime = 2f;
    public float chaseRadius = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private Transform player;

    private Vector2 movement;
    private float timer;
    private bool isRunning;
    private bool isChasing;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        PickNewDirection();
    }

    void Update()
    {
        if (player == null) return;

        // Detect player
        float dist = Vector2.Distance(transform.position, player.position);
        isChasing = dist <= chaseRadius;

        if (isChasing)
        {
            movement = (player.position - transform.position).normalized;
            isRunning = true;
        }
        else
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
                PickNewDirection();
        }

        // Animate from movement vector (NOT velocity)
        animator.SetFloat("dirX", movement.x);
        animator.SetFloat("dirY", movement.y);
        animator.SetFloat("speed", movement.magnitude);
    }

    void FixedUpdate()
    {
        float speed = isRunning ? runSpeed : walkSpeed;
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    void PickNewDirection()
    {
        movement = Random.insideUnitCircle.normalized;

        if (movement == Vector2.zero)
            movement = Vector2.right;

        isRunning = Random.value > 0.6f;
        timer = directionChangeTime;
    }
}


