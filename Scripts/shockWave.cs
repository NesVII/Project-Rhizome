using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shockWave : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;

    public int shockwaveDamage = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Health player = collision.gameObject.GetComponent<Health>();
            if (collision != null)
            {
                player.TakeDamage(shockwaveDamage);
            }
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
