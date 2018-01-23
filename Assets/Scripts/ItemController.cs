using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public bool isWorking = false;
    public float current = 0.0f;

    public int type;
    public float duration;
    public GameObject itemObject;

    public Item(int itemType, float durationTime, GameObject itemObj)
    {
        duration = durationTime;
        type = itemType;
        itemObject = itemObj;
    }
}

public class ItemController : MonoBehaviour {
    private Item item;
    private Rigidbody2D itemRb2D;
    private SpriteRenderer itemRenderer;

    //private List<Item> itemList;

    public float speed;
    public int itemPeriod;
    public GameObject itemContainer;
    public Sprite rapidItemSprite;
    public Sprite jumpItemSprite;

    private int itemType;
    private const int RAPID_FIRE = 1;
    private const int SUPER_JUMP = 2;

	void Start ()
    {
        //itemList = new List<Item>();
        StartCoroutine(CreateItem());
	}
	
    IEnumerator CreateItem()
    {
        yield return new WaitForSeconds(3);
        while (true)
        {
            itemType %= 2;
            GameObject itemObj = Instantiate(itemContainer, transform.position, transform.rotation);
            item = new Item(itemType, 5.0f, itemObj);
            
            itemRb2D = item.itemObject.GetComponent<Rigidbody2D>();
            itemRenderer = item.itemObject.GetComponent<SpriteRenderer>();

            switch (itemType)
            {
                
                case RAPID_FIRE:
                    itemRenderer.sprite = rapidItemSprite;
                    break;
                case SUPER_JUMP:
                    itemRenderer.sprite = jumpItemSprite;
                    break;
            }

            itemType++;
            itemRb2D.velocity = new Vector2(0.0f, -1.0f) * speed;
            yield return new WaitForSeconds(itemPeriod);
        }
    }

    public Item GetItem()
    {
        return item;
    }

}
