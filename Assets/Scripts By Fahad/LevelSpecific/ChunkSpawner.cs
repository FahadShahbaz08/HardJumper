using UnityEngine;
using HardRunner.Managers;
using HardRunner.Others;
namespace HardRunner.Others
{
    public class ChunkSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject startChunk;
        [SerializeField] private GameObject endChunk;
        [SerializeField] private GameObject[] levelChunks;
        [SerializeField] LevelChunksHandler chunksHandler;

        [Header("Settings")]
        [SerializeField] private float defaultChunkLength = 47.8f;

        private float spawnZ;
        int currentLevel = 1;
        int minimumLevelChunks = 2;
        int chunksMultiplier = 2;

        void Start()
        {
            currentLevel = Managers.LevelManager.CurrentLevel;

            float startLength = GetChunkLength(startChunk);

            // startChunk ke FRONT edge se spawn start karein
            spawnZ = startChunk.transform.position.z + startLength / 2f;

            SpawnChunk();
        }


        private void SpawnChunk()
        {
            int totalChunks = minimumLevelChunks * chunksMultiplier;

            for (int i = 0; i < totalChunks; i++)
            {
                GameObject chunkPrefab = levelChunks[Random.Range(0, levelChunks.Length)];

                float chunkLength = GetChunkLength(chunkPrefab);

                // center position set karein
                float chunkCenterZ = spawnZ + chunkLength / 2f;

                GameObject newChunk = Instantiate(chunkPrefab, new Vector3(0, 0, chunkCenterZ), Quaternion.identity);

                // next spawn point update karein
                spawnZ += chunkLength;
            }

            float endLength = GetChunkLength(endChunk);

            float endCenterZ = spawnZ + endLength / 2f;

            Instantiate(endChunk, new Vector3(0, 0, endCenterZ), Quaternion.identity);
        }


        private float GetChunkLength(GameObject chunk)
        {
            Renderer renderer = chunk.GetComponentInChildren<Renderer>();

            if (renderer != null)
                return renderer.bounds.size.z;

            return defaultChunkLength;
        }


    }
}