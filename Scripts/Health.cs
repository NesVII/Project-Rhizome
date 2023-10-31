using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Animator animator;
    [SerializeField] public int currentHealth, maxHealth;
   // public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;
    [SerializeField] public bool isDead = false;
    public GameObject heartImage;
    public Sprite emptyHeartSprite;
    public Sprite fullHeartSprite;
    public GameObject Gameover;
    private Movement2D mov;
    private Rigidbody2D rb;
    public bool isInvulnerable = false;

    public void InitializeHealth(int healthValue)
    {
        currentHealth = healthValue;
        maxHealth = healthValue;
        isDead = false;
        
    }
    
    void Start()
    {
        //UpdateHeartDisplay();
        Gameover.SetActive(false);
        mov = GetComponent<Movement2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    
    
     void UpdateHeartDisplay()
    {
        for (int i = 0; i < heartImage.transform.childCount; i++)
        {
            Image heart = heartImage.transform.GetChild(i).GetComponent<Image>();
            if (currentHealth > i)
            {
                heart.sprite = fullHeartSprite; // Affiche le coeur plein si le joueur a assez de points de vie
            }
            else
            {
                heart.sprite = emptyHeartSprite; // Affiche le coeur vide si le joueur n'a pas assez de points de vie
            }
        }
    }
  /*  public void GetHit(int amount, GameObject sender)
    {
        if (isDead)
            return;
        if (sender.layer == gameObject.layer)
            return;
        currentHealth -= amount;

        if(currentHealth > 0)
        {
            OnHitWithReference?.Invoke(sender);
        }
        else
        {
            OnDeathWithReference?.Invoke(sender);
            isDead = true;
            Destroy(gameObject);
        }
    }*/
  public void TakeDamage(int damage)
    {
        if (isDead || isInvulnerable) 
        {
            
            return;
        }
        animator.SetTrigger("isHurt");
        currentHealth -= damage;
        UpdateHeartDisplay();
        if (currentHealth <= 0)
        {
            isDead = true;
            animator.SetBool("IsDead", true);
            Gameover.SetActive(true);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            Destroy(mov, 1f);
        }
    }
}
