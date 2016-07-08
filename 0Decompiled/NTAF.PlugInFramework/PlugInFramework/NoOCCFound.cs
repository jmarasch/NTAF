using System;

namespace NTAF.PlugInFramework
{
	/// <summary>
	/// Occurs when an ObjectClass plugin is available but a proper Collector could not be found
	/// </summary>
	public class NoOCCFound : Exception
	{
		/// <summary>
		/// Creates an empty exception
		/// </summary>
		public NoOCCFound()
		{
		}

		/// <summary>
		/// Creates an exception
		/// </summary>
		/// <param name="message">Message about the error</param>
		public NoOCCFound(string message) : base(message)
		{
		}

		/// <summary>
		/// Creates an exception
		/// </summary>
		/// <param name="message">Message about the error</param>
		/// <param name="innerException">hold the exception that steamrolled in to this execption</param>
		public NoOCCFound(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}