using System;

namespace NTAF.PlugInFramework
{
	/// <summary>
	/// A more general execption that can be thrown dealing with missing plugin items or extentions
	/// </summary>
	public class PluginMissing : Exception
	{
		/// <summary>
		/// Creates an empty execption
		/// </summary>
		public PluginMissing()
		{
		}

		/// <summary>
		/// Creates an exception
		/// </summary>
		/// <param name="message">Message about the error</param>
		public PluginMissing(string message) : base(message)
		{
		}

		/// <summary>
		/// Creates an exception
		/// </summary>
		/// <param name="message">Message about the error</param>
		/// <param name="innerException">hold the exception that steamrolled in to this execption</param>
		public PluginMissing(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}