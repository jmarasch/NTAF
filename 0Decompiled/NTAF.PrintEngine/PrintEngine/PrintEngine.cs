using NTAF.PrintEngine.Properties;
using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace NTAF.PrintEngine
{
	public class PrintEngine : PrintDocument
	{
		private ArrayList _printObjects = new ArrayList();

		public Font PrintFont = Settings.Default.printFontSettings;

		public Brush PrintBrush = Brushes.Black;

		public PrintElement Header;

		public PrintElement Footer;

		private ArrayList _printElements;

		private int _printIndex = 0;

		private int _pageNum = 0;

		internal Rectangle pageBounds = new Rectangle();

		private string dataFileName;

		public Rectangle PageBounds
		{
			get
			{
				return this.pageBounds;
			}
			set
			{
				this.pageBounds = value;
			}
		}

		public int PageHeight
		{
			get
			{
				return this.pageBounds.Height;
			}
			set
			{
				this.pageBounds.Height = value;
			}
		}

		public int PageWidth
		{
			get
			{
				return this.pageBounds.Width;
			}
			set
			{
				this.pageBounds.Width = value;
			}
		}

		public PrintEngine(string DataFileName)
		{
			this.dataFileName = DataFileName;
			if (Settings.Default.printFontSettings == null)
			{
				Settings.Default.printFontSettings = new Font("Arial", 10f);
				this.PrintFont = Settings.Default.printFontSettings;
			}
			if (Settings.Default.printSettings == null)
			{
				Settings.Default.printSettings = base.DefaultPageSettings;
			}
			this.Header = new PrintElement(null);
			this.Header.AddTitleText("New Terra Data [fileName]");
			this.Header.AddHorizontalRule();
			this.Header.AddBlankLine();
			this.Footer = new PrintElement(null);
			this.Footer.AddBlankLine();
			this.Footer.AddHorizontalRule();
			this.Footer.AddText("[pagenum]");
		}

		public void AddPrintObject(IPrintable printObject)
		{
			this._printObjects.Add(printObject);
		}

		public void AddPrintObjectsRange(IPrintable[] printObjects)
		{
			IPrintable[] printableArray = printObjects;
			for (int i = 0; i < (int)printableArray.Length; i++)
			{
				this.AddPrintObject(printableArray[i]);
			}
		}

		protected override void OnBeginPrint(PrintEventArgs e)
		{
			this._printElements = new ArrayList();
			this._pageNum = 0;
			this._printIndex = 0;
			foreach (IPrintable printObject in this._printObjects)
			{
				PrintElement element = new PrintElement(printObject);
				this._printElements.Add(element);
				printObject.Print(element);
			}
		}

		protected override void OnPrintPage(PrintPageEventArgs e)
		{
			NTAF.PrintEngine.PrintEngine printEngine = this;
			printEngine._pageNum = printEngine._pageNum + 1;
			float headerHeight = this.Header.CalculateHeight(this, e.Graphics);
			PrintElement header = this.Header;
			Rectangle marginBounds = e.MarginBounds;
			header.Draw(this, (float)marginBounds.Top, e.Graphics, e.MarginBounds);
			float footerHeight = this.Footer.CalculateHeight(this, e.Graphics);
			PrintElement footer = this.Footer;
			marginBounds = e.MarginBounds;
			footer.Draw(this, (float)marginBounds.Bottom - footerHeight, e.Graphics, e.MarginBounds);
			int left = e.MarginBounds.Left;
			marginBounds = e.MarginBounds;
			int top = (int)((float)marginBounds.Top + headerHeight);
			int width = e.MarginBounds.Width;
			marginBounds = e.MarginBounds;
			this.pageBounds = new Rectangle(left, top, width, (int)((float)marginBounds.Height - footerHeight - headerHeight));
			float yPos = (float)this.pageBounds.Top;
			bool morePages = false;
			int elementsOnPage = 0;
			while (this._printIndex < this._printElements.Count)
			{
				PrintElement element = (PrintElement)this._printElements[this._printIndex];
				float height = element.CalculateHeight(this, e.Graphics);
				if (yPos + height > (float)this.pageBounds.Bottom)
				{
					if (elementsOnPage != 0)
					{
						morePages = true;
						break;
					}
				}
				element.Draw(this, yPos, e.Graphics, this.pageBounds);
				yPos = yPos + height;
				NTAF.PrintEngine.PrintEngine printEngine1 = this;
				printEngine1._printIndex = printEngine1._printIndex + 1;
				elementsOnPage++;
			}
			e.HasMorePages = morePages;
		}

		public string ReplaceTokens(string buf)
		{
			buf = buf.Replace("[pagenum]", this._pageNum.ToString());
			buf = buf.Replace("[fileName]", this.dataFileName);
			return buf;
		}

		public void ResetPrintables(IPrintable[] printObjects)
		{
			this._printObjects.Clear();
			IPrintable[] printableArray = printObjects;
			for (int i = 0; i < (int)printableArray.Length; i++)
			{
				this.AddPrintObject(printableArray[i]);
			}
		}

		public FontDialog ShowFontDialog()
		{
			FontDialog retVal = new FontDialog();
			if (Settings.Default.printFontSettings == null)
			{
				Settings.Default.printFontSettings = new Font("Arial", 10f);
			}
			retVal.Font = Settings.Default.printFontSettings;
			return retVal;
		}

		public PageSetupDialog ShowPageSettings()
		{
			PageSetupDialog retVal = new PageSetupDialog();
			if (Settings.Default.printSettings == null)
			{
				Settings.Default.printSettings = base.DefaultPageSettings;
			}
			retVal.PageSettings = Settings.Default.printSettings;
			return retVal;
		}

		public PrintPreviewDialog ShowPreview()
		{
			return new PrintPreviewDialog()
			{
				Document = this
			};
		}

		public PrintDialog ShowPrintDialog()
		{
			PrintDialog retVal = new PrintDialog();
			if (Settings.Default.printerSettings == null)
			{
				Settings.Default.printerSettings = base.PrinterSettings;
			}
			retVal.PrinterSettings = Settings.Default.printerSettings;
			retVal.Document = this;
			retVal.UseEXDialog = true;
			return retVal;
		}
	}
}