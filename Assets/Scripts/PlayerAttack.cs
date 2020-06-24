using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject[] projectiles;
    public Block[] blocks;
    public GameObject magicCircle;

    private Coroutine attackRoutine;
    private Transform target;

    private PlayerMovement player;

    private void Start() 
    {
        target = GameObject.FindGameObjectWithTag("Enemy").transform;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Block();
            if (InLineOfSight())
            {
                attackRoutine = StartCoroutine(Attack());
            }
        }    
    }

    private IEnumerator Attack()
    {
        magicCircle.SetActive(true);
        yield return new WaitForSeconds(3);

        CastSpell();
        magicCircle.SetActive(false);
    }

    public void CastSpell()
    {
        Instantiate(projectiles[2], transform.position, Quaternion.identity);
    }

    private bool InLineOfSight()
    {
        Vector3 targetDirection = (target.transform.position - transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection, Vector2.Distance(transform.position, target.transform.position), 256);

        if (hit.collider == null)
        {
            return true;
        }
        
        return false;
    }

    private void Block()
    {
        foreach (Block block in blocks)
        {
            block.Deactivate();
        }

        blocks[player.facingIndex].Activate();
    }
}
