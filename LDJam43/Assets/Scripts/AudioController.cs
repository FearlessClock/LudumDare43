﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour {
    public bool soundMute;
    public AudioSource audioSource;
    public AudioMixer audioMixer;
    private void Awake()
    {
        AudioController[] objs = GameObject.FindObjectsOfType<AudioController>();

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    void Start () {

        soundMute = PlayerPrefs.GetInt("mute", 0) == 0? false: true;
        SetSound();
    }
    

    public void ToggleMute()
    {
        soundMute = !soundMute;
        PlayerPrefs.SetInt("mute", soundMute ? 1 : 0);
        SetSound();
    }

    private void SetSound()
    {

        if (soundMute)
        {
            audioMixer.FindSnapshot("Mute").TransitionTo(0);
        }
        else
        {
            audioMixer.FindSnapshot("FullVolume").TransitionTo(0);
        }
    }

}
