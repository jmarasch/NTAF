using System;

namespace NTAF.PlugInFramework
{
	/// <summary>
	/// Used to define a TreeNode plugn
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=false, Inherited=false)]
	public class TreeNodePlugIn : Attribute
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
		/// gets the name of the plugin
		/// </summary>
		public readonly string DisplayName = "";

		/// <summary>
		/// defined the types the tree can display
		/// </summary>
		public readonly Type[] IDisplay = new Type[0];

		/// <summary>
		/// Creates an instance of the TreeNodePlugIn attribute, defines basic information about the node
		/// including its name and version info
		/// </summary>
		/// <param name="plugInName">Name of the plugin</param>
		/// <param name="displayName">Name to display on the main branch of the tree</param>
		/// <param name="plugInVersion">Version of this plugin can be entered as a
		/// "0.0.0.0" string, '*' is current not implimented</param>
		/// <param name="iDisplay">the type this tree displays</param>
		public TreeNodePlugIn(string plugInName, string displayName, string plugInVersion, Type iDisplay)
		{
			this.version = new SerializableVersion(plugInName, "TreeNode", plugInVersion);
			this.Name = plugInName;
			this.IDisplay = new Type[] { iDisplay };
			this.DisplayName = displayName;
		}

		/// <summary>
		/// Creates an instance of the TreeNodePlugIn attribute, defines basic information about the node
		/// including its name and version info
		/// </summary>
		/// <param name="plugInName">Name of the plugin</param>
		/// <param name="displayName">Name to display on the main branch of the tree</param>
		/// <param name="plugInVersion">Version of this plugin can be entered as a
		/// "0.0.0.0" string, '*' is current not implimented</param>
		/// <param name="iDisplay">An array of types this tree can display</param>
		public TreeNodePlugIn(string plugInName, string displayName, string plugInVersion, Type[] iDisplay)
		{
			this.version = new SerializableVersion(plugInName, "TreeNode", plugInVersion);
			this.Name = plugInName;
			this.IDisplay = iDisplay;
			this.DisplayName = displayName;
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