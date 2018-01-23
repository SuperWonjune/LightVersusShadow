using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public Button replayButton;

    private bool isGameOver = false;

	void Start () {
        replayButton = GameObject.FindGameObjectWithTag("ReplayButton").GetComponent<Button>();
        setGameOn();
    }
	
	void Update () {

	}

    public void setGameOver()
    {
        isGameOver = true;
        replayButton.gameObject.SetActive(true);
    }
    public void setGameOn()
    {
        isGameOver = false;
    }

    public bool checkIsGameOver()
    {
        return isGameOver;
    }

}
