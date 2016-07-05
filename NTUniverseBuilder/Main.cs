using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
using NTAF.Core;
using NTAF.UniverseBuilder.WinGui.MessageBoxes;
using NTAF.PrintEngine;
using NTAF.Core;
using NTAF.Core;
using NTAF.PlugInFramework;
using GDIDB;
using Microsoft.SqlServer.MessageBox;

namespace NTAF.UniverseBuilder.WinGui {
    using PrintEngine = NTAF.PrintEngine.PrintEngine;
    using NTAF.UniverseBuilder.WinGui.Properties;

    public partial class Main : Form {
        //ErrorDisplay
        //    ErrDisp = new ErrorDisplay();

        ExceptionMessageBox EMB;
        ProgressBox
            progrssBox = new ProgressBox( 0, "" );//

        public NTEventHandler updateTabEvent;

        PrintEngine 
            _PrintEngine = null;// new PrintEngine();

        static string 
            DataPath = Properties.Settings.Default.InstallLocation + @"Data";
        //DataPath = System.AppDomain.CurrentDomain.BaseDirectory + @"Data";

        NTAF.Core.NTData 
            loadedData = null;

        private NTDataFile workingData {
            get {
                NTDataFile retVal = null;
                NTDataTreeView NTDTV = null;
                if ( tabControl1.SelectedTab != null )
                    foreach ( Control control in tabControl1.SelectedTab.Controls ) {
                        if ( control is NTDataTreeView )
                            NTDTV = ( NTDataTreeView )control;
                    }
                if ( NTDTV != null )
                    retVal = NTDTV.DataFile;

                return retVal;
            }
        }

        public Main() {
            InitializeComponent();

            loadedData = new NTAF.Core.NTData( DataPath );

            loadedData.Updating += new NTEventHandler<UpdaterEventArgs>( updating );
            loadedData.Update += new NTEventHandler<UpdateProgressEventArgs>( update );
            loadedData.Updated += new NTEventHandler( updated );

            System.Threading.Thread
                UpdaterThread = new System.Threading.Thread( progrssBox.Show );
        }

        public Main( string[] openingFile ) {
            InitializeComponent();

            loadedData = new NTAF.Core.NTData( DataPath );

            loadedData.Updating += new NTEventHandler<UpdaterEventArgs>( updating );
            loadedData.Update += new NTEventHandler<UpdateProgressEventArgs>( update );
            loadedData.Updated += new NTEventHandler( updated );

            System.Threading.Thread
                UpdaterThread = new System.Threading.Thread( progrssBox.Show );

            try {
                List<System.IO.FileInfo> filesToLoad = new List<System.IO.FileInfo>();

                foreach ( string str in openingFile )
                    filesToLoad.Add( new System.IO.FileInfo( str ) );

                Collection<string> unloadables = new Collection<string>();

                unloadables = loadedData.Load( filesToLoad.ToArray() );

                if ( unloadables.Count != 0 ) {
                    string message = "The file(s)...\n";
                    foreach ( string str in unloadables )
                        message += str + "\n";
                    message += "Could not be loaded or a file with the same name is allreaddy open";
                    MessageBox.Show( message );
                }
            }
            catch ( Exception ex ) {
                //ErrDisp.ShowMsgDialog( ex );
            }
            // MessageBox.Show( ex.Message ); }

        }

        private void Main_Load( object sender, EventArgs e ) {
            memGraphics = new DBGraphics();
            memGraphics.CreateDoubleBuffer( this.CreateGraphics(), this.ClientRectangle.Width, this.ClientRectangle.Height );
            SetWindowLocation();

            Graphics G = this.CreateGraphics();
            Size S = new Size( 0, ( int )Math.Round( G.MeasureString( "string", this.Font ).Height * 2, MidpointRounding.AwayFromZero ) );
            this.tabControl1.ItemSize = S;

            this.Icon = Resources.Program;// Resources.Program;

            tabControl1.ControlAdded += new ControlEventHandler( tabControl1_ControlsChaanged );
            tabControl1.ControlRemoved += new ControlEventHandler( tabControl1_ControlsChaanged );
            tabControl1.Selected += new TabControlEventHandler( tabControl1_Selected );
            tabControl1.Selecting += new TabControlCancelEventHandler( tabControl1_Selecting );
            tabControl1.DrawItem += new DrawItemEventHandler( tabControl1_DrawItem );
            this.Closing += new CancelEventHandler( Main_Closing );

            foreach ( NTDataFile NTDF in loadedData.LoadedData )
                loadTab( NTDF );

            advancedEditorToolStripMenuItem.Checked = Properties.Settings.Default.AdvancedEdit;

            loadedData.EventFileAdded += new NTEventHandler( loadedData_EventFileAdded );
            //loadedData.OrphansUpdated += new NTEventHandler( loadedData_OrphansUpdated );

            updateTabEvent = new NTEventHandler( updateTabs );
        }

        private void SetWindowLocation() {
            if ( Properties.Settings.Default.WindowLoc == default( Point ) )
                this.StartPosition = FormStartPosition.CenterScreen;
            else {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = Properties.Settings.Default.WindowLoc;
            }
            this.Size = Properties.Settings.Default.WindowSize;

        }

