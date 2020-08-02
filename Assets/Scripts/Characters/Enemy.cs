using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    private Transform target;
    private EnemyMovement movement;
    private Vector2 direction;
    
    private IState currentState;

    public float MyAttackRange { get; set; }
    
    public float speed;

    public int atk;

    public Vector2 Direction
    {
        get {
            return direction;
        }
        set {
            direction = value;
        }
    }

    public Transform Target
    {
        get {
            return target;
        }
        set {
            target = value;
        }
    }

    private void Awake() 
    {
        MyAttackRange = 1;
        ChangeState(new IdleState());
    }

    private void Update() 
    {
        currentState.Update();
    }

    public override Transform Select()
    {
        canvasGroup.alpha = 1;

        return base.Select();
    }

    public override void DeSelect()
    {
        canvasGroup.alpha = 0;
        
        base.DeSelect();
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this);
    }

    public void Attack()
    {
        PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
        playerHealth.TakeDamage(atk);
    }
}
