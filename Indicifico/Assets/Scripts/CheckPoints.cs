using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour {
	[System.NonSerialized]
	public Track track;
	public Collider currentPoint;
	public Collider nextPoint;
	private int pastPoint = 0;


	private void Start() {

		nextPoint = track.checkPoints.checkPoints[0];
	}
	private void OnTriggerEnter(Collider other) {
		for (int i = 0; i < track.checkPoints.checkPoints.Count; i++) {
			if (other == track.checkPoints.checkPoints[i] && other == nextPoint) {
				Debug.Log(i);
				currentPoint = nextPoint;
				if (i < track.checkPoints.checkPoints.Count - 1)
					nextPoint = track.checkPoints.checkPoints[i + 1];
				pastPoint += 1;
			}
		}
	}
	private void Update() {
		if (pastPoint == track.checkPoints.checkPoints.Count) {
			Debug.Log("Ganaste");
		}
	}


}
