using System;
using System.Drawing;

namespace NTAF.PrintEngine
{
	public class PrintPrimitiveText : IPrintPrimitive
	{
		private string Text;

		public PrintPrimitiveText(string buf)
		{
			this.Text = buf;
		}

		public float CalculateHeight(NTAF.PrintEngine.PrintEngine engine, Graphics graphics)
		{
			return engine.PrintFont.GetHeight(graphics);
		}

		public void Draw(NTAF.PrintEngine.PrintEngine engine, float yPos, Graphics graphics, Rectangle elementBounds)
		{
			graphics.DrawString(engine.ReplaceTokens(this.Text), engine.PrintFont, engine.PrintBrush, (float)elementBounds.Left, yPos, new StringFormat());
		}
	}
}