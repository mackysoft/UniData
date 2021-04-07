#if UNIDATA_UNIRX_SUPPORT

using System;
using System.Linq;
using UnityEngine;
using UnityEditor;
using MackySoft.UniData.Internal;

namespace MackySoft.UniData {

	[CustomPropertyDrawer(typeof(ReactiveEntryReference<>),true)]
	public class ReactiveEntryReferenceDrawer : PropertyDrawer {

		const string k_None = "<none>";

		static readonly GUIContent[] k_NoneContents = new GUIContent[] {
			new GUIContent(k_None)
		};

		string[] m_EntryIds;

		public override void OnGUI (Rect position,SerializedProperty property,GUIContent label) {
			EditorGUI.BeginProperty(position,label,property);
			
			// Find properties.
			var catalog = property.FindPropertyRelative("m_Catalog");
			var catalogValue = catalog.FindPropertyRelative("value");
			var achievementId = property.FindPropertyRelative("m_EntryId");
			var achievementIdValue = achievementId.FindPropertyRelative("value");

			// Get achievement ids. Index 0 is none.
			bool hasCatalogReference = (catalogValue.objectReferenceValue != null);
			if ((m_EntryIds == null) && hasCatalogReference && catalogValue.objectReferenceValue is DataCatalog flagCatalog) {
				m_EntryIds = flagCatalog
					.Where(IsValidEntry)
					.Select(x => x.Id)
					.Prepend(k_None)
					.ToArray();
			}

			// Draw the prefix label.
			EditorGUI.PrefixLabel(position,label);
			position.xMin += EditorGUIUtility.labelWidth;

			// Draw the catalog reference.
			Rect catalogPosition = new Rect(position);
			catalogPosition.xMax -= catalogPosition.width * 0.5f;
			EditorGUI.PropertyField(catalogPosition,catalog,GUIContent.none);

			// Draw the entry id popup.
			Rect achievementIdPosition = new Rect(position);
			achievementIdPosition.xMin += achievementIdPosition.width * 0.5f;

			if (hasCatalogReference) {
				int selectedIndex = Array.IndexOf(m_EntryIds,achievementIdValue.stringValue);
				if (selectedIndex < 0) {
					selectedIndex = 0;
				}
				int selection = EditorGUI.Popup(achievementIdPosition,selectedIndex,m_EntryIds);
				achievementIdValue.stringValue = (selection > 0) ? m_EntryIds[selection] : string.Empty;
			} else {
				// Draw none popup.
				EditorGUI.BeginDisabledGroup(true);
				EditorGUI.Popup(achievementIdPosition,0,k_NoneContents);
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
			Type entryReferenceType = fieldEntryReferenceType.FindGenericTypeFromDefinition(typeof(ReactiveEntryReference<>));
			
			// Get generic argument of the EntryReference<T>.
			Type entryReferenceTypeArgument = entryReferenceType.GetGenericArguments()[0];

			return entryReferenceTypeArgument.IsAssignableFromRecursive(entry.GetType());
		}

	}
}
#endif