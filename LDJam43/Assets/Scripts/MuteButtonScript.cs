using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteButtonScript : MonoBehaviour {
    AudioController audioController;
	// Use this for initialization
	void Start () {
        audioController = FindObjectOfType<AudioController>();

    }
	
    public void Mute()
    {
        audioController.ToggleMute();
    }
}
