using System;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Windows.Forms;
using NTAF.Core;
using NTAF.PlugInFramework;
using NTAF.PlugInFramework.OrphanControls;
using NTAF.PrintEngine;
using System.Collections.Generic;
using PE = NTAF.PlugInFramework.PluginEngine;
using System.Linq;

//
/*
todo: save functions for multi lodaed files
todo: display file nodes different when data has changed
todo: copy and paste from one file to another
todo:
todo:
todo:
todo:
todo:
todo:
todo:
todo:
todo:
*/

namespace UniverseBuilderSingle {

    public partial class Main : Form {
        //NTDataFile
        //    DataFile = new NTDataFile();

        List<NTDataFile>
            DataFiles = new List<NTDataFile>(),
            LoadCache = new List<NTDataFile>();

        //TreeNode rootNode = new TreeNode("OpenDataFiles");

        BackgroundWorker
            bgw = new BackgroundWorker();

        //todo have orphans managed in data file, need to make sure they don't get saved out to the file
        //TreeNode
        //    Orphans = new TreeNode( "Orphaned Objects" );

        PrintEngine 
            _PrintEngine = null;

        ImageList TreeIcons = new ImageList();

        private NTDataFile DataFile {
        get {
            try {
                TreeNode selectedNode = DataView.SelectedNode;

                do {
                    selectedNode = selectedNode.Parent;
                } while (selectedNode.Parent != null);

                string datafileName = selectedNode.Text;

                NTDataFile retVal = DataFiles.First(df => df.FileName == datafileName);

                return retVal;

                } catch (Exception ex) {
                    return null;
                }
            }
            
        }

        public Main() {
            InitializeComponent();

            Text = "ProtoGears Universe Builder";

            BuildMenu();


            //DataView.Nodes.Add(rootNode);
            
            //TreeIcons.Images.Add("databaseLocked", Properties.Resources.database_Locked);
            //TreeIcons.Images.Add("databaseUnLocked", Properties.Resources.database_UnLocked);
            //TreeIcons.Images.Add("Folder", Properties.Resources.Folder);
            //TreeIcons.Images.Add("FolderOpen", Properties.Resources.Folder_Open);
            //TreeIcons.Images.Add("FileClass", Properties.Resources.File_Class);
            //TreeIcons.Images.Add("FolderQuestion", Properties.Resources.Folder_Question);
            //TreeIcons.Images.Add("FileQuestion", Properties.Resources.File_Question);

            //NTAF.PlugInFramework.Properties.Settings.Default.ImageDataNodeLocked = "databaseLocked";
            //NTAF.PlugInFramework.Properties.Settings.Default.ImageDataNodeUnlocked = "databaseUnLocked";
            //NTAF.PlugInFramework.Properties.Settings.Default.ImageOCCNodeClosed = "Folder";
            //NTAF.PlugInFramework.Properties.Settings.Default.ImageOCCNodeOpen = "FolderOpen";
            //NTAF.PlugInFramework.Properties.Settings.Default.ImageOCNode = "File_Class";
            //NTAF.PlugInFramework.Properties.Settings.Default.ImageOrphanCollectorNode = "FolderQuestion";
            //NTAF.PlugInFramework.Properties.Settings.Default.ImageOrphanNode = "FileQuestion";

            //DataView.ImageList = TreeIcons;

            //bgw.RunWorkerAsync();

            bgw.DoWork += new DoWorkEventHandler( bgw_DoWork );

            bgw.ProgressChanged += new ProgressChangedEventHandler( bgw_ProgressChanged );

            bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler( bgw_RunWorkerCompleted );

            bgw.WorkerReportsProgress = true;

            bgw.WorkerSupportsCancellation = true;

            //DataFile.EventOrphansChanged += new NTEventHandler<ItemChangedArgs>( DataFile_EventOrphansChanged );

            //DataFile.LockStatusChange += new NTEventHandler( DataFile_LockStatusChange );
            //DataFile = new NTDataFile(
        }

        void DataFile_LockStatusChange() {
            //lockFileToolStripMenuItem.Checked = DataFile.FileLocked;
            if (DataFile.FileLocked) {
                toolStripStatusFileLock.Text = "<Locked>";
                toolStripStatusFileLock.BackColor = System.Drawing.Color.Red;
            }else {
                toolStripStatusFileLock.Text = "<UnLocked>";
                toolStripStatusFileLock.BackColor = System.Drawing.Color.Green;
            }
            //todo: make label to replace this control
            //FileLockIndicator.Checked = DataFile.FileLocked;
        }

