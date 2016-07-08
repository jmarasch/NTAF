using NTAF.Core;
using NTAF.Permissions;
using NTAF.PlugInFramework;
using System;

namespace NTAF.ObjectClasses
{
	public static class PermissionFormatter
	{
		public static WSPPermission changeType(WSPPermission permission, PermissionType toType)
		{
			WSPPermission weaponPermission = null;
			switch (toType)
			{
				case PermissionType.Weapon:
				{
					weaponPermission = new WeaponPermission();
					break;
				}
				case PermissionType.Psy:
				{
					weaponPermission = new PsyPermission();
					break;
				}
				case PermissionType.Skill:
				{
					weaponPermission = new SkillPermission();
					break;
				}
			}
			weaponPermission.ID = permission.ID;
			weaponPermission.Name = permission.Name;
			weaponPermission.RaceCanEquip = permission.RaceCanEquip;
			weaponPermission.SpeciesCanEquip = permission.SpeciesCanEquip;
			weaponPermission.myOwner = permission.myOwner;
			return weaponPermission;
		}
	}
}