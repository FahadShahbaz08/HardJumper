using System.Collections;
using UnityEngine;

namespace HardRunner.Managers
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [Header("Music")]
        [SerializeField] AudioClip mainMenuBgMusic;
        [SerializeField] AudioClip[] gamePlayMusic;

        [Header("SFX")]
        [SerializeField] AudioClip jumpSound;
        [SerializeField] AudioClip coinPickSound;
        [SerializeField] AudioClip deathSound;

        [Header("Sources")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;

        private Coroutine gameplayMusicRoutine;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            ApplySettings();

            if (IsMusicEnabled())
                Invoke(nameof(PlayMainMenuMusic), 1f);
        }

        void ApplySettings()
        {
            musicSource.mute = !HardRunner.Economy.Prefs.MusicEnabled;
            sfxSource.mute = !HardRunner.Economy.Prefs.SfxEnabled;
        }

        // ================= MUSIC =================

        public void PlayMainMenuMusic()
        {
            if (!IsMusicEnabled()) return;

            StopGameplayMusic();

            if (musicSource.clip == mainMenuBgMusic && musicSource.isPlaying)
                return;

            musicSource.clip = mainMenuBgMusic;
            musicSource.loop = true;
            musicSource.Play();
        }

        public void PlayGameplayMusic()
        {
            if (!IsMusicEnabled()) return;

            StopGameplayMusic();

            gameplayMusicRoutine = StartCoroutine(GameplayMusicLoop());
        }

        IEnumerator GameplayMusicLoop()
        {
            while (IsMusicEnabled())
            {
                AudioClip clip = gamePlayMusic[Random.Range(0, gamePlayMusic.Length)];

                musicSource.clip = clip;
                musicSource.loop = false;
                musicSource.Play();

                yield return new WaitForSeconds(clip.length);
            }
        }

        public void StopGameplayMusic()
        {
            if (gameplayMusicRoutine != null)
            {
                StopCoroutine(gameplayMusicRoutine);
                gameplayMusicRoutine = null;
            }

            musicSource.Stop();
        }

        public void StopMusic()
        {
            StopGameplayMusic();
        }

        // ================= SFX =================

        public void PlayJumpSound()
        {
            if (!IsSfxEnabled()) return;
            sfxSource.PlayOneShot(jumpSound);
        }

        public void PlayCoinPickSound()
        {
            if (!IsSfxEnabled()) return;
            sfxSource.PlayOneShot(coinPickSound);
        }

        public void PlayDeathSound()
        {
            if (!IsSfxEnabled()) return;
            sfxSource.PlayOneShot(deathSound);
        }

        // ================= SETTINGS =================

        public void SetMusicEnabled(bool enabled)
        {
            HardRunner.Economy.Prefs.MusicEnabled = enabled;
            musicSource.mute = !enabled;

            if (!enabled)
                StopGameplayMusic();
        }

        public void SetSfxEnabled(bool enabled)
        {
            HardRunner.Economy.Prefs.SfxEnabled = enabled;
            sfxSource.mute = !enabled;
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
