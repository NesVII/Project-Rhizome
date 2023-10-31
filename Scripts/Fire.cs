using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;

    public int fireballDamage = 3;
    public GameObject impactEffect;
    

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        //Debug.Log(hitInfo);
        // if(hitInfo.tag != "Player")
        // {
        if (hitInfo.tag == "Enemy")
        {
            Enemy enemy = hitInfo.GetComponent<Enemy>();
            if (enemy != null)
            {
                rb.velocity = Vector2.zero;
                enemy.TakeDamage(fireballDamage);
                Destroy(gameObject);
            }
        }
        if (hitInfo.tag == "Boss")
        {
            BossHealth BH = hitInfo.GetComponent<BossHealth>();
            if (BH != null)
            {
                rb.velocity = Vector2.zero;
                BH.TakeDamage(fireballDamage);
                Destroy(gameObject);
            }
            //  }

            



            //Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject, 3f);
        }

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
