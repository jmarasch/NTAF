using System;
using System.Drawing;

namespace NTAF.PrintEngine
{
	public class PrintPrimitiveCategoryText : IPrintPrimitive
	{
		private string Category;

		private string Data;

		public PrintPrimitiveCategoryText(string category)
		{
			this.Category = category;
			this.Data = "";
		}

		public PrintPrimitiveCategoryText(string category, string data)
		{
			this.Category = category;
			this.Data = data;
		}

		public float CalculateHeight(NTAF.PrintEngine.PrintEngine engine, Graphics graphics)
		{
			Font tmpFont = new Font(engine.PrintFont.FontFamily, engine.PrintFont.Size, FontStyle.Bold);
			return tmpFont.GetHeight(graphics);
		}

		public void Draw(NTAF.PrintEngine.PrintEngine engine, float yPos, Graphics graphics, Rectangle elementBounds)
		{
			Font boldFont = new Font(engine.PrintFont.FontFamily, engine.PrintFont.Size, FontStyle.Bold);
			graphics.DrawString(engine.ReplaceTokens(this.Category), boldFont, engine.PrintBrush, (float)elementBounds.Left, yPos, new StringFormat());
			float dataLeft = (float)elementBounds.Left;
			SizeF sizeF = graphics.MeasureString(engine.ReplaceTokens(this.Category), boldFont);
			dataLeft = dataLeft + sizeF.Width;
			if (this.Data != "")
			{
				graphics.DrawString(engine.ReplaceTokens(this.Data), engine.PrintFont, engine.PrintBrush, dataLeft, yPos, new StringFormat());
			}
		}
	}
}