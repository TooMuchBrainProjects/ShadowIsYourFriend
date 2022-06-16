using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class OptionMenu : MonoBehaviour
{
    [Header("Classes")]
    public MainMenu mainMenu;
    public UsernameManager usernameManager;

    [Header("Audio Settings")]
    public AudioMixer audioMixer;

    [Header("UI Settings")]
    public Slider slider;
    public GameObject mainMenuUI;
    public GameObject optionsMenuUI;
    public GameObject usernameMenuUI;
    public GameObject VolumeSlider;

    public TMP_InputField usernameInputFieldUI;

    [Header("Keyboard Settings")]
    public ButtonSelectManager buttonSelectManager;

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
        if(optionsMenuUI.activeSelf)
            if (Input.GetKeyDown("backspace") || Input.GetKeyDown("escape"))
                BackToMain();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
        Volume = volume;
    }

    public void ChangeUsername()
    {

        // Values
        usernameInputFieldUI.text = usernameManager.Username;
        optionsMenuUI.SetActive(false);
        usernameMenuUI.SetActive(true);

        // Menu
        buttonSelectManager.OptionsToUsername();
    }

    public void ResetHighscore()
    {
        PlayerPrefs.SetInt("Highscore", 0);
        Highscores.DeleteHighscore(usernameManager.Username);
    }

    public void BackToMain()
    {
        optionsMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);
        buttonSelectManager.OptionsToMain();
    }
}
