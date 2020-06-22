using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public bool isFocused = false;
    public Transform[] waypoints;

    public int damageOnCollision = 10;

    public SpriteRenderer graphics;

    private Transform target;
    private int destinationPoint = 0;

    void Start()
    {
        target = waypoints[0];
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        //If arrive to destination soon
        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            destinationPoint = (destinationPoint + 1) % waypoints.Length;
            target = waypoints[destinationPoint];
            graphics.flipX = !graphics.flipX;
        }
    }

    private void OnMouseDown() 
    {
        isFocused = true;
        CursorController.instance.ActivateTargetCursor();
    }

    private void OnMouseUp() 
    {
        isFocused = false;
        CursorController.instance.ClearCursor();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageOnCollision);
        }
    }
}
