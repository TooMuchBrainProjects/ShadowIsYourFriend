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
<<<<<<< HEAD
        
=======
        scoreBackgroundTransition.SetTrigger("ScoreBackgroundEndwithExitTime");
        attentionLevelTranstion.SetTrigger("AttentionLevelEnd");
>>>>>>> 9e91062fadfe8efb874439b84428095ef4dac556
    }

    public void Retry()
    {
        AudioManager.Instance.Stop("visible");
        AudioManager.Instance.Stop("invisible");
        SceneManager.LoadScene("GameScene");
    }
}
