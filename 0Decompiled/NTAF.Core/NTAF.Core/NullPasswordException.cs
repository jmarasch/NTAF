using System;

namespace NTAF.Core
{
	public class NullPasswordException : Exception
	{
		public NullPasswordException()
		{
		}

		public NullPasswordException(string message) : base(message)
		{
		}

		public NullPasswordException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}