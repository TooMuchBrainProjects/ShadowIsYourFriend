using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UsernameMenu : MonoBehaviour
{
    [Header("Classes")]
    public UsernameManager usernameManager;

    [Header("UI Settings")]
    public GameObject usernameMenuUI;
    public GameObject optionsMenuUI;
    public GameObject mainMenuUI;

    public TMP_InputField usernameInputFieldUI;
    public GameObject usernameSaveButton;

    [Header("Keyboard Settings")]
    public ButtonSelectManager buttonSelectManager;

    void Awake()
    {
        if (string.IsNullOrEmpty(usernameManager.Username))
        {
            buttonSelectManager.OptionsToUsername();
            mainMenuUI.SetActive(false);
            usernameMenuUI.SetActive(true);
        }
    }

    void Update()
    {
        if (usernameMenuUI.activeSelf)
            if (Input.GetKeyDown("escape"))
                BackToOptions();

        if (Input.GetKeyDown("tab"))
        {
            // Clear selected object
            EventSystem.current.SetSelectedGameObject(null);

            // Set a new selected object
            EventSystem.current.SetSelectedGameObject(usernameSaveButton);
        }
    }

    public void BackToOptions() // Save Button
    {
        Highscores.DeleteHighscore(usernameManager.Username);
        usernameManager.Username = usernameInputFieldUI.text;
        Highscores.AddNewHighscore(usernameManager.Username, PlayerPrefs.GetInt("Highscore"));

        usernameMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
        buttonSelectManager.UsernameToOptions();
    }

}
