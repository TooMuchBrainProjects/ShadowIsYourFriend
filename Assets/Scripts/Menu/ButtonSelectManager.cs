using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelectManager : MonoBehaviour
{
    public GameObject mainFirstButton;
    public GameObject optionsFirstButtonMain;
    public GameObject optionsClosedButtonMain;

    public GameObject pauseFirstButton;
    public GameObject overFirstButton;

    public void ToMain()
    {
        // Clear selected object
        EventSystem.current.SetSelectedGameObject(null);

        // Set a new selected object
        EventSystem.current.SetSelectedGameObject(mainFirstButton);
    }

    public void MainToOptions()
    {
        // Clear selected object
        EventSystem.current.SetSelectedGameObject(null);

        // Set a new selected object
        EventSystem.current.SetSelectedGameObject(optionsFirstButtonMain);
    }

    public void OptionsToMain()
    {
        // Clear selected object
        EventSystem.current.SetSelectedGameObject(null);

        // Set a new selected object
        EventSystem.current.SetSelectedGameObject(optionsClosedButtonMain);
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
