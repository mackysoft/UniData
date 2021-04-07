﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

namespace MackySoft.UniData.Internal.Editor {
	public abstract class SettingsProviderBase : SettingsProvider {

		static readonly Lazy<GUIStyle> k_SettingsStyle = new Lazy<GUIStyle>(() => {
			var style = new GUIStyle {
				margin = new RectOffset(10,10,0,0)
			};
			return style;
		});

		UnityEditor.Editor m_SettingsEditor;

		protected SettingsProviderBase (string path,SettingsScope scopes,IEnumerable<string> keywords = null) : base(path,scopes,keywords) {
		}

		public override void OnActivate (string searchContext,VisualElement rootElement) {
			m_SettingsEditor = UnityEditor.Editor.CreateEditor(GetSettings());
		}

		public override void OnDeactivate () {
			base.OnDeactivate();
			if (m_SettingsEditor != null) {
				UnityEngine.Object.DestroyImmediate(m_SettingsEditor);
				m_SettingsEditor = null;
			}
		}

		public override void OnGUI (string searchContext) {
			if (m_SettingsEditor == null) {
				return;
			}

			EditorGUILayout.BeginVertical(k_SettingsStyle.Value);

			m_SettingsEditor.OnInspectorGUI();

			EditorGUILayout.EndVertical();
		}

		protected abstract UnityEngine.Object GetSettings ();

	}
}