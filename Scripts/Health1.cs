using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.InputSystem;

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

    public float perfectParryWindow = 0.4f; // Fenêtre de temps pour réaliser un perfect parry
    public float parryCooldown = 0.5f; // Durée de l'animation du perfect parry
    public LayerMask enemyLayer; // Layer des ennemis

    private bool isParrying = false; // Indique si le joueur est en train de parer une attaque
    private bool canParry = true; // Indique si le joueur est en train de parer une attaque
    private float parryTimer = 0f; // Timer pour le perfect parry

    public bool isInvulnerable;

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

    public void Parry(InputAction.CallbackContext context)
    {
        if (context.performed && canParry)
        {
            isParrying = true;
            canParry = false;
            parryTimer = perfectParryWindow;
            //Debug.Log("Start Parrying");
            animator.SetTrigger("Parry");
        }
    }

    private void Update()
    {
        if (isParrying)
        {
            parryTimer -= Time.deltaTime;

            if (parryTimer <= 0f)
            {
                isParrying = false;
                Invoke("resetParry", parryCooldown);
            }
        }
    }

    void resetParry()
    {
        canParry = true;
    }
        

    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
        {
            return;
        }
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f, enemyLayer);
        foreach (Collider2D collider in colliders)
        {
            if (parryTimer > 0f)
            {
                Debug.Log("Perfect Parry!");
            }
        }
        if (!isParrying)
        {
            Debug.Log("ouch");
            if (isDead)
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
}
