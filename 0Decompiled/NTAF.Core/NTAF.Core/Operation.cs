using System;

namespace NTAF.Core
{
	public class Operation
	{
		private Enum i_Action = null;

		private object i_Data = null;

		public Enum Action
		{
			get
			{
				return this.i_Action;
			}
		}

		public object Data
		{
			get
			{
				return this.i_Data;
			}
		}

		public Operation(Enum action, object data)
		{
			this.i_Data = data;
			this.i_Action = action;
		}
	}
}