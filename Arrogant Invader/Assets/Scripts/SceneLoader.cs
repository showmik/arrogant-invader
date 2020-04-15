using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int buildIndex)
    {
        transition.SetTrigger("StartTransition");
        yield return new WaitForSecondsRealtime(transitionTime);
        SceneManager.LoadScene(buildIndex);
    }
}