        void bgw_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {
            UpdateProgressLabel1.Text = "Ready...";
            UpdateProgressBar1.Value = 0;



            foreach (NTDataFile dataFile in LoadCache) {
                dataFile.getTreeNodes(DataView.Nodes, FileNodeMenuStrip, OCCMenuStrip, OCMenuStrip, OrphanRootMenuStrip, OrphanMenuStrip);
                DataFiles.Add(dataFile);
            }
            LoadCache.Clear();
            UpdateProgressBar1.Visible = false;

        }

        void bgw_ProgressChanged( object sender, ProgressChangedEventArgs e ) {
            if ( !UpdateProgressBar1.Visible )
                UpdateProgressBar1.Visible = true;
            UpdateProgressEventArgs args = e.UserState as UpdateProgressEventArgs;
            UpdateProgressBar1.Maximum = args.NumberOfItems;
            UpdateProgressBar1.Value = args.current;
            UpdateProgressLabel1.Text = args.ProcessingMessage;
            this.statusStrip1.Invalidate( true );
        }

        void bgw_DoWork( object sender, DoWorkEventArgs e ) {
  

            //DataFile.Load(); //00:00:20.8476186 to open and link 200ish items
            //DataFile.LinkData();

            //DataFile.Load2(); //00:00:08.1744501 to open and link 200ish items
            //DataFile.LinkData();

            //todo:: the only way its going to get faster is if linking is done on demand when an object is used
            foreach (NTDataFile dataFile in LoadCache) {
                    DateTime fileloadStart = DateTime.Now;
                    dataFile.Load3();
                    DateTime fileloadFinish = DateTime.Now;
                    Console.WriteLine(String.Format("Load Method 1:{0}", (fileloadFinish - fileloadStart)));
                }
            //DataFile.Load3(); //00:00:08.2179888 to open and link 200ish items
            //data could be linked on demand for example when you edit an object is find references to that object and links at that time.

            }

        void DataFile_Updating( UpdaterEventArgs args ) {
            //UpdateProgressBar.Value = 0;
            //UpdateProgressBar.Minimum = 0;
            //UpdateProgressBar.Maximum = args.NumberOfItems;
        }

        void DataFile_Update( UpdateProgressEventArgs args ) {
            bgw.ReportProgress( args.Percent, args );
        }

        void DataFile_Updated() {

        }

        //private String Title {
        //    get { return this.Text; }
        //    set {
        //        if ( value == "" | value == null )
        //            this.Text = "New Terra Universe Builder - New Data File";
        //        else
        //            this.Text = "New Terra Universe Builder - " + value;
        //    }
        //}

        private void DataView_AfterSelect(object sender, TreeViewEventArgs e) {
            //clear last selection of nodes
            comboBox1.Items.Clear();
            Type t = DataView.SelectedNode.GetType();
            if (!(DataView.SelectedNode is OCCNode) & !(DataView.SelectedNode is OCNode) &
                 !(DataView.SelectedNode is Orphan) & !(DataView.SelectedNode is DataNode)) {
                //basic node that should contain nodes of OCCNodes
                foreach (OCCNode occn in DataView.SelectedNode.Nodes) {
                    foreach (OCNode ocn in occn.Nodes) {
                        comboBox1.Items.Add(ocn.ObjectClass);
                    }
                }
            }
            if (DataView.SelectedNode is OCCNode) {
                foreach (OCNode ocn in DataView.SelectedNode.Nodes) {
                    comboBox1.Items.Add(ocn.ObjectClass);
                }
            }
            if (DataView.SelectedNode is OCNode) {
                comboBox1.Items.Add(((OCNode)DataView.SelectedNode).ObjectClass);
            }

            if (comboBox1.Items.Count >= 1)
                comboBox1.SelectedIndex = 0;
            else {
                comboBox1.Items.Clear();
                comboBox1.Text = "";
                ObjectViewer.SelectedObject = null;
            }
        }

        private void comboBox1_SelectedIndexChanged( object sender, EventArgs e ) {
            if ( comboBox1.SelectedIndex == -1 )
                ObjectViewer.SelectedObject = null;
            else
                ObjectViewer.SelectedObject = comboBox1.SelectedItem;

        }

