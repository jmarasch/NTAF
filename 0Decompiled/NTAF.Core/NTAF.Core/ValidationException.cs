using System;
using System.Collections.Generic;

namespace NTAF.Core
{
	public class ValidationException : Exception
	{
		public readonly List<FieldAndMessage> Errors;

		public override string Message
		{
			get
			{
				string retval = string.Concat(base.Message, "\n");
				foreach (FieldAndMessage err in this.Errors)
				{
					string str = retval;
					string[] field = new string[] { str, err.Field, ":", err.Message, "\n" };
					retval = string.Concat(field);
				}
				return retval;
			}
		}

		public ValidationException() : base("A validation error has occurd with no explicit reason given")
		{
		}

		public ValidationException(FieldAndMessage[] FieldAndError) : base("A validation error has occured in the following fields")
		{
			this.Errors = new List<FieldAndMessage>(FieldAndError);
		}

		public ValidationException(string message, FieldAndMessage[] FieldAndError, Exception innerEcecption) : base(message, innerEcecption)
		{
			this.Errors = new List<FieldAndMessage>(FieldAndError);
		}
	}
}