using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SimpleCarController))]
public class DetectSurface : MonoBehaviour {
	public float normalSidewaysStifness = 1;
	public LayerMask layerMask;

	private SimpleCarController carController;
	private float stifness = 0;
	private GameObject lastCollision;

	// Use this for initialization
	void Start () {
		carController = GetComponent<SimpleCarController>();
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		if (Physics.Raycast(transform.position + transform.up * 2, -transform.up, out hit, 100000, (int)layerMask)) {
			SurfaceMaterial material = hit.collider.GetComponent<SurfaceMaterial>();
			if (material) {
				carController.back_leftW.sidewaysFriction = material.wheelFrictionCurve;
				carController.back_rightW.sidewaysFriction = material.wheelFrictionCurve;
				carController.front_leftW.sidewaysFriction = material.wheelFrictionCurve;
				carController.front_rightW.sidewaysFriction = material.wheelFrictionCurve;
				stifness = material.wheelFrictionCurve.stiffness;
			} else {
				ResetValue(carController.back_leftW);
				ResetValue(carController.back_rightW);
				ResetValue(carController.front_leftW);
				ResetValue(carController.front_rightW);
			}
			lastCollision = hit.collider.gameObject;
		} else {
			ResetValue(carController.back_leftW);
			ResetValue(carController.back_rightW);
			ResetValue(carController.front_leftW);
			ResetValue(carController.front_rightW);
		}
	}

	private void ResetValue(WheelCollider wheel) {
		var val = wheel.sidewaysFriction;
		val.stiffness = normalSidewaysStifness;
		wheel.sidewaysFriction = val;
		stifness = normalSidewaysStifness;
	}
}
