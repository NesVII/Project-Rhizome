using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private float normalGravity;
    public Health health;
    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public LayerMask leverLayers;
    //public Movement2D player;
    //public Enemy E;
    

    public float attackRange = 0.5f;
    public int attackDamage = 1;

    public float attackRate = 2f;
    float nextAttackTime = 0f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        normalGravity = rb.gravityScale;

    }
    // Update is called once per frame
    void Update()
    {
        if (health.isDead)
        {
            return;
        }
    }

    public void CombatInput(InputAction.CallbackContext context)
    {
        if(Time.time >= nextAttackTime)
        {
            if (context.performed)
                    {
                        Attack();
                        nextAttackTime = Time.time + 1f / attackRate;
                    }
        }
        
    }

    public void Attack()
    {
        // Play an attack animation
        animator.SetTrigger("Attack");
        /*// Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);


        // Damage taken
        foreach(Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<Enemy>())
            {
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
            else if (enemy.GetComponentInChildren<BossHealth>())
            {
                enemy.GetComponentInChildren<BossHealth>().TakeDamage(attackDamage);
            }
            else if (enemy.GetComponent<FlyingEnemy>())
            {
                enemy.GetComponent<FlyingEnemy>().TakeDamage(attackDamage);
            }
            else if (enemy.GetComponent<Wyvern_Health>())
            {
                enemy.GetComponent<Wyvern_Health>().TakeDamage();
            }

        }

        Collider2D[] hitLever = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, leverLayers);
        foreach(Collider2D lever in hitLever)
        {
            if (lever.GetComponent<leverOpen>())
            {
                lever.GetComponent<leverOpen>().Hit();
            }
        }*/
    }

   /* public void HitEnemy()
    {
        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);


        // Damage taken
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<Enemy>())
            {
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
            else if (enemy.GetComponentInChildren<BossHealth>())
            {
                enemy.GetComponentInChildren<BossHealth>().TakeDamage(attackDamage);
            }
            else if (enemy.GetComponent<FlyingEnemy>())
            {
                enemy.GetComponent<FlyingEnemy>().TakeDamage(attackDamage);
            }
            else if (enemy.GetComponent<Wyvern_Health>())
            {
                enemy.GetComponent<Wyvern_Health>().TakeDamage();
            }

        }

        Collider2D[] hitLever = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, leverLayers);
        foreach (Collider2D lever in hitLever)
        {
            if (lever.GetComponent<leverOpen>())
            {
                lever.GetComponent<leverOpen>().Hit();
            }
        }
    }*/
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        

    }



}
