using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    [Header("Classes")]
    public GameOver gameOver;

    [Header("Logic Settings")]
    [HideInInspector] public static bool GameIsPaused = false;

    [Header("Audio Settings")]
    public AudioMixer audioMixer;

    [Header("UI Settings")]
    public GameObject pauseMenuUI;

    [Header("Animation Settings")]
    public Animator scoreBackgroundTransition;
    public Animator attentionLevelTranstion;

    [Header("Keyboard Settings")]
    public ButtonSelectManager buttonSelectManager;

    public float Volume
    {
        get { return PlayerPrefs.GetFloat("Volume"); }
        set { PlayerPrefs.SetFloat("Volume", value); }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !gameOver.IsDead)
        {
            if (GameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        scoreBackgroundTransition.SetTrigger("ScoreBackgroundStart");
        attentionLevelTranstion.SetTrigger("AttentionLevelStart");
        Volume = Volume + Mathf.Abs(-80f - Volume) / 2;
        audioMixer.SetFloat("Volume", Volume);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        scoreBackgroundTransition.SetTrigger("ScoreBackgroundEnd");
        attentionLevelTranstion.SetTrigger("AttentionLevelEnd");
        buttonSelectManager.ToPause();
        Volume = Volume - Mathf.Abs(-80f - Volume) / 3;
        audioMixer.SetFloat("Volume", Volume);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Volume = Volume + Mathf.Abs(-80f - Volume) / 2;
        audioMixer.SetFloat("Volume", Volume);
        Time.timeScale = 1f;
        GameIsPaused = false;
        AudioManager.Instance.StopWithFade("visible", 0.25f);
        AudioManager.Instance.StopWithFade("invisible", 0.25f);
        SceneManager.LoadScene("MainMenu");
    }
}
