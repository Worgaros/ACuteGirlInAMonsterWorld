using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {
    private Rigidbody2D body;
    
    Vector2 direction;
    
    [SerializeField] float speed;
    
    [SerializeField] float jumpForce;
    [SerializeField] float raycastJumpLength;
    [SerializeField] float timeStopJump;
    float timerStopJump = 0f;
    [SerializeField] float jumpFallingModifier;
    bool canJump = false;
    bool isJumpFallingModifier = false;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        direction = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);
        timerStopJump -= Time.deltaTime;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastJumpLength, 1 << LayerMask.NameToLayer("Platforms"));

        if (timerStopJump <= 0) 
        {
            
            if (hit.rigidbody != null)
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
            direction = new Vector2(body.velocity.x, jumpForce); 
            canJump = false; 
            timerStopJump = timeStopJump;
        }
        
        if (body.velocity.y < 0.1 && isJumpFallingModifier)
        {
            direction = new Vector2(body.velocity.x, body.velocity.y * jumpFallingModifier);
        }
        
        body.velocity = direction;
    }
    
    void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine((Vector2)transform.position, (Vector2)transform.position + Vector2.down * raycastJumpLength);
    }
}