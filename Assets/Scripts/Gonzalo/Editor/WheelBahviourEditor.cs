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

		if (targetScript.m_suspensionDistance < 0) {
			targetScript.m_suspensionDistance = 0;
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
		if (!targetScript.m_chassisRigidbody) return;
		if (targetScript.m_anchor.Equals(targetScript.transform.localPosition) 
		    && targetScript.m_grossor.Equals(wheelCollider.bounds.extents.x)
		    && targetScript.m_radius.Equals(wheelCollider.bounds.extents.y)
		    && targetScript.m_suspensionForce.Equals(targetScript.m_chassisRigidbody.mass)
		   ) return;

		if (GUILayout.Button("Recalculate Properties")) {
			targetScript.m_anchor = targetScript.transform.localPosition;
			targetScript.m_suspensionForce = targetScript.m_chassisRigidbody.mass;

			targetScript.m_grossor = wheelCollider.bounds.extents.x;
			targetScript.m_radius = wheelCollider.bounds.extents.y;
		}
	}
}
