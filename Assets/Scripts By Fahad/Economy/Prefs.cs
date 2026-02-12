using UnityEngine;

namespace HardRunner.Economy
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

        static string EnvLevelKey(string env) => $"Env_{env}_Unlocked";

        public static int GetUnlockedLevels(string env)
        {
            return PlayerPrefs.GetInt(EnvLevelKey(env), 1);
        }

        public static void SetUnlockedLevels(string env, int value)
        {
            PlayerPrefs.SetInt(EnvLevelKey(env), value);
            PlayerPrefs.Save();
        }
        static string EnvUnlockKey(string env) => $"Env_{env}_Unlocked";

        public static bool IsEnvironmentUnlocked(string env)
        {
            return PlayerPrefs.GetInt(EnvUnlockKey(env), 0) == 1;
        }


        public static void UnlockEnvironment(string env)
        {
            PlayerPrefs.SetInt(EnvUnlockKey(env), 1);
            PlayerPrefs.Save();
        }

    }
}
