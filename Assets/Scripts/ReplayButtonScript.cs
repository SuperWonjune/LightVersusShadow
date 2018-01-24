using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReplayButtonScript : MonoBehaviour {

    private GameController gameController;

    private Button replayButton;

    void Start () {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        replayButton = GameObject.FindGameObjectWithTag("ReplayButton").GetComponent<Button>();

        replayButton.onClick.AddListener(() => onRestartButtonClick());
        replayButton.gameObject.SetActive(false);
    }

    private void onRestartButtonClick()
    {
        gameController.setGameOn();
        SceneManager.LoadScene("Main");
    }
}
