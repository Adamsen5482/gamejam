using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class CountDown : MonoBehaviour
{
    public float totalTimeInSeconds = 10;
    public Text timerUI;
    public bool timerisout = true;
    public bool startRunningOnEnable = false;


    private float remainingTime;

    public UnityEvent onTimeOut;

    private void OnEnable()
    {
        if (this.startRunningOnEnable)
        {
            this.startTimer(this.totalTimeInSeconds);
        }
    }

    void Update()
    {
        if (timerisout == false)
        {

            this.remainingTime -= Time.deltaTime;
            UpdateLevelTimer(this.remainingTime);
            if (remainingTime < 0)
            {
                timerisout = true;
                this.onTimeOut.Invoke();
            }
        }
    }

    public void startTimer(float timeSeconds)
    {
        timerisout = false;
        this.remainingTime = timeSeconds;
    }

    public void stopTimer()
    {
        timerisout = true;
    }
    public void UpdateLevelTimer(float remainingTime)
    {
        int minutes;
        int seconds;

        if (remainingTime > 0)
        {
            minutes = Mathf.FloorToInt(remainingTime / 60f);
            seconds = Mathf.RoundToInt(remainingTime % 60f);

            if (seconds == 60)
            {
                seconds = 0;
                minutes += 1;
            }
        }
        else
        {
            minutes = 0;
            seconds = 0;
        }

        this.timerUI.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
