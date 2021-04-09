using System;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace MackySoft.UniData.Editor {

	[CustomEditor(typeof(DataCatalog),true)]
	public class DataCatalogInspector : UnityEditor.Editor {

		static readonly Lazy<GUIStyle> k_OpenButtonStyle = new Lazy<GUIStyle>(() => {
			var style = new GUIStyle(GUI.skin.button) {
				fontSize = EditorStyles.largeLabel.fontSize,
				margin = new RectOffset(80,80,10,10)
			};
			return style;
		});

		SerializedDataCatalog m_SerializedCatalog;
		SerializedProperty m_Id;
		ReorderableList m_EntriesList;

		void OnEnable () {
			m_SerializedCatalog = new SerializedDataCatalog(target as DataCatalog);
			
			m_Id = m_SerializedCatalog.SerializedCatalog.FindProperty("m_Id");

			m_EntriesList = new ReorderableList(m_SerializedCatalog.SerializedObject,m_SerializedCatalog.Entries) {
				drawHeaderCallback = (rect) => EditorGUI.LabelField(rect,m_SerializedCatalog.SerializedEntries.displayName),
				drawElementCallback = (rect,index,isActive,isFocused) => {
					var element = m_EntriesList.serializedProperty.GetArrayElementAtIndex(index);
					rect.xMin += 10f;
					EditorGUI.PropertyField(rect,element);
				},
				elementHeightCallback = (index) => {
					var element = m_EntriesList.serializedProperty.GetArrayElementAtIndex(index);
					return EditorGUI.GetPropertyHeight(element);
				}
			};
		}
		
		public override void OnInspectorGUI () {
			m_SerializedCatalog.SerializedObject.Update();

			// Draw the open button. (Pro version only)
			if (UniDataEditorUtility.IsProVersion) {
				if (GUILayout.Button($"Open with {nameof(DataCatalogWindow)}",k_OpenButtonStyle.Value,GUILayout.MinHeight(30f))) {
					DataCatalogEditorUtility.FocusInWindow(m_SerializedCatalog.Catalog,true);
				}
			}

			// Draw the id property.
			EditorGUILayout.BeginVertical(GUI.skin.box);
			{
				EditorGUI.BeginDisabledGroup(Application.isPlaying);
				EditorGUI.BeginChangeCheck();
				EditorGUILayout.DelayedTextField(m_Id);
				if (EditorGUI.EndChangeCheck()) {
					DataCatalogEditorUtility.ForceDetectCatalogs();
				}
				EditorGUI.EndDisabledGroup();

				if (string.IsNullOrWhiteSpace(m_Id.stringValue)) {
					EditorGUILayout.HelpBox("Id is null or white space.",MessageType.Error);
				} else if (DataCatalogEditorUtility.HasDuplication(m_Id.stringValue)) {
					EditorGUILayout.HelpBox($"Id \"{m_Id.stringValue}\" is duplicated with other catalogs.",MessageType.Error);
				} else {
					EditorGUILayout.HelpBox("Id is a catalog identifier used to save and load catalog data.\nIf you change it carelessly, you may lose the saved data.",MessageType.Info);
				}
			}
			EditorGUILayout.EndVertical();

			EditorGUILayout.Space();

			// Draw the entries list.
			m_EntriesList.serializedProperty = m_SerializedCatalog.Entries;
			m_EntriesList.DoLayoutList();

			m_SerializedCatalog.SerializedObject.ApplyModifiedProperties();
		}

	}
}