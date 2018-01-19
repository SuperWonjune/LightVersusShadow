using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsGravityController : MonoBehaviour {

    public float gravityModifier = 1f;

    // 위치한 장소를 설정하기 위한 변수
    const int WHITE = 0;
    const int BLACK = 1;

    private Vector2 gravityVelocity;


    //Physics2D.gravity = new Vector2(10f, 0f, 0f);
    private Vector2 whiteAreaGravity = new Vector2(9.8f, 0f);
    private Vector2 blackAreaGravity = new Vector2(-9.8f, 0f);

    // 게임 내 중력의 영향을 받는 물체들을 담는 리스트
    private List<ObjectsOnGravity> gravityObjectsList;

	void Start () {
        gravityObjectsList = new List<ObjectsOnGravity>();
	}

	void Update () {
        
    }

    private void FixedUpdate()
    {
        setGravityToObjects();
    }

    public void addObjects(ObjectsOnGravity objectsToPush)
    {
        gravityObjectsList.Add(objectsToPush);
    }

    private void setGravityToObjects()
    {
        Rigidbody2D gotRB;
        // 리스트 안에 들어있는 Object들의 현재 위치를 조사
        // 있는 위치에 따라 서로 다른 정반대의 중력을 적용

        if (gravityObjectsList.Count == 0)
        {
            return;
        }

        gravityVelocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        Vector2 deltaPosition = gravityVelocity * Time.deltaTime;

        Vector2 move = Vector2.left * deltaPosition.y;

        foreach (ObjectsOnGravity objects in gravityObjectsList)
        {
            gotRB = objects.getRB();

            

            // 영역에 따라 중력의 방향만 설정

            // 흰색의 영역에 물체가 존재할 경우
            if (objects.returnLocatedArea() == WHITE)
            {
                gotRB.AddForce(whiteAreaGravity * Physics.gravity.magnitude * gotRB.mass * Time.fixedDeltaTime);
            }


            // 검은 영역에 물체가 존재할 경우
            else
            {
                gotRB.AddForce(blackAreaGravity);
            }

            // 중력 설정
            gotRB.position = gotRB.position + move;


        }


        
    }
}
