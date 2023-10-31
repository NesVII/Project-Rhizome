using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WyvernSlam : MonoBehaviour
{
    [SerializeField] private Vector3 target;
    public Vector3 originalPosition;
    public float aimOffset = 1.0f;
    public Rigidbody2D rb;
    public int slamDamage = 1;
    public Vector2 player;
    public float slamSpeed = 10f;
    public float speed = 1f;
    public float switchOffset = 0f;
    public float slamTimer=5f;
    public bool grounded;
    [SerializeField] private LayerMask groundLayer;
    public Transform groundSpot;
    [SerializeField] private float groundCooldown = 0f;
    public bool recovering = false;
    public bool following = false;
    public bool slaming = false;
    public bool stunned = false;
    public float stunDuration = 4f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        rb.GetComponent<enemyPatrol>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
            CheckGround();
        if (grounded)
        {
            stunned = true;
            rb.velocity = Vector2.zero;
        }
/*        if (recovering)
        {
            Recovery();
        }*/
    }


    void CheckGround()
    {
        grounded = Physics2D.OverlapCircle(groundSpot.position, 0.2f, groundLayer);
    }
/*    void SlamAttack()
    {
        following = false;
        rb.velocity = new Vector2(0f, -slamSpeed * Time.deltaTime);
        
    }

    void Recovery()
    {
        stunDuration -= Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, originalPosition, speed * Time.deltaTime);
    }*/
}
