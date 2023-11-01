using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAbsorbingArea : MonoBehaviour
{
    public GameObject player;
    public bool isInFire = false;
    public int fireAmount = 3;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger("Load", Mathf.Abs(fireAmount));

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.tag == "Player")
        {
            isInFire = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isInFire = false;
        }
    }

    public void ReduceFire()
    {
        if(fireAmount>0)
            fireAmount--;
    }
}
