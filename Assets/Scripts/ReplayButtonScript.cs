using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReplayButtonScript : MonoBehaviour {

    private GameController gameController;

    private Button restartButton;

    void Start () {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        restartButton = GameObject.FindGameObjectWithTag("ReplayButton").GetComponent<Button>();
        restartButton.onClick.AddListener(() => onRestartButtonClick());
    }

    private void onRestartButtonClick()
    {
        gameController.setGameOn();
        SceneManager.LoadScene("Main");
    }
}
