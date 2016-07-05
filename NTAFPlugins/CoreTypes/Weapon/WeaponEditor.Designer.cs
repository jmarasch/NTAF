namespace NTAF.ObjectClasses {
    partial class WeaponEditor {
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
            this.comboBox_BaseWeaponType = new System.Windows.Forms.ComboBox();
            this.Label_BaseRace = new System.Windows.Forms.Label();
            this.label_Name = new System.Windows.Forms.Label();
            this.textBox_Description = new System.Windows.Forms.TextBox();
            this.textBox_Cost = new System.Windows.Forms.TextBox();
            this.label_Description = new System.Windows.Forms.Label();
            this.label_Cost = new System.Windows.Forms.Label();
            this.textBox_Range = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_MvsP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_MvsA = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_SIOR = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_Special = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_Shots = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_SaveMod = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_Permission = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // button_Cancel
            // 
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Location = new System.Drawing.Point( 370, 449 );
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size( 102, 23 );
            this.button_Cancel.TabIndex = 26;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler( this.button_Cancel_Click );
            // 
            // button_Edit
            // 
            this.button_Edit.Location = new System.Drawing.Point( 262, 129 );
            this.button_Edit.Name = "button_Edit";
            this.button_Edit.Size = new System.Drawing.Size( 210, 23 );
            this.button_Edit.TabIndex = 18;
            this.button_Edit.Text = "Edit";
            this.button_Edit.UseVisualStyleBackColor = true;
            this.button_Edit.Visible = false;
            this.button_Edit.Click += new System.EventHandler( this.button_Edit_Click );
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point( 262, 449 );
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size( 102, 23 );
            this.button_Save.TabIndex = 25;
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
            // 
            // comboBox_BaseWeaponType
            // 
            this.comboBox_BaseWeaponType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_BaseWeaponType.FormattingEnabled = true;
            this.comboBox_BaseWeaponType.Location = new System.Drawing.Point( 15, 64 );
            this.comboBox_BaseWeaponType.Name = "comboBox_BaseWeaponType";
            this.comboBox_BaseWeaponType.Size = new System.Drawing.Size( 210, 21 );
            this.comboBox_BaseWeaponType.TabIndex = 7;
            this.comboBox_BaseWeaponType.Leave += new System.EventHandler( this.Leave_field );
            // 
            // Label_BaseRace
            // 
            this.Label_BaseRace.AutoSize = true;
            this.Label_BaseRace.Location = new System.Drawing.Point( 12, 48 );
            this.Label_BaseRace.Name = "Label_BaseRace";
            this.Label_BaseRace.Size = new System.Drawing.Size( 105, 13 );
            this.Label_BaseRace.TabIndex = 6;
            this.Label_BaseRace.Text = "Base Weapon Type:";
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
            // textBox_Description
            // 
            this.textBox_Description.Location = new System.Drawing.Point( 15, 310 );
            this.textBox_Description.Multiline = true;
            this.textBox_Description.Name = "textBox_Description";
            this.textBox_Description.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_Description.Size = new System.Drawing.Size( 457, 134 );
            this.textBox_Description.TabIndex = 22;
            this.textBox_Description.Leave += new System.EventHandler( this.Leave_field );
            // 
            // textBox_Cost
            // 
            this.textBox_Cost.Location = new System.Drawing.Point( 50, 452 );
            this.textBox_Cost.Name = "textBox_Cost";
            this.textBox_Cost.Size = new System.Drawing.Size( 68, 20 );
            this.textBox_Cost.TabIndex = 24;
            this.textBox_Cost.Leave += new System.EventHandler( this.Leave_field );
            // 
            // label_Description
            // 
            this.label_Description.AutoSize = true;
            this.label_Description.Location = new System.Drawing.Point( 13, 294 );
            this.label_Description.Name = "label_Description";
            this.label_Description.Size = new System.Drawing.Size( 63, 13 );
            this.label_Description.TabIndex = 21;
            this.label_Description.Text = "Description:";
            // 
            // label_Cost
            // 
            this.label_Cost.AutoSize = true;
            this.label_Cost.Location = new System.Drawing.Point( 13, 455 );
            this.label_Cost.Name = "label_Cost";
            this.label_Cost.Size = new System.Drawing.Size( 31, 13 );
            this.label_Cost.TabIndex = 23;
            this.label_Cost.Text = "Cost:";
            // 
            // textBox_Range
            // 
            this.textBox_Range.Location = new System.Drawing.Point( 231, 25 );
            this.textBox_Range.Name = "textBox_Range";
            this.textBox_Range.Size = new System.Drawing.Size( 117, 20 );
            this.textBox_Range.TabIndex = 3;
            this.textBox_Range.Leave += new System.EventHandler( this.Leave_field );
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point( 228, 9 );
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size( 42, 13 );
            this.label2.TabIndex = 2;
            this.label2.Text = "Range:";
            // 
            // textBox_MvsP
            // 
            this.textBox_MvsP.Location = new System.Drawing.Point( 231, 64 );
            this.textBox_MvsP.Name = "textBox_MvsP";
            this.textBox_MvsP.Size = new System.Drawing.Size( 117, 20 );
            this.textBox_MvsP.TabIndex = 9;
            this.textBox_MvsP.Leave += new System.EventHandler( this.Leave_field );
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point( 228, 48 );
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size( 37, 13 );
            this.label3.TabIndex = 8;
            this.label3.Text = "MvsP:";
            // 
            // textBox_MvsA
            // 
            this.textBox_MvsA.Location = new System.Drawing.Point( 231, 103 );
            this.textBox_MvsA.Name = "textBox_MvsA";
            this.textBox_MvsA.Size = new System.Drawing.Size( 117, 20 );
            this.textBox_MvsA.TabIndex = 15;
            this.textBox_MvsA.Leave += new System.EventHandler( this.Leave_field );
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point( 228, 87 );
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size( 37, 13 );
            this.label4.TabIndex = 14;
            this.label4.Text = "MvsA:";
            // 
            // textBox_SIOR
            // 
            this.textBox_SIOR.Location = new System.Drawing.Point( 354, 25 );
            this.textBox_SIOR.Name = "textBox_SIOR";
            this.textBox_SIOR.Size = new System.Drawing.Size( 117, 20 );
            this.textBox_SIOR.TabIndex = 5;
            this.textBox_SIOR.Leave += new System.EventHandler( this.Leave_field );
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point( 351, 9 );
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size( 36, 13 );
            this.label5.TabIndex = 4;
            this.label5.Text = "SIOR:";
            // 
            // textBox_Special
            // 
            this.textBox_Special.Location = new System.Drawing.Point( 15, 158 );
            this.textBox_Special.Multiline = true;
            this.textBox_Special.Name = "textBox_Special";
            this.textBox_Special.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_Special.Size = new System.Drawing.Size( 457, 134 );
            this.textBox_Special.TabIndex = 20;
            this.textBox_Special.Leave += new System.EventHandler( this.Leave_field );
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point( 13, 142 );
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size( 45, 13 );
            this.label6.TabIndex = 19;
            this.label6.Text = "Special:";
            // 
            // textBox_Shots
            // 
            this.textBox_Shots.Location = new System.Drawing.Point( 354, 64 );
            this.textBox_Shots.Name = "textBox_Shots";
            this.textBox_Shots.Size = new System.Drawing.Size( 117, 20 );
            this.textBox_Shots.TabIndex = 11;
            this.textBox_Shots.Leave += new System.EventHandler( this.Leave_field );
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point( 351, 48 );
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size( 37, 13 );
            this.label7.TabIndex = 10;
            this.label7.Text = "Shots:";
            // 
            // textBox_SaveMod
            // 
            this.textBox_SaveMod.Location = new System.Drawing.Point( 354, 103 );
            this.textBox_SaveMod.Name = "textBox_SaveMod";
            this.textBox_SaveMod.Size = new System.Drawing.Size( 117, 20 );
            this.textBox_SaveMod.TabIndex = 17;
            this.textBox_SaveMod.Leave += new System.EventHandler( this.Leave_field );
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point( 351, 87 );
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size( 75, 13 );
            this.label8.TabIndex = 16;
            this.label8.Text = "Save Modifier:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point( 13, 87 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size( 105, 13 );
            this.label1.TabIndex = 12;
            this.label1.Text = "Requires Permission:";
            // 
            // comboBox_Permission
            // 
            this.comboBox_Permission.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Permission.FormattingEnabled = true;
            this.comboBox_Permission.Location = new System.Drawing.Point( 16, 103 );
            this.comboBox_Permission.Name = "comboBox_Permission";
            this.comboBox_Permission.Size = new System.Drawing.Size( 210, 21 );
            this.comboBox_Permission.TabIndex = 13;
            this.comboBox_Permission.Leave += new System.EventHandler( this.Leave_field );
            // 
            // WeaponEditor
            // 
            this.AcceptButton = this.button_Save;
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancel;
            this.ClientSize = new System.Drawing.Size( 481, 481 );
            this.Collectors = new NTAF.PlugInFramework.OCCBase[0];
            this.ControlBox = false;
            this.Controls.Add( this.textBox_SaveMod );
            this.Controls.Add( this.label8 );
            this.Controls.Add( this.textBox_Shots );
            this.Controls.Add( this.label7 );
            this.Controls.Add( this.textBox_Special );
            this.Controls.Add( this.label6 );
            this.Controls.Add( this.textBox_SIOR );
            this.Controls.Add( this.label5 );
            this.Controls.Add( this.textBox_MvsA );
            this.Controls.Add( this.label4 );
            this.Controls.Add( this.textBox_MvsP );
            this.Controls.Add( this.label3 );
            this.Controls.Add( this.textBox_Range );
            this.Controls.Add( this.label2 );
            this.Controls.Add( this.textBox_Description );
            this.Controls.Add( this.textBox_Cost );
            this.Controls.Add( this.label_Description );
            this.Controls.Add( this.label_Cost );
            this.Controls.Add( this.button_Cancel );
            this.Controls.Add( this.button_Edit );
            this.Controls.Add( this.button_Save );
            this.Controls.Add( this.FIELD_Name );
            this.Controls.Add( this.comboBox_Permission );
            this.Controls.Add( this.label1 );
            this.Controls.Add( this.comboBox_BaseWeaponType );
            this.Controls.Add( this.Label_BaseRace );
            this.Controls.Add( this.label_Name );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "WeaponEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WeaponForm";
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_Edit;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.TextBox FIELD_Name;
        private System.Windows.Forms.ComboBox comboBox_BaseWeaponType;
        private System.Windows.Forms.Label Label_BaseRace;
        private System.Windows.Forms.Label label_Name;
        private System.Windows.Forms.TextBox textBox_Description;
        private System.Windows.Forms.TextBox textBox_Cost;
        private System.Windows.Forms.Label label_Description;
        private System.Windows.Forms.Label label_Cost;
        private System.Windows.Forms.TextBox textBox_Range;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_MvsP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_MvsA;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_SIOR;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_Special;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_Shots;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_SaveMod;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_Permission;
    }
}