using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int damageOnCollision = 10;

    private Enemy enemy;
    
    public Animator animator;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>(); 
    }

    void Update()
    {
        UpdateAnimationAndMove();
    }

    void UpdateAnimationAndMove()
    {
        if (enemy.Direction != Vector2.zero)
        {
            animator.SetFloat("moveX", enemy.Direction .x);
            animator.SetFloat("moveY", enemy.Direction .y);
            animator.SetFloat("Speed", enemy.Direction .sqrMagnitude);
            animator.SetBool("moving", true);
        } 
        else 
        {
            animator.SetBool("moving", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageOnCollision);

            PlayerAttack playerAttack = collision.transform.GetComponent<PlayerAttack>();
            playerAttack.StopAttack();
        }
    }
}
