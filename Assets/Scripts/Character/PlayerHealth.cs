using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    
    public HealthBar healthBar;

    public GameObject heartEffect;

    private Coroutine effectRoutine;

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
            CombatTextManager.MyInstance.CreateText(transform.position, amount.ToString(), CombatTextType.HEAL, false);
        }
        healthBar.SetHealth(currentHealth);
        effectRoutine = StartCoroutine(ShowHealAnimation());
    }

    private IEnumerator ShowHealAnimation()
    {
        heartEffect.SetActive(true);

        yield return new WaitForSeconds(1);

        heartEffect.SetActive(false);
        StopCoroutine(effectRoutine);
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
            //AudioManager.MyInstance.PlayClipAt(hitSound, transform.position);
            currentHealth -= damage;
            CombatTextManager.MyInstance.CreateText(transform.position, damage.ToString(), CombatTextType.DAMAGE, false);
        }
        healthBar.SetHealth(currentHealth);
    }

    public void Die()
    {
        //AudioManager.MyInstance.PlayClipAt(deathSound, transform.position);
        GameOverManager.instance.OnPlayerDeath();
    }
}
