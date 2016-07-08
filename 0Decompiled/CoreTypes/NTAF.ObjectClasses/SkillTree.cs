using NTAF.PlugInFramework;
using System;

namespace NTAF.ObjectClasses
{
	[TreeNodePlugIn("Skill Tree", "Skills", "0.0.0.0", typeof(Skill))]
	public class SkillTree : OCTreeNodeBase
	{
		public SkillTree()
		{
		}
	}
}