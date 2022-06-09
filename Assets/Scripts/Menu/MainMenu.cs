using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource titlemusic;
    public void PlayGame()
    {
        DontDestroyOnLoad(this);
        Coroutine m = StartCoroutine(FadeAudioSource.StartFade(titlemusic, 0.25f, 0f, () => Destroy(GameObject.Find("MainMenuManager"))));
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
