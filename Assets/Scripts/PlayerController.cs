using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ObjectsOnGravity {

    public int playerIndex;
    public float speed;
    public float jumpPower;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;

    private float nextFire = 0.0F;

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
        //horizontalMove = Input.GetAxisRaw("Horizontal" + playerIndex);
        //verticalMove = Input.GetAxisRaw("Vertical" + playerIndex);
        

        // Touch Movement
        // player_index = 1 -> BLACK
        // playeR_index = 2 -> WHITE

        if (Input.touchCount < 1)
        {
            return;
        }

        //Touch myTouch = Input.GetTouch(0);
        Touch[] myTouches = Input.touches;

        for (int i= 0; i< Input.touchCount; i++)
        {
            float touchXPos = myTouches[i].position.x;
            float touchYPos = myTouches[i].position.y;


            // BLACK STEER
            if (playerIndex == 1)
            {

                // BLACK MOVE
                if (touchXPos < 120)
                {
                    if (touchYPos > 450)
                    {
                        horizontalMove = 1;
                    }

                    else
                    {
                        horizontalMove = -1;
                    }
                }

                // BLACK SHOOT
                else if (120 <= touchXPos && touchXPos < 502)
                {
                    BulletController bullet = Instantiate(shot, shotSpawn.position, shotSpawn.rotation).GetComponent<BulletController>();
                    bullet.initialize(shotSpawn.position.x, shotSpawn.position.y, touchXPos, touchYPos);
                }


            }


            // WHITE STEER
            else if (playerIndex == 2)
            {

                // WHITE MOVE
                if (touchXPos > 1080)
                {
                    if (touchYPos > 450)
                    {
                        horizontalMove = 1;
                    }
                    else
                    {
                        horizontalMove = -1;
                    }


                }

                // WHITE SHOOT
                else if (702 < touchXPos && touchXPos <= 1080)
                {
                    BulletController bullet = Instantiate(shot, shotSpawn.position, shotSpawn.rotation).GetComponent<BulletController>();
                    bullet.initialize(shotSpawn.position.x, shotSpawn.position.y, touchXPos, touchYPos);
                }
            }
        }
    }

    private void setShoot()
    {
        if ((Input.GetButton("Fire" + playerIndex)) && (Time.time > nextFire))
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

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

        if (!Input.GetKey(KeyCode.Space) || !isOnGround)
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
