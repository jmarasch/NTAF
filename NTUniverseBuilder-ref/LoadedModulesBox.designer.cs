namespace NTAF.UniverseBuilder.WinGui {
    partial class LoadedModuleBox {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            this.tableLayoutPanelx = new System.Windows.Forms.TableLayoutPanel();
            this.okButton = new System.Windows.Forms.Button();
            this.tbLoadedMods = new System.Windows.Forms.TextBox();
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanelx.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanelx
            // 
            this.tableLayoutPanelx.ColumnCount = 2;
            this.tableLayoutPanelx.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanelx.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67F));
            this.tableLayoutPanelx.Controls.Add(this.okButton, 1, 1);
            this.tableLayoutPanelx.Controls.Add(this.tbLoadedMods, 1, 0);
            this.tableLayoutPanelx.Controls.Add(this.logoPictureBox, 0, 0);
            this.tableLayoutPanelx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelx.Location = new System.Drawing.Point(9, 9);
            this.tableLayoutPanelx.Name = "tableLayoutPanelx";
            this.tableLayoutPanelx.RowCount = 2;
            this.tableLayoutPanelx.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelx.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelx.Size = new System.Drawing.Size(417, 265);
            this.tableLayoutPanelx.TabIndex = 0;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.okButton.Location = new System.Drawing.Point(339, 239);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 24;
            this.okButton.Text = "&OK";
            // 
            // tbLoadedMods
            // 
            this.tbLoadedMods.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLoadedMods.Location = new System.Drawing.Point(143, 3);
            this.tbLoadedMods.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.tbLoadedMods.Multiline = true;
            this.tbLoadedMods.Name = "tbLoadedMods";
            this.tbLoadedMods.ReadOnly = true;
            this.tbLoadedMods.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbLoadedMods.Size = new System.Drawing.Size(271, 229);
            this.tbLoadedMods.TabIndex = 23;
            this.tbLoadedMods.TabStop = false;
            this.tbLoadedMods.Text = "LoadedMods";
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logoPictureBox.Image = global::NTAF.UniverseBuilder.WinGui.Properties.Resources.Galaxy_AboutScreen;
            this.logoPictureBox.Location = new System.Drawing.Point(3, 3);
            this.logoPictureBox.Name = "logoPictureBox";
            this.tableLayoutPanelx.SetRowSpan(this.logoPictureBox, 2);
            this.logoPictureBox.Size = new System.Drawing.Size(131, 259);
            this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoPictureBox.TabIndex = 12;
            this.logoPictureBox.TabStop = false;
            // 
            // LoadedModuleBox
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 283);
            this.Controls.Add(this.tableLayoutPanelx);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoadedModuleBox";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Loaded Modules...";
            this.tableLayoutPanelx.ResumeLayout(false);
            this.tableLayoutPanelx.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelx;
        private System.Windows.Forms.TextBox tbLoadedMods;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.PictureBox logoPictureBox;
    }
}
