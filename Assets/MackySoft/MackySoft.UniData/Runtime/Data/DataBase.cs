using UnityEngine;

namespace MackySoft.UniData.Data {

	/// <summary>
	/// Base class that is useful for creating data.
	/// </summary>
	public abstract class DataBase<T> : IData<T> {

		[SerializeField]
		protected string m_Id;

		public string Id => m_Id;

		public abstract T Value { get; set; }

		protected DataBase () {

		}

		protected DataBase (string id) : this(id,default) {
			
		}

		protected DataBase (string id,T initialValue) {
			m_Id = id;
			Value = initialValue;
		}

		#region Explicit implementation

		object IData.Value {
			get => Value;
			set => Value = (T)value;
		}

		object IReadOnlyData.Value => Value;

		#endregion

	}

	/// <summary>
	/// Base class that is useful for creating readonly data.
	/// </summary>
	public abstract class ReadOnlyDataBase<T> : IReadOnlyData<T> {

		[SerializeField]
		protected string m_Id;

		public string Id => m_Id;

		public abstract T Value { get; }

		protected ReadOnlyDataBase () {

		}

		protected ReadOnlyDataBase (string id) {
			m_Id = id;
		}

		#region Explicite implementation

		object IReadOnlyData.Value => Value;

		#endregion

	}

}