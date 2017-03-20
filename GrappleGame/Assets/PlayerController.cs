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
	public Text levelComplete; // this text box is used only for the prototype demo
	private float moveSpeed;
	private Vector3 jumpHeight;
	private Vector3 slideHeight;
	private Rigidbody2D rb;
	private bool canJump;
	private bool isWallJumping;
	private bool isWallSliding;
	private float oldHeight;
	private float currentHeight;

	void Start ()
	{
		rb = gameObject.GetComponent<Rigidbody2D> ();
		jumpHeight = new Vector3 (0f, 10f, 0f);
		slideHeight = new Vector3 (0f, 10f, 0f);
		moveSpeed = 5f;
		canJump = true;
		isWallJumping = false;
		isWallSliding = false;
		currentHeight = gameObject.transform.position.y;
		oldHeight = currentHeight;
		levelComplete.text = "";
	}

	void Update ()
	{
		if (isWallJumping == true) {
			moveSpeed = 4f; // affects the distance that the player can jump from a wall
		} else {
			moveSpeed = 5f; // affects the speed that the player moves around
		}
		transform.rotation = Quaternion.Euler (0, 0, 0); // stops rotation
		if (Input.GetKey ("a")) {
			transform.Translate (Vector3.left * moveSpeed * Time.deltaTime); // move to the left
		}
		if (Input.GetKey ("d")) {
			transform.Translate (Vector3.right * moveSpeed * Time.deltaTime); // move to the right
		}
		if (Input.GetKey ("w") && canJump == true) {
			rb.AddForce (jumpHeight, ForceMode2D.Impulse); // jump
			canJump = false;
		}
		currentHeight = gameObject.transform.position.y; // did this so the jump won't look floaty
		if (currentHeight < oldHeight) { // if player is falling gravity is less
			rb.gravityScale = 1;
		} else { // if player is jumping gravity is more
			rb.gravityScale = 2;
		}
		oldHeight = currentHeight;
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
			levelComplete.text = "Level Complete";
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
		SceneManager.LoadScene ("Player Movement Prototype");
	}
}
