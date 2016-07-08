using System;

namespace NTAF.PrintEngine
{
	public class PrintBreakLine : IPrintable
	{
		public PrintBreakLine()
		{
		}

		public void Print(PrintElement element)
		{
			element.AddHorizontalRule();
		}
	}
}