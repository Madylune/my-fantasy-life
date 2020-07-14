using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Block[] blocks;
    public GameObject magicCircle;

    private Coroutine attackRoutine;
    
    public Transform MyTarget { get; set; }

    private PlayerMovement movement;

    private SpellBook spellBook;

    private void Start() 
    {
        movement = GetComponent<PlayerMovement>();
        spellBook = GetComponent<SpellBook>();
    }

    private void Update() 
    {
        if (MyTarget == null)
        {
            StopAttack();
        }
    }

    private IEnumerator Attack(string spellName)
    {
        Spell spell = spellBook.CastSpell(spellName);

        magicCircle.SetActive(true);
        yield return new WaitForSeconds(spell.MyCastTime);
        
        if (MyTarget != null && InLineOfSight())
        {
            Projectile projectile = Instantiate(spell.MySpellPrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
            projectile.Initialize(MyTarget, spell.MyDamage);
        }

        StopAttack();
    }

    public void CastSpell(string spellName)
    {
        Block();

        if (MyTarget != null && InLineOfSight())
        {
            attackRoutine = StartCoroutine(Attack(spellName));
        }
    }

    private bool InLineOfSight()
    {
        if (MyTarget != null)
        {
            Vector3 targetDirection = (MyTarget.transform.position - transform.position).normalized;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection, Vector2.Distance(transform.position, MyTarget.transform.position), 256);

            if (hit.collider == null)
            {
                return true;
            }
        }
        
        return false;
    }

    private void Block()
    {
        foreach (Block block in blocks)
        {
            block.Deactivate();
        }

        blocks[movement.facingIndex].Activate();
    }

    public void StopAttack()
    {
        spellBook.StopCasting();
        magicCircle.SetActive(false);

        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
        }
    }
}
