using NTAF.PlugInFramework;
using System;

namespace NTAF.ObjectClasses
{
	[OCCPlugIn("SkillPermissions", "0.0.0.0")]
	public class SkillPermissionCollector : OCCBase
	{
		public override Type CollectionType
		{
			get
			{
				return typeof(SkillPermission);
			}
		}

		public override byte objectLayer
		{
			get
			{
				return (byte)1;
			}
		}

		public SkillPermissionCollector()
		{
		}
	}
}