        private void DataView_Click( object sender, EventArgs e ) {

        }

        private bool CheckForSave() {
            if (DataFile == null) return true;
            if ( DataFile.DataChanged ) {
                switch ( MessageBox.Show( String.Format( "{0} has changed since it was opened would you like to save it now?", DataFile.FileName ), "Save file?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1 ) ) {
                    case DialogResult.Yes:
                        DataFile.Save();
                        return true;
                    case DialogResult.Cancel:
                        return false;

                }
                return true;
            }
            return true;
        }

        private void Main_FormClosing( object sender, FormClosingEventArgs e ) {
            if ( !CheckForSave() ) {
                e.Cancel = true;
            }
        }

        private void DataView_KeyPress( object sender, KeyPressEventArgs e ) {
            e.Handled = true;
        }

        private void DataView_KeyUp( object sender, KeyEventArgs e ) {
            try {
                switch ( e.KeyCode | e.Modifiers ) {
                    case PreViewObjectKey:
                        previewObjectToolStripMenuItem_Click( null, null );
                        e.Handled = true;
                        break;
                    case EditObjectKey:
                        editObjectToolStripMenuItem_Click( null, null );
                        e.Handled = true;
                        break;
                    case DeleteObjectKey:
                        deleteObjectToolStripMenuItem_Click( null, null );
                        e.Handled = true;
                        break;
                    case CopyObjectKey:
                        copyObjectToolStripMenuItem_Click( null, null );
                        e.Handled = true;
                        break;
                    case NewObjectKey:
                        newObjectToolStripMenuItem_Click( null, null );
                        e.Handled = true;
                        break;
                    case ClearObjectKey:
                        clearObjectsToolStripMenuItem_Click( null, null );
                        e.Handled = true;
                        break;
                }
            }
            catch ( Exception ex ) { Console.Write( ex.Message ); }
        }

        private void DataView_MouseDoubleClick( object sender, MouseEventArgs e ) {
            if ( DataView.SelectedNode is OCNode )
                editObjectToolStripMenuItem_Click( sender, null );
        }

        public void setUpPrint() {
            if ( _PrintEngine == null )
                _PrintEngine = new PrintEngine( DataFile.FileName );

            List<IPrintable> printObjects = new List<IPrintable>();
            foreach ( Object obj in DataFile.AllData )
                if ( obj is IPrintable ) {
                    printObjects.Add( ( IPrintable )obj );
                    printObjects.Add( new PrintBreakLine() );
                }

            _PrintEngine.ResetPrintables( printObjects.ToArray() );
        }

        private void FileLockIndicator_Click( object sender, EventArgs e ) {
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

        //=============================menu stuff=============================//
     #region Menu stuff
        const Keys PreViewObjectKey = Keys.Control | Keys.Shift | Keys.P;
        const Keys EditObjectKey = Keys.Control | Keys.Shift | Keys.E;
        const Keys DeleteObjectKey = Keys.Delete;
        const Keys CopyObjectKey = Keys.Control | Keys.Shift | Keys.C;
        const Keys NewObjectKey = Keys.Control | Keys.Shift | Keys.N;
        const Keys ClearObjectKey = Keys.Control | Keys.Shift | Keys.D;

        ContextMenuStrip FileNodeMenuStrip;
        ToolStripMenuItem closeFileMenuItem;
        ToolStripMenuItem saveFileMenuItem;
        ToolStripMenuItem reloadFileMenuItem;

        ContextMenuStrip OCMenuStrip;
        ToolStripMenuItem previewObjectToolStripMenuItem;
        ToolStripMenuItem editObjectToolStripMenuItem;
        ToolStripMenuItem deleteObjectToolStripMenuItem;
        ToolStripMenuItem copyObjectToolStripMenuItem;

        ContextMenuStrip OCCMenuStrip;
        ToolStripMenuItem newObjectToolStripMenuItem;
        ToolStripMenuItem clearObjectsToolStripMenuItem;

        ContextMenuStrip OrphanRootMenuStrip;
        ToolStripMenuItem OrphanPurgeMenuItem;

        ContextMenuStrip OrphanMenuStrip;
        ToolStripMenuItem moveOprphanToCollectorMenuItem;
        ToolStripMenuItem findOrphanReferencesMenuItem;

        void BuildMenu() {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Main));

            previewObjectToolStripMenuItem = new ToolStripMenuItem();
            previewObjectToolStripMenuItem.Name = "previewObjectToolStripMenuItem";
            previewObjectToolStripMenuItem.ShortcutKeys = PreViewObjectKey;
            previewObjectToolStripMenuItem.Text = "&Preview Object";
            previewObjectToolStripMenuItem.Click += new EventHandler(previewObjectToolStripMenuItem_Click);

            editObjectToolStripMenuItem = new ToolStripMenuItem();
            editObjectToolStripMenuItem.Name = "editObjectToolStripMenuItem";
            editObjectToolStripMenuItem.ShortcutKeys = EditObjectKey;
            editObjectToolStripMenuItem.Text = "&Edit Object";
            editObjectToolStripMenuItem.Click += new EventHandler(editObjectToolStripMenuItem_Click);

            deleteObjectToolStripMenuItem = new ToolStripMenuItem();
            deleteObjectToolStripMenuItem.Name = "deleteObjectToolStripMenuItem";
            deleteObjectToolStripMenuItem.ShortcutKeys = DeleteObjectKey;
            deleteObjectToolStripMenuItem.Text = "&Delete Object";
            deleteObjectToolStripMenuItem.Click += new EventHandler(deleteObjectToolStripMenuItem_Click);

            copyObjectToolStripMenuItem = new ToolStripMenuItem();
            //copyObjectToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyObjectToolStripMenuItem.Image")));
            copyObjectToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            copyObjectToolStripMenuItem.Name = "copyObjectToolStripMenuItem";
            copyObjectToolStripMenuItem.ShortcutKeys = CopyObjectKey;
            copyObjectToolStripMenuItem.Text = "&Copy";
            copyObjectToolStripMenuItem.Click += new EventHandler(copyObjectToolStripMenuItem_Click);

            newObjectToolStripMenuItem = new ToolStripMenuItem();
            newObjectToolStripMenuItem.Name = "newObjectToolStripMenuItem";
            newObjectToolStripMenuItem.ShortcutKeys = NewObjectKey;
            newObjectToolStripMenuItem.Text = "&New Object";
            newObjectToolStripMenuItem.Click += new EventHandler(newObjectToolStripMenuItem_Click);

            clearObjectsToolStripMenuItem = new ToolStripMenuItem();
            clearObjectsToolStripMenuItem.Name = "clearObjectsToolStripMenuItem";
            clearObjectsToolStripMenuItem.ShortcutKeys = ClearObjectKey;
            clearObjectsToolStripMenuItem.Text = "&Clear All Objects";
            clearObjectsToolStripMenuItem.Click += new EventHandler(clearObjectsToolStripMenuItem_Click);

            moveOprphanToCollectorMenuItem = new ToolStripMenuItem();
            moveOprphanToCollectorMenuItem.Name = "moveOprphanToCollectorMenuItem";
            //moveOprphanToCollectorMenuItem.ShortcutKeys = ClearObjectKey;
            moveOprphanToCollectorMenuItem.Text = "Recreate orphan";
            //todo create click event
            //moveOprphanToCollectorMenuItem.Click += new EventHandler(clearObjectsToolStripMenuItem_Click);

            findOrphanReferencesMenuItem = new ToolStripMenuItem();
            findOrphanReferencesMenuItem.Name = "findOrphanReferencesMenuItem";
            //findOrphanReferencesMenuItem.ShortcutKeys = ClearObjectKey;
            findOrphanReferencesMenuItem.Text = "Find Orphan References";
            //todo create click event
            //findOrphanReferencesMenuItem.Click += new EventHandler(clearObjectsToolStripMenuItem_Click);

            OrphanPurgeMenuItem = new ToolStripMenuItem();
            OrphanPurgeMenuItem.Name = "OrphanPurgeMenuItem";
            //OrphanPurgeMenuItem.ShortcutKeys = ClearObjectKey;
            OrphanPurgeMenuItem.Text = "Purge All Orphans";
            //todo create click event
            //OrphanPurgeMenuItem.Click += new EventHandler(clearObjectsToolStripMenuItem_Click);

            closeFileMenuItem = new ToolStripMenuItem(
                "Close File",
                null,
                null,
                "closeFileMenuItem");
            saveFileMenuItem = new ToolStripMenuItem(
                "Save File",
                null,
                null,
                "saveFileMenuItem");
            reloadFileMenuItem = new ToolStripMenuItem(
                "Reload File",
                null,
                null,
                "reloadFileMenuItem");

            OCMenuStrip = new ContextMenuStrip();
            OCMenuStrip.Items.AddRange(new ToolStripItem[]{
                previewObjectToolStripMenuItem,
                editObjectToolStripMenuItem,
                copyObjectToolStripMenuItem,
                deleteObjectToolStripMenuItem});
            OCMenuStrip.Name = "OCMenuStrip";

            OCCMenuStrip = new ContextMenuStrip();
            OCCMenuStrip.Items.AddRange(new ToolStripItem[]{
                newObjectToolStripMenuItem,
                clearObjectsToolStripMenuItem});
            OCCMenuStrip.Name = "OCCMenuStrip";

            OrphanRootMenuStrip = new ContextMenuStrip();
            OrphanRootMenuStrip.Items.AddRange(new ToolStripItem[] {
                OrphanPurgeMenuItem
            });
            OrphanRootMenuStrip.Name = "OrphanRootMenuStrip";

            //todo add orphan menu here
            OrphanMenuStrip = new ContextMenuStrip();
            OrphanMenuStrip.Items.AddRange(new ToolStripItem[] {
                moveOprphanToCollectorMenuItem,
                findOrphanReferencesMenuItem });
            OrphanMenuStrip.Name = "OrphanMenuStrip";

            FileNodeMenuStrip = new ContextMenuStrip();
            FileNodeMenuStrip.Items.AddRange(new ToolStripItem[] {
                closeFileMenuItem,
                saveFileMenuItem,
                reloadFileMenuItem
            });
            FileNodeMenuStrip.Name = "FileNodeMenuStrip";
        }

