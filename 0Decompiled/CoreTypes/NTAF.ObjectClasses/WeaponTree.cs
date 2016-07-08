using NTAF.PlugInFramework;
using System;

namespace NTAF.ObjectClasses
{
	[TreeNodePlugIn("Weapon Tree", "Weapons", "0.0.0.0", typeof(Weapon))]
	public class WeaponTree : OCTreeNodeBase
	{
		public WeaponTree()
		{
		}
	}
}