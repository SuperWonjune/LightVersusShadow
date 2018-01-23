using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour {

    ObjectsOnGravity objectsOnGravity;

	void Start () {
		
	}
	
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        // player 접촉시
        if (collision.gameObject.tag == "Angel" || collision.gameObject.tag == "Devil")
        {
            // 컨트롤러 획득
            objectsOnGravity = collision.GetComponent<ObjectsOnGravity>();

            // 해당물체의 isOnGround를 true로 설정
            objectsOnGravity.setTrueOnGround();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // player 접촉 해제시
        if (collision.gameObject.tag == "Angel" || collision.gameObject.tag == "Devil")
        {
            // 컨트롤러 획득
            objectsOnGravity = collision.GetComponent<ObjectsOnGravity>();

            // 해당물체의 isOnGround를 false로 설정
            objectsOnGravity.setFalseOnGround();
        }
    }
}
