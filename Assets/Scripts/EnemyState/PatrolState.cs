using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : ienemyState
{
    private float patrolTimer; // patrol timer
    private readonly float patrolDuration = 10; //patrolling duration

    private Enemy enemy;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;

    }

    public void Execute()
    {
        Debug.Log("patrolling"); //print out thee patrolling
        Patrol(); //execute patrol function
        enemy.Move(); //Execute the move function

        if(enemy.Target != null ) //if the enemy target is null
        {
            enemy.ChangeState(new RangedState()); // if target not found change to new ranged state
        }
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D other)
    {
        if(other.tag == "Edge") //change direction if collide wit the edge
        {
            enemy.ChangeDirection();
        }
    }

    public void Patrol() //patrol function
    {

        patrolTimer += Time.deltaTime;


        if (patrolTimer >= patrolDuration) //if patrol timer is greater than or equals to patrolduration then change state to new idle state
        {
            enemy.ChangeState(new IdleState());
        }
    }
}
