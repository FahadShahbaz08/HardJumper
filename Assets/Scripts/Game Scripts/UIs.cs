using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
public class UIs : MonoBehaviour
{
    public GameObject MainMenuUI;
    public GameObject restartMenuUI;
    public GameObject loadingPanelUI;

    public GameObject pauseMenuUIGO;
    public GameObject pauseGo;

    public Text InGameCoins;
 
    public string levelString;
    public string homeString;
    ScoreManager scoreManager;
    void Start()
    {
        restartMenuUI.SetActive(false);
        InGameCoins = GameObject.Find("CoinsGO").GetComponent<Text>();
        //pauseMenuUIGO = GameObject.Find("PauseMenu");
        //pauseGo = GameObject.Find("PauseGO");

        pauseMenuUIGO.SetActive(false);
        pauseGo.SetActive(false);

      
        scoreManager = FindObjectOfType<ScoreManager>();
        
    }



    public void startGame()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().playerActive = true;
        MainMenuUI.SetActive(false);
        pauseGo.SetActive(true);
    }


    public void restartGame()
    {
        SceneManager.LoadScene(levelString);
        restartMenuUI.SetActive(false);
  
    }

    public void homeMenu()
    {
        loadingPanelUI.SetActive(true);
        SceneManager.LoadScene(homeString);
    }

    public void showRestartMenu()
    {
        Camera.main.GetComponent<UIs>().restartMenuUI.SetActive(true);
        pauseGo.SetActive(false);
     }


    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenuUIGO.SetActive(true);
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        pauseMenuUIGO.SetActive(false);
    }

   

}
