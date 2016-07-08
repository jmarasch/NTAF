using System;

namespace NTAF.Core
{
	public class FieldAndMessage
	{
		public readonly string Field;

		public readonly string Message;

		public FieldAndMessage(string field, string message)
		{
			this.Field = field;
			this.Message = message;
		}
	}
}