using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverUI;

    public void Dead()
    {
        gameOverUI.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene("GameScene");
    }
}
