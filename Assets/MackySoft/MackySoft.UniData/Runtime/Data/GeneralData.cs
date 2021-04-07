using System;
using UnityEngine;

namespace MackySoft.UniData.Data.General {

	internal static class GeneralData {
		public const string k_Path = "General/";
		public const int k_Order = -10;
	}

	[AddTypeMenu(GeneralData.k_Path + nameof(BoolData),GeneralData.k_Order)]
	[Serializable]
	public sealed class BoolData : Data<bool> {
		public BoolData () {
		}

		public BoolData (string id) : base(id) {
		}

		public BoolData (string id,bool initialValue) : base(id,initialValue) {
		}
	}

	[AddTypeMenu(GeneralData.k_Path + nameof(IntData),GeneralData.k_Order)]
	[Serializable]
	public sealed class IntData : Data<int> {
		public IntData () {
		}

		public IntData (string id) : base(id) {
		}

		public IntData (string id,int initialValue) : base(id,initialValue) {
		}

	}

	[AddTypeMenu(GeneralData.k_Path + nameof(LongData),GeneralData.k_Order)]
	[Serializable]
	public sealed class LongData : Data<long> {
		public LongData () {
		}

		public LongData (string id) : base(id) {
		}

		public LongData (string id,long initialValue) : base(id,initialValue) {
		}
	}

	[AddTypeMenu(GeneralData.k_Path + nameof(ByteData),GeneralData.k_Order)]
	[Serializable]
	public sealed class ByteData : Data<byte> {
		public ByteData () {
		}

		public ByteData (string id) : base(id) {
		}

		public ByteData (string id,byte initialValue) : base(id,initialValue) {
		}
	}

	[AddTypeMenu(GeneralData.k_Path + nameof(FloatData),GeneralData.k_Order)]
	[Serializable]
	public sealed class FloatData : Data<float> {
		public FloatData () {
		}

		public FloatData (string id) : base(id) {
		}

		public FloatData (string id,float initialValue) : base(id,initialValue) {
		}
	}

	[AddTypeMenu(GeneralData.k_Path + nameof(DoubleData),GeneralData.k_Order)]
	[Serializable]
	public sealed class DoubleData : Data<double> {
		public DoubleData () {
		}

		public DoubleData (string id) : base(id) {
		}

		public DoubleData (string id,double initialValue) : base(id,initialValue) {
		}
	}

	[AddTypeMenu(GeneralData.k_Path + nameof(StringData),GeneralData.k_Order)]
	[Serializable]
	public sealed class StringData : Data<string> {
		public StringData () {
		}

		public StringData (string id) : base(id) {
		}

		public StringData (string id,string initialValue) : base(id,initialValue) {
		}
	}

	[AddTypeMenu(GeneralData.k_Path + nameof(Vector2Data),GeneralData.k_Order)]
	[Serializable]
	public sealed class Vector2Data : Data<Vector2> {
		public Vector2Data () {
		}

		public Vector2Data (string id) : base(id) {
		}

		public Vector2Data (string id,Vector2 initialValue) : base(id,initialValue) {
		}
	}

	[AddTypeMenu(GeneralData.k_Path + nameof(Vector3Data),GeneralData.k_Order)]
	[Serializable]
	public sealed class Vector3Data : Data<Vector3> {
		public Vector3Data () {
		}

		public Vector3Data (string id) : base(id) {
		}

		public Vector3Data (string id,Vector3 initialValue) : base(id,initialValue) {
		}
	}

	[AddTypeMenu(GeneralData.k_Path + nameof(Vector4Data),GeneralData.k_Order)]
	[Serializable]
	public sealed class Vector4Data : Data<Vector4> {
		public Vector4Data () {
		}

		public Vector4Data (string id) : base(id) {
		}

		public Vector4Data (string id,Vector4 initialValue) : base(id,initialValue) {
		}
	}

	[AddTypeMenu(GeneralData.k_Path + nameof(Vector2IntData),GeneralData.k_Order)]
	[Serializable]
	public sealed class Vector2IntData : Data<Vector2Int> {
		public Vector2IntData () {
		}

