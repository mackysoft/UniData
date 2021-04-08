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

	/// <summary>
	/// <para> A reference to an entry in the <see cref="DataCatalog"/>. </para>
	/// </summary>
	/// <typeparam name="T"> Type of entry to reference. </typeparam>
	public interface IEntryReference <out T> : IReadOnlyEntryReference<T> where T : IEntry {
		new DataCatalog Catalog { get; set; }
		new string EntryId { get; set; }
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

	/// <summary>
	/// <para> A reference to an entry in the <see cref="DataCatalog"/>. </para>
	/// </summary>
	/// <typeparam name="T"> Type of entry to reference. </typeparam>
	public interface IReadOnlyEntryReference : IReadOnlyEntryReference<IEntry> {

	}

	/// <summary>
	/// <para> A reference to an entry in the <see cref="DataCatalog"/>. </para>
	/// </summary>
	/// <typeparam name="T"> Type of entry to reference. </typeparam>
	public interface IEntryReference : IEntryReference<IEntry> {

	}

	/// <summary>
	/// An EntryReference that can refer to entries of all types.
	/// </summary>
	[Serializable]
	public class EntryReference : EntryReference<IEntry>, IEntryReference {

	}

	public static class EntryReferenceExtensions {

		/// <summary>
		/// <para> Get the referenced entry casted to the specified type. </para>
		/// </summary>
		/// <returns> Returns the referenced entry that has been cast to the specified type. If the entry cannot be cast, default value is returned. </returns>
		public static T GetEntry<T> (this IReadOnlyEntryReference<IEntry> source) where T : IEntry {
			return source.Entry is T entry ? entry : default;
		}

		/// <summary>
		/// Try to cast the referenced entry to the specified type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="result"> The referenced entry that was cast. </param>
		/// <returns> Returns True if the referenced entry could be cast to the specified type. Otherwise, it returns False. </returns>
		public static bool TryGetEntry<T> (this IReadOnlyEntryReference<IEntry> source,out T result) where T : IEntry {
			result = source.GetEntry<T>();
			return result != null;
		}

	}

}