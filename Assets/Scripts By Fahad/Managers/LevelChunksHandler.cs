using HardRunner.Others;
using UnityEngine;

namespace HardRunner.Managers
{
    public class LevelChunksHandler : MonoBehaviour
    {
        [SerializeField] private ObstacleData[] obstacles;
        [SerializeField] private GameObject coinPrefab;

        public void HandleChunk(LevelChunk chunk)
        {
            //    foreach (var point in chunk.middleRowPoints)
            //    {
            //        float r = Random.value;

            //        if (r < 0.4f)
            //            SpawnObstacle(chunk, point);
            //        else if (r < 0.7f)
            //            SpawnCoins(chunk, point);
            //    }
        }

        void SpawnObstacle(LevelChunk chunk, Transform point)
        {
            var data = obstacles[Random.Range(0, obstacles.Length)];

            if (data.size == ObstacleSize.SingleLane)
            {
                GameObject obj = Instantiate(data.prefab, chunk.transform);
                obj.transform.position = point.position;
            }
            else
            {
                Vector3 pos = new Vector3(chunk.middleRowPoints[0].position.x, point.position.y, point.position.z);
                GameObject obj = Instantiate(data.prefab, chunk.transform);
                obj.transform.position = pos;
            }
        }

        void SpawnCoins(LevelChunk chunk, Transform startPoint)
        {
            int count = Random.Range(3, 7);
            for (int i = 0; i < count; i++)
            {
                Vector3 pos = startPoint.position + new Vector3(0, 1.5f , i * 1.2f);
                GameObject coin = Instantiate(coinPrefab, chunk.transform);
                coin.transform.position = pos;
            }
        }

    }
}
