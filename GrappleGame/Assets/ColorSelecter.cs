using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSelecter : MonoBehaviour
{
	SpriteRenderer gamerenderer;

	void start()
	{
		gamerenderer = gameObject.GetComponent<SpriteRenderer> ();
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.R)) 
		{
			gamerenderer.color = Color.red;
		}

		if (Input.GetKeyDown (KeyCode.B)) 
		{
			gamerenderer.color = Color.blue;
		}

		if (Input.GetKeyDown (KeyCode.G)) 
		{
			gamerenderer.color = Color.green;
		}
	}
}
