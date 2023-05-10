using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    Rigidbody2D _rb;
    Animator animator;
    [SerializeField] IOController iocontroller;
    [SerializeField] Inventory inventory;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask groundLM;

    [Header("Habilidades desbloqueadas")]
    [SerializeField] private bool unlockedDash;
    [SerializeField] private bool unlockedWallJump;
    [SerializeField] private bool unlockedWallGrab;
    [SerializeField] private bool unlockedClimbing;
    [SerializeField] private bool unlockedBreakItems;

    [Header("Movement")]
    [SerializeField] private float movAcce;
    [SerializeField] private float maxMovSpeed;
    [SerializeField] private float groundDrag;
    [SerializeField] public bool facingRight = true;
    private float horDir;
    private float verDir;
    private bool changeDir => (_rb.velocity.x > 0f && horDir < 0f) || (_rb.velocity.x < 0f && horDir > 0f);
    private bool wallGrab => onWall && !isGrounded && Input.GetButton("WallGrab");
    private bool wallSlide => onWall && !isGrounded && _rb.velocity.y < 0f;
    public bool canMove => !wallGrab;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 12.0f;
    [SerializeField] private float airDrag = 2.5f;
    [SerializeField] private float fallSpeed = 8f;
    [SerializeField] private float lowFallSpeed = 5f;
    [SerializeField] private int nExtraJumps = 1;
    [SerializeField] private float hangTime= 0.1f;
    [SerializeField] private float jumpBufferLen= 0.1f;
    private bool isJumping;
    private bool hasLanded;
    private int extraJumpsValue;
    private float hangTimeCounter;
    private float jumpBufferCounter;
    private bool canJump => jumpBufferCounter > 0.0f && (hangTimeCounter > 0f || extraJumpsValue > 0 || onWall);

    [Header("Ground Collision")]
    [SerializeField] private float groundRaycastLength;
    [SerializeField] private Vector3 groundRaycastOffset;
    private bool isGrounded;

    [Header("Wall Jump")]
    [SerializeField] private float wallRayCastLenght; 
    [SerializeField] private Vector3 wallRaycastOffset;
    [SerializeField] private float wallSlideMod;
    [SerializeField] private float wallJumpXVel = 0.2f;
    public bool onWall;
    public bool onRightWall;

    [Header("Dash")]
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private float dashLength = .3f;
    [SerializeField] private float dashBufferLength = 0.1f;
    private float dashBufferCounter;
    private bool hasDashed = false;
    private bool isDashing;
    private bool canDash => dashBufferCounter > 0.0f && !hasDashed && unlockedDash;

    

    [Header("Climbing")]
    [SerializeField] private float wallClimbingSpeed = 1.0f;
    private bool canClimb = false;
    public bool isClimbing;

    [Header ("Corner Correction")]
    [SerializeField] private float topRayCastLen;
    [SerializeField] private Vector3 edgeRayOffset;
    [SerializeField] private Vector3 innerRayOffset;
    private bool canCorner;

    public bool UnlockedBreakItems { get => unlockedBreakItems; set => unlockedBreakItems = value; }
    public bool UnlockedClimbing { get => unlockedClimbing; set => unlockedClimbing = value; }
    public bool UnlockedWallGrab { get => unlockedWallGrab; set => unlockedWallGrab = value; }
    public bool UnlockedDash { get => unlockedDash; set => unlockedDash = value; }
    public bool UnlockedWallJump { get => unlockedWallJump; set => unlockedWallJump = value; }
    public int NExtraJumps { get => nExtraJumps; set => nExtraJumps = value; }


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        foreach (string skill in inventory.skillList) { UnlockSkill(skill, true); }
    }
    
    void Update()
    {
        horDir = GetInput().x;
        verDir = GetInput().y;
        Animate();
        
        //Jump
        if (Input.GetButtonDown("Jump")) jumpBufferCounter = jumpBufferLen;
        else jumpBufferCounter -= Time.deltaTime;
        
        //Dash
        if (Input.GetButtonDown("Dash")) dashBufferCounter = dashBufferLength;
        else dashBufferCounter -= Time.deltaTime;

    }
    
    private void FixedUpdate()
    {
        CheckCollisions();
        if (canDash && unlockedDash) StartCoroutine(Dash(horDir, verDir));
        if (!isDashing)
        {
            if (canMove) MoveCharacter();
            if (canClimb && verDir != 0 && unlockedClimbing) Climb();
            else _rb.velocity = Vector2.Lerp(_rb.velocity, (new Vector2(horDir * maxMovSpeed, _rb.velocity.y)), 0.5f * Time.fixedDeltaTime);
            if (isGrounded)
            {
                animator.SetBool("IsJumping", false);
                if (hasLanded == true)
                {
                    animator.SetBool("Landed", true);
                    hasLanded= false;
                }
                extraJumpsValue = nExtraJumps;
                hangTimeCounter = hangTime;
                ApplyGroundLinearDrag();
                hasDashed = false;
                isClimbing = false;
            }
            else
            {
                ApplyAirLinearDrag();
                FallMultiplier();
                hangTimeCounter -= Time.fixedDeltaTime;
                if (!onWall || _rb.velocity.y < 0f) isJumping = false;
            }
            if (canJump)
            {
                if (onWall && !isGrounded && unlockedWallJump)
                {
                    WallJump();
                    Flip();
                }
                else if(isGrounded || !onWall)
                {
                    Jump(Vector2.up);
                }

            }
            if (!isJumping)
            {
                if (wallGrab && unlockedWallGrab) WallGrab();
                if (wallSlide) WallSlide();
            }

        }
        if (canCorner) CornerCorrect(_rb.velocity.y);

    }
    private static Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
    private void Animate()
    {
        animator.SetFloat("Speed", Mathf.Abs(horDir));
        if (horDir > 0 && !facingRight) Flip();
        else if (horDir < 0 && facingRight) Flip();

        if (isDashing)
        {
            animator.SetBool("IsDashing", true);
            animator.SetBool("IsJumping", false);
            //animator.SetBool("IsClimbing", false);
        }
        if (isJumping)
        {
            animator.SetBool("IsJumping", true);
            //animator.SetBool("IsClimbing", false);
            animator.SetBool("IsDashing", false);
        }
        if (isClimbing)
        {
            animator.SetBool("IsJumping", false);
            //animator.SetBool("IsClimbing", true);
            animator.SetBool("IsDashing", false);
        }
        //if(!isClimbing) animator.SetBool("IsClimbing", false);


    }
    IEnumerator Dash(float x, float y)
    {
        float dashStart = Time.time;
        hasDashed = true;
        isDashing = true;
        isJumping = false;

        _rb.velocity = Vector2.zero;
        _rb.gravityScale = 0f;
        _rb.drag = 0f;

        Vector2 dir;
        if (x != 0f || y != 0f) dir = new Vector2(x, y);
        else
        {
            if (facingRight) dir = new Vector2(1, 0);
            else dir = new Vector2(-1,0);
        }
        while (Time.time < dashStart + dashLength)
        {
            _rb.velocity = dir.normalized * dashSpeed;
            yield return null;
        }
        animator.SetBool("IsDashing", false);
        isDashing = false;
    }
    private void MoveCharacter()
    {
        _rb.AddForce(new Vector2(horDir, 0f) * movAcce);
        if (Mathf.Abs(_rb.velocity.x) > maxMovSpeed)
        {
            _rb.velocity = new Vector2(Mathf.Sign(_rb.velocity.x) * maxMovSpeed, _rb.velocity.y);
        }
    }
    private void Climb()
    {
        isClimbing = true;
        _rb.velocity = new Vector2(horDir, maxMovSpeed * wallClimbingSpeed * verDir);
    }
    private void WallGrab()
    {
        hasDashed = false;
        _rb.gravityScale = 0;
        _rb.velocity = new Vector2(_rb.velocity.x, 0);
    }
    private void WallSlide()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, -maxMovSpeed * wallSlideMod);
    }
    private void ApplyGroundLinearDrag()
    {
        if (Mathf.Abs(horDir) < 0.4f || changeDir)
        {
            _rb.drag = groundDrag;
        }
        else
        {
            _rb.drag = 0.0f;
        }
    }
    private void ApplyAirLinearDrag()
    {
        _rb.drag = airDrag;
    }
    private void Jump(Vector2 direc)
    {
        animator.SetBool("IsJumping", true);
        if (direc == Vector2.up)
        {
            if (!isGrounded && !onWall)
            {
                extraJumpsValue--;
                _rb.velocity = new Vector2(_rb.velocity.x, 0);
                _rb.AddForce(direc * jumpForce * 0.8f, ForceMode2D.Impulse);
            }
            else
            {
                _rb.velocity = new Vector2(_rb.velocity.x, 0);
                _rb.AddForce(direc * jumpForce, ForceMode2D.Impulse);
            }
        }
        else
        {
            if (!isGrounded && !onWall)
            {
                extraJumpsValue--;
                _rb.velocity = new Vector2(_rb.velocity.x, 0);
                _rb.AddForce(direc * jumpForce * 0.8f, ForceMode2D.Impulse);
            }
            else
            {
                _rb.velocity = new Vector2(_rb.velocity.x, 0);
                _rb.AddForce(direc * jumpForce * 1.5f, ForceMode2D.Impulse);
            }
        }
        hangTimeCounter = 0f;
        jumpBufferCounter = 0f;
        isJumping = true;
    }
    private void WallJump()
    {
        Vector2 jumpDir = onRightWall ? Vector2.left : Vector2.right;
        Jump(Vector2.up/2 + jumpDir);
    }
    
    private void FallMultiplier()
    {
        if (_rb.velocity.y < 0)  _rb.gravityScale = fallSpeed;
        
        else if (_rb.velocity.y > 0 && !Input.GetButton("Jump"))  _rb.gravityScale = lowFallSpeed;
        
        else  _rb.gravityScale = 1;
        
    }
    private void CornerCorrect(float velY)
    {
        RaycastHit2D _hit = Physics2D.Raycast(transform.position - innerRayOffset + Vector3.up * topRayCastLen, Vector3.left, topRayCastLen, groundLM);
        if (_hit.collider != null)
        {
            float _newPos = Vector3.Distance(new Vector3(_hit.point.x, transform.position.y, 0f) + Vector3.up * topRayCastLen,
                transform.position - edgeRayOffset + Vector3.up * topRayCastLen);
            transform.position = new Vector3(transform.position.x + _newPos, transform.position.y, transform.position.z);
            _rb.velocity = new Vector2(_rb.velocity.x, velY);
            return;
        }
        _hit = Physics2D.Raycast(transform.position + innerRayOffset + Vector3.up * topRayCastLen, Vector3.right, topRayCastLen, groundLM);
        if (_hit.collider != null)
        {
            float _newPos = Vector3.Distance(new Vector3(_hit.point.x, transform.position.y, 0f) + Vector3.up * topRayCastLen,
                transform.position + edgeRayOffset + Vector3.up * topRayCastLen);
            transform.position = new Vector3(transform.position.x - _newPos, transform.position.y, transform.position.z);
            _rb.velocity = new Vector2(_rb.velocity.x, velY);
        }
    }
    private void CheckCollisions()
    {

        isGrounded = Physics2D.Raycast(transform.position * groundRaycastLength, Vector2.down, groundRaycastLength, groundLM);

        isGrounded = Physics2D.Raycast(transform.position + groundRaycastOffset, Vector2.down, groundRaycastLength, groundLM) ||
                     Physics2D.Raycast(transform.position - groundRaycastOffset, Vector2.down, groundRaycastLength, groundLM);

        onWall = Physics2D.Raycast(transform.position + wallRaycastOffset, Vector2.right, wallRayCastLenght, groundLM) || 
                 Physics2D.Raycast(transform.position + wallRaycastOffset, Vector2.left, wallRayCastLenght, groundLM);

        onRightWall = Physics2D.Raycast(transform.position, Vector2.right, wallRayCastLenght, groundLM);
    }
    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow; 
        Gizmos.DrawLine(transform.position + groundRaycastOffset, transform.position + groundRaycastOffset + Vector3.down * groundRaycastLength);
        Gizmos.DrawLine(transform.position - groundRaycastOffset, transform.position - groundRaycastOffset + Vector3.down * groundRaycastLength);

        Gizmos.DrawLine(transform.position + wallRaycastOffset, transform.position + wallRaycastOffset + Vector3.right * wallRayCastLenght);
        Gizmos.DrawLine(transform.position + wallRaycastOffset, transform.position + wallRaycastOffset + Vector3.left * wallRayCastLenght);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Climb"))
        {
            canClimb = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Climb"))
        {
            canClimb = false;
        }
    }
    public void UnlockSkill(string skill, bool initial)
    {
        if (skill == "Dash") unlockedDash = true;
        if (skill == "WallJump") unlockedWallJump = true;
        if (skill == "WallGrab") unlockedWallGrab = true;
        if (skill == "Climbing") UnlockedClimbing = true;
        if (skill == "BreakItems") UnlockedBreakItems = true;
        if (skill == "ExtraJump") nExtraJumps = 1;
 
    }
    public void WriteSkills()
    {
        inventory.SaveSkills();
    }
}
