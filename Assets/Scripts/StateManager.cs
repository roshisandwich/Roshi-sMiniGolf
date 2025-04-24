using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    public Animator circleWipe;
    public void LoadLevel(string levelName)
    {
        StartCoroutine(LoadingLevel(levelName));
    }

    public void ReloadLevel()
    {
        StartCoroutine(ReloadingLevel());
    }

    IEnumerator LoadingLevel(string levelName)
    {
        circleWipe.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);

    }

    IEnumerator ReloadingLevel()
    {
        circleWipe.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
