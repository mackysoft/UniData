using System;
using UnityEngine;

namespace MackySoft.UniData.Data {

	/// <summary>
	/// The most basic data with a single value.
	/// </summary>
	[Serializable]
	public class Data<T> : DataBase<T> {

		[SerializeField]
		T m_Value;

		public Data () {
		}

		public Data (string id) : base(id) {
		}

		public Data (string id,T initialValue) : base(id,initialValue) {
		}

		public override T Value {
			get => m_Value;
			set => m_Value = value;
		}

	}
}