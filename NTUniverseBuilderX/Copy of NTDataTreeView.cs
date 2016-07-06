using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using NTAF.Core;
using NTAF.PrintEngine;
using NTAF.Core;
using NTAF.Core;
using NTAF.UniverseBuilder.WinGui.MessageBoxes;
using NTAF.PlugInFramework;
using System.Drawing;

namespace NTAF.UniverseBuilder.WinGui {
    using PrintEngine = NTAF.PrintEngine.PrintEngine;
    using System.Threading;
    public partial class NTDataTreeView : UserControl {
        private NTDataFile dataFile = null;

        public event NTEventHandler EventNodeMouseDoubleClick;
        public event EventHandler EventMakeNew;
        public event EventHandler EventEditExisting;
        public event EventHandler EventPreview;

        ProgressBox
            progrssBox = new ProgressBox( 0, "" );//

        public object SelectedNode {
            get {
                return Data.SelectedNode;
            }
        }

        public NTDataFile DataFile {
            get {
                return dataFile;
            }
        }

        private List<OCTreeNodeBase>
            i_TreeNodePlugins = new List<OCTreeNodeBase>( PluginEngine.GetTreePlugIns() );

        public NTDataTreeView( NTDataFile File ) {
            InitializeComponent();

            dataFile = File;

            LoadTreeNodes( );

            NTAF.UniverseBuilder.WinGui.Properties.Settings.Default.PropertyChanged += new PropertyChangedEventHandler( SettingChanged );

            //check through settings and load corectly

            SettingChanged( this, new PropertyChangedEventArgs( "" ) );

        }

        void NTDataTreeView_MouseClick( object sender, MouseEventArgs e ) {
            if ( e.Button == MouseButtons.Right )
                Data.SelectedNode = Data.GetNodeAt( e.X, e.Y );
        }

        void SettingChanged( object sender, PropertyChangedEventArgs e ) {
            ObjectViewer.Visible = Properties.Settings.Default.AdvancedEdit;
            AboutNode.Visible = !( Properties.Settings.Default.AdvancedEdit );
        }

        private TreeNode
            OrphanBranch = new TreeNode( "Orphaned Nodes" );

        public void UpdateOrphans( TreeNode[] orphans ) {
            OrphanBranch.Nodes.Clear();
            OrphanBranch.Nodes.AddRange( orphans );
            if ( !Data.Nodes.Contains( OrphanBranch ) )
                Data.Nodes.Add( OrphanBranch );
        }

        internal void Data_AfterSelect( object sender, TreeViewEventArgs e ) {
            if ( Data.SelectedNode is NTTreeNode ) {
                if ( ( ( NTTreeNode )Data.SelectedNode ).NodeValue is IPrintable ) {
                    AboutNode.Text = "";
                    AboutNode_Paint( this, new PaintEventArgs( AboutNode.CreateGraphics(), new Rectangle( AboutNode.Location, AboutNode.Size ) ) );
                }
                else {
                    if ( ( ( NTTreeNode )Data.SelectedNode ).NodeValue is IAboutMe )
                        AboutNode.Text = ( ( IAboutMe )( ( NTTreeNode )Data.SelectedNode ).NodeValue ).aboutMe;
                }
                if ( ( ( NTTreeNode )Data.SelectedNode ).NodeValue is INTId )
                    ObjectViewer.SelectedObject = ( ( NTTreeNode )Data.SelectedNode ).NodeValue;
            }
            else {
                AboutNode.Text = "";
                ObjectViewer.SelectedObject = null;
            }
        }

        private void LoadTreeNodes( ) {
            foreach ( OCTreeNodeBase treeNodePlug in i_TreeNodePlugins ) {
                foreach ( OCCBase occ in DataFile.Collectors ) {
                    if ( treeNodePlug.CanDisplay( occ.CollectionType ) )
                        treeNodePlug.AttachOCC( occ );
                }
                treeNodePlug.Updating += new NTEventHandler<UpdaterEventArgs>( treePlugIn_BranchUpdating );
                treeNodePlug.Updated += new NTEventHandler( treePlugIn_BranchUpdated );
                treeNodePlug.Update += new NTEventHandler<UpdateProgressEventArgs>( treePlugIn_BranchUpdate );
                treeNodePlug.SetMenus( RootEditingMenu, NTObjectEditingMenu );

                Data.Nodes.Add( treeNodePlug.MainBranch() );
                //treeNodePlug.GrowBranch();
            }
            //foreach ( OCCBase occ in DataFile.Collectors ) {
            //    foreach ( OCTreeNodeBase treePlugIn in i_TreeNodePlugins ) {
            //        if ( treePlugIn.CollectionType == occ.CollectionType ) {
            //            treePlugIn.SetMenus( RootEditingMenu, NTObjectEditingMenu );
            //            treePlugIn.AttachToOCC( occ );
            //            treePlugIn.Updating += new NTEventHandler<UpdaterEventArgs>( treePlugIn_BranchUpdating );
            //            treePlugIn.Updated += new NTEventHandler( treePlugIn_BranchUpdated );
            //            treePlugIn.Update += new NTEventHandler<UpdateProgressEventArgs>( treePlugIn_BranchUpdate );
            //            Data.Nodes.Add( treePlugIn.MainBranch() );
            //            break;
            //        }
            //    }
            //}
        }

        void treePlugIn_BranchUpdate( UpdateProgressEventArgs args ) {
            progrssBox.UpdateBox( args );
        }

        void treePlugIn_BranchUpdated() {
            progrssBox.closeMe();
        }

        void treePlugIn_BranchUpdating( UpdaterEventArgs args ) {
            progrssBox = new ProgressBox( args.NumberOfItems, "Updating Branch" );
            progrssBox.Show();
        }

