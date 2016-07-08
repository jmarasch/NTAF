using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NTAF.Core;
using NTAF.PlugInFramework;
using PE = NTAF.PlugInFramework.PluginEngine;
using NTAF.PrintEngine;

namespace UniverseBuilder {

    public partial class Main : Form {
        const Keys PreViewObjectKey = Keys.Control | Keys.Shift | Keys.P;
        const Keys EditObjectKey    = Keys.Control | Keys.Shift | Keys.E;
        const Keys DeleteObjectKey  = Keys.Delete;
        const Keys CopyObjectKey    = Keys.Control | Keys.Shift | Keys.C;
        const Keys NewObjectKey     = Keys.Control | Keys.Shift | Keys.N;
        const Keys ClearObjectKey   = Keys.Control | Keys.Shift | Keys.D;


        ContextMenuStrip OCMenuStrip;
        ToolStripMenuItem previewObjectToolStripMenuItem;
        ToolStripMenuItem editObjectToolStripMenuItem;
        ToolStripMenuItem deleteObjectToolStripMenuItem;
        ToolStripMenuItem copyObjectToolStripMenuItem;

        ContextMenuStrip OCCMenuStrip;
        ToolStripMenuItem newObjectToolStripMenuItem;
        ToolStripMenuItem clearObjectsToolStripMenuItem;

        void BuildMenu() {
            ComponentResourceManager resources = new ComponentResourceManager( typeof( Main ) );

            previewObjectToolStripMenuItem = new ToolStripMenuItem();
            previewObjectToolStripMenuItem.Name = "previewObjectToolStripMenuItem";
            previewObjectToolStripMenuItem.ShortcutKeys = PreViewObjectKey;
            previewObjectToolStripMenuItem.Text = "&Preview Object";
            previewObjectToolStripMenuItem.Click += new EventHandler( previewObjectToolStripMenuItem_Click );

            editObjectToolStripMenuItem = new ToolStripMenuItem();
            editObjectToolStripMenuItem.Name = "editObjectToolStripMenuItem";
            editObjectToolStripMenuItem.ShortcutKeys = EditObjectKey;
            editObjectToolStripMenuItem.Text = "&Edit Object";
            editObjectToolStripMenuItem.Click += new EventHandler( editObjectToolStripMenuItem_Click );

            deleteObjectToolStripMenuItem = new ToolStripMenuItem();
            deleteObjectToolStripMenuItem.Name = "deleteObjectToolStripMenuItem";
            deleteObjectToolStripMenuItem.ShortcutKeys = DeleteObjectKey;
            deleteObjectToolStripMenuItem.Text = "&Delete Object";
            deleteObjectToolStripMenuItem.Click += new EventHandler( deleteObjectToolStripMenuItem_Click );

            copyObjectToolStripMenuItem = new ToolStripMenuItem();
            copyObjectToolStripMenuItem.Image = ( ( System.Drawing.Image )( resources.GetObject( "copyObjectToolStripMenuItem.Image" ) ) );
            copyObjectToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            copyObjectToolStripMenuItem.Name = "copyObjectToolStripMenuItem";
            copyObjectToolStripMenuItem.ShortcutKeys = CopyObjectKey;
            copyObjectToolStripMenuItem.Text = "&Copy";
            copyObjectToolStripMenuItem.Click += new EventHandler( copyObjectToolStripMenuItem_Click );

            newObjectToolStripMenuItem = new ToolStripMenuItem();
            newObjectToolStripMenuItem.Name = "newObjectToolStripMenuItem";
            newObjectToolStripMenuItem.ShortcutKeys = NewObjectKey;
            newObjectToolStripMenuItem.Text = "&New Object";
            newObjectToolStripMenuItem.Click += new EventHandler( newObjectToolStripMenuItem_Click );

            clearObjectsToolStripMenuItem = new ToolStripMenuItem();
            clearObjectsToolStripMenuItem.Name = "clearObjectsToolStripMenuItem";
            clearObjectsToolStripMenuItem.ShortcutKeys = ClearObjectKey;
            clearObjectsToolStripMenuItem.Text = "&Clear All Objects";
            clearObjectsToolStripMenuItem.Click += new EventHandler( clearObjectsToolStripMenuItem_Click );

            OCMenuStrip = new ContextMenuStrip();
            OCMenuStrip.Items.AddRange( new ToolStripItem[]{
                previewObjectToolStripMenuItem,
                editObjectToolStripMenuItem,
                copyObjectToolStripMenuItem,
                deleteObjectToolStripMenuItem} );
            OCMenuStrip.Name = "OCMenuStrip";

            OCCMenuStrip = new ContextMenuStrip();
            OCCMenuStrip.Items.AddRange( new ToolStripItem[]{
                newObjectToolStripMenuItem,
                clearObjectsToolStripMenuItem} );
            OCCMenuStrip.Name = "OCCMenuStrip";


        }

