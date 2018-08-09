using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarController : MonoBehaviour {
	public Axle frontAxle;
	[Space]
	public Axle backAxle;
	[Space]
	public float maxMotorTorque;
	public float maxSteeringAngle;
  public char player;

	public void FixedUpdate () {
		float motorTorque = maxMotorTorque * Input.GetAxis ("Aceleration"+player);
		float steeringAngle = maxSteeringAngle * Input.GetAxis ("Horizontal"+player);

		backAxle.UpdateRotationAndTorque (steeringAngle, motorTorque);
		frontAxle.UpdateRotationAndTorque (steeringAngle, motorTorque);
	}

}