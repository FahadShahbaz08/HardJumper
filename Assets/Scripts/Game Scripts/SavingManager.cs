using UnityEngine;

public class SavingManager : MonoBehaviour
{
    [Header("Level Unlock Settings")]
    [Tooltip("The level to unlock after the player dies.")]
    private string levelToUnlock;
    private int levelUnlockInt;

    private ScoreManager scoreManager;
    private Player player;

    private void Start()
    {
        // Try to find ScoreManager in the scene safely
        GameObject gm = GameObject.Find("GameManager");
        if (gm != null)
        {
            scoreManager = gm.GetComponent<ScoreManager>();
        }

        // Fallback if not found
        if (scoreManager == null)
        {
            scoreManager = FindObjectOfType<ScoreManager>();
            if (scoreManager == null)
            {
                Debug.LogError("⚠️ SavingManager: Could not find ScoreManager in scene!");
                return;
            }
        }

        // Find player safely
        player = FindObjectOfType<Player>();
        if (player == null)
        {
            Debug.LogError("⚠️ SavingManager: Could not find Player in scene!");
            return;
        }

        // Optional: You can preload PlayerPrefs here if needed
        Debug.Log($"[SavingManager] Loaded ScoreManager and Player successfully.");
    }

    // -------------------------------
    // Save Data Methods
    // -------------------------------
    public void SaveCoins()
    {
        if (scoreManager == null || player == null)
        {
            Debug.LogWarning("⚠️ Cannot save coins — missing references.");
            return;
        }

        int currentCoins = PlayerPrefs.GetInt("coins", 0);
        int currentScore = PlayerPrefs.GetInt("score", 0);

        currentCoins += scoreManager.intCoinsGot;
        currentScore += player.totalScore;

        PlayerPrefs.SetInt("coins", currentCoins);
        PlayerPrefs.SetInt("score", currentScore);

        Debug.Log($"✅ Coins Saved: {currentCoins} | Score Saved: {currentScore}");
    }

    public void Deaths()
    {
        int deaths = PlayerPrefs.GetInt("deaths", 0);
        deaths++;
        PlayerPrefs.SetInt("deaths", deaths);

        MakeALevelUnlockable();

        Debug.Log($"💀 Player Deaths: {deaths}");
    }

    public void MakeALevelUnlockable()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        if (levelReached < levelUnlockInt)
        {
            PlayerPrefs.SetInt("levelReached", levelUnlockInt);
            Debug.Log($"🔓 New level unlocked: {levelUnlockInt}");
        }
    }
}
