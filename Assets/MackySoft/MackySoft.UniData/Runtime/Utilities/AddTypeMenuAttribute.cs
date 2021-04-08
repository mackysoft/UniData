using System;

namespace MackySoft.UniData {

	/// <summary>
	/// <para> An attribute that overrides the name of the type displayed in the SubclassSelector popup. </para>
	/// <example>
	/// <para> The following is a sample of applying the AddTypeMenu attribute to a class that implements <see cref="IEntry"/>. </para>
	/// <code>
	/// [AddTypeMenu("Example/Example Data")]
	/// [Serializable]
	/// public class ExampleData : IEntry { ... }
	/// </code>
	/// </example>
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface,AllowMultiple = false,Inherited = false)]
	public sealed class AddTypeMenuAttribute : Attribute {

		public string MenuName { get; }

		public int Order { get; }

		public AddTypeMenuAttribute (string menuName,int order = 0) {
			MenuName = menuName;
			Order = order;
		}

		static readonly char[] k_Separeters = new char[] { '/' };

		/// <summary>
		/// Returns the <see cref="MenuName"/> splitted by '/'.
		/// </summary>
		public string[] GetSplittedMenuName () {
			return !string.IsNullOrWhiteSpace(MenuName) ? MenuName.Split(k_Separeters,StringSplitOptions.RemoveEmptyEntries) : Array.Empty<string>();
		}

		/// <summary>
		/// Returns the display name without the path.
		/// </summary>
		public string GetTypeNameWithoutPath () {
			string[] splittedDisplayName = GetSplittedMenuName();
			return (splittedDisplayName.Length != 0) ? splittedDisplayName[splittedDisplayName.Length - 1] : null;
		}

	}
}