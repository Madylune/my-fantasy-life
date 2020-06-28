using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3;
    public int facingIndex = 2;
    
    public Rigidbody2D rigidbody;

    private Vector2 direction;
    private Vector3 targetPosition;

    private bool isMoving = false;

    public Animator animator;

    private PlayerAttack playerAttack;

    private void Start() 
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();   
        playerAttack = GetComponent<PlayerAttack>(); 
    }

    void Update()
    {
        direction = Vector2.zero;
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        UpdateAnimationAndMove();
        UpdateFacing();

        // if (Input.GetMouseButtonDown(0))
        // {
        //     SetTargetPosition();
        // }
        // if (isMoving)
        // {
        //     OnClickMoving();
        // }
    }

    void SetTargetPosition()
    {
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = transform.position.z;

        isMoving = true;
    }

    void OnClickMoving()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Mathf.Round(targetPosition.x) > Mathf.Round(transform.position.x))
        {
            animator.SetFloat("moveX", 1);
            animator.SetFloat("moveY", 0);
        }
        if (Mathf.Round(targetPosition.x) < Mathf.Round(transform.position.x))
        {
            animator.SetFloat("moveX", -1);
            animator.SetFloat("moveY", 0);
        }
        if (Mathf.Round(targetPosition.y) > Mathf.Round(transform.position.y))
        {
            animator.SetFloat("moveY", 1);
            animator.SetFloat("moveX", 0);
        }
        if (Mathf.Round(targetPosition.y) < Mathf.Round(transform.position.y))
        {
            animator.SetFloat("moveY", -1);
            animator.SetFloat("moveX", 0);
        }

        animator.SetFloat("Speed", targetPosition.sqrMagnitude);
        animator.SetBool("moving", true);

        if (transform.position == targetPosition)
        {
            isMoving = false;
        }
    }

    void UpdateAnimationAndMove()
    {
        if (direction != Vector2.zero)
        {
            MovePlayer();
            animator.SetFloat("moveX", direction.x);
            animator.SetFloat("moveY", direction.y);
            animator.SetFloat("Speed", direction.sqrMagnitude);
            animator.SetBool("moving", true);
        } 
        else 
        {
            animator.SetBool("moving", false);
        }
    }

    void MovePlayer()
    {
        rigidbody.MovePosition(rigidbody.position + direction * moveSpeed * Time.fixedDeltaTime);
        playerAttack.StopAttack();
    }

    void UpdateFacing()
    {
        if (direction.y == -1)
        {
            facingIndex = 2; //UP
        }
        if (direction.x == 1) 
        {
            facingIndex = 1; //RIGHT
        }
        if (direction.y == 1)
        {
            facingIndex = 0; //DOWN
        }
        if (direction.x == -1) 
        {
            facingIndex = 3; //LEFT
        }
    }
}
