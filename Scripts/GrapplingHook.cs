using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class GrapplingHook : MonoBehaviour
{
    public float grapplingSpeed = 10f;
    public float maxGrapplingDistance = 10f;
    public Transform hookShootPoint;
    public LayerMask grappleLayer;

    public bool isGrappling = false;
    [SerializeField] private Vector2 grapplePoint;
    private Rigidbody2D rb;
    private DistanceJoint2D joint;
    private float gravityScale;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       // joint = GetComponent<DistanceJoint2D>();
        //joint.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
       // GrappleHook();
    }
    private void FixedUpdate()
    {
        //GrappleHook();
        if (isGrappling)
        {
            Vector2 playerPosition = transform.position;
            Vector2 direction = (grapplePoint - playerPosition).normalized;
            float distanceToGrapple = Vector2.Distance(playerPosition, grapplePoint);

            float currentSpeed = Mathf.Lerp(grapplingSpeed, 0f, distanceToGrapple / grapplingSpeed);
            Vector2 targetVelocity = direction * currentSpeed;

            rb.velocity = targetVelocity;
        }
        
    }

    public void Grapple(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!isGrappling)
            {
            StartGrapple();
            }
            else
            {
                StopGrapple();
            }
           
        }
        Debug.DrawRay(hookShootPoint.position, Vector2.zero, Color.red, maxGrapplingDistance);

    }

    void StartGrapple()
    {
        Debug.Log("Starting");
        RaycastHit2D hit = Physics2D.Raycast(hookShootPoint.position, Vector2.zero, grappleLayer);
        if (hit.collider != null && hit.collider.gameObject.layer == grappleLayer) 
        {
            Debug.Log(hit);
            gravityScale = rb.gravityScale;
            isGrappling = true;
            grapplePoint = hit.point;
            //joint.enabled = true;
            //joint.connectedAnchor = grapplePoint;
            //joint.distance = Vector2.Distance(transform.position, grapplePoint);
            //joint.autoConfigureDistance = false;
            rb.gravityScale = 0;
            
        }
        else
        {
            Debug.Log("No anchor point found");
        }
    }
    void StopGrapple()
    {
        isGrappling = false;
        joint.enabled = false;
        rb.gravityScale = gravityScale;
    }

}
