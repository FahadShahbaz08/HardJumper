using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header("UI Panels")]
    public Canvas mainMenuCanvas;
    public Canvas levelSelectCanvas;

    [Header("Level Buttons Setup")]
    [Tooltip("Assign all level buttons in order (e.g., Level 1-1, 1-2, 1-3, 2-1, etc.)")]
    public Button[] levelButtons;

    [Tooltip("Assign text labels showing level status (same order as buttons).")]
    public Text[] levelStatusTexts;

    [Tooltip("Optional play prompt UI for locked levels (same order).")]
    public GameObject[] promptUIs;

    [Header("Settings")]
    [Tooltip("How many levels are currently available (stored in PlayerPrefs).")]
    public int defaultUnlockedLevel = 1;

    private int levelReached;

    private void Start()
    {
        // Load unlocked level from PlayerPrefs (or default)
        levelReached = PlayerPrefs.GetInt("levelReached", defaultUnlockedLevel);

        // Safety: lock all first
        for (int i = 0; i < levelButtons.Length; i++)
        {
            bool unlocked = i < levelReached;

            // Enable / disable button interactivity
            levelButtons[i].interactable = unlocked;

            // Update status text if provided
            if (levelStatusTexts != null && i < levelStatusTexts.Length)
            {
                levelStatusTexts[i].text = unlocked ? "Level Available" : "Locked";
                levelStatusTexts[i].color = unlocked ? Color.green : Color.gray;
            }

            // Hide any prompt UIs
            if (promptUIs != null && i < promptUIs.Length)
                promptUIs[i].SetActive(false);
        }

        // Ensure proper starting UI state
        if (levelSelectCanvas != null) levelSelectCanvas.enabled = false;
        if (mainMenuCanvas != null) mainMenuCanvas.enabled = true;
    }

    // ========================
    // Level Unlock Management
    // ========================

    public void UnlockNextLevel(int nextLevelIndex)
    {
        int currentMax = PlayerPrefs.GetInt("levelReached", defaultUnlockedLevel);
        if (nextLevelIndex > currentMax)
        {
            PlayerPrefs.SetInt("levelReached", nextLevelIndex);
            PlayerPrefs.Save();
            Debug.Log($"🔓 New Level Unlocked: {nextLevelIndex}");
        }
    }

    // ========================
    // UI Navigation
    // ========================

    public void ShowLevelSelect()
    {
        if (mainMenuCanvas) mainMenuCanvas.enabled = false;
        if (levelSelectCanvas) levelSelectCanvas.enabled = true;
    }

    public void ShowMainMenu()
    {
        if (mainMenuCanvas) mainMenuCanvas.enabled = true;
        if (levelSelectCanvas) levelSelectCanvas.enabled = false;
    }

    // ========================
    // Scene Loading
    // ========================

    public void PlayLevel(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogWarning("⚠️ Scene name is missing!");
            return;
        }

        Debug.Log($"▶ Loading level: {sceneName}");
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Debug.Log("🛑 Exiting game...");
        Application.Quit();
    }
}
