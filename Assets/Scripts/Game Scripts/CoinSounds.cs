using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSounds : MonoBehaviour
{
    public  AudioClip coinClip;
    public  AudioSource source;

    public AudioClip jumpClip;
    public AudioClip deathClip;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public  void getCoinSound()
    {
        source.clip = coinClip;
        source.Play();
       
    }

    public void jumpSound()
    {
        source.clip = jumpClip;
        source.Play();
        //Debug.Log("READ");
    }

    public void deathSound()
    {
        source.clip = deathClip;
        source.Play();

    }

}
