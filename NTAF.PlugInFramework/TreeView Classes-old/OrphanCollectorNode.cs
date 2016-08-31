using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using NTAF.Core;

namespace NTAF.PlugInFramework {



    //todo orphaning place holder may need code, check OCCNode for possible requirements
    public class OrphanCollectorNode : TreeNode {

        ContextMenuStrip
               i_OrphanedRootMenu,
               i_OrphanMenu;


        List<OCCBase>
            i_ObjectCollector = new List<OCCBase>();

        OrphanNode
            i_Branch = new OrphanNode();


        /// <summary>
        /// triggered when the tree goes in to update mode
        /// </summary>
        public event NTEventHandler<UpdaterEventArgs> Updating;

        /// <summary>
        /// triggered when a step in the update process is completed
        /// </summary>
        public event NTEventHandler<UpdateProgressEventArgs> Update;

        /// <summary>
        /// triggered when update completes
        /// </summary>
        public event NTEventHandler Updated;

        public OrphanCollectorNode(OrphanNode[] nodes) : base("Orphaned Objects") {
            this.Nodes.AddRange(nodes);
            this.ImageKey = Properties.Settings.Default.ImageOrphanCollectorNode;
        }

        /// <summary>
        /// Use this to set the optional context menues for when a node is right clicked
        /// </summary>
        /// <param name="OrphanRootMenu"></param>
        /// <param name="OrphanMenu"></param>
        public void SetMenus(ContextMenuStrip OrphanRootMenu, ContextMenuStrip OrphanMenu ) {
            i_OrphanedRootMenu = OrphanRootMenu;
            i_OrphanMenu = OrphanMenu;
        }

        /// <summary>
        /// Attaches a collector to this object so it can create the branch and leaves
        /// </summary>
        /// <param name="collector">Collector object to reference</param>
        public void AttachOCC(OCCBase collector) {
            collector.CollectionUpdated +=
                new NTEventHandler<ItemChangedArgs>(i_ObjectCollector_CollectionUpdated);

            i_ObjectCollector.Add(collector);

        }

        /// <summary>
        /// event that occurs when the collection has been changed
        /// </summary>
        /// <param name="args">ItemChangedArgs defines what happend</param>
        void i_ObjectCollector_CollectionUpdated(ItemChangedArgs args) {
            if (args.Action == ArgAction.Add) {
                OrphanNode
                    newNode = new OrphanNode();

                Boolean added = false;

                //if it knows what what menus to asign do it here
                if (i_OrphanMenu != null)
                    newNode.ContextMenuStrip = i_OrphanMenu;

                //add the object to the node for back checking later
                newNode.ObjectClass = (ObjectClassBase)args.Item;

                if (i_ObjectCollector.Count >= 2) {
                    foreach (OCCNode TN in i_Branch.Nodes)
                        if (TN.Collector.CollectionType == newNode.ObjectClass.CollectionType)
                            if (TN.Nodes.Count >= 1) {
                                for (int x = 0; x <= TN.Nodes.Count - 1; x++) {
                                    if (newNode.ObjectClass.Name.CompareTo(((OCNode)TN.Nodes[x]).ObjectClass.Name) <= 0) {
                                        TN.Nodes.Insert(x, newNode);
                                        added = true;
                                        break;
                                    }
                                }
                                if (!added)
                                    TN.Nodes.Add(newNode);
                            }
                            else
                                TN.Nodes.Add(newNode);
                    //TN.Nodes.Add( newNode );
                }
                else {
                    //finally add the sub node to the main node
                    if (i_Branch.Nodes.Count >= 1) {
                        for (int x = 0; x <= i_Branch.Nodes.Count - 1; x++) {
                            if (newNode.ObjectClass.Name.CompareTo(((OCNode)i_Branch.Nodes[x]).ObjectClass.Name) <= 0) {
                                i_Branch.Nodes.Insert(x, newNode);
                                added = true;
                                break;
                            }
                        }
                        if (!added)
                            i_Branch.Nodes.Add(newNode);
                    }
                    else
                        i_Branch.Nodes.Add(newNode);
                }

            }
            if (args.Action == ArgAction.Remove) {
                OCNode
                    remove = null;

                if (i_ObjectCollector.Count >= 2) {
                    foreach (TreeNode TN in i_Branch.Nodes)
                        foreach (TreeNode tn in TN.Nodes)
                            if (tn is OCNode)
                                if (((OCNode)tn).ObjectClass == args.Item)
                                    remove = (OCNode)tn;
                }
                else {
                    foreach (TreeNode tn in i_Branch.Nodes)
                        if (tn is OCNode)
                            if (((OCNode)tn).ObjectClass == args.Item)
                                remove = (OCNode)tn;
                }



                if (remove != null) {
                    i_Branch.Nodes.Remove(remove);
                }
                else
                    throw new Exception("Could not find the leaf in the branch " + i_Branch.Text +
                                         " for the object being removed " +
                                         (args.Item is INTName ? ((INTName)args.Item).Name :
                                         args.Item.ToString()));
            }
            if (args.Action == ArgAction.Edit) {

                //if ( i_ObjectCollector.Count >= 2 ) {
                //    foreach ( OCNode TN in i_Branch.Nodes )
                //        foreach ( OCNode tn in TN.Nodes )
                //            if ( tn is OCNode )
                //                if ( ( ( INTId )( ( OCNode )tn ).ObjectClass ).ID == ( ( INTId )args.Item ).ID ) {
                //                    ( ( OCNode )tn ).ObjectClass = args.Item;
                //                }
                //}
                //else {
                //    foreach ( TreeNode tn in i_Branch.Nodes )
                //        if ( tn is OCNode )
                //            if ( ( ( INTId )( ( OCNode )tn ).ObjectClass ).ID == ( ( INTId )args.Item ).ID ) {
                //                ( ( OCNode )tn ).ObjectClass = args.Item;
                //            }
                //}
            }

        }
        public String TreeName {
            get {
                Object[]
                atts = this.GetType().GetCustomAttributes(typeof(TreeNodePlugIn), true);

                if (atts.Length >= 1)
                    return ((TreeNodePlugIn)atts[0]).DisplayName;

                return "Branch name is unable to be set";
            }
        }
    }
}
