using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelector : MonoBehaviour {
	public CarStageBase[] carStageBases;
	public Transform[] playerSpawns;
	public int player;
	public float minSelectionTime = 1;

	private int selectedBase;
	private float rotationAngle;
	private float lastSelectionTime;

	public bool isActive = true;

	// Use this for initialization
	void Start() {
		rotationAngle = 360 / carStageBases.Length;
		float angle = 0;
		foreach (CarStageBase stage in carStageBases) {
			stage.targetAngle = angle + rotationAngle * 3 + rotationAngle;
			angle -= rotationAngle;
		}

		selectedBase = 0;
	}

	// Update is called once per frame
	void Update() {
		float horizontalAxis = Input.GetAxisRaw("Horizontal");
		if (isActive && (Time.time - lastSelectionTime) > minSelectionTime && !horizontalAxis.Equals(0)) {
			if (horizontalAxis < -0.8F) {
				rotateAndSelect(true);
			} else if (horizontalAxis > 0.8F) {
				rotateAndSelect(false);
			}
		}

		if (isActive && Input.GetButtonDown("Fire1")) {
			UnityEngine.Camera.main.GetComponent<CameraFollower>().target =
				GameObject.Instantiate(CarSelected(), playerSpawns[player].position, playerSpawns[player].rotation).transform;
			isActive = false;
		}

		if (!isActive && Input.GetButtonDown("Start")) {
			Destroy(UnityEngine.Camera.main.GetComponent<CameraFollower>().target.gameObject);
			UnityEngine.Camera.main.GetComponent<CameraFollower>().target = transform;
			isActive = true;
		}
	}

	void rotateAndSelect(bool forward) {
		lastSelectionTime = Time.time;

		foreach (CarStageBase stage in carStageBases) {
			stage.initialAngle = Mathf.LerpAngle(stage.initialAngle, stage.targetAngle, stage.traslationCurve.Evaluate(stage.progress));
			stage.progress = 0;
			stage.isDone = false;
			stage.targetAngle += forward ? rotationAngle : -rotationAngle;
		}

		if (!forward)
		if (selectedBase == 0) {
			selectedBase = carStageBases.Length - 1;
		} else {
			selectedBase -= 1;
		}
		 
		else
		if (selectedBase == carStageBases.Length - 1) {
			selectedBase = 0;
		} else {
			selectedBase += 1;
		}
	}

	public GameObject CarSelected() {
		return carStageBases[selectedBase].car;
	}
}