            #region MenuClick Events
                void editObjectToolStripMenuItem_Click(object sender, EventArgs e) {
                    try {
                        OCEditorBase ed = null;
                        Type editType = ((OCNode)DataView.SelectedNode).ObjectClass.MyType();
                        foreach (OCEditorBase tmpEd in PE.GetEditorPlugIns()) {
                            if (tmpEd.IEdit(editType)) {
                                ed = tmpEd; break;
                            }
                        }

                        List<string>
                            IDs = new List<string>(DataFile.IDs);

                        //todo make better
                        if (ed == null)
                            throw new Exception(
                                "I couldnt find an editor to go with this Object type," + Environment.NewLine +
                                "Please makesure that the plugin creator also created a GUI editor for this object.");

                        ed.Collectors = DataFile.Collectors;

                        ed.MyObject = ((OCNode)DataView.SelectedNode).ObjectClass;

                        switch (ed.RunEditor(EditorMode.Edit)) {
                            case EditorExitCode.OK:
                                DataFile.Edit(((OCNode)DataView.SelectedNode).ObjectClass, ed.MyObject);
                                break;
                            case EditorExitCode.Cancel:
                                break;
                            default:
                                break;
                        }
                    }
                    catch (Exception ex) {
                        //todo need exception msg box
                    }
                }

