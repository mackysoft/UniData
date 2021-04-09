#if UNIDATA_UNIRX_SUPPORT

using UniRx;

namespace MackySoft.UniData.UniRx {

	public interface IReadOnlyReactiveData<T> : IReadOnlyData<T>, IReadOnlyReactiveProperty<T> {
		new T Value { get; }
	}

	public interface IReactiveData<T> : IReadOnlyReactiveData<T>, IData<T>, IReactiveProperty<T> {
		new T Value { get; set; }
	}

}
#endif