using UnityEngine;
using HardRunner.Economy;
using System.Collections.Generic;
using HardRunner.Scriptable;
using System.Security.Cryptography;
using TMPro;
using UnityEngine.SceneManagement;

namespace HardRunner.UI
{
    public class UiManager : MonoBehaviour
    {
        [Header("Required Ui Panels")]
        [SerializeField] GameObject mainMenuPanel;
        [SerializeField] GameObject environementPanel;
        [SerializeField] GameObject leaderBoardPanel;
        [SerializeField] GameObject exitGamePanel;
        [SerializeField] GameObject settingsPanel;

        [Header("Environement Selection Ui Components")]
        [SerializeField] TextMeshProUGUI envPanelHeaderText;
        [SerializeField] EnvironmentItem envItem;
        [SerializeField] LevelItem levelItem;
        [SerializeField] Transform evnScrollviewContent;
        [SerializeField] Transform levelScrollViewContent;
        [SerializeField] GameObject envScrollViewGo;
        [SerializeField] GameObject levelScrollViewGo;
        [SerializeField] List<EnvironementItemScriptable> envItemScriptables;

        EnvironementItemScriptable currentEnv;
        public void ShowEnvPanel()
        {
            environementPanel.SetActive(true);
            mainMenuPanel.SetActive(false);
            levelScrollViewGo.SetActive(false);
            envScrollViewGo.SetActive(true);
            SetupEnvScrollViewContent();
            envPanelHeaderText.text = "Choose Map";
        }
        public void ShowLeaderBoard()
        {
            leaderBoardPanel.SetActive(true);
        }

        public void ExitGame()
        {
            exitGamePanel.SetActive(true);
        }

        public void ShowSettingsPanel()
        {
            settingsPanel.SetActive(true);
        }
        
        public void ExitGameApp()
        {
            Application.Quit();
        }


        private void SetupEnvScrollViewContent()
        {
            foreach(Transform item in evnScrollviewContent)
            {
                Destroy(item.gameObject);
            }

            foreach (var item in envItemScriptables)
            {
                GameObject obj = Instantiate(envItem.gameObject, evnScrollviewContent);
                EnvironmentItem newItem = obj.GetComponent<EnvironmentItem>();

                newItem.bgImage.sprite = item.environmentImage;
                newItem.categoryNameText.text = item.environmentCategory;

                newItem.btn.onClick.AddListener(() =>
                {
                    currentEnv = item;
                    OpenLevelsPanel();
                });
            }
        }

        private void OpenLevelsPanel()
        {
            envPanelHeaderText.text = "Choose Level";
            envScrollViewGo.SetActive(false);
            levelScrollViewGo.SetActive(true);

            foreach(Transform item in levelScrollViewContent)
            {
                Destroy(item?.gameObject);
            }

            int unlocked = Prefs.GetUnlockedLevels(currentEnv.environmentCategory);

            for (int i = 1; i <= currentEnv.maxLevels; i++)
            {
                GameObject obj = Instantiate(levelItem.gameObject, levelScrollViewContent);
                LevelItem li = obj.GetComponent<LevelItem>();

                li.levelNumber.text = "Level "+i.ToString();

                bool isUnlocked = i <= unlocked;
                li.lockImage.SetActive(!isUnlocked);
                li.btn.interactable = isUnlocked;

                int levelIndex = i;
                li.btn.onClick.AddListener(() =>
                {
                    HardRunner.Managers.LevelManager.SetLevel(currentEnv.environmentCategory, levelIndex);
                    SceneManager.LoadScene("GameScene");
                });
            }
        }
    }
}