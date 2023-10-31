using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public Animator animator;
    private Rigidbody2D rb;
    [Header("Movement Variables")]
    private float moveVector;
    
    public float moveSpeed;
    [SerializeField] bool isFacingRight;
    [Space(10)]
    [SerializeField] private float movementAcceleration;
    [SerializeField] private float maxMoveSpeed;
    [SerializeField] private float linearDrag;
    private bool changingDirection => (rb.velocity.x > 0f && moveVector < 0f) || (rb.velocity.x < 0f && moveVector > 0f);

    [Space(10)]
    [Header("Jump")]
    public float jumpDistance;
    [SerializeField] private float airLinearDrag = 2.5f;
    /*public float jumpHeight;
    public float jumpTimeToAPex;
    public float jumpForce;*/

    [Header("Coyote Time")]
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    [Header("Jump Buffer")]
    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    [Header("Ground Check")]
    [SerializeField] bool isGrounded;
    public Transform groundSpot;
    public LayerMask groundLayer;
    [SerializeField] bool doubleJump;

    [Header("Dash Variables")]
    [SerializeField] bool canDash = true;
    [SerializeField] bool isDashing;
    [SerializeField] float dashPower;
    [SerializeField] float dashTime;
    [SerializeField] float dashCooldown;
    private TrailRenderer dashTrail;
    [SerializeField] float dashGravity;
    private float normalGravity;
    private float waitTime;

    [Header("Sliding")]
    [SerializeField] bool isWallSliding;
    [SerializeField] float wallSlidingSpeed = 2f;

    [Header("Wall Jumping Variables")]
    [SerializeField] bool isWallJumping;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDirection;
    private float wallJumpingDuration = 0.5f;
    public Vector2 wallJumpForce = new Vector2(8f, 10f);
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;

    [Header("Gravity")]
    public float gravityStrength;
    public float gravityScale;
    [Space(5)]
    public float fallGravityMult;
    public float maxFallSpeed;
    [Space(5)]
    public float fastFallGravityMult;
    public float maxFastFallSpeed;




    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTrail = GetComponent<TrailRenderer>();
        normalGravity = rb.gravityScale;
        isFacingRight = true;
        canDash = true;
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        if(isGrounded && !context.performed)
        {
            doubleJump = false;
        }
        if (context.performed)
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            if (isGrounded || doubleJump) {
                rb.velocity = new Vector2(rb.velocity.x, jumpDistance);
                //rb.velocity = new Vector2(rb.velocity.x, 0f);
                /*rb.AddForce(Vector2.up * jumpDistance, ForceMode2D.Impulse);
                animator.SetBool("IsJumping", true);
                doubleJump = !doubleJump;
                jumpBufferCounter = 0f;
                print(rb.velocity.y);
                if (context.canceled && rb.velocity.y > 0f)
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                    Debug.Log("Falling faster");
                    //animator.SetBool("IsJumping", true);
                    coyoteTimeCounter = 0f;
                }*/
            }
        }
        
        if (context.performed && isGrounded)
        {
            //rb.velocity = new Vector2(rb.velocity.x, jumpDistance);
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * jumpDistance, ForceMode2D.Impulse);
            animator.SetBool("IsJumping", true);
        }
        else if(context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if(context.performed && canDash)
        {
            if (waitTime >= dashCooldown)
             {
                 waitTime = 0;
                 Invoke("Dash", 0);
             }
            
        }
    }

    private void Update()
    {
        waitTime += Time.deltaTime;
        if (isDashing)
        {
            return;
        }
        isGrounded = Physics2D.OverlapCircle(groundSpot.position, 0.2f, groundLayer);
        WallSlide();
        if (!isWallJumping)
        {
            CheckDirection();
        }
        animator.SetFloat("Speed",Mathf.Abs(moveVector));

        if (isGrounded)
        {
            animator.SetBool("IsJumping", false);
        }
        else
        {
            animator.SetBool("IsJumping", true);
        }
        resetDash();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        if (!isWallJumping)
        {
            //rb.velocity = new Vector2(moveVector * moveSpeed, rb.velocity.y);
            rb.AddForce(new Vector2(moveVector, 0f) * movementAcceleration);
            if (Mathf.Abs(rb.velocity.x) > maxMoveSpeed)
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxMoveSpeed, rb.velocity.y);
            
        }
        if (isGrounded)
        {
            ApplyLinearDrag();
        }
        else
        {
            ApplyAirLinearDrag();
        }
        
        
    }

    private void ApplyLinearDrag()
    {
        if (Mathf.Abs(moveVector) < 0.4f|| changingDirection)
        {
            rb.drag = linearDrag;
        }
        else
        {
            rb.drag = 0f;
        }
    }

    private void ApplyAirLinearDrag()
    {
        rb.drag = airLinearDrag;
        
    }

    void CheckDirection()
    {
        if (moveVector > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveVector < 0 && isFacingRight)
        {
            Flip();
        }
    }
    void Flip()
    {
        isFacingRight = !isFacingRight;
        /* Vector3 localScale = transform.localScale;
         localScale.x *= -1f;
         transform.localScale = localScale;*/
        transform.Rotate(0f, 180f,0f);

    }
    
    public void Dash()
    {
        canDash = false;
        isDashing = true;
        dashTrail.emitting = true;
        rb.gravityScale = dashGravity;

        if(moveVector == 0)
        {
             if (isFacingRight)
             {
                 rb.velocity = new Vector2(transform.localScale.x * dashPower, 0);
             }
             if(!isFacingRight)
             {
                 rb.velocity = new Vector2(-transform.localScale.x * dashPower, 0);
             }
            //rb.velocity = new Vector2(transform.localScale.x * dashPower, 0);
        }
        else
        {
            rb.velocity = new Vector2(moveVector * dashPower, 0);
        }
        Invoke("StopDash", dashTime);
    }
    public void StopDash()
    {
        isDashing = false;
        dashTrail.emitting = false;
        rb.gravityScale = normalGravity;
    }

    private void WallSlide()
    {
        if(OnWall() && !isGrounded && moveVector != 0f)
        {
            //print("Walled");
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }
    public void WallJump(InputAction.CallbackContext context)
    {
        int x = 1;
        if (isFacingRight)
        {
            x = 1;
        }
        else if (!isFacingRight)
        {
            x = -1;
        }
        if (isWallSliding)
        {
            isWallJumping = false;
            //wallJumpingDirection = -transform.localScale.x;
            if (isFacingRight)
            {
                wallJumpingDirection = -1;
            }else if (!isFacingRight)
            {
                wallJumpingDirection = 1;
            }
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (context.performed && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            print(isWallJumping);
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpForce.x, wallJumpForce.y);
            wallJumpingCounter = 0f;
            if (x != wallJumpingDirection)
            {
                Flip();
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }

    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private bool OnWall()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }
    private void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }
    private void resetDash()
    {
        if (isGrounded)
            canDash = true;
    }
}

