#if UNIDATA_UNIRX_SUPPORT

using System;
using UnityEngine;

namespace MackySoft.UniData.Data {

	internal static class ReactiveGeneralData {
		public const string k_Path = "General (Reactive)/";
		public const int k_Order = -1;
	}

	[AddTypeMenu(ReactiveGeneralData.k_Path + nameof(BoolReactiveData),ReactiveGeneralData.k_Order)]
	[Serializable]
	public sealed class BoolReactiveData : ReactiveData<bool> {
		public BoolReactiveData () {
		}

		public BoolReactiveData (string id) : base(id) {
		}

		public BoolReactiveData (string id,bool initialValue) : base(id,initialValue) {
		}
	}

	[AddTypeMenu(ReactiveGeneralData.k_Path + nameof(IntReactiveData),ReactiveGeneralData.k_Order)]
	[Serializable]
	public sealed class IntReactiveData : ReactiveData<int> {
		public IntReactiveData () {
		}

		public IntReactiveData (string id) : base(id) {
		}

		public IntReactiveData (string id,int initialValue) : base(id,initialValue) {
		}
	}

	[AddTypeMenu(ReactiveGeneralData.k_Path + nameof(LongReactiveData),ReactiveGeneralData.k_Order)]
	[Serializable]
	public sealed class LongReactiveData : ReactiveData<long> {
		public LongReactiveData () {
		}

		public LongReactiveData (string id) : base(id) {
		}

		public LongReactiveData (string id,long initialValue) : base(id,initialValue) {
		}
	}

	[AddTypeMenu(ReactiveGeneralData.k_Path + nameof(ByteReactiveData),ReactiveGeneralData.k_Order)]
	[Serializable]
	public sealed class ByteReactiveData : ReactiveData<byte> {
		public ByteReactiveData () {
		}

		public ByteReactiveData (string id) : base(id) {
		}

		public ByteReactiveData (string id,byte initialValue) : base(id,initialValue) {
		}
	}

	[AddTypeMenu(ReactiveGeneralData.k_Path + nameof(FloatReactiveData),ReactiveGeneralData.k_Order)]
	[Serializable]
	public sealed class FloatReactiveData : ReactiveData<float> {
		public FloatReactiveData () {
		}

		public FloatReactiveData (string id) : base(id) {
		}

		public FloatReactiveData (string id,float initialValue) : base(id,initialValue) {
		}
	}

	[AddTypeMenu(ReactiveGeneralData.k_Path + nameof(DoubleReactiveData),ReactiveGeneralData.k_Order)]
	[Serializable]
	public sealed class DoubleReactiveData : ReactiveData<double> {
		public DoubleReactiveData () {
		}

		public DoubleReactiveData (string id) : base(id) {
		}

		public DoubleReactiveData (string id,double initialValue) : base(id,initialValue) {
		}
	}

	[AddTypeMenu(ReactiveGeneralData.k_Path + nameof(StringReactiveData),ReactiveGeneralData.k_Order)]
	[Serializable]
	public sealed class StringReactiveData : ReactiveData<string> {
		public StringReactiveData () {
		}

		public StringReactiveData (string id) : base(id) {
		}

		public StringReactiveData (string id,string initialValue) : base(id,initialValue) {
		}
	}

	[AddTypeMenu(ReactiveGeneralData.k_Path + nameof(Vector2ReactiveData),ReactiveGeneralData.k_Order)]
	[Serializable]
	public sealed class Vector2ReactiveData : ReactiveData<Vector2> {
		public Vector2ReactiveData () {
		}

		public Vector2ReactiveData (string id) : base(id) {
		}

		public Vector2ReactiveData (string id,Vector2 initialValue) : base(id,initialValue) {
		}
	}

	[AddTypeMenu(ReactiveGeneralData.k_Path + nameof(Vector3ReactiveData),ReactiveGeneralData.k_Order)]
	[Serializable]
	public sealed class Vector3ReactiveData : ReactiveData<Vector3> {
		public Vector3ReactiveData () {
		}

		public Vector3ReactiveData (string id) : base(id) {
		}

		public Vector3ReactiveData (string id,Vector3 initialValue) : base(id,initialValue) {
		}
	}

	[AddTypeMenu(ReactiveGeneralData.k_Path + nameof(Vector4ReactiveData),ReactiveGeneralData.k_Order)]
	[Serializable]
	public sealed class Vector4ReactiveData : ReactiveData<Vector4> {
		public Vector4ReactiveData () {
		}

		public Vector4ReactiveData (string id) : base(id) {
		}

		public Vector4ReactiveData (string id,Vector4 initialValue) : base(id,initialValue) {
		}
	}

	[AddTypeMenu(ReactiveGeneralData.k_Path + nameof(Vector2IntReactiveData),ReactiveGeneralData.k_Order)]
	[Serializable]
	public sealed class Vector2IntReactiveData : ReactiveData<Vector2Int> {
		public Vector2IntReactiveData () {
		}

