using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    
    public HealthBar healthBar;

    public AudioClip hitSound;
    public AudioClip deathSound;

    public static PlayerHealth instance;

    private void Awake() 
    {
        if (instance != null)
        {
            Debug.LogWarning("The scene already has a PlayerHealth");
            return;
        }
        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void HealPlayer(int amount)
    {
        if ((currentHealth + amount) > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += amount;
        }
        healthBar.SetHealth(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        if ((currentHealth - damage) < 0)
        {
            AudioManager.MyInstance.PlayClipAt(deathSound, transform.position);
            currentHealth = 0;
            Die();
        }
        else
        {
            AudioManager.MyInstance.PlayClipAt(hitSound, transform.position);
            currentHealth -= damage;
        }
        healthBar.SetHealth(currentHealth);
    }

    public void Die()
    {
        GameOverManager.instance.OnPlayerDeath();
    }
}
