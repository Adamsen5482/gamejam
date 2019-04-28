using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audiomanager : MonoBehaviour
{


    // Start is called before the first frame update


    
    
    public AudioSource gamebegin;
  

    public AudioSource menutrack;
    public AudioSource buttonClick;
    public AudioSource TypeWriter;
    public AudioSource GhostWinTrack;
    public AudioSource FinalVote;
    public static Audiomanager instance = null;     //Allows other scripts to call functions from SoundManager.             



    void Awake()
    {

        if (instance == null)

            instance = this;

        else if (instance != this)

            Destroy(gameObject);


        DontDestroyOnLoad(gameObject);
    }


    public void playButtonClick()
    {
        buttonClick.Play();
    }
    public void playFinalVote()
    {
        menutrack.Stop();
        FinalVote.Play();
    }
    public void PlayGameBegin()
    {
        gamebegin.Play();
    }

    public void PlayMenutrack()
    {
        menutrack.Play();
    }

    public void PlayTypeWriter()
    {
        menutrack.Pause();
        if (!TypeWriter.isPlaying)
        {
            TypeWriter.Play();
        }
       
    }

    public void UnpauseMenuTrack()
    {
        menutrack.UnPause();
        
    }
    public void PlayGhostWinTrack()
    {
        GhostWinTrack.Play();
    }
}
