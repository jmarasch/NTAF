using System;
using System.Drawing;

namespace NTAF.PrintEngine
{
	public class PrintPrimitiveTitleText : IPrintPrimitive
	{
		private string Text;

		private float sizeChange;

		public PrintPrimitiveTitleText(string buf, float SizeAdjusment)
		{
			this.Text = buf;
			this.sizeChange = SizeAdjusment;
		}

		public float CalculateHeight(NTAF.PrintEngine.PrintEngine engine, Graphics graphics)
		{
			Font tmpFont = new Font(engine.PrintFont.FontFamily, engine.PrintFont.Size + this.sizeChange, FontStyle.Bold);
			return tmpFont.GetHeight(graphics);
		}

		public void Draw(NTAF.PrintEngine.PrintEngine engine, float yPos, Graphics graphics, Rectangle elementBounds)
		{
			Font tmpFont = new Font(engine.PrintFont.FontFamily, engine.PrintFont.Size + this.sizeChange, FontStyle.Bold);
			graphics.DrawString(engine.ReplaceTokens(this.Text), tmpFont, engine.PrintBrush, (float)elementBounds.Left, yPos, new StringFormat());
		}
	}
}