using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private AudioSource audioSource;

	void Start () {
        audioSource = GetComponent<AudioSource>();
	}

    public void StopMusic()
    {
        audioSource.Stop();
    }
}
