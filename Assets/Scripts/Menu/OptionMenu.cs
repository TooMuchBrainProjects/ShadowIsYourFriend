using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider slider;

    public float Volume
    {
        get { return PlayerPrefs.GetFloat("Volume"); }
        set { PlayerPrefs.SetFloat("Volume", value); }
    }

    void Start()
    {
        audioMixer.SetFloat("Volume", Volume);
        slider.value = Volume;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
        Volume = volume;
    }

    public void ResetHighscore()
    {
        PlayerPrefs.SetInt("Highscore", 0);
    }
}
