using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wyvern_IsStun : StateMachineBehaviour
{
    Wyvern wyvern;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        wyvern = animator.GetComponent<Wyvern>();
        animator.GetComponent<WyvernSlam>().enabled = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //wyvern.LookAtPlayer();
        animator.SetBool("IsStunned", true);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }


}
