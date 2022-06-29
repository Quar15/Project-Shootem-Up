using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenFade : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    public IEnumerator LoadLevel(string levelName)
    {
        Time.timeScale = 1f;
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelName, LoadSceneMode.Single);
    }

    public IEnumerator LoadLevel(int levelIndex)
    {
        Time.timeScale = 1f;
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex, LoadSceneMode.Single);
    }
}
