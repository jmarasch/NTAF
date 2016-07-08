using NTAF.PlugInFramework;
using System;

namespace NTAF.ObjectClasses
{
	[OCCPlugIn("Archetypes", "0.0.0.0")]
	public class ArchetypeCollector : OCCBase
	{
		public override Type CollectionType
		{
			get
			{
				return typeof(Archetype);
			}
		}

		public ArchetypeCollector()
		{
		}
	}
}