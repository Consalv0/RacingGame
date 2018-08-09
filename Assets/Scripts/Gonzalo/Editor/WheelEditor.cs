using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (Wheel))]
[CanEditMultipleObjects]
internal class WheelEditor : Editor {
	Wheel targetScript {
		get { return serializedObject.targetObject as Wheel; }
	}
	MeshFilter wheelMesh {
		get { return targetScript.GetComponent<MeshFilter> (); }
	}

	public override void OnInspectorGUI () {
		serializedObject.Update ();
		DrawDefaultInspector ();
		RecalculateGUI ();

		serializedObject.ApplyModifiedProperties ();
	}

	private void RecalculateGUI () {
		if (!wheelMesh) return;
		if (targetScript.pivot.Equals (targetScript.transform.localPosition)
			&& targetScript.grossor.Equals (wheelMesh.sharedMesh.bounds.extents.x)
			&& targetScript.radius.Equals (wheelMesh.sharedMesh.bounds.extents.y)
		   ) return;

		if (GUILayout.Button ("Recalculate Properties")) {
			targetScript.pivot = targetScript.transform.localPosition;

			targetScript.grossor = wheelMesh.sharedMesh.bounds.extents.x;
			targetScript.radius = wheelMesh.sharedMesh.bounds.extents.y + 0.1F;
		}
	}
}
