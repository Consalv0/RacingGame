using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStageBase : MonoBehaviour {
	public GameObject car;
	public float radius;
	public Vector3 center;
	public float targetAngle;
	public AnimationCurve traslationCurve = AnimationCurve.Linear(0, 0, 1, 1);
	[Range(0, 1)]
	public float progress;
	public bool isDone;

	[System.NonSerialized]
	public float initialAngle;

	// Use this for initialization
	void Start () {
		initialAngle = targetAngle;	
	}
	
	// Update is called once per frame
	void Update () {
		if (!isDone) {
			progress += Time.deltaTime;
			isDone = progress >= 1;
			progress = Mathf.Clamp01(progress);

			if (isDone) {
				progress = 0;
				initialAngle = targetAngle;
			}
		}

		float angle = Mathf.LerpAngle(initialAngle, targetAngle, traslationCurve.Evaluate(progress));
		transform.position = center + Quaternion.AngleAxis(angle, Vector3.up) * (Vector3.forward * radius);
	}
}
