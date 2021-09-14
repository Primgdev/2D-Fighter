using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : Character
{

    private Rigidbody2D rb2d; // rigidbody for the player

    //declare animator class for animation
    

    

    [SerializeField]
    private float speed; // speed for the player to move

    [SerializeField]
    private float groundarea; //to check the ground area

    [SerializeField]
    private LayerMask isground; // seralized to declare for the unity hub

    private Vector2 startPos;

    
    
    private bool isgrounded;// check if the player is grounded or not
    private bool jump; // check if it is true or false 

    [SerializeField]
    private bool aircontrol; 

    [SerializeField]
    private float jumpforce;

    public Rigidbody2D MyRigidbody2D { get; set; } //
    public new bool Attack { get; set; } // if the attack is true then attack
    public bool OnGround { get; set; } //check if the player is on ground and jump


    [SerializeField]
    private float meleeRange; // enemy is in range or not

    [SerializeField]
    private Transform[] groundpoints;


    private static Player instance;

 

    public static Player Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<Player>();
            }

            {
                return instance;
            }
        }
    }

    public override bool IsDead // if dead return 0
    {
        get
        {
            return health <= 0;
        }
    }

    public static int Health { get; internal set; }




    // Start is called before the first frame update
    public override void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); 

        
        base.Start();
        startPos = transform.position;
        
    }


    void Update()
    {
        if(!TakingDamage) //if not take damage from enemy
        {
            if(transform.position.y <= -14f) //transform the position of y by -14
            {
                MyRigidbody2D.velocity = Vector2.zero;
                transform.position = startPos;
            }
            
            HandleInput();//update the handle input
        }
        
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (!TakingDamage)
        {

            float horizontal = Input.GetAxis("Horizontal");



            HandleMovement(horizontal);//update horizontal movement

            Flip(horizontal); //update the flip

            HandleAttack(); //update the handle attack

            ResetValue(); // reset all the values

            isgrounded = IsGrounded(); // check if grounded

        }
    }

   

    private void HandleMovement(float horizontal)//function for the player's movement
    {

        if (!this.myanimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && (isgrounded || aircontrol))
        {
            rb2d.velocity = new Vector2(horizontal * speed, rb2d.velocity.y);
            myanimator.SetFloat("speed", Mathf.Abs(horizontal));
        }

        if(isgrounded &&  jump) // if is ggrounded or jump then add force
        {
            isgrounded = false;
            rb2d.AddForce(new Vector2(0, jumpforce));

        }

    }


    private void HandleAttack()//function for player to attack
    {
        if(attack && !this.myanimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            myanimator.SetTrigger("Attack"); // player the attacck animation
            

            rb2d.velocity = Vector2.zero;
        }
    }



   



    



    private void HandleInput()//key input to attack
    {
        if (Input.GetKeyDown(KeyCode.Space)) // press space to  jump
        {
            jump = true;
            myanimator.SetTrigger("jump"); //animation to jump
           
        }


        if(Input.GetKeyDown(KeyCode.LeftShift)) // press left shit to attack
        {
            attack = true;
            myanimator.SetTrigger("Attack"); //play attack animation
            


        }
    }


    private void Flip(float horizontal)//to flip the player whereever facing
    {
        if(horizontal > 0 && right || horizontal < 0 && !right)
        {
            
            ChangeDirection(); //function to change the direction
        }
    }

    private void ResetValue() // reset all the value
    {
        attack = false;
        jump = false;
    }

    private bool IsGrounded() // to check if the player is on the ground before jump
    {
        if (rb2d.velocity.y <= 0)
        {
            foreach(Transform point in groundpoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundarea, isground);
                for(int i = 0; i<colliders.Length; i++)
                {
                    if(colliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false; // else return false
    }

    public override IEnumerator TakeDamage() // function to take the damage from the enemy
    {
        health -= 10; // decrease the health by -10
       


        if (!IsDead) //if not death
        {
            
            myanimator.SetTrigger("damage");
            

        }
        else // if death
        {
            myanimator.SetTrigger("death");
            SceneManager.LoadScene(4); // load the scene 4 if death
        }
            yield return null;
        
    }

    
}