using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

  public Transform car;

	
	// Update is called once per frame
	void LateUpdate () {

    transform.LookAt(car);
	}
}
