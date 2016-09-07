//using System.Windows.Controls.Ribbon;
using Fluent;
using NTAF.Core;
using NTAF.PlugInFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ProtoGears_World_Builder {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow {

        #region Fields

        //TreeNode rootNode = new TreeNode("OpenDataFiles");
        private BackgroundWorker
            bgw = new BackgroundWorker();

        private List<NTDataFile>
                    DataFiles = new List<NTDataFile>(),
            LoadCache = new List<NTDataFile>();

        #endregion Fields

        //private PrintEngine
        //    _PrintEngine = null;

        #region Constructors

        public MainWindow() {
            InitializeComponent();
            bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);
            bgw.ProgressChanged += new ProgressChangedEventHandler(bgw_ProgressChanged);
            bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);
            bgw.WorkerReportsProgress = true;
            bgw.WorkerSupportsCancellation = true;

            string[] recentFiles = Properties.Settings.Default.RecentFiles.Split(';');

            foreach (string sFile in recentFiles) {
                //RecentFileItem menuItem = new RecentFileItem(sFile);
                Fluent.MenuItem menuItem = new Fluent.MenuItem {
                    Header = System.IO.Path.GetFileName(sFile),
                    ToolTip = sFile,
                    Icon = "Images/Icons/File.ico"
                    };
                menuItem.Click += RecentFile_Click;
                RecentFileControl.Items.Add(menuItem);
                }

            //TreeViewItem
            //    root = new TreeViewItem { Header = "Root" };
            //TreeNode
            //    child1 = new TreeNode { Text = "Child1" },
            //    child2 = new TreeNode { Text = "Child2" },
            //    child3 = new TreeNode { Text = "Child3" },
            //    child4 = new TreeNode { Text = "Child4" };

            //root.Items.Add(child1);
            //root.Items.Add(child2);
            //child2.Nodes.Add(child3);
            //root.Items.Add(child4);

            //DataView.Items.Add(root);
            }

        #endregion Constructors

        #region Properties

        private TreeViewItem CurrentDataFileNode {
            get {
                try {
                    TreeViewItem selectedNode = (TreeViewItem)DataView.SelectedItem;

                    while (selectedNode.Parent is TreeViewItem) {
                        selectedNode = (TreeViewItem)selectedNode.Parent;
                        }

                    return selectedNode;
                    } catch (Exception ex) {
                    return null;
                    }
                }
            }

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

        #endregion Properties

        #region Methods

        private void bgw_DoWork(object sender, DoWorkEventArgs e) {
            //todo:: the only way its going to get faster is if linking is done on demand when an object is used
            foreach (NTDataFile dataFile in LoadCache) {
                DateTime fileloadStart = DateTime.Now;
                FileEventSubscriptions(dataFile, false);
                dataFile.Load3();
                DateTime fileloadFinish = DateTime.Now;
                Console.WriteLine(String.Format("Load Method 1:{0}", (fileloadFinish - fileloadStart)));
                }
            }

        private void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            if (UpdateProgressBar.Visibility == Visibility.Collapsed)
                UpdateProgressBar.Visibility = Visibility.Visible;
            UpdateProgressEventArgs args = e.UserState as UpdateProgressEventArgs;
            UpdateProgressBar.Maximum = args.NumberOfItems;
            UpdateProgressBar.Value = args.current;
            UpdateProgressLabel.Value = args.ProcessingMessage;
            this.StatusStrip.InvalidateVisual();
            }

        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            //UpdateProgressLabel1.Text = "Ready...";
            //UpdateProgressBar1.Value = 0;

            NTDataTreeNode dataTree;

            foreach (NTDataFile dataFile in LoadCache) {
                //dataTree = new NTDataTreeNode();
                dataTree = dataFile.GetDataTree();
                DataView.Items.Add(ConvertTree(dataTree));
                foreach (OCCBase item in dataFile.Collectors) {
                    item.TreeDataChanged += Item_TreeDataChanged;
                    }
                DataFiles.Add(dataFile);
                }
            LoadCache.Clear();
            UpdateProgressBar.Visibility = Visibility.Collapsed;
            UpdateProgressLabel.Value = "Ready...";
            }

        private TreeViewItem BuildSubNodes(NTDataTreeNode node) {
            TreeViewItemExtention tvi = new TreeViewItemExtention {
                Header = node.Text,
                Tag = node.ObjectID,
                NodeType = ConvertNodeType(node.NodeType)
                };

            if (node.Nodes.Count >= 1) {
                foreach (NTDataTreeNode item in node.Nodes) {
                    tvi.Items.Add(BuildSubNodes(item));
                    }
                }
            return tvi;
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

        /// <summary>
        /// Converts from generic tree data node type to this forms node type
        /// </summary>
        /// <param name="orig">original data's enum</param>
        /// <returns>enum value, defaults to other if it cant figure it out</returns>
        private TreeViewItemExtention.NodeTypeEnum ConvertNodeType(NTDataTreeNode.NodeTypeEnum orig) {
            switch (orig) {
                case NTDataTreeNode.NodeTypeEnum.DataRoot:
                    return TreeViewItemExtention.NodeTypeEnum.DataRoot;

                case NTDataTreeNode.NodeTypeEnum.ObjectCollector:
                    return TreeViewItemExtention.NodeTypeEnum.ObjectCollector;

                case NTDataTreeNode.NodeTypeEnum.Object:
                    return TreeViewItemExtention.NodeTypeEnum.Object;
                }
            return TreeViewItemExtention.NodeTypeEnum.Other;
            }

        private TreeViewItemExtention ConvertTree(NTDataTreeNode tree) {
            //iterate thru all root nodes untill the one contatin the data file is found then clear it and readd it,
            //if this is a new node then add the file node and populate children,
            //todo: need to create a way to update specific nodes and such
            TreeViewItemExtention retval = new TreeViewItemExtention {
                Header = tree.Text,
                Tag = tree.ObjectID,
                NodeType = ConvertNodeType(tree.NodeType)
                };

            if (tree.Nodes.Count >= 1) {
                foreach (NTDataTreeNode item in tree.Nodes) {
                    retval.Items.Add(BuildSubNodes(item));
                    }
                }
            return retval;
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

        private void DataFile_Update(UpdateProgressEventArgs args) {
            bgw.ReportProgress(args.Percent, args);
            }

        private void DataFile_Updated() {
            }

        private void DataFile_Updating(UpdaterEventArgs args) {
            //UpdateProgressBar.Value = 0;
            //UpdateProgressBar.Minimum = 0;
            //UpdateProgressBar.Maximum = args.NumberOfItems;
            }

        private void DataItemSelector_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (DataItemSelector.SelectedIndex == -1)
                ObjectViewer.SelectedObject = null;
            else {
                ComboBoxItem cbiSelected = (ComboBoxItem)DataItemSelector.SelectedItem;
                string objectID = cbiSelected.Tag.ToString();
                object selectedObject = DataFile.FindObject(objectID);

                ObjectViewer.SelectedObject = selectedObject;
                }
            }

        private void DataView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
            //var a = DataView.SelectedItem;

            ////clear last selection of nodes
            //DataItemSelector.Items.Clear();

            TreeViewItemExtention selectedItem = DataView.SelectedItem as TreeViewItemExtention;

            if (selectedItem != null) {
                DataItemSelector.Items.Clear();
                if (selectedItem.NodeType != TreeViewItemExtention.NodeTypeEnum.Object) {
                    foreach (TreeViewItemExtention item in selectedItem.AllMyChildren()) {
                        DataItemSelector.Items.Add(new ComboBoxItem {
                            Content = item.Header,
                            Tag = item.Tag
                            });
                        }
                    } else {
                    DataItemSelector.Items.Add(new ComboBoxItem {
                        Content = selectedItem.Header,
                        Tag = selectedItem.Tag
                        });
                    }
                BooleanToVisibilityConverter converter = new BooleanToVisibilityConverter();
                tabDataGroup.Visibility = (Visibility)converter.Convert(selectedItem.NodeType == TreeViewItemExtention.NodeTypeEnum.DataRoot, null, null, null);
                //if() {
                //        tabDataGroup.Visibility = Visibility.Visible;
                //    } else {
                //    tabDataGroup.Visibility = Visibility.Collapsed;
                //    }

                switch (selectedItem.NodeType) {
                    case TreeViewItemExtention.NodeTypeEnum.DataRoot:
                        //tabDataGroup.Visibility = Visibility.Visible;
                        //if (DataView.SelectedNode is NTDataNode) {
                        //    if (DataFile.FileLocked) {
                        //        editLockToolStripMenuItem.Text = "UnLock File";
                        //        } else {
                        //        editLockToolStripMenuItem.Text = "Lock File";
                        //        }
                        //    }
                        break;

                    case TreeViewItemExtention.NodeTypeEnum.ObjectCollector:
                        //basic node that should contain nodes of OCCNodes
                        //if (selectedItem.HasItems) {
                        //    }
                        //foreach (TreeViewItemExtention occn in selectedItem.Ite .Nodes) {
                        //    foreach (OCNode ocn in occn.Nodes) {
                        //        comboBox1.Items.Add(ocn.ObjectClass);
                        //        }
                        //    }

                        //if (DataView.SelectedNode is OCCNode) {
                        //    foreach (OCNode ocn in DataView.SelectedNode.Nodes) {
                        //        comboBox1.Items.Add(ocn.ObjectClass);
                        //        }
                        //    }
                        break;

                    case TreeViewItemExtention.NodeTypeEnum.Object:
                        //if (DataView.SelectedNode is OCNode) {
                        //    comboBox1.Items.Add(((OCNode)DataView.SelectedNode).ObjectClass);
                        //    }
                        break;

                    case TreeViewItemExtention.NodeTypeEnum.Other:

                        break;

                    default:

                        break;
                    }
                }
            if (DataItemSelector.Items.Count >= 1)
                DataItemSelector.SelectedIndex = 0;
            else {
                DataItemSelector.Text = "";
                ObjectViewer.SelectedObject = null;
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

        private void fileOpen_Click(object sender, RoutedEventArgs e) {
            //try {
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

                    Fluent.MenuItem rfi = new Fluent.MenuItem {
                        Header = System.IO.Path.GetFileName(OFD.FileName),
                        ToolTip = OFD.FileName,
                        Icon = "Images/Icons/File.ico"
                        };

                    UpdateRecentFiles(rfi);
                    }
                }
            }

        private void Item_TreeDataChanged(CollectionChanged args) {
            NTDataFile fileref = (NTDataFile)args.Sender.Owner;
            OCCBase collectorRef = args.Sender;
            int nodeindex;
            foreach (TreeViewItem item in DataView.Items) {
                if ((string)item.Header == fileref.FileName) {
                    foreach (TreeViewItem node in item.Items) {
                        if ((string)node.Header == collectorRef.CollectionName) {
                            nodeindex = item.Items.IndexOf(node);
                            //node.Items.Clear();
                            item.Items.Remove(node);
                            item.Items.Insert(nodeindex, BuildSubNodes(collectorRef.TreeData));
                            }
                        }
                    }
                }
            }

        private void mnuExit_Click(object sender, RoutedEventArgs e) {
            this.Close();
            }

        private void OpenFile(string[] files) {
            }

        //private TreeViewItemExtention[] selectedObjectSubNodes = new TreeViewItemExtention[0];
        private void OpenFile_Click(object sender, RoutedEventArgs e) {
            }

        private void RecentFile_Click(object sender, RoutedEventArgs e) {
            if (bgw.IsBusy) {
                System.Windows.MessageBox.Show("Currently Loading files, please wait until their done.", "Slow down cowboy", MessageBoxButton.OK);
                } else {
                Fluent.MenuItem rfi = (Fluent.MenuItem)sender;
                //                rfi = (RecentFileItem)sender;
                //LoadCache.Add(new NTDataFile(rfi.FilePath));
                LoadCache.Add(new NTDataFile(rfi.ToolTip.ToString()));
                bgw.RunWorkerAsync();
                }

            UpdateRecentFiles(sender);
            }

        private void UpdateRecentFiles(object sender) {
            //RecentFileItem file = (RecentFileItem)sender;
            Fluent.MenuItem file = (Fluent.MenuItem)sender;

            //foreach (RecentFileItem rfi in RecentFileControl.Items) {
            foreach (Fluent.MenuItem rfi in RecentFileControl.Items) {
                rfi.Click -= RecentFile_Click;
                }

            RecentFileControl.Items.Clear();

            List<String> recentFiles = new List<string>(Properties.Settings.Default.RecentFiles.Split(';'));

            //recentFiles.RemoveAll(s => s == file.FilePath);
            recentFiles.RemoveAll(s => s == file.ToolTip.ToString());

            //recentFiles.Insert(0, file.FilePath);
            recentFiles.Insert(0, file.ToolTip.ToString());

            string saveString = "";

            for (int i = 0; i <= recentFiles.Count - 1 & i < 10; i++) {
                //RecentFileItem menuItem = new RecentFileItem(recentFiles[i]);
                Fluent.MenuItem menuItem = new Fluent.MenuItem {
                    Header = System.IO.Path.GetFileName(recentFiles[i]),
                    ToolTip = recentFiles[i],
                    Icon = "Images/Icons/File.ico"
                    };
                menuItem.Click += RecentFile_Click;
                RecentFileControl.Items.Add(menuItem);

                saveString += recentFiles[i];
                if (i < recentFiles.Count - 1)
                    saveString += ";";
                }
            Properties.Settings.Default.RecentFiles = saveString;
            Properties.Settings.Default.Save();
            }
        private void UpdateTreeView() {
            }

        #endregion Methods
        }
    }