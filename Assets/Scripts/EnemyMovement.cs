using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;

    public int damageOnCollision = 10;

    private Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    public void FollowTarget()
    {
        if (enemy.Target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemy.Target.position, speed * Time.deltaTime);
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
