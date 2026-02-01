using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class startLevel : MonoBehaviour
{
    public string levelString;
    public string testLevel;
    public void Play()
    {
        SceneManager.LoadScene(testLevel);
    }
    public void QuitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
    public void LoadLevelViaLevelSelect()
    {
        SceneManager.LoadScene(levelString);
    }

}
