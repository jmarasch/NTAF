using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Ribbon;
using System.Windows.Forms;
using NTAF.Core;
using NTAF.PlugInFramework;
using NTAF.PlugInFramework.OrphanControls;
using NTAF.PrintEngine;
using System.ComponentModel;
using PE = NTAF.PlugInFramework.PluginEngine;

namespace ProtoGears_World_Builder {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow {
        private List<NTDataFile>
            DataFiles = new List<NTDataFile>(),
            LoadCache = new List<NTDataFile>();

        //TreeNode rootNode = new TreeNode("OpenDataFiles");

        private BackgroundWorker
            bgw = new BackgroundWorker();

        //private PrintEngine
        //    _PrintEngine = null;

        private NTDataFile DataFile {
            get {
                try {
                    string datafileName = CurrentDataFileNode.Header.ToString().Split('*')[0];

                    NTDataFile retVal = DataFiles.First(df => df.FileName == datafileName);

                    return retVal;
                    } catch (Exception ex) {
                    return null;
                    }
                }
            }
        private TreeViewItem CurrentDataFileNode {
            get {
                try {
                    TreeViewItem selectedNode = (TreeViewItem)DataView.SelectedItem;

                    while (selectedNode.Parent != null) {
                        selectedNode = (TreeViewItem)selectedNode.Parent;
                        }

                    return selectedNode;
                    } catch (Exception ex) {
                    return null;
                    }
                }
            }

        public MainWindow() {
            InitializeComponent();
            bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);
            bgw.ProgressChanged += new ProgressChangedEventHandler(bgw_ProgressChanged);
            bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);
            bgw.WorkerReportsProgress = true;
            bgw.WorkerSupportsCancellation = true;

            TreeViewItem
                root = new TreeViewItem { Header = "Root" };
            TreeNode
                child1 = new TreeNode { Text = "Child1" },
                child2 = new TreeNode { Text = "Child2" },
                child3 = new TreeNode { Text = "Child3" },
                child4 = new TreeNode { Text = "Child4" };

            root.Items.Add(child1);
            root.Items.Add(child2);
            child2.Nodes.Add(child3);
            root.Items.Add(child4);

