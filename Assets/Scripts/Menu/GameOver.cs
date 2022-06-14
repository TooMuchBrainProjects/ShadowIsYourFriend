using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [HideInInspector] public bool IsDead = false;

    public GameObject gameOverUI;
    public Animator scoreBackgroundTransition;
    public Animator attentionLevelTranstion;
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
