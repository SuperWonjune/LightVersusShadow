using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour {
    private Rigidbody2D rb2D;
    private Collider2D col2D;
    private SpriteRenderer spriteRenderer;

    public int speed;

    private int itemType;
    private const int RAPID_FIRE = 1;

	void Start ()
    {
        rb2D = GetComponent<Rigidbody2D>();
        col2D = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        //rb2D.velocity = new Vector2(0.0f, -1.0f) * speed;
        itemType = 0;
	}
	
	void Update ()
    {
		
	}

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Black Bullet"))
        {
            // 검은색 플레이어에게 아이템 제공
            print("I'm Devil's");
        }
        else
        {
            // 하얀색 플레이어에게 아이템 제공
            print("I'm Angel's");
        }
    }

    // 누구에게 줄 지, 어떤 아이템인지 판단 필요
    private void ApplyItem(int playerIndex, int type)
    {

        // 첫 플레이어 이후는 무시하도록 트리거 설정 해제.
        col2D.isTrigger = false;
    }

    public void CreateItem(int type)
    {
        itemType = type;
        
    }

}
