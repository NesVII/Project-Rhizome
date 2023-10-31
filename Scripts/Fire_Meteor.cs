using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Meteor : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;

    public int fireballDamage = 1;
    public GameObject impactEffect;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = new Vector2(0f, -speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health player = collision.gameObject.GetComponent<Health>();
            Fireball abs = collision.gameObject.GetComponent<Fireball>();
            if (collision != null && !abs.isAbsorbing)
            {
                rb.velocity = Vector2.zero;
                player.TakeDamage(fireballDamage);
                Destroy(gameObject);
            }
            else if (collision != null && abs.isAbsorbing)
            {
                rb.velocity = Vector2.zero;
                //player.TakeDamage(fireballDamage);
                Debug.Log("Absorbed");
                abs.load++;
                Destroy(gameObject);
            }
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
