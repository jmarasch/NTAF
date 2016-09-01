using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NTAF.PlugInFramework {
    /// <summary>
    /// Object Class Node
    /// </summary>
    public class OCNode : NTDataTreeNode {
        ObjectClassBase i_ObjectClass = null;

        /// <summary>
        /// todo
        /// </summary>
        public OCNode() {
            //this.MyIcon = Properties.Settings.Default.ImageOCNode;
        }

        /// <summary>
        /// todo
        /// </summary>
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

        /// <summary>
        /// todo
        /// </summary>
        /// <param name="args"></param>
        void i_ObjectClass_EventNameChanged( NTAF.Core.NameChangeArgs args ) {
            Text = i_ObjectClass.Name;
        }
    }
}
