//StringFormat fmt = new StringFormat();  
  
//fmt.Alignment = StringAlignment.Center;  
  
//fmt.LineAlignment = StringAlignment.Center;  
  
//e.Graphics.FillRectangle(backcolorBrush, e.Bounds);  
  
//e.Graphics.DrawString(tabPage.Text, this.Font, foreColor, e.Bounds,  
  
//fmt);  


using System;
using System.Drawing;
using System.Collections.Generic;

namespace NTAF.PrintEngine {

    public class PrintPrimitiveRule : IPrintPrimitive {
        // CalculateHeight - work out how tall the primitive is... 
        public float CalculateHeight( PrintEngine engine, Graphics graphics ) {
            // we're always five units tall... 
            return 5;
        }

        // Print - draw the rule... 
        public void Draw( PrintEngine engine, float yPos, Graphics graphics, Rectangle elementBounds ) {
            // draw a line... 
            Pen pen = new Pen( engine.PrintBrush, 1 );
            graphics.DrawLine( pen, elementBounds.Left, yPos + 2, elementBounds.Right, yPos + 2 );
        }

    }

    public class PrintPrimitiveText : IPrintPrimitive {
        // members... 
        private String Text;

        public PrintPrimitiveText( String buf ) {
            Text = buf;
        }

        // CalculateHeight - work out how tall the primitive is... 
        public float CalculateHeight( PrintEngine engine, Graphics graphics ) {
            // return the height... 
            return engine.PrintFont.GetHeight( graphics );
        }

        // Print - draw the text... 
        public void Draw( PrintEngine engine, float yPos, Graphics graphics, Rectangle elementBounds ) {
            // draw it... 
            
            graphics.DrawString( engine.ReplaceTokens( Text ), engine.PrintFont, engine.PrintBrush, elementBounds.Left, yPos, new StringFormat() );
        }
        

    }

    public class PrintPrimitiveTitleText : IPrintPrimitive {
        // members... 
        private String Text;
        private float sizeChange;

        public PrintPrimitiveTitleText( String buf, float SizeAdjusment ) {
            Text = buf;
            sizeChange = SizeAdjusment;
        }

        // CalculateHeight - work out how tall the primitive is... 
        public float CalculateHeight( PrintEngine engine, Graphics graphics ) {
            // return the height... 
            Font tmpFont = new Font( engine.PrintFont.FontFamily, engine.PrintFont.Size + sizeChange, FontStyle.Bold );

            return tmpFont.GetHeight( graphics );
        }

        // Print - draw the text... 
        public void Draw( PrintEngine engine, float yPos, Graphics graphics, Rectangle elementBounds ) {
            // draw it... 
            Font tmpFont = new Font( engine.PrintFont.FontFamily, engine.PrintFont.Size + sizeChange, FontStyle.Bold );
            graphics.DrawString( engine.ReplaceTokens( Text ), tmpFont, engine.PrintBrush, elementBounds.Left, yPos, new StringFormat() );
        }


    }

    public class PrintPrimitiveBlankLine : IPrintPrimitive {
        // members... 

        public PrintPrimitiveBlankLine() { }

        // CalculateHeight - work out how tall the primitive is... 
        public float CalculateHeight( PrintEngine engine, Graphics graphics ) {
            // return the height... 
            float blah = graphics.MeasureString( "X", engine.PrintFont ).Height;
            return blah;// engine.PrintFont.GetHeight( graphics );
        }

        // Print - draw the text... 
        public void Draw( PrintEngine engine, float yPos, Graphics graphics, Rectangle elementBounds ) {
            // draw it... 
            graphics.DrawString( "", engine.PrintFont, engine.PrintBrush, elementBounds.Left, yPos, new StringFormat() );
        }


    }

    public class PrintPrimitiveCategoryText : IPrintPrimitive {
        // members... 
        private String Category;
        private String Data;

        public PrintPrimitiveCategoryText( String category ) {
            Category = category;
            Data = "";
        }

        public PrintPrimitiveCategoryText( String category, String data ) {
            Category = category;
            Data = data;
        }

