#pragma warning disable CA2235 // Mark all non-serializable fields
#pragma warning disable CA1034 // Nested types should not be visible

using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace MackySoft.UniData.IO.Asset {

	/// <summary>
	/// Save / Load the <see cref="DataCatalog"/> as JSON format via PlayerPrefs.
	/// </summary>
	[CreateAssetMenu(fileName = nameof(PlayerPrefsDataCatalogIOAsset),menuName = "MackySoft/UniData/IO Asset/Player Prefs Data Catalog IO Asset")]
	public class PlayerPrefsDataCatalogIOAsset : DataCatalogIOAsset {
		
		[Tooltip("Key prefix for saving / loading the DataCatalog via PlayerPrefs.")]
		[SerializeField]
		string m_PlayerPrefsKeyPrefix = $"{nameof(DataCatalog)}_";

		/// <summary>
		/// Key prefix for saving / loading the <see cref="DataCatalog"/> via PlayerPrefs.
		/// </summary>
		public string PlayerPrefsKeyPrefix {
			get => m_PlayerPrefsKeyPrefix;
			set => m_PlayerPrefsKeyPrefix = value;
		}

		public override void Load (DataCatalog catalog,DataCatalogIOContext context) {
			string key = GetKey(catalog,context);
			if (TryGetSerializableDataCatalog(key,out SerializableDataCatalog data)) {
				data.FromJsonOverwrite(catalog,context);
			}
		}

		public override void Save (DataCatalog catalog,DataCatalogIOContext context) {
			string key = GetKey(catalog,context);
			var newData = new SerializableDataCatalog(catalog,context);
			if ((context.Ids.Count > 0) && TryGetSerializableDataCatalog(key,out SerializableDataCatalog oldData)) {
				oldData.ApplyChanges(newData,context);
				newData = oldData;
			}
			PlayerPrefs.SetString(key,newData.ToJson());
		}

		string GetKey (DataCatalog catalog,DataCatalogIOContext context) {
			if (context.Index >= 0) {
				return $"{m_PlayerPrefsKeyPrefix}{catalog.Id}_{context.Index}";
			} else {
				return $"{m_PlayerPrefsKeyPrefix}{catalog.Id}";
			}
		}

		static bool TryGetSerializableDataCatalog (string key,out SerializableDataCatalog result) {
			string json = PlayerPrefs.GetString(key,null);
			if (string.IsNullOrEmpty(json)) {
				result = null;
				return false;
			}

			result = JsonUtility.FromJson<SerializableDataCatalog>(json);
			return result != null;
		}

		[Serializable]
		public class SerializableDataCatalog {

			[SerializeField]
			List<SerializableEntry> m_Entries;

			public IList<SerializableEntry> Entries => m_Entries;

			public SerializableDataCatalog () {
				m_Entries = new List<SerializableEntry>();
			}

			public SerializableDataCatalog (DataCatalog catalog,DataCatalogIOContext context) {
				m_Entries = context.GetEntries(catalog)
					.Select(entry => new SerializableEntry(entry))
					.ToList();
			}

			public SerializableEntry GetEntry (string id) {
				for (int i = 0;m_Entries.Count > i;i++) {
					SerializableEntry entry = m_Entries[i];
					if (entry.Id == id) {
						return entry;
					}
				}
				return null;
			}

			public bool TryGetEntry (string id,out SerializableEntry result) {
				result = GetEntry(id);
				return result != null;
			}

			public void ApplyChanges (SerializableDataCatalog changedCatalog,DataCatalogIOContext context) {
				if (context.Ids.Count > 0) {
					for (int i = 0;context.Ids.Count > i;i++) {
						string id = context.Ids[i];
						SerializableEntry newEntry = changedCatalog.GetEntry(id);
						if (TryGetEntry(id,out SerializableEntry result)) {
							result.Data = newEntry.Data;
						} else {
							m_Entries.Add(newEntry);
						}
					}
				} else {
					foreach (SerializableEntry entry in changedCatalog.Entries) {
						if (TryGetEntry(entry.Id,out SerializableEntry result)) {
							result.Data = entry.Data;
						} else {
							m_Entries.Add(entry);
						}
					}
				}
			}

			public void FromJsonOverwrite (DataCatalog catalogToOverwrite,DataCatalogIOContext context) {
				if (context.Ids.Count > 0) {
					foreach (string id in context.Ids) {
						if (catalogToOverwrite.TryGetEntry(id,out IEntry entryToOverwrite) && TryGetEntry(id,out SerializableEntry data)) {
							data.FromJsonOverwrite(entryToOverwrite);
						}
					}
				} else {
					foreach (SerializableEntry data in m_Entries) {
						IEntry entryToOverwrite = catalogToOverwrite.GetEntry(data.Id);
						data.FromJsonOverwrite(entryToOverwrite);
					}
				}
			}

			public string ToJson () {
				return JsonUtility.ToJson(this,false);
			}
			
		}

		[Serializable]
		public class SerializableEntry {

			[SerializeField]
			string m_Id;

			[SerializeField]
			string m_Data;

			public string Id => m_Id;

			/// <summary>
			/// Entry data in JSON format.
			/// </summary>
			public string Data { get => m_Data; set => m_Data = value; }

			public SerializableEntry (IEntry entry) {
				m_Id = entry.Id;
				m_Data = JsonUtility.ToJson(entry,false);
			}

			public void FromJsonOverwrite (IEntry objectToOverwrite) {
				JsonUtility.FromJsonOverwrite(m_Data,objectToOverwrite);
			}

		}

	}
}