using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum EnemyState { Idle, Wander, Chase, Attack }

    [Header("Speeds")]
    public float walkSpeed = 2f;
    public float runSpeed = 4f;

    [Header("AI")]
    public float idleTime = 1.5f;
    public float wanderTime = 3f;
    public float chaseRadius = 6f;
    public float attackRadius = 1.2f;

    private EnemyState currentState;

    private Rigidbody2D rb;
    private Animator animator;
    private Transform player;

    private Vector2 movement;
    private float stateTimer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        ChangeState(EnemyState.Idle);
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        switch (currentState)
        {
            case EnemyState.Idle:
                stateTimer -= Time.deltaTime;

                if (distance < chaseRadius)
                    ChangeState(EnemyState.Chase);

                if (stateTimer <= 0)
                    ChangeState(EnemyState.Wander);
                break;

            case EnemyState.Wander:
                stateTimer -= Time.deltaTime;

                if (distance < chaseRadius)
                    ChangeState(EnemyState.Chase);

                if (stateTimer <= 0)
                    ChangeState(EnemyState.Idle);
                break;

            case EnemyState.Chase:
                movement = (player.position - transform.position).normalized;

                if (distance < attackRadius)
                    ChangeState(EnemyState.Attack);

                if (distance > chaseRadius * 1.5f)
                    ChangeState(EnemyState.Wander);
                break;

            case EnemyState.Attack:
                movement = Vector2.zero;

                if (distance > attackRadius)
                    ChangeState(EnemyState.Chase);
                break;
        }

        animator.SetFloat("dirX", movement.x);
        animator.SetFloat("dirY", movement.y);
        animator.SetFloat("speed", movement.magnitude);
    }

    void FixedUpdate()
    {
        float speed = walkSpeed;

        if (currentState == EnemyState.Chase)
            speed = runSpeed;

        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    void ChangeState(EnemyState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case EnemyState.Idle:
                movement = Vector2.zero;
                stateTimer = idleTime;
                break;

            case EnemyState.Wander:
                movement = Random.insideUnitCircle.normalized;
                stateTimer = wanderTime;
                break;

            case EnemyState.Chase:
                break;

            case EnemyState.Attack:
                movement = Vector2.zero;
                break;
        }
    }
}

