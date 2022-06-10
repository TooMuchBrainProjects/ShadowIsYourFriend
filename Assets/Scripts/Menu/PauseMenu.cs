using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public Animator scoreBackgroundTransition;
    public Animator attentionLevelTranstion;

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
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        scoreBackgroundTransition.SetTrigger("ScoreBackgroundEnd");
        attentionLevelTranstion.SetTrigger("AttentionLevelEnd");
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.StopWithFade("visible", 0.25f);
        AudioManager.Instance.StopWithFade("invisible", 0.25f);
        SceneManager.LoadScene("MainMenu");
    }
}
