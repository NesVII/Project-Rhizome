using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leverOpen : MonoBehaviour
{
    //[SerializeField] Transform toRotate;
    //[SerializeField] SpriteRenderer sr;
    [SerializeField] private bool isOpen;
    [SerializeField] Transform doorPos;
    public Vector3 myPos;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        //toRotate = GetComponentInChildren<Transform>();
        //sr = GetComponentInChildren<SpriteRenderer>();
        isOpen = false;
        myPos = doorPos.position;
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Hit()
    {
        if (!isOpen)
        {
            animator.SetTrigger("Hit");
            isOpen = true;
            Debug.Log("Lever hit");
            //toRotate.Rotate(0f, 0f, -90f);
            Debug.Log("Lever rotate");
            //sr.color = Color.green;
            myPos.y += 3f;
            doorPos.position = myPos;
        }
    }
}
