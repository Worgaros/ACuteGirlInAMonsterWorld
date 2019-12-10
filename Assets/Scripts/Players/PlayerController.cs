using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour {
    Rigidbody2D body;
    Animator animator;
    SpriteRenderer spriteRenderer;
    
    
    Vector2 direction;
    
    [SerializeField] float speed;
    
    [SerializeField] float jumpForce;
    [SerializeField] float raycastJumpLength;
    [SerializeField] float timeStopJump;
    float timerStopJump = 0f;
    [SerializeField] float jumpFallingModifier;
    bool canJump = false;
    bool isJumpFallingModifier = false;
    [SerializeField] float lookingAxis;
    bool isLookingRight = true;
    RaycastHit2D leftLeg;
    RaycastHit2D rightLeg;
    
    [SerializeField] int collectedPages;
    [SerializeField] GameObject magicSpellBook;

    [SerializeField] int life;
    
    [SerializeField] TextMeshProUGUI playerLifeUI;
    [SerializeField] TextMeshProUGUI collectedPagesUI;

    AudioSource audioSource;

    const float securityAxisNumber = 0.1f;
    const int maxPagesNumber = 8;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        direction = new Vector2( Input.GetAxis("Horizontal") * speed, body.velocity.y);
        timerStopJump -= Time.deltaTime;
        Vector2 hitPos = new Vector2(transform.position.x - 0.2f, transform.position.y);
        leftLeg = Physics2D.Raycast(hitPos, Vector2.down, raycastJumpLength, 1 << LayerMask.NameToLayer("Platforms"));
        Vector2 hit2Pos = new Vector2(transform.position.x + 0.2f, transform.position.y);
        rightLeg = Physics2D.Raycast(hit2Pos, Vector2.down, raycastJumpLength, 1 << LayerMask.NameToLayer("Platforms"));

        if (timerStopJump <= 0) 
        {
            
            if (leftLeg.rigidbody != null || rightLeg.rigidbody != null)
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
        
        if (body.velocity.y < securityAxisNumber && isJumpFallingModifier)
        {
            direction = new Vector2(body.velocity.x, body.velocity.y * jumpFallingModifier);
        }
        
        body.velocity = direction;

        AnimatorUpdate();
        ActiveMagicBook(); 
    }

    void AnimatorUpdate()
    {
        lookingAxis = Input.GetAxis("Horizontal");
        animator.SetBool("isGrounded", false);
        animator.SetFloat("speed", Mathf.Abs(body.velocity.x));
        
        if (lookingAxis < -securityAxisNumber && isLookingRight)
        {
            spriteRenderer.flipX = true;
            isLookingRight = false;
        }
        else if (lookingAxis > securityAxisNumber && !isLookingRight)
        {
            spriteRenderer.flipX = false;
            isLookingRight = true;
        }

        if (Input.GetButtonDown("Jump"))
        {
            animator.SetBool("isJumping", true);
        }

        if (body.velocity.y < securityAxisNumber)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", true);
        }
        
        if (leftLeg.rigidbody != null || rightLeg.rigidbody != null)
        {
            animator.SetBool("isFalling", false);
            animator.SetBool("isGrounded", true);
        }
    }

    public void AddBookPage(int value)
    {
        audioSource.Play();
        collectedPages += value;
        collectedPagesUI.text = collectedPages.ToString();
    }

    public void AddSpeed(int value)
    {
        speed += value;
    }

    void ActiveMagicBook()
    {
        if (magicSpellBook != null)
        {
            if (collectedPages == maxPagesNumber && magicSpellBook.active == false)
            {
                magicSpellBook.SetActive(true);
            }
        }
    }

    public void TakeDamages(int damages)
    {
        life -= damages;
        playerLifeUI.text = life.ToString();
    }

    public void CheckDeath()
    {
        if (life <= 0)
        {
            SceneManager.LoadScene("DefeatScene");
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