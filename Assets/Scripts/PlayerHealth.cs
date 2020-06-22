using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    
    public HealthBar healthBar;

    public static PlayerHealth instance;

    private Inventory inventory;

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

        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //Use potion
        }
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
            currentHealth = 0;
            Die();
        }
        else
        {
            currentHealth -= damage;
        }
        healthBar.SetHealth(currentHealth);
    }

    public void Die()
    {
        GameOverManager.instance.OnPlayerDeath();
    }
}