		public Vector2IntData (string id) : base(id) {
		}

		public Vector2IntData (string id,Vector2Int initialValue) : base(id,initialValue) {
		}
	}

	[AddTypeMenu(GeneralData.k_Path + nameof(Vector3IntData),GeneralData.k_Order)]
	[Serializable]
	public sealed class Vector3IntData : Data<Vector3Int> {
		public Vector3IntData () {
		}

		public Vector3IntData (string id) : base(id) {
		}

		public Vector3IntData (string id,Vector3Int initialValue) : base(id,initialValue) {
		}
	}

	[AddTypeMenu(GeneralData.k_Path + nameof(QuaternionData),GeneralData.k_Order)]
	[Serializable]
	public sealed class QuaternionData : Data<Quaternion> {
		public QuaternionData () {
		}

		public QuaternionData (string id) : base(id) {
		}

		public QuaternionData (string id,Quaternion initialValue) : base(id,initialValue) {
		}
	}

	[AddTypeMenu(GeneralData.k_Path + nameof(RectData),GeneralData.k_Order)]
	[Serializable]
	public sealed class RectData : Data<Rect> {
		public RectData () {
		}

		public RectData (string id) : base(id) {
		}

		public RectData (string id,Rect initialValue) : base(id,initialValue) {
		}
	}

	[AddTypeMenu(GeneralData.k_Path + nameof(RectIntData),GeneralData.k_Order)]
	[Serializable]
	public sealed class RectIntData : Data<RectInt> {
		public RectIntData () {
		}

		public RectIntData (string id) : base(id) {
		}

		public RectIntData (string id,RectInt initialValue) : base(id,initialValue) {
		}
	}

	[AddTypeMenu(GeneralData.k_Path + nameof(BoundsData),GeneralData.k_Order)]
	[Serializable]
	public sealed class BoundsData : Data<Bounds> {
		public BoundsData () {
		}

		public BoundsData (string id) : base(id) {
		}

		public BoundsData (string id,Bounds initialValue) : base(id,initialValue) {
		}
	}

	[AddTypeMenu(GeneralData.k_Path + nameof(BoundsIntData),GeneralData.k_Order)]
	[Serializable]
	public sealed class BoundsIntData : Data<BoundsInt> {
		public BoundsIntData () {
		}

		public BoundsIntData (string id) : base(id) {
		}

		public BoundsIntData (string id,BoundsInt initialValue) : base(id,initialValue) {
		}
	}

	[AddTypeMenu(GeneralData.k_Path + nameof(ColorData),GeneralData.k_Order)]
	[Serializable]
	public sealed class ColorData : Data<Color> {
		public ColorData () {
		}

		public ColorData (string id) : base(id) {
		}

		public ColorData (string id,Color initialValue) : base(id,initialValue) {
		}
	}

	[AddTypeMenu(GeneralData.k_Path + nameof(GradientData),GeneralData.k_Order)]
	[Serializable]
	public sealed class GradientData : Data<Gradient> {
		public GradientData () {
		}

		public GradientData (string id) : base(id) {
		}

		public GradientData (string id,Gradient initialValue) : base(id,initialValue) {
		}
	}

	[AddTypeMenu(GeneralData.k_Path + nameof(AnimationCurveData),GeneralData.k_Order)]
	[Serializable]
	public sealed class AnimationCurveData : Data<AnimationCurve> {
		public AnimationCurveData () {
		}

		public AnimationCurveData (string id) : base(id) {
		}

		public AnimationCurveData (string id,AnimationCurve initialValue) : base(id,initialValue) {
		}
	}

	[AddTypeMenu(GeneralData.k_Path + nameof(LayerMaskData),GeneralData.k_Order)]
	[Serializable]
	public sealed class LayerMaskData : Data<LayerMask> {
		public LayerMaskData () {
		}

		public LayerMaskData (string id) : base(id) {
		}

		public LayerMaskData (string id,LayerMask initialValue) : base(id,initialValue) {
		}
	}

}