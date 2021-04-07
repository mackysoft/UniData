#pragma warning disable CA1819 // Properties should not return arrays

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MackySoft.UniData.Internal.Editor {

	public class TypePopup {

		Type[] m_Types;
		GUIContent[] m_TypeDisplayNames;
		string[] m_TypeFullNames;

		public Type[] Types => m_Types;
		public string[] TypeFullNames => m_TypeFullNames;

		public bool DisplayNullAtFirst { get; set; }

		public GUIContent NullDisplayLabel { get; set; } = GUIContentUtility.NullContent;

		public TypePopup () {

		}

		public TypePopup (IEnumerable<Type> types,bool displayNullAtFirst) {
			DisplayNullAtFirst = displayNullAtFirst;
			SetTypes(types);
		}

		public void SetTypes (IEnumerable<Type> types) {
			if (DisplayNullAtFirst) {
				types = types?.Prepend(null);
			}

			m_Types = types?.OrderByType().ToArray();

			var builder = new StringBuilder();
			m_TypeDisplayNames = m_Types?.Select(type => {
				if (type == null) {
					return NullDisplayLabel;
				}
				AddTypeMenuAttribute typeDisplayName = TypeMenuUtility.GetAttribute(type);
				if (typeDisplayName != null) {
					return new GUIContent(ObjectNames.NicifyVariableName(typeDisplayName.MenuName));
				}

				builder.Clear();

				string[] names = type.FullName.Split('.');
				for (int i = 0;names.Length > i;i++) {
					// Append type name.
					bool isLastIndex = (i == (names.Length - 1));
					builder.Append(isLastIndex ? ObjectNames.NicifyVariableName(names[i]) : names[i]);

					if (isLastIndex) {
						break;
					}

					// Append sparator.
					builder.Append((i == (names.Length - 2)) ? '/' : '.');
				}

				return new GUIContent(builder.ToString());
			}).ToArray();

			m_TypeFullNames = m_Types?.Select(type => (type != null) ? $"{type.Assembly.ToString().Split(',')[0]} {type.FullName}" : string.Empty).ToArray();
		}

		public int Do (Rect position,string currentFullTypeName) {
			int currentIndex = Array.IndexOf(m_TypeFullNames,currentFullTypeName);
			return EditorGUI.Popup(position,currentIndex,m_TypeDisplayNames);
		}

		public int Do (Rect position,string currentFullTypeName,GUIStyle style) {
			int currentIndex = Array.IndexOf(m_TypeFullNames,currentFullTypeName);
			return EditorGUI.Popup(position,currentIndex,m_TypeDisplayNames,style);
		}

		public void Do (Rect position,string currentFullTypeName,Action<int> changedCallback) {
			int currentIndex = Array.IndexOf(m_TypeFullNames,currentFullTypeName);
			
			var popupLabel = new GUIContent(m_TypeDisplayNames[currentIndex].text.Split('/').Last());
			if (EditorGUI.DropdownButton(position,popupLabel,FocusType.Passive)) {
				var menu = new GenericMenu();

				if (DisplayNullAtFirst) {
					menu.AddItem(NullDisplayLabel,false,() => {
						if (currentIndex != 0) {
							changedCallback.Invoke(0);
						}
					});
					menu.AddSeparator("");
				}

				AddTo(menu,currentFullTypeName,userData => {
					int index = Array.IndexOf(m_Types,(Type)userData);
					if (currentIndex != index) {
						changedCallback.Invoke(index);
					}
				});

				menu.DropDown(position);
			}
		}

		public GenericMenu AddTo (GenericMenu menu,string currentFullTypeName,GenericMenu.MenuFunction2 callback) {
			if (menu == null) {
				menu = new GenericMenu();
			}

			bool hasSelection = !string.IsNullOrEmpty(currentFullTypeName);
			for (int i = 0;m_Types.Length > i;i++) {
				if (m_Types[i] == null) {
					continue;
				}
				menu.AddItem(m_TypeDisplayNames[i],hasSelection && (currentFullTypeName == m_TypeFullNames[i]),callback,m_Types[i]);
			}

			return menu;
		}

	}
}