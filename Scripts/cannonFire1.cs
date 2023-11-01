using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannonFire : MonoBehaviour
{
    public Transform firepoint;
    public GameObject fireballPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("Fire", 2f);
    }

    void Fire()
    {
        Instantiate(fireballPrefab, firepoint.position, firepoint.rotation);
    }
}
