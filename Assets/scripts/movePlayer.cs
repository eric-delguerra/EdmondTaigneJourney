using System;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public bool isJumping;
    public bool isGrounded;
    private bool _alignPosition = true;
    private float horizontalMove;
    public float gravityScale;
    private bool _canPlane;

    public SpriteRenderer sp;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayer;
    
    public Rigidbody2D rb;
    
    private Vector3 _velocity = Vector3.zero;

    private void Update()
    {   
        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
                isJumping = true;
                _canPlane = true;
        }
        MoveDirection(rb.velocity.x);
        sp.flipX = _alignPosition;
        
        
        if (_canPlane && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("plane");
            Plane();
        }
        else if (_canPlane && Input.GetKeyUp(KeyCode.Space))
        {
            rb.gravityScale = 1f;
        }
    }

    // Plus pour la gestion de la physique et pas d'entrée d'input
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayer);
        horizontalMove = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        PlayerMove(horizontalMove);

      
    }

    void PlayerMove(float _horizontaleMove)
    {
        Vector3 targetVelocity = new Vector2(_horizontaleMove, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref _velocity, .05f);

        if (isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
    }

    void Plane()
    {
        rb.gravityScale = gravityScale;
    }
    
    void MoveDirection(float _num)
    {
        if (_num > 0.1f)
        {
            _alignPosition = false;
        }
        else if (_num < -0.1f)
        {
            _alignPosition = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
