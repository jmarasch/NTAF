using System;
using System.Runtime.CompilerServices;

namespace NTAF.PlugInFramework
{
	public class DataMember
	{
		public object Data
		{
			get;
			private set;
		}

		public string Field
		{
			get;
			private set;
		}

		public DataMember(string field, object data)
		{
			this.Field = field;
			this.Data = data;
		}
	}
}