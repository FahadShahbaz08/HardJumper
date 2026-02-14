using HardRunner.Managers;
using Solo.MOST_IN_ONE;
using UnityEngine;


namespace HardRunner.Economy
{
    public class Coin : MonoBehaviour
    {

        void Update()
        {
            Vector3 euler = this.transform.localEulerAngles;
            euler.y += 5f;
            this.transform.localEulerAngles = euler;

        }
        private void OnTriggerEnter(Collider other)
        {
            Prefs.Coins++;
            MOST_HapticFeedback.Generate(MOST_HapticFeedback.HapticTypes.LightImpact);
            gameObject.SetActive(false);
            GameEventManager.OnCoinCollected();
            AudioManager.Instance.PlayCoinPickSound();
        }
    }
}
