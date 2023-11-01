using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wyvern_Slamming : StateMachineBehaviour
{
    private Rigidbody2D rb;
    public float slamSpeed = 10f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb.velocity = new Vector2(0,-slamSpeed*Time.fixedDeltaTime) ;
        if (rb.GetComponent<WyvernSlam>().grounded)
        {
            animator.SetTrigger("Grounded");
            rb.velocity = Vector2.zero;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Grounded");
        rb.GetComponent<WyvernSlam>().enabled = false;
    }

}
