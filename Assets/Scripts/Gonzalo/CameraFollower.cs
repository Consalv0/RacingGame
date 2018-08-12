using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {
	[Range(0.0F, 1.0F)]
	public float damping = 1;
	public float elasticity = 2;
	public Vector3 naturalOffset;
	public Transform target;

	private Vector3 m_velocity;

	public Vector3 velocity {
		get { return m_velocity; }
		set { m_velocity = value; }
	}

	// Use this for initialization
	void Start () {
			
	}

	void FixedUpdate () {
		if (target) {
			Vector3 direction = (transform.position - target.position);
			m_velocity += -elasticity * (direction + target.rotation * naturalOffset) - damping * m_velocity;
			transform.position += m_velocity * Time.fixedDeltaTime;

			transform.LookAt (target.position);
		}
	}
}
