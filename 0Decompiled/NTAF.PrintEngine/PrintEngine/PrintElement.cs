using NTAF.PrintEngine.Properties;
using System;
using System.Collections;
using System.Drawing;

namespace NTAF.PrintEngine
{
	public class PrintElement
	{
		private ArrayList _printPrimitives = new ArrayList();

		private IPrintable _printObject;

		public PrintElement(IPrintable printObject)
		{
			this._printObject = printObject;
		}

		public void AddBlankLine()
		{
			this.AddPrimitive(new PrintPrimitiveBlankLine());
		}

		public void AddCategoryText(string Category)
		{
			this.AddPrimitive(new PrintPrimitiveCategoryText(Category));
		}

		public void AddCategoryText(string Category, string Data)
		{
			this.AddPrimitive(new PrintPrimitiveCategoryText(Category, Data));
		}

		public void AddData(string dataName, string dataValue)
		{
			this.AddText(string.Concat(dataName, ": ", dataValue));
		}

		public void AddHeader(string buf)
		{
			this.AddText(buf);
			this.AddHorizontalRule();
		}

		public void AddHorizontalRule()
		{
			this.AddPrimitive(new PrintPrimitiveRule());
		}

		public void AddHTML(string HTML)
		{
		}

		public void AddMText(string buf)
		{
			this.AddPrimitive(new PrintPrimitiveMText(buf));
		}

		public void AddPrimitive(IPrintPrimitive primitive)
		{
			this._printPrimitives.Add(primitive);
		}

		public void AddText(string buf)
		{
			this.AddPrimitive(new PrintPrimitiveText(buf));
		}

		public void AddTitleText(string buf)
		{
			if (Settings.Default.printFontSettings == null)
			{
				Settings.Default.printFontSettings = PrintingSettings.DefaultFont;
			}
			float fontheight = Settings.Default.printFontSettings.Size;
			fontheight = ((double)fontheight < 64.8 ? fontheight * 0.1f : 7f);
			this.AddPrimitive(new PrintPrimitiveTitleText(buf, fontheight));
		}

		public float CalculateHeight(NTAF.PrintEngine.PrintEngine engine, Graphics graphics)
		{
			float height = 0f;
			foreach (IPrintPrimitive primitive in this._printPrimitives)
			{
				height = height + primitive.CalculateHeight(engine, graphics);
			}
			return height;
		}

		public void Draw(NTAF.PrintEngine.PrintEngine engine, float yPos, Graphics graphics, Rectangle pageBounds)
		{
			float height = this.CalculateHeight(engine, graphics);
			Rectangle elementBounds = new Rectangle(pageBounds.Left, (int)yPos, pageBounds.Right - pageBounds.Left, (int)height);
			foreach (IPrintPrimitive primitive in this._printPrimitives)
			{
				primitive.Draw(engine, yPos, graphics, elementBounds);
				yPos = yPos + primitive.CalculateHeight(engine, graphics);
			}
		}
	}
}