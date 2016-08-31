using System;
using Gtk;

namespace NTAF.PlugInFramework {
    public class OCCNode : TreeNode {

        public OCCNode( String collectionName, Type containingType ) {
            CollectionName = collectionName;
            ContainingType = containingType;
        }

        public Menu PopupMenu { get; set; }

        [TreeNodeValue( Column = 0 )]
        public String CollectionName { get; private set; }

        public Type ContainingType { get; private set; } 
    }
}
