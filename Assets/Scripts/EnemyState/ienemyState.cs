using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ienemyState 
{
    void Execute(); // to execute
    void Enter(Enemy enemy); //to enter
    void Exit(); //to exit
    void OnTriggerEnter(Collider2D other);
}
