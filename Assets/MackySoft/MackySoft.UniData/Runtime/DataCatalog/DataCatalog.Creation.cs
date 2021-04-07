using System.Collections.Generic;

namespace MackySoft.UniData {

	// Creation methods
	public partial class DataCatalog {

		/// <summary>
		/// Create the new catalog instance with specified id.
		/// </summary>
		/// <param name="id"> The identifier of the catalog to be created. </param>
		public static T Create<T> (string id) where T : DataCatalog {
			var instance = CreateInstance<T>();
			instance.m_Id = id;
			return instance;
		}

		/// <summary>
		/// Create the new catalog instance with specified id.
		/// </summary>
		/// <param name="id"> The identifier of the catalog to be created. </param>
		public static T Create<T> (string id,IEnumerable<IEntry> entries) where T : DataCatalog {
			var instance = Create<T>(id);
			foreach (var entry in entries) {
				instance.Add(entry);
			}
			return instance;
		}

	}
}