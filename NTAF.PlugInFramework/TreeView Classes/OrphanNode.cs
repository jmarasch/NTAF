using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NTAF.PlugInFramework {
    public class OrphanNode : TreeNode {
        ObjectClassBase i_ObjectClass = null;

        public OrphanNode(ObjectClassBase Item)
            : base() {

            base.ForeColor = System.Drawing.Color.Red;

            ObjectClass = Item;
        }

        public ObjectClassBase ObjectClass {
            get { return i_ObjectClass; }
            set {
                if ( i_ObjectClass != null )
                    i_ObjectClass.EventNameChanged -= i_ObjectClass_EventNameChanged;

                i_ObjectClass = value;
                i_ObjectClass.EventNameChanged += new NTAF.Core.NTEventHandler<NTAF.Core.NameChangeArgs>( i_ObjectClass_EventNameChanged );

                this.Text = i_ObjectClass.Name;
            }
        }

        void i_ObjectClass_EventNameChanged( NTAF.Core.NameChangeArgs args ) {
            this.Text = i_ObjectClass.Name;
        }
    }
}