        // CalculateHeight - work out how tall the primitive is... 
        public float CalculateHeight( PrintEngine engine, Graphics graphics ) {
            // return the height... 
            Font tmpFont = new Font( engine.PrintFont.FontFamily, engine.PrintFont.Size, FontStyle.Bold );

            return tmpFont.GetHeight( graphics );
        }

        // Print - draw the text... 
        public void Draw( PrintEngine engine, float yPos, Graphics graphics, Rectangle elementBounds ) {
            // draw it... 
            Font boldFont = new Font( engine.PrintFont.FontFamily, engine.PrintFont.Size, FontStyle.Bold );
            graphics.DrawString( engine.ReplaceTokens( Category ), boldFont, engine.PrintBrush, elementBounds.Left, yPos, new StringFormat() );
            float dataLeft = elementBounds.Left;
            dataLeft += graphics.MeasureString( engine.ReplaceTokens( Category ), boldFont ).Width;
            if(Data!="")
                graphics.DrawString( engine.ReplaceTokens( Data ), engine.PrintFont, engine.PrintBrush, dataLeft, yPos, new StringFormat() );
        }


    }

    public class PrintPrimitiveMText : IPrintPrimitive {
        // members... 
        private String Text;

        public PrintPrimitiveMText( String buf ) {
            Text = buf;
        }

        // CalculateHeight - work out how tall the primitive is... 
        public float CalculateHeight( PrintEngine engine, Graphics graphics ) {
            // return the height...
            float retVal = engine.PrintFont.GetHeight( graphics );
            if ( Text != "" )
                retVal *= ( float )( MultiLine( engine, graphics ).Length );
            return retVal;
        }

        // Print - draw the text... 
        public void Draw( PrintEngine engine, float yPos, Graphics graphics, Rectangle elementBounds ) {
            // draw it... 
            string[] printLines = MultiLine( engine, graphics );

            for ( int x = 0; x <= printLines.Length - 1; x++ )
                graphics.DrawString( engine.ReplaceTokens( printLines[x] ), engine.PrintFont, engine.PrintBrush, elementBounds.Left, yPos + ( x * engine.PrintFont.Height ), new StringFormat() );
        }

        private string[] MultiLine( PrintEngine engine, Graphics graphics ) {
            List<string> retVal = new List<string>();
            List<string> words = new List<string>();
            Rectangle pageBounds = engine.pageBounds;

            words.AddRange( Text.Split( ' ' ) );

            string line = "";
            foreach ( string word in words ) {
                if ( graphics.MeasureString( line + word, engine.PrintFont ).Width > pageBounds.Width ) {
                    retVal.Add( line );
                    line = "";
                }
                line += word + " ";
            }
            if ( line != "" || line != " " )
                retVal.Add( line );
            return retVal.ToArray();
        }
    }

    //public class PrintStatGrid : IPrintPrimitive {
    //    const float buffer = 4;

    //    private int _Movement,
    //                 _HitPoints, 
    //                 _HandToHand, 
    //                 _AttackPoints,
    //                 _PsyPoints,
    //                 _RangedWeapons,
    //                 _Might,
    //                 _Toughness,
    //                 _Agility,
    //                 _Willpower,
    //                 _HorrorFactor;
       


    //    public PrintStatGrid( int M, int HP, int HH, int AP, int PP, int RW, int MT, int T, int AG, int WP, int HF ) {
    //        _Movement = M;
    //        _HitPoints = HP;
    //        _HandToHand = HH;
    //        _AttackPoints = AP;
    //        _PsyPoints = PP;
    //        _RangedWeapons = RW;
    //        _Might = MT;
    //        _Toughness = T;
    //        _Agility = AG;
    //        _Willpower = WP;
    //        _HorrorFactor = HF;
    //    }

    //    public float CalculateHeight( PrintEngine engine, Graphics graphics ) {
    //        float retVal = 0;
    //        retVal += engine.PrintFont.GetHeight( graphics ) * 2;
    //        retVal += buffer * 4;
    //        return retVal;
    //    }

    //    public void Draw( PrintEngine engine, float yPos, Graphics graphics, Rectangle elementBounds ) {
    //        List<float> HorizontalLineSpacing = new List<float>();
    //        List<float> HorizontalTextSpacing1 = new List<float>();
    //        List<float> HorizontalTextSpacing2 = new List<float>();

