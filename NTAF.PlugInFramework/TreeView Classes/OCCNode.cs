using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NTAF.PlugInFramework {
    public class OCCNode : TreeNode {
        
        OCCBase I_Collector = null;

        public OCCBase Collector {
            get { return I_Collector; }
            set {
                I_Collector = value;
                this.Text = I_Collector.CollectionName;
            }
        }
    }
}
