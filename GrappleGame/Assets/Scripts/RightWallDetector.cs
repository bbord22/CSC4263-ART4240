using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightWallDetector : MonoBehaviour {

	private PlayerController player;

	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController>();
	}


	void OnTriggerEnter2D()
	{
		player.rb.gravityScale = 1;
		player.isWallJumping = false;
		if (player.isWallSliding == false && player.isPaused == false) 
		{
			if (player.isFalling == false) {
				//player._Acc = 0;
				//player._Velocity = 0;

			}

				player.rb.AddRelativeForce (Vector2.left * player.wallJumpForce, ForceMode2D.Impulse);


			Debug.Log ("Wall Slide");
		}
	}

	void OnTriggerStay2D()
	{
		player.isWallSliding = true;
		player.canJump = false;
		if (player.isPaused == false) 
		{
			if (Input.GetKey ("d")) 
			{
				player.wallGrabRight = true;
			}
			if (Input.GetKeyUp ("d")) 
			{
				player.wallGrabRight = false;
			}
		}
	}

	void OnTriggerExit2D()
	{
		if (Input.GetKey ("w") && player.isRising) {
			player.rb.AddForce (player.slideHeight, ForceMode2D.Impulse);
		}

		player.isWallSliding = false;
		player.isTouchingGround = false;

		player.wallGrabLeft = false;
		player.wallGrabRight = false;
		player.isWallJumping = true;
		if (Input.GetKeyDown ("w") && player.isPaused == false) {
			player.rb.AddForce (player.slideHeight, ForceMode2D.Impulse);

		}

		Debug.Log ("Wall Jump");
	}
}
