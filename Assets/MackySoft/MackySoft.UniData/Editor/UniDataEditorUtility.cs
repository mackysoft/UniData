using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace MackySoft.UniData.Editor {
	public static class UniDataEditorUtility {

		public const string k_MenuItemPath = "Tools/UniData/";
		const string k_DocumentationURL = "https://github.com/mackysoft/UniData";

		static readonly Lazy<bool> k_IsProVersion = new Lazy<bool>(() => {
			return AppDomain.CurrentDomain.GetAssemblies()
				.Any(assembly => assembly.GetType("MackySoft.UniData.Editor.DataCatalogWindow") != null);
		});

		/// <summary>
		/// Whether the UniData package is a pro version.
		/// </summary>
		public static bool IsProVersion => k_IsProVersion.Value;

		[MenuItem(k_MenuItemPath + "Open Settings",priority = 1)]
		public static void OpenSettings () {
			SettingsService.OpenProjectSettings(UniDataSettingsProvider.k_SettingsPath);
		}

		[MenuItem(k_MenuItemPath + "Open Documentation",priority = 31)]
		public static void OpenDocumentation () {
			Application.OpenURL(k_DocumentationURL);
		}

	}
}