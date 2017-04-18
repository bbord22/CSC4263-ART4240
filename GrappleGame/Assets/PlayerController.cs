// To make this script work, all of the Ground objects must have the tag "Ground"
// and all of the Wall objects must have the tag "Wall". Also, the Player object
// must have a rigidbody2D attached.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	private float moveSpeed;
	private float runSlideSpeed;
	private Vector3 jumpHeight;
	private Vector3 slideHeight;
	private Rigidbody2D rb;
	private bool canJump;
	private bool isWallJumping;
	private bool isWallSliding;
	private float oldHeight;
	private float currentHeight;
	private bool runningLeft;
	private bool runningRight;
	private double idleTime;

	void Start ()
	{
		Physics.defaultSolverIterations = 10;
		rb = gameObject.GetComponent<Rigidbody2D> ();
		jumpHeight = new Vector3 (0f, 27f, 0f);
		slideHeight = new Vector3 (0f, 12f, 0f);
		moveSpeed = 9f;
		runSlideSpeed = 4f;
		canJump = true;
		isWallJumping = false;
		isWallSliding = false;
		runningLeft = false;
		runningRight = false;
		currentHeight = gameObject.transform.position.y;
		oldHeight = currentHeight;
		idleTime = 0.35; // amount of standing still time (in seconds) that must pass before the player is considered idle
	}

	void FixedUpdate ()
	{
		if (isWallJumping == true) {
			moveSpeed = 7f; // affects the distance that the player can jump from a wall
		} else {
			moveSpeed = 9f; // affects the speed that the player moves around
		}
		if (Time.time >= idleTime) { // did this so the player won't slide while running after standing still for 0.35 seconds
			Debug.Log ("Player is idle");
			runningLeft = false; 
			runningRight = false;
		}
		transform.rotation = Quaternion.Euler (0, 0, 0); // stops rotation
		if (Input.GetKey ("a")) {
			if (runningRight == false) {
				transform.Translate (Vector3.left * moveSpeed * Time.deltaTime); // move to the left
				runningLeft = true;
			} else {
				StartCoroutine ("SlideRight"); // slide to the right before moving left
			}
			idleTime = Time.time + 0.35; // resets the idle time
		}

		if (Input.GetKey ("d")) {
			if (runningLeft == false) {
				transform.Translate (Vector3.right * moveSpeed * Time.deltaTime); // move to the right
				runningRight = true;
			} else {
				StartCoroutine ("SlideLeft"); // slide to the left before moving right
			}
			idleTime = Time.time + 0.35; // resets the idle time
		}
		if (Input.GetKey ("w") && canJump == true) {
			rb.AddForce (jumpHeight, ForceMode2D.Impulse); // jump
			canJump = false;
		}
		currentHeight = gameObject.transform.position.y; // did this so the jump won't look floaty
		if (currentHeight < oldHeight) { // if player is falling gravity is more
			rb.gravityScale = 3;
		} else { // if player is jumping gravity is less
			rb.gravityScale = 2;
		}
		oldHeight = currentHeight;
	}

	IEnumerator SlideRight(){
		transform.Translate (Vector3.right * runSlideSpeed * Time.deltaTime); // slide to the right a little
		yield return new WaitForSeconds (0.5f); // delay left movement
		runningRight = false;
	}

	IEnumerator SlideLeft(){
		transform.Translate (Vector3.left * runSlideSpeed * Time.deltaTime); // slide to the left a little
		yield return new WaitForSeconds (0.5f); // delay right movement
		runningLeft = false;
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		rb.gravityScale = 1;
		isWallJumping = false;
		if (other.gameObject.tag == "Ground") {
			canJump = true;
			isWallSliding = false;
			Debug.Log ("Touched Ground");
		}
		if (other.gameObject.tag == "Wall" && isWallSliding == false) {
			rb.AddForce (slideHeight, ForceMode2D.Impulse);
			canJump = false;
			isWallSliding = true;
			Debug.Log ("Wall Slide");
		}
		if (other.gameObject.name == "Finish Flag") {
			other.gameObject.GetComponent<BoxCollider2D> ().enabled = false;
			StartCoroutine ("Restart");
		}
	}

	void OnCollisionStay2D (Collision2D other)
	{
		if (other.gameObject.tag == "Wall" && Input.GetKey ("w") && isWallSliding == false) {
			isWallSliding = true;
			rb.gravityScale = 1;
			rb.AddForce (slideHeight, ForceMode2D.Impulse);
		}
	}

	void OnCollisionExit2D (Collision2D other)
	{
		isWallSliding = false;
		canJump = false;
		Debug.Log ("Left the " + other.gameObject.tag);
		if (other.gameObject.tag == "Wall") {
			isWallJumping = true;
			Debug.Log ("Wall Jump");
		}
	}

	IEnumerator Restart(){
		yield return new WaitForSeconds (2);
		SceneManager.LoadScene ("Mashup");
	}
}
