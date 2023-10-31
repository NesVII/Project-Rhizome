using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFire : MonoBehaviour
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
