using NTAF.Controls.StatusBar;
using NTAF.Core;
using NTAF.PlugInFramework;
using NTAF.PrintEngine;
using NTAF.PrintEngine.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Drawing.Printing;
using System.Resources;
using System.Windows.Forms;
using UniverseBuilder.Properties;

namespace UniverseBuilder
{
	public class Main : Form
	{
		private const Keys PreViewObjectKey = Keys.ShiftKey | Keys.P | Keys.Shift | Keys.Control;

		private const Keys EditObjectKey = Keys.LButton | Keys.MButton | Keys.XButton1 | Keys.A | Keys.D | Keys.E | Keys.Shift | Keys.Control;

		private const Keys DeleteObjectKey = Keys.Delete;

		private const Keys CopyObjectKey = Keys.LButton | Keys.RButton | Keys.Cancel | Keys.A | Keys.B | Keys.C | Keys.Shift | Keys.Control;

		private const Keys NewObjectKey = Keys.RButton | Keys.MButton | Keys.XButton2 | Keys.Back | Keys.LineFeed | Keys.Clear | Keys.B | Keys.D | Keys.F | Keys.H | Keys.J | Keys.L | Keys.N | Keys.Shift | Keys.Control;

		private const Keys ClearObjectKey = Keys.MButton | Keys.D | Keys.Shift | Keys.Control;

		private NTDataFile DataFile = new NTDataFile();

		private BackgroundWorker bgw = new BackgroundWorker();

		private TreeNode Orphans = new TreeNode("Orpahned Objects");

		private PrintEngine _PrintEngine = null;

		private IContainer components = null;

		private SplitContainer splitContainer1;

		private TableLayoutPanel tableLayoutPanel1;

		private PropertyGrid ObjectViewer;

		private ComboBox comboBox1;

		private TreeView DataView;

		private TableLayoutPanel tableLayoutPanel2;

		private StatusStrip statusStrip1;

		private MenuStrip menuStrip1;

		private ToolStripMenuItem fileToolStripMenuItem;

		private ToolStripMenuItem newToolStripMenuItem;

		private ToolStripMenuItem openToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator;

		private ToolStripMenuItem saveToolStripMenuItem;

		private ToolStripMenuItem saveAsToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripMenuItem printToolStripMenuItem;

		private ToolStripMenuItem printPreviewToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripMenuItem exitToolStripMenuItem;

		private ToolStripMenuItem editToolStripMenuItem;

		private ToolStripMenuItem undoToolStripMenuItem;

		private ToolStripMenuItem redoToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripMenuItem cutToolStripMenuItem;

		private ToolStripMenuItem copyToolStripMenuItem;

		private ToolStripMenuItem pasteToolStripMenuItem;

		private ToolStripMenuItem toolsToolStripMenuItem;

		private ToolStripMenuItem customizeToolStripMenuItem;

		private ToolStripMenuItem optionsToolStripMenuItem;

		private ToolStripMenuItem helpToolStripMenuItem;

		private ToolStripMenuItem contentsToolStripMenuItem;

		private ToolStripMenuItem indexToolStripMenuItem;

		private ToolStripMenuItem searchToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripMenuItem aboutToolStripMenuItem;

		private ToolStripStatusLabel UpdateProgressLabel;

		private ToolStripProgressBar UpdateProgressBar;

		private ToolStripMenuItem purgeFileToolStripMenuItem;

		private ToolStripMenuItem securityToolStripMenuItem;

		private ToolStripMenuItem lockFileToolStripMenuItem;

		private ToolStripMenuItem setPasswordToolStripMenuItem;

		private ToolStripMenuItem PageSettingsMenuItem;

		private ToolStripMenuItem PrintSettingsMenuItem;

		private StatusBarToggleButton FileLockIndicator;

		private ToolStripMenuItem currentlyLoadedPluginsToolStripMenuItem;

		private ToolStripMenuItem exportToolStripMenuItem;

		private ToolStripMenuItem tXTFileToolStripMenuItem;

		private ToolStripMenuItem cVSToolStripMenuItem;

		private ToolStripMenuItem excellWorkBookToolStripMenuItem;

		private System.Windows.Forms.ContextMenuStrip OCMenuStrip;

		private ToolStripMenuItem previewObjectToolStripMenuItem;

		private ToolStripMenuItem editObjectToolStripMenuItem;

		private ToolStripMenuItem deleteObjectToolStripMenuItem;

		private ToolStripMenuItem copyObjectToolStripMenuItem;

		private System.Windows.Forms.ContextMenuStrip OCCMenuStrip;

		private ToolStripMenuItem newObjectToolStripMenuItem;

		private ToolStripMenuItem clearObjectsToolStripMenuItem;

		private string Title
		{
			get
			{
				return this.Text;
			}
			set
			{
				if (!((value == "") | value == null))
				{
					this.Text = string.Concat("New Terra Universe Builder - ", value);
				}
				else
				{
					this.Text = "New Terra Universe Builder - New Data File";
				}
			}
		}

		public Main()
		{
			this.InitializeComponent();
			this.BuildMenu();
			this.newToolStripMenuItem_Click(null, null);
			this.bgw.DoWork += new DoWorkEventHandler(this.bgw_DoWork);
			this.bgw.ProgressChanged += new ProgressChangedEventHandler(this.bgw_ProgressChanged);
			this.bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgw_RunWorkerCompleted);
			this.bgw.WorkerReportsProgress = true;
			this.bgw.WorkerSupportsCancellation = true;
			this.DataFile.EventOrphansChanged += new NTEventHandler<ItemChangedArgs>(this.DataFile_EventOrphansChanged);
			this.DataFile.LockStatusChange += new NTEventHandler(this.DataFile_LockStatusChange);
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			(new AboutBox1()).ShowDialog();
		}

		private void bgw_DoWork(object sender, DoWorkEventArgs e)
		{
			this.DataFile.Load();
			this.DataFile.LinkData();
		}

