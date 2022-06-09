using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource titlemusic;
    public void PlayGame()
    {
<<<<<<< HEAD
        DontDestroyOnLoad(this);
        Coroutine m = StartCoroutine(FadeAudioSource.StartFade(titlemusic, 0.25f, 0f, () => Destroy(GameObject.Find("MainMenuManager"))));
  
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
=======
        SceneManager.LoadScene("GameScene");
>>>>>>> 59bd6aaaba43c5db9932554b78e7fed8ca7c9c60
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
