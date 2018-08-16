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
		float motorTorque = maxMotorTorque * (Input.GetAxis("Fire1") - Input.GetAxis("Fire2"));
		float steeringAngle = maxSteeringAngle * Input.GetAxis ("Horizontal");

		backAxle.UpdateRotationAndTorque (steeringAngle, motorTorque);
		frontAxle.UpdateRotationAndTorque (steeringAngle, motorTorque);
	}

}