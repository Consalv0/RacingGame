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
	[Range(0, 1)]
	public float compressionFrictionFactor = 0;
	[Range(0, 1)]
	public float forwardStiffnessFactor = 0;
	[Range(0, 1)]
	public float sidewaysStiffnessFactor = 0;
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
	private float m_forwardSlip;
	private float m_usedForwardFriction;
	private float m_sideSlip;
	private float m_usedSideFriction;

    public float debugSlipForceX = 0;
    public float debugSlipForceY = 0;
    public float debugSlipForceZ = 0;

	private void Start () {
		m_wheelCircumference = radius * Mathf.PI * 2;
	}

	private void SolveGroundCollision() {
		Vector3 down = transform.TransformDirection (-Vector3.up);
		RaycastHit hit;

		if (Physics.Raycast (GroundRay, out hit, radius + springRadius, gameObject.layer)) {
			m_grounded = true;

			// calculate spring compression
			// difference in positions divided by total suspension range
			float compression = hit.distance / (springRadius + radius);
			compression = -compression + 1;

			// final force
			Vector3 force = -down * compression * springForce;
			// velocity at point of contact transformed into local space

			Vector3 t = transform.InverseTransformDirection (m_localVelocity);

			// local x and z directions = 0
			t.z = t.x = 0;

			// back to world space * -damping
			// this force simulates the force exerted by the friction in the suspension.
			Vector3 shockDrag = transform.TransformDirection (t) * -damper;

			// forwardDifference = local speed along the z axis
			float forwardDifference = transform.InverseTransformDirection(m_localVelocity).z - m_localVelocity.magnitude;

			// newForwardFriction = interpolated between forwardFriction (constant) and
			// increase the current working friction value depending on how hard the shock is pressing the wheel against the ground
			float newForwardFriction = Mathf.Lerp(chassisRigidbody.mass / 10, chassisRigidbody.mass / 10 * compression * 1, compressionFrictionFactor);
			m_usedForwardFriction = Mathf.Lerp(m_usedForwardFriction, newForwardFriction, Time.fixedDeltaTime);

			// calculate one component of the friction force: the difference between the wheel surface velocity and ground surface 
			// velocity times friction (not based on real physics AFAIK)
			Vector3 forwardForce = transform.TransformDirection(new Vector3(0, 0, -forwardDifference)) * m_usedForwardFriction;

			// calculate how much we be slippin  (if the force is high, the tire will give and slip on the road)
			m_forwardSlip = Mathf.Lerp(forwardForce.magnitude, forwardDifference, forwardStiffnessFactor) / chassisRigidbody.mass / 10;

			// forwardDifference = local speed along the x axis
			float sidewaysDifference = transform.InverseTransformDirection(m_localVelocity).x;
			float newSideFriction = Mathf.Lerp(chassisRigidbody.mass / 10, chassisRigidbody.mass / 10 * compression * 1, compressionFrictionFactor);
			m_usedSideFriction = Mathf.Lerp(m_usedSideFriction, newSideFriction, Time.fixedDeltaTime);

			Vector3 sideForce = transform.TransformDirection(new Vector3(-sidewaysDifference, 0, 0)) * m_usedSideFriction;
			m_sideSlip = Mathf.Lerp(sideForce.magnitude, sidewaysDifference, sidewaysStiffnessFactor) / chassisRigidbody.mass / 10;

            debugSlipForceX = sideForce.x;
            debugSlipForceY = sideForce.y;
            debugSlipForceZ = sideForce.z;

			Debug.DrawRay (transform.position, forwardForce, Color.yellow);

			// this thing is totally made up. It's as if god's hand nudges the car back on course whenever it is moving sideways, 
			// by a factor of sidewaysDamp

            //puede calcularse porque en algun momento la velocidad del derrape (sobre el vecotr myright va a ser 0 pues la aceleracion va a empujarlo hacia el frente del auto
			t = transform.InverseTransformDirection(m_localVelocity);
			t.z = 0;
			t.y = 0;

			Vector3 sideDrag = transform.TransformDirection(t) * -chassisRigidbody.mass / 10;

			// By the some of all you combined, I become CAPTAIN FORCE
			chassisRigidbody.AddForceAtPosition (-m_localVelocity * sideFriction, hit.point);
			chassisRigidbody.AddForceAtPosition(force + shockDrag - forwardForce + sideForce + sideDrag, transform.position);

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
