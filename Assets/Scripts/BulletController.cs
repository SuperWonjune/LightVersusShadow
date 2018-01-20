using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : ObjectsOnGravity
{
    public int speed;
    public Sprite blackBullet;
    public Sprite whiteBullet;

    private SpriteRenderer spriteRenderer;
    private PlatformController platformController;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();    
        rb2D.velocity = Vector2.left * speed;
        platformController = GameObject.FindGameObjectWithTag("Platform Controller").GetComponent<PlatformController>();
        
    }

    public override void Update()
    {
        base.Update();
    }

    private void FixedUpdate()
    {
        setColorByLocation();        
    }
    
    // 탄환의 현재 위치에 따라 스프라이트 렌더링 변경.
    private void setColorByLocation()
    {
        if (returnLocatedArea() == WHITE)
        {
            spriteRenderer.sprite = blackBullet;
        }
        else
        {
            spriteRenderer.sprite = whiteBullet;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {   
            platformController.removePlatform(collision.gameObject);
            setDead();
        }
    }

}
