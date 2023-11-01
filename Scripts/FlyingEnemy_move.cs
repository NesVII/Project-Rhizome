using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy_move : StateMachineBehaviour
{
    public float speed = 2.5f;
    public float attackRange = 3f;
    Transform player;
    Rigidbody2D rb;
    FlyingEnemy fe;
    public float attackCooldown = 2f;
    private float delay = 0f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        fe = animator.GetComponent<FlyingEnemy>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        delay += Time.deltaTime;
        fe.LookAtPlayer();
        if (fe.currentHealth <= 0 || fe.damaged)
            return;
        else
        {
            Vector2 target = new Vector2(player.position.x, player.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            if (Vector2.Distance(player.position, rb.position) >= attackRange)
            {
                rb.MovePosition(newPos);
            }
            else if(Vector2.Distance(player.position, rb.position)<=attackRange && delay >= attackCooldown)
            {
                animator.SetTrigger("Attack");
                Debug.Log("Attack");
                
                delay = 0;
            }
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }


}
