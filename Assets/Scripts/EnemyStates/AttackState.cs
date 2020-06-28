using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class AttackState : IState
{
  private Enemy parent;
  public void Enter(Enemy parent)
  {
    this.parent = parent;
  }

  public void Update()
  {   
    if (parent.Target != null)
    {
      float distance = Vector2.Distance(parent.Target.position, parent.transform.position);
      parent.Attack();

      if (distance >= parent.MyAttackRange)
      {
        parent.ChangeState(new FollowState());
      }
    }
    else
    {
      parent.ChangeState(new IdleState());
    }
  }

  public void Exit()
  {

  }
}