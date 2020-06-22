using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3;
    
    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    void Update()
    {
        //Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        //Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
