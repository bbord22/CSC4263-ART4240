using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {
	private bool gamePaused;

	void Start () {
		gamePaused = false;
	}

	public void PauseGame(){
		if (gamePaused == false) {
			Time.timeScale = 0;
			gamePaused = true;
			Debug.Log ("Game Paused");
		}else{
			Time.timeScale = 1;
			gamePaused = false;
			Debug.Log ("Game Resumed");
		}
	}
}
