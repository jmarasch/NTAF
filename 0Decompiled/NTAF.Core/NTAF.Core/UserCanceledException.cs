using System;

namespace NTAF.Core
{
	public class UserCanceledException : Exception
	{
		public UserCanceledException()
		{
		}

		public UserCanceledException(string message) : base(message)
		{
		}

		public UserCanceledException(string message, Exception innerEcecption) : base(message, innerEcecption)
		{
		}
	}
}