using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D body_;
    Animator animator_;
    bool isLookingRight_ = true;
    SpriteRenderer spriteRenderer_;
    
    void Start()
    {
        animator_ = GetComponent<Animator>();
        spriteRenderer_ = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    { 
        animator_.SetFloat("speed", Mathf.Abs(body_.velocity.x));

        if (body_.velocity.x < 0.1f && isLookingRight_)
        {
            spriteRenderer_.flipX = true;
            isLookingRight_ = false;
        }
        else if (body_.velocity.x > 0.1f && !isLookingRight_)
        {
            spriteRenderer_.flipX = false;
            isLookingRight_ = true;
        }

        animator_.SetBool("isJumping", false);
    }
}
