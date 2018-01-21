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

    private float createdX;
    private float createdY;
    private float aimedX;
    private float aimedY;

    public void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

    // Player Controller로부터 터치 정보와 Player의 위치를 받아온 후,
    // 그 정보를 통해 각도를 계산하여 탄환의 방향을 설정.
    public void initialize(float createdX_input, float createdY_input, float aimedX_input, float aimedY_input)
    {
        createdX = createdX_input;
        createdY = createdY_input;
        aimedX = aimedX_input;
        aimedY = aimedY_input;

        Vector3 spawnPosition = new Vector3(createdX, createdY, 0.0f);
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(aimedX, aimedY, 0.0f));

        float angle = Mathf.Atan2(touchPosition.y - spawnPosition.y, touchPosition.x - spawnPosition.x);

        rb2D.velocity = speed * (new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)));

    }


    private void
}
