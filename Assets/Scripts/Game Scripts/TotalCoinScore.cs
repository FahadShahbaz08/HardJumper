using UnityEngine;
using UnityEngine.UI;

public class TotalCoinScore : MonoBehaviour
{
    [Header("UI References")]
    [Tooltip("UI Text showing total coins collected.")]
    public Text coinText;

    [Tooltip("UI Text showing total score earned.")]
    public Text scoreText;

    [Tooltip("UI Text showing total deaths.")]
    public Text deathText;

    private void Start()
    {
        ShowScore();
    }

    /// <summary>
    /// Loads and displays the player's saved coins, score, and deaths.
    /// </summary>
    public void ShowScore()
    {
        // Use default values (0) if PlayerPrefs keys are missing
        int totalCoins = PlayerPrefs.GetInt("coins", 0);
        int totalScore = PlayerPrefs.GetInt("score", 0);
        int totalDeaths = PlayerPrefs.GetInt("deaths", 0);

        // Safely update UI texts if assigned
        if (coinText != null)
            coinText.text = totalCoins.ToString();

        if (scoreText != null)
            scoreText.text = totalScore.ToString();

        if (deathText != null)
            deathText.text = totalDeaths.ToString();

        Debug.Log($"🏆 Loaded Player Stats → Coins: {totalCoins}, Score: {totalScore}, Deaths: {totalDeaths}");
    }
}
