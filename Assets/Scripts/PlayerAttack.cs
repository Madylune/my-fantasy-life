using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject[] projectiles;

    private Transform target;

    void Start() 
    {
        target = GameObject.FindGameObjectWithTag("Enemy").transform;   
    }
    
    void Update()
    {        
        EnemyMovement enemy = target.transform.GetComponent<EnemyMovement>();
        if (Input.GetKeyDown(KeyCode.F1) && enemy.isFocused)
        {   
            CastSpell();
        }
    }

    void CastSpell()
    {
        Instantiate(projectiles[0], transform.position, Quaternion.identity);
    }
}
