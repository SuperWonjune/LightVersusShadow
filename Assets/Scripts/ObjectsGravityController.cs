using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsGravityController : MonoBehaviour {

    public float gravityModifier = 10f;

    // 위치한 장소를 설정하기 위한 변수
    const int WHITE = 0;
    const int BLACK = 1;


    // 게임 내 중력의 영향을 받는 물체들을 담는 리스트
    private List<ObjectsOnGravity> gravityObjectsList;

	void Awake () {
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


        foreach (ObjectsOnGravity objects in gravityObjectsList)
        {
            // 땅에 붙은 물체라면 중력 무시

            if (objects.checkIsOnGround())
            {
                continue;
            }

            gotRB = objects.getRB();

            if (objects.returnLocatedArea() == WHITE)
            {
                // 왼족으로 중력 설정
                gotRB.AddForce(Vector2.left * Physics.gravity.magnitude);
            }


            // 검은 영역에 물체가 존재할 경우
            else
            {
                if (objects.checkIsOnGround())
                    continue;

                // 오른쪽으로 중력 설정
                gotRB.AddForce(Vector2.right * Physics.gravity.magnitude);
            }
        }
    }
}
