using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UnityEditor {
	[CustomPropertyDrawer(typeof(Axle))]
	internal class AxleDrawer : PropertyDrawer {

		public override float GetPropertyHeight (SerializedProperty property, GUIContent label) {
			return EditorGUIUtility.singleLineHeight * 6 + EditorGUIUtility.standardVerticalSpacing * 6;
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			EditorGUI.BeginProperty(position, label, property);

			GUIContent emptyContent = new GUIContent ("");
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			float standarSpace = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

			Rect collidersLabel = new Rect (position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
			Rect leftRect = new Rect (position.x, position.y + standarSpace * 1, position.width / 2, EditorGUIUtility.singleLineHeight);
			Rect rightRect = new Rect (position.x + position.width / 2, position.y + standarSpace * 1, position.width / 2, EditorGUIUtility.singleLineHeight);
			Rect visualsLabel = new Rect (position.x, position.y + standarSpace * 2, position.width, EditorGUIUtility.singleLineHeight);
			Rect leftVisualRect = new Rect (position.x, position.y + standarSpace * 3, position.width / 2, EditorGUIUtility.singleLineHeight);
			Rect rightVisualRect = new Rect (position.x + position.width / 2, position.y + standarSpace * 3, position.width / 2, EditorGUIUtility.singleLineHeight);
			Rect motorRect = new Rect (position.x, position.y + standarSpace * 4, position.width, EditorGUIUtility.singleLineHeight);
			Rect steeringRect = new Rect (position.x, position.y + standarSpace * 5, position.width, EditorGUIUtility.singleLineHeight);

			EditorGUI.LabelField (collidersLabel, new GUIContent ("Wheel Colliders", "Left / Right"));
			EditorGUI.PropertyField(leftVisualRect, property.FindPropertyRelative ("leftWheelVisual"), emptyContent);
			EditorGUI.PropertyField(rightVisualRect, property.FindPropertyRelative ("rightWheelVisual"), emptyContent);
			EditorGUI.LabelField (visualsLabel, new GUIContent ("Visuals", "Left / Right"));
			EditorGUI.PropertyField(leftRect, property.FindPropertyRelative("leftWheel"), emptyContent);
			EditorGUI.PropertyField(rightRect, property.FindPropertyRelative("rightWheel"), emptyContent);
			EditorGUI.PropertyField(motorRect, property.FindPropertyRelative("motor"));
			EditorGUI.PropertyField(steeringRect, property.FindPropertyRelative("steering"));

			EditorGUI.EndProperty();
		}
	}
}