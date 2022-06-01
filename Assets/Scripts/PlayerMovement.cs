using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    Rigidbody2D _rb;
    Animator animator;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask groundLM;

    [Header("Movement")]
    [SerializeField] private float movAcce;
    [SerializeField] private float maxMovSpeed;
    [SerializeField] private float groundDrag;
    [SerializeField] private bool facingRight = true;
    private float horDir;
    private float verDir;
    private bool changeDir => (_rb.velocity.x > 0f && horDir < 0f) || (_rb.velocity.x < 0f && horDir > 0f);
    private bool wallGrab => onWall && !isGrounded && Input.GetButton("WallGrab");
    private bool wallSlide => onWall && !isGrounded && _rb.velocity.y < 0f;
    private bool canMove => !wallGrab;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 12.0f;
    [SerializeField] private float airDrag = 2.5f;
    [SerializeField] private float fallSpeed = 8f;
    [SerializeField] private float lowFallSpeed = 5f;
    [SerializeField] private int nExtraJumps = 1;
    [SerializeField] private float hangTime= 0.1f;
    [SerializeField] private float jumpBufferLen= 0.1f;
    private int extraJumpsValue;
    private float hangTimeCounter;
    private float jumpBufferCounter;
    private bool canJump => jumpBufferCounter > 0.0f && (hangTimeCounter > 0f || extraJumpsValue > 0);

    [Header("Ground Collision")]
    [SerializeField] private float groundRaycastLength;
    [SerializeField] private Vector3 groundRaycastOffset;
    private bool isGrounded;

    [Header("Wall Jump")]
    [SerializeField] private float wallRayCastLenght;
    [SerializeField] private float wallSlideMod;
    public bool onWall;
    public bool onRightWall;

    [Header ("Corner Correction")]
    [SerializeField] private float topRayCastLen;
    [SerializeField] private Vector3 edgeRayOffset;
    [SerializeField] private Vector3 innerRayOffset;
    private bool canCorner;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        horDir = GetInput().x;
        verDir = GetInput().y;
        animator.SetFloat("Speed", Mathf.Abs(horDir));
        if (Input.GetButtonDown("Jump")) jumpBufferCounter = jumpBufferLen;
        else jumpBufferCounter -= Time.deltaTime;
        if (canJump) Jump();
    }
    private void FixedUpdate()
    {
        CheckCollisions();
        if (canMove) MoveCharacter();
        if (isGrounded)
        {
            animator.SetBool("IsJumping", false);
            extraJumpsValue = nExtraJumps; 
            hangTimeCounter = hangTime;
            ApplyGroundLinearDrag();
        }
        else
        {
            animator.SetBool("IsJumping", true);
            ApplyAirLinearDrag();
            FallMultiplier();
            hangTimeCounter -= Time.fixedDeltaTime;
        }
        if (canCorner) CornerCorrect(_rb.velocity.y);
        if (wallGrab) WallGrab();
        if (wallSlide) WallSlide();
    }
    private static Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
    private void MoveCharacter()
    {
        if (horDir > 0 && !facingRight)
        {
            Flip();
        }
        else if (horDir < 0 && facingRight)
        {
            Flip();
        }
        _rb.AddForce(new Vector2(horDir, 0f) * movAcce);
        if (Mathf.Abs(_rb.velocity.x) > maxMovSpeed)
        {
            _rb.velocity = new Vector2(Mathf.Sign(_rb.velocity.x) * maxMovSpeed, _rb.velocity.y);
        }
    }
    private void WallGrab()
    {
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
    private void Jump()
    {
        animator.SetBool("IsJumping", true);
        if (!isGrounded){
            extraJumpsValue--;
            _rb.AddForce(Vector2.up * jumpForce*0.8f, ForceMode2D.Impulse);
        }
        else
        {
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        //_rb.velocity = new Vector2(_rb.velocity.x, 0);
        
        hangTimeCounter = 0f;
        jumpBufferCounter = 0f;
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

        onWall = Physics2D.Raycast(transform.position, Vector2.right, wallRayCastLenght, groundLM) || 
                 Physics2D.Raycast(transform.position, Vector2.left, wallRayCastLenght, groundLM);

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

        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * wallRayCastLenght);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * wallRayCastLenght);


    }
}
