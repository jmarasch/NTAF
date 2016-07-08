using System;
using System.Drawing;

namespace NTAF.PrintEngine
{
	public class PrintPrimitiveBlankLine : IPrintPrimitive
	{
		public PrintPrimitiveBlankLine()
		{
		}

		public float CalculateHeight(NTAF.PrintEngine.PrintEngine engine, Graphics graphics)
		{
			return graphics.MeasureString("X", engine.PrintFont).Height;
		}

		public void Draw(NTAF.PrintEngine.PrintEngine engine, float yPos, Graphics graphics, Rectangle elementBounds)
		{
			graphics.DrawString("", engine.PrintFont, engine.PrintBrush, (float)elementBounds.Left, yPos, new StringFormat());
		}
	}
}