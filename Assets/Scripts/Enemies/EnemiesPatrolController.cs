using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemiesPatrolController : MonoBehaviour {
    [SerializeField] Vector3 leftOffset;
    [SerializeField] Vector3 rightOffset;

    [SerializeField] float speed;

    SpriteRenderer spriteRenderer_;
    Animator animator_;

    private bool isLookingRight_ = true;
    
    Vector3 leftTarget;
    Vector3 rightTarget;

    enum State {
        IDLE,
        PATROLLE,
        CHASE_PLAYER
    }

    State state = State.IDLE;

    bool isGoingRight = true;

    Rigidbody2D body;

    Transform targetChase;
    
    void Start() {
        leftTarget = transform.position + leftOffset;
        rightTarget = transform.position + rightOffset;

        isLookingRight_ = isGoingRight;

        body = GetComponent<Rigidbody2D>();
        spriteRenderer_ = GetComponent<SpriteRenderer>();
        animator_ = GetComponent<Animator>();
    }
    
    void Update() {
        switch (state) {
            case State.IDLE:
                state = State.PATROLLE;
                break;
            case State.PATROLLE:
                if (isGoingRight) {
                    Vector3 velocity = (rightTarget - transform.position).normalized * speed;
                    velocity = new Vector3(velocity.x, body.velocity.y, 0);

                    body.velocity = velocity;
                    if (Vector3.Distance(transform.position, rightTarget) < 0.1f) {
                        isGoingRight = false;
                    }
                } else {
                    Vector3 velocity = (leftTarget - transform.position).normalized * speed;
                    velocity = new Vector3(velocity.x, body.velocity.y, 0);

                    body.velocity = velocity;

                    if (Vector3.Distance(transform.position, leftTarget) < 0.1f) {
                        isGoingRight = true;
                    }
                }
                break;
            case State.CHASE_PLAYER: {
                Vector3 velocity = (targetChase.position - transform.position).normalized * speed;
                velocity = new Vector3(velocity.x, body.velocity.y, 0);

                if (transform.position.x + velocity.x * Time.deltaTime >= rightTarget.x || transform.position.x  + velocity.x * Time.deltaTime <= leftTarget.x) {
                    body.velocity = new Vector2(0, 0);
                } else {
                    body.velocity = velocity;
                }
            } 
                break;
        }

        if (body.velocity.x > 0.1 && !isLookingRight_)
        {
            spriteRenderer_.flipX = false;
            isLookingRight_ = true;
        }
        else if (body.velocity.x < -0.1 && isLookingRight_)
        {
            spriteRenderer_.flipX = true;
            isLookingRight_ = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            state = State.CHASE_PLAYER;
            targetChase = other.transform;
        }
    }
    
    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            state = State.PATROLLE;
        }
    }

    void OnDrawGizmos() {
        //Leff point
        if (leftTarget == Vector3.zero) {
            Gizmos.DrawWireCube(transform.position + leftOffset, Vector3.one);
        } else {
            Gizmos.DrawWireCube(leftTarget, Vector3.one);
        }

        //right point
        if (rightTarget == Vector3.zero) {
            Gizmos.DrawWireCube(transform.position + rightOffset, Vector3.one);
        } else {
            Gizmos.DrawWireCube(rightTarget, Vector3.one);
        }
    }
}