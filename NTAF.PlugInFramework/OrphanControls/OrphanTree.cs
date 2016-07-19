using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTAF.PlugInFramework.OrphanControls {
    [TreeNodePlugIn( "Orphan Tree", "Orphans", "0.0.0.0", typeof( ObjectClassBase ) )]
    public class OrphanTree : OCTreeNodeBase {
        public OrphanTree() { this.ColorFontLeaf = System.Drawing.Color.Red; }
    }
}
