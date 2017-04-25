using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {
	public GameObject pausePanel;
	private static bool gamePaused = false;

	void Start () {
		Time.timeScale = 1;
		gamePaused = false;
		pausePanel.SetActive (false);
	}

	void Update(){
		if (Input.GetKeyDown ("p")) {
			PauseGame ();
		}
	}

	public void PauseGame(){
		if (!gamePaused) {
			pausePanel.SetActive (true);
			gamePaused = true;
			Debug.Log ("Game Paused");
			Time.timeScale = 0;
		}else{
			Time.timeScale = 1;
			pausePanel.SetActive (false);
			gamePaused = false;
			Debug.Log ("Game Resumed");
		}
	}

	void OnDisable(){
		Time.timeScale = 1;
		gamePaused = false;
		pausePanel.SetActive (false);
	}
}
