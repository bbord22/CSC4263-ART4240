using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
    public int speed;
    private Rigidbody2D rigidbody;

	void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		
	}
}
