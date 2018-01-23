using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private bool isQuitting = false;
    private bool isGameOver = false;

	void Start () {
	}
	
	void Update () {
		if (isGameOver)
        {
            
        }

	}

    public bool getIsQuitting()
    {
        return isQuitting;
    }

    public void setGameOver()
    {
        isGameOver = true;
    }

    public bool checkIsGameOver()
    {
        return isGameOver;
    }

}
