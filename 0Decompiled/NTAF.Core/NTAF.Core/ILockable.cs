using System;

namespace NTAF.Core
{
	public interface ILockable
	{
		bool FileLocked
		{
			get;
		}

		string FilePassword
		{
			set;
		}

		bool CheckPassword(string cleartypePassword);

		void LockFile();

		void UnLockFile(string cleartypePassword);

		event NTEventHandler LockStatusChange;
	}
}