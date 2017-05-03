/** 
 This script was written by Kristofer Oubre for the
 game Flee Bee © Kristofer Oubre which is available
 in the Apple App Store and Google Play Store. Some 
 functions will need to be tweaked so this script 
 can be useful in other games. Enjoy!
*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Countdown : MonoBehaviour
{
	public static float volume = 1;
	public static int time;
	public AudioClip CountSound;
	public Text countdown;

	protected GUIStyle style = new GUIStyle ();

	// Use this for initialization
	void Start ()
	{
		time = 3;
		StartCoroutine ("PlayTimer");
	}

	private IEnumerator PlayTimer ()
	{
		while (true) {
			if (time != 0) {
				AudioSource.PlayClipAtPoint (CountSound, transform.position, 1);
			}
			yield return new WaitForSeconds (1);
			time -= 1;
			countdown.text = time + "";
			if (time == 0) {
				Timer.countdownOver = true;
				PlayerController.countdownOver = true;
				countdown.enabled = false;
				Destroy (gameObject);
			}
		}
	}
}
