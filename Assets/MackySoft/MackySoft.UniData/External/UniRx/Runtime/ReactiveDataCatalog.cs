#pragma warning disable IDE0074 // 複合代入を使用

#if UNIDATA_UNIRX_SUPPORT

using System;
using UnityEngine;
using UniRx;

namespace MackySoft.UniData.UniRx {

	/// <summary>
	/// <see cref="DataCatalog"/> that notify changes in the collection.
	/// </summary>
	[CreateAssetMenu(fileName = nameof(ReactiveDataCatalog),menuName = "MackySoft/UniData/Reactive Data Catalog")]
	public class ReactiveDataCatalog : DataCatalog, IReactiveCollection<IEntry>, IDisposable {

		#region Collection

		protected override void ClearEntries () {
			int beforeCount = Count;
			base.ClearEntries();

			m_CollectionReset?.OnNext(Unit.Default);
			if (beforeCount > 0) {
				m_CountChanged?.OnNext(Count);
			}
		}

		protected override void InsertEntry (int index,IEntry entry) {
			base.InsertEntry(index,entry);

			m_CollectionAdd?.OnNext(new CollectionAddEvent<IEntry>(index,entry));
			m_CountChanged?.OnNext(Count);
		}

		protected override void MoveEntry (int oldIndex,int newIndex) {
			IEntry entry = this[oldIndex];
			base.MoveEntry(oldIndex,newIndex);

			m_CollectionMove?.OnNext(new CollectionMoveEvent<IEntry>(oldIndex,newIndex,entry));
		}

		protected override void RemoveEntry (int index) {
			IEntry item = this[index];
			base.RemoveEntry(index);

			m_CollectionRemove?.OnNext(new CollectionRemoveEvent<IEntry>(index,item));
			m_CountChanged?.OnNext(Count);
		}

		protected override void SetEntry (int index,IEntry entry) {
			IEntry oldEntry = this[index];
			base.SetEntry(index,entry);

			m_CollectionReplace?.OnNext(new CollectionReplaceEvent<IEntry>(index,oldEntry,entry));
		}

		#endregion

		#region Observeable

		[NonSerialized]
		bool m_IsDisposed = false;

		[NonSerialized]
		Subject<Unit> m_CollectionReset;

		public IObservable<Unit> ObserveReset () {
			if (m_IsDisposed) {
				return Observable.Empty<Unit>();
			}
			return m_CollectionReset ?? (m_CollectionReset = new Subject<Unit>());
		}

		[NonSerialized]
		Subject<CollectionAddEvent<IEntry>> m_CollectionAdd;

		public IObservable<CollectionAddEvent<IEntry>> ObserveAdd () {
			if (m_IsDisposed) {
				return Observable.Empty<CollectionAddEvent<IEntry>>();
			}
			return m_CollectionAdd ?? (m_CollectionAdd = new Subject<CollectionAddEvent<IEntry>>());
		}

		[NonSerialized]
		Subject<CollectionRemoveEvent<IEntry>> m_CollectionRemove;

		public IObservable<CollectionRemoveEvent<IEntry>> ObserveRemove () {
			if (m_IsDisposed) {
				return Observable.Empty<CollectionRemoveEvent<IEntry>>();
			}
			return m_CollectionRemove ?? (m_CollectionRemove = new Subject<CollectionRemoveEvent<IEntry>>());
		}

		[NonSerialized]
		Subject<CollectionMoveEvent<IEntry>> m_CollectionMove;

		public IObservable<CollectionMoveEvent<IEntry>> ObserveMove () {
			if (m_IsDisposed) {
				return Observable.Empty<CollectionMoveEvent<IEntry>>();
			}
			return m_CollectionMove ?? (m_CollectionMove = new Subject<CollectionMoveEvent<IEntry>>());
		}

		[NonSerialized]
		Subject<CollectionReplaceEvent<IEntry>> m_CollectionReplace;

		public IObservable<CollectionReplaceEvent<IEntry>> ObserveReplace () {
			if (m_IsDisposed) {
				return Observable.Empty<CollectionReplaceEvent<IEntry>>();
			}
			return m_CollectionReplace ?? (m_CollectionReplace = new Subject<CollectionReplaceEvent<IEntry>>());
		}

		[NonSerialized]
		Subject<int> m_CountChanged;

		public IObservable<int> ObserveCountChanged (bool notifyCurrentCount = false) {
			if (m_IsDisposed) {
				return Observable.Empty<int>();
			}
			var subject = m_CountChanged ?? (m_CountChanged = new Subject<int>());
			if (notifyCurrentCount) {
				return subject.StartWith(() => Count);
			} else {
				return subject;
			}
		}

		#endregion

		#region IDisposable Support

		static void DisposeSubject<T> (ref Subject<T> subject) {
			if (subject != null) {
				try {
					subject.OnCompleted();
				} finally {
					subject.Dispose();
					subject = null;
				}
			}
		}

		protected virtual void Dispose (bool disposing) {
			if (!m_IsDisposed) {
				if (disposing) {
					DisposeSubject(ref m_CollectionReset);
					DisposeSubject(ref m_CollectionAdd);
					DisposeSubject(ref m_CollectionRemove);
					DisposeSubject(ref m_CollectionMove);
					DisposeSubject(ref m_CollectionReplace);
					DisposeSubject(ref m_CountChanged);
				}

				m_IsDisposed = true;
			}
		}

		public void Dispose () {
			Dispose(true);
		}

		#endregion

	}

	public static class ReactiveDataCatalogExtensions {

		public static ReactiveDataCatalog ToReactiveDataCatalog<T> (this T source) where T : DataCatalog {
			return DataCatalog.Create<ReactiveDataCatalog>(source.Id,source);
		}

	}

}
#endif