using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public LayerMask leverLayers;


    public float attackRange = 0.5f;
    public int attackDamage = 1;

    public float attackRate = 2f;
    float nextAttackTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HitEnemy()
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
    }
}
