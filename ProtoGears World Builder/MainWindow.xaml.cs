using Fluent;
using Microsoft.WindowsAPICodePack.Dialogs;
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

            //tabHome.Visibility = Visibility.Hidden;

            string[] recentFiles = Properties.Settings.Default.RecentFiles.Split(';');

            recentFiles = recentFiles.Where(s => s != "").ToArray();

            foreach (string sFile in recentFiles) {

                Fluent.MenuItem menuItem = new Fluent.MenuItem {
                    Header = System.IO.Path.GetFileName(sFile),
                    ToolTip = sFile,
                    Icon = "Images/Icons/File.ico",
                    Size = RibbonControlSize.Large,
                    Width = 300

                    };

                menuItem.Click += RecentFile_Click;
                RecentFileGallery.Items.Add(menuItem);
                //RecentFileControl.Items.Add(menuItem);
                }
            
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
        private TreeViewItem SelectedNode {
            get {
                try {
                    return (TreeViewItem)DataView.SelectedItem;
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
            UpdateProgressBar.Value = 0;

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
                object selectedObject;
                if (cbiSelected.Tag is string) {
                    string objectID = cbiSelected.Tag.ToString();
                    selectedObject = DataFile.FindObject(objectID);
                    }else {
                    selectedObject = cbiSelected.Tag;
                    }
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
                tabCollectorGroup.Visibility = (Visibility)converter.Convert(selectedItem.NodeType == TreeViewItemExtention.NodeTypeEnum.ObjectCollector, null, null, null);
                tabObjectGroup.Visibility = (Visibility)converter.Convert(selectedItem.NodeType == TreeViewItemExtention.NodeTypeEnum.Object, null, null, null);

                //if() {
                //        tabDataGroup.Visibility = Visibility.Visible;
                //    } else {
                //    tabDataGroup.Visibility = Visibility.Collapsed;
                //    }

                switch (selectedItem.NodeType) {
                    case TreeViewItemExtention.NodeTypeEnum.DataRoot:
                        DataItemSelector.Items.Insert(0, new ComboBoxItem {
                            Content = DataFile.FileName,
                            Tag = DataFile
                            });
                        RibbonWin.SelectedTabItem = tabFileTools;
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
                        RibbonWin.SelectedTabItem = tabCollectorTools;
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
                        RibbonWin.SelectedTabItem = tabObjectTools;
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
                        Icon = "Images/Icons/File.ico",
                        Size = RibbonControlSize.Large,
                        Width = 300

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

        private void RecentFile_Click(object sender, RoutedEventArgs e) {
            if (bgw.IsBusy) {
                System.Windows.MessageBox.Show("Currently Loading files, please wait until their done.", "Slow down cowboy", MessageBoxButton.OK);
                } else {

                Fluent.MenuItem rfi = (Fluent.MenuItem)sender;
                LoadCache.Add(new NTDataFile(rfi.ToolTip.ToString()));

                bgw.RunWorkerAsync();
                }

            UpdateRecentFiles(sender);
            }

        private void UpdateRecentFiles(object sender) {

            Fluent.MenuItem file = (Fluent.MenuItem)sender;

            foreach (Fluent.MenuItem rfi in RecentFileGallery.Items) {

                rfi.Click -= RecentFile_Click;
                }

            RecentFileGallery.Items.Clear();

            List<String> recentFiles = new List<string>(Properties.Settings.Default.RecentFiles.Split(';'));

            recentFiles.Remove("");

            recentFiles.RemoveAll(s => s == file.ToolTip.ToString());

            recentFiles.Insert(0, file.ToolTip.ToString());

            string saveString = "";

            for (int i = 0; i <= recentFiles.Count - 1 & i < 10; i++) {

                Fluent.MenuItem menuItem = new Fluent.MenuItem {
                    Header = System.IO.Path.GetFileName(recentFiles[i]),
                    ToolTip = recentFiles[i],
                    Icon = "Images/Icons/File.ico",
                    Size = RibbonControlSize.Large,
                    Width = 300
                    };

                menuItem.Click += RecentFile_Click;
                RecentFileGallery.Items.Add(menuItem);
                //RecentFileControl.Items.Add(menuItem);

                saveString += recentFiles[i];
                if (i < recentFiles.Count - 1)
                    saveString += ";";
                }
            Properties.Settings.Default.RecentFiles = saveString;
            Properties.Settings.Default.Save();
            }

        private void buttonCloseFile_Click(object sender, RoutedEventArgs e) {
            if (DataFile.DataChanged) {
                switch (MessageBox.Show(String.Format("{0} has changed since it was opened would you like to save it now?", DataFile.FileName), "Save file?", MessageBoxButton.YesNoCancel)) {
                    case MessageBoxResult.Yes:
                        DataFile.Save();
                        break;
                    case MessageBoxResult.Cancel:
                        return;
                    }
                }
            foreach (OCCBase item in DataFile.Collectors) {
                item.TreeDataChanged -= Item_TreeDataChanged;
                }
            
            int indexof = DataFiles.IndexOf(DataFile);
            bool removed = DataFiles.Remove(DataFile);
            DataView.Items.Remove(DataView.SelectedItem);
            if( DataView.SelectedItem==null)
                tabDataGroup.Visibility = Visibility.Collapsed;
            }

        private void UpdateTreeView() {
            
            }

        private void btnPlugins_Click(object sender, RoutedEventArgs e) {
            LoadedPlugins wind = new LoadedPlugins();
            wind.ShowDialog();
            }

        private void btnNew_Click(object sender, RoutedEventArgs e) {
            NewFile wndNewFile = new NewFile();
            wndNewFile.Owner = this;
            wndNewFile.ShowInTaskbar = false;
            wndNewFile.ShowDialog();

            if (wndNewFile.Result == CommonFileDialogResult.Ok) {
                NTDataFile newFile = new NTDataFile(
                    wndNewFile.txtFilePathText.Text + "\\" + wndNewFile.txtFileNameText.Text + ".ntx",
                    wndNewFile.txtFileIDText.Text,
                    wndNewFile.txtFileDataSetText.Text
                    );
                newFile.Author = wndNewFile.txtAuthorText.Text;
                newFile.AuthorEmail = wndNewFile.txtAuthorEmailText.Text;
                newFile.AuthorWebsite = wndNewFile.txtAuthorWebText.Text;
                newFile.Description = wndNewFile.txtDescriptionText.Text;
                newFile.DateCreated = DateTime.Now;

                newFile.Save();
                newFile = null;

                newFile = new NTDataFile(wndNewFile.txtFilePathText.Text + "\\" + wndNewFile.txtFileNameText.Text + ".ntx");
                FileEventSubscriptions(newFile, false);
                LoadCache.Add(newFile);

                bgw.RunWorkerAsync();
                }
            }

        private void btnSaveAll_Click(object sender, RoutedEventArgs e) {
            foreach (NTDataFile file in DataFiles) {
                file.Save();
                }
            }

        private void btnSettings_Click(object sender, RoutedEventArgs e) {
            ProgramSettings wndNewFile = new ProgramSettings();
            wndNewFile.Owner = this;
            wndNewFile.ShowInTaskbar = false;
            wndNewFile.ShowDialog();
            }

        private void btnClearRecent_Click(object sender, RoutedEventArgs e) {
            RecentFileGallery.Items.Clear();
            Properties.Settings.Default.RecentFiles = "";
            Properties.Settings.Default.Save();
            }

        private void btnExportFile_Click(object sender, RoutedEventArgs e) {
            if (DataFile == null) return;

            string folder = GetFolderLocation();

            if (folder == "") return;
            if ((FrameworkElement)sender == btnExport)
                DataFile.ExportToXMLSingle(folder + "\\");
            if ((FrameworkElement)sender == btnExportTEXT)
                DataFile.ExportToTXT(folder + "\\");
            if ((FrameworkElement)sender == btnExportCSV)
                DataFile.ExportToCSV(folder + "\\");
            if ((FrameworkElement)sender == buttonExportGroup)
                DataFile.ExportCollectorToXML(folder + "\\", SelectedNode.Header.ToString());
            
            }

        private string GetFolderLocation() {
            var dlg = new CommonOpenFileDialog();

            dlg.Title = "Export To Folder...";
            dlg.IsFolderPicker = true;

            dlg.AddToMostRecentlyUsedList = false;
            dlg.AllowNonFileSystemItems = false;
            dlg.EnsureFileExists = true;
            dlg.EnsurePathExists = true;
            dlg.EnsureReadOnly = false;
            dlg.EnsureValidNames = true;
            dlg.Multiselect = false;
            dlg.ShowPlacesList = true;

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok) {
                return dlg.FileName;
                }
            return "";
            }

        private void btnPurge_Click(object sender, RoutedEventArgs e) {
            DataFile.PurgeFile();
            }

        private void btnLockFile_Click(object sender, RoutedEventArgs e) {
            InputWindows.InputPasswordWindow pswrdBox;
            if (DataFile.FileLocked) {
                //get password from user
                pswrdBox = new InputWindows.InputPasswordWindow("Password:", false);
                if (pswrdBox.ShowDialog() == false) return;
                if (DataFile.CheckPassword(pswrdBox.Answer)) {
                    DataFile.UnLockFile(pswrdBox.Answer);
                    } else {
                    MessageBox.Show("Invalid Password...");
                    }
                } else {
                pswrdBox = new InputWindows.InputPasswordWindow("Password:", true);
                if (pswrdBox.ShowDialog() == false) return;
                DataFile.FilePassword = pswrdBox.Answer;
                DataFile.LockFile();
                }
            }

        private void btnSetFilePassword(object sender, RoutedEventArgs e) {
            if (DataFile.FilePassword != "") {
                InputWindows.InputPasswordWindow pswrdBox = new InputWindows.InputPasswordWindow("Enter Current Password:");
                if (pswrdBox.ShowDialog() == false) return;
                if(!DataFile.CheckPassword(pswrdBox.Answer)) {
                    MessageBox.Show("Incorrect Password!");
                    return;
                    }
                }
            InputWindows.InputPasswordWindow newPassBox = new InputWindows.InputPasswordWindow("Enter New Password:", true);
            if (newPassBox.ShowDialog() == false) return;
            DataFile.FilePassword = newPassBox.Answer;
            }

        private void btnCopySelected(object sender, RoutedEventArgs e) {
            TreeViewItemExtention node = (TreeViewItemExtention) SelectedNode;
            switch (node.NodeType) {
                case TreeViewItemExtention.NodeTypeEnum.DataRoot:
                    DataFile.CopyFileToClipboard();
                    break;
                case TreeViewItemExtention.NodeTypeEnum.ObjectCollector:
                    DataFile.CopyCollectorToClipboard(node.Header.ToString());
                    break;
                case TreeViewItemExtention.NodeTypeEnum.Object:
                    DataFile.CopyObjecctToClipboard(node.Tag.ToString());
                    break;
                case TreeViewItemExtention.NodeTypeEnum.Other:
                    break;
                default:
                    break;
                }
            //CopyClip.CopyToClipboard()
            }

        private void ObjectViewer_PropertyChanged(object sender, PropertyChangedEventArgs e) {

            }

        private void ObjectViewer_PropertyEditingFinished(object sender, RoutedEventArgs e) {

            }

        private void ObjectViewer_PropertyEditingStarted(object sender, RoutedEventArgs e) {

            }

        private void ObjectViewer_PropertyValueChanged(object sender, System.Windows.Controls.WpfPropertyGrid.PropertyValueChangedEventArgs e) {

            }

        private bool CheckForSave() {
            if (DataFile != null) {
                if (DataFile.DataChanged) {
                    switch (MessageBox.Show(String.Format("{0} has changed since it was opened would you like to save it now?", DataFile.FileName), "Save file?", MessageBoxButton.YesNoCancel)) {
                        case MessageBoxResult.Yes:
                            DataFile.Save();
                            break;

                        case MessageBoxResult.Cancel:
                            break;
                        }
                    }
                }
            return true;
            }
        #endregion Methods
        }
    }