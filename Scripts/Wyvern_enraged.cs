using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wyvern_enraged : StateMachineBehaviour
{
    Rigidbody2D rb;
    GameObject point;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        point = GameObject.FindGameObjectWithTag("PatrolPoint");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 target = new Vector2(rb.position.x, point.transform.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, 15 * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
        if (rb.position == target)
        {
            //target = rb.position;
            animator.SetTrigger("Roar");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }


}
