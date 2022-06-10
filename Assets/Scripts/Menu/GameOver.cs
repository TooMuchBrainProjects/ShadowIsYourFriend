using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverUI;
    public Animator scoreBackgroundTransition;
    public Animator attentionLevelTranstion;

    public void Dead()
    {
        gameOverUI.SetActive(true);
        scoreBackgroundTransition.SetTrigger("ScoreBackgroundEndwithExitTime");
        attentionLevelTranstion.SetTrigger("AttentionLevelEnd");
    }

    public void Retry()
    {
        AudioManager.Instance.Stop("visible");
        AudioManager.Instance.Stop("invisible");
        SceneManager.LoadScene("GameScene");
    }
}
