using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FieldsButton : MonoBehaviour
{
    [Header("UI References")]
    public GameObject playLevelPromptGO;
    public GameObject unlockLevelPromptGO;
    public Text insufficientCoinsText;

    [Header("Level Info")]
    public string levelString;        // Scene name
    public string levelKey;           // Example: "Level1_2"
    public int requiredCoins;

    private int playerCoins;

    void Start()
    {
        if (insufficientCoinsText != null)
            insufficientCoinsText.text = "";

        // Optional: auto-hide prompts
        //if (playLevelPromptGO != null) playLevelPromptGO.SetActive(false);
        //if (unlockLevelPromptGO != null) unlockLevelPromptGO.SetActive(false);
    }

    // ===============================
    // UI PROMPTS
    // ===============================

    public void ShowPlayLevelUI() => ToggleUI(playLevelPromptGO, true);
    public void ClosePlayLevelUI() => ToggleUI(playLevelPromptGO, false);

    public void ShowUnlockPromptUI() => ToggleUI(unlockLevelPromptGO, true);
    public void CloseUnlockPromptUI()
    {
        ToggleUI(unlockLevelPromptGO, false);
        if (insufficientCoinsText != null) insufficientCoinsText.text = "";
    }

    private void ToggleUI(GameObject uiObject, bool state)
    {
        if (uiObject != null)
            uiObject.SetActive(state);
    }

    // ===============================
    // SCENE HANDLING
    // ===============================

    public void LoadLevel()
    {
        if (!string.IsNullOrEmpty(levelString))
            SceneManager.LoadScene(levelString);
        else
            Debug.LogWarning("No scene assigned to load.");
    }

    // ===============================
    // LEVEL PURCHASE SYSTEM
    // ===============================

    public void PurchaseLevel()
    {
        if (string.IsNullOrEmpty(levelKey))
        {
            Debug.LogError("No level key set for this button!");
            return;
        }

        playerCoins = PlayerPrefs.GetInt("coins", 0);

        if (playerCoins < requiredCoins)
        {
            if (insufficientCoinsText != null)
                insufficientCoinsText.text = "Insufficient Coins";
            Debug.Log("Not enough coins to unlock " + levelKey);
            return;
        }

        // Deduct coins and unlock level
        playerCoins -= requiredCoins;
        PlayerPrefs.SetInt("coins", playerCoins);
        PlayerPrefs.SetInt(levelKey, 1);
        PlayerPrefs.Save();

        Debug.Log($"Unlocked {levelKey}. Remaining coins: {playerCoins}");

        // Hide UI
        if (unlockLevelPromptGO != null)
            unlockLevelPromptGO.SetActive(false);
        if (insufficientCoinsText != null)
            insufficientCoinsText.text = "";
    }
}
