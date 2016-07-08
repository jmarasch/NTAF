using System;

namespace NTAF.PlugInFramework
{
	/// <summary>
	/// Standard operationg modes for the editors
	/// </summary>
	public enum EditorMode
	{
		/// <summary>
		/// the object is new and being edited
		/// </summary>
		New,
		/// <summary>
		/// object is old and being changed
		/// </summary>
		Edit,
		/// <summary>
		/// currently onl viewing the object
		/// </summary>
		View,
		/// <summary>
		/// object cannot be changed
		/// </summary>
		ReadOnly
	}
}