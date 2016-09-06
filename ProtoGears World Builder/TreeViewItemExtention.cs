using System.Collections.Generic;
using System.Windows.Controls;

namespace ProtoGears_World_Builder {
    class TreeViewItemExtention : TreeViewItem {

        public NodeTypeEnum NodeType { get; set; }

        public enum NodeTypeEnum {
            DataRoot,
            ObjectCollector,
            Object,
            Other
            }

        public TreeViewItemExtention[] AllMyChildren() {
            List<TreeViewItemExtention> retval = new List<TreeViewItemExtention>();
            foreach (TreeViewItemExtention item in this.Items) {
                if(item.NodeType == NodeTypeEnum.Object) {
                    retval.Add(item);
                    } else {
                    retval.AddRange(item.AllMyChildren());
                    }
                }
            return retval.ToArray();
            }
        }
    }
