/* Portions of this Printing engine created with help from
 * http://www.devarticles.com/c/a/C-Sharp/Printing-Using-C-sharp/
 * by:Wrox Team, wroxteam@devarticles.com, http://www.wrox.com/
 */
using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using NTAF.PrintEngine.Properties;

namespace NTAF.PrintEngine {
    

    public class PrintEngine : PrintDocument {
        private ArrayList _printObjects = new ArrayList();
        public Font PrintFont = Properties.Settings.Default.printFontSettings;
        public Brush PrintBrush = Brushes.Black;
        public PrintElement Header;
        public PrintElement Footer;
        private ArrayList _printElements;
        private int _printIndex = 0;
        private int _pageNum = 0;
        internal Rectangle pageBounds = new Rectangle();
        private string dataFileName;

        public Rectangle PageBounds {
            get { return pageBounds; }
            set { pageBounds = value; }
        }
        public Int32 PageWidth {
            get { return pageBounds.Width; }
            set { pageBounds.Width = value; }
        }
        public Int32 PageHeight {
            get { return pageBounds.Height; }
            set { pageBounds.Height = value; }
        }

        public void AddPrintObject( IPrintable printObject ) {
            _printObjects.Add( printObject );
        }

        public void AddPrintObjectsRange( IPrintable[] printObjects ) {
            foreach ( IPrintable iprint in printObjects )
                AddPrintObject( iprint );
        }

        public void ResetPrintables( IPrintable[] printObjects ) {
            _printObjects.Clear();
            foreach ( IPrintable iprint in printObjects )
                AddPrintObject( iprint );
        }

        public PrintEngine(string DataFileName) {
            dataFileName = DataFileName;
            if ( Settings.Default.printFontSettings == null ) {
                Settings.Default.printFontSettings = new Font( "Arial", 10 );
                PrintFont = Settings.Default.printFontSettings;
            }
            if ( Settings.Default.printSettings == null )
                Settings.Default.printSettings = DefaultPageSettings;

            
            // create the header... 
            Header = new PrintElement( null );
            Header.AddTitleText( "New Terra Data [fileName]" );
            Header.AddHorizontalRule();
            Header.AddBlankLine();

            // create the footer... 
            Footer = new PrintElement( null );
            Footer.AddBlankLine();
            Footer.AddHorizontalRule();
            Footer.AddText( "[pagenum]" );
        }

        public PrintPreviewDialog ShowPreview() {
            PrintPreviewDialog dialog = new PrintPreviewDialog();
            dialog.Document = this;

            return dialog;
        }

        protected override void OnBeginPrint( PrintEventArgs e ) {
            _printElements = new ArrayList();
            _pageNum = 0;
            _printIndex = 0;

            foreach ( IPrintable printObject in _printObjects ) {
                PrintElement element = new PrintElement( printObject );
                _printElements.Add( element );

                printObject.Print( element );
            }

        }

        // ReplaceTokens - take a string and remove any tokens replacing them with values... 
        public String ReplaceTokens( String buf ) {
            // replace... 
            buf = buf.Replace( "[pagenum]", _pageNum.ToString() );
            buf = buf.Replace( "[fileName]", dataFileName );

            // return... 
            return buf;
        }

        protected override void OnPrintPage( PrintPageEventArgs e ) {
            _pageNum++;
            float headerHeight = Header.CalculateHeight( this, e.Graphics );
            Header.Draw( this, e.MarginBounds.Top, e.Graphics, e.MarginBounds );
            float footerHeight = Footer.CalculateHeight( this, e.Graphics );
            Footer.Draw( this, e.MarginBounds.Bottom - footerHeight, e.Graphics, e.MarginBounds );
            pageBounds = new Rectangle( e.MarginBounds.Left,
                                      ( int )( e.MarginBounds.Top + headerHeight ),
                                      e.MarginBounds.Width,
                                      ( int )( e.MarginBounds.Height - footerHeight - headerHeight ) );

            float yPos = pageBounds.Top;
            // ok, now we need to loop through the elements... 
            bool morePages = false;
            int elementsOnPage = 0;
            while ( _printIndex < _printElements.Count ) {
                // get the element... 
                PrintElement element = ( PrintElement )_printElements[_printIndex];
                // how tall is the primitive? 
                float height = element.CalculateHeight( this, e.Graphics );

                // will it fit on the page? 
                if ( yPos + height > pageBounds.Bottom ) {
                    // we don't want to do this if we're the first thing on the page... 
                    if ( elementsOnPage != 0 ) {
                        morePages = true;
                        break;
                    }
                }
                // now draw the element... 
                element.Draw( this, yPos, e.Graphics, pageBounds );

                // move the y-pos... 
                yPos += height;

                // next... 
                _printIndex++;
                elementsOnPage++;
            }
            //more pages?
            e.HasMorePages = morePages;
        }

        public PageSetupDialog ShowPageSettings() {
            PageSetupDialog retVal = new PageSetupDialog();

            if ( Properties.Settings.Default.printSettings == null )
                Properties.Settings.Default.printSettings = DefaultPageSettings;

            PageSettings settings = Properties.Settings.Default.printSettings;
            retVal.PageSettings = settings;

            return retVal;
        }

        public PrintDialog ShowPrintDialog() {
            PrintDialog retVal = new PrintDialog();

            if ( Properties.Settings.Default.printerSettings == null )
                Properties.Settings.Default.printerSettings = PrinterSettings;

            retVal.PrinterSettings = Properties.Settings.Default.printerSettings;
            retVal.Document = this;

            retVal.UseEXDialog = true;

            return retVal;
        }

        public FontDialog ShowFontDialog() {
            FontDialog retVal = new FontDialog();
            if ( Properties.Settings.Default.printFontSettings == null )
                Properties.Settings.Default.printFontSettings = new Font( "Arial", 10 );

            retVal.Font = Properties.Settings.Default.printFontSettings;

            return retVal;
        }
    }

    
}
