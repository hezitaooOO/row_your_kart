using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//https://www.youtube.com/watch?v=x-C95TuQtf0&ab_channel=N3KEN
public class TimerController : MonoBehaviour
{
    public Text timerText;
    private float startTime;
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        float t = Time.time - startTime;
        string minutes = ((int)t / 60).ToString();
        if (((int)t / 60) < 10) { 
            minutes = "0" + minutes;
        }
        //string digits = ((float)(t % 60 - Math.Truncate(t % 60))).ToString("f1");
        string seconds = (t % 60).ToString("f2");
        if ((t % 60) < 10) { 
            seconds = "0" + seconds;
        }
        string displayText = minutes + ":" + seconds;
        displayText = displayText.Replace('.', ':');
        timerText.text = "Time: " + displayText;
    }
}
