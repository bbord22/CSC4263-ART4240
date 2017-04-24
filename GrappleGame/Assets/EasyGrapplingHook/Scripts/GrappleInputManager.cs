using UnityEngine;
using System.Collections;

public class GrappleInputManager : MonoBehaviour {
	
	GrappleScript grapple;
	public Camera cam;
	public GameObject arm;

	public float angleStep = 1;
	/* The angle around the target that the rope can attach to
	 * i.e 90 means 90 degrees clockwise + 90 counter clockwise.*/
	[Range(0.0f,360.0f)]
	public float angleTolerance = 90;
	public static bool swingReady = true;
	public float timer = 2;
	public static bool startTimer = false;

	void Start()
	{
		grapple = GetComponent<GrappleScript>();
		cam = Camera.main;
		arm = GameObject.FindGameObjectWithTag ("Arm");
	}
	
	void Update()
	{
		UpdateInput();
		if (startTimer) 
		{
			Debug.Log ("Timer Started");
			timer -= Time.deltaTime;
			if (timer <= 0) {
				startTimer = false;
				swingReady = true;
				Debug.Log ("Time is Up");
			}
		}
	}
	
	private void UpdateInput () {
		if(Input.GetMouseButton(0))
		{
			// Find mouse position
			Vector3 mouseInput = new Vector3(Input.mousePosition.x,Input.mousePosition.y,10);
			Vector2 mouseClick = cam.ScreenToWorldPoint(mouseInput);
			
			// Find ray direction and raycast
			Vector2 rayDirection = mouseClick - (Vector2)this.transform.position;
			RaycastHit2D hit = Physics2D.Raycast((Vector2)this.transform.position , rayDirection , grapple.grapplingHookRange, ~(1<<grapple.playerLayer));
			float angle = angleStep;
			Quaternion rot;

			// If the raycast does not hit anything, loop raycast until object is hit
			while(hit.collider == null && angle<angleTolerance)
			{
				rot = Quaternion.AngleAxis(angle , Vector3.forward);
				hit = Physics2D.Raycast((Vector2)this.transform.position , rot*rayDirection, grapple.grapplingHookRange, ~(1<<grapple.playerLayer));
				
				if(hit.collider!=null)
					break;
				
				rot = Quaternion.AngleAxis(-angle , Vector3.forward);
				hit = Physics2D.Raycast((Vector2)this.transform.position , rot*rayDirection, grapple.grapplingHookRange, ~(1<<grapple.playerLayer));
				angle+=angleStep;
				
			}
			// if something is hit, and that is not the player
			if(hit.collider != null && hit.transform.tag != "Player" && swingReady == true && hit.transform.tag == "Anchor")
			{
				grapple.AttachRope(hit.point);
				GameObject.FindGameObjectWithTag("Player").GetComponent<ArmCursorFollow> ().enabled = false;
			}
		}
		// Check for rope release
		else// if(Input.GetMouseButtonUp(1))
		{
			grapple.ReleaseRope();
			GameObject.FindGameObjectWithTag("Player").GetComponent<ArmCursorFollow> ().enabled = true;

		}

		// Setting reeling and paying out
		grapple.reeling_in = Input.GetMouseButton(1);
		//grapple.paying_out = Input.GetKey(KeyCode.X);

	}
}
