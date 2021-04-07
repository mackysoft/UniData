#pragma warning disable CA2235 // Mark all non-serializable fields

using System;
using UnityEngine;

namespace MackySoft.UniData {

	/// <summary>
	/// <para> A reference to an entry in the <see cref="DataCatalog"/>. </para>
	/// </summary>
	/// <typeparam name="T"> Type of entry to reference. </typeparam>
	public interface IReadOnlyEntryReference<out T> where T : IEntry {
		DataCatalog Catalog { get; }
		string EntryId { get; }
		T Entry { get; }
	}

	public interface IReadOnlyEntryReference : IReadOnlyEntryReference<IEntry> {

	}

	/// <summary>
	/// <para> A reference to an entry in the <see cref="DataCatalog"/>. </para>
	/// </summary>
	/// <typeparam name="T"> Type of entry to reference. </typeparam>
	public interface IEntryReference <out T> : IReadOnlyEntryReference<T> where T : IEntry {
		new DataCatalog Catalog { get; set; }
		new string EntryId { get; set; }
	}

	public interface IEntryReference : IEntryReference<IEntry> {

	}

	/// <summary>
	/// <para> A reference to an entry in the <see cref="DataCatalog"/>. </para>
	/// <para> By using this class, you can easily set and get reference to an entry in the <see cref="DataCatalog"/> in the editor. </para>
	/// </summary>
	/// <typeparam name="T"> Type of entry to reference. </typeparam>
	[Serializable]
	public class EntryReference<T> : IEntryReference<T> where T : IEntry {

		[SerializeField]
		DataCatalog m_Catalog;
		
		[SerializeField]
		string m_EntryId;

		T m_CachedEntry;

		public DataCatalog Catalog {
			get => m_Catalog;
			set {
				if (m_Catalog != value) {
					m_Catalog = value;
					m_CachedEntry = default;
				}
			}
		}

		public string EntryId {
			get => m_EntryId;
			set {
				if (m_EntryId != value) {
					m_EntryId = value;
					m_CachedEntry = default;
				}
			}
		}

		public T Entry {
			get {
				if ((m_CachedEntry == null) && (m_Catalog != null) && !string.IsNullOrWhiteSpace(m_EntryId)) {
					m_CachedEntry = m_Catalog.GetEntry<T>(m_EntryId);
				}
				return m_CachedEntry;
			}
		}

		/// <summary>
		/// Clear the cache of reference to entry.
		/// </summary>
		public void ClearCache () {
			m_CachedEntry = default;
		}

	}

	[Serializable]
	public class EntryReference : EntryReference<IEntry> {
		public T GetEntry<T> () where T : IEntry {
			return Entry is T entry ? entry : default;
		}
	}

}