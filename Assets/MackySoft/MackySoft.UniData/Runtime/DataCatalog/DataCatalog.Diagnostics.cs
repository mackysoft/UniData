using System.Linq;

namespace MackySoft.UniData.Diagnostics {
	public static partial class DataCatalogExtensions {

		public static bool HasDuplication (this DataCatalog source) {
			return source.Any(entry => (entry != null) && source.HasDuplication(entry.Id));
		}

		public static bool HasDuplication (this DataCatalog source,string id) {
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
