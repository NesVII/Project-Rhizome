using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyFollow : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public float attackRange;
    public float speed;
    private GameObject player;
    public float lineOfSite;
    public float distanceFromPlayer;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);
        if(distanceFromPlayer<lineOfSite && distanceFromPlayer > attackRange)
        {
            animator.SetBool("Moving", true);
        } else if (distanceFromPlayer > lineOfSite || distanceFromPlayer< attackRange){
            animator.SetBool("Moving", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
