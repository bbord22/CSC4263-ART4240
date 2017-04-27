using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour {

	public PlayerController player;
	public Animator anime;

	void Start () {
		player = GameObject.Find("Player").GetComponent<PlayerController>();
		anime = this.GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update ()
	{
		
		/*if ((Input.GetKey ("a") || player.wallGrabRight || player.isTouchingGround == false && !player.isWallSliding) && player.arm.transform.position.z != -1) {
			player.arm.transform.Translate (Vector3.back);
		} else if ((Input.GetKey ("d") || player.wallGrabLeft) && player.arm.transform.position.z != 0) {
			player.arm.transform.Translate (Vector3.forward);
		}*/

		if (player.isTouchingGround == false && !player.isWallSliding || player.isTouchingGround == false && !player.isWallSliding) {
			anime.SetInteger ("State", 3);
			player.armAttached = true;
		}
		if ((player.isTouchingGround == false && Input.GetKey ("a") && !player.isWallSliding))
			anime.SetInteger ("State", 4);
		player.armAttached = true;

		if (player.isTouchingGround && Input.GetKey("d") && !player.isWallSliding) 
		{
			anime.SetInteger ("State", 1);
			player.armAttached = true;
		}
		if (player.isTouchingGround && Input.GetKey("a") && !player.isWallSliding) 
		{
			anime.SetInteger ("State", 2);
			player.armAttached = true;
		}
		if (player.isTouchingGround && player.stationaryX && !player.isWallSliding) 
		{
			anime.SetInteger ("State", 0);
			player.armAttached = false;
		}

		if (player.isWallSliding && player.wallGrabRight) 
		{
			anime.SetInteger ("State", 5);
			player.armAttached = true;
		}
		if (player.isWallSliding && player.wallGrabLeft) 
		{
			anime.SetInteger ("State", 6);
			player.armAttached = true;
		}

	}

}
