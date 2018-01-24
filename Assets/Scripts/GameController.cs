using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    private GameObject replayButton;
    private CameraController cameraController;

    private bool isGameOver = false;

	void Start () {
        replayButton = GameObject.FindGameObjectWithTag("ReplayButton");
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
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
        cameraController.StopMusic();
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
