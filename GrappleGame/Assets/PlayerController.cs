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
	private Vector2 dir;
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
	public bool isRising = false;
	public bool isFalling = true;
	private bool wallGrabRight = false;
	private bool wallGrabLeft = false;
	private float wallJumpForce;
	private bool armAttached = true;
	public float maxSlideSpeed;
	public GameObject arm;
	private bool isPlaying = false;



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

	private bool normalMovement;
	private bool leftGround;

    private bool isPaused = false;
    public GameObject pausePanel;

	void Start ()
	{
		Physics.defaultSolverIterations = 10;
		rb = gameObject.GetComponent<Rigidbody2D> ();
		jumpHeight = new Vector3 (0f, 14f, 0f);
		slideHeight = new Vector3 (0f, 10.5f, 0f);
		moveSpeed = 9f;
		runSlideSpeed = 6f;
		maxSlideSpeed = 2;
		wallJumpForce = 10;
		canJump = true;
		isWallJumping = false;
		isWallSliding = false;
		runningLeft = false;
		runningRight = false;
		currentHeight = gameObject.transform.position.y;
		oldHeight = currentHeight;
		zeroVelocity = false;
		arm = GameObject.FindGameObjectWithTag ("Arm");
        pausePanel.SetActive(false);
	}

	void Update()
	{
        if(Input.GetKeyDown("p"))
        {
            pause();
        }

        if (!isPaused)
        {
            if (Input.GetKeyDown("e") && armAttached == false)
            {
                armAttached = true;
            }
            else if (Input.GetKeyDown("e") && armAttached == true)
            {
                armAttached = false;
            }

            if (armAttached)
            {
                arm.SetActive(true);
                gameObject.GetComponent<HingeJoint2D>().enabled = true;
                gameObject.GetComponent<ArmCursorFollow>().enabled = true;
            }
            if (!armAttached)
            {
                gameObject.GetComponent<HingeJoint2D>().enabled = false;
                gameObject.GetComponent<ArmCursorFollow>().enabled = false;
                arm.SetActive(false);
            }
            if (normalMovement == true)
            {
                if (Input.GetKeyDown("w") && canJump == true)
                {
                    rb.AddForce(jumpHeight, ForceMode2D.Impulse); // jump
                    canJump = false;
                }
                if ((runningLeft || runningRight) && isPlaying == false)
                {
                    GetComponent<AudioSource>().Play();

                    isPlaying = true;
                }

                else if (!(runningLeft || runningRight) && isPlaying == true)
                {
                    GetComponent<AudioSource>().Stop();
                    isPlaying = false;
                }
            }
            else
            {
                isPlaying = false;
                GetComponent<AudioSource>().Stop();
            }
        }
	}

	void FixedUpdate ()
	{
		if (leftGround == true)
		{
			normalMovement = false;
		}
		else
		{
			normalMovement = true;
		}

		if (isFalling && isWallSliding && (wallGrabLeft || wallGrabRight))
		{
			rb.velocity = rb.velocity.normalized * maxSlideSpeed;
		}

		if (isWallJumping == true) {
			moveSpeed = 7f; // affects the distance that the player can jump from a wall
		} else {
			moveSpeed = 9f; // affects the speed that the player moves around
		}

		transform.rotation = Quaternion.Euler (0, 0, 0); // stops rotation

		if (normalMovement == true) {
			if (Input.anyKey) {
				zeroVelocity = false;
				if (Input.GetKey ("a")) {
					_Acc -= _AccSpeed;
					runningLeft = true;
				}

				if (Input.GetKey ("d")) {
					_Acc += _AccSpeed;
					runningRight = true;
				}

			} else {
				runningLeft = false;
				runningRight = false;
				if (zeroVelocity == false) {
					if (_Velocity > -2.0f && _Velocity < 2.0f) {
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
			Debug.Log ("Using normal movement");
		} else {
			if (Input.GetKey ("d") && !wallGrabLeft && !wallGrabRight)
			{
				rb.AddForce (Vector2.right, ForceMode2D.Impulse);
			}

			if (Input.GetKey ("a")  && !wallGrabLeft && !wallGrabRight)
			{
				rb.AddForce (Vector2.left, ForceMode2D.Impulse);
			}

			rb.velocity = Vector2.ClampMagnitude (rb.velocity, 15);
			Debug.Log("Using alternate movement");
		}

		currentHeight = gameObject.transform.position.y; // did this so the jump won't look floaty
		if (currentHeight < oldHeight) { // if player is falling gravity is more
			isFalling = true;
			isRising = false;
		} else { // if player is jumping gravity is less
			isRising = true;
			isFalling = false;
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
			leftGround = false;
			Debug.Log ("Touched Ground");
			_Acc = 0;
			_Velocity = 0;
		}
		if (other.gameObject.tag == "Wall" && isWallSliding == false) {
			{
				if (isFalling == false) {
					_Acc = 0;
					_Velocity = 0;
					if (Input.GetKey ("w")) {
						rb.AddForce (slideHeight, ForceMode2D.Impulse);
					}
				}
				canJump = false;
				isWallSliding = true;
				Debug.Log ("Wall Slide");
			}
			if (other.gameObject.name == "Finish Flag") {
				other.gameObject.GetComponent<BoxCollider2D> ().enabled = false;
				StartCoroutine ("Restart");
			}
		}
	}

	void OnCollisionStay2D (Collision2D other)
	{
		dir.x = other.contacts [0].point.x - other.gameObject.transform.position.x;
		dir = dir.normalized;


		if (other.gameObject.tag == "Wall" && Input.GetKey ("w") && isWallSliding == false) {
			isWallSliding = true;
		}
		if(dir.x < 0 && Input.GetKey("d"))
		{
			wallGrabRight = true;
		}
		if(dir.x > 0 && Input.GetKey("a"))
		{
			wallGrabLeft = true;
		}
		if (Input.GetKeyUp ("d"))
		{
			wallGrabRight = false;
		}
		if (Input.GetKeyUp ("a"))
		{
			wallGrabLeft = false;
		}
		if (other.gameObject.tag == "Ground") {
			isTouchingGround = true;
			isWallSliding = false;
			canJump = true;
		}
	}

	void OnCollisionExit2D (Collision2D other)
	{
		wallGrabLeft = false;
		wallGrabRight = false;
		isWallSliding = false;
		canJump = false;
		isTouchingGround = false;
		Debug.Log ("Left the " + other.gameObject.tag);

		dir.x = other.contacts [0].point.x - other.gameObject.transform.position.x;
		dir.y = other.contacts [0].point.y - other.gameObject.transform.position.y;
		dir.y = 0;
		dir = dir.normalized;

		if (other.gameObject.tag == "Wall") {
			isWallJumping = true;
			if (Input.GetKeyDown ("w")) {
				rb.AddForce (slideHeight, ForceMode2D.Impulse);
				rb.AddRelativeForce (dir * wallJumpForce, ForceMode2D.Impulse);
			}
			Debug.Log ("Wall Jump");
		}

		if (other.gameObject.tag == "Ground") {
			canJump = false;
			isWallSliding = false;
			leftGround = true;
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

    public void pause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            Debug.Log("Pausing game");
        }
        else
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
            Debug.Log("Resuming game");
        }
    }
}
