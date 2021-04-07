using System;
using System.Collections.Generic;
using UnityEngine;
using MackySoft.UniData.IO;

namespace MackySoft.UniData {

	/// <summary>
	/// <para> UniDataSettings is where you can configure settings related to UniData.</para>
	/// <para> It can be opened from the "Tools/UniData/Open Settings" menu. </para>
	/// </summary>
	// NOTE: UniDatSettings will be created automatically.
	// [CreateAssetMenu(fileName = nameof(UniDataSettings),menuName = "MackySoft/UniData/UniData Settings")]
	public class UniDataSettings : ScriptableObject {

		static UniDataSettings m_Instance;

		public static UniDataSettings Instance {
			get {
				if (m_Instance == null) {
					m_Instance = Resources.Load<UniDataSettings>("UniDataSettings");
				}
				return m_Instance;
			}
		}

		[SerializeField]
		DataCatalogIOAsset[] m_Loaders = new DataCatalogIOAsset[1];

		[SerializeField]
		DataCatalog[] m_PreloadedCatalogs = Array.Empty<DataCatalog>();

		[SerializeField]
		AutoSaveTiming m_AutoSaveTiming = AutoSaveTiming.OnApplicationPause | AutoSaveTiming.OnApplicationQuit;
		
		public IReadOnlyList<DataCatalogIOAsset> Loaders => m_Loaders;

		public DataCatalogIOAsset Saver => (m_Loaders.Length > 0) ? m_Loaders[m_Loaders.Length - 1] : null;

		public IReadOnlyList<DataCatalog> PreloadedCatalogs => m_PreloadedCatalogs;

		public AutoSaveTiming AutoSaveTiming { get => m_AutoSaveTiming; set => m_AutoSaveTiming = value; }

	}
}