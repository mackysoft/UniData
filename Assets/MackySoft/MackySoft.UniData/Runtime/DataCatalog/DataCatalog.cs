using System.Collections.Generic;
using UnityEngine;
using MackySoft.UniData.Internal;

namespace MackySoft.UniData {

	/// <summary>
	/// Provides a collection of data that is flexible and editable in the editor.
	/// </summary>
	[CreateAssetMenu(fileName = nameof(DataCatalog),menuName = "MackySoft/UniData/Data Catalog")]
	[HelpURL("https://mackysoft.github.io/UniData/")]
	public partial class DataCatalog : ScriptableObject, ISerializationCallbackReceiver {

		[Tooltip("The identifier of catalog.")]
		[SerializeField]
		string m_Id;
		
		[SerializeReference, SubclassSelector]
		List<IEntry> m_Entries = new List<IEntry>();

		/// <summary>
		/// The identifier of catalog. 
		/// </summary>
		public string Id => m_Id;

		IList<IEntry> Entries {
			get {
#if UNITY_EDITOR
				if (Application.isPlaying) {
					return EditorRuntimeEntries;
				}
#endif
				return m_Entries;
			}
		}

		void OnDestroy () {
#if UNITY_EDITOR
			DestroyEditorRuntimeCatalog();
#endif
		}

		#region Editor

#if UNITY_EDITOR
		/// <summary>
		/// <para> An instance of a catalog that is used only at editor runtime. </para>
		/// <para> This catalog is destroyed when playmode is exited. </para>
		/// </summary>
		[SerializeField, HideInInspector]
		DataCatalog m_EditorRuntimeCatalog;

		/// <summary>
		/// Entries that are only available at editor runtime.
		/// </summary>
		IList<IEntry> EditorRuntimeEntries {
			get {
				if (Application.isPlaying) {
					if (m_EditorRuntimeCatalog == null) {
						m_EditorRuntimeCatalog = Instantiate(this);
					}
					return m_EditorRuntimeCatalog.m_Entries;
				}
				return null;
			}
		}

		public bool HasEditorRuntimeCatalog => (EditorRuntimeEntries != null);

		void DestroyEditorRuntimeCatalog () {
			if (m_EditorRuntimeCatalog != null) {
				Destroy(m_EditorRuntimeCatalog);
				m_EditorRuntimeCatalog = null;
			}
		}
#endif

		#endregion

		#region ISerializationCallbackReceiver

		void ISerializationCallbackReceiver.OnBeforeSerialize () {
#if UNITY_EDITOR
			if (!Application.isPlaying) {
				DestroyEditorRuntimeCatalog();
			}
#endif
		}

		void ISerializationCallbackReceiver.OnAfterDeserialize () {
			
		}

		#endregion

	}
}