        void editObjectToolStripMenuItem_Click( object sender, EventArgs e ) {
            try {
                OCEditorBase ed = null;
                Type editType =( ( OCNode )DataView.SelectedNode ).ObjectClass.MyType();
                foreach ( OCEditorBase tmpEd in PE.GetEditorPlugIns() ) {
                    if ( tmpEd.IEdit( editType ) ) {
                        ed = tmpEd; break;
                    }
                }

                List<string>
                    IDs = new List<string>( DataFile.IDs );

                //todo make better
                if ( ed == null )
                    throw new Exception(
                        "I couldnt find an editor to go with this Object type," + Environment.NewLine +
                        "Please makesure that the plugin creator also created a GUI editor for this object." );

                ed.Collectors = DataFile.Collectors;

                ed.MyObject = ( ( OCNode )DataView.SelectedNode ).ObjectClass;

                switch ( ed.RunEditor( EditorMode.Edit ) ) {
                    case EditorExitCode.OK:
                        DataFile.Edit( ( ( OCNode )DataView.SelectedNode ).ObjectClass, ed.MyObject );
                        break;
                    case EditorExitCode.Cancel:
                        break;
                    default:
                        break;
                }
            }
            catch ( Exception ex ) {
                //todo need exeception msg box
            }
        }

        private void copyObjectToolStripMenuItem_Click( object sender, EventArgs e ) {
            try {
                if (DataView.SelectedNode is OCNode)
                    CopyClip.CopyToClipboard(((OCNode) DataView.SelectedNode).ObjectClass);
            }
            catch (Exception ex) {
                //todo need exeception msg box

            }
        }

        private void clearObjectsToolStripMenuItem_Click( object sender, EventArgs e ) {
            //todo
        }

        private void newObjectToolStripMenuItem_Click( object sender, EventArgs e ) {
            try {
                OCEditorBase ed = null;
                Type editType =( ( OCCNode )DataView.SelectedNode ).Collector.CollectionType;
                foreach ( OCEditorBase tmpEd in PE.GetEditorPlugIns() ) {
                    if ( tmpEd.IEdit( editType ) ) {
                        ed = tmpEd; break;
                    }
                }

                List<string>
                IDs = new List<string>( DataFile.IDs );

                //todo make better
                if ( ed == null )
                    throw new Exception(
                        "I couldnt find an editor to go with this Object type," + Environment.NewLine +
                        "Please makesure that the plugin creator also created a GUI editor for this object." );

                ed.Collectors = DataFile.Collectors;

                ed.MyObject = ( ObjectClassBase )Activator.CreateInstance( editType );

                ed.MyObject.ID = DataFile.GenerateIDCode();

                //ed.MyObject.myOwner = DataFile;

                switch ( ed.RunEditor( EditorMode.New ) ) {
                    case EditorExitCode.OK:
                        //ed.MyObject.myOwner = DataFile;

                        DataFile.Add( ed.MyObject );
                        break;
                    case EditorExitCode.Cancel:
                        break;
                    default:
                        break;
                }
            }
            catch ( Exception ex ) {
                //todo need exeception msg box

            }
        }

