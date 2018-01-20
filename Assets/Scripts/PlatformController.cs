﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {
    public GameObject whitePlatform;
    public GameObject blackPlatform;

    private List<GameObject> platformList;
    private const int LEFT = 0;
    private const int MIDDLE_LEFT = 1;    
    private const int CENTER = 2;
    private const int MIDDLE_RIGHT = 3;
    private const int RIGHT = 4;

    // Use this for initialization
    void Start () {
        platformList = new List<GameObject>();
        StartCoroutine(generatePlatforms());
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    IEnumerator generatePlatforms()
    {
        while (true)
        {
            createPlatforms();
            yield return new WaitForSeconds(5);

            removePlatforms();
            yield return new WaitForSeconds(2);
        }
        
    }

    // 왼쪽, 가운데, 오른쪽 발판을 하나씩 생성.
    private void createPlatforms()
    {
        createPlatformByDirection(LEFT);
        createPlatformByDirection(MIDDLE_LEFT);
        createPlatformByDirection(CENTER);
        createPlatformByDirection(MIDDLE_RIGHT);
        createPlatformByDirection(RIGHT);
    }

    // Y축(왼쪽, 가운데, 오른쪽 중 택)은 고정시킨 상태에서 X축은 랜덤하게 발판을 생성
    private void createPlatformByDirection(int direction)
    {
        Vector2 createPosition = new Vector2(0, 0);
        Quaternion createRotation = Quaternion.identity;
        float randomX = Random.Range(3.0f, 6.0f);
        

        switch (direction)
        {
            case LEFT:
                createPosition = new Vector2(randomX, -4);
                break;
            case MIDDLE_LEFT:
                createPosition = new Vector2(randomX, -2);
                break;
            case CENTER:
                createPosition = new Vector2(randomX, 0);
                break;
            case MIDDLE_RIGHT:
                createPosition = new Vector2(randomX, 2);
                break;
            case RIGHT:
                createPosition = new Vector2(randomX, 4);
                break;
        }

        GameObject whitePF = Instantiate(whitePlatform, createPosition, createRotation);
        GameObject blackPF = Instantiate(blackPlatform, -createPosition, createRotation);

        platformList.Add(whitePF);
        platformList.Add(blackPF);        
    }

    // 모든 오브젝트를 파괴한 후 리스트 비우기.
    private void removePlatforms()
    {
        foreach (GameObject objects in platformList)
        {
            Destroy(objects);
        }
        platformList.Clear();
    }

    // 외부 Object와 충돌로 파괴되었을 때 호출.
    public void removePlatform(GameObject platform)
    {
        platformList.Remove(platform);
        Destroy(platform);
    }


}
