using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UnityEditor {
	[CustomPropertyDrawer(typeof(AxleInfo))]
	internal class AxleInfoDrawer : PropertyDrawer {
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			EditorGUI.BeginProperty(position, label, property);

			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			EditorGUI.PropertyField(position, property.FindPropertyRelative("leftWheel"));
			EditorGUI.PropertyField(position, property.FindPropertyRelative("rightWheel"));
			EditorGUI.PropertyField(position, property.FindPropertyRelative("motor"));
			EditorGUI.PropertyField(position, property.FindPropertyRelative("steering"));

			EditorGUI.EndProperty();
		}
	}
}