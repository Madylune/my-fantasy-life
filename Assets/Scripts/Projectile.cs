using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rigidbody;
    private Transform target;

    void Start() 
    {
        rigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Enemy").transform;
    }

    private void FixedUpdate() 
    {
        Vector2 direction = target.position - transform.position;
        rigidbody.velocity = direction.normalized * speed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); 
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
