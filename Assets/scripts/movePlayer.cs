using System;
using UnityEngine;

public class movePlayer : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public bool isJumping;
    public bool isGrounded;
    bool alignPosition = true;

    public SpriteRenderer sp;
    public Transform groundCheckLeft;
    public Transform groundCheckRight;
    
    public Rigidbody2D rb;
    
    private Vector3 velocity = Vector3.zero;
    
    void Start()
    {
        
    }

    private void Update()
    {
        MoveDirection(rb.velocity.x);
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
        
        float horizontalMove = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;


        sp.flipX = alignPosition;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        }
        
        playerMove(horizontalMove);
    }

    void playerMove(float _horizontaleMove)
    {
        Vector3 targetVelocity = new Vector2(_horizontaleMove, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

        if (isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
    }

    void MoveDirection(float num)
    {
        if (num < 0f)
        {
            alignPosition = true;
        }
        else
        {
            alignPosition = false;
        }
    }
}
