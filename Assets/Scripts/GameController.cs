using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    private GameObject replayButton;

    private bool isGameOver = false;

	void Start () {
        replayButton = GameObject.FindGameObjectWithTag("ReplayButton");
        setGameOn();
    }
	
	void Update () {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void setGameOver()
    {
        isGameOver = true;
        replayButton.SetActive(true);
    }
    public void setGameOn()
    {
        isGameOver = false;
        replayButton.SetActive(false);
    }

    public bool checkIsGameOver()
    {
        return isGameOver;
    }

}
