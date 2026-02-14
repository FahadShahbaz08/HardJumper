using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using HardRunner.Managers;

namespace HardRunner.UI
{
    public class SplashPanel : MonoBehaviour
    {
        public Slider loadingBar;
        private Coroutine currentCoroutine;

        private void OnEnable()
        {
            if (currentCoroutine == null)
                currentCoroutine = StartCoroutine(DummyLoadMainMenu());
        }

        IEnumerator DummyLoadMainMenu()
        {
            loadingBar.value = 0f;
            while (loadingBar.value < 1f)
            {
                loadingBar.value += Time.deltaTime; 
                yield return null;
            }
            loadingBar.value = 1f;
            currentCoroutine = null;
            gameObject.SetActive(false);

            AudioManager.Instance.PlayMainMenuMusic();
        }

        public void LoadSceneByName(string sceneName)
        {
            if (currentCoroutine != null)
                StopCoroutine(currentCoroutine);

            currentCoroutine = StartCoroutine(LoadScene(sceneName));
            gameObject.SetActive(true);
        }

        IEnumerator LoadScene(string sceneName)
        {
            Time.timeScale = 1f;
            loadingBar.value = 0f;

            AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
            async.allowSceneActivation = false;

            while (async.progress < 0.9f)
            {
                loadingBar.value = Mathf.Clamp01(async.progress / 0.9f);
                yield return null;
            }

            while (loadingBar.value < 1f)
            {
                loadingBar.value += Time.deltaTime * 2f;
                yield return null;
            }

            loadingBar.value = 1f;

            async.allowSceneActivation = true;
            currentCoroutine = null;
        }
    }



}
