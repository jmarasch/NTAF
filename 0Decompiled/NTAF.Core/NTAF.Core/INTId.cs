using System;

namespace NTAF.Core
{
	public interface INTId
	{
		string ID
		{
			get;
			set;
		}

		string IDPreFix
		{
			get;
		}
	}
}