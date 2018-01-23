using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public GameObject blackPortal;
    public GameObject whitePortal;
    public int portalPeriod;

    private GameController gameController;
    private GameObject blackPortalInstance;
    private GameObject whitePortalInstance;
    

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        
        StartCoroutine(generatePortals());
    }

    private void Update()
    {
 
    }

    IEnumerator generatePortals()
    {
        while (true)
        {
            yield return new WaitForSeconds(portalPeriod);
            CreatePortals();
            yield return new WaitForSeconds(portalPeriod);
            RemovePortals();
        }
    }

    private void CreatePortals()
    {
        float randomX, randomY;
        Vector3 RandomPosition;

        randomX = Random.Range(-5.0f, -2.5f);
        randomY = Random.Range(-3.0f, 3.0f);
        RandomPosition = new Vector3(randomX, randomY);

        blackPortalInstance = Instantiate(blackPortal, RandomPosition, Quaternion.identity);
        whitePortalInstance = Instantiate(whitePortal, -RandomPosition, Quaternion.identity);
    }

    private void RemovePortals()
    {
        Destroy(blackPortalInstance);
        Destroy(whitePortalInstance);
    }


}
