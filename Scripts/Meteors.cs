using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteors : MonoBehaviour
{
    public GameObject meteorPrefab;
    public Transform pointA;
    public Transform pointB;
    public Transform pointC;
    public Transform pointD;
    public Transform pointE;
    public Wyvern_Health wh;
    private float delay = 0;
    private bool alternate = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        delay += Time.deltaTime;
        if (wh.isEnraged && wh.health <= 4 && wh.health > 0 && delay > 3 && !alternate) 
        {
            Instantiate(meteorPrefab, pointA.position, pointA.rotation);
            Instantiate(meteorPrefab, pointB.position, pointB.rotation);
            Instantiate(meteorPrefab, pointC.position, pointC.rotation);
            alternate = true;
            delay = 0;
        }
        if (wh.isEnraged && wh.health <= 4 && wh.health > 0 && delay > 3 && alternate)
        {
            Instantiate(meteorPrefab, pointD.position, pointD.rotation);
            Instantiate(meteorPrefab, pointE.position, pointE.rotation);
            alternate = false;
            delay = 0;
        }




    }
}
