using HardRunner.Economy;
using UnityEngine;

namespace HardRunner.Managers
{
    public static class LevelManager
    {
        public static string CurrentEnvironment { get; private set; }
        public static int CurrentLevel { get; private set; }

        public static void SetLevel(string env, int level)
        {
            CurrentEnvironment = env;
            CurrentLevel = level;
        }

        public static void CompleteLevel()
        {
            int unlocked = Prefs.GetUnlockedLevels(CurrentEnvironment);
            if (CurrentLevel >= unlocked)
            {
                Prefs.SetUnlockedLevels(CurrentEnvironment, CurrentLevel + 1);
            }
        }
    }
}