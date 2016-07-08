using NTAF.PlugInFramework;
using System;

namespace NTAF.ObjectClasses
{
	[OCCPlugIn("WeaponPermissions", "0.0.0.0")]
	public class WeaponPermissionCollector : OCCBase
	{
		public override Type CollectionType
		{
			get
			{
				return typeof(WeaponPermission);
			}
		}

		public override byte objectLayer
		{
			get
			{
				return (byte)1;
			}
		}

		public WeaponPermissionCollector()
		{
		}
	}
}