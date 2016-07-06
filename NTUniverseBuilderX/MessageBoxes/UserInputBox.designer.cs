namespace NTAF.UniverseBuilder.WinGui.MessageBoxes {
    partial class UserInputBox {
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
            this.lblMessage = new System.Windows.Forms.Label( );
            this.btnCancle = new System.Windows.Forms.Button( );
            this.btnOK = new System.Windows.Forms.Button( );
            this.txtUserInput = new System.Windows.Forms.TextBox( );
            this.SuspendLayout( );
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
            this.lblMessage.TabIndex = 6;
            this.lblMessage.Text = "label1";
            // 
            // btnCancle
            // 
            this.btnCancle.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.btnCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancle.Location = new System.Drawing.Point( 391, 59 );
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size( 75, 23 );
            this.btnCancle.TabIndex = 2;
            this.btnCancle.Text = "button2";
            this.btnCancle.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point( 310, 59 );
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size( 75, 23 );
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "button1";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // txtUserInput
            // 
            this.txtUserInput.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.txtUserInput.Location = new System.Drawing.Point( 15, 32 );
            this.txtUserInput.Name = "txtUserInput";
            this.txtUserInput.Size = new System.Drawing.Size( 451, 20 );
            this.txtUserInput.TabIndex = 0;
            this.txtUserInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.txtUserInput_KeyPress );
            // 
            // UserInputBox
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancle;
            this.ClientSize = new System.Drawing.Size( 478, 94 );
            this.ControlBox = false;
            this.Controls.Add( this.txtUserInput );
            this.Controls.Add( this.lblMessage );
            this.Controls.Add( this.btnCancle );
            this.Controls.Add( this.btnOK );
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserInputBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UserInputBox";
            this.ResumeLayout( false );
            this.PerformLayout( );

        }

        #endregion

        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtUserInput;
    }
}