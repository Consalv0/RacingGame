using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WheelsAxle {
	public WheelBehaviour leftWheel;
	public WheelBehaviour rightWheel;
	public bool motor;
	public bool steering;

	public void UpdateRotationAndTorque(float steerAngle, float motorTorque) {
		if (steering) {
			leftWheel.steerAngle = steerAngle;
			rightWheel.steerAngle = steerAngle;
		}
		if (motor) {
			leftWheel.motorTorque = motorTorque;
			rightWheel.motorTorque = motorTorque;
		}
	}
}