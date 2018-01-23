using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour {

    private AudioSource newAudio;

    void Start () {
        newAudio = gameObject.GetComponent<AudioSource>();
    }
	
	void Update () {
		
	}


    public void playSound(string soundFileName)
    {
        newAudio.PlayOneShot((AudioClip)Resources.Load(soundFileName));
    }
}
