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
        IsDead = true;
        gameOverUI.SetActive(true);
        scoreBackgroundTransition.SetTrigger("ScoreBackgroundEndwithExitTime");
        attentionLevelTranstion.SetTrigger("AttentionLevelEnd");
        buttonSelectManager.ToOver();
    }

    public void Retry()
    {
        AudioManager.Instance.Stop("visible");
        AudioManager.Instance.Stop("invisible");
        SceneManager.LoadScene("GameScene");
    }
}
