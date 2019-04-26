using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CountDown : MonoBehaviour
{

    public float totalTimeInSeconds;
    public GameObject timerUI;
    public static CountDown instance = null;
    public bool timerisout = false;
    // Start is called before the first frame update


    void Awake()
    {

        if (instance == null)

            instance = this;
    }


    // Update is called once per frame
    void Update()
    {
        if (timerisout == false)
        {

            totalTimeInSeconds -= Time.deltaTime;
            UpdateLevelTimer(totalTimeInSeconds);
            if (totalTimeInSeconds < 1)
            {
                timerisout = true;
            }
        }
    }

    public void startTimer(int timeSeconds)
    {
        timerisout = false;
        totalTimeInSeconds = timeSeconds;
    }

    public void stopTimer()
    {
        timerisout = true;
    }
    public void UpdateLevelTimer(float totalSeconds)
    {
        int minutes = Mathf.FloorToInt(totalSeconds / 60f);
        int seconds = Mathf.RoundToInt(totalSeconds % 60f);

        string formatedSeconds = seconds.ToString();

        if (seconds == 60)
        {
            seconds = 0;
            minutes += 1;
        }

        GetComponent<Text>().text = minutes.ToString("00") + "." + seconds.ToString("00");
    }
}
