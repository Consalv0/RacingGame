using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WheelBehaviour))]
[CanEditMultipleObjects]
internal class WheelBahviourEditor : Editor {
	WheelBehaviour targetScript {
		get { return serializedObject.targetObject as WheelBehaviour; }
	}
	MeshCollider wheelCollider {
		get { return targetScript.GetComponent<MeshCollider>(); }
	}

	public override void OnInspectorGUI() {
		serializedObject.Update();
		DrawDefaultInspector();
		RecalculateGUI();

		if (targetScript.suspensionDistance < 0) {
			targetScript.suspensionDistance = 0;
		}

		serializedObject.ApplyModifiedProperties();

		if (wheelCollider) {
			if (wheelCollider.isTrigger) {
				EditorGUILayout.HelpBox("Collider can't be trigger", MessageType.Error);
			}
		} else {
			EditorGUILayout.HelpBox("Can't find collider", MessageType.Error);
		}
	}

	private void RecalculateGUI() {
		if (!wheelCollider) return;
		if (!targetScript.chassisRigidbody) return;
		if (targetScript.anchor.Equals(targetScript.transform.localPosition) 
		    && targetScript.grossor.Equals(wheelCollider.bounds.extents.x)
		    && targetScript.radius.Equals(wheelCollider.bounds.extents.y)
		   ) return;

		if (GUILayout.Button("Recalculate Properties")) {
			targetScript.anchor = targetScript.transform.localPosition;

			targetScript.grossor = wheelCollider.bounds.extents.x;
			targetScript.radius = wheelCollider.bounds.extents.y;
		}
	}
}
