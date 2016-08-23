using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NTAF.Core;
using System.Drawing;

namespace NTAF.PlugInFramework {
    /// <summary>
    /// The Object Class Tree Node Base provides the ability to create a TreeVew
    /// plug-in class for the NTTreView Control.
    /// 
    /// The only required override is CollectionType
    /// 
    /// This class will require an appropriate OCCBase class to read and write to
    /// </summary>
    public class OCTreeNodeBase {

        ContextMenuStrip
            i_RootMenu,
            i_NodeMenu;

        List<OCCBase>
            i_ObjectCollector = new List<OCCBase>();

        TreeNode
            i_Branch = new OCCNode();

        #region events

        /// <summary>
        /// triggered when the tree goes in to update mode
        /// </summary>
        public event NTEventHandler<UpdaterEventArgs>  Updating;

        /// <summary>
        /// triggered when a step in the update process is compleeted
        /// </summary>
        public event NTEventHandler<UpdateProgressEventArgs>  Update;

        /// <summary>
        /// trigered when update compleetes
        /// </summary>
        public event NTEventHandler  Updated;

        #endregion

        /// <summary>
        /// Use this to set the optional context menues for when a node is right clicked
        /// </summary>
        /// <param name="RootMenu"></param>
        /// <param name="NodeMenu"></param>
        public void SetMenus( ContextMenuStrip RootMenu, ContextMenuStrip NodeMenu) {//,ContextMenuStrip OrphanRootMenu, ContextMenuStrip OrphanMenu ) {
            i_RootMenu = RootMenu;
            i_NodeMenu = NodeMenu;
            //i_OrphanedRootMenu = OrphanRootMenu;
            //i_OrphanMenu = OrphanMenu;
        }

        /// <summary>
        /// Creates and returns the collection node "branch" and all of its ObectClass nodes "leaflings" from the objects in the collector.
        /// </summary>
        /// <returns>A root node for the NTTreeView control</returns>
        /// <exception cref="Exception">Thrown when the ObjectClassCollector has not been set</exception>
        public TreeNode MainBranch() {

            if ( i_ObjectCollector.Count <= 0 )
                throw new Exception( "ObjectCalssCollector(s) has(have) not been set" );
            //todo add a check to verify if it has multiple occs to verrify their internal counts and grow as neccisarry

            if ( i_Branch.Nodes.Count <= 0 )
                GrowBranch();

            //checks to see if it has sub nodes if its not a collection of subnodes it will add the collectors opperation menus
            if ( i_ObjectCollector.Count == 1 && i_RootMenu != null )
                i_Branch.ContextMenuStrip = i_RootMenu;
            else
                i_Branch.ContextMenuStrip = null;

            return i_Branch;
        }

        ///// <summary>
        ///// Gets all the leaves for this treenode branch object
        ///// </summary>
        //public NTTreeNode[] Leaves {
        //    get {
        //        if ( i_Branch.Nodes.Count <= 0 || !VerrifyLeaves( i_Branch.Nodes ) )
        //            GrowBranch();

        //        List<NTTreeNode>
        //            retVal = new List<NTTreeNode>();

        //        foreach ( TreeNode tn in i_Branch.Nodes ) {
        //            retVal.Add( TreeNodeUpsacler( tn ) );
        //        }

        //        return retVal.ToArray();
        //    }
        //}

        ///// <summary>
        ///// verrifies all leaves in the branch
        ///// </summary>
        ///// <param name="treeNodeCollection">Collection to check</param>
        ///// <returns>false if a descrepency is found</returns>
        //private bool VerrifyLeaves( TreeNodeCollection treeNodeCollection ) {
        //    Boolean
        //        retVal = true;

        //    if ( i_Branch.Nodes.Count >= 0 ) {
        //        if ( retVal )
        //            retVal = i_Branch.Nodes.Count == i_ObjectCollector.Count;
        //        if ( retVal ) {
        //            List<Object>
        //                objs = new List<object>( i_ObjectCollector.Objects );

        //            foreach ( TreeNode tn in i_Branch.Nodes ) {
        //                if ( tn is NTTreeNode && retVal )
        //                    retVal = objs.Contains( ( ( NTTreeNode )tn ).NodeValue );
        //            }
        //        }
        //    }
        //    return retVal;
        //}

        ///// <summary>
        ///// transmutes a standard tree node to a NTTreeNode minus the containing object
        ///// </summary>
        ///// <param name="TN">Treenode to upscale</param>
        ///// <returns>Upscaled NTTreeNode</returns>
        //private NTTreeNode TreeNodeUpsacler( TreeNode TN ) {
        //    if ( TN is NTTreeNode )
        //        return ( NTTreeNode )TN;
        //    else
        //        return new NTTreeNode( TN.Text );
        //}

