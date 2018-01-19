using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 중력에 영향을 받는 모든 물체들이 상속하는 클래스

public class ObjectsOnGravity : MonoBehaviour {

    protected ObjectsGravityController objectsGravityController;

    const int WHITE = 0;
    const int BLACK = 1;

    protected Rigidbody2D rb2D;



    protected bool isOnGround = false;
    // Located Area
    // 0 -> white space
    // 1 -> black space
    protected int locatedArea = WHITE;


    protected Vector2 gravityVelocity;


    virtual public void Start () {
        // 컨트롤러 연결
        objectsGravityController = GameObject.FindGameObjectWithTag("ObjectsGravityController").GetComponent<ObjectsGravityController>();

        rb2D = GetComponent<Rigidbody2D>();

        objectsGravityController.addObjects(this);
	}
	

	virtual public void Update () {
        checkWhichAreaIn();
        SetGravityToZero();
	}

    private void checkWhichAreaIn()
    {
        // object의 위치를 검사, 어느 영역에 있는지 설정
        if (gameObject.transform.position.x > 0)
        {
            locatedArea = BLACK;
        }
        else
        {
            locatedArea = WHITE;
        }

    }

    private void SetGravityToZero()
    {
        if (isOnGround)
        {
            gravityVelocity = Vector2.zero;
        }
    }

    public int returnLocatedArea()
    {
        return (locatedArea == WHITE) ? WHITE : BLACK;
    }

    public Rigidbody2D getRB()
    {
        return rb2D;
    }

    public void setTrueOnGround()
    {
        isOnGround = true;
    }

    public void setFalseOnGround()
    {
        isOnGround = false;
    }

    public bool checkIsOnGround()
    {
        return isOnGround;
    }

    public void addToGravityVelocity(Vector2 addVector2)
    {
        gravityVelocity += addVector2;
    }

    public Vector2 getGravityVelocity()
    {
        return gravityVelocity;
    }

}
