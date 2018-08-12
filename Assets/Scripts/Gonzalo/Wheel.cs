using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour {
	public Vector3 pivot;
	public Transform visual;
	public Rigidbody chassisRigidbody;
	[Header ("Wheel")]
	public float collisionRadius = 0.2F;
	public float radius = 0.5F;
	public float grossor = 0.1F;
	public AnimationCurve sideFrictionCurve = AnimationCurve.EaseInOut (0, -1, 1, 1);
	public AnimationCurve forwardFrictionCurve = AnimationCurve.EaseInOut (0, -1, 1, 1);
	[Header("Spring")]
	public float springRadius = 0.125F;
	public float springElasticity = 1;
	public float springForce = 5000;
	public float damper = 1000;
	[Range(0, 1)]
	public float damping = 1;

	public bool debug;

	[System.NonSerialized] public float steerAngle;
	[System.NonSerialized] public float torque;

	private bool m_grounded;
	private Vector3 m_springVelocity;
	private float m_wheelCircumference;
	private Vector3 m_localVelocity;

	private void Start () {
		m_wheelCircumference = radius * Mathf.PI * 2;
	}

	private void SolveGroundCollision() {
		Vector3 downwards = transform.TransformDirection (-Vector3.up);
		RaycastHit hit;

		if (Physics.Raycast (GroundRay, out hit, radius, gameObject.layer)) {
			m_grounded = true;
			// the velocity at point of contact
			Vector3 velocityAtTouch = chassisRigidbody.GetPointVelocity (hit.point);

			// calculate spring compression
			// difference in positions divided by total suspension range
			float compression = hit.distance / (springRadius + radius);
			compression = -compression + 1;

			// final force
			Vector3 force = -downwards * compression * springForce;
			// velocity at point of contact transformed into local space

			Vector3 t = transform.InverseTransformDirection (velocityAtTouch);

			// local x and z directions = 0
			t.z = t.x = 0;

			// back to world space * -damping
			Vector3 damping = transform.TransformDirection (t) * -damper;
			Vector3 finalForce = force + damping;

			// VERY simple turning - force rigidbody in direction of wheel
			t = chassisRigidbody.transform.InverseTransformDirection (velocityAtTouch);
			t.y = 0;
			t.z = 0;

			t = visual.TransformDirection (t);

			// forwardDifference = local speed along the z axis
			// the difference between the speed of the ground and the wheel taking rotation + the car's velocity into account. 
			// (in local space, that means, relative to the wheel. So if you were standing on the wheel surface, 
			// how fast would you percieve the ground moving? Either squashing you every time the wheel goes around 
			// (not skidding, absolute value close to zero) or scrapping you to bits (skidding, large absolute value))

			float forwardDifference = transform.InverseTransformDirection (velocityAtTouch).z - m_localVelocity.z;
			Vector3 forwardForce = transform.TransformDirection (new Vector3 (0, 0, -forwardDifference));

			Debug.DrawRay (transform.position, forwardForce, Color.yellow);

			chassisRigidbody.AddForceAtPosition (finalForce + (t), hit.point);
			chassisRigidbody.AddForceAtPosition (-forwardForce * sideFriction * chassisRigidbody.mass, hit.point);
			// chassisRigidbody.AddForceAtPosition (-velocityAtTouch * sideFriction * chassisRigidbody.mass, hit.point);

			pivot = downwards * (hit.distance - radius);


		}
		else {
			m_grounded = false;
			m_springVelocity += -springElasticity * (pivot - Vector3.down * springRadius) - damping * m_springVelocity;
			pivot += m_springVelocity * Time.fixedDeltaTime;
		}
	}
	
	public float sideFriction {
		get {
			return sideFrictionCurve.Evaluate (
				Vector3.Dot (Vector3.ProjectOnPlane(m_localVelocity, transform.up).normalized, transform.forward));
		}
	}

	private void FixedUpdate () {
		m_localVelocity = chassisRigidbody.GetPointVelocity (transform.position);
		SolveGroundCollision ();
	}

	private void LateUpdate () {
		transform.localRotation = Quaternion.Euler (0, steerAngle, 0);

		if (visual) {
			visual.localPosition = pivot;
			visual.Rotate (360 * (Vector3.Dot( transform.forward, m_localVelocity) / m_wheelCircumference) * Time.deltaTime, 0, 0);
		}

		if (debug) {
			Debug.DrawRay (transform.position, Vector3.ProjectOnPlane (m_localVelocity, transform.up).normalized, Color.green);
			Vector3 wheelForward = Quaternion.Euler ( transform.TransformVector (new Vector3 (0, steerAngle, 0)) ) * transform.forward;
			Debug.DrawRay (transform.position, wheelForward, Color.red);

			// Debug.DrawRay (transform.position, Vector3.Project(chassisRigidbody.velocity, wheelForward), Color.magenta);
		}
	}

	public bool IsGrounded {
		get {
			return m_grounded;
		}
	}

	private Ray GroundRay {
		get {
			return new Ray (transform.position, -transform.up);
		}
	}

	private void OnDrawGizmosSelected () {
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere (GroundRay.origin + GroundRay.direction * radius, collisionRadius);
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere (GroundRay.origin, radius);
		Gizmos.color = Color.white;
	}
}
