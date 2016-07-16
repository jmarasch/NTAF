namespace NTAF.UniverseBuilder.WinGui.MessageBoxes {
    partial class ProgressBox {
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.LastAction = new System.Windows.Forms.Label();
            this.current = new System.Windows.Forms.Label();
            this.PercentCompleete = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(12, 12);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(412, 23);
            this.progressBar1.TabIndex = 0;
            // 
            // LastAction
            // 
            this.LastAction.AutoSize = true;
            this.LastAction.Location = new System.Drawing.Point(9, 61);
            this.LastAction.Name = "LastAction";
            this.LastAction.Size = new System.Drawing.Size(35, 13);
            this.LastAction.TabIndex = 1;
            this.LastAction.Text = "label1";
            // 
            // current
            // 
            this.current.AutoSize = true;
            this.current.Location = new System.Drawing.Point(12, 38);
            this.current.Name = "current";
            this.current.Size = new System.Drawing.Size(35, 13);
            this.current.TabIndex = 2;
            this.current.Text = "label1";
            // 
            // PercentCompleete
            // 
            this.PercentCompleete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PercentCompleete.AutoSize = true;
            this.PercentCompleete.Location = new System.Drawing.Point(389, 38);
            this.PercentCompleete.Name = "PercentCompleete";
            this.PercentCompleete.Size = new System.Drawing.Size(35, 13);
            this.PercentCompleete.TabIndex = 3;
            this.PercentCompleete.Text = "label1";
            // 
            // ProgressBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 113);
            this.ControlBox = false;
            this.Controls.Add(this.PercentCompleete);
            this.Controls.Add(this.current);
            this.Controls.Add(this.LastAction);
            this.Controls.Add(this.progressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MinimumSize = new System.Drawing.Size(330, 120);
            this.Name = "ProgressBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SomethingsHapening...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label LastAction;
        private System.Windows.Forms.Label current;
        private System.Windows.Forms.Label PercentCompleete;
    }
}