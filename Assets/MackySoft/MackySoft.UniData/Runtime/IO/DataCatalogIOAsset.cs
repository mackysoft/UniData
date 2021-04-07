using UnityEngine;

namespace MackySoft.UniData.IO {

	/// <summary>
	/// <para> An abstract ScriptableObject that implements the I/O logic for <see cref="DataCatalog"/>. </para>
	/// <para> Example: <see cref="MackySoft.UniData.IO.Assets.PlayerPrefsDataCatalogIOAsset"/> </para>
	/// </summary>
	public abstract class DataCatalogIOAsset : ScriptableObject {
		public abstract void Load (DataCatalog catalog,DataCatalogIOContext context);
		public abstract void Save (DataCatalog catalog,DataCatalogIOContext context);
	}
}