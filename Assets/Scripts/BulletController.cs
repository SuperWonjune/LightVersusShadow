using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : ObjectsOnGravity
{
    public int speed;

    private float createdX;
    private float createdY;
    private float aimedX;
    private float aimedY;

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

    public void initialize(float createdX_input, float createdY_input, float aimedX_input, float aimedY_input)
    {
        createdX = createdX_input;
        createdY = createdY_input;
        aimedX = aimedX_input;
        aimedY = aimedY_input;
    }
}
