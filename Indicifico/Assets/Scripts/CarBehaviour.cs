using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehaviour : MonoBehaviour {

	public WheelsAxle frontAxle;
	[Space]
	public WheelsAxle backAxle;
	[Space]
	public float maxMotorTorque = 10000;
	public float maxSteeringAngle = 30;

	public void FixedUpdate() {
		float motorTorque = maxMotorTorque * Input.GetAxis("Vertical");
		float steeringAngle = maxSteeringAngle * Input.GetAxis("Horizontal");

		frontAxle.UpdateRotationAndTorque(steeringAngle, motorTorque);
		backAxle.UpdateRotationAndTorque(steeringAngle, motorTorque);
	}

}
