namespace NTAF.ObjectClasses {
    partial class ArchetypeEditor {
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
            this.comboBox_BaseUnitType = new System.Windows.Forms.ComboBox();
            this.Label_BaseRace = new System.Windows.Forms.Label();
            this.label_Name = new System.Windows.Forms.Label();
            this.Label_ID = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_Cancel
            // 
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Location = new System.Drawing.Point( 119, 120 );
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size( 102, 23 );
            this.button_Cancel.TabIndex = 6;
            this.button_Cancel.Text = "&Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler( this.button_Cancel_Click );
            // 
            // button_Edit
            // 
            this.button_Edit.Location = new System.Drawing.Point( 11, 91 );
            this.button_Edit.Name = "button_Edit";
            this.button_Edit.Size = new System.Drawing.Size( 102, 23 );
            this.button_Edit.TabIndex = 4;
            this.button_Edit.Text = "&Edit";
            this.button_Edit.UseVisualStyleBackColor = true;
            this.button_Edit.Visible = false;
            this.button_Edit.Click += new System.EventHandler( this.button_Edit_Click );
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point( 11, 120 );
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size( 102, 23 );
            this.button_Save.TabIndex = 5;
            this.button_Save.Text = "&Finished";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler( this.button_Save_Click );
            // 
            // FIELD_Name
            // 
            this.FIELD_Name.Location = new System.Drawing.Point( 11, 25 );
            this.FIELD_Name.Name = "FIELD_Name";
            this.FIELD_Name.Size = new System.Drawing.Size( 210, 20 );
            this.FIELD_Name.TabIndex = 1;
            this.FIELD_Name.Leave += new System.EventHandler( this.Leave_field );
            this.FIELD_Name.Enter += new System.EventHandler( this.Enter_field );
            // 
            // comboBox_BaseUnitType
            // 
            this.comboBox_BaseUnitType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_BaseUnitType.FormattingEnabled = true;
            this.comboBox_BaseUnitType.Location = new System.Drawing.Point( 11, 64 );
            this.comboBox_BaseUnitType.Name = "comboBox_BaseUnitType";
            this.comboBox_BaseUnitType.Size = new System.Drawing.Size( 210, 21 );
            this.comboBox_BaseUnitType.TabIndex = 3;
            this.comboBox_BaseUnitType.Leave += new System.EventHandler( this.Leave_field );
            // 
            // Label_BaseRace
            // 
            this.Label_BaseRace.AutoSize = true;
            this.Label_BaseRace.Location = new System.Drawing.Point( 8, 48 );
            this.Label_BaseRace.Name = "Label_BaseRace";
            this.Label_BaseRace.Size = new System.Drawing.Size( 83, 13 );
            this.Label_BaseRace.TabIndex = 2;
            this.Label_BaseRace.Text = "&Base Unit Type:";
            // 
            // label_Name
            // 
            this.label_Name.AutoSize = true;
            this.label_Name.Location = new System.Drawing.Point( 8, 9 );
            this.label_Name.Name = "label_Name";
            this.label_Name.Size = new System.Drawing.Size( 38, 13 );
            this.label_Name.TabIndex = 0;
            this.label_Name.Text = "&Name:";
            // 
            // Label_ID
            // 
            this.Label_ID.AutoSize = true;
            this.Label_ID.Location = new System.Drawing.Point( 8, 7 );
            this.Label_ID.Name = "Label_ID";
            this.Label_ID.Size = new System.Drawing.Size( 21, 13 );
            this.Label_ID.TabIndex = 21;
            this.Label_ID.Text = "ID:";
            this.Label_ID.Visible = false;
            // 
            // ArchetypeEditor
            // 
            this.AcceptButton = this.button_Save;
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancel;
            this.ClientSize = new System.Drawing.Size( 232, 149 );
            this.Collectors = new NTAF.PlugInFramework.OCCBase[0];
            this.ControlBox = false;
            this.Controls.Add( this.button_Cancel );
            this.Controls.Add( this.button_Edit );
            this.Controls.Add( this.button_Save );
            this.Controls.Add( this.FIELD_Name );
            this.Controls.Add( this.comboBox_BaseUnitType );
            this.Controls.Add( this.Label_BaseRace );
            this.Controls.Add( this.label_Name );
            this.Controls.Add( this.Label_ID );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ArchetypeEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UnitTypeForm";
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_Edit;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.TextBox FIELD_Name;
        private System.Windows.Forms.ComboBox comboBox_BaseUnitType;
        private System.Windows.Forms.Label Label_BaseRace;
        private System.Windows.Forms.Label label_Name;
        private System.Windows.Forms.Label Label_ID;
    }
}