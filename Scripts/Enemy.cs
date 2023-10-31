using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    [SerializeField] Rigidbody2D rb;
    public int maxHealth = 2;
    public int currentHealth;
    public Health health;
    [SerializeField] private int damage = 1;
    public Movement2D playerMovement;

    [Header("Knock back")]
    public float KnockbackForce;
   /* public float KnockbackCounter;
    public float KBTotalTime;*/
    public bool KnockFromRight = true;

    public float deathTimer = 5f;
    public EnemyFollow EF;
    public bool damaged;
    public bool isFlipped=false;

    public bool isInvulnerable;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement2D>();
    }

    private void Update()
    {
       /* if (EF.close)
        {
        animator.SetBool("Close", true);
        }
        else if (!EF.close)
        {
            animator.SetBool("Close", false);
        }
        if (rb.isKinematic == true)
        {
            rb.isKinematic = false;
        }*/
    }

    public void LookAtPlayer()
    {
        if(transform.position.x>playerMovement.transform.position.x && isFlipped)
        {
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if( transform.position.x<playerMovement.transform.position.x && !isFlipped)
        {
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
            return;
        currentHealth -= damage;
        Debug.Log("hit");

        rb.isKinematic = true;
        rb.velocity = Vector2.zero;

        animator.SetTrigger("Hurt");
        damaged = true;

        //KnockBack
        //Vector2 direction = (transform.position - playerMovement.transform.position).normalized;
        // rb.AddForce(direction * strength, ForceMode2D.Impulse);
       
        /*if (!EF.isRightOfPlayer)
        {
            rb.velocity = new Vector2(-KnockbackForce, 0f);
            Invoke("resetVelocity", 0.1f);
        }
        else if (EF.isRightOfPlayer)
        {
            rb.velocity = new Vector2(KnockbackForce, 0f);
            Invoke("resetVelocity", 0.1f);
        }*/
        


        if (currentHealth <= 0)
        {
            Die();

        }
        Invoke("Reset", 0.5f);
    }

    private void resetVelocity()
    {
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
    }
    private void Reset()
    {
        damaged = false;
    }
    void Die()
    {
        Debug.Log("Enemy Died");

        //Die Animation
        animator.SetBool("isDeath", true);

        //Disable the enemy

        GetComponent<Collider2D>().enabled = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        
        this.enabled = false;
        Invoke("Remove", 2f);
    }

    //DON'T TOUCH
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerMovement.KBCounter = playerMovement.KbTotalTime;
            if(collision.transform.position.x <= transform.position.x)
            {
                playerMovement.KnockFromRight = true;
            }
            else if (collision.transform.position.x > transform.position.x)
            {
                playerMovement.KnockFromRight = false;
            }
            health.TakeDamage(damage);
        }
    }


    private void Remove()
    {
        Destroy(gameObject);
    }
}
