using System;

namespace NTAF.Core
{
	public class ItemException : Exception
	{
		public ItemException()
		{
		}

		public ItemException(string message) : base(message)
		{
		}

		public ItemException(string message, Exception innerEcecption) : base(message, innerEcecption)
		{
		}
	}
}