using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public Animator animator;
    [SerializeField] Rigidbody2D rb;
    public int maxHealth = 1;
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
    public bool damaged;
    public bool isFlipped = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LookAtPlayer()
    {
        if(transform.position.x>playerMovement.transform.position.x && isFlipped)
        {
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        } else if(transform.position.x<playerMovement.transform.position.x && !isFlipped)
        {
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        rb.isKinematic = true;
        rb.velocity = Vector2.zero;

        animator.SetTrigger("Hurt");
        damaged = true;

        if(currentHealth <= 0)
        {
            Die();
        }
        Invoke("Reset", 0.5f);
    }

    private void Reset()
    {
        damaged = false;
    }

    void Die()
    {
        Debug.Log("Flying Enemy Died");
        animator.SetBool("isDeath", true);

        GetComponent<Collider2D>().enabled = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        this.enabled = false;
        Invoke("Remove", 1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerMovement.KBCounter = playerMovement.KbTotalTime;
            if (collision.transform.position.x <= transform.position.x)
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
