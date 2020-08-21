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
    private string type;

    [SerializeField]
    private int level;

    [SerializeField]
    private GameObject gravePrefab;

    public string MyType { get => type; }

    public int MyLevel { get => level; set => level = value; }

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
            bool critChance = Random.Range(0, 5) < 1 ? true : false;

            stats.currentHealth -= damage;
            CombatTextManager.MyInstance.CreateText(transform.position, damage.ToString(), CombatTextType.HIT, critChance);
        }
    }

    public void Die()
    {
        if (this is Enemy)
        {
            Player.MyInstance.GainExp(XPManager.CalculateXP((this as Enemy)));
        }

        // Show grave
        GameObject grave = Instantiate(gravePrefab, transform.position, transform.rotation);

        // Send to GraveScript the LootTable from the Enemy
        grave.GetComponent<GraveScript>().SetLoot(gameObject.GetComponent<LootTable>());

        GameManager.MyInstance.OnKillConfirmed(this);

        Destroy(gameObject);
        UIManager.MyInstance.HideTargetFrame();
    }
}
