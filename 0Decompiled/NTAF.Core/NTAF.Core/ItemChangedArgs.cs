using System;

namespace NTAF.Core
{
	public class ItemChangedArgs : EventArgs
	{
		public readonly int Index;

		public readonly object Item;

		public readonly ArgAction Action;

		public ItemChangedArgs(int index, object item, ArgAction action)
		{
			this.Index = index;
			this.Item = item;
			this.Action = action;
		}
	}
}