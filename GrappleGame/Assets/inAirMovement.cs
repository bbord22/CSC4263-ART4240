using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inAirMovement : MonoBehaviour 
{
	Rigidbody2D rb;
	// Use this for initialization
	void Start () 
	{
		rb = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (Input.GetKey ("d")) 
		{
			rb.AddForce (Vector2.right, ForceMode2D.Impulse);
		}

		if (Input.GetKey ("a")) 
		{
			rb.AddForce (Vector2.left, ForceMode2D.Impulse);
		}
	}
}
