using NTAF.PlugInFramework;
using System;

namespace NTAF.ObjectClasses
{
	[OCCPlugIn("PsyPermissions", "0.0.0.0")]
	public class PsyPermissionCollector : OCCBase
	{
		public override Type CollectionType
		{
			get
			{
				return typeof(PsyPermission);
			}
		}

		public override byte objectLayer
		{
			get
			{
				return (byte)1;
			}
		}

		public PsyPermissionCollector()
		{
		}
	}
}