using NTAF.PlugInFramework;
using System;

namespace NTAF.ObjectClasses
{
	[OCCPlugIn("Psys", "0.0.0.0")]
	public class PsyCollector : OCCBase
	{
		public override Type CollectionType
		{
			get
			{
				return typeof(Psy);
			}
		}

		public override byte objectLayer
		{
			get
			{
				return (byte)2;
			}
		}

		public PsyCollector()
		{
		}
	}
}