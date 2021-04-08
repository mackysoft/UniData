#pragma warning disable CA1802 // Use literals where appropriate

using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using MackySoft.UniData.Internal.Editor;

namespace MackySoft.UniData.Editor {

	[CustomEditor(typeof(UniDataSettings))]
	public class UniDataSettingsInspector : UnityEditor.Editor {

		static readonly string k_LoadersDescription =
			$"{nameof(UniDataSettings.Loaders)} is a {nameof(ScriptableObject)} that manages the save / load system for UniData.\n\n" +
			$"When loading, load the {nameof(DataCatalog)} in order from the first loader.\n" +
			$"When saving, save the {nameof(DataCatalog)} using only last loader.";

		static readonly string k_PreloadedCatalogsDescription =
			$"{nameof(UniDataSettings.PreloadedCatalogs)} is an array of catalogs that you want to load automatcally at runtime initialization.";

		SerializedProperty m_Loaders;
		SerializedProperty m_PreloadedCatalogs;
		SerializedProperty m_AutoSaveTiming;
		ReorderableList m_LoadersList;
		ReorderableList m_PreloadedCatalogsList;

		void OnEnable () {
			m_Loaders = serializedObject.FindProperty("m_Loaders");
			m_PreloadedCatalogs = serializedObject.FindProperty("m_PreloadedCatalogs");
			m_AutoSaveTiming = serializedObject.FindProperty("m_AutoSaveTiming");

			m_LoadersList = ReorderableListUtility.CreateDefaultList(m_Loaders);
			m_LoadersList.onCanRemoveCallback = list => (list.count > 1);
			m_LoadersList.drawElementCallback = (rect,index,isActive,isFocused) => {
				var element = m_LoadersList.serializedProperty.GetArrayElementAtIndex(index);
				
				var label = new GUIContent($"Loader {index}");
				if (index == (m_Loaders.arraySize - 1)) {
					label.text += " / Saver";
				}
				EditorGUI.PropertyField(rect,element,label);
			};

			m_PreloadedCatalogsList = ReorderableListUtility.CreateDefaultList(m_PreloadedCatalogs);
		}

		public override void OnInspectorGUI () {
			serializedObject.Update();

			EditorGUILayout.HelpBox(k_LoadersDescription,MessageType.Info);
			m_LoadersList.DoLayoutList();

			EditorGUILayout.Space();

			EditorGUILayout.HelpBox(k_PreloadedCatalogsDescription,MessageType.Info);
			m_PreloadedCatalogsList.DoLayoutList();

			EditorGUILayout.Space();

			EditorGUILayout.PropertyField(m_AutoSaveTiming);

			serializedObject.ApplyModifiedProperties();
		}

	}
}