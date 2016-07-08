using NTAF.PlugInFramework;
using System;

namespace NTAF.ObjectClasses
{
	[TreeNodePlugIn("WSPPermission Tree", "WSP Permissions", "0.0.0.0", new Type[] { typeof(PsyPermission), typeof(WeaponPermission), typeof(SkillPermission) })]
	public class PsyPermissionTree : OCTreeNodeBase
	{
		public PsyPermissionTree()
		{
		}
	}
}