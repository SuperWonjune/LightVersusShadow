using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {
    public GameObject whitePlatform;
    public GameObject blackPlatform;

    private AudioSource newAudio;
    private List<GameObject> platformList;
    private GameController gameController;

    private int platformUpdateFlag;
    private const int LEFT = 0;
    private const int MIDDLE_LEFT = 1;    
    private const int CENTER = 2;
    private const int MIDDLE_RIGHT = 3;
    private const int RIGHT = 4;

    // Use this for initialization
    void Start () {
        platformList = new List<GameObject>();
        newAudio = gameObject.AddComponent<AudioSource>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        StartCoroutine(generatePlatforms());
        platformUpdateFlag = 1;
    }
	
	// Update is called once per frame
	void Update () {

        
        foreach (GameObject objects in platformList.ToArray())
        {   
            if (platformUpdateFlag == 1)
            {
                if (objects.transform.localScale.y > 2.0f)
                    break;
                objects.transform.localScale += new Vector3(0.0f, 0.1f, 0.0f);
            }

            else if (platformUpdateFlag == 2)
            {
                if (objects.transform.localScale.y < 0.2f)
                    removePlatforms();
                objects.transform.localScale -= new Vector3(0.0f, 0.1f, 0.0f);
            }
            
        }
	}

    IEnumerator generatePlatforms()
    {
        while (true)
        {
            platformUpdateFlag = 1;
            createPlatforms();
            yield return new WaitForSeconds(4);

            platformUpdateFlag = 2;
            yield return new WaitForSeconds(2);
        }
        
    }

    // 왼쪽, 가운데, 오른쪽 발판을 하나씩 생성.
    private void createPlatforms()
    {
        createPlatformByDirection(LEFT);
        createPlatformByDirection(MIDDLE_LEFT);
        createPlatformByDirection(MIDDLE_RIGHT);
        createPlatformByDirection(RIGHT);
    }

    // Y축(왼쪽, 가운데, 오른쪽 중 택)은 고정시킨 상태에서 X축은 랜덤하게 발판을 생성
    private void createPlatformByDirection(int direction)
    {
        Vector2 createPosition = new Vector2(0, 0);
        Quaternion createRotation = Quaternion.identity;
        float randomX;
        int platformCount = platformList.Count;

        if (platformCount == 0) { randomX = Random.Range(2.0f, 5.5f); }
        else {
            float select = Random.value;
            float prevHeight = platformList[platformCount - 1].transform.position.x;

            if (select < 0.5) {
                if (prevHeight + 1.5f < 6.0f)
                    randomX = prevHeight + 1.5f;
                else
                    randomX = prevHeight - 1.5f;
            }
            else {
                if (prevHeight - 1.5f > 2.5f)
                    randomX = prevHeight - 1.5f;
                else
                    randomX = prevHeight + 1.5f;
            }
        }
        
        switch (direction)
        {
            case LEFT:
                createPosition = new Vector2(randomX, -3.5f);
                break;
            case MIDDLE_LEFT:
                createPosition = new Vector2(randomX, -1.2f);
                break;
            case CENTER:
                createPosition = new Vector2(randomX, 0);
                break;
            case MIDDLE_RIGHT:
                createPosition = new Vector2(randomX, 1.2f);
                break;
            case RIGHT:
                createPosition = new Vector2(randomX, 3.5f);
                break;
        }

        GameObject whitePF = Instantiate(whitePlatform, createPosition, createRotation);
        GameObject blackPF = Instantiate(blackPlatform, -createPosition, createRotation);

        platformList.Add(blackPF);
        platformList.Add(whitePF);        
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
        newAudio.PlayOneShot((AudioClip)Resources.Load("ObjectsDestroyed"));
        platformList.Remove(platform);
        Destroy(platform);
    }


}
