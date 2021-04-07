using System.Linq;

namespace MackySoft.UniData.Diagnostics {

	public static partial class ValidationUtility {

		/// <summary>
		/// Check if there are any duplicate ID's in the <see cref="DataCatalog"/> entries.
		/// </summary>
		/// <returns> Returns True if there is a duplicate ID in the <see cref="DataCatalog"/> entries, otherwise returns False. </returns>
		public static bool HasDuplication<T> (this T source) where T : DataCatalog {
			return source.Any(entry => (entry != null) && source.HasDuplication(entry.Id));
		}

		/// <summary>
		/// Check if there are any duplicate of the specified ID in the <see cref="DataCatalog"/> entries.
		/// </summary>
		/// <param name="id"> The entry ID that you want to check for duplicates. </param>
		/// <returns> Returns True if there is a duplicate of the specified ID in the <see cref="DataCatalog"/> entries, otherwise returns False. </returns>
		public static bool HasDuplication<T> (this T source,string id) where T : DataCatalog {
			bool found = false;
			foreach (var entry in source) {
				if ((entry == null) || (entry.Id != id)) {
					continue;
				}
				if (found) {
					return true;
				} else {
					found = true;
				}
			}
			return false;
		}

	}
}
