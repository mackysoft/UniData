namespace MackySoft.UniData {

	public interface IEntry {
		string Id { get; }
	}

	/// <summary>
	/// <para> An interface that allows simple access to the main value of data. </para>
	/// <para> This interface is mainly used to implement editor-related functions in UniData. In many cases, users can use the generic version of <see cref="IReadOnlyData{T}"/> instead. </para>
	/// </summary>
	public interface IReadOnlyData : IEntry {

		/// <summary>
		/// Main value of data.
		/// </summary>
		object Value { get; }

	}

	/// <summary>
	/// <para> An interface that allows simple access to the main value of data. </para>
	/// <para> This interface is mainly used to implement editor-related functions in UniData. In many cases, users can use the generic version of <see cref="IData{T}"/> instead. </para>
	/// </summary>
	public interface IData : IReadOnlyData {

		/// <summary>
		/// Main value of data.
		/// </summary>
		new object Value { get; set; }

	}

	/// <summary>
	/// An interface that allows simple access to the main value of data.
	/// </summary>
	public interface IReadOnlyData<out T> : IReadOnlyData {

		/// <summary>
		/// Main value of data.
		/// </summary>
		new T Value { get; }

	}

	/// <summary>
	/// An interface that allows simple access to the main value of data.
	/// </summary>
	public interface IData<T> : IReadOnlyData<T>, IData {

		/// <summary>
		/// Main value of data.
		/// </summary>
		new T Value { get; set; }

	}

}