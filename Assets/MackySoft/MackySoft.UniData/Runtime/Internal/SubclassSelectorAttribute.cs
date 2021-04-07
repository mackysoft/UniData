using System;
using UnityEngine;

namespace MackySoft.UniData.Internal {

	/// <summary>
	/// Attribute to specify the type of the field serialized by the SerializeReference attribute in the inspector.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field,AllowMultiple = false)]
	internal sealed class SubclassSelectorAttribute : PropertyAttribute {
		
	}
}