using NTAF.PlugInFramework;
using System;

namespace NTAF.ObjectClasses
{
	[TreeNodePlugIn("Item Tree", "Items", "0.0.0.0", typeof(Item))]
	public class ItemTree : OCTreeNodeBase
	{
		public ItemTree()
		{
		}
	}
}