using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;
using NTDataCore;

namespace NTPrintEngine {
    class GridPrimitive : IPrintPrimitive {
        // CalculateHeight - work out how tall the primitive is... 
        public float CalculateHeight( PrintEngine engine, Graphics graphics ) {

            return 5;
        }

        // Print - draw the rule... 
        public void Draw( PrintEngine engine, float yPos, Graphics graphics, Rectangle elementBounds ) {
            // draw a line... 
            Pen pen = new Pen( engine.PrintBrush, 1 );
            graphics.DrawLine( pen, elementBounds.Left, yPos + 2, elementBounds.Right, yPos + 2 );
            
        }
    }

    public class PrintStatGrid : IPrintPrimitive {
        const float
            buffer = 4;

        private String[] StatValues = new String[11];

        String[]
                StatHeaderText = new String[] { 
                    "M", 
                    "HP", 
                    "H+H", 
                    "AP",
                    "PP", 
                    "RW",
                    "MT", 
                    "T",
                    "AG", 
                    "WP", 
                    "HF" };

        //String[]
        //    StatHeaderText = new String[] { 
        //        "Movement", 
        //        "Hit Points", 
        //        "Hand to Hand", 
        //        "Actions",
        //        "Psy Points", 
        //        "Ranged Weapons",
        //        "Might", 
        //        "Toughness",
        //        "Agility", 
        //        "Willpower", 
        //        "Horror Factor" };


        public PrintStatGrid( int M, int HP, int HH, int AP, int PP, int RW, int MT, int T, int AG, int WP, int HF ) {
            StatValues[0] = M.ToString();
            StatValues[1] = HP.ToString();
            StatValues[2] = HH.ToString();
            StatValues[3] = AP.ToString();
            StatValues[4] = PP.ToString();
            StatValues[5] = RW.ToString();
            StatValues[6] = MT.ToString();
            StatValues[7] = T.ToString();
            StatValues[8] = AG.ToString();
            StatValues[9] = WP.ToString();
            StatValues[10] = HF.ToString();
        }

        public float CalculateHeight( PrintEngine engine, Graphics graphics ) {
            float 
                retVal = 0;

            retVal += engine.PrintFont.GetHeight( graphics ) * 2;
            retVal += buffer * 4;
            return retVal;
        }

        public void Draw( PrintEngine engine, float yPos, Graphics graphics, Rectangle elementBounds ) {
            List<float>
                HorizontalLineSpacing  = new List<float>(),
                HorizontalTextSpacing = new List<float>();

            Single[]
                colWidths = new Single[11];

            HorizontalLineSpacing.Add( ( float )elementBounds.Left );
            for ( int space = 0; space <= 10; space++ )
                if ( graphics.MeasureString( StatHeaderText[space], engine.PrintFont ).Width
                    >=
                    graphics.MeasureString( StatValues[space], engine.PrintFont ).Width )
                    HorizontalLineSpacing.Add( ( buffer * 2 ) + HorizontalLineSpacing[space] + graphics.MeasureString( StatHeaderText[space], engine.PrintFont ).Width );
                else
                    HorizontalLineSpacing.Add( ( buffer * 2 ) + HorizontalLineSpacing[space] + graphics.MeasureString( StatValues[space], engine.PrintFont ).Width );

            for ( int space = 0; space <= 10; space++ )
                HorizontalTextSpacing.Add( ( ( HorizontalLineSpacing[space + 1] - HorizontalLineSpacing[space] ) / 2 ) + HorizontalLineSpacing[space] );

            Pen pen = new Pen( engine.PrintBrush, 1 );
            foreach ( float x in HorizontalLineSpacing )
                graphics.DrawLine( pen, x, yPos, x, yPos + this.CalculateHeight( engine, graphics ) );

            graphics.DrawLine( pen, HorizontalLineSpacing[0], yPos, HorizontalLineSpacing[11], yPos );
            graphics.DrawLine( pen, HorizontalLineSpacing[0], yPos + this.CalculateHeight( engine, graphics ) / 2, HorizontalLineSpacing[11], yPos + this.CalculateHeight( engine, graphics ) / 2 );
            graphics.DrawLine( pen, HorizontalLineSpacing[0], yPos + this.CalculateHeight( engine, graphics ), HorizontalLineSpacing[11], yPos + this.CalculateHeight( engine, graphics ) );

            for ( int space = 0; space <= 10; space++ ) {
                graphics.DrawString( StatHeaderText[space], engine.PrintFont, engine.PrintBrush, HorizontalTextSpacing[space], yPos + buffer + ( float )( ( float )engine.PrintFont.Height * 0.5 ), StringHelper.AlignTC() );
                graphics.DrawString( StatValues[space], engine.PrintFont, engine.PrintBrush, HorizontalTextSpacing[space], yPos + ( buffer * 3 ) + pen.Width + ( float )( ( float )engine.PrintFont.Height * 1.5 ), StringHelper.AlignTC() );

            }
        }
    }

