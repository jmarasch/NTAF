using System;

namespace NTAF.Core
{
	public class ClipperException : Exception
	{
		public ClipperException()
		{
		}

		public ClipperException(string message) : base(message)
		{
		}

		public ClipperException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}