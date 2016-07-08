using System;

namespace NTAF.PlugInFramework
{
	/// <summary>
	/// Eception used to denote the fact that a proper editor could not be found
	/// to edit the object.
	/// </summary>
	public class NoEditorFound : Exception
	{
		private readonly string Creator = "";

		private readonly string Contact = "";

		private readonly string WebUrl = "";

		private readonly string Version = "";

		/// <summary>
		/// Creates an exception object
		/// </summary>
		/// <param name="creator">Name of the person that made the ObjectClass</param>
		/// <param name="contacct">Contact information of the person that created the Object Class plugin</param>
		/// <param name="webUrl">WebLocation that the plugin shouldhave been goten from</param>
		/// <param name="version">Version of the current Object Class plugin</param>
		public NoEditorFound(string creator, string contacct, string webUrl, string version)
		{
			this.Creator = creator;
			this.Contact = contacct;
			this.WebUrl = webUrl;
			this.Version = version;
		}

		/// <summary>
		/// Creates an exception object
		/// </summary>
		/// <param name="creator">Name of the person that made the ObjectClass</param>
		/// <param name="contacct">Contact information of the person that created the Object Class plugin</param>
		/// <param name="webUrl">WebLocation that the plugin shouldhave been goten from</param>
		/// <param name="version">Version of the current Object Class plugin</param>
		/// <param name="message">Error message that accompanies the error</param>
		public NoEditorFound(string creator, string contacct, string webUrl, string version, string message) : base(message)
		{
			this.Creator = creator;
			this.Contact = contacct;
			this.WebUrl = webUrl;
			this.Version = version;
		}

		/// <summary>
		/// Creates an exception object
		/// </summary>
		/// <param name="creator">Name of the person that made the ObjectClass</param>
		/// <param name="contacct">Contact information of the person that created the Object Class plug in</param>
		/// <param name="webUrl">WebLocation that the plug in should have been goten from</param>
		/// <param name="version">Version of the current Object Class plugin</param>
		/// <param name="message">Error message that accompanies the error</param>
		/// <param name="innerException">Underlying error that caused this error</param>
		public NoEditorFound(string creator, string contacct, string webUrl, string version, string message, Exception innerException) : base(message, innerException)
		{
			this.Creator = creator;
			this.Contact = contacct;
			this.WebUrl = webUrl;
			this.Version = version;
		}
	}
}