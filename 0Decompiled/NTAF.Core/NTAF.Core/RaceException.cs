using System;

namespace NTAF.Core
{
	public class RaceException : Exception
	{
		public RaceException()
		{
		}

		public RaceException(string message) : base(message)
		{
		}

		public RaceException(string message, Exception innerEcecption) : base(message, innerEcecption)
		{
		}
	}
}