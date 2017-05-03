using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour {
	
	public Text lastTime;
	public Text bestTime;
	public GameObject TimePanel;

	void Start ()
	{
		if (SceneManager.GetActiveScene ().name == "MainMenu") {
			TimePanel.SetActive (false);
		}
	}

	public void ShowTimes(){
		lastTime.text = PlayerPrefs.GetString ("LastTime", "00:00");
		bestTime.text = PlayerPrefs.GetString ("BestTime", "00:00");
		TimePanel.SetActive (true);
	}

	public void HideTimes(){
		TimePanel.SetActive (false);
	}
}
