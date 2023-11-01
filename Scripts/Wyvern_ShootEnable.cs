using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wyvern_ShootEnable : StateMachineBehaviour
{
    private float delay = 0f;
    public float attackCooldown;
    public float slamTimer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<WyvernShoot>().enabled = true;
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        slamTimer += Time.deltaTime;
        delay += Time.deltaTime;
        if (delay >= attackCooldown)
        {
            animator.SetTrigger("Fireball");
            delay = 0;
        }
        if (slamTimer > 5f)
        {
            animator.SetBool("StartSlam", true);
            animator.SetBool("Flying", false);
            slamTimer = 0f;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Fireball");

    }


}
