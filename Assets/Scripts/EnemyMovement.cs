using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public int damageOnCollision = 10;

    private Enemy enemy;

    private Vector2 direction;
    
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
        if (direction != Vector2.zero)
        {
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

    public void FollowTarget()
    {
        if (enemy.Target != null)
        {
            direction = (enemy.Target.transform.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, enemy.Target.position, speed * Time.deltaTime);
        }
        else 
        {
            direction = Vector2.zero;
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
