using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLevelObjects : MonoBehaviour
{
    public GameObject[] grounds, groundsInScene;
    public float[] groundDistances;
    public int randomGround, lastSpawned;
    public GameObject lastGroundPos;


    // Start is called before the first frame update
    void Start()
    {
        lastGroundPos = null;
        //groundDistances = new float[] { -47.8f, -47.8f, -47.8f, -47.8f, -47.8f, -47.8f, -47.8f, -47.8f };
        groundDistances = new float[] {
          -47.8f, -47.8f, -47.8f, -47.8f, -47.8f
        , -47.8f, -47.8f, -47.8f, -47.8f, -47.8f
        , -47.8f, -47.8f, -47.8f, -47.8f, -47.8f
        , -47.8f, -47.8f, -47.8f, -47.8f, -47.8f
        , -47.8f, -47.8f, -47.8f, -47.8f, -47.8f
        , -47.8f, -47.8f, -47.8f, -47.8f, -47.8f
        , -47.8f, -47.8f, -47.8f, -47.8f, -47.8f
        };
        //groundDistances = new float[] { -48f, -48f, -48f };
        spawnGround();
    }

    // Update is called once per frame
    void Update()
    {
        groundsInScene = GameObject.FindGameObjectsWithTag("spawnGO");
        if (groundsInScene.Length <= 3)
        {
            spawnGround();
        }
    }

    public void spawnGround()
    {
        randomGround = Random.Range(0, grounds.Length);
        if (lastGroundPos != null)
        {
            if (randomGround == lastSpawned)
            {
                randomGround = Random.Range(0, grounds.Length);
            }
            else
            {
                lastGroundPos = Instantiate(grounds[randomGround], new Vector3(lastGroundPos.transform.position.x,
                    lastGroundPos.transform.position.y, lastGroundPos.transform.position.z + groundDistances[lastSpawned]), Quaternion.identity) as GameObject;
                lastSpawned = randomGround;
            }
        }
        else
        {
            //-121.32 ,,-95.64f ,,-47.8f
            lastGroundPos = Instantiate(grounds[randomGround], new Vector3(0, 0, -121.30f), Quaternion.identity) as GameObject;
            lastSpawned = randomGround;
        }
    }


}
