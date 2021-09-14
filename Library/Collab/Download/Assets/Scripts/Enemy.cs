using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{

    private ienemyState currentState;

    public GameObject Target { get; set; }


    [SerializeField]
    private float meleeRange;


    public bool InMeleeRange
    {
        get
        {
            if(Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= meleeRange;
            }
            else
            {
                return false;
            }
        }
    }

    private float speed;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        ChangeState(new IdleState());
        speed = 4;
       
    }

    // Update is called once per frame
    void Update()
    {

        currentState.Execute();
        LookAtTarget();
        
    }


    public void ChangeState(ienemyState newState)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }


        currentState = newState;

        currentState.Enter(this);
    }


    public void Move()
    {
        if (!Attack)
        {
            myanimator.SetFloat("speed", 1);
            transform.Translate(GetDirection() * (speed * Time.deltaTime));


        }
    }


    public Vector2 GetDirection()
    {
        return right ? Vector2.right : Vector2.left;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentState.OnTriggerEnter(collision);
    }


    private void LookAtTarget()
    {
        if(Target != null)
        {
            float Xdirection = Target.transform.position.x - transform.position.x;

            if(Xdirection <0 && right || Xdirection > 0  && !right)
                {
                ChangeDirection();
            }
        }
    }
}
