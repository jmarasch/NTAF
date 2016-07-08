
namespace UniverseBuilder {
    partial class Main {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing ) {
            if ( disposing && ( components != null ) ) {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( Main ) );
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.DataView = new System.Windows.Forms.TreeView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ObjectViewer = new System.Windows.Forms.PropertyGrid();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.FileLockIndicator = new NTAF.Controls.StatusBar.StatusBarToggleButton();
            this.UpdateProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.UpdateProgressLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tXTFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cVSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.excellWorkBookToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PageSettingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PrintSettingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.purgeFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.securityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lockFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setPasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.currentlyLoadedPluginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point( 3, 27 );
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add( this.DataView );
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add( this.tableLayoutPanel1 );
            this.splitContainer1.Size = new System.Drawing.Size( 776, 510 );
            this.splitContainer1.SplitterDistance = 479;
            this.splitContainer1.TabIndex = 0;
            // 
            // DataView
            // 
            this.DataView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataView.Location = new System.Drawing.Point( 0, 0 );
            this.DataView.Name = "DataView";
            this.DataView.Size = new System.Drawing.Size( 479, 510 );
            this.DataView.TabIndex = 0;
            this.DataView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler( this.DataView_MouseDoubleClick );
            this.DataView.MouseClick += new System.Windows.Forms.MouseEventHandler( this.DataView_MouseClick );
            this.DataView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler( this.DataView_AfterSelect );
            this.DataView.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.DataView_KeyPress );
            this.DataView.KeyUp += new System.Windows.Forms.KeyEventHandler( this.DataView_KeyUp );
            this.DataView.Click += new System.EventHandler( this.DataView_Click );
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 100F ) );
            this.tableLayoutPanel1.Controls.Add( this.ObjectViewer, 0, 1 );
            this.tableLayoutPanel1.Controls.Add( this.comboBox1, 0, 0 );
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point( 0, 0 );
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Absolute, 25F ) );
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 100F ) );
            this.tableLayoutPanel1.Size = new System.Drawing.Size( 293, 510 );
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // ObjectViewer
            // 
            this.ObjectViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ObjectViewer.Location = new System.Drawing.Point( 3, 28 );
            this.ObjectViewer.Name = "ObjectViewer";
            this.ObjectViewer.Size = new System.Drawing.Size( 287, 479 );
            this.ObjectViewer.TabIndex = 0;
            // 
            // comboBox1
            // 
            this.comboBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point( 3, 3 );
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size( 287, 21 );
            this.comboBox1.Sorted = true;
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler( this.comboBox1_SelectedIndexChanged );
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 100F ) );
            this.tableLayoutPanel2.Controls.Add( this.splitContainer1, 0, 1 );
            this.tableLayoutPanel2.Controls.Add( this.statusStrip1, 0, 2 );
            this.tableLayoutPanel2.Controls.Add( this.menuStrip1, 0, 0 );
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point( 0, 0 );
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 100F ) );
            this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Absolute, 20F ) );
            this.tableLayoutPanel2.Size = new System.Drawing.Size( 782, 560 );
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.AllowItemReorder = true;
            this.statusStrip1.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.FileLockIndicator,
            this.UpdateProgressBar,
            this.UpdateProgressLabel} );
            this.statusStrip1.Location = new System.Drawing.Point( 0, 540 );
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size( 782, 20 );
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // FileLockIndicator
            // 
            this.FileLockIndicator.BackColor = System.Drawing.SystemColors.ControlDark;
            this.FileLockIndicator.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.FileLockIndicator.BorderColor = System.Drawing.Color.Black;
            this.FileLockIndicator.BorderWidth = 3F;
            this.FileLockIndicator.Checked = false;
            this.FileLockIndicator.CheckedBackColor = System.Drawing.Color.Yellow;
            this.FileLockIndicator.CheckedForeColor = System.Drawing.Color.Black;
            this.FileLockIndicator.CheckedImage = global::UniverseBuilder.Properties.Resources.locked;
            this.FileLockIndicator.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FileLockIndicator.ImageTransparentColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 255 ) ) ) ), ( ( int )( ( ( byte )( 174 ) ) ) ), ( ( int )( ( ( byte )( 201 ) ) ) ) );
            this.FileLockIndicator.Margin = new System.Windows.Forms.Padding( 2 );
            this.FileLockIndicator.Name = "FileLockIndicator";
            this.FileLockIndicator.Padding = new System.Windows.Forms.Padding( 1 );
            this.FileLockIndicator.ShowBorder = false;
            this.FileLockIndicator.Size = new System.Drawing.Size( 53, 16 );
            this.FileLockIndicator.Text = "File Lock";
            this.FileLockIndicator.UnCheckedBackColor = System.Drawing.SystemColors.ControlDark;
            this.FileLockIndicator.UnCheckedForeColor = System.Drawing.SystemColors.ControlText;
            this.FileLockIndicator.UnCheckedImage = global::UniverseBuilder.Properties.Resources.unlocked;
            this.FileLockIndicator.Click += new System.EventHandler( this.FileLockIndicator_Click );
            // 
            // UpdateProgressBar
            // 
            this.UpdateProgressBar.AutoSize = false;
            this.UpdateProgressBar.BackColor = System.Drawing.SystemColors.Control;
            this.UpdateProgressBar.Name = "UpdateProgressBar";
            this.UpdateProgressBar.Size = new System.Drawing.Size( 100, 14 );
            this.UpdateProgressBar.Step = 1;
            this.UpdateProgressBar.Visible = false;
            // 
            // UpdateProgressLabel
            // 
            this.UpdateProgressLabel.Name = "UpdateProgressLabel";
            this.UpdateProgressLabel.Size = new System.Drawing.Size( 50, 15 );
            this.UpdateProgressLabel.Text = "Ready...";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem} );
            this.menuStrip1.Location = new System.Drawing.Point( 0, 0 );
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size( 782, 24 );
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.toolStripSeparator1,
            this.printToolStripMenuItem,
            this.printPreviewToolStripMenuItem,
            this.PageSettingsMenuItem,
            this.PrintSettingsMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem} );
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size( 35, 20 );
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = ( ( System.Drawing.Image )( resources.GetObject( "newToolStripMenuItem.Image" ) ) );
            this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ( ( System.Windows.Forms.Keys )( ( System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N ) ) );
            this.newToolStripMenuItem.Size = new System.Drawing.Size( 151, 22 );
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler( this.newToolStripMenuItem_Click );
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ( ( System.Drawing.Image )( resources.GetObject( "openToolStripMenuItem.Image" ) ) );
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ( ( System.Windows.Forms.Keys )( ( System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O ) ) );
            this.openToolStripMenuItem.Size = new System.Drawing.Size( 151, 22 );
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler( this.openToolStripMenuItem_Click );
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size( 148, 6 );
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ( ( System.Drawing.Image )( resources.GetObject( "saveToolStripMenuItem.Image" ) ) );
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ( ( System.Windows.Forms.Keys )( ( System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S ) ) );
            this.saveToolStripMenuItem.Size = new System.Drawing.Size( 151, 22 );
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler( this.saveToolStripMenuItem_Click );
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size( 151, 22 );
            this.saveAsToolStripMenuItem.Text = "Save &As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler( this.saveAsToolStripMenuItem_Click );
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.tXTFileToolStripMenuItem,
            this.cVSToolStripMenuItem,
            this.excellWorkBookToolStripMenuItem} );
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size( 151, 22 );
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // tXTFileToolStripMenuItem
            // 
            this.tXTFileToolStripMenuItem.Name = "tXTFileToolStripMenuItem";
            this.tXTFileToolStripMenuItem.Size = new System.Drawing.Size( 163, 22 );
            this.tXTFileToolStripMenuItem.Text = "TXT File";
            this.tXTFileToolStripMenuItem.Click += new System.EventHandler( this.ExportTXTFile );
            // 
            // cVSToolStripMenuItem
            // 
            this.cVSToolStripMenuItem.Name = "cVSToolStripMenuItem";
            this.cVSToolStripMenuItem.Size = new System.Drawing.Size( 163, 22 );
            this.cVSToolStripMenuItem.Text = "CVS";
            this.cVSToolStripMenuItem.Click += new System.EventHandler( this.ExportCSVFile );
            // 
            // excellWorkBookToolStripMenuItem
            // 
            this.excellWorkBookToolStripMenuItem.Enabled = false;
            this.excellWorkBookToolStripMenuItem.Name = "excellWorkBookToolStripMenuItem";
            this.excellWorkBookToolStripMenuItem.Size = new System.Drawing.Size( 163, 22 );
            this.excellWorkBookToolStripMenuItem.Text = "Excell WorkBook";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size( 148, 6 );
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Image = ( ( System.Drawing.Image )( resources.GetObject( "printToolStripMenuItem.Image" ) ) );
            this.printToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.ShortcutKeys = ( ( System.Windows.Forms.Keys )( ( System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P ) ) );
            this.printToolStripMenuItem.Size = new System.Drawing.Size( 151, 22 );
            this.printToolStripMenuItem.Text = "&Print";
            this.printToolStripMenuItem.Click += new System.EventHandler( this.printToolStripMenuItem_Click );
            // 
            // printPreviewToolStripMenuItem
            // 
            this.printPreviewToolStripMenuItem.Image = ( ( System.Drawing.Image )( resources.GetObject( "printPreviewToolStripMenuItem.Image" ) ) );
            this.printPreviewToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
            this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size( 151, 22 );
            this.printPreviewToolStripMenuItem.Text = "Print Pre&view";
            this.printPreviewToolStripMenuItem.Click += new System.EventHandler( this.printPreviewToolStripMenuItem_Click );
            // 
            // PageSettingsMenuItem
            // 
            this.PageSettingsMenuItem.Name = "PageSettingsMenuItem";
            this.PageSettingsMenuItem.Size = new System.Drawing.Size( 151, 22 );
            this.PageSettingsMenuItem.Text = "Page Settings";
            this.PageSettingsMenuItem.Click += new System.EventHandler( this.PageSettingsMenuItem_Click );
            // 
            // PrintSettingsMenuItem
            // 
            this.PrintSettingsMenuItem.Name = "PrintSettingsMenuItem";
            this.PrintSettingsMenuItem.Size = new System.Drawing.Size( 151, 22 );
            this.PrintSettingsMenuItem.Text = "Print Settings";
            this.PrintSettingsMenuItem.Click += new System.EventHandler( this.PrintSettingsMenuItem_Click );
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size( 148, 6 );
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size( 151, 22 );
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler( this.exitToolStripMenuItem_Click );
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator3,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem} );
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size( 37, 20 );
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ( ( System.Windows.Forms.Keys )( ( System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z ) ) );
            this.undoToolStripMenuItem.Size = new System.Drawing.Size( 150, 22 );
            this.undoToolStripMenuItem.Text = "&Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler( this.undoToolStripMenuItem_Click );
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ( ( System.Windows.Forms.Keys )( ( System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y ) ) );
            this.redoToolStripMenuItem.Size = new System.Drawing.Size( 150, 22 );
            this.redoToolStripMenuItem.Text = "&Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler( this.redoToolStripMenuItem_Click );
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size( 147, 6 );
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Image = ( ( System.Drawing.Image )( resources.GetObject( "cutToolStripMenuItem.Image" ) ) );
            this.cutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.ShortcutKeys = ( ( System.Windows.Forms.Keys )( ( System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X ) ) );
            this.cutToolStripMenuItem.Size = new System.Drawing.Size( 150, 22 );
            this.cutToolStripMenuItem.Text = "Cu&t";
            this.cutToolStripMenuItem.Click += new System.EventHandler( this.cutToolStripMenuItem_Click );
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = ( ( System.Drawing.Image )( resources.GetObject( "copyToolStripMenuItem.Image" ) ) );
            this.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ( ( System.Windows.Forms.Keys )( ( System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C ) ) );
            this.copyToolStripMenuItem.Size = new System.Drawing.Size( 150, 22 );
            this.copyToolStripMenuItem.Text = "&Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler( this.copyObjectToolStripMenuItem_Click );
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Image = ( ( System.Drawing.Image )( resources.GetObject( "pasteToolStripMenuItem.Image" ) ) );
            this.pasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ( ( System.Windows.Forms.Keys )( ( System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V ) ) );
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size( 150, 22 );
            this.pasteToolStripMenuItem.Text = "&Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler( this.pasteToolStripMenuItem_Click );
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.purgeFileToolStripMenuItem,
            this.securityToolStripMenuItem,
            this.customizeToolStripMenuItem,
            this.optionsToolStripMenuItem} );
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size( 44, 20 );
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // purgeFileToolStripMenuItem
            // 
            this.purgeFileToolStripMenuItem.Name = "purgeFileToolStripMenuItem";
            this.purgeFileToolStripMenuItem.Size = new System.Drawing.Size( 144, 22 );
            this.purgeFileToolStripMenuItem.Text = "Purge File...";
            this.purgeFileToolStripMenuItem.Click += new System.EventHandler( this.purgeFileToolStripMenuItem_Click );
            // 
            // securityToolStripMenuItem
            // 
            this.securityToolStripMenuItem.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.lockFileToolStripMenuItem,
            this.setPasswordToolStripMenuItem} );
            this.securityToolStripMenuItem.Name = "securityToolStripMenuItem";
            this.securityToolStripMenuItem.Size = new System.Drawing.Size( 144, 22 );
            this.securityToolStripMenuItem.Text = "S&ecurity";
            // 
            // lockFileToolStripMenuItem
            // 
            this.lockFileToolStripMenuItem.Name = "lockFileToolStripMenuItem";
            this.lockFileToolStripMenuItem.Size = new System.Drawing.Size( 191, 22 );
            this.lockFileToolStripMenuItem.Text = "Edit &Lock";
            this.lockFileToolStripMenuItem.Click += new System.EventHandler( this.lockFileToolStripMenuItem_Click );
            // 
            // setPasswordToolStripMenuItem
            // 
            this.setPasswordToolStripMenuItem.Name = "setPasswordToolStripMenuItem";
            this.setPasswordToolStripMenuItem.Size = new System.Drawing.Size( 191, 22 );
            this.setPasswordToolStripMenuItem.Text = "&Set/Change Password";
            this.setPasswordToolStripMenuItem.Click += new System.EventHandler( this.setPasswordToolStripMenuItem_Click );
            // 
            // customizeToolStripMenuItem
            // 
            this.customizeToolStripMenuItem.Name = "customizeToolStripMenuItem";
            this.customizeToolStripMenuItem.Size = new System.Drawing.Size( 144, 22 );
            this.customizeToolStripMenuItem.Text = "&Customize";
            this.customizeToolStripMenuItem.Visible = false;
            this.customizeToolStripMenuItem.Click += new System.EventHandler( this.customizeToolStripMenuItem_Click );
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size( 144, 22 );
            this.optionsToolStripMenuItem.Text = "&Options";
            this.optionsToolStripMenuItem.Visible = false;
            this.optionsToolStripMenuItem.Click += new System.EventHandler( this.optionsToolStripMenuItem_Click );
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.contentsToolStripMenuItem,
            this.indexToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.toolStripSeparator5,
            this.aboutToolStripMenuItem,
            this.currentlyLoadedPluginsToolStripMenuItem} );
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size( 40, 20 );
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // contentsToolStripMenuItem
            // 
            this.contentsToolStripMenuItem.Enabled = false;
            this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
            this.contentsToolStripMenuItem.Size = new System.Drawing.Size( 216, 22 );
            this.contentsToolStripMenuItem.Text = "&Contents";
            this.contentsToolStripMenuItem.Click += new System.EventHandler( this.contentsToolStripMenuItem_Click );
            // 
            // indexToolStripMenuItem
            // 
            this.indexToolStripMenuItem.Enabled = false;
            this.indexToolStripMenuItem.Name = "indexToolStripMenuItem";
            this.indexToolStripMenuItem.Size = new System.Drawing.Size( 216, 22 );
            this.indexToolStripMenuItem.Text = "&Index";
            this.indexToolStripMenuItem.Click += new System.EventHandler( this.indexToolStripMenuItem_Click );
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Enabled = false;
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size( 216, 22 );
            this.searchToolStripMenuItem.Text = "&Search";
            this.searchToolStripMenuItem.Click += new System.EventHandler( this.searchToolStripMenuItem_Click );
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size( 213, 6 );
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size( 216, 22 );
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler( this.aboutToolStripMenuItem_Click );
            // 
            // currentlyLoadedPluginsToolStripMenuItem
            // 
            this.currentlyLoadedPluginsToolStripMenuItem.Name = "currentlyLoadedPluginsToolStripMenuItem";
            this.currentlyLoadedPluginsToolStripMenuItem.Size = new System.Drawing.Size( 216, 22 );
            this.currentlyLoadedPluginsToolStripMenuItem.Text = "Currently Loaded Plugins...";
            this.currentlyLoadedPluginsToolStripMenuItem.Click += new System.EventHandler( this.currentlyLoadedPluginsToolStripMenuItem_Click );
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 782, 560 );
            this.Controls.Add( this.tableLayoutPanel2 );
            this.DoubleBuffered = true;
            this.Icon = ( ( System.Drawing.Icon )( resources.GetObject( "$this.Icon" ) ) );
            this.Name = "Main";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler( this.Main_FormClosing );
            this.splitContainer1.Panel1.ResumeLayout( false );
            this.splitContainer1.Panel2.ResumeLayout( false );
            this.splitContainer1.ResumeLayout( false );
            this.tableLayoutPanel1.ResumeLayout( false );
            this.tableLayoutPanel2.ResumeLayout( false );
            this.tableLayoutPanel2.PerformLayout();
            this.statusStrip1.ResumeLayout( false );
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout( false );
            this.menuStrip1.PerformLayout();
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PropertyGrid ObjectViewer;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TreeView DataView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel UpdateProgressLabel;
        private System.Windows.Forms.ToolStripProgressBar UpdateProgressBar;
        private System.Windows.Forms.ToolStripMenuItem purgeFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem securityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lockFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setPasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PageSettingsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PrintSettingsMenuItem;
        private NTAF.Controls.StatusBar.StatusBarToggleButton FileLockIndicator;
        private System.Windows.Forms.ToolStripMenuItem currentlyLoadedPluginsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tXTFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cVSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem excellWorkBookToolStripMenuItem;

    }
}

