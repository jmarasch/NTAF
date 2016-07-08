using System;
using System.Collections.Generic;

namespace NTAF.Core
{
	public class ValidationWarning : Exception
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
				retval = string.Concat(retval, "Do you wish to continue with this action?");
				return retval;
			}
		}

		public ValidationWarning() : base("A validation error has occurd with no explicit reason given")
		{
		}

		public ValidationWarning(FieldAndMessage[] FieldAndWarning) : base("A validation warning has occured in the following fields")
		{
			this.Errors = new List<FieldAndMessage>(FieldAndWarning);
		}

		public ValidationWarning(string message, FieldAndMessage[] FieldAndWarning, Exception innerEcecption) : base(message, innerEcecption)
		{
			this.Errors = new List<FieldAndMessage>(FieldAndWarning);
		}
	}
}