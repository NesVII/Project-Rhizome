using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPatrol : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Animator animator;
    private Transform currentPoint;
    public float speed;
    public float enragedSpeed;
    private float whichSpeed;
    public Vector2 originalPoint;
    public float flipTime;
    public Wyvern_Health wh;
    public float slamTime = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentPoint = pointB.transform;
        wh = GetComponent<Wyvern_Health>();
        //animator.SetBool("Close", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (wh.isEnraged)
            whichSpeed = enragedSpeed;
        else
        {
            whichSpeed = speed;
        }
        
        slamTime += Time.deltaTime;
        
        
        /*if (slamTime > 5f)
        {
            animator.SetBool("StartSlam", true);
            slamTime = 0f;
        }*/
        //Debug.Log(Vector2.Distance(transform.position, currentPoint.position));
        Vector2 point = currentPoint.position - transform.position;
        if(currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(whichSpeed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-whichSpeed, 0);
        }

        /*if (Vector2.Distance(transform.position, currentPoint.position) < flipTime)
        {
            animator.SetTrigger("Flip");
        }*/

        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint.position.x == pointB.transform.position.x)
        {
            currentPoint = pointA.transform;
            
            Flip();
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint.position.x == pointA.transform.position.x)
        {
            currentPoint = pointB.transform;
            
            Flip();
        }
        if(transform.position.y != pointA.transform.position.y)
        {
            var target = new Vector2(transform.position.x, pointA.transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.Rotate(0f, 180f, 0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}
