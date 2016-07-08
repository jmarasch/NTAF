using System;

namespace NTAF.Core
{
	public class NTFileExecption : Exception
	{
		private string fileName;

		public string FileName
		{
			get
			{
				return this.fileName;
			}
		}

		public NTFileExecption(string Filename) : base(string.Format("File {0} could not be saved", Filename))
		{
			this.fileName = Filename;
		}

		public NTFileExecption(string Filename, string message) : base(message)
		{
			this.fileName = Filename;
		}

		public NTFileExecption(string Filename, string message, Exception innerException) : base(message, innerException)
		{
			this.fileName = Filename;
		}
	}
}