                private void copyObjectToolStripMenuItem_Click(object sender, EventArgs e) {
                    try {
                        if (DataView.SelectedNode is OCNode)
                            CopyClip.CopyToClipboard(((OCNode)DataView.SelectedNode).ObjectClass);
                    }
                    catch (Exception ex) {
                        //todo need exception msg box

                    }
                }

                private void clearObjectsToolStripMenuItem_Click(object sender, EventArgs e) {
                    //todo
                }

                private void newObjectToolStripMenuItem_Click(object sender, EventArgs e) {
                    try {
                        OCEditorBase ed = null;
                        Type editType = ((OCCNode)DataView.SelectedNode).Collector.CollectionType;
                        foreach (OCEditorBase tmpEd in PE.GetEditorPlugIns()) {
                            if (tmpEd.IEdit(editType)) {
                                ed = tmpEd; break;
                            }
                        }

                        List<string>
                        IDs = new List<string>(DataFile.IDs);

                        //todo make better
                        if (ed == null)
                            throw new Exception(
                                "I couldnt find an editor to go with this Object type," + Environment.NewLine +
                                "Please makesure that the plugin creator also created a GUI editor for this object.");

                        ed.Collectors = DataFile.Collectors;

                        ed.MyObject = (ObjectClassBase)Activator.CreateInstance(editType);

                        ed.MyObject.ID = DataFile.GenerateIDCode();

                        //ed.MyObject.myOwner = DataFile;

                        switch (ed.RunEditor(EditorMode.New)) {
                            case EditorExitCode.OK:
                                //ed.MyObject.myOwner = DataFile;

                                DataFile.Add(ed.MyObject);
                                break;
                            case EditorExitCode.Cancel:
                                break;
                            default:
                                break;
                        }
                    }
                    catch (Exception ex) {
                        //todo need exception msg box

                    }
                }

