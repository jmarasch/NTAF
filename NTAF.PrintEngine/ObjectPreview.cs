using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NTAF.PrintEngine {
    public partial class ObjectPreview : Form {
        object 
            i_objToPreview = null;

        public ObjectPreview( Object objToPreview ) {
            if ( !( objToPreview is IPrintable ) )
                throw new Exception( "Object is not valid for preview" );

            i_objToPreview = objToPreview;

            InitializeComponent();
        }

        private void ObjectPreview_Paint( object sender, PaintEventArgs e ) {
            e.Graphics.Clear( Color.White );
            PrintElement element = new PrintElement(( IPrintable )i_objToPreview);

            ( ( IPrintable )i_objToPreview ).Print( element );
            //element.Draw
            //drawObject.Print( element );
            PrintEngine X = new PrintEngine( "" );
            X.pageBounds.Width = this.Size.Width;
            int fontheight = (int) Math.Round( e.Graphics.MeasureString("X", X.PrintFont).Height, 0, MidpointRounding.AwayFromZero);
            Int32 formHeight = ( Int32 )( ( float )Math.Round( element.CalculateHeight( X, e.Graphics ), 0, MidpointRounding.AwayFromZero ) ) + fontheight;

            this.Size = new Size( this.Size.Width, formHeight );
            Rectangle windSize = new Rectangle( new Point( 0, 0 ), this.Size );
            X.pageBounds = windSize;
            element.Draw( X, 0, e.Graphics, windSize );
        }

        private void ObjectPreview_ResizeBegin( object sender, EventArgs e ) {
            this.SuspendLayout();
        }

        private void ObjectPreview_ResizeEnd( object sender, EventArgs e ) {
            this.ResumeLayout();
        }

        private void ObjectPreview_KeyUp( object sender, KeyEventArgs e ) {
            if ( e.KeyCode == Keys.Escape )
                this.Close();
        }
    }
}