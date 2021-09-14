using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    private Rigidbody2D rb2d;

     //declare animator class for animation

   


    [SerializeField]
    private float speed; 

    [SerializeField]
    private float groundarea;

    [SerializeField]
    private LayerMask isground;

    private Vector2 startPos;

    
    
    private bool isgrounded;
    private bool jump;

    [SerializeField]
    private bool aircontrol;

    [SerializeField]
    private float jumpforce;

    public Rigidbody2D MyRigidbody2D { get; set; }
    public new bool Attack { get; set; }
    public bool OnGround { get; set; }
    

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

    public override bool IsDead
    {
        get
        {
            return health <= 0;
        }
    }


    // Start is called before the first frame update
    public override void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        
        base.Start();
        startPos = transform.position;
        

    }


    void Update()
    {
        HandleInput();//update the handle input
    }



    // Update is called once per frame
    void FixedUpdate()
    {

        float horizontal = Input.GetAxis("Horizontal");
      


        HandleMovement(horizontal);//update horizontal movement

        Flip(horizontal); //update the flip

        HandleAttack(); //update the handle attack

        ResetValue(); // reset all the values

        isgrounded = IsGrounded(); // check if grounded
                                                                                                            
    }



    private void HandleMovement(float horizontal)//function for the player's movement
    {

        if (!this.myanimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && (isgrounded || aircontrol))
        {
            rb2d.velocity = new Vector2(horizontal * speed, rb2d.velocity.y);
            myanimator.SetFloat("speed", Mathf.Abs(horizontal));
        }

        if(isgrounded &&  jump)
        {
            isgrounded = false;
            rb2d.AddForce(new Vector2(0, jumpforce));

        }

    }


    private void HandleAttack()//function for player to attack
    {
        if(attack && !this.myanimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            myanimator.SetTrigger("attack");
            rb2d.velocity = Vector2.zero;
        }
    }



   



    



    private void HandleInput()//key input to attack
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
            myanimator.SetTrigger("jump");
           
        }


        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            attack = true;
            myanimator.SetTrigger("attack");
            
            
        }
    }


    private void Flip(float horizontal)//to flip the player whereever facing
    {
        if(horizontal > 0 && right || horizontal < 0 && !right)
        {
            
            ChangeDirection();
        }
    }

    private void ResetValue()
    {
        attack = false;
        jump = false;
    }

    private bool IsGrounded()
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
        return false;
    }

    public override IEnumerator TakeDamage()
    {
        
        
            yield return null;
        
    }
}