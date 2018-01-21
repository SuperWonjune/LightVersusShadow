﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : ObjectsOnGravity {
    public int playerIndex;
    public float speed;
    public float jumpPower;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;

    private float nextFire = 0.0F;

    private bool isJumping = false;
    private Animator animator;

    // 이동 관련
    Vector3 movement;
    float horizontalMove;
    float verticalMove;

    public override void Start () {
        base.Start();
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	public override void Update () {
        base.Update();
        getTouch();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
        CheckOutOfMap();
    }

    
    private void getTouch()
    {
        horizontalMove = 0;
        verticalMove = 0;
        isJumping = false;
        //horizontalMove = Input.GetAxisRaw("Horizontal" + playerIndex);
        //verticalMove = Input.GetAxisRaw("Vertical" + playerIndex);


        // Touch Movement
        // player_index = 1 -> BLACK
        // playeR_index = 2 -> WHITE


        if (Input.touchCount < 1)
        {
            animator.SetBool("isWalk", false);
            return;
        }

       if (isOnGround)
        {
            animator.SetBool("isJump", false);
        } else
        {
            animator.SetBool("isJump", true);
        }

        
        Touch[] myTouches = Input.touches;

        bool blackYUpPressed = false;
        bool blackYDownPressed = false;
        bool whiteYUpPressed = false;
        bool whiteYDownPressed = false;

        for (int i= 0; i< Input.touchCount; i++)
        {
            Touch myTouch = myTouches[i];
            float touchXPos = myTouch.position.x;
            float touchYPos = myTouch.position.y;


            // BLACK STEER
            if (playerIndex == 1)
            {

                // BLACK MOVE
                if (touchXPos < GameSizeDefiner.blackMoveXBoundary)
                {
                    animator.SetBool("isWalk", true);

                    if (touchYPos > GameSizeDefiner.blackMoveYBoundary)
                    {
                        horizontalMove = 1;
                        blackYUpPressed = true;
                    }

                    else
                    {
                        horizontalMove = -1;
                        blackYDownPressed = true;
                    }

                    // BLACK JUMP
                    if (blackYUpPressed && blackYDownPressed) { 
                        isJumping = true;
                        animator.SetBool("isJump", true);
                    }

                }

                // BLACK SHOOT
                else if (GameSizeDefiner.blackMoveXBoundary <= touchXPos && touchXPos < GameSizeDefiner.blackShootXBoundary)
                {                      
                    Shoot(touchXPos, touchYPos);                 
                }


            }


            // WHITE STEER
            else if (playerIndex == 2)
            {

                // WHITE MOVE
                if (touchXPos > GameSizeDefiner.whiteMoveXBoundary)
                {   
                    if (touchYPos > GameSizeDefiner.whiteMoveYBoundary)
                    {
                        horizontalMove = 1;
                        whiteYUpPressed = true;
                    }
                    else
                    {
                        horizontalMove = -1;
                        whiteYDownPressed = true;
                    }

                    // WHITE JUMP
                    if (whiteYUpPressed && whiteYDownPressed)
                    {
                        isJumping = true;
                    }


                }

                // WHITE SHOOT
                else if (GameSizeDefiner.whiteShootXBoundary < touchXPos && touchXPos <= GameSizeDefiner.whiteMoveXBoundary)
                {
                    Shoot(touchXPos, touchYPos);
                }
            }
        }
    }

    private void Shoot(float touchXPos, float touchYPos)
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            BulletController bullet = Instantiate(shot, shotSpawn.position, shotSpawn.rotation).GetComponent<BulletController>();
            bullet.initialize(playerIndex, touchXPos, touchYPos);
        }
    }

    private void Move()
    {
        
        Vector2 movement = new Vector2(rb2D.velocity.x, horizontalMove * speed);
        rb2D.velocity =  movement;

    }

    private void Jump()
    {
        int jumpDriection = 1;

        if (!isJumping || !isOnGround)
        {
            return;
        }

        if (locatedArea == WHITE)
        {
            jumpDriection = -1;
        } else
        {
            jumpDriection = 1;
        }

        rb2D.AddForce(Vector2.left * jumpPower * jumpDriection, ForceMode2D.Impulse);
        

    }

    private void CheckOutOfMap()
    {
        if (gameObject.transform.position.y < -5)
        {
            gameObject.transform.position = new Vector2(transform.position.x, 5);
        }

        else if (gameObject.transform.position.y > 5)
        {
            gameObject.transform.position = new Vector2(transform.position.x, -5);
        }
    }

    
}
