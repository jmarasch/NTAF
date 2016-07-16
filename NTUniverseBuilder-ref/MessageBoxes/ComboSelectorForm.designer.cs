namespace NTAF.UniverseBuilder.WinGui.MessageBoxes {
    partial class ComboSelectorForm {
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancle = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.cbxSelected = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point( 318, 59 );
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size( 75, 23 );
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "button1";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancle
            // 
            this.btnCancle.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.btnCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancle.Location = new System.Drawing.Point( 399, 59 );
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size( 75, 23 );
            this.btnCancle.TabIndex = 2;
            this.btnCancle.Text = "button2";
            this.btnCancle.UseVisualStyleBackColor = true;
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point( 12, 9 );
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size( 35, 13 );
            this.lblMessage.TabIndex = 2;
            this.lblMessage.Text = "label1";
            // 
            // cbxSelected
            // 
            this.cbxSelected.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.cbxSelected.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSelected.FormattingEnabled = true;
            this.cbxSelected.Location = new System.Drawing.Point( 12, 32 );
            this.cbxSelected.Name = "cbxSelected";
            this.cbxSelected.Size = new System.Drawing.Size( 465, 21 );
            this.cbxSelected.TabIndex = 0;
            this.cbxSelected.SelectedIndexChanged += new System.EventHandler( this.cbxSelected_SelectedIndexChanged );
            // 
            // ComboSelectorForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancle;
            this.ClientSize = new System.Drawing.Size( 486, 94 );
            this.ControlBox = false;
            this.Controls.Add( this.cbxSelected );
            this.Controls.Add( this.lblMessage );
            this.Controls.Add( this.btnCancle );
            this.Controls.Add( this.btnOK );
            this.Name = "ComboSelectorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ComboSelectorForm";
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.ComboBox cbxSelected;
    }
}