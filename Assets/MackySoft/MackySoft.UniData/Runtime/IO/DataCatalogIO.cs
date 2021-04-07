using System;
using UnityEngine;

namespace MackySoft.UniData.IO {

	/// <summary>
	/// 
	/// </summary>
	public static class DataCatalogIO {
		
		/// <summary>
		/// <para> Execute loaders in sequence to load the catalog data. </para>
		/// <para> See: <see cref="UniDataSettings.Loaders"/> </para>
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		public static void Load<T> (this T catalog) where T : DataCatalog {
			if (catalog == null) {
				throw new ArgumentNullException(nameof(catalog));
			}
			Load(catalog,DataCatalogIOContext.Default);
		}

		/// <summary>
		/// <para> Execute loaders in sequence to load the catalog data. </para>
		/// <para> See: <see cref="UniDataSettings.Loaders"/> </para>
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		public static void Load<T> (this T catalog,DataCatalogIOContext context) where T : DataCatalog {
			if (catalog == null) {
				throw new ArgumentNullException(nameof(catalog));
			}

			Debug.Log($"Load \"{catalog.name}\" catalog.");

			foreach (DataCatalogIOAsset loader in UniDataSettings.Instance.Loaders) {
				loader.Load(catalog,context);
			}
		}

		/// <summary>
		/// <para> Save the catalog data. </para>
		/// <para> See: <see cref="UniDataSettings.Saver"/> </para>
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		public static void Save<T> (this T catalog) where T : DataCatalog {
			if (catalog == null) {
				throw new ArgumentNullException(nameof(catalog));
			}
			Save(catalog,DataCatalogIOContext.Default);
		}

		/// <summary>
		/// <para> Save the catalog data. </para>
		/// <para> See: <see cref="UniDataSettings.Saver"/> </para>
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		public static void Save<T> (this T catalog,DataCatalogIOContext context) where T : DataCatalog {
			if (catalog == null) {
				throw new ArgumentNullException(nameof(catalog));
			}
			DataCatalogIOAsset saver = UniDataSettings.Instance.Saver;
			if (saver != null) {
				Debug.Log($"Save \"{catalog.name}\" catalog.");
				saver.Save(catalog,context);
			}
		}

	}
}