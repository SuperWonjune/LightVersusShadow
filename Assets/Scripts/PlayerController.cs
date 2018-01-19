using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ObjectsOnGravity {

    public int playerIndex;
    public int speed;

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

        // 컴포넌트 등록
        //rb2D = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	public override void Update () {
        base.Update();
        setMove();
        Shoot();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    
    private void setMove()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal" + playerIndex);
        verticalMove = Input.GetAxisRaw("Vertical" + playerIndex);
    }

    private void Move()
    {
        Vector2 movement = new Vector2(0, horizontalMove);
        rb2D.velocity = movement * speed;
    }

    private void Jump()
    {

    }

    private void Shoot()
    {
        if ((Input.GetButton("Fire" + playerIndex)) && (Time.time > nextFire))
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }
    }
}
