using System;
using System.Collections.Generic;
using UnityEngine;

namespace MackySoft.UniData.IO {
	
	[Flags]
	public enum AutoSaveTiming {
		/// <summary>
		/// Do not auto-save.
		/// </summary>
		None = 0,

		/// <summary>
		/// Do auto-save when the player application is qutting.
		/// </summary>
		OnApplicationQuit = 1,

		/// <summary>
		/// Do auto-save when the player application is paused.
		/// </summary>
		OnApplicationPause = 2
	}

	/// <summary>
	/// A class that manages the automatic saving of <see cref="DataCatalog"/>.
	/// </summary>
	public static class DataCatalogAutoIO {

		static readonly HashSet<DataCatalog> m_Catalogs = new HashSet<DataCatalog>();

		public static IReadOnlyCollection<DataCatalog> Catalogs => m_Catalogs;

		[RuntimeInitializeOnLoadMethod]
		static void Initialize () {
			foreach (DataCatalog preloadedCatalog in UniDataSettings.Instance.PreloadedCatalogs) {
				RegisterCatalog(preloadedCatalog);
				preloadedCatalog.Load();
			}

			Application.focusChanged += OnApplicationPaused;
			Application.quitting += OnApplicationQuit;
		}

		static void OnApplicationPaused (bool paused) {
			if (!paused) {
				return;
			}

			AutoSaveTiming timing = UniDataSettings.Instance.AutoSaveTiming;
			if (
				timing.HasFlag(AutoSaveTiming.OnApplicationPause) ||
				Application.isMobilePlatform && timing.HasFlag(AutoSaveTiming.OnApplicationQuit)
			) {
				Save();
			}
		}

		static void OnApplicationQuit () {
			AutoSaveTiming timing = UniDataSettings.Instance.AutoSaveTiming;
			if (timing.HasFlag(AutoSaveTiming.OnApplicationQuit)) {
				Save();
			}
		}

		public static void RegisterCatalog (DataCatalog catalog) {
			m_Catalogs.Add(catalog);
		}

		public static void UnregisterCatalog (DataCatalog catalog) {
			m_Catalogs.Remove(catalog);
		}

		public static void Save () {
			foreach (var catalog in m_Catalogs) {
				if (catalog == null) {
					Debug.LogError("Catalog reference does not exist.");
					continue;
				}
				catalog.Save();
			}
		}

	}
}