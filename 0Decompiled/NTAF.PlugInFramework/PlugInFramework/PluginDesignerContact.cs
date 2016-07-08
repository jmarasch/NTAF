using System;

namespace NTAF.PlugInFramework
{
	/// <summary>
	/// Use this attribute to define a method to contact the plugindesigner
	/// </summary>
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple=false)]
	public sealed class PluginDesignerContact : Attribute
	{
		/// <summary>
		/// gets contact info
		/// </summary>
		private readonly string retVal;

		/// <summary>
		/// gets contact information
		/// </summary>
		public string DesignerContact
		{
			get
			{
				return this.retVal;
			}
		}

		/// <summary>
		/// creates instance of attribute
		/// </summary>
		/// <param name="designerContact">Designers contact info</param>
		public PluginDesignerContact(string designerContact)
		{
			this.retVal = designerContact;
		}

		/// <summary>
		/// gets contact information
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return this.retVal;
		}
	}
}