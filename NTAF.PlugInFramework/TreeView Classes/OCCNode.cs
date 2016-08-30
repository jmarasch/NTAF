using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Forms;

namespace NTAF.PlugInFramework {
    public class OCCNode : TreeNode {
        public OCCNode() {
            ImageKey = Properties.Settings.Default.ImageOCCNodeClosed;
            SelectedImageKey = Properties.Settings.Default.ImageOCCNodeOpen;
            }
        OCCBase I_Collector = null;

        public OCCBase Collector {
            get { return I_Collector; }
            set {
                I_Collector = value;
                Text = I_Collector.CollectionName;
                }
            }
        }

    public class OCCNodeItem : TreeViewItem {
        public OCCNodeItem() {

            }
        OCCBase I_Collector = null;

        public OCCBase Collector {
            get { return I_Collector; }
            set {
                I_Collector = value;
                Header = I_Collector.CollectionName;
                }
            }
        }
    }