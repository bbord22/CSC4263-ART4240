using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenScript : MonoBehaviour {

	public float AggroRadius;
	private GameObject player;
	private Transform playerT;
	public Animator anim;
	public float distFromPlayer;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		playerT = player.transform;
		anim = this.GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {
		checkPlayerDist ();
	}

	void checkPlayerDist()
	{
		distFromPlayer = Vector2.Distance(playerT.position, transform.position);

		if(distFromPlayer < AggroRadius)
		{
			anim.SetInteger ("State", 1);
		}
	}

	void OnTriggerEnter2D()
	{
		UnityEngine.SceneManagement.SceneManager.LoadSceneAsync ("End", UnityEngine.SceneManagement.LoadSceneMode.Single);
	}
}
