using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steer : MonoBehaviour {
	public Rigidbody m_rightWheel;
	public Rigidbody m_leftWheel;

	public float m_minAngle = -30;
	public float m_maxAngle = 30;

	public float m_rotation;

	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("Horizontal") != 0) {
			m_rotation = Mathf.LerpUnclamped(
				(m_minAngle + m_maxAngle) * 0.5F,
				(m_maxAngle + m_maxAngle) * 0.5F,
				Input.GetAxis("Horizontal")
			);
		} else {
			m_rotation = 0;
		}

		Quaternion targetRotation = Quaternion.Euler (0, transform.rotation.eulerAngles.y + m_rotation, 0);
		
		m_rightWheel.MoveRotation (targetRotation);
		m_leftWheel.MoveRotation (targetRotation);
	}
}
