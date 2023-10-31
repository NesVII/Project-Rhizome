using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = false;
    //public float distanceEngage = 10f;
    

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
       // this.gameObject.SetActive(false);
    }

    private void Update()
    {
        
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if(transform.position.x>player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if(transform.position.x< player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
   /* private void OnDrawGizmosSelected()
    {
        
        Gizmos.DrawWireSphere(transform.position, distanceEngage);

    }*/



}
