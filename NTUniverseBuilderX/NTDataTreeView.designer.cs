namespace NTAF.UniverseBuilder.WinGui {
    partial class NTDataTreeView {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.Data = new System.Windows.Forms.TreeView();
            this.NTObjectEditingMenu = new System.Windows.Forms.ContextMenuStrip( this.components );
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RootEditingMenu = new System.Windows.Forms.ContextMenuStrip( this.components );
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutNode = new System.Windows.Forms.Label();
            this.ObjectViewer = new System.Windows.Forms.PropertyGrid();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.NTObjectEditingMenu.SuspendLayout();
            this.RootEditingMenu.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Data
            // 
            this.Data.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Data.Location = new System.Drawing.Point( 0, 0 );
            this.Data.Name = "Data";
            this.Data.Size = new System.Drawing.Size( 538, 697 );
            this.Data.TabIndex = 0;
            this.Data.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler( this.Data_NodeMouseDoubleClick );
            this.Data.MouseClick += new System.Windows.Forms.MouseEventHandler( this.NTDataTreeView_MouseClick );
            this.Data.AfterSelect += new System.Windows.Forms.TreeViewEventHandler( this.Data_AfterSelect );
            this.Data.KeyUp += new System.Windows.Forms.KeyEventHandler( this.Data_KeyUp );
            // 
            // NTObjectEditingMenu
            // 
            this.NTObjectEditingMenu.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.previewToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.importToolStripMenuItem} );
            this.NTObjectEditingMenu.Name = "NTObjectEditing";
            this.NTObjectEditingMenu.Size = new System.Drawing.Size( 150, 114 );
            this.NTObjectEditingMenu.Opening += new System.ComponentModel.CancelEventHandler( this.NTObjectEditingMenu_Opening );
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size( 149, 22 );
            this.editToolStripMenuItem.Text = "View/Edit";
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size( 149, 22 );
            this.removeToolStripMenuItem.Text = "Remove";
            // 
            // previewToolStripMenuItem
            // 
            this.previewToolStripMenuItem.Name = "previewToolStripMenuItem";
            this.previewToolStripMenuItem.Size = new System.Drawing.Size( 149, 22 );
            this.previewToolStripMenuItem.Text = "Preview";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ( ( System.Windows.Forms.Keys )( ( System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C ) ) );
            this.copyToolStripMenuItem.Size = new System.Drawing.Size( 149, 22 );
            this.copyToolStripMenuItem.Text = "Copy";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Enabled = false;
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size( 149, 22 );
            this.importToolStripMenuItem.Text = "Import";
            this.importToolStripMenuItem.Click += new System.EventHandler( this.importToolStripMenuItem_Click );
            // 
            // RootEditingMenu
            // 
            this.RootEditingMenu.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem} );
            this.RootEditingMenu.Name = "NTObjectEditing";
            this.RootEditingMenu.Size = new System.Drawing.Size( 107, 26 );
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size( 106, 22 );
            this.newToolStripMenuItem.Text = "New";
            // 
            // AboutNode
            // 
            this.AboutNode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AboutNode.Location = new System.Drawing.Point( 0, 0 );
            this.AboutNode.Name = "AboutNode";
            this.AboutNode.Size = new System.Drawing.Size( 258, 697 );
            this.AboutNode.TabIndex = 2;
            this.AboutNode.Paint += new System.Windows.Forms.PaintEventHandler( this.AboutNode_Paint );
            // 
            // ObjectViewer
            // 
            this.ObjectViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ObjectViewer.Location = new System.Drawing.Point( 0, 0 );
            this.ObjectViewer.Name = "ObjectViewer";
            this.ObjectViewer.Size = new System.Drawing.Size( 258, 697 );
            this.ObjectViewer.TabIndex = 3;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point( 0, 0 );
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add( this.Data );
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add( this.AboutNode );
            this.splitContainer1.Panel2.Controls.Add( this.ObjectViewer );
            this.splitContainer1.Size = new System.Drawing.Size( 800, 697 );
            this.splitContainer1.SplitterDistance = 538;
            this.splitContainer1.TabIndex = 4;
            // 
            // NTDataTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.splitContainer1 );
            this.Name = "NTDataTreeView";
            this.Size = new System.Drawing.Size( 800, 697 );
            this.NTObjectEditingMenu.ResumeLayout( false );
            this.RootEditingMenu.ResumeLayout( false );
            this.splitContainer1.Panel1.ResumeLayout( false );
            this.splitContainer1.Panel2.ResumeLayout( false );
            this.splitContainer1.ResumeLayout( false );
            this.ResumeLayout( false );

        }
        private System.Windows.Forms.PropertyGrid ObjectViewer;
        public System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        public System.Windows.Forms.ContextMenuStrip NTObjectEditingMenu;
        private System.Windows.Forms.ContextMenuStrip RootEditingMenu;
        public System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;

        #endregion

        private System.Windows.Forms.Label AboutNode;
        private System.Windows.Forms.SplitContainer splitContainer1;
        public System.Windows.Forms.ToolStripMenuItem previewToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.TreeView Data;
    }
}
