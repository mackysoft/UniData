using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace MackySoft.UniData.Editor {

	/// <summary>
	/// A class that provides utilities related to UniData editor tools.
	/// </summary>
	public static class UniDataEditorUtility {

		public const string k_MenuItemPath = "Tools/UniData/";
		const string k_DocumentationURL = "https://mackysoft.github.io/UniData/";

		static readonly Lazy<bool> k_IsProVersion = new Lazy<bool>(() => {
			return AppDomain.CurrentDomain.GetAssemblies()
				.Any(assembly => assembly.GetType("MackySoft.UniData.Editor.DataCatalogWindow") != null);
		});

		/// <summary>
		/// Whether the UniData package is a pro version.
		/// </summary>
		public static bool IsProVersion => k_IsProVersion.Value;

		/// <summary>
		/// Open <see cref="UniDataSettings"/> in the editor. 
		/// </summary>
		[MenuItem(k_MenuItemPath + "Open Settings",priority = 1)]
		public static void OpenSettings () {
			SettingsService.OpenProjectSettings(UniDataSettingsProvider.k_SettingsPath);
		}

		/// <summary>
		/// Open the UniData documentation in your web browser.
		/// </summary>
		[MenuItem(k_MenuItemPath + "Open Documentation",priority = 31)]
		public static void OpenDocumentation () {
			Application.OpenURL(k_DocumentationURL);
		}

	}
}