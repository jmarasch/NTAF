using System;

namespace NTAF.PlugInFramework
{
	/// <summary>
	/// Used to define an editor plugin
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=false, Inherited=false)]
	public class EditorPlugIn : Attribute
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
		/// used to define if the editor is graphical
		/// </summary>
		public readonly bool isGUI = false;

		/// <summary>
		/// defined the types the editor can edit
		/// </summary>
		public readonly Type[] IEdit = new Type[0];

		/// <summary>
		/// Creates an instance of the EditorPlugIn attribute, defines basic information about the editor
		/// including what it can edit, its name and version info
		/// </summary>
		/// <param name="plugInName">Name of the plugin</param>
		/// <param name="plugInVersion">Version of this plugin can be entered as a
		/// "0.0.0.0" string, '*' is current not implimented</param>
		/// <param name="IsGUI">Is the gui graphical</param>
		/// <param name="iEdit">what type does it edit</param>
		public EditorPlugIn(string plugInName, string plugInVersion, bool IsGUI, Type iEdit)
		{
			this.version = new SerializableVersion(plugInName, "Editor", plugInVersion);
			this.Name = plugInName;
			this.isGUI = IsGUI;
			this.IEdit = new Type[] { iEdit };
		}

		/// <summary>
		/// Creates an instance of the EditorPlugIn attribute, defines basic information about the editor
		/// including what it can edit, its name and version info
		/// </summary>
		/// <param name="plugInName">Name of the plugin</param>
		/// <param name="plugInVersion">Version of this plugin can be entered as a
		/// "0.0.0.0" string, '*' is current not implimented</param>
		/// <param name="IsGUI">Is the gui graphical</param>
		/// <param name="iEdit">what types does it edit</param>
		public EditorPlugIn(string plugInName, string plugInVersion, bool IsGUI, Type[] iEdit)
		{
			this.version = new SerializableVersion(plugInName, "Editor", plugInVersion);
			this.Name = plugInName;
			this.isGUI = IsGUI;
			this.IEdit = iEdit;
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