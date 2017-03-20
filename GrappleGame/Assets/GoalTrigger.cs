using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D other)
	{
		other.gameObject.GetComponent<SpriteRenderer> ().color = Color.yellow;
	}
}
