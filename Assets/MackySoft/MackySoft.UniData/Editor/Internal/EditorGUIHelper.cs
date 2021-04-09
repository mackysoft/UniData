using System;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace MackySoft.UniData.Internal.Editor {
	public static class EditorGUIHelper {

		/// <summary>
		/// Makes the appropriate fields for the specified object type.
		/// </summary>
		/// <returns> Changed value of the object. </returns>
		public static object GenericObjectField (Rect position,object value) {
			object changedValue = null;
			switch (value) {
				case bool boolValue:
					changedValue = EditorGUI.Toggle(position,boolValue);
					break;
				case int intValue:
					changedValue = EditorGUI.IntField(position,intValue);
					break;
				case long longValue:
					changedValue = EditorGUI.LongField(position,longValue);
					break;
				case byte byteValue:
					changedValue = (byte)Mathf.Clamp(EditorGUI.IntField(position,byteValue),byte.MinValue,byte.MaxValue);
					break;
				case float floatValue:
					changedValue = EditorGUI.FloatField(position,floatValue);
					break;
				case double doubleValue:
					changedValue = EditorGUI.DoubleField(position,doubleValue);
					break;
				case string stringValue:
					changedValue = EditorGUI.TextField(position,stringValue);
					break;
				case Vector2 vector2Value:
					changedValue = EditorGUI.Vector2Field(position,GUIContent.none,vector2Value);
					break;
				case Vector3 vector3Value:
					changedValue = EditorGUI.Vector3Field(position,GUIContent.none,vector3Value);
					break;
				case Vector4 vector4Value:
					changedValue = EditorGUI.Vector4Field(position,GUIContent.none,vector4Value);
					break;
				case Vector2Int vector2IntValue:
					changedValue = EditorGUI.Vector2IntField(position,GUIContent.none,vector2IntValue);
					break;
				case Vector3Int vector3IntValue:
					changedValue = EditorGUI.Vector3IntField(position,GUIContent.none,vector3IntValue);
					break;
				case Quaternion quaternionValue:
					Vector3 vector3 = EditorGUI.Vector3Field(position,GUIContent.none,quaternionValue.eulerAngles);
					changedValue = Quaternion.Euler(vector3);
					break;
				case Rect rectValue:
					changedValue = EditorGUI.RectField(position,rectValue);
					break;
				case RectInt rectIntValue:
					changedValue = EditorGUI.RectIntField(position,rectIntValue);
					break;
				case Bounds boundsValue:
					changedValue = EditorGUI.BoundsField(position,boundsValue);
					break;
				case BoundsInt boundsIntValue:
					changedValue = EditorGUI.BoundsIntField(position,boundsIntValue);
					break;
				case Color colorValue:
					changedValue = EditorGUI.ColorField(position,colorValue);
					break;
				case Gradient gradientValue:
					changedValue = EditorGUI.GradientField(position,gradientValue);
					break;
				case Enum enumValue:
					if (Attribute.IsDefined(enumValue.GetType(),typeof(FlagsAttribute))) {
						changedValue = EditorGUI.EnumFlagsField(position,enumValue);
					} else {
						changedValue = EditorGUI.EnumPopup(position,enumValue);
					}
					break;
				case AnimationCurve animationCurveValue:
					changedValue = EditorGUI.CurveField(position,animationCurveValue);
					break;
				case LayerMask layerMaskValue:
					changedValue = (LayerMask)EditorGUI.MaskField(position,layerMaskValue,InternalEditorUtility.layers);
					break;
			}
			return changedValue;
		}

	}
}