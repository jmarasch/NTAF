using NTAF.PlugInFramework;
using System;

namespace NTAF.ObjectClasses
{
	[OCCPlugIn("Skills", "0.0.0.0")]
	public class SkillCollector : OCCBase
	{
		public override Type CollectionType
		{
			get
			{
				return typeof(Skill);
			}
		}

		public override byte objectLayer
		{
			get
			{
				return (byte)2;
			}
		}

		public SkillCollector()
		{
		}
	}
}