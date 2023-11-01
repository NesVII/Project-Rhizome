using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;

    public int fireballDamage = 50;
    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
       
        if (hitInfo.tag == "Enemy")
        {
            Enemy enemy = hitInfo.GetComponent<Enemy>();
            if (enemy != null)
            {
                rb.velocity = Vector2.zero;
                enemy.TakeDamage(fireballDamage);
                Explode();
            }
            SlimeEnemy slimeEnemy = hitInfo.GetComponent<SlimeEnemy>();
            if (slimeEnemy != null)
            {
                rb.velocity = Vector2.zero;
                slimeEnemy.TakeDamage(fireballDamage);
                Explode();
            }
        }
        if (hitInfo.tag == "Boss")
        {
            BossHealth BH = hitInfo.GetComponent<BossHealth>();
            if (BH != null)
            {
                rb.velocity = Vector2.zero;
                BH.TakeDamage(fireballDamage);
                Explode();
            }
            
        }
        if (hitInfo.tag == "Ice")
        {
           
            Destroy(hitInfo.gameObject);
            Explode();
        }
        if (hitInfo.tag == "Wall" || hitInfo.tag == "Ground")
        {
            Explode();
        }

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void Explode()
    {
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
