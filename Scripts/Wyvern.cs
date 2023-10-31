using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wyvern : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = false;

    [SerializeField] private int damage = 1;

    [Header("Knock Back")]
    public float KnockBackForce;
    public bool KnockFromRight = true;
    public Movement2D playerMovement;
    public Health health;

    public Transform shockWavepointA;
    public Transform shockWavepointB;
    public GameObject shockWavePrefab;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement2D>();
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        //flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            //transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            //transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
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

    public void ShockWaveSpawn()
    {
        Instantiate(shockWavePrefab, shockWavepointA.position, shockWavepointA.rotation);
        Instantiate(shockWavePrefab, shockWavepointB.position, shockWavepointB.rotation);
    }
}
