using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceMaterial : MonoBehaviour {
	public WheelFrictionCurve wheelFrictionCurve {
		get {
			WheelFrictionCurve wheelFriction = new WheelFrictionCurve();

			wheelFriction.extremumSlip = extremumSlip;
			wheelFriction.extremumValue = extremumValue;
			wheelFriction.asymptoteSlip = asymptoteSlip;
			wheelFriction.asymptoteValue = asymptoteValue;
			wheelFriction.stiffness = stiffness;

			return wheelFriction;
		}
	}

	//
	// Summary:
	//     Extremum point slip (default 1).
	public float extremumSlip = 0.2F;
	//
	// Summary:
	//     Force at the extremum slip (default 20000).
	public float extremumValue = 1;
	//
	// Summary:
	//     Asymptote point slip (default 2).
	public float asymptoteSlip = 0.5F;
	//
	// Summary:
	//     Force at the asymptote slip (default 10000).
	public float asymptoteValue = 0.75F;
	//
	// Summary:
	//     Multiplier for the extremumValue and asymptoteValue values (default 1).
	public float stiffness = 1;
}