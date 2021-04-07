#pragma warning disable CA1819 // Properties should not return arrays

using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using MackySoft.UniData.IO;
using MackySoft.UniData.Internal;
using MackySoft.UniData.Internal.Editor;

namespace MackySoft.UniData.Editor {
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
		/// All catalogs detected.
		/// </summary>
		public static DataCatalog[] Catalogs {
			get {
				DetectCatalogs();
				return m_Catalogs;
			}
		}

		public static GUIContent[] CatalogDisplayNames {
			get {
				DetectCatalogs();
				return m_CatalogDisplayNames;
			}
		}

		public static bool HasCatalogs => Catalogs.Length > 0;

		public static void DetectCatalogs () {
			if (m_Catalogs != null) {
				return;
			}
			ForceDetectCatalogs();
		}

		public static void ForceDetectCatalogs () {
			m_Catalogs = AssetDatabase.FindAssets($"t:{nameof(DataCatalog)}")
				.Select(AssetDatabase.GUIDToAssetPath)
				.Select(AssetDatabase.LoadAssetAtPath<DataCatalog>)
				.ToArray();
			m_CatalogDisplayNames = m_Catalogs
				.Select(x => new GUIContent($"{ObjectNames.NicifyVariableName(x.name)} ({x.Id})"))
				.ToArray();
		}

		public static IEnumerable<Type> GetCatalogTypes () {
			return ReflectionUtility.GetAllTypes(type => {
				return typeof(DataCatalog).IsAssignableFromRecursive(type);
			});
		}

		public static GUIContent GetCatalogDisplayName (DataCatalog catalog) {
			if ((catalog == null) || !HasCatalogs) {
				return GUIContentUtility.NullContent;
			}
			int index = Array.IndexOf(Catalogs,catalog);
			return CatalogDisplayNames[index];
		}

		public static void FocusInWindow (DataCatalog catalog,bool forceOpenWindow) {
			var window = forceOpenWindow ? DataCatalogWindow.Open() : DataCatalogWindow.Window;
			if (window != null) {
				window.Catalog = catalog;
			}
		}

		public static void SaveCatalog (DataCatalog catalog,DataCatalogIOAsset loader) {
			bool ok = EditorUtility.DisplayDialog(
					$"Save \"{ObjectNames.NicifyVariableName(catalog.name)}\" catalog",
					$"Do you want to use \"{loader.name}\" to save the catalog data ?",
					"Save",
					"Cancel"
				);
			if (ok) {
				loader.Save(catalog,DataCatalogIOContext.Default);
				Debug.Log("Saved.");
			}
		}

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