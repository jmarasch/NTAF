using System;

namespace NTAF.Core
{
	public class ReadOnlyException : Exception
	{
		public ReadOnlyException()
		{
		}

		public ReadOnlyException(string message) : base(message)
		{
		}

		public ReadOnlyException(string message, Exception innerEcecption) : base(message, innerEcecption)
		{
		}
	}
}