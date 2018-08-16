using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(MeshCollider))]
public class WheelBehaviour : MonoBehaviour {
	private MeshCollider m_wheelCollider;

	public Rigidbody chassisRigidbody;
	public Vector3 anchor;
	public float suspensionDistance = 0.3F;
	public float dampingRatio = 0.3F;
	public float suspensionForce = 5000F;
	public float radius = 1F;
	public float grossor = 0.2F;

	private float torque = 0;

	public float steerAngle {
		set {
			transform.localRotation = Quaternion.AngleAxis(value, Vector3.up);
		}
	}

	public float motorTorque {
		set {
			RaycastHit collisionHit;

			if (!Physics.Raycast(transform.position, transform.parent.rotation * Vector3.down, out collisionHit, radius + 0.1F)) {
			} else
				if (!Physics.Raycast(transform.position + new Vector3(grossor, 0, 0), transform.parent.rotation * Vector3.down, out collisionHit, radius + 0.1F)) {
			} else
					if (!Physics.Raycast(transform.position - new Vector3(grossor, 0, 0), transform.parent.rotation * Vector3.down, out collisionHit, radius + 0.1F)) {
				return;
			}
			
			chassisRigidbody.AddForceAtPosition(transform.parent.rotation * Vector3.forward * value * suspensionForce, transform.position);
		}
	}


	private float m_anchorOffset; 

	private void Reset() {
		chassisRigidbody = GetComponentInParent<Rigidbody>();
		anchor = transform.localPosition;

		m_wheelCollider = GetComponent<MeshCollider>();
		if (m_wheelCollider) {
			grossor = m_wheelCollider.bounds.extents.x;
			radius = m_wheelCollider.bounds.extents.y;
		}
	}

	// Use this for initialization
	void Start () {
		m_wheelCollider = GetComponent<MeshCollider>();
	}

	// Update is called once per frame
	void FixedUpdate() {
		float springForce = 0;
		RaycastHit collisionHit;
		float collisionForce = 0;

		if (Physics.Raycast(transform.position, transform.parent.rotation * Vector3.down, out collisionHit, radius + 0.1F)) {
			collisionForce = ((chassisRigidbody.velocity + Physics.gravity) * (chassisRigidbody.mass / suspensionForce)).y;
			chassisRigidbody.AddForceAtPosition(transform.parent.rotation * Vector3.up * suspensionForce, transform.position);
		} else
			if (Physics.Raycast(transform.position + new Vector3(grossor, 0, 0), transform.parent.rotation * Vector3.down, out collisionHit, radius + 0.1F)) {
				collisionForce = ((chassisRigidbody.velocity + Physics.gravity) * (chassisRigidbody.mass / suspensionForce)).y;
				chassisRigidbody.AddForceAtPosition(transform.parent.rotation * Vector3.up * suspensionForce, transform.position + new Vector3(grossor, 0, 0));
			} else
				if (Physics.Raycast(transform.position - new Vector3(grossor, 0, 0), transform.parent.rotation * Vector3.down, out collisionHit, radius + 0.1F)) {
					collisionForce = ((chassisRigidbody.velocity + Physics.gravity) * (chassisRigidbody.mass / suspensionForce)).y;
					chassisRigidbody.AddForceAtPosition(transform.parent.rotation * Vector3.up * suspensionForce, transform.position - new Vector3(grossor, 0, 0));
				}

		m_anchorOffset -= collisionForce * dampingRatio * Time.fixedDeltaTime;

		springForce = (Mathf.LerpUnclamped(suspensionDistance / 2, -suspensionDistance / 2, m_anchorOffset) - 0.5F) * 2F;
		m_anchorOffset += chassisRigidbody.mass / (suspensionForce * springForce) * Time.fixedDeltaTime;
		m_anchorOffset = Mathf.Clamp(m_anchorOffset, -suspensionDistance / 2, suspensionDistance / 2);

		transform.localPosition = GetPosition();
	}

	private void OnCollisionStay(Collision collision) {
	}

	public float CalculateSuspensionForce() {
		return suspensionDistance;
	}

	public Vector3 GetPosition() {
		return anchor + new Vector3(0, m_anchorOffset, 0);
	}

	private void OnDrawGizmosSelected() {
		Gizmos.matrix = transform.parent.localToWorldMatrix;
		Gizmos.DrawLine(GetPosition(), GetPosition() + new Vector3(0, -radius, 0));

		Gizmos.DrawLine(GetPosition(), GetPosition() + new Vector3(grossor, 0, 0));
		Gizmos.DrawLine(GetPosition(), GetPosition() + new Vector3(-grossor, 0, 0));
		Gizmos.DrawLine(GetPosition() + new Vector3(grossor, 0, 0), GetPosition() + new Vector3(grossor, -radius, 0));
		Gizmos.DrawLine(GetPosition() + new Vector3(-grossor, 0, 0), GetPosition() + new Vector3(-grossor, -radius, 0));
	}
}
