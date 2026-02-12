using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HardRunner.Scriptable
{
    [CreateAssetMenu(fileName = "NewEnvironmentItem", menuName = "HardRunner/Environment Item")]
    public class EnvironementItemScriptable : ScriptableObject
    {
        public string environmentCategory;
        public Sprite environmentImage;

        public int maxLevels = 0;

        [Header("Unlock Settings")]
        public int unlockCost;

        [Header("Scene Settings")]
        public string sceneName;
    }
}