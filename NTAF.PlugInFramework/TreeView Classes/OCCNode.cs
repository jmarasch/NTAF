using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Forms;

namespace NTAF.PlugInFramework {
    /// <summary>
    /// Object Class Collector Node
    /// </summary>
    public class OCCNode : NTTreeNode {
        /// <summary>
        /// todo
        /// </summary>
        public OCCNode() {
            //ImageKey = Properties.Settings.Default.ImageOCCNodeClosed;
            //SelectedImageKey = Properties.Settings.Default.ImageOCCNodeOpen;
            }
        OCCBase I_Collector = null;

        /// <summary>
        /// todo
        /// </summary>
        public OCCBase Collector {
            get { return I_Collector; }
            set {
                I_Collector = value;
                Text = I_Collector.CollectionName;
                }
            }
        }
    }