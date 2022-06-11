using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public AudioMixer audioMixer;
    public GameObject pauseMenuUI;
    public Animator scoreBackgroundTransition;
    public Animator attentionLevelTranstion;
    public ButtonSelectManager buttonSelectManager;

    public float Volume
    {
        get { return PlayerPrefs.GetFloat("Volume"); }
        set { PlayerPrefs.SetFloat("Volume", value); }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
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