        void update( UpdateProgressEventArgs args ) {
            MainStatusLabel.Text = String.Format( "{0} {1}...", args.verb, args.lastItem );
            MainStatusOf.Text = args.OfCount;
            MainStatusPBar.Maximum = args.NumberOfItems;
            MainStatusPBar.Value = args.current;
            MainStatusPercent.Text = args.PercentCompleeted;
            //progrssBox.UpdateBox( args );
            //System.Threading.Thread.Sleep( 1000 );
        }

        void updated() {
            MainStatusLabel.Text = String.Empty;
            MainStatusOf.Text = String.Empty;
            MainStatusPBar.Maximum = 100;
            MainStatusPBar.Value = 0;
            MainStatusPercent.Text = "0%";

            MainStatusLabel.Visible = false;
            MainStatusOf.Visible = false;
            MainStatusPBar.Visible = false;
            MainStatusPercent.Visible = false;

            //progrssBox.closeMe();

            //UpdaterThread.Suspend();
        }

        void updating( UpdaterEventArgs args ) {
            MainStatusLabel.Text = "Doing something please wait...";
            MainStatusOf.Text = String.Format( "0 of {0}", args.NumberOfItems );
            MainStatusPBar.Maximum = args.NumberOfItems;
            MainStatusPBar.Value = 0;
            MainStatusPercent.Text = "0%";

            MainStatusLabel.Visible = true;
            MainStatusOf.Visible = true;
            MainStatusPBar.Visible = true;
            MainStatusPercent.Visible = true;

            //progrssBox.ResetBox( args, "" );

            //UpdaterThread.Start();
        }

        void loadedData_EventFileAdded() {
            tabControl1.Invoke( updateTabEvent );

        }

        void updateTabs() {
            foreach ( NTDataFile NTDF in loadedData.LoadedData ) {
                bool loaded = false;
                foreach ( TabPage TP in this.tabControl1.Controls )
                    if ( TP.Tag.ToString() == NTDF.DataFileName + ':' + NTDF.FileName )
                        loaded = true;
                if ( !loaded )
                    loadTab( NTDF );
            }
            //insert orphan up date here
            OrphansUpdate();
        }

        void OrphansUpdate() {
            foreach ( TabPage TP in this.tabControl1.Controls )
                foreach ( Control ctrl in TP.Controls )
                    if ( ctrl is NTDataTreeView )
                        ( ( NTDataTreeView )ctrl ).UpdateOrphans( loadedData.OrphanedLeaves );
        }

        void tabControl1_DrawItem( object sender, DrawItemEventArgs e ) {
            e.DrawBackground();

            TabPage tabPage = tabControl1.TabPages[e.Index] as TabPage;

            using ( Brush foreColor = new SolidBrush( SystemColors.ControlText ) ) {

                using ( Brush backcolorBrush = new SolidBrush( SystemColors.Control ) ) {

                    StringFormat fmt = new StringFormat();

                    fmt.Alignment = StringAlignment.Center;

                    fmt.LineAlignment = StringAlignment.Center;

                    e.Graphics.FillRectangle( backcolorBrush, e.Bounds );

                    e.Graphics.DrawString( tabPage.Text, this.Font, foreColor, e.Bounds, fmt );

                }

            }
        }

        void tabControl1_Selecting( object sender, TabControlCancelEventArgs e ) {
            if ( workingData != null )
                workingData.LockStatusChange -= new NTEventHandler( workingData_LockStatusChange );
        }

        void workingData_LockStatusChange() {
            FileLockedToolStripMenuItem.Checked = workingData.FileLocked;
        }

        void tabControl1_Selected( object sender, TabControlEventArgs e ) {
            if ( workingData != null ) {
                _PrintEngine = new PrintEngine( workingData.DataFileName );
                _PrintEngine.DefaultPageSettings = NTAF.PrintEngine.Properties.Settings.Default.printSettings;
                FileLockedToolStripMenuItem.Checked = workingData.FileLocked;
                workingData.LockStatusChange += new NTEventHandler( workingData_LockStatusChange );
            }
        }

        void Main_Closing( object sender, CancelEventArgs e ) {
            foreach ( NTDataFile NTDF in loadedData.LoadedData ) {
                if ( NTDF.DataChanged ) {
                    switch ( MessageBox.Show( String.Format( "The file {0} has changed, \n save changes?", workingData.FileName ),
                                           "Save changed Data?", MessageBoxButtons.YesNoCancel ) ) {
                        case DialogResult.Yes:
                            workingData.Save();
                            break;
                        case DialogResult.Cancel:
                            e.Cancel = true;
                            break;
                    }
                }
            }
        }

        void tabControl1_ControlsChaanged( object sender, ControlEventArgs e ) {
            if ( tabControl1.Controls.Count >= 1 ) {
                tabControl1.Visible = true;
                closeTabToolStripMenuItem.Visible = true;
                securityToolStripMenuItem.Enabled = true;
                printPreviewToolStripMenuItem.Enabled = true;
                printToolStripMenuItem.Enabled = true;
                FileLockedToolStripMenuItem.Checked = workingData.FileLocked;
                _PrintEngine = new PrintEngine( workingData.DataFileName );
            }
            else {
                tabControl1.Visible = false;
                closeTabToolStripMenuItem.Visible = false;
                securityToolStripMenuItem.Enabled = false;
                printPreviewToolStripMenuItem.Enabled = false;
                printToolStripMenuItem.Enabled = false;
            }
        }

