using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        Idle,
        Wander,
        Chase,
        Attack
    }

    private State currentState;

    [Header("Movement")]
    public float walkSpeed = 2f;
    public float runSpeed = 4f;

    [Header("AI")]
    public float idleTime = 2f;
    public float wanderTime = 3f;
    public float detectionRadius = 6f;
    public float attackRadius = 1.5f;

    private Rigidbody2D rb;
    private Animator animator;
    private Transform player;

    private Vector2 moveDirection;
    private float stateTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        player = GameObject.FindWithTag("Player")?.transform;

        ChangeState(State.Idle);

        Debug.Log("Enemy AI started");
    }

    void Update()
    {
        if (player == null)
        {
            Debug.LogError("Player not found!");
            return;
        }

        float distance = Vector2.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.Idle:
                UpdateIdle(distance);
                break;

            case State.Wander:
                UpdateWander(distance);
                break;

            case State.Chase:
                UpdateChase(distance);
                break;

            case State.Attack:
                UpdateAttack(distance);
                break;
        }

        UpdateAnimation();
    }

    void FixedUpdate()
    {
        float speed = (currentState == State.Chase) ? runSpeed : walkSpeed;

        rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
    }

    void UpdateIdle(float distance)
    {
        stateTimer -= Time.deltaTime;

        moveDirection = Vector2.zero;

        if (distance < detectionRadius)
            ChangeState(State.Chase);

        if (stateTimer <= 0)
            ChangeState(State.Wander);
    }

    void UpdateWander(float distance)
    {
        stateTimer -= Time.deltaTime;

        if (distance < detectionRadius)
            ChangeState(State.Chase);

        if (stateTimer <= 0)
            ChangeState(State.Idle);
    }

    void UpdateChase(float distance)
    {
        moveDirection = (player.position - transform.position).normalized;

        if (distance < attackRadius)
            ChangeState(State.Attack);

        if (distance > detectionRadius * 1.5f)
            ChangeState(State.Wander);
    }

    void UpdateAttack(float distance)
    {
        moveDirection = Vector2.zero;

        if (distance > attackRadius)
            ChangeState(State.Chase);
    }

    void ChangeState(State newState)
    {
        currentState = newState;

        Debug.Log("Enemy state: " + newState);

        switch (newState)
        {
            case State.Idle:
                stateTimer = idleTime;
                break;

            case State.Wander:
                stateTimer = wanderTime;
                moveDirection = Random.insideUnitCircle.normalized;
                break;

            case State.Chase:
                break;

            case State.Attack:
                break;
        }
    }

    void UpdateAnimation()
    {
        animator.SetFloat("dirX", moveDirection.x);
        animator.SetFloat("dirY", moveDirection.y);
        animator.SetFloat("speed", moveDirection.magnitude);
    }
}