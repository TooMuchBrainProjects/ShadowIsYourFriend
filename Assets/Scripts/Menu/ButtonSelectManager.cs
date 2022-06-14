using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelectManager : MonoBehaviour
{
    [Header("MainMenu")]
    public GameObject mainFirstButton;
    public GameObject optionsClosedButtonMain;
    public GameObject highscoreClosedButtonMain;

    [Header("OptionsMenu")]
    public GameObject optionsFirstButton;
    public GameObject usernameFirstButton;
    public GameObject usernameClosedButtonOptions;

    [Header("HighscoreMenu")]
    public GameObject highscoreFirstButton;

    [Header("PauseMenu")]
    public GameObject pauseFirstButton;

    [Header("GameOver")]
    public GameObject overFirstButton;

    public void ToMain()
    {
        // Clear selected object
        EventSystem.current.SetSelectedGameObject(null);

        // Set a new selected object
        EventSystem.current.SetSelectedGameObject(mainFirstButton);
    }

    public void OptionsToMain()
    {
        // Clear selected object
        EventSystem.current.SetSelectedGameObject(null);

        // Set a new selected object
        EventSystem.current.SetSelectedGameObject(optionsClosedButtonMain);
    }

    public void HighscoreToMain()
    {
        // Clear selected object
        EventSystem.current.SetSelectedGameObject(null);

        // Set a new selected object
        EventSystem.current.SetSelectedGameObject(highscoreClosedButtonMain);
    }

    public void MainToOptions()
    {
        // Clear selected object
        EventSystem.current.SetSelectedGameObject(null);

        // Set a new selected object
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
    }

    public void OptionsToUsername()
    {
        // Clear selected object
        EventSystem.current.SetSelectedGameObject(null);

        // Set a new selected object
        EventSystem.current.SetSelectedGameObject(usernameFirstButton);
    }

    public void UsernameToOptions()
    {
        // Clear selected object
        EventSystem.current.SetSelectedGameObject(null);

        // Set a new selected object
        EventSystem.current.SetSelectedGameObject(usernameClosedButtonOptions);
    }

    public void MainToHighscore()
    {
        // Clear selected object
        EventSystem.current.SetSelectedGameObject(null);

        // Set a new selected object
        EventSystem.current.SetSelectedGameObject(highscoreFirstButton);
    }

    public void ToPause()
    {
        // Clear selected object
        EventSystem.current.SetSelectedGameObject(null);

        // Set a new selected object
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);
    }

    public void ToOver()
    {
        // Clear selected object
        EventSystem.current.SetSelectedGameObject(null);

        // Set a new selected object
        EventSystem.current.SetSelectedGameObject(overFirstButton);
    }
}
