using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossHealth : MonoBehaviour
{
    public int health = 10;

    public Transform joueur;

    public CinemachineVirtualCamera virtualCamera;

    public GameObject deathEffect;

    public bool isInvulnerable = false;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
            return;
        health -= damage;
        animator.SetTrigger("Hurt");

        if (health <= 5)
        {
            GetComponent<Animator>().SetBool("IsEnraged", true);
        }

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        virtualCamera.Follow = joueur;
    }
}
