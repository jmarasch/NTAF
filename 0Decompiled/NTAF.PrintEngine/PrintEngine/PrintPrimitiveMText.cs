using System;
using System.Collections.Generic;
using System.Drawing;

namespace NTAF.PrintEngine
{
	public class PrintPrimitiveMText : IPrintPrimitive
	{
		private string Text;

		public PrintPrimitiveMText(string buf)
		{
			this.Text = buf;
		}

		public float CalculateHeight(NTAF.PrintEngine.PrintEngine engine, Graphics graphics)
		{
			float retVal = engine.PrintFont.GetHeight(graphics);
			if (this.Text != "")
			{
				retVal = retVal * (float)((int)this.MultiLine(engine, graphics).Length);
			}
			return retVal;
		}

		public void Draw(NTAF.PrintEngine.PrintEngine engine, float yPos, Graphics graphics, Rectangle elementBounds)
		{
			string[] printLines = this.MultiLine(engine, graphics);
			for (int x = 0; x <= (int)printLines.Length - 1; x++)
			{
				graphics.DrawString(engine.ReplaceTokens(printLines[x]), engine.PrintFont, engine.PrintBrush, (float)elementBounds.Left, yPos + (float)(x * engine.PrintFont.Height), new StringFormat());
			}
		}

		private string[] MultiLine(NTAF.PrintEngine.PrintEngine engine, Graphics graphics)
		{
			List<string> retVal = new List<string>();
			List<string> words = new List<string>();
			Rectangle pageBounds = engine.pageBounds;
			string text = this.Text;
			char[] chrArray = new char[] { ' ' };
			words.AddRange(text.Split(chrArray));
			string line = "";
			foreach (string word in words)
			{
				if (graphics.MeasureString(string.Concat(line, word), engine.PrintFont).Width > (float)pageBounds.Width)
				{
					retVal.Add(line);
					line = "";
				}
				line = string.Concat(line, word, " ");
			}
			if ((line != "" ? true : line != " "))
			{
				retVal.Add(line);
			}
			return retVal.ToArray();
		}
	}
}