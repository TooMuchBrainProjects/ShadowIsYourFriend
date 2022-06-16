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
        // Audio
        Volume = Volume + Mathf.Abs(-80f - Volume) / 2;
        audioMixer.SetFloat("Volume", Volume);

        // Animation
        scoreBackgroundTransition.SetTrigger("ScoreBackgroundStart");
        attentionLevelTranstion.SetTrigger("AttentionLevelStart");

        // Values
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        // Audio
        Volume = Volume - Mathf.Abs(-80f - Volume) / 3;
        audioMixer.SetFloat("Volume", Volume);

        // Animation
        scoreBackgroundTransition.SetTrigger("ScoreBackgroundEnd");
        attentionLevelTranstion.SetTrigger("AttentionLevelEnd");

        // Keyboard
        buttonSelectManager.ToPause();

        // Values
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Retry()
    {
        // Audio
        Volume = Volume + Mathf.Abs(-80f - Volume) / 2;
        audioMixer.SetFloat("Volume", Volume);
        AudioManager.Instance.Stop("visible");
        AudioManager.Instance.Stop("invisible");

        // Animation
        scoreBackgroundTransition.SetTrigger("ScoreBackgroundStart");
        attentionLevelTranstion.SetTrigger("AttentionLevelStart");

        // Values
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        // Menu
        SceneManager.LoadScene("GameScene");
    }

    public void LoadMenu()
    {
        // Audio
        Volume = Volume + Mathf.Abs(-80f - Volume) / 2;
        audioMixer.SetFloat("Volume", Volume);
        AudioManager.Instance.StopWithFade("visible", 0.25f);
        AudioManager.Instance.StopWithFade("invisible", 0.25f);

        // Values
        Time.timeScale = 1f;
        GameIsPaused = false;

        // Menu
        SceneManager.LoadScene("MainMenu");
    }
}
