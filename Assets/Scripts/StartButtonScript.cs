using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonScript : MonoBehaviour {

	void Start () {
		
	}

    private void OnMouseUp()
    {
        SceneManager.LoadScene("Main");
    }

}
