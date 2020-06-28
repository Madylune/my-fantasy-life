using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public string name;
    public int level;
    public Sprite icon;

    void Start()
    {
        currentHealth = maxHealth;
    }
}
