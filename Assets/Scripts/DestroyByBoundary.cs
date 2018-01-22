using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ObjectsOnGravity objects;

        if (collision.gameObject.CompareTag("Item"))
            return;

        objects = collision.gameObject.GetComponent<ObjectsOnGravity>();
        if (objects == null)
        {
            Destroy(collision.gameObject);
        }

        else if ( objects.CompareTag("White Bullet") || objects.CompareTag("Black Bullet") )
        {
            objects.setDead();
        }
    }
}
