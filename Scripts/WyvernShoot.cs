using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WyvernShoot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject wyverFireballPrefab;

    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*cooldown += Time.deltaTime;
        if (cooldown >= 5f)
        {
            ShootPlayer();
            cooldown = 0;
        }*/
    }


    public void ShootPlayer()
    {
        //animator.SetTrigger("Firing");
        Instantiate(wyverFireballPrefab, firePoint.position, firePoint.rotation);
    }
}
