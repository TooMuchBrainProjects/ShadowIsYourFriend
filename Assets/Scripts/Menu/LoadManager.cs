using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = .25f;

    public IEnumerator LoadScene(string sceneName)
    {
        // Start Transition
        transition.SetTrigger("Start");

        // Wait until Transition complite
        yield return new WaitForSeconds(transitionTime);

        // LoadScene
        SceneManager.LoadScene(sceneName);
    }
}