                private void deleteObjectToolStripMenuItem_Click(object sender, EventArgs e) {
                    if (DataView.SelectedNode is OCNode)
                        DataFile.Drop(((OCNode)DataView.SelectedNode).ObjectClass);
                }

                private void previewObjectToolStripMenuItem_Click(object sender, EventArgs e) {
                    ObjectPreview objpre = null;
                    if (DataView.SelectedNode is OCNode) {
                        ObjectClassBase obj = ((OCNode)DataView.SelectedNode).ObjectClass;

                        if (obj != null) {
                            objpre = new ObjectPreview(obj);
                            objpre.ShowDialog();
                        }
                    }
                }

                private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
                    try {
                        DataFile.Save();
                        //Title = DataFile.FileName;
                    }
                    catch (Exception ex) {
                        //todo need exception msg box

                    }
                }

                private void saveAsToolStripMenuItem_Click(object sender, EventArgs e) {
                    try {
                        DataFile.SaveAs();
                        //Title = DataFile.FileName;

                    }
                    catch (Exception ex) {
                        //todo need exception msg box

                    }
                }

                private void newToolStripMenuItem_Click(object sender, EventArgs e) {

                    try {
                        //does the current data need to be saved?
                        if (CheckForSave()) {
                            //action not canceled flush the current node list
                            DataView.Nodes.Clear();

                            //todo orphan nodes are being moved to the data file, this wont be needed
                            //Orphans.Nodes.Clear();
                            //create a new reference
                            
                            //DataFile = new NTDataFile();

                            //DataFile.Updating += new NTEventHandler<UpdaterEventArgs>(DataFile_Updating);
                            //DataFile.Update += new NTEventHandler<UpdateProgressEventArgs>(DataFile_Update);
                            //DataFile.Updated += new NTEventHandler(DataFile_Updated);

                            //todo move to data file management
                            //DataFile.EventOrphansChanged += new NTEventHandler<ItemChangedArgs>(DataFile_EventOrphansChanged);

                            //DataFile.LockStatusChange += new NTEventHandler(DataFile_LockStatusChange);

                            //load the nodes
                            //DataFile.getTreeNodes(DataView.Nodes, FileNodeMenuStrip, OCCMenuStrip, OCMenuStrip, OrphanRootMenuStrip, OrphanMenuStrip);
                            //todo orphans are being moved to data file management
                            //DataView.Nodes.Add(Orphans);
                            //change the file title
                            //Title = DataFile.FileName;
                        }
                    }
                    catch (Exception ex) {
                        //todo need exception message box

                    }
                }

