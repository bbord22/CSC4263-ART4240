using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
	public Text killedBy;
	public Text lastTime;

	private GameObject shooter;
	private GameObject dasher;
	private string enemyName;

	void Start ()
	{
		if (SceneManager.GetActiveScene ().name == "GameOver") {
			enemyName = PlayerPrefs.GetString ("KilledBy", "");
			shooter = GameObject.Find ("Shooter (Game Over)");
			dasher = GameObject.Find ("Dasher (Game Over)");
			shooter.GetComponent<SpriteRenderer> ().enabled = false;
			dasher.GetComponent<SpriteRenderer> ().enabled = false;
			killedBy.text = "";
			lastTime.text = "Time Lasted: " + PlayerPrefs.GetString ("TimeLasted", "00:00");
			ShowKiller ();
		} else if (SceneManager.GetActiveScene ().name == "End") {
			lastTime.text = "Completion Time: " + PlayerPrefs.GetString ("LastTime", "00:00");
		}
	}

	void ShowKiller ()
	{
		if (enemyName == "Shooter") {
			shooter.GetComponent<SpriteRenderer> ().enabled = true;
			killedBy.text = "You were killed by the Shooter...";
		} else if (enemyName == "Dasher") {
			dasher.GetComponent<SpriteRenderer> ().enabled = true;
			killedBy.text = "You were killed by the Dasher...";
		}
	}

}