		public Vector2IntReactiveData (string id) : base(id) {
		}

		public Vector2IntReactiveData (string id,Vector2Int initialValue) : base(id,initialValue) {
		}
	}

	[AddTypeMenu(ReactiveGeneralData.k_Path + nameof(Vector3IntReactiveData),ReactiveGeneralData.k_Order)]
	[Serializable]
	public sealed class Vector3IntReactiveData : ReactiveData<Vector3Int> {
		public Vector3IntReactiveData () {
		}

		public Vector3IntReactiveData (string id) : base(id) {
		}

		public Vector3IntReactiveData (string id,Vector3Int initialValue) : base(id,initialValue) {
		}
	}

	[AddTypeMenu(ReactiveGeneralData.k_Path + nameof(QuaternionReactiveData),ReactiveGeneralData.k_Order)]
	[Serializable]
	public sealed class QuaternionReactiveData : ReactiveData<Quaternion> {
		public QuaternionReactiveData () {
		}

		public QuaternionReactiveData (string id) : base(id) {
		}

		public QuaternionReactiveData (string id,Quaternion initialValue) : base(id,initialValue) {
		}
	}

	[AddTypeMenu(ReactiveGeneralData.k_Path + nameof(RectReactiveData),ReactiveGeneralData.k_Order)]
	[Serializable]
	public sealed class RectReactiveData : ReactiveData<Rect> {
		public RectReactiveData () {
		}

		public RectReactiveData (string id) : base(id) {
		}

		public RectReactiveData (string id,Rect initialValue) : base(id,initialValue) {
		}
	}

	[AddTypeMenu(ReactiveGeneralData.k_Path + nameof(RectIntReactiveData),ReactiveGeneralData.k_Order)]
	[Serializable]
	public sealed class RectIntReactiveData : ReactiveData<RangeInt> {
		public RectIntReactiveData () {
		}

		public RectIntReactiveData (string id) : base(id) {
		}

		public RectIntReactiveData (string id,RangeInt initialValue) : base(id,initialValue) {
		}
	}

	[AddTypeMenu(ReactiveGeneralData.k_Path + nameof(BoundsReactiveData),ReactiveGeneralData.k_Order)]
	[Serializable]
	public sealed class BoundsReactiveData : ReactiveData<Bounds> {
		public BoundsReactiveData () {
		}

		public BoundsReactiveData (string id) : base(id) {
		}

		public BoundsReactiveData (string id,Bounds initialValue) : base(id,initialValue) {
		}
	}

	[AddTypeMenu(ReactiveGeneralData.k_Path + nameof(BoundsIntReactiveData),ReactiveGeneralData.k_Order)]
	[Serializable]
	public sealed class BoundsIntReactiveData : ReactiveData<BoundsInt> {
		public BoundsIntReactiveData () {
		}

		public BoundsIntReactiveData (string id) : base(id) {
		}

		public BoundsIntReactiveData (string id,BoundsInt initialValue) : base(id,initialValue) {
		}
	}

	[AddTypeMenu(ReactiveGeneralData.k_Path + nameof(ColorReactiveData),ReactiveGeneralData.k_Order)]
	[Serializable]
	public sealed class ColorReactiveData : ReactiveData<Color> {
		public ColorReactiveData () {
		}

		public ColorReactiveData (string id) : base(id) {
		}

		public ColorReactiveData (string id,Color initialValue) : base(id,initialValue) {
		}
	}

	[AddTypeMenu(ReactiveGeneralData.k_Path + nameof(GradientReactiveData),ReactiveGeneralData.k_Order)]
	[Serializable]
	public sealed class GradientReactiveData : ReactiveData<Gradient> {
		public GradientReactiveData () {
		}

		public GradientReactiveData (string id) : base(id) {
		}

		public GradientReactiveData (string id,Gradient initialValue) : base(id,initialValue) {
		}
	}

	[AddTypeMenu(ReactiveGeneralData.k_Path + nameof(AnimationCurveReactiveData),ReactiveGeneralData.k_Order)]
	[Serializable]
	public sealed class AnimationCurveReactiveData : ReactiveData<AnimationCurve> {
		public AnimationCurveReactiveData () {
		}

		public AnimationCurveReactiveData (string id) : base(id) {
		}

		public AnimationCurveReactiveData (string id,AnimationCurve initialValue) : base(id,initialValue) {
		}
	}

	[AddTypeMenu(ReactiveGeneralData.k_Path + nameof(LayerMaskReactiveData),ReactiveGeneralData.k_Order)]
	[Serializable]
	public sealed class LayerMaskReactiveData : ReactiveData<LayerMask> {
		public LayerMaskReactiveData () {
		}

		public LayerMaskReactiveData (string id) : base(id) {
		}

		public LayerMaskReactiveData (string id,LayerMask initialValue) : base(id,initialValue) {
		}
	}

}
#endif