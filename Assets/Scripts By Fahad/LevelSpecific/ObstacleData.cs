using UnityEngine;

public enum ObstacleSize
{
    SingleLane,
    ThreeLane
}

namespace HardRunner.Others
{
    [CreateAssetMenu(fileName ="Obstacle", menuName = "HardRunner/Obstacle Item")]
    [System.Serializable]
    public class ObstacleData : ScriptableObject
    {
        public GameObject prefab;
        public ObstacleSize size;
    }
}