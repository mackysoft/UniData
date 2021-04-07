using UnityEngine;

namespace MackySoft.UniData.Internal.Editor {
	public static class GUIContentUtility {

		static readonly GUIContent k_NullContent = new GUIContent("<null>");
		static readonly GUIContent k_NoneContent = new GUIContent("<none>");

		public static GUIContent NullContent => k_NullContent;
		public static GUIContent NoneContent => k_NoneContent;
	}
}