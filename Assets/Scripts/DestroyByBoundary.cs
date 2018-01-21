using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ObjectsOnGravity objects;

        objects = collision.gameObject.GetComponent<ObjectsOnGravity>();
        if (objects.CompareTag("Bullet"))
        {
            objects.setDead();
        }
    }
}
