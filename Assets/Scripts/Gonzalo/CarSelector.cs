using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelector : MonoBehaviour {
	public CarStageBase[] carStageBases;
	public Transform[] playerSpawns;
	public Transform cameraPosition;
	public int player;
	public float minSelectionTime = 1;

	private int selectedBase;
	private float rotationAngle;
	private float lastSelectionTime;

	public bool isActive = true;
	public bool isStage = false;
	public bool OutInStage = true;
	public bool done = false;

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

        if (isStage)
        {
            if (OutInStage)
            {
                UnityEngine.Camera.main.GetComponent<Transform>().LookAt(playerSpawns[player]);
            }
            else
            {
                UnityEngine.Camera.main.GetComponent<Transform>().LookAt(playerSpawns[player + 2]);
            }
        }

        if (isStage && isActive && (Time.time - lastSelectionTime) > minSelectionTime && !horizontalAxis.Equals(0))
        {
            
            if (horizontalAxis > 0.8F)
            {
                OutInStage = true;
                UnityEngine.Camera.main.GetComponent<Transform>().LookAt(playerSpawns[player].position);
            }
            else if (horizontalAxis < -0.8F)
            {
                OutInStage = false;
                UnityEngine.Camera.main.GetComponent<Transform>().LookAt(playerSpawns[player+2].position); 
            }
        }
        if (!isStage && isActive && (Time.time - lastSelectionTime) > minSelectionTime && !horizontalAxis.Equals(0)) {
			if (horizontalAxis < -0.8F) {
				rotateAndSelect(true);
			} else if (horizontalAxis > 0.8F) {
				rotateAndSelect(false);
			}
		}

		if (isActive && Input.GetButtonDown("Fire1")) {          
            if (!isStage && isActive)
            {
                isStage = true;
                UnityEngine.Camera.main.GetComponent<CameraFollower>().target = cameraPosition;
                //UnityEngine.Camera.main.GetComponent<Transform>().LookAt(playerSpawns[player]);
            }
            else if (isStage && isActive)
            {
                isStage = false;
                isActive = false;
                done = true;
                Debug.Log("Deberia de empezar");
            }
		}
        if (done)
        {
            if (OutInStage)
            {
                Debug.Log("Wat");
                UnityEngine.Camera.main.GetComponent<CameraFollower>().target =
                    GameObject.Instantiate(CarSelected(), playerSpawns[player].position, playerSpawns[player].rotation).transform;
                done = false;
            }
            if (!OutInStage)
            {
                Debug.Log("Waaaaaat");
                UnityEngine.Camera.main.GetComponent<CameraFollower>().target =
                    GameObject.Instantiate(CarSelected(), playerSpawns[player + 2].position, playerSpawns[player + 2].rotation).transform;
                done = false;
            }
        }
        if (!isActive && Input.GetButtonDown("Start")) {
			Destroy(UnityEngine.Camera.main.GetComponent<CameraFollower>().target.gameObject);
			UnityEngine.Camera.main.GetComponent<CameraFollower>().target = transform;
			isActive = true;
            isStage = false;
            done = false;
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