        private void deleteObjectToolStripMenuItem_Click( object sender, EventArgs e ) {
            if ( DataView.SelectedNode is OCNode )
                DataFile.Drop( ( ( OCNode )DataView.SelectedNode ).ObjectClass );
        }

        private void previewObjectToolStripMenuItem_Click( object sender, EventArgs e ) {
            ObjectPreview objpre = null;
            if ( DataView.SelectedNode is OCNode ) {
                ObjectClassBase obj = ( ( OCNode )DataView.SelectedNode ).ObjectClass;

                if ( obj != null ) {
                    objpre = new ObjectPreview( obj );
                    objpre.ShowDialog();
                }
            }
        }

        private void saveToolStripMenuItem_Click( object sender, EventArgs e ) {
            try {
                DataFile.Save();
                Title = DataFile.FileName;
            }
            catch ( Exception ex ) {
                //todo need exeception msg box

            }
        }

        private void saveAsToolStripMenuItem_Click( object sender, EventArgs e ) {
            try {
                DataFile.SaveAs();
                Title = DataFile.FileName;

            }
            catch ( Exception ex ) {
                //todo need exeception msg box

            }
        }

        private void newToolStripMenuItem_Click( object sender, EventArgs e ) {

            try {
                //does the curent data need to be saved?
                if ( CheckForSave() ) {
                    //action not canceled flush the current node list
                    DataView.Nodes.Clear();
                    Orphans.Nodes.Clear();
                    //create a new reference
                    DataFile = new NTDataFile();

                    DataFile.Updating += new NTEventHandler<UpdaterEventArgs>( DataFile_Updating );
                    DataFile.Update += new NTEventHandler<UpdateProgressEventArgs>( DataFile_Update );
                    DataFile.Updated += new NTEventHandler( DataFile_Updated );
                    DataFile.EventOrphansChanged += new NTEventHandler<ItemChangedArgs>( DataFile_EventOrphansChanged );
                    DataFile.LockStatusChange += new NTEventHandler( DataFile_LockStatusChange );

                    //load the nodes
                    DataFile.getTreeNodes( DataView.Nodes, OCCMenuStrip, OCMenuStrip );
                    DataView.Nodes.Add( Orphans );
                    //change the file title
                    Title = DataFile.FileName;
                }
            }
            catch ( Exception ex ) {
                //todo need exeception msg box

            }
        }

        private void openToolStripMenuItem_Click( object sender, EventArgs e ) {
            try {
                if ( CheckForSave() ) {
                    OpenFileDialog OFD = new OpenFileDialog();
                    OFD.Filter = "NewTerra Dat Files (*.ntx)|*.ntx";
                    OFD.SupportMultiDottedExtensions = true;
                    OFD.Multiselect = false;
                    if ( OFD.ShowDialog() == DialogResult.OK ) {
                        Orphans.Nodes.Clear();
                        DataView.Nodes.Clear();
                        DataFile = new NTDataFile( OFD.FileName );

                        DataFile.Updating += new NTEventHandler<UpdaterEventArgs>( DataFile_Updating );
                        DataFile.Update += new NTEventHandler<UpdateProgressEventArgs>( DataFile_Update );
                        DataFile.Updated += new NTEventHandler( DataFile_Updated );
                        DataFile.EventOrphansChanged += new NTEventHandler<ItemChangedArgs>( DataFile_EventOrphansChanged );
                        DataFile.LockStatusChange += new NTEventHandler( DataFile_LockStatusChange );

                        //DataFile.Load();
                        bgw.RunWorkerAsync();

                        Title = DataFile.FileName;
                        //DataFile.getTreeNodes( DataView.Nodes, OCCMenuStrip, OCMenuStrip );
                    }
                }
            }
            catch ( Exception ex ) {
                //todo need exeception msg box

            }
        }

