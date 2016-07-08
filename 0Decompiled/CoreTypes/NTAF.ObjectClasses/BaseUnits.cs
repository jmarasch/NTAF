using NTAF.PlugInFramework;
using System;

namespace NTAF.ObjectClasses
{
	[OCCPlugIn("BaseUnits", "0.0.0.0")]
	public class BaseUnits : OCCBase
	{
		public override Type CollectionType
		{
			get
			{
				return typeof(BaseUnit);
			}
		}

		public override byte objectLayer
		{
			get
			{
				return (byte)3;
			}
		}

		public BaseUnits()
		{
		}
	}
}