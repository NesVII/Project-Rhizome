using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Start : MonoBehaviour
{

    public GameObject Boss;
    // Start is called before the first frame update
    void Start()
    {
        Boss.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("entered");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Entered");
            Boss.SetActive(true);

        }
    }
}