    //        float MovementHeaderWidth = graphics.MeasureString( "M", engine.PrintFont ).Width;
    //        float HitPointsHeaderWidth = graphics.MeasureString( "HP", engine.PrintFont ).Width;
    //        float HandToHandHeaderWidth = graphics.MeasureString( "H+H", engine.PrintFont ).Width;
    //        float AttackPointsHeaderWidth = graphics.MeasureString( "AP", engine.PrintFont ).Width;
    //        float PsyPointsHeaderWidth = graphics.MeasureString( "PP", engine.PrintFont ).Width;
    //        float RangedWeaponsHeaderWidth = graphics.MeasureString( "RW", engine.PrintFont ).Width;
    //        float MightHeaderWidth = graphics.MeasureString( "MT", engine.PrintFont ).Width;
    //        float ToughnessHeaderWidth = graphics.MeasureString( "T", engine.PrintFont ).Width;
    //        float AgilityHeaderWidth = graphics.MeasureString( "AG", engine.PrintFont ).Width;
    //        float WillpowerHeaderWidth = graphics.MeasureString( "WP", engine.PrintFont ).Width;
    //        float HorrorFactorHeaderWidth = graphics.MeasureString( "HF", engine.PrintFont ).Width;

    //        float MovementValueWidth = graphics.MeasureString( _Movement.ToString(), engine.PrintFont ).Width;
    //        float HitPointsValueWidth = graphics.MeasureString( _HitPoints.ToString(), engine.PrintFont ).Width;
    //        float HandToHandValueWidth = graphics.MeasureString( _HandToHand.ToString(), engine.PrintFont ).Width;
    //        float AttackPointsValueWidth = graphics.MeasureString( _AttackPoints.ToString(), engine.PrintFont ).Width;
    //        float PsyPointsValueWidth = graphics.MeasureString( _PsyPoints.ToString(), engine.PrintFont ).Width;
    //        float RangedWeaponsValueWidth = graphics.MeasureString( _RangedWeapons.ToString(), engine.PrintFont ).Width;
    //        float MightValueWidth = graphics.MeasureString( _Might.ToString(), engine.PrintFont ).Width;
    //        float ToughnessValueWidth = graphics.MeasureString( _Toughness.ToString(), engine.PrintFont ).Width;
    //        float AgilityValueWidth = graphics.MeasureString( _Agility.ToString(), engine.PrintFont ).Width;
    //        float WillpowerValueWidth = graphics.MeasureString( _Willpower.ToString(), engine.PrintFont ).Width;
    //        float HorrorFactorValueWidth = graphics.MeasureString( _HorrorFactor.ToString(), engine.PrintFont ).Width;

    //        HorizontalLineSpacing.Add( ( float )elementBounds.Left );

