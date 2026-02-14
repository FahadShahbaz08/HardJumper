using UnityEngine;
using UnityEngine.SceneManagement;
namespace HardRunner.Managers
{
    public class GameSceneUiManager : MonoBehaviour
    {
        [SerializeField] GameObject gameoverPanel;
        [SerializeField] GameObject levelCompletePanel;
        [SerializeField] GameObject pausePanel;

        public void LevelComplete()
        {
            levelCompletePanel.SetActive(true);
        }

        public void GameOver()
        {
            gameoverPanel.SetActive(true);
        }

        public void Retry()
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        public void Home()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void EnablePausePanel()
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }

        public void Resume()
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }

        public void NextLevel()
        {
            HardRunner.Managers.LevelManager.CompleteLevel();
            HardRunner.Managers.LevelManager.NextLevel();

            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }


    }
}