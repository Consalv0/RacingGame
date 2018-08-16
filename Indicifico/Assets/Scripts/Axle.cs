using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Axle {
	public WheelCollider leftWheel;
	public Transform leftWheelVisual;
	public WheelCollider rightWheel;
	public Transform rightWheelVisual;
	public bool motor;
	public bool steering;

	private void ApplyLocalPositionToVisuals () {
		Vector3 position;
		Quaternion rotation;

		if (rightWheelVisual) {
			rightWheel.GetWorldPose (out position, out rotation);
			rightWheelVisual.position = position;
			rightWheelVisual.rotation = rotation;
		}
		if (leftWheelVisual) {
			leftWheel.GetWorldPose (out position, out rotation);
			leftWheelVisual.position = position;
			leftWheelVisual.rotation = rotation;
		}
	}

	public void UpdateRotationAndTorque (float steerAngle, float motorTorque) {
		if (steering) {
			leftWheel.steerAngle = steerAngle;
			rightWheel.steerAngle = steerAngle;
		}
		if (motor) {
			leftWheel.motorTorque = motorTorque;
			rightWheel.motorTorque = motorTorque;
		}

		ApplyLocalPositionToVisuals ();
	}
}