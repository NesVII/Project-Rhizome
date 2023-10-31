using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wyvern_Stunned : StateMachineBehaviour
{
    private float timer = 5f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<WyvernSlam>().enabled = false ;
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer -= Time.deltaTime;
        Debug.Log(timer);

        if (animator.GetComponent<Wyvern_Health>().isEnraged)
        {
            animator.SetBool("IsEnraged", true);
        }

        if (timer <= 0)
        {
            animator.SetTrigger("Recovery");
            animator.SetBool("IsStunned", false);
            animator.GetComponent<Wyvern_Health>().hits = 0;
            timer = 5f;
        }
        if (animator.GetComponent<Wyvern_Health>().hits == 3)
        {
            animator.SetTrigger("Recovery");
            animator.SetBool("IsStunned", false);
            animator.GetComponent<Wyvern_Health>().hits = 0;
            timer = 5f;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

}
