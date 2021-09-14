using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{

    [SerializeField]
    private Enemy enemy; //declare enemy


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")// tag = player
        {
            enemy.Target = collision.gameObject; //collision of enemy sight
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            enemy.Target = null; // return null to exit the function
        }
    }


}
