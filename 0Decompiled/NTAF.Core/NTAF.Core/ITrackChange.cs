using System;

namespace NTAF.Core
{
	public interface ITrackChange
	{
		bool DataChanged
		{
			get;
			set;
		}

		event NTEventHandler EventDataStateChanged;
	}
}