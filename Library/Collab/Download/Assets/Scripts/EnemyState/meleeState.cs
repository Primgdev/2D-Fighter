using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeState : ienemyState
{
    private float AttackTimer;
    private float AttackCoolDown = 3;
    private bool canAttack = true;

    private Enemy enemy;

  

    public void Enter(Enemy enemy)
    {
      this.enemy = enemy;
    }

    

    public void Execute()
    {
        
        Attack();
        if(!enemy.InMeleeRange)
        {
            enemy.ChangeState(new RangedState());
        }
        
    }

   
    

    public void Exit()
    {
     
    }

    public void OnTriggerEnter(Collider2D other)
    {
       
    }


    private void Attack()
    {
        AttackTimer += Time.deltaTime;
        if(AttackTimer >= AttackCoolDown )
        {
            canAttack = true;
            AttackTimer = 0;
        }

        if(canAttack)
        {
            canAttack = false;
            enemy.myanimator.SetTrigger("Attack");
      
        }
    }
  
}
