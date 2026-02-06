using UnityEngine;

namespace HardRunner.Others
{
    public class LevelChunk : MonoBehaviour
    {
        public Transform[] leftRowPoints;
        public Transform[] middleRowPoints;
        public Transform[] rightRowPoints;

        public float chunkLength = 30f; 

        public float GetLength()
        {
            return chunkLength;
        }
    }
}