    //        HorizontalLineSpacing.Add( ( buffer * 2 ) + HorizontalLineSpacing[0] + ( MovementHeaderWidth >= MovementValueWidth ? MovementHeaderWidth : MovementValueWidth ) );
    //        HorizontalLineSpacing.Add( ( buffer * 2 ) + HorizontalLineSpacing[1] + ( HitPointsHeaderWidth >= HitPointsValueWidth ? HitPointsHeaderWidth : HitPointsValueWidth ) );
    //        HorizontalLineSpacing.Add( ( buffer * 2 ) + HorizontalLineSpacing[2] + ( HandToHandHeaderWidth >= HandToHandValueWidth ? HandToHandHeaderWidth : HandToHandValueWidth ) );
    //        HorizontalLineSpacing.Add( ( buffer * 2 ) + HorizontalLineSpacing[3] + ( AttackPointsHeaderWidth >= AttackPointsValueWidth ? AttackPointsHeaderWidth : AttackPointsValueWidth ) );
    //        HorizontalLineSpacing.Add( ( buffer * 2 ) + HorizontalLineSpacing[4] + ( PsyPointsHeaderWidth >= PsyPointsValueWidth ? PsyPointsHeaderWidth : PsyPointsValueWidth ) );
    //        HorizontalLineSpacing.Add( ( buffer * 2 ) + HorizontalLineSpacing[5] + ( RangedWeaponsHeaderWidth >= RangedWeaponsValueWidth ? RangedWeaponsHeaderWidth : RangedWeaponsValueWidth ) );
    //        HorizontalLineSpacing.Add( ( buffer * 2 ) + HorizontalLineSpacing[6] + ( MightHeaderWidth >= MightValueWidth ? MightHeaderWidth : MightValueWidth ) );
    //        HorizontalLineSpacing.Add( ( buffer * 2 ) + HorizontalLineSpacing[7] + ( ToughnessHeaderWidth >= ToughnessValueWidth ? ToughnessHeaderWidth : ToughnessValueWidth ) );
    //        HorizontalLineSpacing.Add( ( buffer * 2 ) + HorizontalLineSpacing[8] + ( AgilityHeaderWidth >= AgilityValueWidth ? AgilityHeaderWidth : AgilityValueWidth ) );
    //        HorizontalLineSpacing.Add( ( buffer * 2 ) + HorizontalLineSpacing[9] + ( WillpowerHeaderWidth >= WillpowerValueWidth ? WillpowerHeaderWidth : WillpowerValueWidth ) );
    //        HorizontalLineSpacing.Add( ( buffer * 2 ) + HorizontalLineSpacing[10] + ( HorrorFactorHeaderWidth >= HorrorFactorValueWidth ? HorrorFactorHeaderWidth : HorrorFactorValueWidth ) );

    //        HorizontalTextSpacing1.Add( getTextStartPoints( MovementHeaderWidth, MovementValueWidth, HorizontalLineSpacing[0] )[0] );
    //        HorizontalTextSpacing1.Add( getTextStartPoints( HitPointsHeaderWidth, HitPointsValueWidth, HorizontalLineSpacing[1] )[0] );
    //        HorizontalTextSpacing1.Add( getTextStartPoints( HandToHandHeaderWidth, HandToHandValueWidth, HorizontalLineSpacing[2] )[0] );
    //        HorizontalTextSpacing1.Add( getTextStartPoints( AttackPointsHeaderWidth, AttackPointsValueWidth, HorizontalLineSpacing[3] )[0] );
    //        HorizontalTextSpacing1.Add( getTextStartPoints( PsyPointsHeaderWidth, PsyPointsValueWidth, HorizontalLineSpacing[4] )[0] );
    //        HorizontalTextSpacing1.Add( getTextStartPoints( RangedWeaponsHeaderWidth, RangedWeaponsValueWidth, HorizontalLineSpacing[5] )[0] );
    //        HorizontalTextSpacing1.Add( getTextStartPoints( MightHeaderWidth, MightValueWidth, HorizontalLineSpacing[6] )[0] );
    //        HorizontalTextSpacing1.Add( getTextStartPoints( ToughnessHeaderWidth, ToughnessValueWidth, HorizontalLineSpacing[7] )[0] );
    //        HorizontalTextSpacing1.Add( getTextStartPoints( AgilityHeaderWidth, AgilityValueWidth, HorizontalLineSpacing[8] )[0] );
    //        HorizontalTextSpacing1.Add( getTextStartPoints( WillpowerHeaderWidth, WillpowerValueWidth, HorizontalLineSpacing[9] )[0] );
    //        HorizontalTextSpacing1.Add( getTextStartPoints( HorrorFactorHeaderWidth, HorrorFactorValueWidth, HorizontalLineSpacing[10] )[0] );