        /// <summary>
        /// Attaches a collector to this object so it can create the branch and leaves
        /// </summary>
        /// <param name="collector">Collector object to reference</param>
        public void AttachOCC( OCCBase collector ) {
            if ( !CanDisplay(collector.CollectionType ) )
                throw new ArgumentException( "Collector is not of the proper type and cannot be attached to this branch" );

            //if ( i_ObjectCollector != null ) {
            //    i_ObjectCollector.CollectionUpdated -= i_ObjectCollector_CollectionUpdated;
            //    i_ObjectCollector = null;
            //}

            collector.CollectionUpdated +=
                new NTEventHandler<ItemChangedArgs>( i_ObjectCollector_CollectionUpdated );

            i_ObjectCollector.Add( collector );

            //i_Branch = new TreeNode( i_ObjectCollector.CollectionName );

            //i_ObjectCollector.CollectionUpdated +=
            //    new NTEventHandler<ItemChangedArgs>( i_ObjectCollector_CollectionUpdated );
        }

        /// <summary>
        /// Internally creates a main "branch" of sub nodes "Leafs" or sub collectors to return to the tree
        /// </summary>
        public virtual void GrowBranch() {
            //clean out anything that the main branch may have in it 
            i_Branch.Nodes.Clear();

            //todo if this is null try to find a suitable collector if at all possible ill have to check code to find out


            if ( i_ObjectCollector.Count == 0 ) {
                i_Branch = new TreeNode( "Collector Error: no collector has been assigned to node" );
                //throw new NullReferenceException( "An object collector has not been assigned to the branch" );
                return;
            }

            if ( Updating != null )
                Updating( new UpdaterEventArgs( i_ObjectCollector.Count ) );

            int
                currentCount = i_ObjectCollector.Count;
            //if the count is more than 1 creates a sub tree structure
            if ( i_ObjectCollector.Count >= 2 ) {
                i_Branch = new TreeNode();
                foreach ( OCCBase occ in i_ObjectCollector ) {
                    OCCNode
                        SubRoot = new OCCNode(  );

                    SubRoot.Collector = occ;

                    SubRoot.Nodes.AddRange( PopulateNode( occ, currentCount, out currentCount ) );

                    if ( i_RootMenu != null )
                        SubRoot.ContextMenuStrip = i_RootMenu;

                    i_Branch.Nodes.Add( SubRoot );
                }
            }
            else { //not more than 1 collector get all sub nodes
                i_Branch = new OCCNode();
                i_Branch.Nodes.AddRange( PopulateNode( i_ObjectCollector[0], currentCount, out currentCount ) );
                ((OCCNode)i_Branch).Collector = i_ObjectCollector[0];
            }

            i_Branch.Text = TreeName;
            //todo add image for collection branch here
            if ( Updated != null )
                Updated();
        }

        /// <summary>
        /// populates collector node with object-class items
        /// </summary>
        /// <param name="occ">Collector with the objects</param>
        /// <param name="InCount"></param>
        /// <param name="OutCount"></param>
        /// <returns></returns>
        private TreeNode[] PopulateNode( OCCBase occ, int InCount, out int OutCount ) {
            List<TreeNode>
                retVal = new List<TreeNode>();

            foreach ( ObjectClassBase obj in occ.Objects ) {
                //NTTreeNode
                //        newNode = new NTTreeNode();

                OCNode
                    newNode = new OCNode();
                
                newNode.NodeFont = SystemFonts.DefaultFont;
                newNode.ForeColor = this.ColorFontLeaf;
                newNode.BackColor = this.ColorBackgroundLeaf;
                //todo add image for object here

                //if it knows what menus to assign do it here
                if ( i_NodeMenu != null )
                    //if ( obj is INTName ) 
                    newNode.ContextMenuStrip = i_NodeMenu;

                //add the object to the node for back checking later
                newNode.ObjectClass = obj;

                //finally add the sub node to the main node
                retVal.Add( newNode );

                if ( Update != null )
                    Update( new UpdateProgressEventArgs( "Updating Leaves In " + occ.CollectionName, "Updated",
                        newNode.Text, InCount++, i_ObjectCollector.Count ) );
            }
            OutCount = InCount;
            return retVal.ToArray();
        }


