namespace NTAF.ObjectClasses {
    partial class PermEditor {
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
            this.label_Name = new System.Windows.Forms.Label();
            this.Label_ID = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_ForGroup = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rcFun = new System.Windows.Forms.CheckBox();
            this.rcAquatic = new System.Windows.Forms.CheckBox();
            this.rcDemonic = new System.Windows.Forms.CheckBox();
            this.rcUndead = new System.Windows.Forms.CheckBox();
            this.rcAngelic = new System.Windows.Forms.CheckBox();
            this.rcMutant = new System.Windows.Forms.CheckBox();
            this.rcGenicB = new System.Windows.Forms.CheckBox();
            this.rcHuman = new System.Windows.Forms.CheckBox();
            this.rcGenicA = new System.Windows.Forms.CheckBox();
            this.rcAll = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_PermType = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_Cancel
            // 
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Location = new System.Drawing.Point( 473, 165 );
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size( 102, 23 );
            this.button_Cancel.TabIndex = 9;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler( this.button_Cancel_Click );
            // 
            // button_Edit
            // 
            this.button_Edit.Location = new System.Drawing.Point( 257, 166 );
            this.button_Edit.Name = "button_Edit";
            this.button_Edit.Size = new System.Drawing.Size( 102, 23 );
            this.button_Edit.TabIndex = 7;
            this.button_Edit.Text = "Edit";
            this.button_Edit.UseVisualStyleBackColor = true;
            this.button_Edit.Visible = false;
            this.button_Edit.Click += new System.EventHandler( this.button_Edit_Click );
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point( 365, 165 );
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size( 102, 23 );
            this.button_Save.TabIndex = 8;
            this.button_Save.Text = "Finished";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler( this.button_Save_Click );
            // 
            // FIELD_Name
            // 
            this.FIELD_Name.Location = new System.Drawing.Point( 15, 25 );
            this.FIELD_Name.Name = "FIELD_Name";
            this.FIELD_Name.Size = new System.Drawing.Size( 355, 20 );
            this.FIELD_Name.TabIndex = 1;
            this.FIELD_Name.Leave += new System.EventHandler( this.Leave_field );
            this.FIELD_Name.Enter += new System.EventHandler( this.Enter_field );
            // 
            // label_Name
            // 
            this.label_Name.AutoSize = true;
            this.label_Name.Location = new System.Drawing.Point( 12, 9 );
            this.label_Name.Name = "label_Name";
            this.label_Name.Size = new System.Drawing.Size( 38, 13 );
            this.label_Name.TabIndex = 0;
            this.label_Name.Text = "Name:";
            // 
            // Label_ID
            // 
            this.Label_ID.AutoSize = true;
            this.Label_ID.Location = new System.Drawing.Point( 12, 9 );
            this.Label_ID.Name = "Label_ID";
            this.Label_ID.Size = new System.Drawing.Size( 21, 13 );
            this.Label_ID.TabIndex = 35;
            this.Label_ID.Text = "ID:";
            this.Label_ID.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point( 12, 60 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size( 95, 13 );
            this.label1.TabIndex = 2;
            this.label1.Text = "For Specific Race:";
            // 
            // comboBox_ForGroup
            // 
            this.comboBox_ForGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_ForGroup.FormattingEnabled = true;
            this.comboBox_ForGroup.Location = new System.Drawing.Point( 15, 76 );
            this.comboBox_ForGroup.Name = "comboBox_ForGroup";
            this.comboBox_ForGroup.Size = new System.Drawing.Size( 355, 21 );
            this.comboBox_ForGroup.TabIndex = 3;
            this.comboBox_ForGroup.Leave += new System.EventHandler( this.Leave_field );
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add( this.rcFun );
            this.groupBox1.Controls.Add( this.rcAquatic );
            this.groupBox1.Controls.Add( this.rcDemonic );
            this.groupBox1.Controls.Add( this.rcUndead );
            this.groupBox1.Controls.Add( this.rcAngelic );
            this.groupBox1.Controls.Add( this.rcMutant );
            this.groupBox1.Controls.Add( this.rcGenicB );
            this.groupBox1.Controls.Add( this.rcHuman );
            this.groupBox1.Controls.Add( this.rcGenicA );
            this.groupBox1.Controls.Add( this.rcAll );
            this.groupBox1.Location = new System.Drawing.Point( 387, 12 );
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size( 188, 142 );
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "For General Species";
            // 
            // rcFun
            // 
            this.rcFun.AutoSize = true;
            this.rcFun.Location = new System.Drawing.Point( 81, 111 );
            this.rcFun.Name = "rcFun";
            this.rcFun.Size = new System.Drawing.Size( 44, 17 );
            this.rcFun.TabIndex = 9;
            this.rcFun.Text = "Fun";
            this.rcFun.UseVisualStyleBackColor = true;
            this.rcFun.CheckedChanged += new System.EventHandler( this.rcAll_CheckedChanged );
            // 
            // rcAquatic
            // 
            this.rcAquatic.AutoSize = true;
            this.rcAquatic.Location = new System.Drawing.Point( 6, 111 );
            this.rcAquatic.Name = "rcAquatic";
            this.rcAquatic.Size = new System.Drawing.Size( 62, 17 );
            this.rcAquatic.TabIndex = 4;
            this.rcAquatic.Text = "Aquatic";
            this.rcAquatic.UseVisualStyleBackColor = true;
            this.rcAquatic.CheckedChanged += new System.EventHandler( this.rcAll_CheckedChanged );
            // 
            // rcDemonic
            // 
            this.rcDemonic.AutoSize = true;
            this.rcDemonic.Location = new System.Drawing.Point( 81, 88 );
            this.rcDemonic.Name = "rcDemonic";
            this.rcDemonic.Size = new System.Drawing.Size( 68, 17 );
            this.rcDemonic.TabIndex = 8;
            this.rcDemonic.Text = "Demonic";
            this.rcDemonic.UseVisualStyleBackColor = true;
            this.rcDemonic.CheckedChanged += new System.EventHandler( this.rcAll_CheckedChanged );
            // 
            // rcUndead
            // 
            this.rcUndead.AutoSize = true;
            this.rcUndead.Location = new System.Drawing.Point( 6, 88 );
            this.rcUndead.Name = "rcUndead";
            this.rcUndead.Size = new System.Drawing.Size( 64, 17 );
            this.rcUndead.TabIndex = 3;
            this.rcUndead.Text = "Undead";
            this.rcUndead.UseVisualStyleBackColor = true;
            this.rcUndead.CheckedChanged += new System.EventHandler( this.rcAll_CheckedChanged );
            // 
            // rcAngelic
            // 
            this.rcAngelic.AutoSize = true;
            this.rcAngelic.Location = new System.Drawing.Point( 81, 65 );
            this.rcAngelic.Name = "rcAngelic";
            this.rcAngelic.Size = new System.Drawing.Size( 61, 17 );
            this.rcAngelic.TabIndex = 7;
            this.rcAngelic.Text = "Angelic";
            this.rcAngelic.UseVisualStyleBackColor = true;
            this.rcAngelic.CheckedChanged += new System.EventHandler( this.rcAll_CheckedChanged );
            // 
            // rcMutant
            // 
            this.rcMutant.AutoSize = true;
            this.rcMutant.Location = new System.Drawing.Point( 6, 65 );
            this.rcMutant.Name = "rcMutant";
            this.rcMutant.Size = new System.Drawing.Size( 59, 17 );
            this.rcMutant.TabIndex = 2;
            this.rcMutant.Text = "Mutant";
            this.rcMutant.UseVisualStyleBackColor = true;
            this.rcMutant.CheckedChanged += new System.EventHandler( this.rcAll_CheckedChanged );
            // 
            // rcGenicB
            // 
            this.rcGenicB.AutoSize = true;
            this.rcGenicB.Location = new System.Drawing.Point( 81, 42 );
            this.rcGenicB.Name = "rcGenicB";
            this.rcGenicB.Size = new System.Drawing.Size( 91, 17 );
            this.rcGenicB.TabIndex = 6;
            this.rcGenicB.Text = "Genic Type B";
            this.rcGenicB.UseVisualStyleBackColor = true;
            this.rcGenicB.CheckedChanged += new System.EventHandler( this.rcAll_CheckedChanged );
            // 
            // rcHuman
            // 
            this.rcHuman.AutoSize = true;
            this.rcHuman.Location = new System.Drawing.Point( 6, 42 );
            this.rcHuman.Name = "rcHuman";
            this.rcHuman.Size = new System.Drawing.Size( 60, 17 );
            this.rcHuman.TabIndex = 1;
            this.rcHuman.Text = "Human";
            this.rcHuman.UseVisualStyleBackColor = true;
            this.rcHuman.CheckedChanged += new System.EventHandler( this.rcAll_CheckedChanged );
            // 
            // rcGenicA
            // 
            this.rcGenicA.AutoSize = true;
            this.rcGenicA.Location = new System.Drawing.Point( 81, 19 );
            this.rcGenicA.Name = "rcGenicA";
            this.rcGenicA.Size = new System.Drawing.Size( 91, 17 );
            this.rcGenicA.TabIndex = 5;
            this.rcGenicA.Text = "Genic Type A";
            this.rcGenicA.UseVisualStyleBackColor = true;
            this.rcGenicA.CheckedChanged += new System.EventHandler( this.rcAll_CheckedChanged );
            // 
            // rcAll
            // 
            this.rcAll.AutoSize = true;
            this.rcAll.Location = new System.Drawing.Point( 6, 19 );
            this.rcAll.Name = "rcAll";
            this.rcAll.Size = new System.Drawing.Size( 37, 17 );
            this.rcAll.TabIndex = 0;
            this.rcAll.Text = "All";
            this.rcAll.UseVisualStyleBackColor = true;
            this.rcAll.CheckedChanged += new System.EventHandler( this.rcAll_CheckedChanged );
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point( 12, 104 );
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size( 87, 13 );
            this.label2.TabIndex = 4;
            this.label2.Text = "Permission Type:";
            // 
            // comboBox_PermType
            // 
            this.comboBox_PermType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_PermType.FormattingEnabled = true;
            this.comboBox_PermType.Location = new System.Drawing.Point( 15, 120 );
            this.comboBox_PermType.Name = "comboBox_PermType";
            this.comboBox_PermType.Size = new System.Drawing.Size( 355, 21 );
            this.comboBox_PermType.TabIndex = 5;
            this.comboBox_PermType.Leave += new System.EventHandler( this.Leave_field );
            // 
            // PermEditor
            // 
            this.AcceptButton = this.button_Save;
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancel;
            this.ClientSize = new System.Drawing.Size( 588, 198 );
            this.Collectors = new NTAF.PlugInFramework.OCCBase[0];
            this.ControlBox = false;
            this.Controls.Add( this.groupBox1 );
            this.Controls.Add( this.button_Cancel );
            this.Controls.Add( this.button_Edit );
            this.Controls.Add( this.button_Save );
            this.Controls.Add( this.FIELD_Name );
            this.Controls.Add( this.comboBox_PermType );
            this.Controls.Add( this.comboBox_ForGroup );
            this.Controls.Add( this.label2 );
            this.Controls.Add( this.label1 );
            this.Controls.Add( this.label_Name );
            this.Controls.Add( this.Label_ID );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "PermEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PsyPerm";
            this.groupBox1.ResumeLayout( false );
            this.groupBox1.PerformLayout();
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_Edit;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.TextBox FIELD_Name;
        private System.Windows.Forms.Label label_Name;
        private System.Windows.Forms.Label Label_ID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_ForGroup;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox rcFun;
        private System.Windows.Forms.CheckBox rcAquatic;
        private System.Windows.Forms.CheckBox rcDemonic;
        private System.Windows.Forms.CheckBox rcUndead;
        private System.Windows.Forms.CheckBox rcAngelic;
        private System.Windows.Forms.CheckBox rcMutant;
        private System.Windows.Forms.CheckBox rcGenicB;
        private System.Windows.Forms.CheckBox rcHuman;
        private System.Windows.Forms.CheckBox rcGenicA;
        private System.Windows.Forms.CheckBox rcAll;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox comboBox_PermType;
    }
}