            DataView.Items.Add(root);
            }
        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            //UpdateProgressLabel1.Text = "Ready...";
            //UpdateProgressBar1.Value = 0;

            foreach (NTDataFile dataFile in LoadCache) {
                dataFile.getTreeNodes(DataView.Items, FileNodeMenuStrip, OCCMenuStrip, OCMenuStrip, OrphanRootMenuStrip, OrphanMenuStrip);
                DataFiles.Add(dataFile);
                }
            LoadCache.Clear();
            //UpdateProgressBar1.Visible = false;
            }

        private void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            //if (!UpdateProgressBar1.Visible)
            //    UpdateProgressBar1.Visible = true;
            //UpdateProgressEventArgs args = e.UserState as UpdateProgressEventArgs;
            //UpdateProgressBar1.Maximum = args.NumberOfItems;
            //UpdateProgressBar1.Value = args.current;
            //UpdateProgressLabel1.Text = args.ProcessingMessage;
            //this.statusStrip1.Invalidate(true);
            }

        private void bgw_DoWork(object sender, DoWorkEventArgs e) {
            //todo:: the only way its going to get faster is if linking is done on demand when an object is used
            foreach (NTDataFile dataFile in LoadCache) {
                DateTime fileloadStart = DateTime.Now;
                dataFile.Load3();
                DateTime fileloadFinish = DateTime.Now;
                Console.WriteLine(String.Format("Load Method 1:{0}", (fileloadFinish - fileloadStart)));
                }
            }

        private void DataFile_Updating(UpdaterEventArgs args) {
            //UpdateProgressBar.Value = 0;
            //UpdateProgressBar.Minimum = 0;
            //UpdateProgressBar.Maximum = args.NumberOfItems;
            }

        private void DataFile_Update(UpdateProgressEventArgs args) {
            bgw.ReportProgress(args.Percent, args);
            }

        private void DataFile_Updated() {
            }

        private void OpenFile(string[] files) {

            }

        private void DataView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {

            var a = DataView.SelectedItem;
            //lblDescription.Content = ((TreeViewItem)DataView.SelectedItem).Header.ToString();
            }

        private void OpenFile_Click(object sender, RoutedEventArgs e) {
            try {
                if (CheckForSave()) {
                    // Create OpenFileDialog
                    Microsoft.Win32.OpenFileDialog OFD = new Microsoft.Win32.OpenFileDialog();

                    // Set filter for file extension and default file extension
                    OFD.Filter = "NewTerra Data Files (*.ntx)|*.ntx";
                    OFD.Multiselect = false;

                    if (OFD.ShowDialog() == true) {
                        NTDataFile fileToLoad = new NTDataFile(OFD.FileName);

                        FileEventSubscriptions(fileToLoad, false);

                        LoadCache.Add(fileToLoad);

                        bgw.RunWorkerAsync();
                        }

                    //System.Windows.Forms.OpenFileDialog OFD = new System.Windows.Forms.OpenFileDialog();
                    //OFD.Filter = "NewTerra Data Files (*.ntx)|*.ntx";
                    ////OFD.SupportMultiDottedExtensions = true;
                    //OFD.Multiselect = false;
                    //if (OFD.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                    //    //DataView.Nodes.Clear();
                    //    NTDataFile fileToLoad = new NTDataFile(OFD.FileName);

                    //    FileEventSubscriptions(fileToLoad, false);

                    //    LoadCache.Add(fileToLoad);

                    //    bgw.RunWorkerAsync();
                    //}
                    }
                } catch (Exception ex) {
                //todo need exception message box
                }
            }
        private bool CheckForSave() {
            //foreach (NTDataFile df in DataFiles) {
            //    if (df.DataChanged) {
            //        switch (MessageBox.Show(String.Format("{0} has changed since it was opened would you like to save it now?", df.FileName), "Save file?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1)) {
            //            case DialogResult.Yes:
            //                df.Save();
            //                break;

            //            case DialogResult.Cancel:
            //                break;
            //        }
            //    }
            //}
            return true;
            }
        private void FileEventSubscriptions(NTDataFile file, bool UnSubscribe) {
            if (!UnSubscribe) {
                file.Updating += new NTEventHandler<UpdaterEventArgs>(DataFile_Updating);
                file.Update += new NTEventHandler<UpdateProgressEventArgs>(DataFile_Update);
                file.Updated += new NTEventHandler(DataFile_Updated);
                file.EventDataStateChanged += new NTEventHandler(FileDataState_Change);
                file.LockStatusChange += new NTEventHandler(DataFile_LockStatusChange);
                } else {
                file.Updating -= DataFile_Updating;
                file.Update -= DataFile_Update;
                file.Updated -= DataFile_Updated;
                file.EventDataStateChanged -= FileDataState_Change;
                file.LockStatusChange -= DataFile_LockStatusChange;
                }
            }
        private void FileDataState_Change() {
            try {
                //TreeNode tn = CurrentDataFileNode;
                //if (tn == null) return;
                //System.Drawing.Font fnt = tn.NodeFont;
                //if (DataFile.DataChanged) {
                //    tn.Text = DataFile.FileName + "*";
                //    tn.ForeColor = Color.White;
                //    tn.BackColor = Color.Green;
                //    //tn.NodeFont = new Font(DataView.Font, FontStyle.Bold);
                //} else {
                //    tn.Text = DataFile.FileName;
                //    tn.ForeColor = Color.Black;
                //    tn.BackColor = Color.Empty;
                //    //tn.NodeFont = new Font(tn.NodeFont, FontStyle.Regular);
                //}
                } catch (Exception ex) {
                //Microsoft.SqlServer.MessageBox.ExceptionMessageBox emb = new Microsoft.SqlServer.MessageBox.ExceptionMessageBox(ex);
                //emb.Show(this);
                ////throw;
                }
            }
        private void DataFile_LockStatusChange() {
            try {
                //TreeNode tn = CurrentDataFileNode;
                //if (tn == null) return;
                //if (DataFile.FileLocked) {
                //    tn.ForeColor = Color.White;
                //    tn.BackColor = Color.PaleVioletRed;
                //} else {
                //    tn.ForeColor = Color.Black;
                //    tn.BackColor = Color.Empty;
                //}
                } catch (Exception ex) {
                //Microsoft.SqlServer.MessageBox.ExceptionMessageBox emb = new Microsoft.SqlServer.MessageBox.ExceptionMessageBox(ex);
                //emb.Show(this);
                ////throw;
                }
            }
        }
    }