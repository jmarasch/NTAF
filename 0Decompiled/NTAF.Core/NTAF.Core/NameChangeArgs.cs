using System;

namespace NTAF.Core
{
	public class NameChangeArgs : EventArgs
	{
		public readonly string NewName;

		public readonly string OldName;

		public readonly string ObjID;

		public NameChangeArgs(string newName, string oldName, string objID)
		{
			this.NewName = newName;
			this.OldName = oldName;
			this.ObjID = objID;
		}
	}
}