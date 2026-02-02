using UnityEngine;
using Fahad.Economy;

namespace Fahad.UI
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] GameObject mainMenuPanel;
        [SerializeField] GameObject environementPanel;
        [SerializeField] GameObject leaderBoardPanel;
        [SerializeField] GameObject exitGamePanel;
        [SerializeField] GameObject settingsPanel;
        public void ShowEnvPanel()
        {
            environementPanel.SetActive(true);
            mainMenuPanel.SetActive(false);
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
    }
}