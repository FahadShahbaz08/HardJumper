using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPrefabs : MonoBehaviour
{
   public GameObject gameobjectGO;

    private void Start()
    {
        //GameObject gameobjectGO = GameObject.FindGameObjectWithTag("spawnGO");
    }

    void OnTriggerEnter(Collider trig)
    {
        //if (trig.tag == "spawnGO")
        //{
        //    Debug.Log("spawn go");
        //}

        Destroy(GameObject.FindWithTag("spawnGO"));

    }

    //private void OnCollisionEnter(Collider collision)
    //{
    //    if (collision.gameObject.tag == "spawnGO")
    //    {
    //        Destroy(collision.gameObject);
    //    }
    //}

}
