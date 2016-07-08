using System;
using System.Drawing;

namespace NTAF.PrintEngine
{
	public static class StringHelper
	{
		public static StringFormat AlignTC()
		{
			return new StringFormat()
			{
				Alignment = StringAlignment.Center,
				LineAlignment = StringAlignment.Center
			};
		}
	}
}