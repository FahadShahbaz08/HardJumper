using HardRunner.Economy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace HardRunner.UI
{
    public class CoinUiText : MonoBehaviour
    {
        [SerializeField] Text coinCount;
        [SerializeField] TextMeshProUGUI coinContTMP;
        [SerializeField] bool isInGamePlay = true;
        private int currentCoins = 0;
        [SerializeField] private bool isGameOver = false;
        private void OnEnable()
        {
            if (!isInGamePlay)
            {


                if (coinCount != null)
                    coinCount.text = Prefs.Coins.ToString();

                if (coinContTMP != null)
                    coinContTMP.text = Prefs.Coins.ToString();

            }

            GameEventManager.OnCoinCollect += UpdateCoinText;
            Player.CoinsCollecd += UpdateCoinTextFromPlayer;
            print("Coin Event attahed");

            if (isGameOver)
            {
                currentCoins = Player.CurrentRunCoins;

                if (coinCount != null)
                    coinCount.text = currentCoins.ToString();

                if (coinContTMP != null)
                    coinContTMP.text = currentCoins.ToString();
            }

        }
        private void UpdateCoinText()
        {
            if (!isInGamePlay)
            {


                Debug.Log("UpdateCoinText called. Coins = " + Prefs.Coins);

                if (coinCount != null)
                    coinCount.text = Prefs.Coins.ToString();

                if (coinContTMP != null)
                    coinContTMP.text = Prefs.Coins.ToString();

            }
        }
        private void UpdateCoinTextFromPlayer(int score)
        {
            currentCoins = score;
            Debug.Log("UpdateCoinText called. Coins = " + score);

            if (coinCount != null)
                coinCount.text = score.ToString();

            if (coinContTMP != null)
                coinContTMP.text = score.ToString();
        }
        private void OnDisable()
        {
            GameEventManager.OnCoinCollect -= UpdateCoinText;
            Player.CoinsCollecd -= UpdateCoinTextFromPlayer;
        }
    }
} 