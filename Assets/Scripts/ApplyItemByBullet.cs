using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyItemByBullet : MonoBehaviour {
    private PlayerController devil;
    private PlayerController angel;
    private ItemController itemController;
    private Item item;
    private Collider2D col2D;
    
    void Start ()
    {
        col2D = GetComponent<Collider2D>();
        devil = GameObject.FindGameObjectWithTag("Devil").GetComponent<PlayerController>();
        angel = GameObject.FindGameObjectWithTag("Angel").GetComponent<PlayerController>();
        itemController = GameObject.FindGameObjectWithTag("Item Controller").GetComponent<ItemController>();
        item = itemController.GetItem();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Black Bullet"))
        {
            // 검은색 플레이어에게 아이템 제공
            devil.ApplyItem(item);
            collision.gameObject.GetComponent<ObjectsOnGravity>().setDead();
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("White Bullet"))
        {
            // 하얀색 플레이어에게 아이템 제공
            angel.ApplyItem(item);
            col2D.isTrigger = false;
            collision.gameObject.GetComponent<ObjectsOnGravity>().setDead();
            Destroy(gameObject);
        }
    }
}
