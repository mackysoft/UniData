#if UNIDATA_EASYSAVE_SUPPORT

using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using MackySoft.UniData.IO;

namespace MackySoft.UniData.EasySave.IO.Asset {

	/// <summary>
	/// Load the <see cref="DataCatalog"/> data using EasySave 3 (ES3).
	/// </summary>
	[CreateAssetMenu(fileName = nameof(ES3DataCatalogIOAsset),menuName = "MackySoft/UniData/IO Asset/ES3 DataCatalog IO Asset")]
	public class ES3DataCatalogIOAsset : DataCatalogIOAsset {
		
		[SerializeField]
		string m_FolderPath = "Data";

		public string FolderPath {
			get => m_FolderPath;
			set => m_FolderPath = value;
		}

		public override void Load (DataCatalog catalog,DataCatalogIOContext context) {
			if (catalog == null) {
				throw new ArgumentNullException(nameof(catalog));
			}

			ES3File file = GetFile(catalog,context);

			IEnumerable<string> keys = file.GetKeys();
			if (context.Ids.Count > 0) {
				keys = keys.Intersect(context.Ids);
			}

			// Load entries.
			foreach (string key in keys) {
				if (catalog.TryGetEntry(key,out IEntry entry)) {
					// Overwrite
					file.LoadInto(key,(object)entry);
				} else {
					// New entry
					var loadedEntry = (IEntry)file.Load<object>(key);
					catalog.Add(loadedEntry);
				}
			}
		}

		public override void Save (DataCatalog catalog,DataCatalogIOContext context) {
			if (catalog == null) {
				throw new ArgumentNullException(nameof(catalog));
			}

			ES3File file = GetFile(catalog,context);

			foreach (IEntry entry in context.GetEntries(catalog)) {
				if (entry == null) {
					continue;
				}

				// Save the entry with id as the key.
				file.Save(entry.Id,(object)entry);
			}

			file.Sync();
		}

		ES3File GetFile (DataCatalog catalog,DataCatalogIOContext context) {
			string folderPath = m_FolderPath;
			if (!folderPath.EndsWith("/")) {
				folderPath = folderPath.TrimEnd('/');
			}
			if (context.Index >= 0) {
				folderPath += "_" + context.ToString();
			}
			return new ES3File($"{folderPath}/{catalog.Id}.es3");
		}

	}

}
#endif