        private void DataView_MouseClick( object sender, MouseEventArgs e ) {
            try {
                if ( e.Button == MouseButtons.Right )
                    DataView.SelectedNode = DataView.GetNodeAt( e.Location );
            }
            catch ( Exception ex ) {
                //todo need exeception msg box
            }
        }

        private void exitToolStripMenuItem_Click( object sender, EventArgs e ) {
            Application.Exit();
        }

        private void printPreviewToolStripMenuItem_Click( object sender, EventArgs e ) {
            try {
                setUpPrint();
                PrintPreviewDialog pPreview = _PrintEngine.ShowPreview();
                pPreview.ShowDialog();
            }
            catch ( Exception ex ) {

            }
        }

        private void printToolStripMenuItem_Click( object sender, EventArgs e ) {
            try {
                PrintDialog pDialog = _PrintEngine.ShowPrintDialog();
                setUpPrint();
                if ( pDialog.ShowDialog() == DialogResult.OK ) {
                    NTAF.PrintEngine.Properties.Settings.Default.printerSettings = pDialog.PrinterSettings;
                    _PrintEngine.Print();
                    NTAF.PrintEngine.Properties.Settings.Default.Save();
                }
            }
            catch ( Exception ex ) {

            }
        }

        private void PageSettingsMenuItem_Click( object sender, EventArgs e ) {
            try {
                setUpPrint();
                PageSetupDialog myEngineSettings = _PrintEngine.ShowPageSettings();

                if ( myEngineSettings.ShowDialog() == DialogResult.OK ) {
                    NTAF.PrintEngine.Properties.Settings.Default.printSettings = myEngineSettings.PageSettings;
                    NTAF.PrintEngine.Properties.Settings.Default.Save();
                }
            }
            catch ( Exception ex ) {

            }
        }

        private void PrintSettingsMenuItem_Click( object sender, EventArgs e ) {
            try {
                setUpPrint();
                FontDialog myPrintFontSettings = _PrintEngine.ShowFontDialog();

                if ( myPrintFontSettings.ShowDialog() == DialogResult.OK ) {
                    _PrintEngine.PrintFont = myPrintFontSettings.Font;
                    NTAF.PrintEngine.Properties.Settings.Default.printFontSettings = myPrintFontSettings.Font;
                    NTAF.PrintEngine.Properties.Settings.Default.Save();
                }
            }
            catch ( Exception ex ) {

            }
        }

        private void undoToolStripMenuItem_Click( object sender, EventArgs e ) {
            try { DataFile.DoUndo(); }
            catch ( Exception ex ) { MessageBox.Show( ex.Message ); }
        }

        private void redoToolStripMenuItem_Click( object sender, EventArgs e ) {
            try { DataFile.DoRedo(); }
            catch ( Exception ex ) { MessageBox.Show( ex.Message ); }
        }

        private void cutToolStripMenuItem_Click( object sender, EventArgs e ) {
            try {
                if ( DataView.SelectedNode is OCNode ) {
                    CopyClip.CopyToClipboard( ( ( OCNode )DataView.SelectedNode ).ObjectClass );
                    DataFile.Drop( ( ( OCNode )DataView.SelectedNode ).ObjectClass );
                }
            }
            catch ( Exception ex ) {
                //todo need exeception msg box

            }
        }

        private void pasteToolStripMenuItem_Click( object sender, EventArgs e ) {
            try {
                CopyClip.CopyFromClipboard( DataFile );
            }
            catch ( Exception ex ) {
                //todo need exception msg box
            }
        }

        private void customizeToolStripMenuItem_Click( object sender, EventArgs e ) {

        }

        private void optionsToolStripMenuItem_Click( object sender, EventArgs e ) {

        }

