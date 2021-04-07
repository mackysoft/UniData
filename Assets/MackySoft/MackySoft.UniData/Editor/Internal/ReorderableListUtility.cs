using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace MackySoft.UniData.Internal.Editor {

	public static class ReorderableListUtility {

		public static ReorderableList CreateDefaultList (SerializedProperty source) {
			return CreateDefaultList(source.serializedObject,source);
		}
		
		public static ReorderableList CreateDefaultList (SerializedObject serializedObject,SerializedProperty elements) {
			var list = new ReorderableList(serializedObject,elements) {
				drawHeaderCallback = (rect) => EditorGUI.LabelField(rect,elements.displayName),
				drawElementCallback = (rect,index,isActive,isFocused) => {
					var element = elements.GetArrayElementAtIndex(index);
					rect.height -= 4;
					rect.y += 2;
					EditorGUI.PropertyField(rect,element);
				}
			};
			return list;
		}

	}

}