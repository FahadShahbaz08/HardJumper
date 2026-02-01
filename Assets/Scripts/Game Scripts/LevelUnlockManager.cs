using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelUnlockManager : MonoBehaviour
{
    [SerializeField]
    GameObject Level_1_1_ShowUIGo, Level_1_2_ShowUIGo, Level_1_3_ShowUIGo,
               Level_2_1_ShowUIGo, Level_2_2_ShowUIGo, Level_2_3_ShowUIGo,
               Level_3_1_ShowUIGo, Level_3_2_ShowUIGo, Level_3_3_ShowUIGo;

    //SHOW PLAY UI
    public GameObject level1_2_Prompt_GO, level1_3_Prompt_GO,
                      level2_1_Prompt_GO, level2_2_Prompt_GO, level2_3_Prompt_GO, 
                      level3_1_Prompt_GO, level3_2_Prompt_GO, level3_3_Prompt_GO;

    //UNLOCK BTNS
    public Text lvl1_2Btn, lvl1_3Btn,
                lvl2_1Btn, lvl2_2Btn, lvl2_3Btn, 
                lvl3_1Btn, lvl3_2Btn, lvl3_3Btn;

    //PLAY BTNS
    public Text Play1_2Btn, Play1_3Btn,
                Play2_1Btn, Play2_2Btn, Play2_3Btn,
                Play3_1Btn, Play3_2Btn, Play3_3Btn;

    public Text lvlTxt_1_2, lvlTxt_1_3,
                lvlTxt_2_1, lvlTxt_2_2, lvlTxt_2_3,
                lvlTxt_3_1, lvlTxt_3_2, lvlTxt_3_3;


    public Button[] levelButtons;

    //PLAYERPREFS INTLEVEL
    int intLvl1_2, intLvl1_3, 
        intLvl2_1, intLvl2_2, intLvl2_3, 
        intLvl3_1, intLvl3_2, intLvl3_3;

    public GameObject MainMenuGO_UI;
    public GameObject LevelSelectGO_UI;
    void Start()
    {

        LevelSelectGO_UI.GetComponent<Canvas>().enabled = false;



        //SHOW UI LEVEL DIALOG
        Level_1_1_ShowUIGo = GameObject.Find("Fields_UI_GO");
        Level_1_2_ShowUIGo = GameObject.Find("Fields_UI_GO_2");
        Level_1_3_ShowUIGo = GameObject.Find("Fields_UI_GO_3");


        Level_2_1_ShowUIGo = GameObject.Find("Suburbs_UI_GO");
        Level_2_2_ShowUIGo = GameObject.Find("Suburbs_UI_GO_2");
        Level_2_3_ShowUIGo = GameObject.Find("Suburbs_UI_GO_3");


        Level_3_1_ShowUIGo = GameObject.Find("Town_UI_GO_");
        Level_3_2_ShowUIGo = GameObject.Find("Town_UI_GO_2");
        Level_3_3_ShowUIGo = GameObject.Find("Town_UI_GO_3");


        //PROMPT UI DIALOG
        level1_2_Prompt_GO = GameObject.Find("Fields_1_2Prompt");
        level1_3_Prompt_GO = GameObject.Find("Fields_1_3Prompt");


        level2_1_Prompt_GO = GameObject.Find("Suburbs_1_1Prompt");
        level2_2_Prompt_GO = GameObject.Find("Suburbs_2_2Prompt");
        level2_3_Prompt_GO = GameObject.Find("Suburbs_2_3Prompt");


        level3_1_Prompt_GO = GameObject.Find("Town_1_1Prompt");
        level3_2_Prompt_GO = GameObject.Find("Town_1_2Prompt");
        level3_3_Prompt_GO = GameObject.Find("Town_1_3Prompt");


        Play1_2Btn.enabled = false;
        Play2_1Btn.enabled = false;

        //////////////////////////////////////////////////////////////////////////////////////////


        ////Level select 
        Level_1_1_ShowUIGo.SetActive(false);
        Level_1_2_ShowUIGo.SetActive(false);
        Level_1_3_ShowUIGo.SetActive(false);


        Level_2_1_ShowUIGo.SetActive(false);
        Level_2_2_ShowUIGo.SetActive(false);
        Level_2_3_ShowUIGo.SetActive(false);


        Level_3_1_ShowUIGo.SetActive(false);
        Level_3_2_ShowUIGo.SetActive(false);
        Level_3_3_ShowUIGo.SetActive(false);



        //PROMPT UI
        level1_2_Prompt_GO.SetActive(false);
        level1_3_Prompt_GO.SetActive(false);


        level2_1_Prompt_GO.SetActive(false);
        level2_2_Prompt_GO.SetActive(false);
        level2_3_Prompt_GO.SetActive(false);



        level3_1_Prompt_GO.SetActive(false);
        level3_2_Prompt_GO.SetActive(false);
        level3_3_Prompt_GO.SetActive(false);


        ///////////////////////////////////////////////////////////////////////



        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
                levelButtons[i].interactable = false;
        }


        Debug.Log("LEVEL REACHED " + PlayerPrefs.GetInt("levelReached"));

        intLvl1_2 = PlayerPrefs.GetInt("Level1_2");
        intLvl1_3 = PlayerPrefs.GetInt("Level1_3");

        intLvl2_1 = PlayerPrefs.GetInt("Level2_1");
        intLvl2_2 = PlayerPrefs.GetInt("Level2_2");
        intLvl2_3 = PlayerPrefs.GetInt("Level2_3");

        intLvl3_1 = PlayerPrefs.GetInt("Level3_1");
        intLvl3_2 = PlayerPrefs.GetInt("Level3_2");
        intLvl3_3 = PlayerPrefs.GetInt("Level3_3");


        //Debug.Log(intLvl1_2);

        //FIELDS 2
        //===========================================================
        if (intLvl1_2 == 0)
        {
            Play1_2Btn.enabled = false;

        }
        else
        {
            Play1_2Btn.enabled = true;
            lvl1_2Btn.enabled = false;
            lvlTxt_1_2.text = "This Level Is Available";
           
        }

        //FIELDS 3
        //===========================================================
        if (intLvl1_3 == 0)
        {
            Play1_3Btn.enabled = false;

        }
        else
        {
            Play1_3Btn.enabled = true;
            lvl1_3Btn.enabled = false;
            lvlTxt_1_3.text = "This Level Is Available";

        }



        //LEVEL SUBURBS 1
        //===========================================================
        if (intLvl2_1 == 0)
        {
            Play2_1Btn.enabled = false;
        }
        else
        {
            Play2_1Btn.enabled = true;
            lvl2_1Btn.enabled = false;
            lvlTxt_2_1.text = "This Level Is Available";
        }

        ////LEVEL SUBURBS 2
        //===========================================================
        if (intLvl2_2 == 0)
        {
            Play2_2Btn.enabled = false;
        }
        else
        {
            Play2_2Btn.enabled = true;
            lvl2_2Btn.enabled = false;
            lvlTxt_2_2.text = "This Level Is Available";
        }

        ////LEVEL SUBURBS 3
        //===========================================================
        if (intLvl2_3 == 0)
        {
            Play2_3Btn.enabled = false;
        }
        else
        {
            Play2_3Btn.enabled = true;
            lvl2_3Btn.enabled = false;
            lvlTxt_2_3.text = "This Level Is Available";
        }


        ////LEVEL town 1
        //===========================================================
        if (intLvl3_1 == 0)
        {
            Play3_1Btn.enabled = false;
        }
        else
        {
            Play3_1Btn.enabled = true;
            lvl3_1Btn.enabled = false;
            lvlTxt_3_1.text = "This Level Is Available";
        }

        ////LEVEL town 2
        //===========================================================
        if (intLvl3_2 == 0)
        {
            Play3_2Btn.enabled = false;
        }
        else
        {
            Play3_2Btn.enabled = true;
            lvl3_2Btn.enabled = false;
            lvlTxt_3_2.text = "This Level Is Available";
        }

        ////LEVEL town 3
        //===========================================================
        if (intLvl3_3 == 0)
        {
            Play3_3Btn.enabled = false;
        }
        else
        {
            Play3_3Btn.enabled = true;
            lvl3_3Btn.enabled = false;
            lvlTxt_3_3.text = "This Level Is Available";
        }



    }

    private void Update()
    {
        intLvl1_2 = PlayerPrefs.GetInt("Level1_2");
        intLvl1_3 = PlayerPrefs.GetInt("Level1_3");

        intLvl2_1 = PlayerPrefs.GetInt("Level2_1");
        intLvl2_2 = PlayerPrefs.GetInt("Level2_2");
        intLvl2_3 = PlayerPrefs.GetInt("Level2_3");


        intLvl3_1 = PlayerPrefs.GetInt("Level3_1");
        intLvl3_2 = PlayerPrefs.GetInt("Level3_2");
        intLvl3_3 = PlayerPrefs.GetInt("Level3_3");

        //LEVEL FIELD 2
        if (intLvl1_2 == 0)
        {
            Play1_2Btn.enabled = false;

        }
        else
        {
            Play1_2Btn.enabled = true;
            lvl1_2Btn.enabled = false;
            lvlTxt_1_2.text = "This Level Is Available";

        }


        //FIELDS 3
        //===========================================================
        if (intLvl1_3 == 0)
        {
            Play1_3Btn.enabled = false;

        }
        else
        {
            Play1_3Btn.enabled = true;
            lvl1_3Btn.enabled = false;
            lvlTxt_1_3.text = "This Level Is Available";

        }

        //LEVEL SUBURB 1
        if (intLvl2_1 == 0)
        {
            Play2_1Btn.enabled = false;
        }
        else
        {
            Play2_1Btn.enabled = true;
            lvl2_1Btn.enabled = false;
            lvlTxt_2_1.text = "This Level Is Available";
        }

        //LEVEL SUBURBS 2
        //===========================================================
        if (intLvl2_2 == 0)
        {
            Play2_2Btn.enabled = false;
        }
        else
        {
            Play2_2Btn.enabled = true;
            lvl2_2Btn.enabled = false;
            lvlTxt_2_2.text = "This Level Is Available";
        }

        ////LEVEL SUBURBS 3
        //===========================================================
        if (intLvl2_3 == 0)
        {
            Play2_3Btn.enabled = false;
        }
        else
        {
            Play2_3Btn.enabled = true;
            lvl2_3Btn.enabled = false;
            lvlTxt_2_3.text = "This Level Is Available";
        }


        ////LEVEL town 1
        //===========================================================
        if (intLvl3_1 == 0)
        {
            Play3_1Btn.enabled = false;
        }
        else
        {
            Play3_1Btn.enabled = true;
            lvl3_1Btn.enabled = false;
            lvlTxt_3_1.text = "This Level Is Available";
        }

        ////LEVEL town 2
        //===========================================================
        if (intLvl3_2 == 0)
        {
            Play3_2Btn.enabled = false;
        }
        else
        {
            Play3_2Btn.enabled = true;
            lvl3_2Btn.enabled = false;
            lvlTxt_3_2.text = "This Level Is Available";
        }

        ////LEVEL town 3
        //===========================================================
        if (intLvl3_3 == 0)
        {
            Play3_3Btn.enabled = false;
        }
        else
        {
            Play3_3Btn.enabled = true;
            lvl3_3Btn.enabled = false;
            lvlTxt_3_3.text = "This Level Is Available";
        }



    }

    public void ShowLevelSelect()
    {
 
        LevelSelectGO_UI.GetComponent<Canvas>().enabled = true;
        MainMenuGO_UI.GetComponent<Canvas>().enabled = false;
    }

    public void ShowMainMenu()
    {
        LevelSelectGO_UI.GetComponent<Canvas>().enabled = false;
        MainMenuGO_UI.GetComponent<Canvas>().enabled = true;
    }

    //FIELDS
    public void playLevel1_2()
    {

         SceneManager.LoadScene("Fields_med");
        
    }

    public void playLevel1_3()
    {

        SceneManager.LoadScene("Fields_med_2");

    }

    //SUBURBS
    public void playLevel2_1()
    {

        SceneManager.LoadScene("suburbs_easy");

    }

    public void playLevel2_2()
    {

        SceneManager.LoadScene("suburbs_med");

    }

    public void playLevel2_3()
    {

        SceneManager.LoadScene("suburbs_med_2");

    }

    //TOWN
    public void playLevel3_1()
    {

        SceneManager.LoadScene("town_easy");

    }


    public void playLevel3_2()
    {

        SceneManager.LoadScene("town_med");

    }

    public void playLevel3_3()
    {

        SceneManager.LoadScene("town_med_2");

    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
