using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManagerInstanceHelper : MonoBehaviour
{

    [Header("Audio Settings")]
    [SerializeField] public GameObject audioManager;

    public void Awake()
    {
        if(AudioManager.Instance == null)
        {
            Instantiate(audioManager);
        }
    }

    public void Play(string soundName)
    {
        AudioManager.Instance.Play(soundName);
    }

    public void Stop(string soundName)
    {
        AudioManager.Instance.Stop(soundName);
    }
}