        /// <summary>
        /// event that occurs when the collection has been changed
        /// </summary>
        /// <param name="args">ItemChangedArgs defines what happend</param>
        void i_ObjectCollector_CollectionUpdated( ItemChangedArgs args ) {
            if ( args.Action == ArgAction.Add ) {
                OCNode
                    newNode = new OCNode();

                Boolean added = false;

                //if it knows what menus to assign do it here
                if ( i_NodeMenu != null )
                    newNode.ContextMenuStrip = i_NodeMenu;

                newNode.NodeFont = SystemFonts.DefaultFont;
                newNode.ForeColor = this.ColorFontLeaf;
                newNode.BackColor = this.ColorBackgroundLeaf;

                //add the object to the node for back checking later
                newNode.ObjectClass = (ObjectClassBase)args.Item;

                if ( i_ObjectCollector.Count >= 2 ) {
                    foreach ( OCCNode TN in i_Branch.Nodes )
                        if ( TN.Collector.CollectionType == newNode.ObjectClass.CollectionType )
                            if ( TN.Nodes.Count >= 1 ) {
                                for ( int x = 0; x <= TN.Nodes.Count - 1; x++ ) {
                                    if ( newNode.ObjectClass.Name.CompareTo( ( ( OCNode )TN.Nodes[x] ).ObjectClass.Name ) <= 0 ) {
                                        TN.Nodes.Insert( x, newNode );
                                        added = true;
                                        break;
                                    }
                                }
                                if ( !added )
                                    TN.Nodes.Add( newNode );
                            }
                            else
                                TN.Nodes.Add( newNode );
                            //TN.Nodes.Add( newNode );
                }
                else {
                    //finally add the sub node to the main node
                    if ( i_Branch.Nodes.Count >= 1 ) {
                        for ( int x = 0; x <= i_Branch.Nodes.Count - 1; x++ ) {
                            if ( newNode.ObjectClass.Name.CompareTo( ( ( OCNode )i_Branch.Nodes[x] ).ObjectClass.Name ) <= 0 ) {
                                i_Branch.Nodes.Insert( x, newNode );
                                added = true;
                                break;
                            }
                        }
                        if ( !added )
                            i_Branch.Nodes.Add( newNode );
                    }
                    else
                        i_Branch.Nodes.Add( newNode );
                }

            }
            if ( args.Action == ArgAction.Remove ) {
                OCNode
                    remove = null;

                if ( i_ObjectCollector.Count >= 2 ) {
                    foreach ( TreeNode TN in i_Branch.Nodes )
                        foreach ( TreeNode tn in TN.Nodes )
                            if ( tn is OCNode )
                                if ( ( ( OCNode )tn ).ObjectClass == args.Item )
                                    remove = ( OCNode )tn;
                }
                else {
                    foreach ( TreeNode tn in i_Branch.Nodes )
                        if ( tn is OCNode )
                            if ( ( ( OCNode )tn ).ObjectClass == args.Item )
                                remove = ( OCNode )tn;
                }

                

                if ( remove != null ) {
                    i_Branch.Nodes.Remove( remove );
                }
                else
                    throw new Exception( "Could not find the leaf in the branch " + i_Branch.Text +
                                         " for the object being removed " +
                                         ( args.Item is INTName ? ( ( INTName )args.Item ).Name :
                                         args.Item.ToString() ) );
            }
            if ( args.Action == ArgAction.Edit ) {

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

        ///// <summary>
        ///// checks a type to see if it matches the set type
        ///// </summary>
        ///// <param name="T">Type to check</param>
        ///// <returns>true if the types match</returns>
        //public bool IsOfType( Type T ) {
        //    return T == CollectionType;
        //}

        ///// <summary>
        ///// checks a type to see if it matches the set type
        ///// </summary>
        ///// <param name="O">Type to check</param>
        ///// <returns>true if the types match</returns>
        //public bool IsOfType( Object O ) {
        //    return typeof( Object ) == CollectionType;
        //}

        ///// <summary>
        ///// Requires override for the class to work properly
        ///// Returns the collection type that this branch displays
        ///// </summary>
        //public virtual Type CollectionType { get { return typeof( Object ); } }

        public bool CanDisplay( Type thisType ) {
            Object[]
                atts = this.GetType().GetCustomAttributes( typeof( TreeNodePlugIn ), true );

            if ( atts.Length >= 1 )
                foreach ( Type typ in ( ( TreeNodePlugIn )atts[0] ).IDisplay )
                    if ( typ == thisType )
                        return true;

            return false;
        }

        public String TreeName {
            get {
                Object[]
                atts = this.GetType().GetCustomAttributes( typeof( TreeNodePlugIn ), true );

                if ( atts.Length >= 1 )
                    return ( ( TreeNodePlugIn )atts[0] ).DisplayName;

                return "Branch name is unable to be set";
            }
        }

        public Color ColorFontBranch { get; set; }
        public Color ColorFontLeaf { get; set; }
        public Color ColorBackgroundBranch { get; set; }
        public Color ColorBackgroundLeaf { get; set; }
    }
}