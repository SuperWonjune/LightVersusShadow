using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMover : ObjectsOnGravity {
    public int speed;

	// Use this for initialization
	public override void Start () {
        base.Start();
	}

    public override void Update()
    {
        base.Update();
    }

    private void FixedUpdate()
    {
        rb2D = GetComponent<Rigidbody2D>();
 
        rb2D.velocity = Vector2.left * speed;
    }
}
