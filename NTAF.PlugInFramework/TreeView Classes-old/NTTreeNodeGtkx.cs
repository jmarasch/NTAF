using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gtk;
using HollyLibrary;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace NTAF.PlugInFramework {
    public class NTTreeNodeGtk : HTreeNode {
        private TreeStore children = new TreeStore( typeof( NTTreeNodeGtk ) );

        public NTTreeNodeGtk( ObjectClassBase myObject )
            : base() {
            MyObject = myObject;
            base.Text = MyObject.Name;
        }

        public NTTreeNodeGtk( String name, Type objectType )
            : base() {
            MyObject = ( ObjectClassBase )Activator.CreateInstance( objectType );

            MyObject.Name = name;
            base.Text = MyObject.Name;
        }

        public TreeStore Children {
            get { return children; }
            set { children = value; }
        }

        public Menu PopupMenu { get; set; }

        public ObjectClassBase MyObject { get; set; }

        public String Name { get { return MyObject.Name; } }

        public Type ContainingType { get { return MyObject.MyType(); } }

        public override string ToString() {
            return Name;
        }

        public override void OnDrawItem( DrawItemEventArgs args ) {
            //base.OnDrawItem( args );
            String text      = Name;
            //take font from style
            //Font font        = new Font( Style.FontDesc.Family, Style.FontDesc.Size / 1000, FontStyle.Bold );
            System.Drawing.Font font = new Font( "Tahoma", 12, FontStyle.Bold );

            // take color from style
            Color c          = Color.Blue;
            if ( args.ItemIndex % 2 == 0 ) c = Color.Red;

            Brush b          = new SolidBrush( c );
            //set quality to HighSpeed
            args.Graphics.CompositingQuality = CompositingQuality.HighSpeed;
            args.Graphics.DrawString( text, font, b, args.CellArea.X, args.CellArea.Y );
            args.Graphics.Dispose();
        }
    }
}
