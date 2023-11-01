using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Wyvern_Health : MonoBehaviour
{
    public int health = 10;
    /*public Transform player;
    public CinemachineVirtualCamera VC;*/
    public bool isInvulnerable = false;
    public bool isEnraged = false;
    public float hits=0f;
    Animator animator;
    private float stunTime = 3f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    public void TakeDamage()
    {
        if (isInvulnerable)
        {
            return;
        }
        hits++;
        
        
        animator.SetTrigger("Hurt");
        health--;

        if (health <= 3)
        {
            GetComponent<Animator>().SetBool("IsEnraged",true);
            isEnraged = true;
            animator.SetBool("IsStunned", false);
            animator.SetTrigger("Recovery");
        }
        
        if (health <= 0)
        {
            Die();

        }
        if (hits == 3)
        {
            animator.SetTrigger("Recovery");
            animator.SetBool("IsStunned", false);
            hits = 0;
        }
        Debug.Log(hits);
    }

    void Die()
    {
        animator.SetBool("IsDead", true);
        
        Destroy(gameObject, 5f);
        //VC.Follow = player;
    }
}
