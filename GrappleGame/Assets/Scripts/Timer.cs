using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float timer;
    private int seconds, minutes;
    private Text text;

	void Start ()
    {
        timer = 0.0f;
        seconds = 0;
        minutes = 0;
        text = GetComponent<Text>();
	}
	
	void Update ()
    {
        timer += Time.deltaTime;
        
        if(timer >= 1f)
        {
            seconds++;
            timer -= 1f;
        }

        if (seconds == 60)
        {
            minutes++;
            seconds = 0;
        }

        if(minutes == 0)
        {
            text.text = "Time: " + seconds.ToString() + "s";
        }
        else
        {
            text.text = "Time: " + minutes.ToString() + "m:" + seconds.ToString() + "s";
        }
    }
}
