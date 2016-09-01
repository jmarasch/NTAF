using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
//using System.Windows.Forms;
using System.Threading.Tasks;

namespace NTAF.PlugInFramework {
    /// <summary>
    /// todo
    /// </summary>
    public class NTDataNode : NTTreeNode {
        /// <summary>
        /// todo
        /// </summary>
        /// <param name="FileName"></param>
        public NTDataNode(string FileName) : base() { Header = FileName; }
        /// <summary>
        /// todo
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="imageKey"></param>
        public NTDataNode(string FileName, string imageKey) {
            Text = FileName;
            
            //MyIcon = Properties.Settings.Default.ImageDataNodeUnlocked;

        }
    }
}
