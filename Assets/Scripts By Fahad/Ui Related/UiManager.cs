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
        [SerializeField] SplashPanel splashPanel;

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

        private void Start()
        {
            if (envItemScriptables.Count > 0)
            {
                string firstEnv = envItemScriptables[0].environmentCategory;
                Prefs.UnlockEnvironment(firstEnv);
            }
        }

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
            foreach (Transform item in evnScrollviewContent)
            {
                Destroy(item.gameObject);
            }

            for (int index = 0; index < envItemScriptables.Count; index++)
            {
                var item = envItemScriptables[index];

                GameObject obj = Instantiate(envItem.gameObject, evnScrollviewContent);
                EnvironmentItem newItem = obj.GetComponent<EnvironmentItem>();

                newItem.bgImage.sprite = item.environmentImage;
                newItem.categoryNameText.text = item.environmentCategory;

                bool isFirstEnv = index == 0;
                bool isUnlocked = isFirstEnv || Prefs.IsEnvironmentUnlocked(item.environmentCategory);

                newItem.lockImage.SetActive(!isUnlocked);
                newItem.unlockCostText.text = item.unlockCost.ToString();
                newItem.btn.interactable = true;

                newItem.btn.onClick.RemoveAllListeners();

                if (isUnlocked)
                {
                    newItem.btn.onClick.AddListener(() =>
                    {
                        currentEnv = item;
                        OpenLevelsPanel();
                    });
                }
                else
                {
                    newItem.btn.onClick.AddListener(() =>
                    {
                        TryUnlockEnvironment(item);
                    });
                }
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
                    //SceneManager.LoadScene(currentEnv.sceneName);
                    splashPanel.gameObject.SetActive(true);
                    splashPanel.LoadSceneByName(currentEnv.sceneName);
                });
            }
        }

        private void TryUnlockEnvironment(EnvironementItemScriptable env)
        {
            if (Prefs.Coins >= env.unlockCost)
            {
                Prefs.Coins -= env.unlockCost;
                Prefs.UnlockEnvironment(env.environmentCategory);

                SetupEnvScrollViewContent(); // Refresh UI
            }
            else
            {
                Debug.Log("Not enough coins!");
            }
        }

    }
}