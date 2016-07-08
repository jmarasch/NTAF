using System;
using System.Drawing;

namespace NTAF.PrintEngine
{
	public class PrintPrimitiveRule : IPrintPrimitive
	{
		public PrintPrimitiveRule()
		{
		}

		public float CalculateHeight(NTAF.PrintEngine.PrintEngine engine, Graphics graphics)
		{
			return 5f;
		}

		public void Draw(NTAF.PrintEngine.PrintEngine engine, float yPos, Graphics graphics, Rectangle elementBounds)
		{
			Pen pen = new Pen(engine.PrintBrush, 1f);
			graphics.DrawLine(pen, (float)elementBounds.Left, yPos + 2f, (float)elementBounds.Right, yPos + 2f);
		}
	}
}