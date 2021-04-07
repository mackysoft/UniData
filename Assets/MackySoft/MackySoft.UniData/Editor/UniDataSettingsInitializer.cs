using System;
using UnityEditor;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace MackySoft.UniData.Editor {

    /// <summary>
	/// Automatically create <see cref="UniDataSettings"/>.
	/// </summary>
    class UniDataSettingsInitializer {

		const string k_ParentFolderName = "MackySoft.UniData";

		[InitializeOnLoadMethod]
        static void Initialize () {
            EditorApplication.projectChanged -= CreateSettings;
            EditorApplication.projectChanged += CreateSettings;
		}

        static void CreateSettings () {
#if !UNIDATA_DISABLE_GENERATE_SETTINGS
			var settings = UniDataSettings.Instance;
			if (settings != null) {
				return;
			}

			settings = ScriptableObject.CreateInstance<UniDataSettings>();
			try {
				MonoScript settingsScript = MonoScript.FromScriptableObject(settings);

				// Assets/MackySoft/MackySoft.UniData/Runtime/UniDataSettings.cs
				string settingsScriptPath = AssetDatabase.GetAssetPath(settingsScript);
				int lastIndex = settingsScriptPath.IndexOf(k_ParentFolderName);
				string parentFolderPath = settingsScriptPath.Remove(lastIndex + k_ParentFolderName.Length);

				if (!AssetDatabase.IsValidFolder(parentFolderPath + "/Resources")) {
					AssetDatabase.CreateFolder(parentFolderPath,"Resources");
				}

				AssetDatabase.CreateAsset(settings,$"{parentFolderPath}/Resources/{nameof(UniDataSettings)}.asset");
				AssetDatabase.Refresh();
			} catch (Exception e) {
				UnityObject.DestroyImmediate(settings);
				throw e;
			}
#endif
		}

	}
}