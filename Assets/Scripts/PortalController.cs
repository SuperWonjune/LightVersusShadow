using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public GameObject blackPortal;
    public GameObject whitePortal;
    public int portalPeriod;

    private void Start()
    {

    }

    IEnumerator generatePortals()
    {
        while (true)
        {
            yield return new WaitForSeconds(portalPeriod);
            CreatePortals();
        }
    }

    private void CreatePortals()
    {
        float randomX, randomY;
        Vector3 RandomPosition;

        randomX = Random.Range(-5.0f, 2.5f);
        randomY = Random.Range(-3.0f, 3.0f);
        RandomPosition = new Vector3(randomX, randomY);

        Instantiate(blackPortal, RandomPosition, Quaternion.identity);
        Instantiate(whitePortal, -RandomPosition, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Black Bullet") || collision.gameObject.CompareTag("White Bullet"))
        {
            Transform convertedObject;
            convertedObject = collision.gameObject.transform;
            convertedObject.position = -convertedObject.position;
        }
    }

}
