using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(MeshCollider))]
public class WheelBehaviour : MonoBehaviour {
	private MeshCollider m_wheelCollider;

	public Rigidbody m_chassisRigidbody;
	public Vector3 m_anchor;
	public float m_suspensionDistance = 0.3F;
	public float m_suspensionForce = 5000F;
	public float m_radius = 1F;
	public float m_grossor = 0.2F;

	private float m_anchorOffset; 

	private void Reset() {
		m_chassisRigidbody = GetComponentInParent<Rigidbody>();
		m_anchor = transform.localPosition;

		m_wheelCollider = GetComponent<MeshCollider>();
		if (m_wheelCollider) {
			m_grossor = m_wheelCollider.bounds.extents.x;
			m_radius = m_wheelCollider.bounds.extents.y;
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

		if (Physics.Raycast(transform.position, transform.rotation * Vector3.down, out collisionHit, m_radius + 0.1F)) {
			m_anchorOffset -= ((m_chassisRigidbody.velocity + Physics.gravity) * (m_chassisRigidbody.mass / m_suspensionForce)).y * Time.fixedDeltaTime;
			springForce = (Mathf.LerpUnclamped(m_suspensionDistance / 2, -m_suspensionDistance / 2, m_anchorOffset) - 0.5F) * 2F;
			m_chassisRigidbody.AddForceAtPosition(transform.rotation * Vector3.up * m_suspensionForce, transform.position);
		}

		springForce = (Mathf.LerpUnclamped(m_suspensionDistance / 2, -m_suspensionDistance / 2, m_anchorOffset) - 0.5F) * 2F;
		m_anchorOffset += m_chassisRigidbody.mass / (m_suspensionForce * springForce) * Time.fixedDeltaTime;
		m_anchorOffset = Mathf.Clamp(m_anchorOffset, -m_suspensionDistance / 2, m_suspensionDistance / 2);

		transform.localPosition = GetPosition();
	}

	private void OnCollisionStay(Collision collision) {
	}

	public float CalculateSuspensionForce() {
		return m_suspensionDistance;
	}

	public Vector3 GetPosition() {
		return m_anchor + new Vector3(0, m_anchorOffset, 0);
	}

	private void OnDrawGizmosSelected() {
		Gizmos.matrix = transform.parent.localToWorldMatrix;
		Gizmos.DrawLine(GetPosition(), GetPosition() + new Vector3(0, -m_radius, 0));

		Gizmos.DrawLine(GetPosition(), GetPosition() + new Vector3(m_grossor, 0, 0));
		Gizmos.DrawLine(GetPosition(), GetPosition() + new Vector3(-m_grossor, 0, 0));
		Gizmos.DrawLine(GetPosition() + new Vector3(m_grossor, 0, 0), GetPosition() + new Vector3(m_grossor, -m_radius, 0));
		Gizmos.DrawLine(GetPosition() + new Vector3(-m_grossor, 0, 0), GetPosition() + new Vector3(-m_grossor, -m_radius, 0));
	}
}
