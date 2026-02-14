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
        private void OnEnable()
        {
            if (coinCount != null)
                coinCount.text = Prefs.Coins.ToString();

            if (coinContTMP != null)
                coinContTMP.text = Prefs.Coins.ToString();

            GameEventManager.OnCoinCollect += UpdateCoinText;
        }

        private void UpdateCoinText()
        {
            if(coinCount != null)
                coinCount.text = Prefs.Coins.ToString();

            if(coinContTMP != null ) 
                coinContTMP.text = Prefs.Coins.ToString();
        }

        private void OnDisable()
        {
            GameEventManager.OnCoinCollect -= UpdateCoinText;
        }
    }
} 