using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] clips;
    private AudioSource audioSource;


    public    GameObject soundButtons;

    //public GameObject[] soundButtons;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
        audioSource.loop = false;


        audioSource = GetComponent<AudioSource>();


    
        updateSound();


    }

    private AudioClip getRandomClip()
    {
        return clips[Random.Range(0, clips.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = getRandomClip();
            audioSource.Play();
        }
    }

    //public void turnOffSound()
    //{
    //    if (PlayerPrefs.HasKey("soundoff") && PlayerPrefs.HasKey("soundon"))
    //    {
    //        int intOn = PlayerPrefs.GetInt("soundon");
    //        int intOff = PlayerPrefs.GetInt("soundoff");

    //        PlayerPrefs.SetInt("soundon", 0);
    //        PlayerPrefs.SetInt("soundoff", 1);

    //        if (PlayerPrefs.GetInt("sound") == 1)
    //        {

    //        }

    //    }
    //    else
    //    {

    //    }
    //}

    public void turnOnOffSound()
    {
        if (PlayerPrefs.GetInt("sounds") == 1)
        {
            PlayerPrefs.SetInt("sounds", 0);
            PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.SetInt("sounds", 1);
            PlayerPrefs.Save();
        }
        updateSound();

    }

    public void updateSound()
    {
        //soundButtons = GameObject.FindGameObjectWithTag("Soundtg");

        if (PlayerPrefs.GetInt("sounds") == 0)
        {
          

            soundButtons.GetComponentInChildren<Text>().text = "Music On";

            audioSource.mute = false;
            //characterSpeaker.mute = false;
            //characterSpeaker2.mute = false;
            //coinSpeaker.mute = false;
            //gameSpeaker.mute = false;
        }
        if (PlayerPrefs.GetInt("sounds") == 1)
        {

            soundButtons.GetComponentInChildren<Text>().text = "Music Off";

            audioSource.mute = true;
            //characterSpeaker.mute = true;
            //characterSpeaker2.mute = true;
            //coinSpeaker.mute = true;
            //gameSpeaker.mute = true;
        }
    }

}
