using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
	public Text killedBy;
	public Text lastTime;

	private GameObject shooter;
	private GameObject dasher;
	private string enemyName;

	void Start ()
	{
		enemyName = PlayerPrefs.GetString ("KilledBy", "");
		shooter = GameObject.Find ("Shooter (Game Over)");
		dasher = GameObject.Find ("Dasher (Game Over)");
		shooter.GetComponent<SpriteRenderer> ().enabled = false;
		dasher.GetComponent<SpriteRenderer> ().enabled = false;
		killedBy.text = "";
		lastTime.text = "Time Lasted: " + PlayerPrefs.GetString ("TimeLasted", "00:00");
		ShowKiller ();
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
