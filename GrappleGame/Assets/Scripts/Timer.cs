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
	public static bool countdownOver;
	public static bool stopTimer;
	public static bool playerKilled;

	void Start ()
	{
		timer = 0.0f;
		seconds = 0;
		minutes = 0;
		text = GetComponent<Text> ();
		text.text = "00:00";
		countdownOver = false;
		stopTimer = false;
		playerKilled = false;
	}

	void Update ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");

		if (player != null && countdownOver == true && stopTimer == false) {
			updateTime ();
			text.text = "" + TotalTime;
		} 
		if (stopTimer == true) {
			stopTimer = false;
			SaveCompletionTime ();
		}
		if (playerKilled == true) {
			SaveKilledTime ();
		}
	}

	void SaveCompletionTime ()
	{
		PlayerPrefs.SetString ("LastTime", TotalTime);
		PlayerPrefs.Save ();
		if (TotalTime.CompareTo(PlayerPrefs.GetString ("BestTime", "00:00")) == 1) {
			PlayerPrefs.SetString("BestTime", TotalTime);
			PlayerPrefs.Save ();
		}
		Debug.Log ("Last Time: " + PlayerPrefs.GetString ("LastTime", "00:00"));
		Debug.Log ("Best Time: " + PlayerPrefs.GetString ("BestTime", "00:00"));
	}

	void SaveKilledTime ()
	{
		PlayerPrefs.SetString ("TimeLasted", TotalTime);
		PlayerPrefs.Save ();
	}

	void updateTime ()
	{
		timer += Time.deltaTime;

		if (countdownOver == true) {

			if (timer >= 1f) {
				seconds++;
				timer -= 1f;
			}

			if (seconds == 60) {
				minutes++;
				seconds = 0;
			}

			if (seconds < 10 && minutes < 10) {
				TotalTime = "0" + minutes.ToString () + ":0" + seconds.ToString ();
			} else if (minutes < 10 && seconds >= 10) {
				TotalTime = "0" + minutes.ToString () + ":" + seconds.ToString ();
			} else if (minutes >= 10 && seconds < 10) {
				TotalTime = minutes.ToString () + ":0" + seconds.ToString ();
			} else {
				TotalTime = minutes.ToString () + ":" + seconds.ToString ();
			}

		}
	}
}
