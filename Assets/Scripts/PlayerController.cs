using System.Collections;
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

    // 이동 관련
    Vector3 movement;
    float horizontalMove;
    float verticalMove;

    public override void Start () {
        base.Start();
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
            return;
        }

       

        
        Touch[] myTouches = Input.touches;

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
                    if (touchYPos > GameSizeDefiner.blackMoveYBoundary)
                    {
                        horizontalMove = 1;
                    }

                    else
                    {
                        horizontalMove = -1;
                    }

                    // BLACK JUMP
                    if (myTouch.phase == TouchPhase.Began)
                    {
                        if (myTouch.tapCount == 2)
                        {
                            isJumping = true;
                        }
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
                    }
                    else
                    {
                        horizontalMove = -1;
                    }

                    // WHITE JUMP
                    if (myTouch.phase == TouchPhase.Began)
                    {
                        if (myTouch.tapCount == 2)
                        {
                            isJumping = true;
                        }
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

            Vector3 playerPosition = transform.position;
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touchXPos, touchYPos, 0.0f));
            float angle = Mathf.Atan2(playerPosition.x - touchPosition.x, -touchPosition.y - playerPosition.y);

            Vector3 spawnPosition = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0.0f);
            Quaternion quaternion = Quaternion.identity;

            //shotSpawn.localPosition = 0.5f * spawnPosition;   

            BulletController bullet = Instantiate(shot, shotSpawn.position, shotSpawn.rotation).GetComponent<BulletController>();
            bullet.initialize(transform.position.x, transform.position.y, touchXPos, touchYPos);
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

    
}
