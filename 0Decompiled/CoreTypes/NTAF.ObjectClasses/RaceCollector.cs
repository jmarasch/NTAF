using NTAF.PlugInFramework;
using System;

namespace NTAF.ObjectClasses
{
	[OCCPlugIn("Races", "0.5.0.0")]
	public class RaceCollector : OCCBase
	{
		public override Type CollectionType
		{
			get
			{
				return typeof(Race);
			}
		}

		public RaceCollector()
		{
		}
	}
}