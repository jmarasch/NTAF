using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace NTAF.PlugInFramework {
    public class DataNode : TreeNode {
        public DataNode(string FileName) : base(FileName) { }
    }
}
