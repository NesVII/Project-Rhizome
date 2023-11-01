using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_move : StateMachineBehaviour
{
    public float speed = 2.5f;
    public float attackRange = 1.24f;
    Transform player;
    Rigidbody2D rb;
    Enemy enemy;
    public float attackCooldown = 2f;
    private float delay = 0f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        enemy = animator.GetComponent<Enemy>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        delay += Time.deltaTime;
        enemy.LookAtPlayer();
        if (enemy.currentHealth <= 0 || enemy.damaged)
            return;
        else { 
            Vector2 target = new Vector2(player.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            if (Vector2.Distance(player.position, rb.position) >= attackRange)
            {
                rb.MovePosition(newPos);
            }
            else if (Vector2.Distance(player.position, rb.position) <= attackRange && delay >= attackCooldown)
            {
                animator.SetTrigger("Attack");
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
