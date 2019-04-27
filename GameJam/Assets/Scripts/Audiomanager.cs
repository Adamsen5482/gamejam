using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audiomanager : MonoBehaviour
{


    // Start is called before the first frame update


    
    
    public AudioSource gamebegin;
  

    public AudioSource menutrack;
    public AudioSource alert;
    public AudioSource MurderWinTrack;
    public AudioSource GhostWinTrack;
    public static Audiomanager instance = null;     //Allows other scripts to call functions from SoundManager.             



    void Awake()
    {

        if (instance == null)

            instance = this;

        else if (instance != this)

            Destroy(gameObject);


        DontDestroyOnLoad(gameObject);
    }


 
    public void PlayGameBegin()
    {
        gamebegin.Play();
    }

    public void PlayMenutrack()
    {
        menutrack.Play();
    }
    public void PlayAlert()
    {
        alert.Play();
    }
    public void PlayMurderWinTrack()
    {
        MurderWinTrack.Play();
    }
    public void PlayGhostWinTrack()
    {
        GhostWinTrack.Play();
    }
}
