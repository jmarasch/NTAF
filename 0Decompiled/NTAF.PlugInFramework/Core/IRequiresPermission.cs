using System;

namespace NTAF.Core
{
	public interface IRequiresPermission
	{
		Permission RequiresPermission
		{
			get;
			set;
		}

		event NTEventHandler EventRequiredPermissionChanged;
	}
}