    [Serializable()]
    public class Cell : IMemberCopy {
        #region Fields
        public static Cell
            defaultSettings = new Cell( "", StringAlignment.Center, new Font( "Tahoma", ( float )11.0 ) );

        String
            i_Contents;

        Font
            i_Font;

        StringAlignment
            i_Alignment;

        Color
            i_Background = Color.White,
            i_Foreground = Color.Black;

        #endregion

        #region Properties
        [XmlAnyAttribute(), Description("Contents of cell"), Category("Content")]
        public String Contents {
            get { return i_Contents; }
            set { i_Contents = value; }
        }

        [Description( "Cell Font" ), Category( "Font" )]
        public Font Font {
            get { return i_Font; }
            set { i_Font = value; }
        }

        [XmlAnyAttribute(), Description( "Text alignment" ), Category( "Font" )]
        public StringAlignment Alignment {
            get { return i_Alignment; }
            set { i_Alignment = value; }
        }

        [XmlAnyAttribute(), Description( "Sets the background color of the cell" ), Category( "Colors" )]
        public Color Background {
            get { return i_Background; }
            set { i_Background = value; }
        }

        [XmlAnyAttribute(), Description( "Sets the text color of the cell" ), Category( "Colors" )]
        public Color Foreground {
            get { return i_Foreground; }
            set { i_Foreground = value; }
        }

        #endregion

        #region Constructors
        public Cell() { CopyMembers( defaultSettings ); }
        public Cell( Cell cell ) { }
        public Cell( String content ) { Contents = content; }
        public Cell( String content, StringAlignment alignment ) {
            Contents = content;
            Alignment = alignment;
        }
        public Cell( String content, StringAlignment alignment, Font font ) {
            Contents = content;
            Alignment = alignment;
            Font = font;
        }

        #endregion

        #region Methods
        public SizeF ContentSize( Graphics g ) {
            return g.MeasureString( Contents, Font );
        } 
        #endregion

        #region IMemberCopy Members

        public void CopyMembers( object members ) {
            if ( !( members is Cell ) )
                throw new ArgumentException( "Object is not of the correct type" );

            Contents = ((Cell)members).Contents;
            Alignment = ( ( Cell )members ).Alignment;
            Font = ( ( Cell )members ).Font;
        }

        #endregion
    }

    [Serializable()]
    public class Row {
        float
            i_Height = 14.0F;

        Boolean
            i_Hidden = false;

        [XmlAnyAttribute(), Description( "Sets or Gets the row height" ), Category( "Size" )]
        public float Height { get { return i_Height; } set { i_Height = value; } }

        [XmlAnyAttribute(), Description( "Will hide or unhide a row while maintaing its last size" ), Category( "Visibility" )]
        public Boolean Hidden { get { return i_Hidden; } }

        public Row() { }
        public Row( float RowHeight ) { Height = RowHeight; }
    }
    
    [Serializable()]
    public class Column {
        float
            i_Width = 14.0F;

        Boolean
            i_Hidden = false;

        [XmlAnyAttribute(), Description( "Sets or Gets the column width" ), Category( "Size" )]
        public float Width { get { return i_Width; } set { i_Width = value; } }

        [XmlAnyAttribute(), Description( "Will hide or unhide a row while maintaing its last size" ), Category( "Visibility" )]
        public Boolean Hidden { get { return i_Hidden; } }

        public Column() { }
        public Column( float RowWidth ) { Width = RowWidth; }
    }

    [Serializable()]
    public class Grid {
        static UInt16
            defaultSize = 65535;
        
        Cell[,]
            i_Cells = new Cell[defaultSize, defaultSize];

        Row[]
            i_Rows = new Row[defaultSize];

        Column[]
            i_Columns = new Column[defaultSize];

        public int RowCount {
            get { return i_Rows.Length; }
        }

        public int ColumnCount {
            get { return i_Columns.Length; }
        }

        public Size GridSize {
            get { return new Size( ColumnCount, RowCount ); }
            //set { i_Cells = new Cell[value.Width, value.Height]; }
        }

        public Grid() { }
        public Grid(int Columns, int Rows) {
            
        }

        public void AddRows( int RowsToAdd ) {

        }

        public void RemoveRow( int RowToRemove ) {

        }

        public void AddColumns( int ColumnsToAdd ) {

        }

        public void RemoveColumn( int ColumnToRemove ) {

        }
    }
}
