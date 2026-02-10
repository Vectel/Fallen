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
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 moveDirection = new Vector2(moveX, moveY);
        transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime);

        // walking check (simple and reliable)
        bool isWalking = moveX != 0 || moveY != 0;
        animator.SetBool("isWalking", isWalking);

        if (isWalking)
        {
            if (moveX < 0)
                animator.SetInteger("direction", 1); // left
            else if (moveX > 0)
                animator.SetInteger("direction", 2); // right
            else
                animator.SetInteger("direction", 0); // forward
        }

        Debug.Log($"Walking: {isWalking} | Dir: {animator.GetInteger("direction")}");
    }
}
