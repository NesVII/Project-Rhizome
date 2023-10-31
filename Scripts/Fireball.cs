using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Fireball : MonoBehaviour
{
    public Animator animator;
    public Transform firePoint;
    public GameObject fireballPrefab;
    [SerializeField] private bool canFire = true;
    //private float fireCooldown = 0.5f;
    [SerializeField] public int load = 0;
    public bool isInFire;
    [SerializeField] private Collider2D col;
    public bool isAbsorbing = false;
    public bool fireAbsorb;
    [SerializeField] float absorbRate=0.5f;
    float nextAbsorb=1f;
    float absorbCooldown = 0f;
    private void Awake()
    {
        //animator = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        
        if (load >= 1)
            canFire = true;
        else if (load <= 0)
        {
            canFire = false;
        }
        absorbCooldown += Time.deltaTime;
        //Debug.Log(isAbsorbing);
    }
    private void FixedUpdate()
    {
        AbsorbFire();
    }
    public void FireInput(InputAction.CallbackContext context)
    {
        if (context.performed && canFire)
        {
            animator.SetBool("isFireballing",true);
            load--;
            //Debug.Log("fire");
            Invoke("Shoot",0.3f);
        }
        
        
    }
    void Shoot()
    {
        //shooting logic
        //print("shooting");
        //Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);

        
        Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
        //Invoke("resetFire", fireCooldown);
        animator.SetBool("isFireballing", false);
    }

   /* private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Fire")
            isInFire = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Fire")
            isInFire = false;
    }
    */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Fire")
        {
            isInFire = true;
            Debug.Log(collision);
            col = collision;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Fire")
        {
            isInFire = false;
        }
    }

    /* void resetFire()
     {
         canFire = true;
     }*/


    public void Absorb(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isAbsorbing = true;
            Debug.Log(isAbsorbing);
            //AbsorbFire();
        }
        if (context.performed)
        {
            isAbsorbing = true;
            Debug.Log(isAbsorbing);
            //AbsorbFire();
        }
        if (context.canceled)
        {
            isAbsorbing = false;
        }
    }

    private void AbsorbFire()
    {
        if (isAbsorbing && absorbCooldown>nextAbsorb)
        {
            absorbCooldown = 0f;
            animator.SetBool("isFireballing",true);
            //Debug.Log("Held");
            if (isInFire && load < 3 && load >= 0) 
            {
                col.GetComponent<FireAbsorbingArea>().ReduceFire();
                    load++;
            }
        }
        else
        {
            animator.SetBool("isFireballing", false);
        }

    }


}

    
