using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wyvern_Start : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        animator.GetComponent<Wyvern>().enabled = true;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*if (transform.position.y != pointA.transform.position.y)
        {
            var target = new Vector2(transform.position.x, pointA.transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }*/
        if (animator.GetComponent<Transform>().position.y != animator.GetComponent<enemyPatrol>().pointA.transform.position.y)
        {
            var target = new Vector2(animator.GetComponent<Transform>().position.x, animator.GetComponent<enemyPatrol>().pointA.transform.position.y);
            animator.GetComponent<Transform>().position = Vector2.MoveTowards(animator.GetComponent<Transform>().position, target,
                animator.GetComponent<enemyPatrol>().speed * Time.deltaTime);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Wyvern_Health>().hits = 0;
        animator.GetComponent<enemyPatrol>().enabled = true;
        animator.SetBool("Flying", true);
        Debug.Log("Flying");
    }


}
