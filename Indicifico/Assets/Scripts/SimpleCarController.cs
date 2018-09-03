using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarController : MonoBehaviour {

	private float horizontalInput;
	private float verticalInput;
	private float steeringAngle;
	private float breakInput;



	public WheelCollider front_leftW, front_rightW;
	public WheelCollider back_leftW, back_rightW;


	[HideInInspector] public float negative_motorForce;

	private float originalSteerAngle;
	private float SteerAngleClipping;

	private float originalMotorForce;
	private float boostMotorForce;


	public Transform front_leftT, front_rightT;
	public Transform back_leftT, back_rightT;

	public float maxSteerAngle = 30;  //How fast you can turn
	public float motorForce = 50;
	public float plusSteerAngle = 10;
	public float boost = 50;

	public float maxVelocity;
	public float friction = -100;

	public float clippingTime;
	public float BoostTime;

	public float stop = 20;

	private float originalBtime;
	private float originalCtime;
	private bool StartBTime = false;
	private bool StartCTime = false;
	private bool DoneCliping = false;
	private bool DoneBoost = false;

	public string player;
	public Rigidbody rb;
	private bool UniqueBoost;

	public void Start() {
		front_leftT.transform.Rotate(new Vector3(0, -90, 0));
		front_rightT.transform.Rotate(new Vector3(0, 90, 0));
		back_leftT.transform.Rotate(new Vector3(0, -90, 0));
		back_rightT.transform.Rotate(new Vector3(0, 90, 0));
		negative_motorForce = motorForce * -1;
		originalSteerAngle = maxSteerAngle;
		SteerAngleClipping = maxSteerAngle + plusSteerAngle;
		originalMotorForce = motorForce;
		boostMotorForce = motorForce + boost;
		originalBtime = BoostTime;
		originalCtime = clippingTime;
		rb = GetComponent<Rigidbody>();
	}
	public void GetInput() {
		horizontalInput = Input.GetAxis("Turn" + player);
		verticalInput = Input.GetAxis("Aceleration" + player);
		breakInput = Input.GetAxis("Break" + player);
	}
	public void Clipping() {
		if (Input.GetAxis("Derrape" + player) == 1) {
			maxSteerAngle = SteerAngleClipping;
			StartCTime = true;
		} else {
			maxSteerAngle = originalSteerAngle;
			clippingTime = originalCtime;
			StartCTime = false;
		}

	}
	public void Boost() {

		if (DoneCliping) {
			//Debug.Log("bbos");
			motorForce = boostMotorForce;
			StartBTime = true;
		} else if (UniqueBoost && Input.GetAxis("Boost" + player) == 1) {

			motorForce = boostMotorForce;
			StartBTime = true;
			if (DoneBoost) {
				Debug.Log("terminado");
				UniqueBoost = false;
			}
		} else {

			motorForce = originalMotorForce;
			BoostTime = originalBtime;
			StartBTime = false;
		}
	}
	public void AllDone() {
		if (DoneCliping && DoneBoost) {
			DoneCliping = false;
			DoneBoost = false;
		}
	}
	public void Steer() {
		steeringAngle = maxSteerAngle * horizontalInput;
		front_leftW.steerAngle = steeringAngle;
		front_rightW.steerAngle = steeringAngle;
	}

	private void Acelerate() {

		if (verticalInput == 1) {

			front_leftW.motorTorque = verticalInput * motorForce;
			front_rightW.motorTorque = verticalInput * motorForce;
		} else if (breakInput == 1) {

			front_leftW.motorTorque = breakInput * negative_motorForce;
			front_rightW.motorTorque = breakInput * negative_motorForce;
		} else {
			frictionUpdate();
		}

	}

	void frictionUpdate() {
		if (rb.velocity.magnitude > 0 && breakInput == 0 && verticalInput == 0) {


			if (front_leftW.motorTorque > 0) {
				front_leftW.motorTorque = -stop;
				front_rightW.motorTorque = -stop;
			} else {
				front_leftW.motorTorque = stop;
				front_rightW.motorTorque = stop;
			}
		}
	}
	private void UpdateWheelPoses() {
		UpdateWheelPose(front_leftW, front_leftT);
		UpdateWheelPose(front_rightW, front_rightT);
		UpdateWheelPose(back_leftW, back_leftT);
		UpdateWheelPose(back_rightW, back_rightT);
	}

	private void UpdateWheelPose(WheelCollider collider, Transform _transform) {
		Vector3 pos = _transform.position;
		Quaternion quaternion = _transform.rotation;

		collider.GetWorldPose(out pos, out quaternion);

		_transform.position = pos;
		_transform.rotation = quaternion;
	}

	public void ClippingTimer() {

		if (clippingTime <= 0) {
			DoneCliping = true;
		}

	}

	public void BoostTimer() {
		if (BoostTime <= 0) {
			DoneBoost = true;
		}
	}

	public void CheckVelocity() {
		float velocityp = (70 * maxVelocity) / 100;
		//if(StartBTime)
		//{
		//  Debug.Log("Boost");
		//}
		if (rb.velocity.magnitude > maxVelocity && !StartBTime && verticalInput == 1) {

			motorForce = friction;

		} else if (rb.velocity.magnitude >= velocityp && rb.velocity.magnitude < maxVelocity && !StartBTime) {
			motorForce = -friction;

		}

	}
	private void Update() {

		if (StartCTime) {
			clippingTime -= Time.deltaTime;
		}
		if (StartBTime)
			BoostTime -= Time.deltaTime;

		ClippingTimer();
		BoostTimer();
		AllDone();


	}

	private void FixedUpdate() {
		GetInput();

		Clipping();
		Boost();
		Steer();
		CheckVelocity();
		Acelerate();



		UpdateWheelPoses();
	}

}
