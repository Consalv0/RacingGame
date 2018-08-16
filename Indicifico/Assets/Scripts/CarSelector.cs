using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelector : MonoBehaviour {
	public CarStageBase[] carStageBases;
	
	private int selectedBase;
	private float rotationAngle;

	// Use this for initialization
	void Start() {
		rotationAngle = 360 / carStageBases.Length;
		float angle = 0;
		foreach (CarStageBase stage in carStageBases) {
			stage.targetAngle = angle + 120 + rotationAngle;
			angle += rotationAngle;
		}

		selectedBase = 0;
	}

	// Update is called once per frame
	void Update() {
		if (Input.GetKeyDown(KeyCode.D)) {
			rotateAndSelect(true);
		} else if (Input.GetKeyDown(KeyCode.A)) {
			rotateAndSelect(false);
		}

		if (Input.GetButtonDown("Fire1")) {
			Camera.main.GetComponent<CameraFollower>().target =
				GameObject.Instantiate(CarSelected(), new Vector3(0, 100, 0), Quaternion.identity).transform;
			gameObject.SetActive(false);
		}
	}

	void rotateAndSelect(bool forward) {
		foreach (CarStageBase stage in carStageBases) {
			stage.initialAngle = stage.targetAngle;
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
