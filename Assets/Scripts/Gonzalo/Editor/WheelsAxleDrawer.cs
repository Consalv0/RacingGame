using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UnityEditor {
	[CustomPropertyDrawer(typeof(WheelsAxle))]
	internal class WheelsAxleDrawer : PropertyDrawer {

		public override float GetPropertyHeight (SerializedProperty property, GUIContent label) {
			return EditorGUIUtility.singleLineHeight * 4 + EditorGUIUtility.standardVerticalSpacing * 4;
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			EditorGUI.BeginProperty(position, label, property);

			GUIContent emptyContent = new GUIContent ("");
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			float standarSpace = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

			Rect collidersLabel = new Rect (position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
			Rect leftRect = new Rect (position.x, position.y + standarSpace * 1, position.width / 2, EditorGUIUtility.singleLineHeight);
			Rect rightRect = new Rect (position.x + position.width / 2, position.y + standarSpace * 1, position.width / 2, EditorGUIUtility.singleLineHeight);
			Rect motorRect = new Rect (position.x, position.y + standarSpace * 2, position.width, EditorGUIUtility.singleLineHeight);
			Rect steeringRect = new Rect (position.x, position.y + standarSpace * 3, position.width, EditorGUIUtility.singleLineHeight);

			EditorGUI.LabelField (collidersLabel, new GUIContent ("Wheel Behaviors", "Left / Right"));
			EditorGUI.PropertyField(leftRect, property.FindPropertyRelative("leftWheel"), emptyContent);
			EditorGUI.PropertyField(rightRect, property.FindPropertyRelative("rightWheel"), emptyContent);
			EditorGUI.PropertyField(motorRect, property.FindPropertyRelative("motor"));
			EditorGUI.PropertyField(steeringRect, property.FindPropertyRelative("steering"));

			EditorGUI.EndProperty();
		}
	}
}