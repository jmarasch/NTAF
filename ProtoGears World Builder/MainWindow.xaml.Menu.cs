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
    public partial class MainWindow {
        //=============================menu stuff=============================//

        #region Menu stuff

        //private const Keys PreViewObjectKey = Keys.Control | Keys.Shift | Keys.P;
        //private const Keys EditObjectKey = Keys.Control | Keys.Shift | Keys.E;
        //private const Keys DeleteObjectKey = Keys.Delete;
        //private const Keys CopyObjectKey = Keys.Control | Keys.Shift | Keys.C;
        //private const Keys NewObjectKey = Keys.Control | Keys.Shift | Keys.N;
        //private const Keys ClearObjectKey = Keys.Control | Keys.Shift | Keys.D;

        private ContextMenu FileNodeMenuStrip;
        private MenuItem 
            closeFileMenuItem,
            saveFileMenuItem,
            reloadFileMenuItem,
            exportToolStripMenuItem1,
            printToolStripMenuItem1,
            printPreviewToolStripMenuItem1,
            securityToolStripMenuItem1,
            purgeFileToolStripMenuItem1;

        private ContextMenu OCMenuStrip;
        private MenuItem
            previewObjectToolStripMenuItem,
            editObjectToolStripMenuItem,
            deleteObjectToolStripMenuItem,
            copyObjectToolStripMenuItem;

        private ContextMenu OCCMenuStrip;
        private MenuItem
            newObjectToolStripMenuItem,
            clearObjectsToolStripMenuItem;

        private ContextMenu OrphanRootMenuStrip;
        private MenuItem OrphanPurgeMenuItem;

        private ContextMenu OrphanMenuStrip;
        private MenuItem
            moveOprphanToCollectorMenuItem, 
            findOrphanReferencesMenuItem;

        private void BuildMenu() {
            moveOprphanToCollectorMenuItem = new MenuItem();
            moveOprphanToCollectorMenuItem.Name = "moveOprphanToCollectorMenuItem";
            //moveOprphanToCollectorMenuItem.ShortcutKeys = ClearObjectKey;
            moveOprphanToCollectorMenuItem.Header = "Recreate orphan";
            //todo create click event
            //moveOprphanToCollectorMenuItem.Click += new EventHandler(clearObjectsToolStripMenuItem_Click);

            findOrphanReferencesMenuItem = new MenuItem();
            findOrphanReferencesMenuItem.Name = "findOrphanReferencesMenuItem";
            //findOrphanReferencesMenuItem.ShortcutKeys = ClearObjectKey;
            findOrphanReferencesMenuItem.Header = "Find Orphan References";
            //todo create click event
            //findOrphanReferencesMenuItem.Click += new EventHandler(clearObjectsToolStripMenuItem_Click);

            OrphanPurgeMenuItem = new MenuItem();
            OrphanPurgeMenuItem.Name = "OrphanPurgeMenuItem";
            //OrphanPurgeMenuItem.ShortcutKeys = ClearObjectKey;
            OrphanPurgeMenuItem.Header = "Purge All Orphans";
            //todo create click event
            //OrphanPurgeMenuItem.Click += new EventHandler(clearObjectsToolStripMenuItem_Click);

            closeFileMenuItem = new MenuItem();
            closeFileMenuItem.Header = "Close File";
            closeFileMenuItem.Name = "closeFileMenuItem";
            //closeFileMenuItem.Click +=

            saveFileMenuItem = new MenuItem();
            saveFileMenuItem.Header = "Save File";
            saveFileMenuItem.Name ="saveFileMenuItem";
            //saveFileMenuItem.Click +=

            reloadFileMenuItem = new MenuItem();
            reloadFileMenuItem.Header = "Reload File";
            reloadFileMenuItem.Name = "reloadFileMenuItem";
            //reloadFileMenuItem.Click +=

            OCMenuStrip = new ContextMenu();
            OCMenuStrip.Items.Add(previewObjectToolStripMenuItem);
            OCMenuStrip.Items.Add(editObjectToolStripMenuItem);
            OCMenuStrip.Items.Add(copyObjectToolStripMenuItem);
            OCMenuStrip.Items.Add(deleteObjectToolStripMenuItem);
            OCMenuStrip.Name = "OCMenuStrip";

            OCCMenuStrip = new ContextMenu();
            OCCMenuStrip.Items.Add(newObjectToolStripMenuItem);
            OCCMenuStrip.Items.Add(clearObjectsToolStripMenuItem);
            OCCMenuStrip.Name = "OCCMenuStrip";

            OrphanRootMenuStrip = new ContextMenu();
            OrphanRootMenuStrip.Items.Add(OrphanPurgeMenuItem);
            OrphanRootMenuStrip.Name = "OrphanRootMenuStrip";

            //todo add orphan menu here
            OrphanMenuStrip = new ContextMenu();
            OrphanMenuStrip.Items.Add(moveOprphanToCollectorMenuItem);
            OrphanMenuStrip.Items.Add(findOrphanReferencesMenuItem );
            OrphanMenuStrip.Name = "OrphanMenuStrip";

            FileNodeMenuStrip = new ContextMenu();
            FileNodeMenuStrip.Items.Add(closeFileMenuItem);
            FileNodeMenuStrip.Items.Add(saveFileMenuItem);
            FileNodeMenuStrip.Items.Add(reloadFileMenuItem);
            FileNodeMenuStrip.Items.Add(exportToolStripMenuItem1);
            FileNodeMenuStrip.Items.Add(printToolStripMenuItem1);
            FileNodeMenuStrip.Items.Add(printPreviewToolStripMenuItem1);
            FileNodeMenuStrip.Items.Add(securityToolStripMenuItem1);
            FileNodeMenuStrip.Items.Add(purgeFileToolStripMenuItem1);
            FileNodeMenuStrip.Name = "FileNodeMenuStrip";
            }

        #region MenuClick Events

        //private void editObjectToolStripMenuItem_Click(object sender, EventArgs e) {
        //    try {
        //        OCEditorBase ed = null;
        //        Type editType = ((OCNode)DataView.SelectedNode).ObjectClass.MyType();
        //        foreach (OCEditorBase tmpEd in PE.GetEditorPlugIns()) {
        //            if (tmpEd.IEdit(editType)) {
        //                ed = tmpEd; break;
        //                }
        //            }

        //        List<string>
        //            IDs = new List<string>(DataFile.IDs);

        //        //todo make better
        //        if (ed == null)
        //            throw new Exception(
        //                "I couldn't find an editor to go with this Object type," + Environment.NewLine +
        //                "Please make sure that the plug in creator also created a GUI editor for this object.");

        //        ed.Collectors = DataFile.Collectors;

        //        ed.MyObject = ((OCNode)DataView.SelectedNode).ObjectClass;

        //        switch (ed.RunEditor(EditorMode.Edit)) {
        //            case EditorExitCode.OK:
        //                DataFile.Edit(((OCNode)DataView.SelectedNode).ObjectClass, ed.MyObject);
        //                break;

        //            case EditorExitCode.Cancel:
        //                break;

        //            default:
        //                break;
        //            }
        //        } catch (Exception ex) {
        //        //todo need exception msg box
        //        }
        //    }

        //private void copyObjectToolStripMenuItem_Click(object sender, EventArgs e) {
        //    try {
        //        if (DataView.SelectedNode is OCNode)
        //            CopyClip.CopyToClipboard(((OCNode)DataView.SelectedNode).ObjectClass);
        //        } catch (Exception ex) {
        //        //todo need exception msg box
        //        }
        //    }

        //private void clearObjectsToolStripMenuItem_Click(object sender, EventArgs e) {
        //    //todo
        //    }

        //private void newObjectToolStripMenuItem_Click(object sender, EventArgs e) {
        //    try {
        //        OCEditorBase ed = null;
        //        Type editType = ((OCCNode)DataView.SelectedNode).Collector.CollectionType;
        //        foreach (OCEditorBase tmpEd in PE.GetEditorPlugIns()) {
        //            if (tmpEd.IEdit(editType)) {
        //                ed = tmpEd; break;
        //                }
        //            }

        //        List<string>
        //        IDs = new List<string>(DataFile.IDs);

        //        //todo make better
        //        if (ed == null)
        //            throw new Exception(
        //                "I couldn't find an editor to go with this Object type," + Environment.NewLine +
        //                "Please make sure that the plug in creator also created a GUI editor for this object.");

        //        ed.Collectors = DataFile.Collectors;

        //        ed.MyObject = (ObjectClassBase)Activator.CreateInstance(editType);

        //        ed.MyObject.ID = DataFile.GenerateIDCode();

        //        //ed.MyObject.myOwner = DataFile;

        //        switch (ed.RunEditor(EditorMode.New)) {
        //            case EditorExitCode.OK:
        //                //ed.MyObject.myOwner = DataFile;

        //                DataFile.Add(ed.MyObject);
        //                break;

        //            case EditorExitCode.Cancel:
        //                break;

        //            default:
        //                break;
        //            }
        //        } catch (Exception ex) {
        //        //todo need exception msg box
        //        }
        //    }

        //private void deleteObjectToolStripMenuItem_Click(object sender, EventArgs e) {
        //    if (DataView.SelectedNode is OCNode)
        //        DataFile.Drop(((OCNode)DataView.SelectedNode).ObjectClass);
        //    }

        //private void previewObjectToolStripMenuItem_Click(object sender, EventArgs e) {
        //    ObjectPreview objpre = null;
        //    if (DataView.SelectedNode is OCNode) {
        //        ObjectClassBase obj = ((OCNode)DataView.SelectedNode).ObjectClass;

        //        if (obj != null) {
        //            objpre = new ObjectPreview(obj);
        //            objpre.ShowDialog();
        //            }
        //        }
        //    }

        //private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
        //    try {
        //        DataFile.Save();
        //        //Title = DataFile.FileName;
        //        } catch (Exception ex) {
        //        //todo need exception msg box
        //        }
        //    }

        //private void saveAsToolStripMenuItem_Click(object sender, EventArgs e) {
        //    try {
        //        DataFile.SaveAs();
        //        //Title = DataFile.FileName;
        //        } catch (Exception ex) {
        //        //todo need exception msg box
        //        }
        //    }

        //private void newToolStripMenuItem_Click(object sender, EventArgs e) {
        //    try {
        //        //does the current data need to be saved?
        //        if (CheckForSave()) {
        //            //create a new reference
        //            NTDataFile newFile = new NTDataFile();

        //            //todo: need to add dialog boxes to get info here
        //            newFile.IDPreFix = "NEWF";

        //            newFile.DataFileName = "NewFile";

        //            FileEventSubscriptions(newFile, false);

        //            //load the nodes
        //            newFile.getTreeNodes(DataView.Nodes, FileNodeMenuStrip, OCCMenuStrip, OCMenuStrip, OrphanRootMenuStrip, OrphanMenuStrip);

        //            //add file to loaded files list
        //            DataFiles.Add(newFile);
        //            }
        //        } catch (Exception ex) {
        //        //todo need exception message box
        //        }
        //    }

        //private void openToolStripMenuItem_Click(object sender, EventArgs e) {
        //    try {
        //        if (CheckForSave()) {
        //            OpenFileDialog OFD = new OpenFileDialog();
        //            OFD.Filter = "NewTerra Data Files (*.ntx)|*.ntx";
        //            OFD.SupportMultiDottedExtensions = true;
        //            OFD.Multiselect = false;
        //            if (OFD.ShowDialog() == DialogResult.OK) {
        //                //DataView.Nodes.Clear();
        //                NTDataFile fileToLoad = new NTDataFile(OFD.FileName);

        //                FileEventSubscriptions(fileToLoad, false);

        //                LoadCache.Add(fileToLoad);

        //                bgw.RunWorkerAsync();
        //                }
        //            }
        //        } catch (Exception ex) {
        //        //todo need exception message box
        //        }
        //    }

        //private void DataView_MouseClick(object sender, MouseEventArgs e) {
        //    try {
        //        if (e.Button == MouseButtons.Right)
        //            DataView.SelectedNode = DataView.GetNodeAt(e.Location);
        //        } catch (Exception ex) {
        //        //todo need exception msg box
        //        }
        //    }

        //private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
        //    Application.Exit();
        //    }

        //private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e) {
        //    try {
        //        setUpPrint();
        //        PrintPreviewDialog pPreview = _PrintEngine.ShowPreview();
        //        pPreview.ShowDialog();
        //        } catch (Exception ex) {
        //        }
        //    }

        //private void printToolStripMenuItem_Click(object sender, EventArgs e) {
        //    try {
        //        PrintDialog pDialog = _PrintEngine.ShowPrintDialog();
        //        setUpPrint();
        //        if (pDialog.ShowDialog() == DialogResult.OK) {
        //            NTAF.PrintEngine.Properties.Settings.Default.printerSettings = pDialog.PrinterSettings;
        //            _PrintEngine.Print();
        //            NTAF.PrintEngine.Properties.Settings.Default.Save();
        //            }
        //        } catch (Exception ex) {
        //        }
        //    }

        //private void PageSettingsMenuItem_Click(object sender, EventArgs e) {
        //    try {
        //        setUpPrint();
        //        PageSetupDialog myEngineSettings = _PrintEngine.ShowPageSettings();

        //        if (myEngineSettings.ShowDialog() == DialogResult.OK) {
        //            NTAF.PrintEngine.Properties.Settings.Default.printSettings = myEngineSettings.PageSettings;
        //            NTAF.PrintEngine.Properties.Settings.Default.Save();
        //            }
        //        } catch (Exception ex) {
        //        }
        //    }

        //private void PrintSettingsMenuItem_Click(object sender, EventArgs e) {
        //    try {
        //        setUpPrint();
        //        FontDialog myPrintFontSettings = _PrintEngine.ShowFontDialog();

        //        if (myPrintFontSettings.ShowDialog() == DialogResult.OK) {
        //            _PrintEngine.PrintFont = myPrintFontSettings.Font;
        //            NTAF.PrintEngine.Properties.Settings.Default.printFontSettings = myPrintFontSettings.Font;
        //            NTAF.PrintEngine.Properties.Settings.Default.Save();
        //            }
        //        } catch (Exception ex) {
        //        }
        //    }

        //private void undoToolStripMenuItem_Click(object sender, EventArgs e) {
        //    try { DataFile.DoUndo(); } catch (Exception ex) { MessageBox.Show(ex.Message); }
        //    }

        //private void redoToolStripMenuItem_Click(object sender, EventArgs e) {
        //    try { DataFile.DoRedo(); } catch (Exception ex) { MessageBox.Show(ex.Message); }
        //    }

        //private void cutToolStripMenuItem_Click(object sender, EventArgs e) {
        //    try {
        //        if (DataView.SelectedNode is OCNode) {
        //            CopyClip.CopyToClipboard(((OCNode)DataView.SelectedNode).ObjectClass);
        //            DataFile.Drop(((OCNode)DataView.SelectedNode).ObjectClass);
        //            }
        //        } catch (Exception ex) {
        //        //todo need exception msg box
        //        }
        //    }

        //private void pasteToolStripMenuItem_Click(object sender, EventArgs e) {
        //    try {
        //        CopyClip.CopyFromClipboard(DataFile);
        //        } catch (Exception ex) {
        //        //todo need exception msg box
        //        }
        //    }

        //private void customizeToolStripMenuItem_Click(object sender, EventArgs e) {
        //    }

        //private void optionsToolStripMenuItem_Click(object sender, EventArgs e) {
        //    }

        //private void contentsToolStripMenuItem_Click(object sender, EventArgs e) {
        //    }

        //private void indexToolStripMenuItem_Click(object sender, EventArgs e) {
        //    }

        //private void searchToolStripMenuItem_Click(object sender, EventArgs e) {
        //    }

        //private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
        //    AboutBox1 abx = new AboutBox1();
        //    abx.ShowDialog();
        //    }

        //private void currentlyLoadedPluginsToolStripMenuItem_Click(object sender, EventArgs e) {
        //    LoadedModuleBox lmbx = new LoadedModuleBox();
        //    lmbx.ShowDialog();
        //    }

        //private void purgeFileToolStripMenuItem_Click(object sender, EventArgs e) {
        //    DataFile.PurgeFile();
        //    }

        //private void ExportTXTFile(object sender, EventArgs e) {
        //    SaveFileDialog sfd = new SaveFileDialog();
        //    sfd.Title = "Export to .txt file...";
        //    sfd.Filter = "Text file (*.txt)|*.txt";
        //    sfd.AddExtension = true;
        //    sfd.OverwritePrompt = true;

        //    if (sfd.ShowDialog() == DialogResult.OK) {
        //        DataFile.ExportToTXT(sfd.FileName);
        //        }
        //    }

        //private void ExportCSVFile(object sender, EventArgs e) {
        //    SaveFileDialog sfd = new SaveFileDialog();
        //    sfd.Title = "Export to .csv file...";
        //    sfd.Filter = "Comma Separated Values file (*.csv)|*.csv";
        //    sfd.AddExtension = true;
        //    sfd.OverwritePrompt = true;

        //    if (sfd.ShowDialog() == DialogResult.OK) {
        //        DataFile.ExportToCSV(sfd.FileName);
        //        }
        //    }

        //private void lockFileToolStripMenuItem_Click(object sender, EventArgs e) {
        //    if (DataFile.FileLocked) {
        //        try { DataFile.UnLockFile(InputBox.Show("Please enter the files password", "Password Protected...", true)); } catch (InvalidPasswordException ex) { MessageBox.Show("Incorrect Password"); } catch (Exception ex) { /*todo need exception msg box*/ }
        //        } else {
        //        bool exitCode = false;
        //        while (!exitCode) {
        //            try {
        //                DataFile.LockFile();
        //                exitCode = true;
        //                } catch (NullPasswordException) {
        //                String Password =
        //                    InputBox.Show("Please enter a password for the file", "Enter New Password...", true);

        //                //if the user didn't enter anything exit the function
        //                if (Password == "") {
        //                    MessageBox.Show("Password not set", "File not locked...", MessageBoxButtons.OK); return;
        //                    }

        //                //get a matching password from the user
        //                String PasswordConf =
        //                    InputBox.Show("Please confirm the password for the file", "Confirm New Password...", true);

        //                if (Password.CompareTo(PasswordConf) != 0) {
        //                    MessageBox.Show("Passwords don't match", "Could Not Confirm New Password...", MessageBoxButtons.OK);
        //                    } else {
        //                    DataFile.FilePassword = Password;
        //                    }
        //                }
        //            }
        //        }
        //    }

        //private void setPasswordToolStripMenuItem_Click(object sender, EventArgs e) {
        //    try {
        //        String Password =
        //                    InputBox.Show("Please enter a password for the file", "Enter New Password...", true);

        //        //if the user didn't enter anything exit the function
        //        if (Password == "") {
        //            MessageBox.Show("Password not set", "File not locked...", MessageBoxButtons.OK); return;
        //            }

        //        //get a matching password from the user
        //        String PasswordConf =
        //                    InputBox.Show("Please confirm the password for the file", "Confirm New Password...", true);

        //        if (Password.CompareTo(PasswordConf) != 0) {
        //            MessageBox.Show("Passwords don't match", "Could Not Confirm New Password...", MessageBoxButtons.OK);
        //            } else {
        //            DataFile.FilePassword = Password;
        //            }
        //        } catch (FileLockedException) {
        //        MessageBox.Show("Password not set", "File not locked...", MessageBoxButtons.OK); return;
        //        }
        //    }

        #endregion MenuClick Events

        #endregion Menu stuff
        }
    }