        private void LoadTreeNodes( object[] nodesToAdd ) {
            throw new NotImplementedException();
            //foreach ( Object obj in nodesToAdd ) {
            //    NTTreeNode newNode = new NTTreeNode();

            //    if ( obj is IOwner )
            //        if ( ( ( IOwner )obj ).myOwner == null ) {//is an orphan
            //            newNode.ForeColor = System.Drawing.Color.Red;
            //            newNode.NodeFont = new Font( this.Font, FontStyle.Bold );
            //        }

            //    newNode.NodeValue = obj;

            //    if ( obj is INTName ) newNode.ContextMenuStrip = NTObjectEditingMenu;
            //    if ( obj is BaseUnit ) BaseUnitsCollection.Add( newNode );
            //    if ( obj is Race ) RacesCollection.Add( newNode );
            //    if ( obj is Archetype ) ArchetypesCollection.Add( newNode );
            //    if ( obj is Weapon ) WeaponsCollection.Add( newNode );
            //    if ( obj is Skill ) SkillsCollection.Add( newNode );
            //    if ( obj is Psy ) PsysCollection.Add( newNode );
            //    if ( obj is Item ) ItemsCollection.Add( newNode );
            //    if ( ( obj is IPermission ) && !( obj is Item ) ) PermissionsCollection.Add( newNode );
            //}
        }

        private void Data_NodeMouseDoubleClick( object sender, TreeNodeMouseClickEventArgs e ) {
            if ( EventNodeMouseDoubleClick != null )
                EventNodeMouseDoubleClick();
        }

        private void Data_KeyUp( object sender, KeyEventArgs e ) {
            if ( e.KeyCode == Keys.Delete )
                DeleteKeyPressed();

            if ( e.KeyCode == Keys.N && ( e.Alt && e.Control ) )
                if ( EventMakeNew != null )
                    EventMakeNew( null, null );

            if ( e.KeyCode == Keys.P && ( e.Alt && e.Control ) )
                if ( EventPreview != null )
                    EventPreview( null, null );

            if ( e.KeyCode == Keys.E && ( e.Alt && e.Control ) )
                if ( EventEditExisting != null )
                    EventEditExisting( null, null );

        }//end key up

        private void DeleteKeyPressed() {

            if ( Data.SelectedNode.ForeColor != System.Drawing.Color.Red ) {
                String EraseType = Data.SelectedNode.Text;
                if ( Data.SelectedNode.Tag != null ) {
                    switch ( MessageBox.Show( String.Format( "This will delete all items under {0}\n Do you wish to continue?",
                                                                EraseType ), "Deleting all items", MessageBoxButtons.OKCancel ) ) {
                        case DialogResult.OK:
                            List<INTId> objsToRemove = new List<INTId>();
                            foreach ( NTTreeNode tn in Data.SelectedNode.Nodes )
                                objsToRemove.Add( ( INTId )tn.NodeValue );
                            foreach ( INTId obj in objsToRemove )
                                dataFile.Drop( obj );
                            break;
                    }
                }//end check if master item
                else {
                    switch ( MessageBox.Show( String.Format( "Delete {0}, from {1}?", Data.SelectedNode.Text,
                                                                Data.SelectedNode.Parent.Text ), "Deleting all items", MessageBoxButtons.OKCancel ) ) {
                        case DialogResult.OK:
                            dataFile.Drop( ( INTId )( ( NTTreeNode )Data.SelectedNode ).NodeValue );
                            break;
                    }
                }
            }
            else {
                MessageBox.Show( String.Format( "The selected node {0} cannot be removed. \nItems with a bold red text are Orphined items and not valid to be reused.\nTo remove this item remove all references to it. To make this item useable right click it and select 'import'.", Data.SelectedNode.Text ) );
            }
        }

        private void AboutNode_Paint( object sender, PaintEventArgs e ) {
            e.Graphics.Clear( SystemColors.Control );
            if ( Data.SelectedNode != null && Data.SelectedNode is NTTreeNode ) {
                if ( ( ( NTTreeNode )Data.SelectedNode ).NodeValue is IPrintable ) {
                    PrintElement element = new PrintElement( ( IPrintable )( ( NTTreeNode )Data.SelectedNode ).NodeValue );
                    ( ( IPrintable )( ( NTTreeNode )Data.SelectedNode ).NodeValue ).Print( element );

                    PrintEngine X = new PrintEngine( "" );
                    X.PageWidth = AboutNode.Width;

                    //this.Size = new Size( PrintPrev.Width, PrintPrev.Height );
                    Rectangle windSize = new Rectangle( new Point( 0, 0 ), AboutNode.Size );
                    X.PageBounds = windSize;
                    element.Draw( X, 0, e.Graphics, windSize );
                }
            }
        }

        private void NTObjectEditingMenu_Opening( object sender, CancelEventArgs e ) {
            importToolStripMenuItem.Enabled = ( Data.SelectedNode.ForeColor == System.Drawing.Color.Red );
            removeToolStripMenuItem.Enabled = !( Data.SelectedNode.ForeColor == System.Drawing.Color.Red );
        }

        private void importToolStripMenuItem_Click( object sender, EventArgs e ) {
            INTId
                ObjToMove=NTData.FindINTIdObjectByID( DataFile.Orphans, ( ( INTId )( ( NTTreeNode )Data.SelectedNode ).NodeValue ).ID );

            Data.SelectedNode.Remove();

            if ( ObjToMove is IOwner )
                ( ( IOwner )ObjToMove ).myOwner = DataFile;

            DataFile.DropOrphan( ObjToMove );
            DataFile.Add( ObjToMove );

            ObjToMove.ID = DataFile.GenerateIDCode();
        }
    }
}