                private void openToolStripMenuItem_Click(object sender, EventArgs e) {
                    try {
                        if (CheckForSave()){
                            OpenFileDialog OFD = new OpenFileDialog();
                            OFD.Filter = "NewTerra Data Files (*.ntx)|*.ntx";
                            OFD.SupportMultiDottedExtensions = true;
                            OFD.Multiselect = false;
                            if (OFD.ShowDialog() == DialogResult.OK){
                                //DataView.Nodes.Clear();
                                NTDataFile fileToLoad = new NTDataFile(OFD.FileName);

                                fileToLoad.Updating += new NTEventHandler<UpdaterEventArgs>(DataFile_Updating);
                                fileToLoad.Update += new NTEventHandler<UpdateProgressEventArgs>(DataFile_Update);
                                fileToLoad.Updated += new NTEventHandler(DataFile_Updated);
                                fileToLoad.LockStatusChange += new NTEventHandler(DataFile_LockStatusChange);

                                LoadCache.Add(fileToLoad);

                                bgw.RunWorkerAsync();
                            }
                        }

                        //if (CheckForSave()) {
                        //            OpenFileDialog OFD = new OpenFileDialog();
                        //            OFD.Filter = "NewTerra Data Files (*.ntx)|*.ntx";
                        //            OFD.SupportMultiDottedExtensions = true;
                        //            OFD.Multiselect = false;
                        //            if (OFD.ShowDialog() == DialogResult.OK) {

                        //                //todo this is moving to the data file shouldn't be needed
                        //                //Orphans.Nodes.Clear();
                        //                DataView.Nodes.Clear();
                        //                DataFile = new NTDataFile(OFD.FileName);

                        //                DataFile.Updating += new NTEventHandler<UpdaterEventArgs>(DataFile_Updating);
                        //                DataFile.Update += new NTEventHandler<UpdateProgressEventArgs>(DataFile_Update);
                        //                DataFile.Updated += new NTEventHandler(DataFile_Updated);
                        //                //DataFile.EventOrphansChanged += new NTEventHandler<ItemChangedArgs>(DataFile_EventOrphansChanged);
                        //                DataFile.LockStatusChange += new NTEventHandler(DataFile_LockStatusChange);

                        //                //DataFile.Load();
                        //                bgw.RunWorkerAsync();

                        //                //Title = DataFile.FileName;
                        //                //DataFile.getTreeNodes( DataView.Nodes, OCCMenuStrip, OCMenuStrip );
                        //            }
                        //        }
                    }
                    catch (Exception ex) {
                        //todo need exception message box

                    }
                }

                private void DataView_MouseClick(object sender, MouseEventArgs e) {
                    try {
                        if (e.Button == MouseButtons.Right)
                            DataView.SelectedNode = DataView.GetNodeAt(e.Location);
                    }
                    catch (Exception ex) {
                        //todo need exception msg box
                    }
                }

