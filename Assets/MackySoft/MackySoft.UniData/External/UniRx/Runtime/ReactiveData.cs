#if UNIDATA_UNIRX_SUPPORT

using System;
using UnityEngine;
using UniRx;
using MackySoft.UniData.Data.General;

namespace MackySoft.UniData.Data {

	public interface IReadOnlyReactiveData<T> : IReadOnlyData<T>, IReadOnlyReactiveProperty<T> {
		new T Value { get; }
	}

	public interface IReactiveData<T> : IReadOnlyReactiveData<T>, IData<T>, IReactiveProperty<T> {
		new T Value { get; set; }
	}

	[Serializable]
	public class ReactiveData<T> : ReactiveProperty<T>, IReactiveData<T> {

		[SerializeField]
		string m_Id = string.Empty;

		public string Id => m_Id;

		object IData.Value { get => Value; set => Value = (T)value; }

		object IReadOnlyData.Value => Value;

		public ReactiveData () : base() {
		}

		public ReactiveData (string id) : this(id,default) {
			
		}

		public ReactiveData (string id,T initialValue) : base(initialValue) {
			m_Id = id;
		}

	}

	[Serializable]
	public abstract class ReadOnlyReactiveDataBase<T> : IReadOnlyReactiveData<T> {

		[SerializeField]
		string m_Id = string.Empty;

		T m_LatestValue = default;
		BehaviorSubject<T> m_Subject;

		public abstract T Value { get; }

		public string Id => m_Id;

		public bool HasValue => false;

		public ReadOnlyReactiveDataBase () {
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

	public static class DataExtensions {
		public static BoolReactiveData ToReactiveData (this BoolData source) {
			return new BoolReactiveData(source.Id,source.Value);
		}
		public static IntReactiveData ToReactiveData (this IntData source) {
			return new IntReactiveData(source.Id,source.Value);
		}
		public static LongReactiveData ToReactiveData (this LongData source) {
			return new LongReactiveData(source.Id,source.Value);
		}
		public static ByteReactiveData ToReactiveData (this ByteData source) {
			return new ByteReactiveData(source.Id,source.Value);
		}
		public static FloatReactiveData ToReactiveData (this FloatData source) {
			return new FloatReactiveData(source.Id,source.Value);
		}
		public static DoubleReactiveData ToReactiveData (this DoubleData source) {
			return new DoubleReactiveData(source.Id,source.Value);
		}
		public static StringReactiveData ToReactiveData (this StringData source) {
			return new StringReactiveData(source.Id,source.Value);
		}
		public static Vector2ReactiveData ToReactiveData (this Vector2Data source) {
			return new Vector2ReactiveData(source.Id,source.Value);
		}
		public static Vector3ReactiveData ToReactiveData (this Vector3Data source) {
			return new Vector3ReactiveData(source.Id,source.Value);
		}
		public static Vector4ReactiveData ToReactiveData (this Vector4Data source) {
			return new Vector4ReactiveData(source.Id,source.Value);
		}
		public static QuaternionReactiveData ToReactiveData (this QuaternionData source) {
			return new QuaternionReactiveData(source.Id,source.Value);
		}
		
	}
}
#endif