using System;

namespace NTAF.Core
{
	public class InvalidParameter : Exception
	{
		public InvalidParameter()
		{
		}

		public InvalidParameter(string message) : base(message)
		{
		}

		public InvalidParameter(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}