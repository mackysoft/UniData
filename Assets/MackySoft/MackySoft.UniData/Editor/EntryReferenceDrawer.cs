using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using MackySoft.UniData.Internal;
using MackySoft.UniData.Internal.Editor;

namespace MackySoft.UniData.Editor {

	[CustomPropertyDrawer(typeof(EntryReference<>),true)]
	public class EntryReferenceDrawer : PropertyDrawer {

		public override void OnGUI (Rect position,SerializedProperty property,GUIContent label) {
			EditorGUI.BeginProperty(position,label,property);

			// Find properties.
			var catalog = property.FindPropertyRelative("m_Catalog");
			var entryId = property.FindPropertyRelative("m_EntryId");

			// Get data ids. Index 0 is none.
			bool hasCatalogReference = (catalog.objectReferenceValue != null);

			// Draw the prefix label.
			EditorGUI.PrefixLabel(position,label);
			position.xMin += EditorGUIUtility.labelWidth;

			// Draw the catalog reference.
			Rect catalogPosition = new Rect(position);
			catalogPosition.xMax -= catalogPosition.width * 0.5f;
			EditorGUI.PropertyField(catalogPosition,catalog,GUIContent.none);

			// Draw the entry id popup.
			Rect entryIdPosition = new Rect(position);
			entryIdPosition.xMin += entryIdPosition.width * 0.5f;

			string popupLabel = !string.IsNullOrWhiteSpace(entryId.stringValue) ? entryId.stringValue : GUIContentUtility.NoneContent.text;

			if (hasCatalogReference) {
				var dataCatalog = catalog.objectReferenceValue as DataCatalog;
				
				if (GUI.Button(entryIdPosition,popupLabel,EditorStyles.popup)) {
					var menu = new GenericMenu();

					menu.AddItem(GUIContentUtility.NoneContent,string.IsNullOrWhiteSpace(entryId.stringValue),() => {
						entryId.stringValue = string.Empty;
						entryId.serializedObject.ApplyModifiedProperties();
					});
					menu.AddSeparator("");

					IEnumerable<string> entryIds = dataCatalog
						.Where(IsValidEntry)
						.Select(entry => entry.Id);
					foreach (var id in entryIds) {
						menu.AddItem(new GUIContent(id),(entryId.stringValue == id),userData => {
							entryId.stringValue = (string)userData;
							entryId.serializedObject.ApplyModifiedProperties();
						},id);
					}

					menu.DropDown(entryIdPosition);
				}
			} else {
				// Draw none popup.
				EditorGUI.BeginDisabledGroup(true);
				GUI.Button(entryIdPosition,popupLabel,EditorStyles.popup);
				EditorGUI.EndDisabledGroup();
			}

			EditorGUI.EndProperty();
		}

		bool IsValidEntry (IEntry entry) {
			if (string.IsNullOrWhiteSpace(entry.Id)) {
				return false;
			}

			// Get a field type that is not a collection.
			Type fieldEntryReferenceType = fieldInfo.FieldType.IsCollectionType() ? fieldInfo.FieldType.GetCollectionElementType() : fieldInfo.FieldType;

			// Get EntryReference<T> which is the base type of fieldEntryReferenceType.
			Type entryReferenceType = fieldEntryReferenceType.FindGenericTypeFromDefinition(typeof(EntryReference<>));

			// Get generic argument of the EntryReference<T>.
			Type entryReferenceTypeArgument = entryReferenceType.GetGenericArguments()[0];

			return entryReferenceTypeArgument.IsAssignableFromRecursive(entry.GetType());
		}
	}
}