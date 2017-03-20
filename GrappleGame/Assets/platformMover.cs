using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformMover : MonoBehaviour {
	Rigidbody2D rigidBody;
	Vector2 currentPosition;
	Vector2 initialPosition;

	void Start () 
	{
		rigidBody = gameObject.GetComponent<Rigidbody2D> ();
		initialPosition = gameObject.transform.position;
	}

	void Update () 
	{
		currentPosition = initialPosition;
		currentPosition.x = initialPosition.x + Mathf.Sin (Time.timeSinceLevelLoad);
		rigidBody.MovePosition (currentPosition); 
	}
}
