using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WyvernFireball : MonoBehaviour
{
    public float speed = 5f;
    public float aimOffset = 1.0f;
    public Rigidbody2D rb;
    public int fireballDamage = 1;
    public Vector2 player;
    public Vector3 currentPosition;
    public float rotationModifier;
    Vector3 shootdir;
    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        currentPosition = new Vector2(player.x, player.y + aimOffset);
        //transform.rotation = Quaternion.LookRotation(currentPosition.transform.rotation);
        shootdir = currentPosition - transform.position;
        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(shootdir));
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += transform.forward * speed * Time.deltaTime;
        //rb.AddRelativeForce(Vector3.forward * speed, ForceMode2D.Impulse);

        //transform.position = Vector2.MoveTowards(transform.position, currentPosition.position, speed);

        // transform.position += shootdir * speed * Time.deltaTime;
        //transform.position = Vector2.MoveTowards(transform.position, currentPosition, speed * Time.deltaTime);
        transform.position += shootdir * speed * Time.deltaTime;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
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
            else if(collision != null && abs.isAbsorbing)
            {
                rb.velocity = Vector2.zero;
                //player.TakeDamage(fireballDamage);
                Debug.Log("Absorbed");
                abs.load++;
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Fireball player = collision.gameObject.GetComponent<Fireball>();
            if (collision != null && player.isAbsorbing)
            {
                rb.velocity = Vector2.zero;
                //player.TakeDamage(fireballDamage);
                Debug.Log("Absorbed");
                player.load++;
                Destroy(gameObject);
            }
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

}
