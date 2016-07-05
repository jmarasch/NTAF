using System;
using System.Collections;
using System.Drawing;
using NTAF.PrintEngine.Properties;

namespace NTAF.PrintEngine {
    public class PrintElement {
        // members... 
        private ArrayList _printPrimitives = new ArrayList();
        private IPrintable _printObject;

        public PrintElement( IPrintable printObject ) {
            _printObject = printObject;
        }

        public void AddPrimitive( IPrintPrimitive primitive ) {
            _printPrimitives.Add( primitive );
        }

        public void AddText( String buf ) {
            AddPrimitive( new PrintPrimitiveText( buf ) );
        }

        public void AddMText( String buf ) {
            AddPrimitive( new PrintPrimitiveMText( buf ) );
        }

        public void AddTitleText(String buf){
            if(Settings.Default.printFontSettings == null) {
                Settings.Default.printFontSettings = PrintingSettings.DefaultFont;
            }
            
            float fontheight = Settings.Default.printFontSettings.Size;

            if ( fontheight >= 64.8 )
                fontheight = 7;
            else
                fontheight = fontheight * (float)0.1;

            AddPrimitive( new PrintPrimitiveTitleText( buf, fontheight ) );
        }

        public void AddCategoryText( String Category ) {
            AddPrimitive( new PrintPrimitiveCategoryText( Category ) );
        }

        public void AddCategoryText( String Category, String Data ) {
            AddPrimitive( new PrintPrimitiveCategoryText( Category, Data ) );
        }

        public void AddHTML( String HTML ) {

        }

        // AddData - add data to the element... 
        public void AddData( String dataName, String dataValue ) {
            // add this data to the collection... 
            AddText( dataName + ": " + dataValue );
        }

        // AddHorizontalRule - add a rule to the element... 
        public void AddHorizontalRule() {
            // add a rule object... 
            AddPrimitive( new PrintPrimitiveRule() );
        }

        // AddBlankLine - add a blank line... 
        public void AddBlankLine() {
            // add a blank line... 
            AddPrimitive( new PrintPrimitiveBlankLine() );
        }

        // AddHeader - add a header... 
        public void AddHeader( String buf ) {
            AddText( buf );
            AddHorizontalRule();
        }

        public float CalculateHeight( PrintEngine engine, Graphics graphics ) {
            // loop through the print height... 
            float height = 0;
            foreach ( IPrintPrimitive primitive in _printPrimitives ) {
                // get the height... 
                height += primitive.CalculateHeight( engine, graphics );
            }

            // return the height... 
            return height;
        }
        // Draw - draw the element on a graphics object... 
        public void Draw( PrintEngine engine, float yPos, Graphics graphics, Rectangle pageBounds ) {
            // where... 
            float height = CalculateHeight( engine, graphics );
            Rectangle elementBounds = new Rectangle( pageBounds.Left, ( int )yPos, pageBounds.Right - pageBounds.Left, ( int )height );

            // now, tell the primitives to print themselves... 
            foreach ( IPrintPrimitive primitive in _printPrimitives ) {
                // render it... 
                primitive.Draw( engine, yPos, graphics, elementBounds );

                // move to the next line... 
                yPos += primitive.CalculateHeight( engine, graphics );
            }
        }
    }
}