        private void contentsToolStripMenuItem_Click( object sender, EventArgs e ) {

        }

        private void indexToolStripMenuItem_Click( object sender, EventArgs e ) {

        }

        private void searchToolStripMenuItem_Click( object sender, EventArgs e ) {

        }

        private void aboutToolStripMenuItem_Click( object sender, EventArgs e ) {
            AboutBox1 abx = new AboutBox1();
            abx.ShowDialog();
        }

        private void currentlyLoadedPluginsToolStripMenuItem_Click( object sender, EventArgs e ) {
            LoadedModuleBox lmbx = new LoadedModuleBox();
            lmbx.ShowDialog();
        }

        private void purgeFileToolStripMenuItem_Click( object sender, EventArgs e ) {
            DataFile.PurgeFile();
        }


        private void ExportTXTFile( object sender, EventArgs e ) {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Export to .txt file...";
            sfd.Filter = "Text file (*.txt)|*.txt";
            sfd.AddExtension = true;
            sfd.OverwritePrompt = true;
            
            if(sfd.ShowDialog() == DialogResult.OK ) {
                DataFile.ExportToTXT(sfd.FileName);
            }
        }

        private void ExportCSVFile( object sender, EventArgs e ) {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Export to .csv file...";
            sfd.Filter = "Comma Separated Values file (*.csv)|*.csv";
            sfd.AddExtension = true;
            sfd.OverwritePrompt = true;

            if ( sfd.ShowDialog() == DialogResult.OK ) {
                DataFile.ExportToCSV( sfd.FileName );
            }
        }

        private void lockFileToolStripMenuItem_Click( object sender, EventArgs e ) {
            if ( DataFile.FileLocked ) {
                try { DataFile.UnLockFile( InputBox.Show( "Please enter the files password", "Password Protected...", true ) ); }
                catch ( InvalidPasswordException ex ) { MessageBox.Show( "Incorrect Password" ); }
                catch ( Exception ex ) { /*todo need exception msg box*/ }
            }
            else {
                bool exitCode = false;
                while ( !exitCode ) {
                    try {
                        DataFile.LockFile();
                        exitCode = true;
                    }
                    catch ( NullPasswordException ) {
                        String Password =
                            InputBox.Show( "Please enter a password for the file", "Enter New Password...", true );

                        //if the user didnt enter anything exit the function
                        if ( Password == "" ) {
                            MessageBox.Show( "Password not set", "File not locked...", MessageBoxButtons.OK ); return;
                        }

                        //get a matching password from the user
                        String PasswordConf =
                            InputBox.Show( "Please confirm the password for the file", "Confirm New Password...", true );

                        if ( Password.CompareTo( PasswordConf ) != 0 ) {
                            MessageBox.Show( "Passwords don't match", "Could Not Confirm New Password...", MessageBoxButtons.OK );
                        }
                        else {
                            DataFile.FilePassword = Password;
                        }
                    }
                }
            }
        }

        private void setPasswordToolStripMenuItem_Click( object sender, EventArgs e ) {
            try {
                String Password =
                            InputBox.Show( "Please enter a password for the file", "Enter New Password...", true );

                //if the user didnt enter anything exit the function
                if ( Password == "" ) {
                    MessageBox.Show( "Password not set", "File not locked...", MessageBoxButtons.OK ); return;
                }

                //get a matching password from the user
                String PasswordConf =
                            InputBox.Show( "Please confirm the password for the file", "Confirm New Password...", true );

                if ( Password.CompareTo( PasswordConf ) != 0 ) {
                    MessageBox.Show( "Passwords don't match", "Could Not Confirm New Password...", MessageBoxButtons.OK );
                }
                else {
                    DataFile.FilePassword = Password;
                }
            }
            catch ( FileLockedException ) {
                MessageBox.Show( "Password not set", "File not locked...", MessageBoxButtons.OK ); return;
            }
        }
    }
}