using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.Experimental.Rendering.Volume))]
public class InitializeVolume : MonoBehaviour {
	private void Start() {
		StartCoroutine(EnableVolume());
	}

	IEnumerator EnableVolume() {
		yield return new WaitForSecondsRealtime(1.0F);

		GetComponent<UnityEngine.Experimental.Rendering.Volume>().enabled = true;
	}
}