    //        HorizontalTextSpacing2.Add( getTextStartPoints( MovementHeaderWidth, MovementValueWidth, HorizontalLineSpacing[0] )[1] );
    //        HorizontalTextSpacing2.Add( getTextStartPoints( HitPointsHeaderWidth, HitPointsValueWidth, HorizontalLineSpacing[1] )[1] );
    //        HorizontalTextSpacing2.Add( getTextStartPoints( HandToHandHeaderWidth, HandToHandValueWidth, HorizontalLineSpacing[2] )[1] );
    //        HorizontalTextSpacing2.Add( getTextStartPoints( AttackPointsHeaderWidth, AttackPointsValueWidth, HorizontalLineSpacing[3] )[1] );
    //        HorizontalTextSpacing2.Add( getTextStartPoints( PsyPointsHeaderWidth, PsyPointsValueWidth, HorizontalLineSpacing[4] )[1] );
    //        HorizontalTextSpacing2.Add( getTextStartPoints( RangedWeaponsHeaderWidth, RangedWeaponsValueWidth, HorizontalLineSpacing[5] )[1] );
    //        HorizontalTextSpacing2.Add( getTextStartPoints( MightHeaderWidth, MightValueWidth, HorizontalLineSpacing[6] )[1] );
    //        HorizontalTextSpacing2.Add( getTextStartPoints( ToughnessHeaderWidth, ToughnessValueWidth, HorizontalLineSpacing[7] )[1] );
    //        HorizontalTextSpacing2.Add( getTextStartPoints( AgilityHeaderWidth, AgilityValueWidth, HorizontalLineSpacing[8] )[1] );
    //        HorizontalTextSpacing2.Add( getTextStartPoints( WillpowerHeaderWidth, WillpowerValueWidth, HorizontalLineSpacing[9] )[1] );
    //        HorizontalTextSpacing2.Add( getTextStartPoints( HorrorFactorHeaderWidth, HorrorFactorValueWidth, HorizontalLineSpacing[10] )[1] );

    //        Pen pen = new Pen( engine.PrintBrush, 1 );
    //        foreach ( float x in HorizontalLineSpacing )
    //            graphics.DrawLine( pen, x, yPos, x, yPos + this.CalculateHeight( engine, graphics ) );

    //        graphics.DrawLine( pen, HorizontalLineSpacing[0], yPos, HorizontalLineSpacing[11], yPos );
    //        graphics.DrawLine( pen, HorizontalLineSpacing[0], yPos + this.CalculateHeight( engine, graphics ) / 2, HorizontalLineSpacing[11], yPos + this.CalculateHeight( engine, graphics ) / 2 );
    //        graphics.DrawLine( pen, HorizontalLineSpacing[0], yPos + this.CalculateHeight( engine, graphics ), HorizontalLineSpacing[11], yPos + this.CalculateHeight( engine, graphics ) );

    //        graphics.DrawString( "M", engine.PrintFont, engine.PrintBrush, HorizontalTextSpacing1[0], yPos + buffer, new StringFormat() );
    //        graphics.DrawString( "HP", engine.PrintFont, engine.PrintBrush, HorizontalTextSpacing1[1], yPos + buffer, new StringFormat() );
    //        graphics.DrawString( "H+H", engine.PrintFont, engine.PrintBrush, HorizontalTextSpacing1[2], yPos + buffer, new StringFormat() );
    //        graphics.DrawString( "AP", engine.PrintFont, engine.PrintBrush, HorizontalTextSpacing1[3], yPos + buffer, new StringFormat() );
    //        graphics.DrawString( "PP", engine.PrintFont, engine.PrintBrush, HorizontalTextSpacing1[4], yPos + buffer, new StringFormat() );
    //        graphics.DrawString( "RW", engine.PrintFont, engine.PrintBrush, HorizontalTextSpacing1[5], yPos + buffer, new StringFormat() );
    //        graphics.DrawString( "MT", engine.PrintFont, engine.PrintBrush, HorizontalTextSpacing1[6], yPos + buffer, new StringFormat() );
    //        graphics.DrawString( "T", engine.PrintFont, engine.PrintBrush, HorizontalTextSpacing1[7], yPos + buffer, new StringFormat() );
    //        graphics.DrawString( "AG", engine.PrintFont, engine.PrintBrush, HorizontalTextSpacing1[8], yPos + buffer, new StringFormat() );
    //        graphics.DrawString( "WP", engine.PrintFont, engine.PrintBrush, HorizontalTextSpacing1[9], yPos + buffer, new StringFormat() );
    //        graphics.DrawString( "HF", engine.PrintFont, engine.PrintBrush, HorizontalTextSpacing1[10], yPos + buffer, new StringFormat() );

