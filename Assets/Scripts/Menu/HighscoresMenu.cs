using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoresMenu : MonoBehaviour
{
    [Header("UI Settings")]
    public GameObject highscoreMenuUI;
    public GameObject mainMenuUI;

    [Header("Keyboard Settings")]
    public ButtonSelectManager buttonSelectManager;

    void Update()
    {
        if (highscoreMenuUI.activeSelf)
            if (Input.GetKeyDown("backspace") || Input.GetKeyDown("escape"))
                BackToMain();
    }

    public void BackToMain()
    {
        highscoreMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);
        buttonSelectManager.HighscoreToMain();
    }
}
