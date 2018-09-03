using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifesaver : MonoBehaviour {
	public CheckPoints checkPoints;
	public Rigidbody rigidbody;
	public string resetTag = "Reset";
	public float minVelocity = 0.1F;
	public float maxTime = 5;
	
	private float currentStoppedTime;

	private void Update() {
		if (Mathf.Abs(rigidbody.velocity.magnitude) <= minVelocity) {
			currentStoppedTime += Time.deltaTime;
		} else {
			currentStoppedTime = 0;
		}

		if (currentStoppedTime > maxTime) {
			currentStoppedTime = 0;
			ResetCar();
		}
	}

	private void Start() {
		checkPoints = GetComponent<CheckPoints>();
		rigidbody = GetComponent<Rigidbody>();
	}

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag(resetTag)) {
			ResetCar();
		}
	}

	private void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.CompareTag(resetTag)) {
			ResetCar();
		}
	}

	public void ResetCar() {
		if (checkPoints.currentPoint) {
			transform.position = checkPoints.currentPoint.transform.position;
			transform.rotation = checkPoints.currentPoint.transform.rotation;
		} else {
			transform.position = checkPoints.nextPoint.transform.position;
			transform.rotation = checkPoints.nextPoint.transform.rotation;
		}
		
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
	}
}
