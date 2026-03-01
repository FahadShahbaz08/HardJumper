using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using HardRunner.Economy;
using HardRunner.Managers;

public class DailyRewardManager : MonoBehaviour
{
    [Header("Buttons (Assign Day1 → Day7)")]
    [SerializeField] private Button[] rewardButtons;

    [Header("Coin Rewards (Size must be 7)")]
    [SerializeField] private int[] rewards = new int[8] { 100, 200, 300, 400, 500, 600, 700, 1000 };

    [Header("Optional Timer Text")]
    [SerializeField] private Text timerText;

    private const string DayKey = "DailyReward_DayIndex";
    private const string TimeKey = "DailyReward_LastClaim";

    private int currentDayIndex;

    private void OnEnable()
    {
        LoadData();
        RefreshButtons();
    }

    private void Update()
    {
        UpdateTimer();
    }

    void LoadData()
    {
        currentDayIndex = PlayerPrefs.GetInt(DayKey, 0);
    }

    void RefreshButtons()
    {
        for (int i = 0; i < rewardButtons.Length; i++)
        {
            int index = i;

            rewardButtons[i].interactable = (i == currentDayIndex && CanClaim());

            rewardButtons[i].onClick.RemoveAllListeners();
            rewardButtons[i].onClick.AddListener(() => ClaimReward(index));
        }
    }

    public void ClaimReward(int index)
    {
        if (index != currentDayIndex) return;
        if (!CanClaim()) return;

        // Add coins
        Prefs.Coins += rewards[index];

        GameEventManager.OnCoinCollected();

        // Save time
        PlayerPrefs.SetString(TimeKey, DateTime.UtcNow.ToString());

        // Move to next day
        currentDayIndex++;

        if (currentDayIndex >= 7)
            currentDayIndex = 0; // reset cycle

        PlayerPrefs.SetInt(DayKey, currentDayIndex);

        PlayerPrefs.Save();

        RefreshButtons();
        AudioManager.Instance.PlayUiClickSound();
    }

    bool CanClaim()
    {
        if (!PlayerPrefs.HasKey(TimeKey))
            return true;

        DateTime lastClaim = DateTime.Parse(PlayerPrefs.GetString(TimeKey));

        return (DateTime.UtcNow - lastClaim).TotalHours >= 24;
    }

    void UpdateTimer()
    {
        if (timerText == null) return;

        if (CanClaim())
        {
            timerText.text = "Reward Ready";
            return;
        }

        DateTime lastClaim = DateTime.Parse(PlayerPrefs.GetString(TimeKey));
        TimeSpan remaining = TimeSpan.FromHours(24) - (DateTime.UtcNow - lastClaim);

        timerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}",
            remaining.Hours,
            remaining.Minutes,
            remaining.Seconds);
    }
}