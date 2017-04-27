using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour {
	private PlayerController player;

	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController>();
	}
		
	void OnTriggerEnter2D()
	{
		player.canJump = true;
		player.isWallSliding = false;
		player.leftGround = false;
		//player._Acc = 0;
		player._Velocity = 0;
		player.anim.SetInteger ("State", 0);
		player.armAttached = true;
		player.isWallJumping = false;
	}

	void OnTriggerStay2D()
	{
		player.isTouchingGround = true;
		player.isWallSliding = false;
		player.canJump = true;
	}

	void OnTriggerExit2D()
	{
		player.isWallSliding = false;
		player.canJump = false;
		player.isTouchingGround = false;
		player.leftGround = true;
	}
}
