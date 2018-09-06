using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelector : MonoBehaviour {
	public Camera playerCamera;
	public CarStageBase[] carStageBases;
    public MoveManager manager;

    public int player;
	public float minSelectionTime = 1;

    public Track[] AvailableTracks;
	public Track selectedTrack;
    public static int whattrack;

	private int selectedBase;
	private float rotationAngle;
	private float lastSelectionTime;

    public bool isActive = true;
    public bool isStage = false;
    public bool OutInStage = true;
    public bool done = false;


	// Use this for initialization
	void Start() {
        whattrack = PlayerPrefs.GetInt("track");
        selectedTrack = AvailableTracks[whattrack];
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
		float horizontalAxis = Input.GetAxisRaw("Turn" + player);
		float vertical = Input.GetAxisRaw("Aceleration" + player);

		if (isActive && (Time.time - lastSelectionTime) > minSelectionTime && !horizontalAxis.Equals(0)) {
			if (horizontalAxis < -0.8F) {
				rotateAndSelect(true);
			} else if (horizontalAxis > 0.8F) {
				rotateAndSelect(false);
			}
		}

		if (isActive && Input.GetButtonDown("jump" + player)) {
			Transform spawn = selectedTrack.playerSpawns[player - 1];
			Transform target = playerCamera.GetComponent<CameraFollower>().target =
				GameObject.Instantiate(CarSelected(), spawn.position, spawn.rotation).transform;
            var car = target.GetComponent<SimpleCarController>();
            car.player = player;
			target.GetComponent<Test_Jump>().player = player;
			target.GetComponent<CheckPoints>().track = selectedTrack;
            if (player == 1) manager.car1 = car;
            else manager.car2 = car;
			isActive = false;
		}

		if (!isActive && Input.GetButtonDown("Start")) {
			Destroy(playerCamera.GetComponent<CameraFollower>().target.gameObject);
			playerCamera.GetComponent<CameraFollower>().target = transform;
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