        private void loadTab( NTDataFile NTDF ) {
            TabPage TP = new TabPage( NTDF.DataFileName + '\n' + NTDF.FileName );
            NTDataTreeView NTDTV = new NTDataTreeView( NTDF );

            NTDTV.editToolStripMenuItem.Click += new EventHandler( NTDTV_editToolStripMenuItem_Click );
            NTDTV.removeToolStripMenuItem.Click += new EventHandler( NTDTV_removeToolStripMenuItem_Click );
            NTDTV.newToolStripMenuItem.Click += new EventHandler( NTDTV_newToolStripMenuItem_Click );
            NTDTV.previewToolStripMenuItem.Click += new EventHandler( NTDTV_previewToolStripMenuItem_Click );
            NTDTV.DataFile.EventDataStateChanged += new NTEventHandler( NTDTV_DataFile_EventDataStateChanged );
            NTDTV.copyToolStripMenuItem.Click += new EventHandler( copyToolStripMenuItem_Click );

            if ( tabControl1.TabCount == 0 )
                NTDF.LockStatusChange += new NTEventHandler( workingData_LockStatusChange );


            NTDTV.Dock = DockStyle.Fill;

            NTDTV.EventNodeMouseDoubleClick += new NTEventHandler( NTDTV_MouseClick );
            NTDTV.EventMakeNew += new EventHandler( NTDTV_newToolStripMenuItem_Click );
            NTDTV.EventEditExisting += new EventHandler( NTDTV_editToolStripMenuItem_Click );
            NTDTV.EventPreview += new EventHandler( NTDTV_previewToolStripMenuItem_Click );

            TP.Controls.Add( NTDTV );
            TP.Tag = NTDF.DataFileName + ':' + NTDF.FileName;
            tabControl1.TabPages.Add( TP );
        }

        void NTDTV_MouseClick() {
            try {
                TreeNode selectedNode = null;
                NTDataTreeView NTDTV = null;
                foreach ( Control control in tabControl1.SelectedTab.Controls ) {
                    if ( control is NTDataTreeView )
                        NTDTV = ( NTDataTreeView )control;
                }
                if ( NTDTV == null )
                    throw new DataException( "Could not find the Data Tree control" );
                if ( NTDTV.SelectedNode == null )
                    throw new NothingSelectedException( "No node selected!" );

                selectedNode = ( TreeNode )NTDTV.SelectedNode;

                if ( selectedNode.Tag == null ) {
                    NTDTV_editToolStripMenuItem_Click( null, null );
                }
            }
            catch { };
        }

        private void NTDTV_previewToolStripMenuItem_Click( object sender, EventArgs e ) {
            TreeNode selectedNode = null;
            NTDataTreeView NTDTV = null;
            foreach ( Control control in tabControl1.SelectedTab.Controls ) {
                if ( control is NTDataTreeView )
                    NTDTV = ( NTDataTreeView )control;
            }
            if ( NTDTV == null )
                throw new DataException( "Could not find the Data Tree control" );
            if ( NTDTV.SelectedNode == null )
                throw new NothingSelectedException( "No node selected!" );

            selectedNode = ( TreeNode )NTDTV.SelectedNode;

            ObjectPreview objpre = null;
            if ( selectedNode is NTTreeNode )
                if ( ( ( NTTreeNode )selectedNode ).NodeValue is IPrintable )
                    objpre = new ObjectPreview( ( ( NTTreeNode )selectedNode ).NodeValue );

            if ( objpre != null )
                try {
                    objpre.ShowDialog();
                }
                catch ( Exception ex ) {
                    //ErrDisp.ShowMsgDialog( ex );
                }//MessageBox.Show( ex.Message ); }
            else
                MessageBox.Show( "Cannot preview this object" );
        }

        void NTDTV_DataFile_EventDataStateChanged() {
            NTDataTreeView NTDTV = null;
            foreach ( Control control in tabControl1.SelectedTab.Controls ) {
                if ( control is NTDataTreeView )
                    NTDTV = ( NTDataTreeView )control;
            }
            tabControl1.SelectedTab.Text = String.Format( "{0}{1}", NTDTV.DataFile.DataFileName + '\n' + NTDTV.DataFile.FileName, NTDTV.DataFile.DataChanged ? "*" : "" );
        }

