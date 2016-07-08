using System;

namespace NTAF.PlugInFramework
{
	/// <summary>
	/// Sets the plugin Designer
	/// </summary>
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple=false)]
	public sealed class PluginDesigner : Attribute
	{
		/// <summary>
		/// Designers Name
		/// </summary>
		private readonly string retVal;

		/// <summary>
		/// Returns the Designers Name
		/// </summary>
		public string Designer
		{
			get
			{
				return this.retVal;
			}
		}

		/// <summary>
		/// Creates an instance of the plugin designer attribute
		/// </summary>
		/// <param name="designer">Designers Name</param>
		public PluginDesigner(string designer)
		{
			this.retVal = designer;
		}

		/// <summary>
		/// also returns the designers name
		/// </summary>
		/// <returns>String as the designers name</returns>
		public override string ToString()
		{
			return this.retVal;
		}
	}
}