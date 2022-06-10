using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI Highscore;

    void Start()
    {
        AudioManager.Instance.Play("title");
        UpdateMainMenuHighscore();
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

    public void UpdateMainMenuHighscore()
    {
        Highscore.text = PlayerPrefs.GetInt("Highscore").ToString();
    }
}
