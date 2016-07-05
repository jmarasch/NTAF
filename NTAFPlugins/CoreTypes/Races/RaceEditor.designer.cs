namespace NTAF.ObjectClasses {
    partial class RaceEditor {
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
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_Edit = new System.Windows.Forms.Button();
            this.button_Save = new System.Windows.Forms.Button();
            this.FIELD_Name = new System.Windows.Forms.TextBox();
            this.comboBox_BaseRace = new System.Windows.Forms.ComboBox();
            this.Label_BaseRace = new System.Windows.Forms.Label();
            this.label_Name = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_Cancel
            // 
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Location = new System.Drawing.Point( 123, 120 );
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size( 102, 23 );
            this.button_Cancel.TabIndex = 6;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler( this.button_Cancel_Click );
            // 
            // button_Edit
            // 
            this.button_Edit.Location = new System.Drawing.Point( 15, 91 );
            this.button_Edit.Name = "button_Edit";
            this.button_Edit.Size = new System.Drawing.Size( 102, 23 );
            this.button_Edit.TabIndex = 4;
            this.button_Edit.Text = "Edit";
            this.button_Edit.UseVisualStyleBackColor = true;
            this.button_Edit.Visible = false;
            this.button_Edit.Click += new System.EventHandler( this.button_Edit_Click );
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point( 15, 120 );
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size( 102, 23 );
            this.button_Save.TabIndex = 5;
            this.button_Save.Text = "Finished";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler( this.button_Save_Click );
            // 
            // FIELD_Name
            // 
            this.FIELD_Name.Location = new System.Drawing.Point( 15, 25 );
            this.FIELD_Name.Name = "FIELD_Name";
            this.FIELD_Name.Size = new System.Drawing.Size( 210, 20 );
            this.FIELD_Name.TabIndex = 1;
            this.FIELD_Name.Leave += new System.EventHandler( this.Leave_field );
            this.FIELD_Name.Enter += new System.EventHandler( this.Enter_field );
            // 
            // comboBox_BaseRace
            // 
            this.comboBox_BaseRace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_BaseRace.FormattingEnabled = true;
            this.comboBox_BaseRace.Location = new System.Drawing.Point( 15, 64 );
            this.comboBox_BaseRace.Name = "comboBox_BaseRace";
            this.comboBox_BaseRace.Size = new System.Drawing.Size( 210, 21 );
            this.comboBox_BaseRace.TabIndex = 3;
            this.comboBox_BaseRace.Leave += new System.EventHandler( this.Leave_field );
            // 
            // Label_BaseRace
            // 
            this.Label_BaseRace.AutoSize = true;
            this.Label_BaseRace.Location = new System.Drawing.Point( 12, 48 );
            this.Label_BaseRace.Name = "Label_BaseRace";
            this.Label_BaseRace.Size = new System.Drawing.Size( 48, 13 );
            this.Label_BaseRace.TabIndex = 2;
            this.Label_BaseRace.Text = "Species:";
            // 
            // label_Name
            // 
            this.label_Name.AutoSize = true;
            this.label_Name.Location = new System.Drawing.Point( 12, 9 );
            this.label_Name.Name = "label_Name";
            this.label_Name.Size = new System.Drawing.Size( 67, 13 );
            this.label_Name.TabIndex = 0;
            this.label_Name.Text = "Race Name:";
            // 
            // RaceEditor
            // 
            this.AcceptButton = this.button_Save;
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancel;
            this.ClientSize = new System.Drawing.Size( 232, 152 );
            this.Collectors = new NTAF.PlugInFramework.OCCBase[0];
            this.ControlBox = false;
            this.Controls.Add( this.button_Cancel );
            this.Controls.Add( this.button_Edit );
            this.Controls.Add( this.button_Save );
            this.Controls.Add( this.FIELD_Name );
            this.Controls.Add( this.comboBox_BaseRace );
            this.Controls.Add( this.Label_BaseRace );
            this.Controls.Add( this.label_Name );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "RaceEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RaceForm";
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_Edit;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.TextBox FIELD_Name;
        private System.Windows.Forms.ComboBox comboBox_BaseRace;
        private System.Windows.Forms.Label Label_BaseRace;
        private System.Windows.Forms.Label label_Name;
    }
}