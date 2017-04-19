using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmCursorFollow : MonoBehaviour {

	//public Vector2 mousePos;
	public float mouseAngle;
	public float currentAngle;
	public HingeJoint2D arm;
	public JointMotor2D motor;
	public bool motorOff;

	void Start ()
	{
		arm = GetComponent<HingeJoint2D> ();
		//JointMotor2D motor = arm.motor;
		motor = new JointMotor2D();
		motor.maxMotorTorque = 10000;
		motorOff = false;

	}

	void Update ()
	{


		Vector2 mousePos = Input.mousePosition;
		Vector2 objectPos = Camera.main.WorldToScreenPoint (transform.position);
		mousePos.x = mousePos.x - objectPos.x;
		mousePos.y = mousePos.y - objectPos.y;

		currentAngle = gameObject.GetComponent<HingeJoint2D> ().jointAngle;
		currentAngle += 90;

		mouseAngle = Mathf.Atan2 (mousePos.y, mousePos.x);
		mouseAngle *= Mathf.Rad2Deg;

		if (mouseAngle < 0) {
			mouseAngle += 360;

		}

		if (motorOff) {
			motor.motorSpeed = 0;
			arm.motor = motor;
		}


		if ((mouseAngle - currentAngle) > 10) {
//			arm.motor.motorSpeed = 100;
//			arm.motor = motor;
			motorOff = false;
			motor.motorSpeed = 1000;
			arm.motor = motor;
		} else if ((mouseAngle - currentAngle) < -10) {
//			arm.motor.motorSpeed = -100;
//			arm.motor = motor;
			motorOff = false;
			motor.motorSpeed = -1000;
			arm.motor = motor;
		}
		if ((mouseAngle - currentAngle) < 10 || (mouseAngle - currentAngle) > -10) {
			{
				motorOff = true;
				arm.motor = motor;
				Debug.Log ("motor off");
			}
		}
	}
}


