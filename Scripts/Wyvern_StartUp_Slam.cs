using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wyvern_StartUp_Slam : StateMachineBehaviour
{
    private Vector3 target;
    Transform player;
    Rigidbody2D rb;
    public float slamTime = 5f;
    public float switchOffset = 0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<enemyPatrol>().enabled = false;
        animator.GetComponent<WyvernSlam>().enabled = true;
        animator.GetComponent<WyvernSlam>().following = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        switchOffset += Time.deltaTime;
        slamTime -= Time.deltaTime;
        /*if (switchOffset % 2 == 0)
        {
            target = new Vector2(player.position.x+1f, rb.position.y);
        }
        else if (switchOffset % 2 != 0)
        {
            target = new Vector2(player.position.x - 1f, rb.position.y);
        }*/
        target = new Vector2(player.position.x,rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, 10*Time.fixedDeltaTime);
        rb.MovePosition(newPos);
        if (slamTime <= 0f)
        {
            animator.SetTrigger("Slam");
            slamTime = 5f;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("StartSlam", false);
        animator.GetComponent<WyvernSlam>().following = false;
    }


}
