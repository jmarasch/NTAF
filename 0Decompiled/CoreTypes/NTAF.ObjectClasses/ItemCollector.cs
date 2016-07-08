using NTAF.PlugInFramework;
using System;

namespace NTAF.ObjectClasses
{
	[OCCPlugIn("Items", "0.0.0.0")]
	public class ItemCollector : OCCBase
	{
		public override Type CollectionType
		{
			get
			{
				return typeof(NTAF.ObjectClasses.Item);
			}
		}

		public override byte objectLayer
		{
			get
			{
				return (byte)2;
			}
		}

		public ItemCollector()
		{
		}
	}
}