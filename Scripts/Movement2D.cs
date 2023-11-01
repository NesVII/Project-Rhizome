using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement2D : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;
    public Animator animator;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [Header("Movement Variables")]
    [SerializeField] private bool isFacingRight;
    [Space(10)]
    [SerializeField] private float moveAccel;
    [SerializeField] private float maxMovSpd;
    [SerializeField] private float linearDrag;
    private float horizontalDirection;
    private bool changingDirection => (rb.velocity.x > 0f && horizontalDirection < 0f) || (rb.velocity.x < 0f && horizontalDirection > 0f);

    [Header("Jump Variables")]
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float airJumpForce = 12f;
    [SerializeField] private float airLinearDrag = 2.5f;
    [SerializeField] private float fallMultiplier = 8f;
    [SerializeField] private float lowJumpFallMultiplier = 5f;
    [SerializeField] private bool _goingDown = false;
    [SerializeField] private int _extraJumps = 1;
    private int extraJumpsValue;

    [Header("Anchor Stuff")]
    public Animator SquachAnimator;
    private bool CheckSquash = true;

    [Header("Wall Jumping Variables")]
    [SerializeField] bool isWallJumping;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDirection;
    private float wallJumpingDuration = 0.5f;
    public Vector2 wallJumpForce = new Vector2(8f, 10f);

    [Header("Dash Variables")]

    [SerializeField] bool canDash = true;
    [SerializeField] bool isDashing;
    [SerializeField] float dashPower;
    public float dashingCooldown = 1f;
    public float dashTime = 0.2f;
    private TrailRenderer dashTrail;
    [SerializeField] float dashGravity;
    private float normalGravity;
    private float waitTime;

    public Vector2 savedVelocity;

    [Header("Sliding")]
    [SerializeField] bool isWallSliding;
    [SerializeField] float wallSlidingSpeed = 2f;
    public Transform wallSpot;

    [Header("Ground Collision Variables")]
    public Transform groundSpot;
    public bool _onGround;

    [Header("Knock Back")]
    public float KBForce;
    public float KBCounter;
    public float KbTotalTime;
    public bool KnockFromRight;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTrail = GetComponent<TrailRenderer>();
        normalGravity = rb.gravityScale;

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontalDirection));
        if (!isWallJumping)
            CheckDirection();
        if (isDashing) { 
            return;
        }
        resetDash();
        
        if (_onGround)
        {
            isWallSliding = false;
            animator.SetBool("IsJumping", false);
            if (CheckSquash)
            {
                SquachAnimator.SetTrigger("TouchGround");
            }
        }
        else
        {
            animator.SetBool("IsJumping", true);
            WallSlide();
            SquachAnimator.SetTrigger("IsFalling");
            Invoke("BS", 0.2f);
        }

        

    }
    private void FixedUpdate()
    {
        if (isDashing)
            return;
        CheckCollision();
        if(!isWallJumping)
            MoveCharacter();
        if (_onGround)
        {
            extraJumpsValue = _extraJumps;
            ApplyGroundLinearDrag();
        }
        else
        {
            ApplyAirLinearDrag();
            FallMultiplier();
        }
        
    }

    public void GetInputMove(InputAction.CallbackContext context)
    {
        horizontalDirection = context.ReadValue<Vector2>().x;
    }

    public void MoveCharacter()
    {
        
        if (KBCounter<=0)
        {
        rb.AddForce(new Vector2(horizontalDirection, 0f) * moveAccel);
            if (Mathf.Abs(rb.velocity.x) > maxMovSpd)
            {
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxMovSpd, rb.velocity.y);
            }
        }
        else
        {
            if (KnockFromRight)
            {
                rb.velocity= new Vector2(-KBForce, 0f);
            }
            else if (!KnockFromRight)
            {
                rb.velocity = new Vector2(KBForce, 0f);
            }

            KBCounter -= Time.deltaTime;
        }
    }
    
    public void GetInputJump(InputAction.CallbackContext context)
    {
        
        if (context.performed && (_onGround || extraJumpsValue>0) && !isWallSliding)
        {
            Jump();
            _goingDown = false;
        }
        if (context.canceled)
        {
            _goingDown = true;
        }
    }

    public void GetInputDash(InputAction.CallbackContext context)
    {
        if(context.performed && canDash)
        {
            Invoke("Dash", 0);
        }
    }

    public void Jump()
    {
        CheckSquash = false;
        //jumpSE.Play();
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        SquachAnimator.SetTrigger("Jump");
        Invoke("BS", 0.5f);
        if (!_onGround)
        {
            animator.SetTrigger("doubleJump");
            extraJumpsValue--;
            rb.AddForce(Vector2.up * airJumpForce, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        
    }

    private void BS()
    {
        CheckSquash = true;
    }

    private void FallMultiplier()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = fallMultiplier;
        }
        else if (rb.velocity.y > 0 && _goingDown)
        {
            rb.gravityScale = lowJumpFallMultiplier;
        }
        else
        {
            rb.gravityScale = 1f;
        }
    }

    private void CheckCollision()
    {
        _onGround = Physics2D.OverlapCircle(groundSpot.position, 0.2f, groundLayer);
    }

    private void CheckDirection()
    {
        
        if (horizontalDirection>0 && !isFacingRight)
        {
            Flip();
        }
        else if(horizontalDirection<0 && isFacingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
        
    }

    public void Dash()
    {
        canDash = false;
        isDashing = true;
        dashTrail.emitting = true;
        rb.gravityScale = dashGravity;
        rb.velocity = new Vector2(horizontalDirection * dashPower, 0);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemies"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Fire"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Shockwave"), true);
        animator.SetBool("Dashing", true);
        Invoke("StopDash", dashTime);
        //Invoke("resetDash", dashingCooldown);
    }
    public void StopDash()
    {
        animator.SetBool("Dashing", false);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemies"), false);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Fire"), false);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Shockwave"), false);
        isDashing = false;
        dashTrail.emitting = false;
        rb.gravityScale = normalGravity;
    }

    private void resetDash()
    {

        waitTime += Time.deltaTime;

        if (_onGround && waitTime >= dashingCooldown)
        {
            canDash = true;
            waitTime = 0;
        }
        else if (!_onGround)
        {

            //canDash = false;
        }
    }

    private void ApplyGroundLinearDrag()
    {
        if (Mathf.Abs(horizontalDirection) < 0.4f || changingDirection)
        {
            rb.drag = linearDrag;
        }
        else
            rb.drag = 0;
    }

    private void ApplyAirLinearDrag()
    {
        rb.drag = airLinearDrag;
        
    }

    private void WallSlide()
    {
        if(OnWall() && !_onGround && horizontalDirection != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private bool OnWall()
    {
        return Physics2D.OverlapCircle(wallSpot.position, 0.2f, wallLayer);
    }

    public void WallJump(InputAction.CallbackContext context)
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            if (isFacingRight)
            {
                wallJumpingDirection = -1;
            }
            else
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

        if(context.performed && wallJumpingCounter > 0f && isWallSliding)
        {
            isWallJumping = true;
            //rb.velocity = new Vector2(rb.velocity.x, 0f);
            //rb.AddForce(Vector2.up * wallJumpForce, ForceMode2D.Impulse);
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpForce.x, wallJumpForce.y);
            wallJumpingCounter = 0f;
            if((isFacingRight && wallJumpingDirection == -1) || (!isFacingRight && wallJumpingDirection == 1))
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
}
