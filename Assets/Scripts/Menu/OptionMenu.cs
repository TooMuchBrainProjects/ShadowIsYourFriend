using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider slider;

    public ButtonSelectManager buttonSelectManager;
    public GameObject mainMenuUI;
    public GameObject optionsMenuUI;
    public GameObject VolumeSlider;

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

    void Update()
    {
        if (Input.GetKeyDown("backspace"))
            BackToMain();
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

    public void BackToMain()
    {
        optionsMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);
        buttonSelectManager.OptionsToMain();
    }
}
