using System;

namespace NTAF.Core
{
	public interface IUpdateProgress
	{
		event NTEventHandler<UpdateProgressEventArgs> Update;

		event NTEventHandler Updated;

		event NTEventHandler<UpdaterEventArgs> Updating;
	}
}