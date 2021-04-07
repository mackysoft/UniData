#pragma warning disable CA2235 // Mark all non-serializable fields

using System;
using System.Collections.Generic;
using UnityEngine;

namespace MackySoft.UniData.IO {

	/// <summary>
	/// Context for I/O operations to the <see cref="DataCatalog"/>.
	/// </summary>
	[Serializable]
	public class DataCatalogIOContext {

		public static DataCatalogIOContext Default { get; } = new DataCatalogIOContext { Index = -1 };

		[Tooltip("An index for I/O operations on DataCatalog.")]
		[SerializeField]
		int m_Index = -1;

		[Tooltip(
			"Id's of entries that perform I/O operations on DataCatalog.\n" +
			"If is null or empty, it will perform I/O operations on all entries."
		)]
		[SerializeField]
		List<string> m_Ids = new List<string>();

		/// <summary>
		/// <para> An index for I/O operations on <see cref="DataCatalog"/>. </para>
		/// <para> For example, it can be used to create multiple game data. </para>
		/// </summary>
		public int Index { get => m_Index; set => m_Index = value; }

		/// <summary>
		/// <para> Id's of entries that perform I/O operations on <see cref="DataCatalog"/>. </para>
		/// <para> If is null or empty, it will perform I/O operations on all entries. </para>
		/// </summary>
		public IList<string> Ids => m_Ids;

		public IEnumerable<IEntry> GetEntries (DataCatalog catalog) {
			if (m_Ids.Count > 0) {
				return GetEntriesWithIds(catalog);
			} else {
				return catalog;
			}
		}

		IEnumerable<IEntry> GetEntriesWithIds (DataCatalog catalog) {
			for (int i = 0;i < m_Ids.Count;i++) {
				if (catalog.TryGetEntry(m_Ids[i],out IEntry result)) {
					yield return result;
				}
			}
		}

	}
}