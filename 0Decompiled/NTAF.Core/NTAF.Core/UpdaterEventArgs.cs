using System;

namespace NTAF.Core
{
	public class UpdaterEventArgs : EventArgs
	{
		public readonly int NumberOfItems;

		public UpdaterEventArgs(int ItemCount)
		{
			this.NumberOfItems = ItemCount;
		}
	}
}