    //        graphics.DrawString( _Movement.ToString(), engine.PrintFont, engine.PrintBrush, HorizontalTextSpacing2[0], yPos + ( buffer * 3 ) + pen.Width + (float)engine.PrintFont.Height, new StringFormat() );
    //        graphics.DrawString( _HitPoints.ToString(), engine.PrintFont, engine.PrintBrush, HorizontalTextSpacing2[1], yPos + ( buffer * 3 ) + pen.Width + ( float )engine.PrintFont.Height, new StringFormat() );
    //        graphics.DrawString( _HandToHand.ToString(), engine.PrintFont, engine.PrintBrush, HorizontalTextSpacing2[2], yPos + ( buffer * 3 ) + pen.Width + ( float )engine.PrintFont.Height, new StringFormat() );
    //        graphics.DrawString( _AttackPoints.ToString(), engine.PrintFont, engine.PrintBrush, HorizontalTextSpacing2[3], yPos + ( buffer * 3 ) + pen.Width + ( float )engine.PrintFont.Height, new StringFormat() );
    //        graphics.DrawString( _PsyPoints.ToString(), engine.PrintFont, engine.PrintBrush, HorizontalTextSpacing2[4], yPos + ( buffer * 3 ) + pen.Width + ( float )engine.PrintFont.Height, new StringFormat() );
    //        graphics.DrawString( _RangedWeapons.ToString(), engine.PrintFont, engine.PrintBrush, HorizontalTextSpacing2[5], yPos + ( buffer * 3 ) + pen.Width + ( float )engine.PrintFont.Height, new StringFormat() );
    //        graphics.DrawString( _Might.ToString(), engine.PrintFont, engine.PrintBrush, HorizontalTextSpacing2[6], yPos + ( buffer * 3 ) + pen.Width + ( float )engine.PrintFont.Height, new StringFormat() );
    //        graphics.DrawString( _Toughness.ToString(), engine.PrintFont, engine.PrintBrush, HorizontalTextSpacing2[7], yPos + ( buffer * 3 ) + pen.Width + ( float )engine.PrintFont.Height, new StringFormat() );
    //        graphics.DrawString( _Agility.ToString(), engine.PrintFont, engine.PrintBrush, HorizontalTextSpacing2[8], yPos + ( buffer * 3 ) + pen.Width + ( float )engine.PrintFont.Height, new StringFormat() ); ;
    //        graphics.DrawString( _Willpower.ToString(), engine.PrintFont, engine.PrintBrush, HorizontalTextSpacing2[9], yPos + ( buffer * 3 ) + pen.Width + ( float )engine.PrintFont.Height, new StringFormat() );
    //        graphics.DrawString( _HorrorFactor.ToString(), engine.PrintFont, engine.PrintBrush, HorizontalTextSpacing2[10], yPos + ( buffer * 3 ) + pen.Width + ( float )engine.PrintFont.Height, new StringFormat() );
    //    }

    //    private List<float> getTextStartPoints( float HeaderWidth, float ValueWidth, float offset ) {
    //        List<float> retVal = new List<float>();

    //        float upperStart = 0, lowerStart = 0, midPoint = 0, totalWidth = 0;

    //        if ( HeaderWidth >= ValueWidth ) {
    //            totalWidth = HeaderWidth + ( buffer * 2 );
    //            midPoint = ( totalWidth / 2 ) + buffer;
    //            upperStart = offset + buffer;
    //            lowerStart = ( midPoint + offset ) - ( ValueWidth / 2 )- buffer;
    //        }
    //        else {
    //            totalWidth = ValueWidth;
    //            midPoint = ( totalWidth / 2 ) + ( buffer * 2 );
    //            lowerStart = offset + buffer;
    //            upperStart = ( midPoint + offset ) - ( HeaderWidth / 2 ) - buffer;
    //        }

    //        retVal.Add( upperStart );
    //        retVal.Add( lowerStart );

    //        return retVal;
    //    }

    //}

    

    public static class StringHelper {
        public static StringFormat AlignTC() {
            StringFormat retVal = new StringFormat();

            retVal.Alignment = StringAlignment.Center;

            retVal.LineAlignment = StringAlignment.Center;

            return retVal;
        }
    }
}
