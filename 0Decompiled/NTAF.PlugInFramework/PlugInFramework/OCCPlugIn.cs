using System;

namespace NTAF.PlugInFramework
{
	/// <summary>
	/// Attach this to an Object Class Collector and make it a plugin, sets the version info, and the plugin name
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=false, Inherited=false)]
	public class OCCPlugIn : Attribute
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
		/// Creates an instance of the Object Class Collector attribute class, defines basic information
		/// about the object types name, and version
		/// </summary>
		/// <param name="plugInName">Name of this plugin</param>
		/// <param name="plugInVersion">Version of this plugin can be entered as a
		/// "0.0.0.0" string, '*' is current not implimented</param>
		public OCCPlugIn(string plugInName, string plugInVersion)
		{
			this.version = new SerializableVersion(plugInName, "OCC", plugInVersion);
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