        void NTDTV_newToolStripMenuItem_Click( object sender, EventArgs e ) {
            try {
                NTDataFile currentData = workingData;
                Object selectedNode = null;
                NTDataTreeView NTDTV = null;
                foreach ( Control control in tabControl1.SelectedTab.Controls ) {
                    if ( control is NTDataTreeView )
                        NTDTV = ( NTDataTreeView )control;
                }
                if ( NTDTV == null )
                    throw new DataException( "Could not find the Data Tree control" );
                if ( NTDTV.SelectedNode == null )
                    throw new NothingSelectedException( "No node selected!" );

                if ( ( ( TreeNode )NTDTV.SelectedNode ).Tag != null )
                    selectedNode = ( ( TreeNode )NTDTV.SelectedNode ).Tag;
                if ( selectedNode == null )
                    selectedNode = ( ( NTTreeNode )NTDTV.SelectedNode ).NodeValue;

                List<string>
                    IDs = new List<string>();

                OCEditorBase
                    editor = null;

                foreach ( OCEditorBase ed in PluginEngine.GetEditorPlugIns() ) {
                    if ( ed.IEdit( ( ( ObjectClassBase )selectedNode ).MyType() ) && ed.Graphical ) {
                        editor = ed; break;
                    }
                }
                //todo make better
                if ( editor == null )
                    throw new Exception(
                        "I couldnt find an editor to go with this Object type," + Environment.NewLine +
                        "Please makesure that the plugin creator also created a GUI editor for this object." );

                editor.Collectors = loadedData.LoadedCollectors();

                editor.MyObject = ( ObjectClassBase )Activator.CreateInstance( ( ( ObjectClassBase )selectedNode ).MyType() );

                if ( editor.MyObject is INTId )
                    ( ( INTId )editor.MyObject ).ID = currentData.GenerateIDCode();

                if ( editor.MyObject is IOwner )
                    ( ( IOwner )editor.MyObject ).myOwner = currentData;
                else
                    throw new ArgumentException( String.Format( "The Object Calss type {0} is unable to recive an owner object and cannot be edited by this application.", editor.MyObject ) );

                switch ( editor.RunEditor( EditorMode.New ) ) {
                    case EditorExitCode.OK:
                        if ( editor.MyObject is IOwner )
                            ( ( IOwner )editor.MyObject ).myOwner = workingData;

                        workingData.Add( editor.MyObject );
                        break;
                    case EditorExitCode.Cancel:
                        break;
                    default:
                        break;
                }
            }
            catch ( NothingSelectedException ex ) {
                //ErrDisp.ShowMsgDialog( ex ); 
            }// MessageBox.Show( ex.Message ); }
            catch ( DataException ex ) { 
                //ErrDisp.ShowMsgDialog( ex ); 
            }// MessageBox.Show( ex.Message ); }
            catch ( Exception ex ) { 
                //ErrDisp.ShowMsgDialog( ex );
            }// MessageBox.Show( ex.Message ); }
        }

        void NTDTV_removeToolStripMenuItem_Click( object sender, EventArgs e ) {
            try {
                NTTreeNode selectedNode = null;
                NTDataTreeView NTDTV = null;
                foreach ( Control control in tabControl1.SelectedTab.Controls ) {
                    if ( control is NTDataTreeView )
                        NTDTV = ( NTDataTreeView )control;
                }
                if ( NTDTV == null )
                    throw new DataException( "Could not find the Data Tree control" );
                if ( NTDTV.SelectedNode == null )
                    throw new NothingSelectedException( "No node selected!" );
                if ( !( NTDTV.SelectedNode is NTTreeNode ) )
                    throw new NothingSelectedException( "No editable node selected!" );

                selectedNode = ( NTTreeNode )NTDTV.SelectedNode;

                if ( selectedNode.NodeValue is INTId ) NTDTV.DataFile.Drop( ( ObjectClassBase )selectedNode.NodeValue );
            }
            catch ( NothingSelectedException ex ) { 
                //ErrDisp.ShowMsgDialog( ex ); 
            }// MessageBox.Show( ex.Message ); }
            catch ( DataException ex ) { 
                //ErrDisp.ShowMsgDialog( ex ); 
            }// MessageBox.Show( ex.Message ); }
            catch ( Exception ex ) { 
                //ErrDisp.ShowMsgDialog( ex ); 
            }// MessageBox.Show( ex.Message ); }
        }

        void NTDTV_editToolStripMenuItem_Click( object sender, EventArgs e ) {
            try {
                NTDataFile currentData = workingData;
                Object selectedNode = null;
                NTDataTreeView NTDTV = null;
                foreach ( Control control in tabControl1.SelectedTab.Controls ) {
                    if ( control is NTDataTreeView )
                        NTDTV = ( NTDataTreeView )control;
                }
                if ( NTDTV == null )
                    throw new DataException( "Could not find the Data Tree control" );
                if ( NTDTV.SelectedNode == null )
                    throw new NothingSelectedException( "No node selected!" );

                if ( ( ( TreeNode )NTDTV.SelectedNode ).Tag != null )
                    selectedNode = ( ( TreeNode )NTDTV.SelectedNode ).Tag;
                if ( selectedNode == null && NTDTV.SelectedNode is NTTreeNode )
                    selectedNode = ( ( NTTreeNode )NTDTV.SelectedNode ).NodeValue;

                List<string>
                    IDs = new List<string>();

                OCEditorBase
                    editor = null;

                foreach ( OCEditorBase ed in PluginEngine.GetEditorPlugIns() ) {
                    if ( ed.IEdit( ( ( ObjectClassBase )selectedNode ).MyType() ) && ed.Graphical ) {
                        editor = ed; break;
                    }
                }

                //todo make better
                if ( editor == null )
                    throw new Exception();

                editor.Collectors = loadedData.LoadedCollectors();

                if ( selectedNode is ObjectClassBase )
                    editor.MyObject = ( ObjectClassBase )selectedNode;

                switch ( editor.RunEditor( EditorMode.Edit ) ) {
                    case EditorExitCode.OK:
                        workingData.Edit( (ObjectClassBase)selectedNode, (ObjectClassBase)editor.MyObject );
                        NTDTV.Data_AfterSelect( null, null );
                        break;
                    case EditorExitCode.Cancel:

                        break;
                    default:
                        break;
                }
            }
            catch ( NothingSelectedException ex ) { 
                //ErrDisp.ShowMsgDialog( ex );
            }// MessageBox.Show( ex.Message ); }
            catch ( DataException ex ) { 
                //ErrDisp.ShowMsgDialog( ex ); 
            }// MessageBox.Show( ex.Message ); }
            catch ( Exception ex ) {
                //ErrDisp.ShowMsgDialog( ex ); 
            }// MessageBox.Show( ex.Message ); }

        }

