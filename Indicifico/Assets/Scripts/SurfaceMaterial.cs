using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceMaterial : MonoBehaviour {
	private static SurfaceMaterial defaultSurface;
	public static SurfaceMaterial DefaultSurface {
		get {
			if (defaultSurface == null) {
				defaultSurface = new SurfaceMaterial {
					friction = 1
				};
			}

			return defaultSurface;
		}
	}

	public float friction = 1;
}
