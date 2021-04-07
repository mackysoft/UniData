using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace MackySoft.UniData {

	// List implementation.
	public partial class DataCatalog : IList<IEntry>, IReadOnlyList<IEntry> {
		
		public IEntry this[int index] {
			get => Entries[index];
			set => SetEntry(index,value);
		}

		public IEntry this[string id] {
			get => TryGetEntry(id,out IEntry result) ? result : throw new KeyNotFoundException("The entry with the specified id was not found.");
		}

		public int Count => Entries.Count;

		/// <summary>
		/// Whether the entry with the specified id is contained in <see cref="Entries"/>.
		/// </summary>
		public bool HasEntry (string id) {
			return Entries.Any(x => x.Id == id);
		}

		/// <summary>
		/// Whether the specified entry is contained in <see cref="Entries"/>.
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		public bool HasEntry (IEntry entry) {
			if (entry == null) {
				throw new ArgumentNullException(nameof(entry));
			}
			return Entries.Contains(entry);
		}

		public IEntry GetEntry (string id) {
			return Entries.FirstOrDefault(x => x.Id == id);
		}

		public T GetEntry<T> (string id) {
			return (GetEntry(id) is T entry) ? entry : default;
		}

		public bool TryGetEntry (string id,out IEntry result) {
			result = GetEntry(id);
			return result != null;
		}

		public bool TryGetEntry<T> (string id,out T result) {
			result = GetEntry<T>(id);
			return result != null;
		}

		/// <summary>
		/// Get only entries of the specified type.
		/// </summary>
		public IEnumerable<T> GetEntriesOfType<T> () {
			foreach (IEntry entry in Entries) {
				if (entry is T result) {
					yield return result;
				}
			}
		}

		/// <summary>
		/// <para> Add the entry to <see cref="Entries"/>. </para>
		/// <para> The addition failed if the specified entry is already contained in <see cref="Entries"/>. </para> 
		/// </summary>
		/// <returns> Whether the addition was successful. </returns>
		/// <exception cref="ArgumentNullException"></exception>
		public bool Add (IEntry entry) {
			if (entry == null) {
				throw new ArgumentNullException(nameof(entry));
			}
			if (HasEntry(entry.Id)) {
				return false;
			}
			InsertEntry(Count,entry);
			return true;
		}

		/// <summary>
		/// <para> Set the entry to <see cref="Entries"/>. </para>
		/// <para> If an entry with the same id as the specified entry is already contained, specified entry is added after already contained entry is removed. </para>
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		public void SetEntry (IEntry entry) {
			if (entry == null) {
				throw new ArgumentNullException(nameof(entry));
			}
			if (HasEntry(entry)) {
				return;
			}
			Remove(entry.Id);
			Add(entry);
		}

		public void Move (int oldIndex,int newIndex) {
			MoveEntry(oldIndex,newIndex);
		}

		public void Clear () {
			ClearEntries();
		}

		public void CopyTo (IEntry[] array,int arrayIndex) {
			Entries.CopyTo(array,arrayIndex);
		}

		public IEnumerator<IEntry> GetEnumerator () {
			return Entries.GetEnumerator();
		}

		public int IndexOf (IEntry entry) {
			return Entries.IndexOf(entry);
		}

		public int IndexOf (string id) {
			return IndexOf(GetEntry(id));
		}

		public void Insert (int index,IEntry entry) {
			InsertEntry(index,entry);
		}

		public bool Remove (IEntry entry) {
			int index = IndexOf(entry);
			if (index < 0) {
				return false;
			}
			RemoveEntry(index);
			return true;
		}

		public bool Remove (string id) {
			return Remove(GetEntry(id));
		}

		public void RemoveAt (int index) {
			RemoveEntry(index);
		}

		protected virtual void ClearEntries () {
			Entries.Clear();
		}

		protected virtual void InsertEntry (int index,IEntry entry) {
			Entries.Insert(index,entry);
		}

		protected virtual void MoveEntry (int oldIndex,int newIndex) {
			IEntry entry = this[oldIndex];
			RemoveEntry(oldIndex);
			InsertEntry(newIndex,entry);
		}

		protected virtual void RemoveEntry (int index) {
			Entries.RemoveAt(index);
		}

		protected virtual void SetEntry (int index,IEntry entry) {
			Entries[index] = entry;
		}

		#region Explicit implementation

		bool ICollection<IEntry>.IsReadOnly => false;

		bool ICollection<IEntry>.Contains (IEntry entry) {
			return HasEntry(entry);
		}

		void ICollection<IEntry>.Add (IEntry entry) {
			Add(entry);
		}

		IEnumerator IEnumerable.GetEnumerator () {
			return Entries.GetEnumerator();
		}

		#endregion

	}
}