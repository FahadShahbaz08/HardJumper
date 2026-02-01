using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public int intCoinsToAdd = 1;
    ScoreManager scoremanager;


   public GameObject coinSounds;

    ////Use this for initialization
    void Start()
    {
        ScoreManager scoremanager = GetComponentInChildren<ScoreManager>();
        //coinSounds.GetComponent<CoinSounds>();
        coinSounds = GameObject.Find("SoundsGO");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() == null)
            return;

        if (other.tag == "Player")
        {


            coinSounds.GetComponentInChildren<CoinSounds>().getCoinSound();

            //scoremanager.GetComponent<ScoreManager>().AddCoins(intCoinsToAdd);
            ScoreManager.AddCoins(intCoinsToAdd);
            Destroy(gameObject);



        }

    }


    // Update is called once per frame
    void Update()
    {
        Vector3 euler = this.transform.localEulerAngles;
        //euler.y += 2f;
        euler.y += 5f;
        this.transform.localEulerAngles = euler;
        //transform.Rotate(0,0,100 * Time.deltaTime);

    }
}
