using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public static int intCoinsCollected;
    public Text uiCoinsCollected;
    public int intCoinsGot;
    void Start()
    {
        uiCoinsCollected = GameObject.Find("CoinsGO").GetComponent<Text>();

        //RESET COINS
        intCoinsCollected = 0;

        if (PlayerPrefs.HasKey("coins") == true && PlayerPrefs.HasKey("score"))
        {
            int intCoins = PlayerPrefs.GetInt("coins");
            int intScore = PlayerPrefs.GetInt("score");

        }
    }

    void Update()
    {
        uiCoinsCollected.text = "" + intCoinsCollected;
        intCoinsGot = intCoinsCollected;
    }
    public static void AddCoins(int coinsToAdd)
    {
        intCoinsCollected += coinsToAdd;
    }
}