                private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
                    Application.Exit();
                }

                private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e) {
                    try {
                        setUpPrint();
                        PrintPreviewDialog pPreview = _PrintEngine.ShowPreview();
                        pPreview.ShowDialog();
                    }
                    catch (Exception ex) {

                    }
                }

                private void printToolStripMenuItem_Click(object sender, EventArgs e) {
                    try {
                        PrintDialog pDialog = _PrintEngine.ShowPrintDialog();
                        setUpPrint();
                        if (pDialog.ShowDialog() == DialogResult.OK) {
                            NTAF.PrintEngine.Properties.Settings.Default.printerSettings = pDialog.PrinterSettings;
                            _PrintEngine.Print();
                            NTAF.PrintEngine.Properties.Settings.Default.Save();
                        }
                    }
                    catch (Exception ex) {

                    }
                }

                private void PageSettingsMenuItem_Click(object sender, EventArgs e) {
                    try {
                        setUpPrint();
                        PageSetupDialog myEngineSettings = _PrintEngine.ShowPageSettings();

                        if (myEngineSettings.ShowDialog() == DialogResult.OK) {
                            NTAF.PrintEngine.Properties.Settings.Default.printSettings = myEngineSettings.PageSettings;
                            NTAF.PrintEngine.Properties.Settings.Default.Save();
                        }
                    }
                    catch (Exception ex) {

                    }
                }

                private void PrintSettingsMenuItem_Click(object sender, EventArgs e) {
                    try {
                        setUpPrint();
                        FontDialog myPrintFontSettings = _PrintEngine.ShowFontDialog();

                        if (myPrintFontSettings.ShowDialog() == DialogResult.OK) {
                            _PrintEngine.PrintFont = myPrintFontSettings.Font;
                            NTAF.PrintEngine.Properties.Settings.Default.printFontSettings = myPrintFontSettings.Font;
                            NTAF.PrintEngine.Properties.Settings.Default.Save();
                        }
                    }
                    catch (Exception ex) {

                    }
                }

                private void undoToolStripMenuItem_Click(object sender, EventArgs e) {
                    try { DataFile.DoUndo(); }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }

                private void redoToolStripMenuItem_Click(object sender, EventArgs e) {
                    try { DataFile.DoRedo(); }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }

                private void cutToolStripMenuItem_Click(object sender, EventArgs e) {
                    try {
                        if (DataView.SelectedNode is OCNode) {
                            CopyClip.CopyToClipboard(((OCNode)DataView.SelectedNode).ObjectClass);
                            DataFile.Drop(((OCNode)DataView.SelectedNode).ObjectClass);
                        }
                    }
                    catch (Exception ex) {
                        //todo need exception msg box

                    }
                }

                private void pasteToolStripMenuItem_Click(object sender, EventArgs e) {
                    try {
                        CopyClip.CopyFromClipboard(DataFile);
                    }
                    catch (Exception ex) {
                        //todo need exception msg box
                    }
                }

                private void customizeToolStripMenuItem_Click(object sender, EventArgs e) {

                }

                private void optionsToolStripMenuItem_Click(object sender, EventArgs e) {

                }

                private void contentsToolStripMenuItem_Click(object sender, EventArgs e) {

                }

                private void indexToolStripMenuItem_Click(object sender, EventArgs e) {

                }

                private void searchToolStripMenuItem_Click(object sender, EventArgs e) {

                }

                private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
                    AboutBox1 abx = new AboutBox1();
                    abx.ShowDialog();
                }

                private void currentlyLoadedPluginsToolStripMenuItem_Click(object sender, EventArgs e) {
                    LoadedModuleBox lmbx = new LoadedModuleBox();
                    lmbx.ShowDialog();
                }

                private void purgeFileToolStripMenuItem_Click(object sender, EventArgs e) {
                    DataFile.PurgeFile();
                }

                private void ExportTXTFile(object sender, EventArgs e) {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Title = "Export to .txt file...";
                    sfd.Filter = "Text file (*.txt)|*.txt";
                    sfd.AddExtension = true;
                    sfd.OverwritePrompt = true;

                    if (sfd.ShowDialog() == DialogResult.OK) {
                        DataFile.ExportToTXT(sfd.FileName);
                    }
                }

                private void ExportCSVFile(object sender, EventArgs e) {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Title = "Export to .csv file...";
                    sfd.Filter = "Comma Separated Values file (*.csv)|*.csv";
                    sfd.AddExtension = true;
                    sfd.OverwritePrompt = true;

                    if (sfd.ShowDialog() == DialogResult.OK) {
                        DataFile.ExportToCSV(sfd.FileName);
                    }
                }

                private void lockFileToolStripMenuItem_Click(object sender, EventArgs e) {
                    if (DataFile.FileLocked) {
                        try { DataFile.UnLockFile(InputBox.Show("Please enter the files password", "Password Protected...", true)); }
                        catch (InvalidPasswordException ex) { MessageBox.Show("Incorrect Password"); }
                        catch (Exception ex) { /*todo need exception msg box*/ }
                    }
                    else {
                        bool exitCode = false;
                        while (!exitCode) {
                            try {
                                DataFile.LockFile();
                                exitCode = true;
                            }
                            catch (NullPasswordException) {
                                String Password =
                                    InputBox.Show("Please enter a password for the file", "Enter New Password...", true);

                                //if the user didnt enter anything exit the function
                                if (Password == "") {
                                    MessageBox.Show("Password not set", "File not locked...", MessageBoxButtons.OK); return;
                                }

                                //get a matching password from the user
                                String PasswordConf =
                                    InputBox.Show("Please confirm the password for the file", "Confirm New Password...", true);

                                if (Password.CompareTo(PasswordConf) != 0) {
                                    MessageBox.Show("Passwords don't match", "Could Not Confirm New Password...", MessageBoxButtons.OK);
                                }
                                else {
                                    DataFile.FilePassword = Password;
                                }
                            }
                        }
                    }
                }

                private void setPasswordToolStripMenuItem_Click(object sender, EventArgs e) {
                try {
                    String Password =
                                InputBox.Show("Please enter a password for the file", "Enter New Password...", true);

                    //if the user didnt enter anything exit the function
                    if (Password == "") {
                        MessageBox.Show("Password not set", "File not locked...", MessageBoxButtons.OK); return;
                    }

                    //get a matching password from the user
                    String PasswordConf =
                                InputBox.Show("Please confirm the password for the file", "Confirm New Password...", true);

                    if (Password.CompareTo(PasswordConf) != 0) {
                        MessageBox.Show("Passwords don't match", "Could Not Confirm New Password...", MessageBoxButtons.OK);
                    }
                    else {
                        DataFile.FilePassword = Password;
                    }
                }
                catch (FileLockedException) {
                    MessageBox.Show("Password not set", "File not locked...", MessageBoxButtons.OK); return;
                }
            } 
            #endregion
        #endregion

    }
}
