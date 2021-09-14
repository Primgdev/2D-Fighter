using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedState : ienemyState
{
    private Enemy enemy;

    private float AttackTimer; //attack timer
    private float AttackCoolDown = 3; // time between attack
    private bool canAttack = true;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        Attack();

        if(enemy.InMeleeRange) //if enemy is in melee range
        {
            enemy.ChangeState(new MeleeState());
        }
        else if (enemy.Target != null) //is target not found
        {
            enemy.Move();
        }
        else
        {
            enemy.ChangeState(new IdleState());
        }
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D other)
    {
        
    }

    private void Attack() //attack function
    {
        AttackTimer += Time.deltaTime;
        if (AttackTimer >= AttackCoolDown) //if attaktimer is greater than or equals to attack cool down
        {
            canAttack = true;
            AttackTimer = 0;
        }

        if (canAttack)
        {
            canAttack = false;
            enemy.myanimator.SetTrigger("Attack"); //play attack animation

        }
    }


}
