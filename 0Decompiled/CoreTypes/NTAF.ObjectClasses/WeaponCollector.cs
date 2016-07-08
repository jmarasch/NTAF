using NTAF.PlugInFramework;
using System;

namespace NTAF.ObjectClasses
{
	[OCCPlugIn("Weapons", "0.0.0.0")]
	public class WeaponCollector : OCCBase
	{
		public override Type CollectionType
		{
			get
			{
				return typeof(Weapon);
			}
		}

		public override byte objectLayer
		{
			get
			{
				return (byte)2;
			}
		}

		public WeaponCollector()
		{
		}
	}
}