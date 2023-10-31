using UnityEngine;

public class SlimeEnemy : MonoBehaviour
{
    
    public float jumpForce = 5f;
    public float detectionDistance = 3f;
    public float jumpDelay = 2f;
    public float jumpHeight = 1f;
    private bool canJump = true;
    private Rigidbody2D rb;
    
    public int maxHealth = 100;
    public int currentHealth;
    public Health health;
    [SerializeField] private int damage = 1;
    public Movement2D playerMovement;
    public float KnockbackForce;
    /* public float KnockbackCounter;
     public float KBTotalTime;*/
    public bool KnockFromRight = true;
    private Transform player;

    [SerializeField] private AudioSource hurtSE;
    [SerializeField] private AudioSource jumpSE;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject playerObject = GameObject.Find("PlayerV3");
        player = playerObject.GetComponent<Transform>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionDistance && canJump)
        {
            JumpTowardsPlayer();
            canJump = false;
            Invoke("ResetJump", jumpDelay);
        }
    }

    private void JumpTowardsPlayer()
    {
        jumpSE.Play();
        Vector2 direction = (player.position - transform.position).normalized;
        Vector2 jumpDirection = new Vector2(direction.x + 0.3f, direction.y + jumpHeight).normalized; // Ajoute un léger ajustement vers le haut pour créer l'arc de saut

        rb.AddForce(jumpDirection * jumpForce, ForceMode2D.Impulse);
    }

    private void ResetJump()
    {
        canJump = true;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Health>().TakeDamage(1);
        }
    }


    public void TakeDamage(int damage)
    {
        hurtSE.Play();
        currentHealth -= damage;

        rb.isKinematic = true;
        rb.velocity = Vector2.zero;


        //KnockBack
        //Vector2 direction = (transform.position - playerMovement.transform.position).normalized;
        // rb.AddForce(direction * strength, ForceMode2D.Impulse);



        if (currentHealth <= 0)
        {
            Die();

        }
        Invoke("Reset", 0.5f);
    }
    void Die()
    {
        Debug.Log("Enemy Died");

        //Die Animation


        //Disable the enemy

        GetComponent<Collider2D>().enabled = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        this.enabled = false;
        Invoke("Remove", 0.4f);
    }

    private void Remove()
    {
        Destroy(gameObject);
    }


}
