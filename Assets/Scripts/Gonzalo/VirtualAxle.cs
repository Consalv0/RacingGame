using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualAxle : MonoBehaviour {
	public Rigidbody chassisBody;
	public Wheel leftWheel;
	public Wheel rightWheel;
	public float maxMotorForce;
	public float rotationForce;
	public float maxSteerAngle;

	private void FixedUpdate () {
		float motorForce = maxMotorForce * Input.GetAxis ("Vertical");
		float steeringAngle = Input.GetAxis ("Horizontal");

		leftWheel.transform.localRotation = Quaternion.AngleAxis (steeringAngle * maxSteerAngle, Vector3.up);
		rightWheel.transform.localRotation = Quaternion.AngleAxis (steeringAngle * maxSteerAngle, Vector3.up);

		Vector3 rotationForceDirection = chassisBody.transform.right;
		Debug.DrawLine (transform.position, transform.position + rotationForceDirection, Color.yellow);

		if (leftWheel.IsGrounded) {
			chassisBody.AddForceAtPosition ((rotationForceDirection / 2) * (steeringAngle * rotationForce), leftWheel.transform.position, ForceMode.VelocityChange);
			chassisBody.AddForceAtPosition ((leftWheel.transform.forward / 2) * motorForce, leftWheel.transform.position, ForceMode.VelocityChange);
		}
		if (leftWheel.IsGrounded) {
			chassisBody.AddForceAtPosition ((rotationForceDirection / 2) * (steeringAngle * rotationForce), rightWheel.transform.position, ForceMode.VelocityChange);
			chassisBody.AddForceAtPosition ((rightWheel.transform.forward / 2) * motorForce, rightWheel.transform.position, ForceMode.VelocityChange);
		}
	}
}
