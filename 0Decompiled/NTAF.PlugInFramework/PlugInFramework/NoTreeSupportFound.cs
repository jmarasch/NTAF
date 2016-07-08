using System;

namespace NTAF.PlugInFramework
{
	/// <summary>
	/// Execption that is thrown when thier is a ObjectClass and a collector but no Tree plugin to display the data in the collector
	/// </summary>
	public class NoTreeSupportFound : Exception
	{
		/// <summary>
		/// Creates an empty execption
		/// </summary>
		public NoTreeSupportFound()
		{
		}

		/// <summary>
		/// Creates an exception
		/// </summary>
		/// <param name="message">Message about the error</param>
		public NoTreeSupportFound(string message) : base(message)
		{
		}

		/// <summary>
		/// Creates an exception
		/// </summary>
		/// <param name="message">Message about the error</param>
		/// <param name="innerException">hold the exception that steamrolled in to this execption</param>
		public NoTreeSupportFound(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}