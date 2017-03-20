using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throttle : MonoBehaviour {

	WheelJoint2D[] wheelJoints;
	JointMotor2D jointMotor;

	void Start () 
	{
		wheelJoints = gameObject.GetComponents<WheelJoint2D> ();
		jointMotor = wheelJoints [1].motor;
	}

	void Update () 
	{
		if (Input.GetKey (KeyCode.RightArrow)) 
		{
			jointMotor.motorSpeed =-500;
			wheelJoints [1].motor = jointMotor;
		}

		else if (Input.GetKey (KeyCode.LeftArrow)) 
		{
			jointMotor.motorSpeed = 500;
			wheelJoints [1].motor = jointMotor;
		}

		else
		{

		}
	}
}
