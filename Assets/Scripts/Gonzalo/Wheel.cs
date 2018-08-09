using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour {
	private Rigidbody parentRigidbody;
	private MeshCollider meshCollider;
	public Vector3 pivot;
	public float collisionRadius = 0.2F;
	public float radius = 0.5F;
	public float grossor = 0.1F;
	public AnimationCurve sideFriction;

	private void Awake () {
		parentRigidbody = GetComponentInParent<Rigidbody> ();
		meshCollider = GetComponentInParent<MeshCollider> ();
	}

	private void FixedUpdate () {
		if (IsGrounded) {
			Vector3 normalDirection = parentRigidbody.velocity.normalized;
			normalDirection = Vector3.ProjectOnPlane (normalDirection, parentRigidbody.transform.up).normalized;
			float totalFriction = Mathf.Abs (Vector3.Dot (normalDirection, transform.right));
			Debug.DrawLine (transform.position, transform.position + -transform.up);
			meshCollider.material.dynamicFriction = sideFriction.Evaluate(totalFriction);
		}
	}

	public bool IsGrounded {
		get {
			return Physics.SphereCast (GroundRay, collisionRadius, radius, gameObject.layer);
		}
	}

	private Ray GroundRay {
		get {
			return new Ray (transform.position + pivot, transform.parent.rotation * Vector3.down);
		}
	}

	private void OnDrawGizmosSelected () {
		Gizmos.color = Color.red;
		Gizmos.DrawSphere (GroundRay.origin + GroundRay.direction * radius, collisionRadius);
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere (GroundRay.origin, radius);
		Gizmos.color = Color.white;
	}
}
