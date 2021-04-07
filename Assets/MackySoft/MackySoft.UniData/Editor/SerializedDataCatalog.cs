#pragma warning disable IDE0074 // 複合代入を使用

using UnityEditor;

namespace MackySoft.UniData.Editor {
	public class SerializedDataCatalog {

		SerializedObject m_SerializedCatalog;
		SerializedProperty m_Entries;

		SerializedObject m_SerializedRuntimeCatalog;
		SerializedProperty m_RuntimeEntries;

		public bool HasRuntimeCatalog => Catalog.HasEditorRuntimeCatalog;

		public DataCatalog Catalog { get; }

		public SerializedObject SerializedObject {
			get {
				if (HasRuntimeCatalog) {
					return SerializedRuntimeCatalog;
				} else {
					return SerializedCatalog;
				}
			}
		}

		public SerializedObject SerializedCatalog {
			get => m_SerializedCatalog ?? (m_SerializedCatalog = new SerializedObject(Catalog));
		}

		public SerializedObject SerializedRuntimeCatalog {
			get {
				if ((m_SerializedRuntimeCatalog == null) && HasRuntimeCatalog) {
					SerializedCatalog.Update();

					var runtimeCatalog = SerializedCatalog.FindProperty("m_EditorRuntimeCatalog").objectReferenceValue;
					m_SerializedRuntimeCatalog = new SerializedObject(runtimeCatalog);
				}
				return m_SerializedRuntimeCatalog;
			}
		}

		public SerializedProperty SerializedEntries {
			get => m_Entries ?? (m_Entries = SerializedCatalog.FindProperty("m_Entries"));
		}

		public SerializedProperty RuntimeEntries {
			get {
				if ((m_RuntimeEntries == null) && HasRuntimeCatalog) {
					m_RuntimeEntries = SerializedRuntimeCatalog.FindProperty("m_Entries");
				}
				return m_RuntimeEntries;
			}
		}

		public SerializedProperty Entries {
			get {
				if (HasRuntimeCatalog) {
					return RuntimeEntries;
				} else {
					return SerializedEntries;
				}
			}
		}

		public SerializedDataCatalog (DataCatalog catalog) {
			Catalog = catalog;
		}

	}
}