using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3;
    
    public Rigidbody2D rigidbody;

    private Vector2 change;

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
}
