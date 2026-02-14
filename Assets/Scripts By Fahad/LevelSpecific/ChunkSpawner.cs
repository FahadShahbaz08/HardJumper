using UnityEngine;
using HardRunner.Managers;
using HardRunner.Others;
namespace HardRunner.Others
{
    public class ChunkSpawner : MonoBehaviour
    {
        [SerializeField] private LevelChunk startChunk;
        [SerializeField] private GameObject endChunk;
        [SerializeField] private LevelChunk[] levelChunks;
        [SerializeField] LevelChunksHandler chunksHandler;

        private float spawnZ;
        int currentLevel = 1;
        int minimumLevelChunks = 2;
        int chunksMultiplier = 2;

        private LevelChunk lastChunk;

        void Start()
        {
            currentLevel = Managers.LevelManager.CurrentLevel;

            lastChunk = startChunk;
            spawnZ = lastChunk.transform.position.z + lastChunk.GetLength();

            SpawnChunk();
        }

        private void SpawnChunk()
        {
            for (int i = 0; i < minimumLevelChunks * chunksMultiplier; i++)
            {
                LevelChunk newChunk = Instantiate(levelChunks[Random.Range(0, levelChunks.Length)]);

                Vector3 pos = new Vector3(0, 0, spawnZ);
                newChunk.transform.position = pos;
                chunksHandler.HandleChunk(newChunk);
                spawnZ -= newChunk.GetLength();
                lastChunk = newChunk;
            }

            GameObject end = Instantiate(endChunk);
            end.transform.position = new Vector3(0, 0, spawnZ);

        }
    }
}