using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace NTAF.PlugInFramework {
    public class DataNode : TreeNode {
        public DataNode(string FileName) : base(FileName) {}
        public DataNode(string FileName, string imageKey) {
            base.Text = FileName;
            
            this.ImageKey = Properties.Settings.Default.ImageDataNodeUnlocked;

        }
            //this.ImageKey = Properties.Resources.database_icon;

//            mFileNode.ImageKey = CacheShellIcon(mFile.FullName)
//mFileNode.SelectedImageKey = mFileNode.ImageKey & "-open"
        
    }
}
