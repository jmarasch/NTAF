using System;

namespace NTAF.PlugInFramework
{
	/// <summary>
	/// DONT USE THIS
	/// </summary>
	[Obsolete("Use attributes o define info", true)]
	public interface ICreatorInfo
	{
		/// <summary>
		/// Returns the classes creators contact info, ie e-mail, phone, or mailing address
		/// </summary>
		string Contact
		{
			get;
		}

		/// <summary>
		/// Returns the classes creators name
		/// </summary>
		string Creator
		{
			get;
		}

		/// <summary>
		/// Returns the classes Version info, not the same as the plugins, dll, or file version neccicarrily.
		/// </summary>
		string Version
		{
			get;
		}

		/// <summary>
		/// Returns the classes creators Web address
		/// </summary>
		string WebUrl
		{
			get;
		}
	}
}