using System;

namespace NTAF.Core
{
	public interface INTName
	{
		string Name
		{
			get;
			set;
		}

		event NTEventHandler<NameChangeArgs> EventNameChanged;
	}
}