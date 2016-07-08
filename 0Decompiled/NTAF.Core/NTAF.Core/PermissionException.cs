using System;

namespace NTAF.Core
{
	public class PermissionException : Exception
	{
		public PermissionException()
		{
		}

		public PermissionException(string message) : base(message)
		{
		}

		public PermissionException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}