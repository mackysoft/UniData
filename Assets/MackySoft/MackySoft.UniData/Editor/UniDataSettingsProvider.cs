#pragma warning disable IDE0051 // 使用されていないプライベート メンバーを削除する

using System.Collections.Generic;
using UnityEditor;
using MackySoft.UniData.Internal.Editor;

namespace MackySoft.UniData.Editor {

	public class UniDataSettingsProvider : SettingsProviderBase {

		public const string k_SettingsPath = "Project/MackySoft/UniData";

		public UniDataSettingsProvider (string path,SettingsScope scopes,IEnumerable<string> keywords = null) : base(path,scopes,keywords) {
			label = "UniData";
		}

		protected override UnityEngine.Object GetSettings () {
			return UniDataSettings.Instance;
		}

		[SettingsProvider]
		static SettingsProvider CreateUniDataSettingsProvider () {
			return new UniDataSettingsProvider(k_SettingsPath,SettingsScope.Project);
		}

	}
}