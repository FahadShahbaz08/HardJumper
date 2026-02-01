using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SplashManager : MonoBehaviour
{
    public string sceneToLoad;
    public float timetoLoad;
    public Slider loadingBar;
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Invoke("LoadSplash", timetoLoad);
    }

    void LoadSplash()
    {
        StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
        Time.timeScale = 1.0f;
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneToLoad);
        async.allowSceneActivation = true;
        while (!async.isDone)
        {
            loadingBar.value = (int)((async.progress) * 112);
            yield return null;
        }

    }
}
