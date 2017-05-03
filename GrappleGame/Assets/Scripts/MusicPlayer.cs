/** 
 This script was written by Kristofer Oubre for the
 game Flee Bee © Kristofer Oubre which is available
 in the Apple App Store and Google Play Store. Some 
 functions will need to be tweaked so this script 
 can be useful in other games. Enjoy!
*/

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
	public AudioSource music;
	private static bool AudioBegin = false;

	void Start ()
	{
		music = GetComponent<AudioSource> ();
	}

	void Awake ()
	{
		if (!AudioBegin) {
			music.Play ();
			DontDestroyOnLoad (gameObject);
			AudioBegin = true;
		}
	}

	void Update ()
	{
		if (SceneManager.GetActiveScene ().name == "Game" || SceneManager.GetActiveScene ().name == "GameOver") {
			music.Stop ();
			AudioBegin = false;
		}
	}
}
