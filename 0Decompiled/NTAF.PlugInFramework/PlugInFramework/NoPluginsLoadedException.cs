using System;

namespace NTAF.PlugInFramework
{
	/// <summary>
	/// Exception used to denote that no plugins have been loaded for a particular type
	/// </summary>
	public class NoPluginsLoadedException : Exception
	{
		/// <summary>
		/// Creates an empty exception object
		/// </summary>
		public NoPluginsLoadedException()
		{
		}

		/// <summary>
		/// Creates an exception object
		/// </summary>
		/// <param name="message">Error message that accompanies the error</param>
		public NoPluginsLoadedException(string message) : base(message)
		{
		}

		/// <summary>
		/// Creates an exception object
		/// </summary>
		/// <param name="message">Error message that acompanies the error</param>
		/// <param name="innerException">Underlying error that that caused this error</param>
		public NoPluginsLoadedException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}