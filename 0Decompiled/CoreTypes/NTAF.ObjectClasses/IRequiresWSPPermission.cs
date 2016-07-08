using NTAF.Core;
using NTAF.Permissions;
using System;
using System.Runtime.CompilerServices;

namespace NTAF.ObjectClasses
{
	public interface IRequiresWSPPermission : IRequiresPermission
	{
		WSPPermission RequiresWSPPermission
		{
			get;
			set;
		}

		event NTEventHandler EventRequiredWSPPermissionChanged;
	}
}