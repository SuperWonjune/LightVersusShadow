using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : ObjectsOnGravity
{
    public int speed;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        rb2D.velocity = Vector2.left * speed;
    }

    public override void Update()
    {
        base.Update();
    }

    private void FixedUpdate()
    {

    }
}
