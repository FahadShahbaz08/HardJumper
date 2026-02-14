using UnityEngine;
using UnityEngine.UI;
using HardRunner.Managers;

namespace HardRunner.UI
{
    public class AudioSettingsUI : MonoBehaviour
    {
        [Header("Music Objects")]
        [SerializeField] GameObject musicOnObject;
        [SerializeField] GameObject musicOffObject;

        [Header("SFX Objects")]
        [SerializeField] GameObject sfxOnObject;
        [SerializeField] GameObject sfxOffObject;

        private Button musicOnButton;
        private Button musicOffButton;
        private Button sfxOnButton;
        private Button sfxOffButton;

        private void Awake()
        {
            // get buttons
            musicOnButton = musicOnObject.GetComponent<Button>();
            musicOffButton = musicOffObject.GetComponent<Button>();
            sfxOnButton = sfxOnObject.GetComponent<Button>();
            sfxOffButton = sfxOffObject.GetComponent<Button>();

            // remove old listeners (safety)
            musicOnButton.onClick.RemoveAllListeners();
            musicOffButton.onClick.RemoveAllListeners();
            sfxOnButton.onClick.RemoveAllListeners();
            sfxOffButton.onClick.RemoveAllListeners();

            // add listeners
            musicOnButton.onClick.AddListener(DisableMusic);
            musicOffButton.onClick.AddListener(EnableMusic);

            sfxOnButton.onClick.AddListener(DisableSfx);
            sfxOffButton.onClick.AddListener(EnableSfx);
        }

        private void Start()
        {
            RefreshUI();
        }

        private void OnEnable()
        {
            // refresh when scene reloads
            RefreshUI();
        }

        public void EnableMusic()
        {
            if (AudioManager.Instance == null) return;

            AudioManager.Instance.SetMusicEnabled(true);
            RefreshUI();
        }

        public void DisableMusic()
        {
            if (AudioManager.Instance == null) return;

            AudioManager.Instance.SetMusicEnabled(false);
            RefreshUI();
        }

        public void EnableSfx()
        {
            if (AudioManager.Instance == null) return;

            AudioManager.Instance.SetSfxEnabled(true);
            RefreshUI();
        }

        public void DisableSfx()
        {
            if (AudioManager.Instance == null) return;

            AudioManager.Instance.SetSfxEnabled(false);
            RefreshUI();
        }

        private void RefreshUI()
        {
            if (AudioManager.Instance == null) return;

            bool musicEnabled = AudioManager.Instance.IsMusicEnabled();
            bool sfxEnabled = AudioManager.Instance.IsSfxEnabled();

            musicOnObject.SetActive(musicEnabled);
            musicOffObject.SetActive(!musicEnabled);

            sfxOnObject.SetActive(sfxEnabled);
            sfxOffObject.SetActive(!sfxEnabled);
        }
    }
}
