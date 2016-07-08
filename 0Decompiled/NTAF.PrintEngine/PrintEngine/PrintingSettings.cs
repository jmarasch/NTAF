using System;
using System.Drawing;

namespace NTAF.PrintEngine
{
	public static class PrintingSettings
	{
		public readonly static Font DefaultFont;

		static PrintingSettings()
		{
			PrintingSettings.DefaultFont = new Font("Arial", 10f);
		}
	}
}