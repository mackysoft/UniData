#pragma warning disable CA2235 // Mark all non-serializable fields

#if UNIDATA_UNIRX_SUPPORT

using System;
using UnityEngine;
using UniRx;

namespace MackySoft.UniData.UniRx.Data {

	public interface IReadOnlyReactiveData<T> : IReadOnlyData<T>, IReadOnlyReactiveProperty<T> {
		new T Value { get; }
	}

	public interface IReactiveData<T> : IReadOnlyReactiveData<T>, IData<T>, IReactiveProperty<T> {
		new T Value { get; set; }
	}

	/// <summary>
	/// Base class that is useful for creating reactive data.
	/// </summary>
	[Serializable]
	public class ReactiveData<T> : ReactiveProperty<T>, IReactiveData<T> {

		[SerializeField]
		string m_Id;

		public string Id => m_Id;

		public ReactiveData () : base() {
		}

		public ReactiveData (string id) : this(id,default) {
			
		}

		public ReactiveData (string id,T initialValue) : base(initialValue) {
			m_Id = id;
		}

		#region Explicit implementation

		object IData.Value { get => Value; set => Value = (T)value; }

		object IReadOnlyData.Value => Value;

		#endregion

	}

	/// <summary>
	/// Base class that is useful for creating readonly reactive data.
	/// </summary>
	[Serializable]
	public abstract class ReadOnlyReactiveDataBase<T> : IReadOnlyReactiveData<T> {

		[SerializeField]
		string m_Id = string.Empty;

		[NonSerialized]
		readonly BehaviorSubject<T> m_Subject;

		public abstract T Value { get; }

		public string Id => m_Id;

		public bool HasValue => false;

		protected ReadOnlyReactiveDataBase () {
			m_Subject = new BehaviorSubject<T>(default);
		}

		public IDisposable Subscribe (IObserver<T> observer) {
			return m_Subject.Subscribe(observer);
		}

		protected void OnValueChanged () {
			m_Subject.OnNext(Value);
		}

		#region Explicit implementation

		object IReadOnlyData.Value => Value;

		#endregion

	}

}
#endif