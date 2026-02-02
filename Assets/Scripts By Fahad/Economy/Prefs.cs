using UnityEngine;

namespace Fahad.Economy
{
    public static class Prefs
    {
        private const string CoinsKey = "Coins";
        private const string DeathsKey = "Deaths";
        private const string LevelsUnlockedKey = "LevelsUnlocked";

        public static int Coins
        {
            get => PlayerPrefs.GetInt(CoinsKey, 0);
            set
            {
                PlayerPrefs.SetInt(CoinsKey, value);
                PlayerPrefs.Save();
            }
        }

        public static int Deaths
        {
            get => PlayerPrefs.GetInt(DeathsKey, 0);
            set
            {
                PlayerPrefs.SetInt(DeathsKey, value);
                PlayerPrefs.Save();
            }
        }

        public static int LevelsUnlocked
        {
            get => PlayerPrefs.GetInt(LevelsUnlockedKey, 1); // default 1st level unlocked
            set
            {
                PlayerPrefs.SetInt(LevelsUnlockedKey, value);
                PlayerPrefs.Save();
            }
        }

    }
}
