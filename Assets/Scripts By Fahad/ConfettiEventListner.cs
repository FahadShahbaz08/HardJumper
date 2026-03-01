using HardRunner.Managers;
using UnityEngine;

public class ConfettiEventListner : MonoBehaviour
{
    [SerializeField] ParticleSystem[] particles;
    private void OnEnable()
    {
        GameEventManager.OnPlayerReachEnd += PlayParticle;
    }

    private void OnDisable()
    {
        GameEventManager.OnPlayerReachEnd -= PlayParticle;
    }

    private void PlayParticle()
    {
        AudioManager.Instance.PlayConffetiSound();
        foreach (var particle in particles)
        {
            particle.gameObject.SetActive(true);
            particle.Play();
        }
    }
}
