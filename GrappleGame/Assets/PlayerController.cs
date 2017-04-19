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
	private bool stationary;
	private bool isTouchingGround;

	public float _Velocity;
	// Current Travelling Velocity
	public float _MaxVelocity;
	// Maxima Velocity
	public float _Acc;
	// Current Acceleration
	public float _AccSpeed;
	// Amount to increase Acceleration with.
	public float _MaxAcc;
	// Max Acceleration
	public float _MinAcc;
	// Min Acceleration

	private bool zeroVelocity;

	//private double idleTime;

	void Start ()
	{
		Physics.defaultSolverIterations = 10;
		rb = gameObject.GetComponent<Rigidbody2D> ();
		jumpHeight = new Vector3 (0f, 27f, 0f);
		slideHeight = new Vector3 (0f, 12f, 0f);
		moveSpeed = 9f;
		runSlideSpeed = 6f;
		canJump = true;
		isWallJumping = false;
		isWallSliding = false;
		runningLeft = false;
		runningRight = false;
		currentHeight = gameObject.transform.position.y;
		oldHeight = currentHeight;
		zeroVelocity = false;
	}

	void FixedUpdate ()
	{
		if (isWallJumping == true) {
			moveSpeed = 7f; // affects the distance that the player can jump from a wall
		} else {
			moveSpeed = 9f; // affects the speed that the player moves around
		}
		transform.rotation = Quaternion.Euler (0, 0, 0); // stops rotation

		if (Input.anyKey) {
			zeroVelocity = false;
			if (Input.GetKey ("a")) {
				_Acc -= _AccSpeed;
			}

			if (Input.GetKey ("d")) {
				_Acc += _AccSpeed;
			}

			if (Input.GetKey ("w") && canJump == true) {
				rb.AddForce (jumpHeight, ForceMode2D.Impulse); // jump
				canJump = false;
			}
		} else {
			if (zeroVelocity == false) {
				if (_Velocity > -3.0f && _Velocity < 3.0f) {
					_Velocity = 0;
					_Acc = 0;
					zeroVelocity = true;
				} else if (_Velocity > 3.0f) {
					_Acc -= _AccSpeed;
				} else if (_Velocity < -3.0f) {
					_Acc += _AccSpeed;
				}


			}
		}
			
		currentHeight = gameObject.transform.position.y; // did this so the jump won't look floaty
		if (currentHeight < oldHeight) { // if player is falling gravity is more
			rb.gravityScale = 3;
		} else { // if player is jumping gravity is less
			rb.gravityScale = 2;
		}
		oldHeight = currentHeight;

		if (_Acc > _MaxAcc)
			_Acc = _MaxAcc;
		else if (_Acc < _MinAcc)
			_Acc = _MinAcc;

		_Velocity += _Acc;

		if (_Velocity > _MaxVelocity)
			_Velocity = _MaxVelocity;
		else if (_Velocity < -_MaxVelocity)
			_Velocity = -_MaxVelocity;

		transform.Translate (Vector3.right * _Velocity * Time.deltaTime);
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
		if (other.gameObject.tag == "Ground") {
			isTouchingGround = true;
		}
	}

	void OnCollisionExit2D (Collision2D other)
	{
		isWallSliding = false;
		canJump = false;
		isTouchingGround = false;
		Debug.Log ("Left the " + other.gameObject.tag);
		if (other.gameObject.tag == "Wall") {
			isWallJumping = true;
			Debug.Log ("Wall Jump");
		}

		if (other.gameObject.tag == "Ground") {
			canJump = false;
			isWallSliding = false;
			Debug.Log ("Left Ground");
		}
	}

	IEnumerator Restart ()
	{
		yield return new WaitForSeconds (2);
		SceneManager.LoadScene ("Mashup");
	}

	void slowDownRight ()
	{
		transform.Translate (Vector3.right * runSlideSpeed * Time.deltaTime);
	}
}
