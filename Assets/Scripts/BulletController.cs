using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : ObjectsOnGravity
{
    public int speed;
    public Sprite blackBullet;
    public Sprite whiteBullet;
    public GameObject blackBulletDestroyParticle;
    public GameObject whiteBulletDestroyParticle;
    public GameObject blackBulletTrail;
    public GameObject whiteBulletTrail;
    public GameObject blackPlatformDestroyParticle;
    public GameObject whitePlatformDestoryParticle;
    

    private SpriteRenderer spriteRenderer;
    private PlatformController platformController;

    private float createdX;
    private float createdY;
    private float aimedX;
    private float aimedY;
    private bool isPassedPortal;

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
        isPassedPortal = false;

        // 총알 trail 생성
        InvokeRepeating("CreateTrail", 0.2f, 0.2f);
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
            GameObject destroyParticle;

            if (returnLocatedArea() == BLACK)
                destroyParticle = whitePlatformDestoryParticle;
            else
                destroyParticle = blackPlatformDestroyParticle;

            platformController.removePlatform(collision.gameObject);
            Instantiate(destroyParticle, transform.position, transform.rotation);
            setDead();
        }

        if (collision.gameObject.CompareTag("Angel") || collision.gameObject.CompareTag("Devil"))
        {
            setDead();
            collision.gameObject.GetComponent<PlayerController>().DestroyLife();
        }
    }

    // Player Controller로부터 터치 정보와 Player의 위치를 받아온 후,
    // 그 정보를 통해 각도를 계산하여 탄환의 방향을 설정.
    public void initialize(int playerIndex, float playerY, float aimedX_input, float aimedY_input)
    {
        if (playerIndex == 1) { createdX = -5.6f; createdY = playerY; }
        if (playerIndex == 2) { createdX = 5.6f; createdY = playerY; }

        aimedX = aimedX_input;
        aimedY = aimedY_input;

        Vector3 spawnPosition = new Vector3(createdX, createdY, 0.0f);
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(aimedX, aimedY, 0.0f));

        float angle = Mathf.Atan2(touchPosition.y - spawnPosition.y, touchPosition.x - spawnPosition.x);

        rb2D.velocity = speed * (new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)));
    }

    private void CreateTrail()
    {
        // trail particle 생성
        GameObject trailParticle;

        if (returnLocatedArea() == BLACK)
        {
            trailParticle = whiteBulletTrail;
        }
        else
        {
            trailParticle = blackBulletTrail;
        }

        Instantiate(trailParticle, transform.position, Quaternion.identity);
    }

    private void OnDestroy()
    {
        // 파괴 particle 생성
        GameObject destroyParticle;
        if (returnLocatedArea() == BLACK)
        {
            destroyParticle = whiteBulletDestroyParticle;
        }
        else
        {
            destroyParticle = blackBulletDestroyParticle;
        }

        Instantiate(destroyParticle, transform.position, Quaternion.identity);
    }

    public void SetIsPassedPortal(bool value)
    {
        isPassedPortal = value;
    }

    public bool GetIsPassedPortal()
    {
        return isPassedPortal;
    }
}
