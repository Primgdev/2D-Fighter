using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Enemy : Character
{
    

    private ienemyState currentState; // check the state of the enemy

    public GameObject Target { get; set; } // target the player game objecct


    [SerializeField]
    private float meleeRange; // to see thee player is in the melee range


    public Image HealthBar; // for health bar image above the enemy

    private float currenthealth; // current healt status of  the enemy after the attack


    public bool InMeleeRange //function for the enemy melee range
    {
        get
        {
            if(Target != null) // if the target is not in the range
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= meleeRange;
            }
            else
            {
                return false;
            }
        }
    }

    public override bool IsDead // if enemy is dead return health is 0
    {
        get
        {
            return health <= 0;
        }
    }

    private float speed; // declare speed of the enemy

    public Enemy()
    {
    }


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        ChangeState(new IdleState());
        speed = 4;
        currenthealth = health;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsDead)
        {
            if (!TakingDamage)
            {
                currentState.Execute();
            }
            
            LookAtTarget();
        }
        
    }


    public void ChangeState(ienemyState newState) // function to change the state of the enemy during patrol
    {
        if(currentState != null)
        {
            currentState.Exit();
        }


        currentState = newState;

        currentState.Enter(this);
    }


    public void Move() // move the enemy if not attacking
    {
        if (!Attack)
        {
            myanimator.SetFloat("speed", 1); // change the speed by 1
            transform.Translate(GetDirection() * (speed * Time.deltaTime)); //change the direction of enemy 


        }
    }


    public Vector2 GetDirection() //  get the player direction to follow
    {
        return right ? Vector2.right : Vector2.left;
    }

    public override void OnTriggerEnter2D(Collider2D other) // trriger to collide withh the player
    {
        base.OnTriggerEnter2D(other);
        currentState.OnTriggerEnter(other);
    }


    private void LookAtTarget() // to look at the player and follow
    {
        if(Target != null)
        {
            float Xdirection = Target.transform.position.x - transform.position.x;

            if(Xdirection <0 && right || Xdirection > 0  && !right) // change the firection as the player flip
                {
                ChangeDirection();
            }
        }
    }

    


    public override IEnumerator TakeDamage()// function to take damage ffrom the enemy
    {
        
       health -= 10;

        HealthBar.fillAmount = health / currenthealth; // health bar function


        if (!IsDead)
            {
            
            myanimator.SetTrigger("damage"); //play the animation after taking damage
            

        }
            else
            {
                myanimator.SetTrigger("death"); //death animation
                yield return null;
            
            

            }
        
    }
}