        void SaveToolStripMenuItemClick( object sender, EventArgs e ) {
            try {
                workingData.Save();
            }
            catch ( Exception ex ) { 
                //ErrDisp.ShowMsgDialog( ex );
            }// MessageBox.Show( ex.Message ); }
        }

        private void advancedEditorToolStripMenuItem_CheckedChanged( object sender, EventArgs e ) {
            Properties.Settings.Default.AdvancedEdit = advancedEditorToolStripMenuItem.Checked;
        }

        private void Main_FormClosing( object sender, FormClosingEventArgs e ) {
            Properties.Settings.Default.WindowLoc = this.Location;
            Properties.Settings.Default.WindowSize = this.Size;
            Properties.Settings.Default.Save();
        }

        private void printPreviewToolStripMenuItem_Click( object sender, EventArgs e ) {
            setUpPrint();
            PrintPreviewDialog pPreview = _PrintEngine.ShowPreview();
            pPreview.ShowDialog();
        }

        private void printToolStripMenuItem_Click( object sender, EventArgs e ) {
            PrintDialog pDialog = _PrintEngine.ShowPrintDialog();
            setUpPrint();
            if ( pDialog.ShowDialog() == DialogResult.OK ) {
                NTAF.PrintEngine.Properties.Settings.Default.printerSettings = pDialog.PrinterSettings;
                _PrintEngine.Print();
                NTAF.PrintEngine.Properties.Settings.Default.Save();
            }
        }

        public void setUpPrint() {
            List<IPrintable> printObjects = new List<IPrintable>();
            foreach ( Object obj in workingData.AllData )
                if ( obj is IPrintable ) {
                    printObjects.Add( ( IPrintable )obj );
                    printObjects.Add( new PrintBreakLine() );
                }

            _PrintEngine.ResetPrintables( printObjects.ToArray() );
        }

        private void pageSetupToolStripMenuItem_Click( object sender, EventArgs e ) {
            PageSetupDialog myEngineSettings = _PrintEngine.ShowPageSettings();
            setUpPrint();
            if ( myEngineSettings.ShowDialog() == DialogResult.OK ) {
                NTAF.PrintEngine.Properties.Settings.Default.printSettings = myEngineSettings.PageSettings;
                NTAF.PrintEngine.Properties.Settings.Default.Save();
            }
        }

        private void fontSettingsToolStripMenuItem_Click( object sender, EventArgs e ) {
            FontDialog myPrintFontSettings = _PrintEngine.ShowFontDialog();

            if ( myPrintFontSettings.ShowDialog() == DialogResult.OK ) {
                _PrintEngine.PrintFont = myPrintFontSettings.Font;
                NTAF.PrintEngine.Properties.Settings.Default.printFontSettings = myPrintFontSettings.Font;
                NTAF.PrintEngine.Properties.Settings.Default.Save();
            }

        }

        private void openToolStripMenuItem_Click( object sender, EventArgs e ) {
            //Stream thisStream = null;
            OpenFileDialog OFD = new OpenFileDialog();

            OFD.InitialDirectory = System.Environment.SpecialFolder.Desktop.ToString();
            OFD.Filter = "New Terra Data Files (*.NTX)|*.NTX";
            OFD.RestoreDirectory = true;
            OFD.Multiselect = true;

            switch ( OFD.ShowDialog() ) {
                case DialogResult.OK:
                    try {
                        List<System.IO.FileInfo> filesToLoad = new List<System.IO.FileInfo>();

                        Collection<string> unloadables = new Collection<string>();

                        foreach ( string str in OFD.FileNames )
                            filesToLoad.Add( new System.IO.FileInfo( str ) );

                        unloadables = loadedData.Load( filesToLoad.ToArray() );

                        if ( unloadables.Count != 0 ) {
                            string message = "";
                            foreach ( string str in unloadables )
                                message += str + "\n";
                            MessageBox.Show( message );
                        }
                    }
                    catch ( Exception ex ) {
                        //ErrDisp.ShowMsgDialog( ex );
                    }// MessageBox.Show( ex.Message ); }
                    break;
            }
        }

