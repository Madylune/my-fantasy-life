using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    // public GameObject effect;
    private Transform player;

    public int potionsHealthPoints = 10;
    public KeyCode keyCode;

    private void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;   
    }

    public void Use()
    {
        PlayerHealth playerHealth = player.transform.GetComponent<PlayerHealth>();
        playerHealth.HealPlayer(potionsHealthPoints);
        Destroy(gameObject);
    }
}
