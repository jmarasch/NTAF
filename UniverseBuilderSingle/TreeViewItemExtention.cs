using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UniverseBuilderSingle {
    class TreeViewItemExtention : TreeViewItem {

        public NodeTypeEnum NodeType { get; set; }

        public enum NodeTypeEnum {
            DataRoot,
            ObjectCollector,
            Object,
            Other
            }
        }
    }