        void CloseTabToolStripMenuItemClick( object sender, EventArgs e ) {
            try {
                NTDataFile tmpfile = workingData;
                if ( workingData.DataChanged )
                    switch ( MessageBox.Show( String.Format( "The file {0} has changed, \n save changes?", workingData.FileName ),
                                           "Save changed Data?", MessageBoxButtons.YesNoCancel ) ) {
                        case DialogResult.Yes:
                            workingData.Save();
                            workingData.LockStatusChange -= new NTEventHandler( workingData_LockStatusChange );
                            loadedData.CloseFile( tmpfile );
                            tabControl1.SelectedTab.Dispose();
                            break;
                        case DialogResult.No:
                            loadedData.CloseFile( tmpfile );
                            tabControl1.SelectedTab.Dispose();
                            break;
                    }
                else {
                    switch ( MessageBox.Show( String.Format( "Close current Tab?", workingData.FileName ),
                                           "Close tab??", MessageBoxButtons.YesNo ) ) {
                        case DialogResult.Yes:
                            workingData.LockStatusChange -= new NTEventHandler( workingData_LockStatusChange );
                            loadedData.CloseFile( tmpfile );
                            tabControl1.SelectedTab.Dispose();
                            break;
                    }
                }
            }
            catch ( Exception ex ) {
                //ErrDisp.ShowMsgDialog( ex );
            }//MessageBox.Show( ex.Message ); }
        }

