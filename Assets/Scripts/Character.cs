using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public Animator myanimator { get; private set; } // animation declaration

    protected bool right; // flip declaration

    protected bool attack;

    public bool Attack { get; internal set; } // check true or  ffalse to attack

    [SerializeField]
    protected int health; // heath to be seralized 

    [SerializeField]
    private EdgeCollider2D Sword;

    private float speed;

    public bool TakingDamage { get; set; } // taking damage

    public abstract bool IsDead { get; } // dead or not

    // Start is called before the first frame update
    public virtual void Start()
    {
        right = true;
        myanimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract IEnumerator TakeDamage(); //function to take the damage

    public void ChangeDirection() // function to change the direction of both character
    {
        right = !right;
        Vector3 theScale = transform.localScale;

        theScale.x *= -1;
        transform.localScale = theScale;
    }


    public void MeleeAttack() // function to enable to attacck
    {
        Sword.enabled = !Sword.enabled;
    }
    

    public virtual void OnTriggerEnter2D(Collider2D other) // function to use sword to collide and attack
    {
        if (other.tag == "Sword") // player sword collider
        {
            StartCoroutine(TakeDamage());
        }
    
        if (other.tag == "enemysword") // enemy sword collider
        {
            StartCoroutine(TakeDamage());
        }
    }
}
