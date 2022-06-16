using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Classes")]
    Highscores highscoreManager;

    [Header("UI Settings")]
    public GameObject mainMenuUI;
    public GameObject optionsMenuUI;
    public GameObject highscoreMenuUI;

    [Header("Keyboard Settings")]
    public ButtonSelectManager buttonSelectManager;

    void Start()
    {
        AudioManager.Instance.Play("title");
    }

    public void PlayGame()
    {
        AudioManager.Instance.StopWithFade("title", 0.25f);
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    public void ShowOptions()
    {
        mainMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
        buttonSelectManager.MainToOptions();
    }

    public void ShowHighscores()
    {
        //highscoreManager.DownloadHighscores();

        mainMenuUI.SetActive(false);
        highscoreMenuUI.SetActive(true);
        buttonSelectManager.MainToHighscore();
    }
}