        void ExitToolStripMenuItemClick( object sender, EventArgs e ) {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click( object sender, EventArgs e ) {
            AboutBox1 abtbox = new AboutBox1();
            abtbox.ShowDialog();
        }

        private void userGuideToolStripMenuItem_Click( object sender, EventArgs e ) {
            MessageBox.Show( "No guide created yet, sorry\njust click around, youll get it", "sorry", MessageBoxButtons.OK );
        }

        private void newToolStripMenuItem_Click( object sender, EventArgs e ) {
            SaveFileDialog SFD = new SaveFileDialog();
            SFD.Filter = "New Terra Data Files (*.ntx)|*.ntx";
            SFD.RestoreDirectory = true;

            UserInputBox DataSetName = new UserInputBox( "OK", "Cancle", "Data Set Name", "Data Set Name" );
            UserInputBox UIB = new UserInputBox( "OK", "Cancle", "4 Digit alphanumeric File ID", "File ID" );

            DataSetName.EscapeNonAlphaNumChars = false;
            UIB.EscapeNonAlphaNumChars = true;

            if ( DataSetName.ShowDialog() == DialogResult.OK )
                if ( UIB.ShowDialog() == DialogResult.OK ) {
                    while ( UIB.UserInput.Length != 4 ) {
                        UIB.Message = String.Format( "{0} is not valid.\nData File ID must be 4 alphanumeric charictors Long", UIB.UserInput );
                        if ( UIB.ShowDialog() != DialogResult.OK )
                            return;
                    }

                    SFD.FileName = DataSetName.UserInput + ".ntx";

                    switch ( SFD.ShowDialog() ) {
                        case DialogResult.OK:
                            try {
                                loadedData.NewFile( SFD.FileName, UIB.UserInput, DataSetName.UserInput );
                            }
                            catch ( Exception ex ) { 
                                //ErrDisp.ShowMsgDialog( ex );
                            }// MessageBox.Show( ex.Message ); }
                            break;
                    }
                }
        }

        private void setPasswordToolStripMenuItem_Click( object sender, EventArgs e ) {
            if ( workingData.FileLocked || workingData.FilePassword != string.Empty ) {
                switch ( getPasswordFromUser().result ) {
                    case PasswordOutcome.Passed:
                        setpassword();
                        break;
                    case PasswordOutcome.Failed:
                        MessageBox.Show( "Password incorrect" );
                        break;
                }
            }
            else setpassword();
        }

        private bool setpassword() {
            bool retVal = false;
            UserInputBox InputBoxA = new UserInputBox( "Set", "Cancle", "Please enter a password for this file", "Set Password...", true );
            UserInputBox InputBoxB = new UserInputBox( "Set", "Cancle", "Please re-enter the password for this file", "Password confirm...", true );
            switch ( InputBoxA.ShowDialog() ) {
                case DialogResult.OK:
                    switch ( InputBoxB.ShowDialog() ) {
                        case DialogResult.OK:
                            if ( InputBoxA.UserInput != InputBoxB.UserInput ) {
                                MessageBox.Show( "Passwords do not match!" );
                                retVal = false;
                            }
                            else {
                                try {
                                    workingData.FilePassword = InputBoxA.UserInput;
                                    retVal = true;
                                }
                                catch ( Exception ex ) {
                                    //ErrDisp.ShowMsgDialog( ex );
                                }//MessageBox.Show( ex.Message ); }
                            }
                            break;
                    }
                    break;
            }
            return retVal;
        }

        private void clearPasswordToolStripMenuItem_Click( object sender, EventArgs e ) {
            try {
                if ( workingData.FileLocked || workingData.FilePassword != string.Empty ) {
                    SecInfo secInfo = getPasswordFromUser();
                    switch ( secInfo.result ) {
                        case PasswordOutcome.Passed:
                            workingData.FilePassword = "";
                            break;
                        case PasswordOutcome.Failed:
                            MessageBox.Show( "Password incorrect" );
                            break;
                    }
                }
            }
            catch ( Exception ex ) {
                //ErrDisp.ShowMsgDialog( ex ); 
            }// MessageBox.Show( ex.Message ); }
        }

        private enum PasswordOutcome {
            Passed = 1,
            Failed = 0,
            Cancel = -1
        }

        private class SecInfo {
            public PasswordOutcome result;
            public string enteredPassword;

            public SecInfo() {
                result = PasswordOutcome.Failed;
                enteredPassword = "";
            }
        }

        private SecInfo getPasswordFromUser() {
            SecInfo retVal = new SecInfo();
            UserInputBox InputBox = new UserInputBox( "OK", "Cancle", "Please enter the files current password", "Enter Password...", true );
            switch ( InputBox.ShowDialog() ) {
                case DialogResult.OK:
                    retVal.enteredPassword = InputBox.UserInput;
                    retVal.result = workingData.CheckPassword( InputBox.UserInput ) ? PasswordOutcome.Passed : PasswordOutcome.Failed;
                    break;
                case DialogResult.Cancel:
                    retVal.result = PasswordOutcome.Cancel;
                    break;
            }
            return retVal;
        }

        private void FileLockedToolStripMenuItem_Click( object sender, EventArgs e ) {
            if ( workingData.FileLocked ) {//file is locked and needs to be unlocked
                SecInfo secInfo = getPasswordFromUser();
                switch ( secInfo.result ) {
                    case PasswordOutcome.Passed:
                        workingData.UnLockFile( secInfo.enteredPassword );
                        break;
                    case PasswordOutcome.Failed:
                        MessageBox.Show( "File could not be unlocked, password incorrect." );
                        break;
                }
            }
            else {//file is unlocked and is being locked
                try {
                    workingData.LockFile();
                }
                catch ( NullPasswordException ) {//file has no passowrd ask to set one
                    if ( setpassword() )
                        try {
                            workingData.LockFile();
                        }
                        catch ( Exception EX ) {
                            //ErrDisp.ShowMsgDialog( EX );
                            //MessageBox.Show( EX.Message );
                        }
                }
            }
        }

        private void tabControl1_SelectedIndexChanged( object sender, EventArgs e ) {

        }

        #region Socketing
        TcpListener listenSocket = new TcpListener( new IPAddress( new byte[] { 127, 0, 0, 1 } ), 44444 );
        TcpClient sendingAppSocket = default( TcpClient );

        public void CommandListener() {
            //start up the listener in the main app
            listenSocket.Start();

            //really just for my debugging isnt really neaded
            Console.WriteLine( "TCP Listener Started" );

            //continuously listen for new commands being sent
            while ( ( true ) ) {
                //encase in a try so when app is closing it doesnt crash
                try {
                    //when a client connects attach to sending app socket
                    sendingAppSocket = listenSocket.AcceptTcpClient();

                    //setup buffers
                    byte[] bytesFrom = new byte[10025];
                    string dataFromClient = null;

                    //get the network stream
                    NetworkStream NS = sendingAppSocket.GetStream();

                    //read network buffer
                    NS.Read( bytesFrom, 0, ( int )sendingAppSocket.ReceiveBufferSize );

                    //decode bytes to a string
                    dataFromClient = System.Text.Encoding.ASCII.GetString( bytesFrom );

                    //trimoff excess garbage data
                    dataFromClient = dataFromClient.TrimEnd( '\0' );

                    //flush the stream
                    NS.Flush();

                    List<string> filesToOpen = new List<string>();
                    List<string> operation = new List<string>();

                    //operation to split the command suffexed by the selected delimiter
                    //in this example it was the exlimation(!)
                    operation.AddRange( dataFromClient.Split( '!' ) );
                    //get the file path strings and store them in an array
                    if ( operation.Count >= 2 )
                        filesToOpen.AddRange( operation[1].Split( ';' ) );

                    //clear byte buffer
                    byte[] returnBytes = null;

                    //create the command to tell the other app it can close
                    returnBytes = Encoding.ASCII.GetBytes( "finished" );

                    //send the return command
                    NS.Write( returnBytes, 0, returnBytes.Length );

                    //clear the buffer
                    NS.Flush();

                    //close the network stream
                    NS.Close();

                    //make the sending app socket collectable by the garbage collector
                    sendingAppSocket = null;

                    //add code here to load files
                    //i typically creat an object libary or object that handles all data operations outside my app
                    //so it can be ported to other avenues just makesure that you use a lock or some other
                    //threadding control mech so your data doesnt get mangled

                    List<FileInfo> flenfo = new List<FileInfo>();
                    if ( operation[0] == "Open" ) {
                        foreach ( String str in filesToOpen )
                            flenfo.Add( new FileInfo( str ) );

                        loadedData.Load( flenfo.ToArray() );

                    }

                }
                //fornow generic catch will be utilized unless new routeens are added that can fail without handeling their own errors
                catch { }
            }
        }

        public void StopListeners() {
            if ( sendingAppSocket != null )
                //close the app socket if its in use a try may be inorder so you dont kill your program i havent had any problems yet thoe
                sendingAppSocket.Close();
            //stop the listening socket
            listenSocket.Stop();
        } 
        #endregion

        static bool IsAdmin() {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal( identity );
            return principal.IsInRole( WindowsBuiltInRole.Administrator );

        }

        //private void installUpdateFileAssociationsToolStripMenuItem1_Click( object sender, EventArgs e ) {
        //    try {
        //        if ( IsAdmin() ) {
        //            String ProgID = "NTDataManager";
        //            FileExtentionKey ntxFileType = new FileExtentionKey( "ntx" );
        //            ProgramID ntxProgID = new ProgramID( ProgID );

        //            ntxFileType.OpenWithProgID = ProgID;
        //            ntxFileType.PerceivedType = "text";
        //            ntxFileType.ContentType = "text/plain";

        //            List<RegistryKeyValue> owpids = new List<RegistryKeyValue>();
        //            owpids.Add( new RegistryKeyValue( ntxFileType.OpenWithKey, "txtfile" ) );
        //            owpids.Add( new RegistryKeyValue( ntxFileType.OpenWithKey, "CompressedFolder" ) );


        //            ntxFileType.OpenWithProgids = owpids.ToArray();

        //            ntxProgID.DefaultIconLocation = Application.StartupPath + "\\Resources\\File.ico";

        //            ntxProgID.DocumentNameType = "New Terra Data File";

        //            ntxProgID.ShellOpenCommand = Application.ExecutablePath + " %1";

        //            MessageBox.Show( "Updated", "You are privileged...", MessageBoxButtons.OK );
        //        }
        //        else {
        //            MessageBox.Show( "The Data Manager needs to be run by a user with administrator privileges, install/update cancled", "You are not privileged...", MessageBoxButtons.OK );
        //        }
        //    }
        //    catch ( Exception ex ) { ErrDisp.ShowMsgDialog( ex ); }// MessageBox.Show( ex.Message, "CRAP!!!", MessageBoxButtons.OK ); }
        //}

        private void undoToolStripMenuItem_Click( object sender, EventArgs e ) {
            try {
                workingData.DoUndo();
            }
            catch ( Exception ex ) { 
                //ErrDisp.ShowMsgDialog( ex );
            }// MessageBox.Show( ex.Message ); }
        }

        private void redoToolStripMenuItem_Click( object sender, EventArgs e ) {
            try {
                workingData.DoRedo();

            }
            catch ( Exception ex ) {
                //ErrDisp.ShowMsgDialog( ex );
            }// MessageBox.Show( ex.Message ); }
        }

        private void copyToolStripMenuItem_Click( object sender, EventArgs e ) {
            try {
                NTDataTreeView NTDTV = null;
                foreach ( Control control in tabControl1.SelectedTab.Controls ) {
                    if ( control is NTDataTreeView )
                        NTDTV = ( NTDataTreeView )control;
                }
                if ( NTDTV == null )
                    throw new DataException( "Could not find the Data Tree control" );
                if ( NTDTV.SelectedNode == null )
                    throw new NothingSelectedException( "No node selected!" );

                if ( NTDTV.SelectedNode is NTTreeNode )
                    if ( ( ( NTTreeNode )NTDTV.SelectedNode ).NodeValue is INTId ) {
                        INTId NodeValue = ( INTId )( ( NTTreeNode )NTDTV.SelectedNode ).NodeValue;

                        CopyClip.CopyToClipboard( NodeValue );
                    }//NodeValue.GetType(), 
            }
            catch { }
        }

        private void pasteToolStripMenuItem_Click( object sender, EventArgs e ) {
            CopyClip.CopyFromClipboard( workingData );
        }

        private void loadedModulesToolStripMenuItem_Click( object sender, EventArgs e ) {
            LoadedModuleBox
                lmb = new LoadedModuleBox();

            lmb.ShowDialog();
        }
        private Boolean
            resizing = false;
        private void Main_ResizeBegin( object sender, EventArgs e ) {
            //this.SuspendLayout( );
            resizing = true;

        }

        private void Main_Resize( object sender, EventArgs e ) {
            memGraphics.CreateDoubleBuffer( this.CreateGraphics(), this.ClientRectangle.Width, this.ClientRectangle.Height );
            Invalidate(); // Force a repaint after has been resized 
        }

        private void Main_ResizeEnd( object sender, EventArgs e ) {
            resizing = false;

            this.Invalidate();
        }

        #region Graphics buffering
        private DBGraphics
            memGraphics;

        protected override void OnPaintBackground( PaintEventArgs e ) { }

        protected override void OnPaint( System.Windows.Forms.PaintEventArgs e ) {

            if ( memGraphics.CanDoubleBuffer() ) {
                // Fill in Background (for effieciency only the area that has been clipped)

                memGraphics.g.FillRectangle( Brushes.Black, e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.Width, e.ClipRectangle.Height );

                // Do our drawing using memGraphics.g instead e.Graphics
                if ( !resizing )
                    memGraphics.g.DrawImage( Resources.effekt1, memGraphics.g.VisibleClipBounds );
                // Render to the form

                memGraphics.Render( e.Graphics );
            }

            MainStatusLabel.Width = statusStrip1.Width - 238;
        }
        #endregion

        private void Main_Paint( object sender, PaintEventArgs e ) {

        }
    }
}