using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : ienemyState
{
    private Enemy enemy;
    private float idleTimer; //time to get idle
    private  float IdleDuration = 5; // time duration for idle
    

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        Debug.Log("idle"); //print idle

        Idle();

        if(enemy.Target == null) //if the target is null
        {
            enemy.ChangeState(new PatrolState()); //return to patrol state in enemy not found
        }

    }


    public void Exit() //exit function
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {
        
    }

    public void Idle() //idle fnction
    {

        enemy.myanimator.SetFloat("speed", 0);

        idleTimer += Time.deltaTime;


        if(idleTimer >= IdleDuration) //if idle timer is greater than or equals to idle duration 
        {
            enemy.ChangeState(new PatrolState()); //return back to patril state after idle
        }


    }



}
