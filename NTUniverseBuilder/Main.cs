using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using NTAF.Core;
using NTAF.PlugInFramework;
using NTAF.PrintEngine;
using System.Collections.Generic;

namespace NTAF.UniverseBuilder.WinGui {

    public partial class Main : Form {
        NTDataFile
            DataFile = new NTDataFile();

        BackgroundWorker
            bgw = new BackgroundWorker();

        TreeNode
            Orphans = new TreeNode( "Orpahned Objects" );

        NTAF.PrintEngine.PrintEngine 
            _PrintEngine = null;

        public Main() {
            InitializeComponent();

            BuildMenu();

            newToolStripMenuItem_Click( null, null );

            //bgw.RunWorkerAsync();

            bgw.DoWork += new DoWorkEventHandler( bgw_DoWork );

            bgw.ProgressChanged += new ProgressChangedEventHandler( bgw_ProgressChanged );

            bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler( bgw_RunWorkerCompleted );

            bgw.WorkerReportsProgress = true;

            bgw.WorkerSupportsCancellation = true;

            DataFile.EventOrphansChanged += new NTEventHandler<ItemChangedArgs>( DataFile_EventOrphansChanged );

            DataFile.LockStatusChange += new NTEventHandler( DataFile_LockStatusChange );
            //DataFile = new NTDataFile(
        }

        void DataFile_LockStatusChange() {
            lockFileToolStripMenuItem.Checked = DataFile.FileLocked;
            //FileLockIndicator.Checked = DataFile.FileLocked;
        }

        void DataFile_EventOrphansChanged( ItemChangedArgs args ) {
            if ( args.Action == ArgAction.Add ) {
                Orphans.Nodes.Insert( args.Index, new OrphanNode( ( ObjectClassBase )args.Item ) );
            }

            if ( args.Action == ArgAction.Remove ) {
                foreach ( OrphanNode OrpNode in Orphans.Nodes )
                    if ( OrpNode.ObjectClass == args.Item ) {
                        OrpNode.Remove();
                        break;
                    }
            }
        }

        void bgw_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {
            UpdateProgressLabel.Text = "Ready...";
            UpdateProgressBar.Value = 0;



            DataFile.getTreeNodes( DataView.Nodes, OCCMenuStrip, OCMenuStrip );
            DataView.Nodes.Add( Orphans );

            UpdateProgressBar.Visible = false;

        }

        void bgw_ProgressChanged( object sender, ProgressChangedEventArgs e ) {
            if ( !UpdateProgressBar.Visible )
                UpdateProgressBar.Visible = true;
            UpdateProgressEventArgs args = e.UserState as UpdateProgressEventArgs;
            UpdateProgressBar.Maximum = args.NumberOfItems;
            UpdateProgressBar.Value = args.current;
            UpdateProgressLabel.Text = args.ProcessingMessage;
            this.statusStrip1.Invalidate( true );
        }

        void bgw_DoWork( object sender, DoWorkEventArgs e ) {
            DataFile.Load();
            DataFile.LinkData();
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

        private String Title {
            get { return this.Text; }
            set {
                if ( value == "" | value == null )
                    this.Text = "New Terra Universe Builder - New Data File";
                else
                    this.Text = "New Terra Universe Builder - " + value;
            }
        }

        private void DataView_AfterSelect( object sender, TreeViewEventArgs e ) {
            //clear last selection of nodes
            comboBox1.Items.Clear();

            if ( !( DataView.SelectedNode is OCCNode ) & !( DataView.SelectedNode is OCNode ) &
                !( DataView.SelectedNode is OrphanNode ) & DataView.SelectedNode != Orphans ) {
                //basic node that should contain nodes of OCCNodes
                foreach ( OCCNode occn in DataView.SelectedNode.Nodes ) {
                    foreach ( OCNode ocn in occn.Nodes ) {
                        comboBox1.Items.Add( ocn.ObjectClass );
                    }
                }
            }
            if ( DataView.SelectedNode is OCCNode ) {
                foreach ( OCNode ocn in DataView.SelectedNode.Nodes ) {
                    comboBox1.Items.Add( ocn.ObjectClass );
                }
            }
            if ( DataView.SelectedNode is OCNode ) {
                comboBox1.Items.Add( ( ( OCNode )DataView.SelectedNode ).ObjectClass );
            }

            if ( comboBox1.Items.Count >= 1 )
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
                _PrintEngine = new NTAF.PrintEngine.PrintEngine( DataFile.FileName );

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




    }
}
