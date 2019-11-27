using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {
    Rigidbody2D body_;
    Animator animator_;
    SpriteRenderer spriteRenderer_;
    
    
    Vector2 direction;
    
    [SerializeField] float speed;
    
    [SerializeField] float jumpForce;
    [SerializeField] float raycastJumpLength;
    [SerializeField] float timeStopJump;
    float timerStopJump = 0f;
    [SerializeField] float jumpFallingModifier;
    bool canJump = false;
    bool isJumpFallingModifier = false;
    [SerializeField] float lookingAxis_;
    bool isLookingRight_ = true;
    RaycastHit2D hit;
    RaycastHit2D hit2;

    void Start()
    {
        body_ = GetComponent<Rigidbody2D>();
        animator_ = GetComponent<Animator>();
        spriteRenderer_ = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        direction = new Vector2( Input.GetAxis("Horizontal") * speed, body_.velocity.y);
        timerStopJump -= Time.deltaTime;
        Vector2 hitPos = new Vector2(transform.position.x - 0.2f, transform.position.y);
        hit = Physics2D.Raycast(hitPos, Vector2.down, raycastJumpLength, 1 << LayerMask.NameToLayer("Platforms"));
        Vector2 hit2Pos = new Vector2(transform.position.x + 0.2f, transform.position.y);
        hit2 = Physics2D.Raycast(hit2Pos, Vector2.down, raycastJumpLength, 1 << LayerMask.NameToLayer("Platforms"));

        if (timerStopJump <= 0) 
        {
            
            if (hit.rigidbody != null || hit2.rigidbody != null)
            {
                canJump = true;
                isJumpFallingModifier = false;
            }
            else
            {
                canJump = false;
                isJumpFallingModifier = true;
            }
        }

        if (Input.GetButtonDown("Jump") && canJump)
        {
            direction = new Vector2(body_.velocity.x, jumpForce); 
            canJump = false; 
            timerStopJump = timeStopJump;
        }
        
        if (body_.velocity.y < 0.1 && isJumpFallingModifier)
        {
            direction = new Vector2(body_.velocity.x, body_.velocity.y * jumpFallingModifier);
        }
        
        body_.velocity = direction;

        AnimatorUpdate();
    }

    void AnimatorUpdate()
    {
        lookingAxis_ = Input.GetAxis("Horizontal");
        animator_.SetBool("isGrounded", false);
        animator_.SetFloat("speed", Mathf.Abs(body_.velocity.x));
        
        if (lookingAxis_ < -0.1f && isLookingRight_)
        {
            spriteRenderer_.flipX = true;
            isLookingRight_ = false;
        }
        else if (lookingAxis_ > 0.1f && !isLookingRight_)
        {
            spriteRenderer_.flipX = false;
            isLookingRight_ = true;
        }

        if (Input.GetButtonDown("Jump"))
        {
            animator_.SetBool("isJumping", true);
        }

        if (body_.velocity.y < 0.1f)
        {
            animator_.SetBool("isJumping", false);
            animator_.SetBool("isFalling", true);
        }
        
        if (hit.rigidbody != null || hit2.rigidbody != null)
        {
            animator_.SetBool("isFalling", false);
            animator_.SetBool("isGrounded", true);
        }
        
    }
    
    void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Vector2 hitPos = new Vector2(transform.position.x - 0.2f, transform.position.y);
        Gizmos.DrawLine((Vector2)hitPos, (Vector2)hitPos + Vector2.down * raycastJumpLength);
        Vector2 hit2Pos = new Vector2(transform.position.x + 0.2f, transform.position.y);
        Gizmos.DrawLine((Vector2)hit2Pos, (Vector2)hit2Pos + Vector2.down * raycastJumpLength);
    }
}