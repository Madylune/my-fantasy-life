using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3;
    public int facingIndex = 2;
    
    public Rigidbody2D rigidbody;

    private Vector2 change;
    private Vector3 targetPosition;

    private bool isMoving = false;

    public Animator animator;

    private void Start() 
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();    
    }

    void Update()
    {
        change = Vector2.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

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
        if (change != Vector2.zero)
        {
            MovePlayer();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetFloat("Speed", change.sqrMagnitude);
            animator.SetBool("moving", true);
        } 
        else 
        {
            animator.SetBool("moving", false);
        }
    }

    void MovePlayer()
    {
        rigidbody.MovePosition(rigidbody.position + change * moveSpeed * Time.fixedDeltaTime);
    }

    void UpdateFacing()
    {
        if (change.y == -1)
        {
            facingIndex = 2; //UP
        }
        if (change.x == 1) 
        {
            facingIndex = 1; //RIGHT
        }
        if (change.y == 1)
        {
            facingIndex = 0; //DOWN
        }
        if (change.x == -1) 
        {
            facingIndex = 3; //LEFT
        }
    }
}
