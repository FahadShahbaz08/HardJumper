using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SplashScreen : MonoBehaviour
{

    public Text startText;
    public int sceneIntIndex;
    float intCountdown = 0;


    public AudioClip coinClip;
    public AudioSource source;


    // Start is called before the first frame update
    void Start()
    {
        startText.enabled = false;
        StartCoroutine("playSound");
    }

    IEnumerator playSound()
    {
        yield return new WaitForSeconds(.5f);
        source.clip = coinClip;
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (intCountdown <= 8)
        {
            intCountdown += 5 * Time.deltaTime;

        }
        else
        {

            startText.enabled = true;
            StartGame();
      
        }
    }

    void StartGame()
    {
        if (Input.GetMouseButtonDown(0))
        {

            LoadMainGame();
        }
    }

    void LoadMainGame()
    {
        //LOAD THE GAME WITH THE SAVED PLAYERPREFS..
        AsyncOperation operation = SceneManager.LoadSceneAsync("MainMenu");
    }




}
