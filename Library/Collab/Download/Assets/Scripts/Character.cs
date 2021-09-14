using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public Animator myanimator { get; private set; }

    protected bool right;

    protected bool attack;

    public bool Attack { get; internal set; }

    [SerializeField]
    protected int health;

    [SerializeField]
    private EdgeCollider2D Sword;

    private float speed;

    public bool TakingDamage { get; set; }

    public abstract bool IsDead { get; }

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

    public abstract IEnumerator TakeDamage();

    public void ChangeDirection()
    {
        right = !right;
        Vector3 theScale = transform.localScale;

        theScale.x *= -1;
        transform.localScale = theScale;
    }


    public void MeleeAttack()
    {
        Sword.enabled = !Sword.enabled;
    }
    

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
      if(other.tag == "Sword")
        {
            StartCoroutine(TakeDamage());
        }
    }
}
