using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public float current = 0.0f;

    public string type;
    public float duration;
    public GameObject itemObject;

    public Item(string itemType, float durationTime, GameObject itemObj)
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
    private GameController gameController;

    //private List<Item> itemList;

    public float speed;
    public int itemPeriod;
    public GameObject itemContainer;
    public Sprite rapidItemSprite;
    public Sprite jumpItemSprite;

    private string itemType;
    private List<string> itemList;

	void Start ()
    {
        //itemList = new List<Item>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        itemList = new List<string>();
        StartCoroutine(CreateItem());
	}
	
    IEnumerator CreateItem()
    {
        yield return new WaitForSeconds(3);
        while (true)
        {
            foreach (string itemType in itemList)
            {
                GameObject itemObj = Instantiate(itemContainer, transform.position, transform.rotation);
                item = new Item(itemType, 5.0f, itemObj);

                itemRb2D = item.itemObject.GetComponent<Rigidbody2D>();
                itemRenderer = item.itemObject.GetComponent<SpriteRenderer>();

                switch (itemType)
                {

                    case "Rapid Fire":
                        itemRenderer.sprite = rapidItemSprite;
                        break;
                    case "Super Jump":
                        itemRenderer.sprite = jumpItemSprite;
                        break;
                }

                itemRb2D.velocity = new Vector2(0.0f, -1.0f) * speed;
                yield return new WaitForSeconds(itemPeriod);
            }
        }
    }

    private void initializeList(List<string> itemList)
    {
        itemList.Add("Rapid Fire");
        itemList.Add("Super Jump");
    }

    public Item GetItem()
    {
        return item;
    }

}
