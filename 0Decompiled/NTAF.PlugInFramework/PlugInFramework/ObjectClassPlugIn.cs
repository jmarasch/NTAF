using System;

namespace NTAF.PlugInFramework
{
	/// <summary>
	/// Used to define a ObjectClass plugin
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=false, Inherited=false)]
	public class ObjectClassPlugIn : Attribute
	{
		/// <summary>
		/// returns the version of the plugin
		/// </summary>
		public readonly SerializableVersion version = null;

		/// <summary>
		/// gets the name of the plugin
		/// </summary>
		public readonly string Name = "";

		/// <summary>
		/// Creates an instance of the ObjectClass attribute, defines basic information about the OC
		/// including its name and version info
		/// </summary>
		/// <param name="plugInName">Name of the plugin</param>
		/// <param name="plugInVersion">Version of this plugin can be entered as a
		/// "0.0.0.0" string, '*' is current not implimented</param>
		public ObjectClassPlugIn(string plugInName, string plugInVersion)
		{
			this.version = new SerializableVersion(plugInName, "ObjectClass", plugInVersion);
			this.Name = plugInName;
		}

		/// <summary>
		/// retrieves version informtion as a string
		/// </summary>
		/// <returns>version as a string</returns>
		public override string ToString()
		{
			return this.version.ToString();
		}
	}
}