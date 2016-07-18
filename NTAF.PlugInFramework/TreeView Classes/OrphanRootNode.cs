using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace NTAF.PlugInFramework {
    //todo orphaning place holder may need code, check OCCNode for possible requirements
    public class OrphanCollectorNode : TreeNode {
        public OrphanCollectorNode(OrphanNode[] nodes) : base("Orphaned Objects") {
            this.Nodes.AddRange(nodes);
            this.ImageKey = Properties.Settings.Default.ImageOrphanCollectorNode;
        }
    }
}
