using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class Character : MonoBehaviour
{
    [SerializeField]
    protected Transform hitBox;

    private Stats stats;

    [SerializeField]
    private GameObject gravePrefab;

    private void Start() 
    {
        stats = GetComponent<Stats>();
    }

    public virtual void TakeDamage(int damage)
    {
        if ((stats.currentHealth - damage) <= 0)
        {
            stats.currentHealth = 0;
            Die();
        }
        else
        {
            stats.currentHealth -= damage;
        }
    }

    public void Die()
    {
        // Show grave
        Instantiate(gravePrefab, transform.position, transform.rotation);

        Destroy(gameObject);
        UIManager.MyInstance.HideTargetFrame();
    }
}
