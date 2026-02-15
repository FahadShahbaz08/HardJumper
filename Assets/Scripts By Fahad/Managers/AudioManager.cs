using System.Collections;
using UnityEngine;

namespace HardRunner.Managers {
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [Header("Music / Sounds")]
        [SerializeField] AudioClip mainMenuBgMusic;
        [SerializeField] AudioClip jumpSound;
        [SerializeField] AudioClip coinPickSound;
        [SerializeField] AudioClip deathSound;
        [SerializeField] AudioClip[] gamePlaySound;

        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            musicSource.mute = !HardRunner.Economy.Prefs.MusicEnabled;
            sfxSource.mute = !HardRunner.Economy.Prefs.SfxEnabled;

            Invoke(nameof(PlayMainMenuMusic), 1f);
        }

        public void PlayMainMenuMusic()
        {
            StopMusic();
            if (musicSource.clip == mainMenuBgMusic && musicSource.isPlaying) return;

            musicSource.clip = mainMenuBgMusic;
            musicSource.loop = true;
            musicSource.Play();
        }

        public void StopMusic()
        {
            musicSource.Stop();
        }

        public void PlayJumpSound()
        {
            sfxSource.PlayOneShot(jumpSound);
        }

        public void PlayCoinPickSound()
        {
            sfxSource.PlayOneShot(coinPickSound);
        }

        public void PlayDeathSound()
        {
            sfxSource.PlayOneShot(deathSound);
        }

        public void PlayRandomGameplaySound()
        {
            StopAllCoroutines();
            StopMusic();

            StartCoroutine(PlayRandomLoop());
        }


        private IEnumerator PlayRandomLoop()
        {
            while (true)
            {
                AudioClip clip = gamePlaySound[Random.Range(0, gamePlaySound.Length)];
                sfxSource.PlayOneShot(clip);

                yield return new WaitForSeconds(clip.length);
            }
        }

        public void SetMusicEnabled(bool enabled)
        {
            musicSource.mute = !enabled;
            HardRunner.Economy.Prefs.MusicEnabled = enabled;
        }

        public void SetSfxEnabled(bool enabled)
        {
            sfxSource.mute = !enabled;
            HardRunner.Economy.Prefs.SfxEnabled = enabled;
        }

        public bool IsMusicEnabled()
        {
            return HardRunner.Economy.Prefs.MusicEnabled;
        }

        public bool IsSfxEnabled()
        {
            return HardRunner.Economy.Prefs.SfxEnabled;
        }


    }
}