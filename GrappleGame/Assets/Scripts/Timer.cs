using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float timer;
    private int seconds, minutes;
    private Text text;
    private string TotalTime;
    private GameObject player;

	void Start ()
    {
        timer = 0.0f;
        seconds = 0;
        minutes = 0;
        text = GetComponent<Text>();
	}
	
	void Update ()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if(player != null)
        {
            updateTime();
        }
        else
        {
            
        }

        text.text = "" + TotalTime;
    }

    void updateTime()
    {
        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            seconds++;
            timer -= 1f;
        }

        if (seconds == 60)
        {
            minutes++;
            seconds = 0;
        }

        if (seconds < 10 && minutes < 10)
        {
            TotalTime = "0" + minutes.ToString() + ":0" + seconds.ToString();
        }
        else if (minutes < 10 && seconds >= 10)
        {
            TotalTime = "0" + minutes.ToString() + ":" + seconds.ToString();
        }
        else if (minutes >= 10 && seconds < 10)
        {
            TotalTime = minutes.ToString() + ":0" + seconds.ToString();
        }
        else
        {
            TotalTime = minutes.ToString() + ":" + seconds.ToString();

        }
    }
}
