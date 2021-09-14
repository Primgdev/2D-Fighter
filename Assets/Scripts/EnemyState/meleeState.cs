using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeState : ienemyState
{
    private float AttackTimer; //float timer for the attack
    private float AttackCoolDown = 1; //enemy attacking  between time
    private bool canAttack = true; //boolean for attack 

    private Enemy enemy;

  

    public void Enter(Enemy enemy)
    {
      this.enemy = enemy;
    }

    

    public void Execute() // to execute the function
    {
        
        Attack();
        if(!enemy.InMeleeRange) //if enemy not in melee range
        {
            enemy.ChangeState(new RangedState());
        }
        
    }

   
    

    public void Exit() //exit the functioon
    {
     
    }

    public void OnTriggerEnter(Collider2D other)
    {
       
    }


    private void Attack() //funtion to attack 
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
            enemy.myanimator.SetTrigger("Attack"); //attack animation trigger
      
        }
    }
  
}
