#pragma warning disable CA1819 // Properties should not return arrays

using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using MackySoft.UniData.IO;
using MackySoft.UniData.Internal;
using MackySoft.UniData.Internal.Editor;

namespace MackySoft.UniData.Editor {

	/// <summary>
	/// A class that provides editor utilities related to <see cref="DataCatalog"/>.
	/// </summary>
	public static class DataCatalogEditorUtility {

		[InitializeOnLoadMethod]
		static void Initialize () {
			EditorApplication.projectChanged -= ForceDetectCatalogs;
			EditorApplication.projectChanged += ForceDetectCatalogs;
		}

		const string k_CreateCatalogPathKey = "UniData_CreateCatalogPathKey";

		static DataCatalog[] m_Catalogs;
		static GUIContent[] m_CatalogDisplayNames;

		/// <summary>
		/// All detected catalogs.
		/// </summary>
		public static DataCatalog[] Catalogs {
			get {
				DetectCatalogs();
				return m_Catalogs;
			}
		}

		/// <summary>
		/// <para> Display names for all detected catalogs. </para>
		/// <para> See: <see cref="Catalogs"/> </para>
		/// </summary>
		public static GUIContent[] CatalogDisplayNames {
			get {
				DetectCatalogs();
				return m_CatalogDisplayNames;
			}
		}

		/// <summary>
		/// Whether one or more catalogs have been detected.
		/// </summary>
		public static bool HasCatalogs => Catalogs.Length > 0;

		/// <summary>
		/// Detects of <see cref="DataCatalog"/>'s in the project.
		/// </summary>
		public static void DetectCatalogs () {
			if (m_Catalogs != null) {
				return;
			}
			ForceDetectCatalogs();
		}

		/// <summary>
		/// Forces the detection of <see cref="DataCatalog"/>'s in the project.
		/// </summary>
		public static void ForceDetectCatalogs () {
			m_Catalogs = AssetDatabase.FindAssets($"t:{nameof(DataCatalog)}")
				.Select(AssetDatabase.GUIDToAssetPath)
				.Select(AssetDatabase.LoadAssetAtPath<DataCatalog>)
				.ToArray();
			m_CatalogDisplayNames = m_Catalogs
				.Select(x => new GUIContent($"{ObjectNames.NicifyVariableName(x.name)} ({x.Id})"))
				.ToArray();
		}

		/// <summary>
		/// Get <see cref="DataCatalog"/> and all its inherited types.
		/// </summary>
		public static IEnumerable<Type> GetCatalogTypes () {
			return ReflectionUtility.GetAllTypes(type => {
				return typeof(DataCatalog).IsAssignableFromRecursive(type);
			});
		}

		/// <summary>
		/// Get the display name of the specified <see cref="DataCatalog"/>.
		/// </summary>
		public static GUIContent GetCatalogDisplayName (DataCatalog catalog) {
			if ((catalog == null) || !HasCatalogs) {
				return GUIContentUtility.NullContent;
			}
			int index = Array.IndexOf(Catalogs,catalog);
			return CatalogDisplayNames[index];
		}

		/// <summary>
		/// Focus the specified <see cref="DataCatalog"/> in the DataCatalogWindow.
		/// </summary>
		/// <param name="forceOpenWindow"> Whether to force a window to open if it is not already open. </param>
		public static void FocusInWindow (DataCatalog catalog,bool forceOpenWindow) {
			Type windowType = ReflectionUtility.GetAllTypes(type => type.FullName == "MackySoft.UniData.Editor.DataCatalogWindow").FirstOrDefault();
			if (windowType == null) {
				return;
			}

			// Get Window
			MethodInfo openMethod = windowType.GetMethod("GetWindow",BindingFlags.Public | BindingFlags.Static);
			object window = openMethod.Invoke(null,new object[] { forceOpenWindow });
			
			if (window != null) {
				// Set Catlaog
				PropertyInfo catalogProperty = windowType.GetProperty("Catalog",BindingFlags.Public | BindingFlags.Instance);
				catalogProperty.SetValue(window,catalog);
			}
		}

		/// <summary>
		/// Save the specified <see cref="DataCatalog"/>.
		/// </summary>
		public static void SaveCatalog (DataCatalog catalog,DataCatalogIOAsset saver) {
			bool ok = EditorUtility.DisplayDialog(
					$"Save \"{ObjectNames.NicifyVariableName(catalog.name)}\" catalog",
					$"Do you want to use \"{saver.name}\" to save the catalog data ?",
					"Save",
					"Cancel"
				);
			if (ok) {
				saver.Save(catalog,DataCatalogIOContext.Default);
				Debug.Log("Saved.");
			}
		}

		/// <summary>
		/// Create a <see cref="DataCatalog"/> of the type specified in the project.
		/// </summary>
		public static DataCatalog CreateCatalog (Type catalogType) {
			string defaultPath = EditorPrefs.GetString(k_CreateCatalogPathKey);
			string path = AssetDatabase.IsValidFolder(defaultPath) ? defaultPath : "Assets/";
			
			string catalogTypeName = ObjectNames.NicifyVariableName(catalogType.Name);

			string filePath = EditorUtility.SaveFilePanelInProject(
				$"Create New {catalogTypeName}",
				$"New {catalogTypeName}.asset",
				"asset",
				"",
				path
			);

			if (filePath.Length <= 0) {
				return null;
			}

			var asset = ScriptableObject.CreateInstance(catalogType) as DataCatalog;
			AssetDatabase.CreateAsset(asset,filePath);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();

			Selection.activeObject = asset;
			EditorGUIUtility.PingObject(asset);

			EditorPrefs.SetString(k_CreateCatalogPathKey,System.IO.Path.GetDirectoryName(filePath));
			
			return asset;
		}

		/// <summary>
		/// Whether there are duplicates in the ID's of the detected catalogs.
		/// </summary>
		public static bool HasDuplication (string id) {
			bool found = false;
			foreach (var catalog in Catalogs) {
				if ((catalog == null) || (catalog.Id != id)) {
					continue;
				}
				if (found) {
					return true;
				} else {
					found = true;
				}
			}
			return false;
		}

	}
}