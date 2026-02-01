using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject mainMenuUI;
    public GameObject gameOverMenuUI;
    public GameObject loadingPanelUI;
    public GameObject pauseMenuUI;
    public GameObject hudGame;

    [Header("UI Elements")]
    public Slider loadingBar;
    public Text inGameCoins;

    [Header("Scene Names")]
    public string levelSceneName;
    public string homeSceneName;

    private ScoreManager scoreManager;
    public static UIManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        // Initialize UI states
        SafeSetActive(gameOverMenuUI, false);
        SafeSetActive(pauseMenuUI, false);
        SafeSetActive(loadingPanelUI, false);
        SafeSetActive(hudGame, false);

        // Get references
        if (inGameCoins == null)
            inGameCoins = GameObject.Find("CoinsGO")?.GetComponent<Text>();

        scoreManager = FindObjectOfType<ScoreManager>();
    }

    // ---------------------------
    // UI Button Events
    // ---------------------------

    public void StartGame()
    {
        var player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
        if (player != null)
            player.playerActive = true;

        SafeSetActive(mainMenuUI, false);
        hudGame.SetActive(true);
    }

    public void RestartGame()
    {
        StartCoroutine(LoadSceneAsync(levelSceneName));
    }

    public void GoToHomeMenu()
    {
        StartCoroutine(LoadSceneAsync(homeSceneName));
    }

    public void ShowRestartMenu()
    {
        SafeSetActive(gameOverMenuUI, true);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        SafeSetActive(pauseMenuUI, true);
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        SafeSetActive(pauseMenuUI, false);
    }

    // ---------------------------
    // Scene Loading with Progress
    // ---------------------------

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogWarning("Scene name is empty or null!");
            yield break;
        }

        SafeSetActive(loadingPanelUI, true);
        Time.timeScale = 1f; // Ensure normal time flow

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;

        float progress = 0f;

        while (!async.isDone)
        {
            progress = Mathf.Clamp01(async.progress / 0.9f);

            if (loadingBar != null)
                loadingBar.value = Mathf.MoveTowards(loadingBar.value, progress, Time.deltaTime * 3f);

            if (async.progress >= 0.9f && loadingBar.value >= 0.99f)
            {
                yield return new WaitForSeconds(0.3f);
                async.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    // ---------------------------
    // Utility
    // ---------------------------

    private void SafeSetActive(GameObject obj, bool state)
    {
        if (obj != null)
            obj.SetActive(state);
    }
}
