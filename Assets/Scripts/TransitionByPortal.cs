using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionByPortal : MonoBehaviour {
    private Rigidbody2D rb2D;
    private bool isIncrease;

    public float rotateSpeed;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.angularVelocity = rotateSpeed;
        isIncrease = false;
    }

    private void Update()
    {
        if (isIncrease)
        {
            if (transform.localScale.x >= 0.1f)
                transform.localScale -= new Vector3(0.01f, 0.01f);
            else
                isIncrease = false;
        }
        else
        {
            if (transform.localScale.x <= 1.0f)
                transform.localScale += new Vector3(0.01f, 0.01f);
            else
                isIncrease = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Black Bullet") || collision.gameObject.CompareTag("White Bullet"))
        {
            BulletController bullet = collision.gameObject.GetComponent<BulletController>();

            if (bullet.GetIsPassedPortal())
                return;

            Transform convertedObject;
            convertedObject = collision.gameObject.transform;
            convertedObject.position = -convertedObject.position;
            bullet.SetIsPassedPortal(true);
        }
    }
}
