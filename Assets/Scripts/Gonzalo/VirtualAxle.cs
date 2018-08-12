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
		float motorForce = maxMotorForce * (Input.GetAxis ("Fire1") - Input.GetAxis("Fire2"));
		float steerAngle = maxSteerAngle * Input.GetAxis ("Horizontal");

		leftWheel.steerAngle = steerAngle;
		rightWheel.steerAngle = steerAngle;

		if (leftWheel.IsGrounded) {
			chassisBody.AddForceAtPosition ((leftWheel.transform.forward / 2) * motorForce, leftWheel.transform.position, ForceMode.Force);
		}
		if (rightWheel.IsGrounded) {
			chassisBody.AddForceAtPosition ((rightWheel.transform.forward / 2) * motorForce, rightWheel.transform.position, ForceMode.Force);
		}
	}
}
