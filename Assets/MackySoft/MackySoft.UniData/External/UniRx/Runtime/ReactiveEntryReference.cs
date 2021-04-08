#if UNIDATA_UNIRX_SUPPORT

using System;
using UnityEngine;
using UniRx;

namespace MackySoft.UniData.UniRx {

	/// <summary>
	/// <para> A reference to an entry in the <see cref="DataCatalog"/>. </para>
	/// </summary>
	/// <typeparam name="T"> Type of entry to reference. </typeparam>
	public interface IReadOnlyReactiveEntryReference<T> : IReadOnlyEntryReference<T> where T : IEntry {
		new IReadOnlyReactiveProperty<DataCatalog> Catalog { get; }
		new IReadOnlyReactiveProperty<string> EntryId { get; }
		new IReadOnlyReactiveProperty<T> Entry { get; }
	}

	/// <summary>
	/// <para> A reference to an entry in the <see cref="DataCatalog"/>. </para>
	/// </summary>
	/// <typeparam name="T"> Type of entry to reference. </typeparam>
	public interface IReactiveEntryReference<T> : IReadOnlyReactiveEntryReference<T>, IEntryReference<T> where T : IEntry {
		new IReactiveProperty<DataCatalog> Catalog { get; }
		new IReactiveProperty<string> EntryId { get; }
	}
	

	/// <summary>
	/// <para> A reference to an entry in the <see cref="DataCatalog"/>. (It does not have to be <see cref="ReactiveDataCatalog"/>) </para>
	/// <para> By using this class, you can easily set and get reference to an entry in the <see cref="DataCatalog"/> in the editor. </para>
	/// </summary>
	/// <typeparam name="T"> Type of entry to reference. </typeparam>
	[Serializable]
	public class ReactiveEntryReference<T> : IReactiveEntryReference<T> where T : IEntry {

		[SerializeField]
		ReactiveProperty<DataCatalog> m_Catalog = new ReactiveProperty<DataCatalog>();

		[SerializeField]
		ReactiveProperty<string> m_EntryId = new ReactiveProperty<string>();

		IReadOnlyReactiveProperty<T> m_CachedEntry;

		public IReactiveProperty<DataCatalog> Catalog => m_Catalog;

		public IReactiveProperty<string> EntryId => m_EntryId;

		public IReadOnlyReactiveProperty<T> Entry {
			get {
				if (m_CachedEntry == null) {
					m_CachedEntry = Observable.CombineLatest(m_Catalog,m_EntryId,(catalog,id) => {
						if ((catalog == null) || string.IsNullOrEmpty(id)) {
							return default;
						}
						return catalog.GetEntry<T>(id);
					}).ToReactiveProperty();
				}
				return m_CachedEntry;
			}
		}

		#region Explicit implementations

		DataCatalog IReadOnlyEntryReference<T>.Catalog => m_Catalog.Value;
		string IReadOnlyEntryReference<T>.EntryId => m_EntryId.Value;
		T IReadOnlyEntryReference<T>.Entry => m_CachedEntry.Value;

		DataCatalog IEntryReference<T>.Catalog { get => m_Catalog.Value; set => m_Catalog.Value = value; }
		string IEntryReference<T>.EntryId { get => m_EntryId.Value; set => m_EntryId.Value = value; }

		IReadOnlyReactiveProperty<DataCatalog> IReadOnlyReactiveEntryReference<T>.Catalog => m_Catalog;
		IReadOnlyReactiveProperty<string> IReadOnlyReactiveEntryReference<T>.EntryId => m_EntryId;

		#endregion

	}

	public interface IReadOnlyReactiveEntryReference : IReadOnlyReactiveEntryReference<IEntry> {

	}

	public interface IReactiveEntryReference : IReactiveEntryReference<IEntry>, IReadOnlyReactiveEntryReference {

	}

	/// <summary>
	/// An ReactiveEntryReference that can refer to entries of all types.
	/// </summary>
	[Serializable]
	public class ReactiveEntryReference : ReactiveEntryReference<IEntry>, IReactiveEntryReference {

	}

}
#endif