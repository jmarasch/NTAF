using System;

namespace NTAF.Core
{
	public class FileLockedException : Exception
	{
		public FileLockedException()
		{
		}

		public FileLockedException(string message) : base(message)
		{
		}

		public FileLockedException(string message, Exception innerEcecption) : base(message, innerEcecption)
		{
		}
	}
}