using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

namespace Fahad.UI
{
    public class SplashPanel : MonoBehaviour
    {
        public string sceneToLoad;
        public float timetoLoad;
        public Slider loadingBar;

        void Start()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Invoke(nameof(LoadSplash), timetoLoad);
        }

        void LoadSplash()
        {
            StartCoroutine(DummyLoadMainMenu());
        }
        IEnumerator DummyLoadMainMenu()
        {
            while (loadingBar.value < 100f)
            {
                loadingBar.value += Time.deltaTime * 100f; 
                loadingBar.value = Mathf.Clamp(loadingBar.value, 0f, 100f);
                yield return null;
            }

            gameObject.SetActive(false);
        }


        public void LoadSceneByName(string sceneName)
        {
            StartCoroutine(LoadScene(sceneName));
        }

        IEnumerator LoadScene(string sceneName)
        {
            Time.timeScale = 1.0f;
            AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
            async.allowSceneActivation = true;

            while (!async.isDone)
            {
                loadingBar.value = async.progress;
                yield return null;
            }
        }
    }
}