		private void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			if (!this.UpdateProgressBar.Visible)
			{
				this.UpdateProgressBar.Visible = true;
			}
			UpdateProgressEventArgs args = e.UserState as UpdateProgressEventArgs;
			this.UpdateProgressBar.Maximum = args.NumberOfItems;
			this.UpdateProgressBar.Value = args.current;
			this.UpdateProgressLabel.Text = args.ProcessingMessage;
			this.statusStrip1.Invalidate(true);
		}

		private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.UpdateProgressLabel.Text = "Ready...";
			this.UpdateProgressBar.Value = 0;
			this.DataFile.getTreeNodes(this.DataView.Nodes, this.OCCMenuStrip, this.OCMenuStrip);
			this.DataView.Nodes.Add(this.Orphans);
			this.UpdateProgressBar.Visible = false;
		}

		private void BuildMenu()
		{
			ComponentResourceManager resources = new ComponentResourceManager(typeof(Main));
			this.previewObjectToolStripMenuItem = new ToolStripMenuItem()
			{
				Name = "previewObjectToolStripMenuItem",
				ShortcutKeys = Keys.ShiftKey | Keys.P | Keys.Shift | Keys.Control,
				Text = "&Preview Object"
			};
			this.previewObjectToolStripMenuItem.Click += new EventHandler(this.previewObjectToolStripMenuItem_Click);
			this.editObjectToolStripMenuItem = new ToolStripMenuItem()
			{
				Name = "editObjectToolStripMenuItem",
				ShortcutKeys = Keys.LButton | Keys.MButton | Keys.XButton1 | Keys.A | Keys.D | Keys.E | Keys.Shift | Keys.Control,
				Text = "&Edit Object"
			};
			this.editObjectToolStripMenuItem.Click += new EventHandler(this.editObjectToolStripMenuItem_Click);
			this.deleteObjectToolStripMenuItem = new ToolStripMenuItem()
			{
				Name = "deleteObjectToolStripMenuItem",
				ShortcutKeys = Keys.Delete,
				Text = "&Delete Object"
			};
			this.deleteObjectToolStripMenuItem.Click += new EventHandler(this.deleteObjectToolStripMenuItem_Click);
			this.copyObjectToolStripMenuItem = new ToolStripMenuItem()
			{
				Image = (Image)resources.GetObject("copyObjectToolStripMenuItem.Image"),
				ImageTransparentColor = Color.Magenta,
				Name = "copyObjectToolStripMenuItem",
				ShortcutKeys = Keys.LButton | Keys.RButton | Keys.Cancel | Keys.A | Keys.B | Keys.C | Keys.Shift | Keys.Control,
				Text = "&Copy"
			};
			this.copyObjectToolStripMenuItem.Click += new EventHandler(this.copyObjectToolStripMenuItem_Click);
			this.newObjectToolStripMenuItem = new ToolStripMenuItem()
			{
				Name = "newObjectToolStripMenuItem",
				ShortcutKeys = Keys.RButton | Keys.MButton | Keys.XButton2 | Keys.Back | Keys.LineFeed | Keys.Clear | Keys.B | Keys.D | Keys.F | Keys.H | Keys.J | Keys.L | Keys.N | Keys.Shift | Keys.Control,
				Text = "&New Object"
			};
			this.newObjectToolStripMenuItem.Click += new EventHandler(this.newObjectToolStripMenuItem_Click);
			this.clearObjectsToolStripMenuItem = new ToolStripMenuItem()
			{
				Name = "clearObjectsToolStripMenuItem",
				ShortcutKeys = Keys.MButton | Keys.D | Keys.Shift | Keys.Control,
				Text = "&Clear All Objects"
			};
			this.clearObjectsToolStripMenuItem.Click += new EventHandler(this.clearObjectsToolStripMenuItem_Click);
			this.OCMenuStrip = new System.Windows.Forms.ContextMenuStrip();
			ToolStripItemCollection items = this.OCMenuStrip.Items;
			ToolStripItem[] toolStripItemArray = new ToolStripItem[] { this.previewObjectToolStripMenuItem, this.editObjectToolStripMenuItem, this.copyObjectToolStripMenuItem, this.deleteObjectToolStripMenuItem };
			items.AddRange(toolStripItemArray);
			this.OCMenuStrip.Name = "OCMenuStrip";
			this.OCCMenuStrip = new System.Windows.Forms.ContextMenuStrip();
			ToolStripItemCollection toolStripItemCollections = this.OCCMenuStrip.Items;
			toolStripItemArray = new ToolStripItem[] { this.newObjectToolStripMenuItem, this.clearObjectsToolStripMenuItem };
			toolStripItemCollections.AddRange(toolStripItemArray);
			this.OCCMenuStrip.Name = "OCCMenuStrip";
		}

		private bool CheckForSave()
		{
			bool flag;
			if (!this.DataFile.DataChanged)
			{
				flag = true;
			}
			else
			{
				System.Windows.Forms.DialogResult dialogResult = MessageBox.Show(string.Format("{0} has changed since it was opened would you like to save it now?", this.DataFile.FileName), "Save file?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
				if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
				{
					flag = false;
				}
				else if (dialogResult == System.Windows.Forms.DialogResult.Yes)
				{
					this.DataFile.Save();
					flag = true;
				}
				else
				{
					flag = true;
				}
			}
			return flag;
		}

		private void clearObjectsToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.comboBox1.SelectedIndex != -1)
			{
				this.ObjectViewer.SelectedObject = this.comboBox1.SelectedItem;
			}
			else
			{
				this.ObjectViewer.SelectedObject = null;
			}
		}

		private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void copyObjectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.DataView.SelectedNode is OCNode)
				{
					CopyClip.CopyToClipboard(((OCNode)this.DataView.SelectedNode).ObjectClass);
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void currentlyLoadedPluginsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			(new LoadedModuleBox()).ShowDialog();
		}

		private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void cutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.DataView.SelectedNode is OCNode)
				{
					CopyClip.CopyToClipboard(((OCNode)this.DataView.SelectedNode).ObjectClass);
					this.DataFile.Drop(((OCNode)this.DataView.SelectedNode).ObjectClass);
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void DataFile_EventOrphansChanged(ItemChangedArgs args)
		{
			if (args.Action == ArgAction.Add)
			{
				this.Orphans.Nodes.Insert(args.Index, new OrphanNode((ObjectClassBase)args.Item));
			}
			if (args.Action == ArgAction.Remove)
			{
				foreach (OrphanNode OrpNode in this.Orphans.Nodes)
				{
					if (OrpNode.ObjectClass != args.Item)
					{
						continue;
					}
					OrpNode.Remove();
					break;
				}
			}
		}

		private void DataFile_LockStatusChange()
		{
			this.lockFileToolStripMenuItem.Checked = this.DataFile.FileLocked;
			this.FileLockIndicator.Checked = this.DataFile.FileLocked;
		}

		private void DataFile_Update(UpdateProgressEventArgs args)
		{
			this.bgw.ReportProgress(args.Percent, args);
		}

		private void DataFile_Updated()
		{
		}

		private void DataFile_Updating(UpdaterEventArgs args)
		{
		}

		private void DataView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			OCNode ocn = null;
			this.comboBox1.Items.Clear();
			if (!(this.DataView.SelectedNode is OCCNode) & !(this.DataView.SelectedNode is OCNode) & !(this.DataView.SelectedNode is OrphanNode) & this.DataView.SelectedNode != this.Orphans)
			{
				foreach (OCCNode occn in this.DataView.SelectedNode.Nodes)
				{
					foreach (OCNode ocn in occn.Nodes)
					{
						this.comboBox1.Items.Add(ocn.ObjectClass);
					}
				}
			}
			if (this.DataView.SelectedNode is OCCNode)
			{
				foreach (OCNode node in this.DataView.SelectedNode.Nodes)
				{
					this.comboBox1.Items.Add(node.ObjectClass);
				}
			}
			if (this.DataView.SelectedNode is OCNode)
			{
				this.comboBox1.Items.Add(((OCNode)this.DataView.SelectedNode).ObjectClass);
			}
			if (this.comboBox1.Items.Count < 1)
			{
				this.comboBox1.Items.Clear();
				this.comboBox1.Text = "";
				this.ObjectViewer.SelectedObject = null;
			}
			else
			{
				this.comboBox1.SelectedIndex = 0;
			}
		}

		private void DataView_Click(object sender, EventArgs e)
		{
		}

		private void DataView_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = true;
		}

		private void DataView_KeyUp(object sender, KeyEventArgs e)
		{
			try
			{
				Keys keyCode = e.KeyCode | e.Modifiers;
				if (keyCode == Keys.Delete)
				{
					this.deleteObjectToolStripMenuItem_Click(null, null);
					e.Handled = true;
				}
				else
				{
					switch (keyCode)
					{
						case Keys.LButton | Keys.RButton | Keys.Cancel | Keys.A | Keys.B | Keys.C | Keys.Shift | Keys.Control:
						{
							this.copyObjectToolStripMenuItem_Click(null, null);
							e.Handled = true;
							break;
						}
						case Keys.MButton | Keys.D | Keys.Shift | Keys.Control:
						{
							this.clearObjectsToolStripMenuItem_Click(null, null);
							e.Handled = true;
							break;
						}
						case Keys.LButton | Keys.MButton | Keys.XButton1 | Keys.A | Keys.D | Keys.E | Keys.Shift | Keys.Control:
						{
							this.editObjectToolStripMenuItem_Click(null, null);
							e.Handled = true;
							break;
						}
						default:
						{
							switch (keyCode)
							{
								case Keys.RButton | Keys.MButton | Keys.XButton2 | Keys.Back | Keys.LineFeed | Keys.Clear | Keys.B | Keys.D | Keys.F | Keys.H | Keys.J | Keys.L | Keys.N | Keys.Shift | Keys.Control:
								{
									this.newObjectToolStripMenuItem_Click(null, null);
									e.Handled = true;
									break;
								}
								case Keys.ShiftKey | Keys.P | Keys.Shift | Keys.Control:
								{
									this.previewObjectToolStripMenuItem_Click(null, null);
									e.Handled = true;
									break;
								}
							}
							break;
						}
					}
				}
			}
			catch (Exception exception)
			{
				Console.Write(exception.Message);
			}
		}

		private void DataView_MouseClick(object sender, MouseEventArgs e)
		{
			try
			{
				if (e.Button == System.Windows.Forms.MouseButtons.Right)
				{
					this.DataView.SelectedNode = this.DataView.GetNodeAt(e.Location);
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void DataView_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (this.DataView.SelectedNode is OCNode)
			{
				this.editObjectToolStripMenuItem_Click(sender, null);
			}
		}

		private void deleteObjectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.DataView.SelectedNode is OCNode)
			{
				this.DataFile.Drop(((OCNode)this.DataView.SelectedNode).ObjectClass);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if ((!disposing ? false : this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void editObjectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				OCEditorBase ed = null;
				Type editType = ((OCNode)this.DataView.SelectedNode).ObjectClass.MyType();
				OCEditorBase[] editorPlugIns = PluginEngine.GetEditorPlugIns();
				int num = 0;
				while (num < (int)editorPlugIns.Length)
				{
					OCEditorBase tmpEd = editorPlugIns[num];
					if (!tmpEd.IEdit(editType))
					{
						num++;
					}
					else
					{
						ed = tmpEd;
						break;
					}
				}
				List<string> IDs = new List<string>(this.DataFile.IDs);
				if (ed == null)
				{
					throw new Exception(string.Concat("I couldnt find an editor to go with this Object type,", Environment.NewLine, "Please makesure that the plugin creator also created a GUI editor for this object."));
				}
				ed.Collectors = this.DataFile.Collectors;
				ed.MyObject = ((OCNode)this.DataView.SelectedNode).ObjectClass;
				switch (ed.RunEditor(EditorMode.Edit))
				{
					case EditorExitCode.OK:
					{
						this.DataFile.Edit(((OCNode)this.DataView.SelectedNode).ObjectClass, ed.MyObject);
						break;
					}
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void ExportCSVFile(object sender, EventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog()
			{
				Title = "Export to .csv file...",
				Filter = "Comma Separated Values file (*.csv)|*.csv",
				AddExtension = true,
				OverwritePrompt = true
			};
			if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.DataFile.ExportToCSV(sfd.FileName);
			}
		}

		private void ExportTXTFile(object sender, EventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog()
			{
				Title = "Export to .txt file...",
				Filter = "Text file (*.txt)|*.txt",
				AddExtension = true,
				OverwritePrompt = true
			};
			if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.DataFile.ExportToTXT(sfd.FileName);
			}
		}

		private void FileLockIndicator_Click(object sender, EventArgs e)
		{
			if (!this.DataFile.FileLocked)
			{
				bool exitCode = false;
				while (!exitCode)
				{
					try
					{
						this.DataFile.LockFile();
						exitCode = true;
					}
					catch (NullPasswordException nullPasswordException)
					{
						string Password = InputBox.Show("Please enter a password for the file", "Enter New Password...", true);
						if (Password == "")
						{
							MessageBox.Show("Password not set", "File not locked...", MessageBoxButtons.OK);
							return;
						}
						else if (Password.CompareTo(InputBox.Show("Please confirm the password for the file", "Confirm New Password...", true)) == 0)
						{
							this.DataFile.FilePassword = Password;
						}
						else
						{
							MessageBox.Show("Passwords don't match", "Could Not Confirm New Password...", MessageBoxButtons.OK);
						}
					}
				}
			}
			else
			{
				try
				{
					this.DataFile.UnLockFile(InputBox.Show("Please enter the files password", "Password Protected...", true));
				}
				catch (InvalidPasswordException invalidPasswordException)
				{
					MessageBox.Show("Incorrect Password");
				}
				catch (Exception exception)
				{
				}
			}
		}

		private void indexToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void InitializeComponent()
		{
			ComponentResourceManager resources = new ComponentResourceManager(typeof(Main));
			this.splitContainer1 = new SplitContainer();
			this.DataView = new TreeView();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.ObjectViewer = new PropertyGrid();
			this.comboBox1 = new ComboBox();
			this.tableLayoutPanel2 = new TableLayoutPanel();
			this.statusStrip1 = new StatusStrip();
			this.FileLockIndicator = new StatusBarToggleButton();
			this.UpdateProgressBar = new ToolStripProgressBar();
			this.UpdateProgressLabel = new ToolStripStatusLabel();
			this.menuStrip1 = new MenuStrip();
			this.fileToolStripMenuItem = new ToolStripMenuItem();
			this.newToolStripMenuItem = new ToolStripMenuItem();
			this.openToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator = new ToolStripSeparator();
			this.saveToolStripMenuItem = new ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new ToolStripMenuItem();
			this.exportToolStripMenuItem = new ToolStripMenuItem();
			this.tXTFileToolStripMenuItem = new ToolStripMenuItem();
			this.cVSToolStripMenuItem = new ToolStripMenuItem();
			this.excellWorkBookToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.printToolStripMenuItem = new ToolStripMenuItem();
			this.printPreviewToolStripMenuItem = new ToolStripMenuItem();
			this.PageSettingsMenuItem = new ToolStripMenuItem();
			this.PrintSettingsMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.exitToolStripMenuItem = new ToolStripMenuItem();
			this.editToolStripMenuItem = new ToolStripMenuItem();
			this.undoToolStripMenuItem = new ToolStripMenuItem();
			this.redoToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator3 = new ToolStripSeparator();
			this.cutToolStripMenuItem = new ToolStripMenuItem();
			this.copyToolStripMenuItem = new ToolStripMenuItem();
			this.pasteToolStripMenuItem = new ToolStripMenuItem();
			this.toolsToolStripMenuItem = new ToolStripMenuItem();
			this.purgeFileToolStripMenuItem = new ToolStripMenuItem();
			this.securityToolStripMenuItem = new ToolStripMenuItem();
			this.lockFileToolStripMenuItem = new ToolStripMenuItem();
			this.setPasswordToolStripMenuItem = new ToolStripMenuItem();
			this.customizeToolStripMenuItem = new ToolStripMenuItem();
			this.optionsToolStripMenuItem = new ToolStripMenuItem();
			this.helpToolStripMenuItem = new ToolStripMenuItem();
			this.contentsToolStripMenuItem = new ToolStripMenuItem();
			this.indexToolStripMenuItem = new ToolStripMenuItem();
			this.searchToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator5 = new ToolStripSeparator();
			this.aboutToolStripMenuItem = new ToolStripMenuItem();
			this.currentlyLoadedPluginsToolStripMenuItem = new ToolStripMenuItem();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			base.SuspendLayout();
			this.splitContainer1.Dock = DockStyle.Fill;
			this.splitContainer1.Location = new Point(3, 27);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Panel1.Controls.Add(this.DataView);
			this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
			this.splitContainer1.Size = new System.Drawing.Size(776, 510);
			this.splitContainer1.SplitterDistance = 479;
			this.splitContainer1.TabIndex = 0;
			this.DataView.Dock = DockStyle.Fill;
			this.DataView.Location = new Point(0, 0);
			this.DataView.Name = "DataView";
			this.DataView.Size = new System.Drawing.Size(479, 510);
			this.DataView.TabIndex = 0;
			this.DataView.MouseDoubleClick += new MouseEventHandler(this.DataView_MouseDoubleClick);
			this.DataView.MouseClick += new MouseEventHandler(this.DataView_MouseClick);
			this.DataView.AfterSelect += new TreeViewEventHandler(this.DataView_AfterSelect);
			this.DataView.KeyPress += new KeyPressEventHandler(this.DataView_KeyPress);
			this.DataView.KeyUp += new KeyEventHandler(this.DataView_KeyUp);
			this.DataView.Click += new EventHandler(this.DataView_Click);
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel1.Controls.Add(this.ObjectViewer, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.comboBox1, 0, 0);
			this.tableLayoutPanel1.Dock = DockStyle.Fill;
			this.tableLayoutPanel1.Location = new Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(293, 510);
			this.tableLayoutPanel1.TabIndex = 1;
			this.ObjectViewer.Dock = DockStyle.Fill;
			this.ObjectViewer.Location = new Point(3, 28);
			this.ObjectViewer.Name = "ObjectViewer";
			this.ObjectViewer.Size = new System.Drawing.Size(287, 479);
			this.ObjectViewer.TabIndex = 0;
			this.comboBox1.Dock = DockStyle.Fill;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new Point(3, 3);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(287, 21);
			this.comboBox1.Sorted = true;
			this.comboBox1.TabIndex = 1;
			this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel2.Controls.Add(this.splitContainer1, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.statusStrip1, 0, 2);
			this.tableLayoutPanel2.Controls.Add(this.menuStrip1, 0, 0);
			this.tableLayoutPanel2.Dock = DockStyle.Fill;
			this.tableLayoutPanel2.Location = new Point(0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 3;
			this.tableLayoutPanel2.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(782, 560);
			this.tableLayoutPanel2.TabIndex = 1;
			this.statusStrip1.AllowItemReorder = true;
			ToolStripItemCollection items = this.statusStrip1.Items;
			ToolStripItem[] fileLockIndicator = new ToolStripItem[] { this.FileLockIndicator, this.UpdateProgressBar, this.UpdateProgressLabel };
			items.AddRange(fileLockIndicator);
			this.statusStrip1.Location = new Point(0, 540);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(782, 20);
			this.statusStrip1.TabIndex = 1;
			this.statusStrip1.Text = "statusStrip1";
			this.FileLockIndicator.BackColor = SystemColors.ControlDark;
			this.FileLockIndicator.BackgroundImageLayout = ImageLayout.Zoom;
			this.FileLockIndicator.BorderColor = Color.Black;
			this.FileLockIndicator.BorderWidth = 3f;
			this.FileLockIndicator.Checked = false;
			this.FileLockIndicator.CheckedBackColor = Color.Yellow;
			this.FileLockIndicator.CheckedForeColor = Color.Black;
			this.FileLockIndicator.CheckedImage = Resources.locked;
			this.FileLockIndicator.ForeColor = SystemColors.ControlText;
			this.FileLockIndicator.ImageTransparentColor = Color.FromArgb(255, 174, 201);
			this.FileLockIndicator.Margin = new System.Windows.Forms.Padding(2);
			this.FileLockIndicator.Name = "FileLockIndicator";
			this.FileLockIndicator.Padding = new System.Windows.Forms.Padding(1);
			this.FileLockIndicator.ShowBorder = false;
			this.FileLockIndicator.Size = new System.Drawing.Size(53, 16);
			this.FileLockIndicator.Text = "File Lock";
			this.FileLockIndicator.UnCheckedBackColor = SystemColors.ControlDark;
			this.FileLockIndicator.UnCheckedForeColor = SystemColors.ControlText;
			this.FileLockIndicator.UnCheckedImage = Resources.unlocked;
			this.FileLockIndicator.Click += new EventHandler(this.FileLockIndicator_Click);
			this.UpdateProgressBar.AutoSize = false;
			this.UpdateProgressBar.BackColor = SystemColors.Control;
			this.UpdateProgressBar.Name = "UpdateProgressBar";
			this.UpdateProgressBar.Size = new System.Drawing.Size(100, 14);
			this.UpdateProgressBar.Step = 1;
			this.UpdateProgressBar.Visible = false;
			this.UpdateProgressLabel.Name = "UpdateProgressLabel";
			this.UpdateProgressLabel.Size = new System.Drawing.Size(50, 15);
			this.UpdateProgressLabel.Text = "Ready...";
			ToolStripItemCollection toolStripItemCollections = this.menuStrip1.Items;
			fileLockIndicator = new ToolStripItem[] { this.fileToolStripMenuItem, this.editToolStripMenuItem, this.toolsToolStripMenuItem, this.helpToolStripMenuItem };
			toolStripItemCollections.AddRange(fileLockIndicator);
			this.menuStrip1.Location = new Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.RenderMode = ToolStripRenderMode.System;
			this.menuStrip1.Size = new System.Drawing.Size(782, 24);
			this.menuStrip1.TabIndex = 2;
			this.menuStrip1.Text = "menuStrip1";
			ToolStripItemCollection dropDownItems = this.fileToolStripMenuItem.DropDownItems;
			fileLockIndicator = new ToolStripItem[] { this.newToolStripMenuItem, this.openToolStripMenuItem, this.toolStripSeparator, this.saveToolStripMenuItem, this.saveAsToolStripMenuItem, this.exportToolStripMenuItem, this.toolStripSeparator1, this.printToolStripMenuItem, this.printPreviewToolStripMenuItem, this.PageSettingsMenuItem, this.PrintSettingsMenuItem, this.toolStripSeparator2, this.exitToolStripMenuItem };
			dropDownItems.AddRange(fileLockIndicator);
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.fileToolStripMenuItem.Text = "&File";
			this.newToolStripMenuItem.Image = (Image)resources.GetObject("newToolStripMenuItem.Image");
			this.newToolStripMenuItem.ImageTransparentColor = Color.Magenta;
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.ShortcutKeys = Keys.RButton | Keys.MButton | Keys.XButton2 | Keys.Back | Keys.LineFeed | Keys.Clear | Keys.B | Keys.D | Keys.F | Keys.H | Keys.J | Keys.L | Keys.N | Keys.Control;
			this.newToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.newToolStripMenuItem.Text = "&New";
			this.newToolStripMenuItem.Click += new EventHandler(this.newToolStripMenuItem_Click);
			this.openToolStripMenuItem.Image = (Image)resources.GetObject("openToolStripMenuItem.Image");
			this.openToolStripMenuItem.ImageTransparentColor = Color.Magenta;
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.ShortcutKeys = Keys.LButton | Keys.RButton | Keys.Cancel | Keys.MButton | Keys.XButton1 | Keys.XButton2 | Keys.Back | Keys.Tab | Keys.LineFeed | Keys.Clear | Keys.Return | Keys.Enter | Keys.A | Keys.B | Keys.C | Keys.D | Keys.E | Keys.F | Keys.G | Keys.H | Keys.I | Keys.J | Keys.K | Keys.L | Keys.M | Keys.N | Keys.O | Keys.Control;
			this.openToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.openToolStripMenuItem.Text = "&Open";
			this.openToolStripMenuItem.Click += new EventHandler(this.openToolStripMenuItem_Click);
			this.toolStripSeparator.Name = "toolStripSeparator";
			this.toolStripSeparator.Size = new System.Drawing.Size(148, 6);
			this.saveToolStripMenuItem.Image = (Image)resources.GetObject("saveToolStripMenuItem.Image");
			this.saveToolStripMenuItem.ImageTransparentColor = Color.Magenta;
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.ShortcutKeys = Keys.LButton | Keys.RButton | Keys.Cancel | Keys.ShiftKey | Keys.ControlKey | Keys.Menu | Keys.Pause | Keys.A | Keys.B | Keys.C | Keys.P | Keys.Q | Keys.R | Keys.S | Keys.Control;
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.saveToolStripMenuItem.Text = "&Save";
			this.saveToolStripMenuItem.Click += new EventHandler(this.saveToolStripMenuItem_Click);
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.saveAsToolStripMenuItem.Text = "Save &As";
			this.saveAsToolStripMenuItem.Click += new EventHandler(this.saveAsToolStripMenuItem_Click);
			ToolStripItemCollection dropDownItems1 = this.exportToolStripMenuItem.DropDownItems;
			fileLockIndicator = new ToolStripItem[] { this.tXTFileToolStripMenuItem, this.cVSToolStripMenuItem, this.excellWorkBookToolStripMenuItem };
			dropDownItems1.AddRange(fileLockIndicator);
			this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
			this.exportToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.exportToolStripMenuItem.Text = "Export";
			this.tXTFileToolStripMenuItem.Name = "tXTFileToolStripMenuItem";
			this.tXTFileToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
			this.tXTFileToolStripMenuItem.Text = "TXT File";
			this.tXTFileToolStripMenuItem.Click += new EventHandler(this.ExportTXTFile);
			this.cVSToolStripMenuItem.Name = "cVSToolStripMenuItem";
			this.cVSToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
			this.cVSToolStripMenuItem.Text = "CVS";
			this.cVSToolStripMenuItem.Click += new EventHandler(this.ExportCSVFile);
			this.excellWorkBookToolStripMenuItem.Enabled = false;
			this.excellWorkBookToolStripMenuItem.Name = "excellWorkBookToolStripMenuItem";
			this.excellWorkBookToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
			this.excellWorkBookToolStripMenuItem.Text = "Excell WorkBook";
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(148, 6);
			this.printToolStripMenuItem.Image = (Image)resources.GetObject("printToolStripMenuItem.Image");
			this.printToolStripMenuItem.ImageTransparentColor = Color.Magenta;
			this.printToolStripMenuItem.Name = "printToolStripMenuItem";
			this.printToolStripMenuItem.ShortcutKeys = Keys.ShiftKey | Keys.P | Keys.Control;
			this.printToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.printToolStripMenuItem.Text = "&Print";
			this.printToolStripMenuItem.Click += new EventHandler(this.printToolStripMenuItem_Click);
			this.printPreviewToolStripMenuItem.Image = (Image)resources.GetObject("printPreviewToolStripMenuItem.Image");
			this.printPreviewToolStripMenuItem.ImageTransparentColor = Color.Magenta;
			this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
			this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.printPreviewToolStripMenuItem.Text = "Print Pre&view";
			this.printPreviewToolStripMenuItem.Click += new EventHandler(this.printPreviewToolStripMenuItem_Click);
			this.PageSettingsMenuItem.Name = "PageSettingsMenuItem";
			this.PageSettingsMenuItem.Size = new System.Drawing.Size(151, 22);
			this.PageSettingsMenuItem.Text = "Page Settings";
			this.PageSettingsMenuItem.Click += new EventHandler(this.PageSettingsMenuItem_Click);
			this.PrintSettingsMenuItem.Name = "PrintSettingsMenuItem";
			this.PrintSettingsMenuItem.Size = new System.Drawing.Size(151, 22);
			this.PrintSettingsMenuItem.Text = "Print Settings";
			this.PrintSettingsMenuItem.Click += new EventHandler(this.PrintSettingsMenuItem_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(148, 6);
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			this.exitToolStripMenuItem.Click += new EventHandler(this.exitToolStripMenuItem_Click);
			ToolStripItemCollection toolStripItemCollections1 = this.editToolStripMenuItem.DropDownItems;
			fileLockIndicator = new ToolStripItem[] { this.undoToolStripMenuItem, this.redoToolStripMenuItem, this.toolStripSeparator3, this.cutToolStripMenuItem, this.copyToolStripMenuItem, this.pasteToolStripMenuItem };
			toolStripItemCollections1.AddRange(fileLockIndicator);
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.editToolStripMenuItem.Text = "&Edit";
			this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
			this.undoToolStripMenuItem.ShortcutKeys = Keys.RButton | Keys.Back | Keys.LineFeed | Keys.ShiftKey | Keys.Menu | Keys.FinalMode | Keys.B | Keys.H | Keys.J | Keys.P | Keys.R | Keys.X | Keys.Z | Keys.Control;
			this.undoToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.undoToolStripMenuItem.Text = "&Undo";
			this.undoToolStripMenuItem.Click += new EventHandler(this.undoToolStripMenuItem_Click);
			this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
			this.redoToolStripMenuItem.ShortcutKeys = Keys.LButton | Keys.Back | Keys.Tab | Keys.ShiftKey | Keys.ControlKey | Keys.FinalMode | Keys.HanjaMode | Keys.KanjiMode | Keys.A | Keys.H | Keys.I | Keys.P | Keys.Q | Keys.X | Keys.Y | Keys.Control;
			this.redoToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.redoToolStripMenuItem.Text = "&Redo";
			this.redoToolStripMenuItem.Click += new EventHandler(this.redoToolStripMenuItem_Click);
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(147, 6);
			this.cutToolStripMenuItem.Image = (Image)resources.GetObject("cutToolStripMenuItem.Image");
			this.cutToolStripMenuItem.ImageTransparentColor = Color.Magenta;
			this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
			this.cutToolStripMenuItem.ShortcutKeys = Keys.Back | Keys.ShiftKey | Keys.FinalMode | Keys.H | Keys.P | Keys.X | Keys.Control;
			this.cutToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.cutToolStripMenuItem.Text = "Cu&t";
			this.cutToolStripMenuItem.Click += new EventHandler(this.cutToolStripMenuItem_Click);
			this.copyToolStripMenuItem.Image = (Image)resources.GetObject("copyToolStripMenuItem.Image");
			this.copyToolStripMenuItem.ImageTransparentColor = Color.Magenta;
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.ShortcutKeys = Keys.LButton | Keys.RButton | Keys.Cancel | Keys.A | Keys.B | Keys.C | Keys.Control;
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.copyToolStripMenuItem.Text = "&Copy";
			this.copyToolStripMenuItem.Click += new EventHandler(this.copyObjectToolStripMenuItem_Click);
			this.pasteToolStripMenuItem.Image = (Image)resources.GetObject("pasteToolStripMenuItem.Image");
			this.pasteToolStripMenuItem.ImageTransparentColor = Color.Magenta;
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			this.pasteToolStripMenuItem.ShortcutKeys = Keys.RButton | Keys.MButton | Keys.XButton2 | Keys.ShiftKey | Keys.Menu | Keys.Capital | Keys.CapsLock | Keys.B | Keys.D | Keys.F | Keys.P | Keys.R | Keys.T | Keys.V | Keys.Control;
			this.pasteToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.pasteToolStripMenuItem.Text = "&Paste";
			this.pasteToolStripMenuItem.Click += new EventHandler(this.pasteToolStripMenuItem_Click);
			ToolStripItemCollection dropDownItems2 = this.toolsToolStripMenuItem.DropDownItems;
			fileLockIndicator = new ToolStripItem[] { this.purgeFileToolStripMenuItem, this.securityToolStripMenuItem, this.customizeToolStripMenuItem, this.optionsToolStripMenuItem };
			dropDownItems2.AddRange(fileLockIndicator);
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			this.toolsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.toolsToolStripMenuItem.Text = "&Tools";
			this.purgeFileToolStripMenuItem.Name = "purgeFileToolStripMenuItem";
			this.purgeFileToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.purgeFileToolStripMenuItem.Text = "Purge File...";
			this.purgeFileToolStripMenuItem.Click += new EventHandler(this.purgeFileToolStripMenuItem_Click);
			ToolStripItemCollection toolStripItemCollections2 = this.securityToolStripMenuItem.DropDownItems;
			fileLockIndicator = new ToolStripItem[] { this.lockFileToolStripMenuItem, this.setPasswordToolStripMenuItem };
			toolStripItemCollections2.AddRange(fileLockIndicator);
			this.securityToolStripMenuItem.Name = "securityToolStripMenuItem";
			this.securityToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.securityToolStripMenuItem.Text = "S&ecurity";
			this.lockFileToolStripMenuItem.Name = "lockFileToolStripMenuItem";
			this.lockFileToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
			this.lockFileToolStripMenuItem.Text = "Edit &Lock";
			this.lockFileToolStripMenuItem.Click += new EventHandler(this.lockFileToolStripMenuItem_Click);
			this.setPasswordToolStripMenuItem.Name = "setPasswordToolStripMenuItem";
			this.setPasswordToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
			this.setPasswordToolStripMenuItem.Text = "&Set/Change Password";
			this.setPasswordToolStripMenuItem.Click += new EventHandler(this.setPasswordToolStripMenuItem_Click);
			this.customizeToolStripMenuItem.Name = "customizeToolStripMenuItem";
			this.customizeToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.customizeToolStripMenuItem.Text = "&Customize";
			this.customizeToolStripMenuItem.Visible = false;
			this.customizeToolStripMenuItem.Click += new EventHandler(this.customizeToolStripMenuItem_Click);
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.optionsToolStripMenuItem.Text = "&Options";
			this.optionsToolStripMenuItem.Visible = false;
			this.optionsToolStripMenuItem.Click += new EventHandler(this.optionsToolStripMenuItem_Click);
			ToolStripItemCollection dropDownItems3 = this.helpToolStripMenuItem.DropDownItems;
			fileLockIndicator = new ToolStripItem[] { this.contentsToolStripMenuItem, this.indexToolStripMenuItem, this.searchToolStripMenuItem, this.toolStripSeparator5, this.aboutToolStripMenuItem, this.currentlyLoadedPluginsToolStripMenuItem };
			dropDownItems3.AddRange(fileLockIndicator);
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
			this.helpToolStripMenuItem.Text = "&Help";
			this.contentsToolStripMenuItem.Enabled = false;
			this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
			this.contentsToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
			this.contentsToolStripMenuItem.Text = "&Contents";
			this.contentsToolStripMenuItem.Click += new EventHandler(this.contentsToolStripMenuItem_Click);
			this.indexToolStripMenuItem.Enabled = false;
			this.indexToolStripMenuItem.Name = "indexToolStripMenuItem";
			this.indexToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
			this.indexToolStripMenuItem.Text = "&Index";
			this.indexToolStripMenuItem.Click += new EventHandler(this.indexToolStripMenuItem_Click);
			this.searchToolStripMenuItem.Enabled = false;
			this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
			this.searchToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
			this.searchToolStripMenuItem.Text = "&Search";
			this.searchToolStripMenuItem.Click += new EventHandler(this.searchToolStripMenuItem_Click);
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(213, 6);
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
			this.aboutToolStripMenuItem.Text = "&About...";
			this.aboutToolStripMenuItem.Click += new EventHandler(this.aboutToolStripMenuItem_Click);
			this.currentlyLoadedPluginsToolStripMenuItem.Name = "currentlyLoadedPluginsToolStripMenuItem";
			this.currentlyLoadedPluginsToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
			this.currentlyLoadedPluginsToolStripMenuItem.Text = "Currently Loaded Plugins...";
			this.currentlyLoadedPluginsToolStripMenuItem.Click += new EventHandler(this.currentlyLoadedPluginsToolStripMenuItem_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(782, 560);
			base.Controls.Add(this.tableLayoutPanel2);
			this.DoubleBuffered = true;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "Main";
			this.Text = "Form1";
			base.FormClosing += new FormClosingEventHandler(this.Main_FormClosing);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			base.ResumeLayout(false);
		}

		private void lockFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!this.DataFile.FileLocked)
			{
				bool exitCode = false;
				while (!exitCode)
				{
					try
					{
						this.DataFile.LockFile();
						exitCode = true;
					}
					catch (NullPasswordException nullPasswordException)
					{
						string Password = InputBox.Show("Please enter a password for the file", "Enter New Password...", true);
						if (Password == "")
						{
							MessageBox.Show("Password not set", "File not locked...", MessageBoxButtons.OK);
							return;
						}
						else if (Password.CompareTo(InputBox.Show("Please confirm the password for the file", "Confirm New Password...", true)) == 0)
						{
							this.DataFile.FilePassword = Password;
						}
						else
						{
							MessageBox.Show("Passwords don't match", "Could Not Confirm New Password...", MessageBoxButtons.OK);
						}
					}
				}
			}
			else
			{
				try
				{
					this.DataFile.UnLockFile(InputBox.Show("Please enter the files password", "Password Protected...", true));
				}
				catch (InvalidPasswordException invalidPasswordException)
				{
					MessageBox.Show("Incorrect Password");
				}
				catch (Exception exception)
				{
				}
			}
		}

		private void Main_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!this.CheckForSave())
			{
				e.Cancel = true;
			}
		}

		private void newObjectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				OCEditorBase ed = null;
				Type editType = ((OCCNode)this.DataView.SelectedNode).Collector.CollectionType;
				OCEditorBase[] editorPlugIns = PluginEngine.GetEditorPlugIns();
				int num = 0;
				while (num < (int)editorPlugIns.Length)
				{
					OCEditorBase tmpEd = editorPlugIns[num];
					if (!tmpEd.IEdit(editType))
					{
						num++;
					}
					else
					{
						ed = tmpEd;
						break;
					}
				}
				List<string> IDs = new List<string>(this.DataFile.IDs);
				if (ed == null)
				{
					throw new Exception(string.Concat("I couldnt find an editor to go with this Object type,", Environment.NewLine, "Please makesure that the plugin creator also created a GUI editor for this object."));
				}
				ed.Collectors = this.DataFile.Collectors;
				ed.MyObject = (ObjectClassBase)Activator.CreateInstance(editType);
				ed.MyObject.ID = this.DataFile.GenerateIDCode();
				switch (ed.RunEditor(EditorMode.New))
				{
					case EditorExitCode.OK:
					{
						this.DataFile.Add(ed.MyObject);
						break;
					}
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.CheckForSave())
				{
					this.DataView.Nodes.Clear();
					this.Orphans.Nodes.Clear();
					this.DataFile = new NTDataFile();
					this.DataFile.Updating += new NTEventHandler<UpdaterEventArgs>(this.DataFile_Updating);
					this.DataFile.Update += new NTEventHandler<UpdateProgressEventArgs>(this.DataFile_Update);
					this.DataFile.Updated += new NTEventHandler(this.DataFile_Updated);
					this.DataFile.EventOrphansChanged += new NTEventHandler<ItemChangedArgs>(this.DataFile_EventOrphansChanged);
					this.DataFile.LockStatusChange += new NTEventHandler(this.DataFile_LockStatusChange);
					this.DataFile.getTreeNodes(this.DataView.Nodes, this.OCCMenuStrip, this.OCMenuStrip);
					this.DataView.Nodes.Add(this.Orphans);
					this.Title = this.DataFile.FileName;
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.CheckForSave())
				{
					OpenFileDialog OFD = new OpenFileDialog()
					{
						Filter = "NewTerra Dat Files (*.ntx)|*.ntx",
						SupportMultiDottedExtensions = true,
						Multiselect = false
					};
					if (OFD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.Orphans.Nodes.Clear();
						this.DataView.Nodes.Clear();
						this.DataFile = new NTDataFile(OFD.FileName);
						this.DataFile.Updating += new NTEventHandler<UpdaterEventArgs>(this.DataFile_Updating);
						this.DataFile.Update += new NTEventHandler<UpdateProgressEventArgs>(this.DataFile_Update);
						this.DataFile.Updated += new NTEventHandler(this.DataFile_Updated);
						this.DataFile.EventOrphansChanged += new NTEventHandler<ItemChangedArgs>(this.DataFile_EventOrphansChanged);
						this.DataFile.LockStatusChange += new NTEventHandler(this.DataFile_LockStatusChange);
						this.bgw.RunWorkerAsync();
						this.Title = this.DataFile.FileName;
					}
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void PageSettingsMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				this.setUpPrint();
				PageSetupDialog myEngineSettings = this._PrintEngine.ShowPageSettings();
				if (myEngineSettings.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					NTAF.PrintEngine.Properties.Settings.Default.printSettings = myEngineSettings.PageSettings;
					NTAF.PrintEngine.Properties.Settings.Default.Save();
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				CopyClip.CopyFromClipboard(this.DataFile);
			}
			catch (Exception exception)
			{
			}
		}

		private void previewObjectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.DataView.SelectedNode is OCNode)
			{
				ObjectClassBase obj = ((OCNode)this.DataView.SelectedNode).ObjectClass;
				if (obj != null)
				{
					(new ObjectPreview(obj)).ShowDialog();
				}
			}
		}

		private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				this.setUpPrint();
				this._PrintEngine.ShowPreview().ShowDialog();
			}
			catch (Exception exception)
			{
			}
		}

		private void PrintSettingsMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				this.setUpPrint();
				FontDialog myPrintFontSettings = this._PrintEngine.ShowFontDialog();
				if (myPrintFontSettings.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this._PrintEngine.PrintFont = myPrintFontSettings.Font;
					NTAF.PrintEngine.Properties.Settings.Default.printFontSettings = myPrintFontSettings.Font;
					NTAF.PrintEngine.Properties.Settings.Default.Save();
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void printToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				PrintDialog pDialog = this._PrintEngine.ShowPrintDialog();
				this.setUpPrint();
				if (pDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					NTAF.PrintEngine.Properties.Settings.Default.printerSettings = pDialog.PrinterSettings;
					this._PrintEngine.Print();
					NTAF.PrintEngine.Properties.Settings.Default.Save();
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void purgeFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.DataFile.PurgeFile();
		}

		private void redoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				this.DataFile.DoRedo();
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				this.DataFile.SaveAs();
				this.Title = this.DataFile.FileName;
			}
			catch (Exception exception)
			{
			}
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				this.DataFile.Save();
				this.Title = this.DataFile.FileName;
			}
			catch (Exception exception)
			{
			}
		}

		private void searchToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void setPasswordToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				string Password = InputBox.Show("Please enter a password for the file", "Enter New Password...", true);
				if (Password == "")
				{
					MessageBox.Show("Password not set", "File not locked...", MessageBoxButtons.OK);
					return;
				}
				else if (Password.CompareTo(InputBox.Show("Please confirm the password for the file", "Confirm New Password...", true)) == 0)
				{
					this.DataFile.FilePassword = Password;
				}
				else
				{
					MessageBox.Show("Passwords don't match", "Could Not Confirm New Password...", MessageBoxButtons.OK);
				}
			}
			catch (FileLockedException fileLockedException)
			{
				MessageBox.Show("Password not set", "File not locked...", MessageBoxButtons.OK);
				return;
			}
		}

		public void setUpPrint()
		{
			if (this._PrintEngine == null)
			{
				this._PrintEngine = new PrintEngine(this.DataFile.FileName);
			}
			List<IPrintable> printObjects = new List<IPrintable>();
			ObjectClassBase[] allData = this.DataFile.AllData;
			for (int i = 0; i < (int)allData.Length; i++)
			{
				object obj = allData[i];
				if (obj is IPrintable)
				{
					printObjects.Add((IPrintable)obj);
					printObjects.Add(new PrintBreakLine());
				}
			}
			this._PrintEngine.ResetPrintables(printObjects.ToArray());
		}

		private void undoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				this.DataFile.DoUndo();
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}
	}
}