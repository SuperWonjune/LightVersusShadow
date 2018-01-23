using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerLife
{
    public Sprite blackLife0;
    public Sprite blackLife1;
    public Sprite blackLife2;
    public Sprite whiteLife0;
    public Sprite whiteLife1;
    public Sprite whiteLife2;
}

public class PlayerController : ObjectsOnGravity {
    public int playerIndex;
    public float speed;
    public float jumpPower;
    public int lifeCount;

    public GameController gameController;
    public GameObject shot;
    public Transform shotSpawn;
    public GameObject life;
    public PlayerLife playerLife;

    public GameObject BlackDeathSparkle;
    public GameObject WhiteDeathSparkle;

    public float fireRate;

    private float nextFire = 0.0F;
    private bool isJumping = false;
    private Animator animator;
    private SpriteRenderer lifeSprite;
    private AudioSource newAudio;

    // 피격 관련
    private bool isInvincible = false;
    private float flashIntervalSec = 0.2f;
    private float invincibleTime = 1f;

    // 아이템 관련
    private bool isItemApplied = false;
    private Item appliedItem;
    
    // 이동 관련
    Vector3 movement;
    float horizontalMove;
    float verticalMove;

    public override void Start () {
        base.Start();
        animator = GetComponent<Animator>();
        lifeSprite = life.GetComponent<SpriteRenderer>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        newAudio = gameObject.AddComponent<AudioSource>();
    }
	
	public override void Update () {
        base.Update();
        getTouch();

        if (isItemApplied)
        {
            appliedItem.current += Time.deltaTime;
            if (appliedItem.current > appliedItem.duration)
            {
                appliedItem.current = 0.0f;
                RemoveItem(appliedItem);
            }
        }
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
        CheckOutOfMap();
        ClampPositionInGame();
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

        if (isOnGround)
        {
            animator.SetBool("isJump", false);
        }
        else
        {
            animator.SetBool("isJump", true);
        }


        if (Input.touchCount < 1)
        {
            animator.SetBool("isWalk", false);
            return;
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

    private void setPlayerDead()
    {
        // 파괴 particle 생성
        GameObject destroyParticle;

        // 흑
        if (playerIndex == 1)
        {
            destroyParticle = BlackDeathSparkle;
        }
        else
        {
            destroyParticle = WhiteDeathSparkle;
        }

        Instantiate(destroyParticle, transform.position, Quaternion.identity);

        setDead();
    }

    private void Shoot(float touchXPos, float touchYPos)
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            BulletController bullet = Instantiate(shot, shotSpawn.position, shotSpawn.rotation).GetComponent<BulletController>();
            bullet.initialize(playerIndex, transform.position.y, touchXPos, touchYPos);
        }
    }

    private void Move()
    {
        
        Vector2 movement = new Vector2(rb2D.velocity.x, horizontalMove * speed);
        rb2D.velocity =  movement;
        
    }

    private void ClampPositionInGame()
    {
        // 각 플레이어의 이동 반경 제한
        Vector2 pos = transform.position;

        // 흑
        if (playerIndex == 1)
        {
            pos.x = Mathf.Clamp(transform.position.x, -7, -0.5f);
        }
        else
        {
            pos.x = Mathf.Clamp(transform.position.x, 0.5f, 7);
        }
        transform.position = pos;
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

        newAudio.PlayOneShot((AudioClip)Resources.Load("JumpSound"));
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

    // 피격 시 라이프를 감소시킨 후 그에 따른 life sprite 변경.
    public void DestroyLife()
    {
        // 무적인 상황일시 무시
        if (isInvincible)
        {
            return;
        }

        newAudio.PlayOneShot((AudioClip)Resources.Load("HitByBullet"));
        lifeCount -= 1;
        tempInvincible(invincibleTime);

        if (playerIndex == 1)
        {
            switch (lifeCount)
            {
                case 0:
                    lifeSprite.sprite = playerLife.blackLife0;
                    setPlayerDead();
                    return;
                case 1:
                    lifeSprite.sprite = playerLife.blackLife1;
                    break;
                case 2:
                    lifeSprite.sprite = playerLife.blackLife2;
                    break;
            }
        }
        else
        {
            switch (lifeCount)
            {
                case 0:
                    lifeSprite.sprite = playerLife.whiteLife0;
                    setPlayerDead();
                    return;
                case 1:
                    lifeSprite.sprite = playerLife.whiteLife1;
                    break;
                case 2:
                    lifeSprite.sprite = playerLife.whiteLife2;
                    break;
            }
        }
    }

    private void tempInvincible(float invincibleTime)
    {
        // input 시간동안 깜빡거리면서 무적
        isInvincible = true;
        int flashTime = (int)(invincibleTime / flashIntervalSec);

        StartCoroutine(Flasher(flashTime));
    }

    IEnumerator Flasher(int totalFlashTime)
    {
        for (int i = 0; i< totalFlashTime; i++)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(flashIntervalSec);
            GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(flashIntervalSec);
        }

        isInvincible = false;
    }

    // 아이템 적용
    public void ApplyItem(Item item)
    {
        appliedItem = item;

        switch (item.type)
        {
            case "Rapid Fire":
                fireRate /= 3;
                break;
            case "Super Jump":
                jumpPower += 2;
                break;
        }

        isItemApplied = true;
    }

    private void RemoveItem(Item item)
    {
        switch (item.type)
        {
            case "Rapid Fire":
                fireRate *= 3;
                break;
            case "Super Jump":
                jumpPower -= 2;
                break;
        }

        appliedItem = null;
        isItemApplied = false;
    }

}
