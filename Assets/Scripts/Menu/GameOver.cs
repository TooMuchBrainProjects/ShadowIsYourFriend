using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [Header("Logic Settings")]
    [HideInInspector] public bool IsDead = false;

    [Header("UI Settings")]
    public GameObject gameOverUI;

    [Header("Animation Settings")]
    public Animator scoreBackgroundTransition;
    public Animator attentionLevelTranstion;

    [Header("Keyboard Settings")]
    public ButtonSelectManager buttonSelectManager;

    public void Dead()
    {
        // Values
        IsDead = true;
        gameOverUI.SetActive(true);

        // Animation
        scoreBackgroundTransition.SetTrigger("ScoreBackgroundEndwithExitTime");
        attentionLevelTranstion.SetTrigger("AttentionLevelEnd");

        // Menu
        buttonSelectManager.ToOver();
    }

    public void Retry()
    {
        // Audio
        AudioManager.Instance.Stop("visible");
        AudioManager.Instance.Stop("invisible");

        // Menu
        SceneManager.LoadScene("GameScene");
    }

    public void LoadMenu()
    {
        // Audio
        AudioManager.Instance.StopWithFade("visible", 0.25f);
        AudioManager.Instance.StopWithFade("invisible", 0.25f);

        // Menu
        SceneManager.LoadScene("MainMenu");
    }
}
