namespace NTAF.ObjectClasses {
    partial class ItemEditor {
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
            this.comboBox_StatToMod = new System.Windows.Forms.ComboBox();
            this.button_RemoveMod = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_Save = new System.Windows.Forms.Button();
            this.button_AddMod = new System.Windows.Forms.Button();
            this.listBox_StatMods = new System.Windows.Forms.ListBox();
            this.textBox_Description = new System.Windows.Forms.TextBox();
            this.textBox_Cost = new System.Windows.Forms.TextBox();
            this.textBox_StatModAmount = new System.Windows.Forms.TextBox();
            this.FIELD_Name = new System.Windows.Forms.TextBox();
            this.label_Description = new System.Windows.Forms.Label();
            this.label_StatMods = new System.Windows.Forms.Label();
            this.label_Name = new System.Windows.Forms.Label();
            this.label_Cost = new System.Windows.Forms.Label();
            this.Label_ID = new System.Windows.Forms.Label();
            this.button_Edit = new System.Windows.Forms.Button();
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
            this.comboBox_ForGroup = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_StatToMod
            // 
            this.comboBox_StatToMod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_StatToMod.FormattingEnabled = true;
            this.comboBox_StatToMod.Location = new System.Drawing.Point( 15, 174 );
            this.comboBox_StatToMod.Name = "comboBox_StatToMod";
            this.comboBox_StatToMod.Size = new System.Drawing.Size( 129, 21 );
            this.comboBox_StatToMod.Sorted = true;
            this.comboBox_StatToMod.TabIndex = 3;
            this.comboBox_StatToMod.Enter += new System.EventHandler( this.Enter_field );
            // 
            // button_RemoveMod
            // 
            this.button_RemoveMod.Location = new System.Drawing.Point( 150, 201 );
            this.button_RemoveMod.Name = "button_RemoveMod";
            this.button_RemoveMod.Size = new System.Drawing.Size( 129, 23 );
            this.button_RemoveMod.TabIndex = 6;
            this.button_RemoveMod.Text = "&Remove Selected Mod";
            this.button_RemoveMod.UseVisualStyleBackColor = true;
            this.button_RemoveMod.Click += new System.EventHandler( this.button_RemoveMod_Click );
            // 
            // button_Cancel
            // 
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Location = new System.Drawing.Point( 385, 386 );
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size( 102, 23 );
            this.button_Cancel.TabIndex = 15;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler( this.button_Cancel_Click );
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point( 277, 386 );
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size( 102, 23 );
            this.button_Save.TabIndex = 14;
            this.button_Save.Text = "&Finished";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler( this.button_Save_Click );
            // 
            // button_AddMod
            // 
            this.button_AddMod.Location = new System.Drawing.Point( 15, 201 );
            this.button_AddMod.Name = "button_AddMod";
            this.button_AddMod.Size = new System.Drawing.Size( 129, 23 );
            this.button_AddMod.TabIndex = 5;
            this.button_AddMod.Text = "Add &Mod";
            this.button_AddMod.UseVisualStyleBackColor = true;
            this.button_AddMod.Click += new System.EventHandler( this.button_AddMod_Click );
            // 
            // listBox_StatMods
            // 
            this.listBox_StatMods.FormattingEnabled = true;
            this.listBox_StatMods.Location = new System.Drawing.Point( 15, 73 );
            this.listBox_StatMods.Name = "listBox_StatMods";
            this.listBox_StatMods.Size = new System.Drawing.Size( 264, 95 );
            this.listBox_StatMods.TabIndex = 29;
            // 
            // textBox_Description
            // 
            this.textBox_Description.Location = new System.Drawing.Point( 14, 243 );
            this.textBox_Description.Multiline = true;
            this.textBox_Description.Name = "textBox_Description";
            this.textBox_Description.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_Description.Size = new System.Drawing.Size( 473, 134 );
            this.textBox_Description.TabIndex = 10;
            this.textBox_Description.Leave += new System.EventHandler( this.Leave_field );
            this.textBox_Description.Enter += new System.EventHandler( this.Enter_field );
            // 
            // textBox_Cost
            // 
            this.textBox_Cost.Location = new System.Drawing.Point( 49, 388 );
            this.textBox_Cost.Name = "textBox_Cost";
            this.textBox_Cost.Size = new System.Drawing.Size( 68, 20 );
            this.textBox_Cost.TabIndex = 12;
            this.textBox_Cost.Leave += new System.EventHandler( this.Leave_field );
            this.textBox_Cost.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.NumOnly_KeyPress );
            this.textBox_Cost.Enter += new System.EventHandler( this.Enter_field );
            // 
            // textBox_StatModAmount
            // 
            this.textBox_StatModAmount.Location = new System.Drawing.Point( 150, 175 );
            this.textBox_StatModAmount.Name = "textBox_StatModAmount";
            this.textBox_StatModAmount.Size = new System.Drawing.Size( 129, 20 );
            this.textBox_StatModAmount.TabIndex = 4;
            this.textBox_StatModAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.NumOnly_KeyPress );
            this.textBox_StatModAmount.Enter += new System.EventHandler( this.Enter_field );
            // 
            // FIELD_Name
            // 
            this.FIELD_Name.Location = new System.Drawing.Point( 15, 25 );
            this.FIELD_Name.Name = "FIELD_Name";
            this.FIELD_Name.Size = new System.Drawing.Size( 264, 20 );
            this.FIELD_Name.TabIndex = 1;
            this.FIELD_Name.Leave += new System.EventHandler( this.Leave_field );
            this.FIELD_Name.Enter += new System.EventHandler( this.Enter_field );
            // 
            // label_Description
            // 
            this.label_Description.AutoSize = true;
            this.label_Description.Location = new System.Drawing.Point( 12, 227 );
            this.label_Description.Name = "label_Description";
            this.label_Description.Size = new System.Drawing.Size( 63, 13 );
            this.label_Description.TabIndex = 9;
            this.label_Description.Text = "&Description:";
            // 
            // label_StatMods
            // 
            this.label_StatMods.AutoSize = true;
            this.label_StatMods.Location = new System.Drawing.Point( 12, 54 );
            this.label_StatMods.Name = "label_StatMods";
            this.label_StatMods.Size = new System.Drawing.Size( 58, 13 );
            this.label_StatMods.TabIndex = 23;
            this.label_StatMods.Text = "Stat Mods:";
            // 
            // label_Name
            // 
            this.label_Name.AutoSize = true;
            this.label_Name.Location = new System.Drawing.Point( 12, 9 );
            this.label_Name.Name = "label_Name";
            this.label_Name.Size = new System.Drawing.Size( 38, 13 );
            this.label_Name.TabIndex = 0;
            this.label_Name.Text = "&Name:";
            // 
            // label_Cost
            // 
            this.label_Cost.AutoSize = true;
            this.label_Cost.Location = new System.Drawing.Point( 12, 391 );
            this.label_Cost.Name = "label_Cost";
            this.label_Cost.Size = new System.Drawing.Size( 31, 13 );
            this.label_Cost.TabIndex = 11;
            this.label_Cost.Text = "&Cost:";
            // 
            // Label_ID
            // 
            this.Label_ID.AutoSize = true;
            this.Label_ID.Location = new System.Drawing.Point( 12, 9 );
            this.Label_ID.Name = "Label_ID";
            this.Label_ID.Size = new System.Drawing.Size( 21, 13 );
            this.Label_ID.TabIndex = 27;
            this.Label_ID.Text = "ID:";
            this.Label_ID.Visible = false;
            // 
            // button_Edit
            // 
            this.button_Edit.Location = new System.Drawing.Point( 169, 386 );
            this.button_Edit.Name = "button_Edit";
            this.button_Edit.Size = new System.Drawing.Size( 102, 23 );
            this.button_Edit.TabIndex = 13;
            this.button_Edit.Text = "&Edit";
            this.button_Edit.UseVisualStyleBackColor = true;
            this.button_Edit.Visible = false;
            this.button_Edit.Click += new System.EventHandler( this.button_Edit_Click );
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
            this.groupBox1.Location = new System.Drawing.Point( 290, 25 );
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size( 197, 142 );
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Species that can use this Permission";
            // 
            // rcFun
            // 
            this.rcFun.AutoSize = true;
            this.rcFun.Location = new System.Drawing.Point( 81, 111 );
            this.rcFun.Name = "rcFun";
            this.rcFun.Size = new System.Drawing.Size( 44, 17 );
            this.rcFun.TabIndex = 9;
            this.rcFun.Text = "F&un";
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
            this.rcAquatic.Text = "&Aquatic";
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
            this.rcDemonic.Text = "Dem&onic";
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
            this.rcUndead.Text = "&Undead";
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
            this.rcAngelic.Text = "Angel&ic";
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
            this.rcMutant.Text = "&Mutant";
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
            this.rcGenicB.Text = "Genic &Type B";
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
            this.rcHuman.Text = "&Human";
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
            this.rcGenicA.Text = "&Genic Type A";
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
            this.rcAll.Text = "A&ll";
            this.rcAll.UseVisualStyleBackColor = true;
            this.rcAll.CheckedChanged += new System.EventHandler( this.rcAll_CheckedChanged );
            // 
            // comboBox_ForGroup
            // 
            this.comboBox_ForGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_ForGroup.FormattingEnabled = true;
            this.comboBox_ForGroup.Location = new System.Drawing.Point( 290, 201 );
            this.comboBox_ForGroup.Name = "comboBox_ForGroup";
            this.comboBox_ForGroup.Size = new System.Drawing.Size( 197, 21 );
            this.comboBox_ForGroup.TabIndex = 8;
            this.comboBox_ForGroup.Leave += new System.EventHandler( this.Leave_field );
            this.comboBox_ForGroup.Enter += new System.EventHandler( this.Enter_field );
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point( 287, 185 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size( 95, 13 );
            this.label1.TabIndex = 7;
            this.label1.Text = "For &Specific Race:";
            // 
            // ItemEditor
            // 
            this.AcceptButton = this.button_Save;
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancel;
            this.ClientSize = new System.Drawing.Size( 503, 419 );
            this.Collectors = new NTAF.PlugInFramework.OCCBase[0];
            this.Controls.Add( this.groupBox1 );
            this.Controls.Add( this.comboBox_ForGroup );
            this.Controls.Add( this.label1 );
            this.Controls.Add( this.button_Edit );
            this.Controls.Add( this.comboBox_StatToMod );
            this.Controls.Add( this.button_RemoveMod );
            this.Controls.Add( this.button_Cancel );
            this.Controls.Add( this.button_Save );
            this.Controls.Add( this.button_AddMod );
            this.Controls.Add( this.listBox_StatMods );
            this.Controls.Add( this.textBox_Description );
            this.Controls.Add( this.textBox_Cost );
            this.Controls.Add( this.textBox_StatModAmount );
            this.Controls.Add( this.FIELD_Name );
            this.Controls.Add( this.label_Description );
            this.Controls.Add( this.label_StatMods );
            this.Controls.Add( this.label_Name );
            this.Controls.Add( this.label_Cost );
            this.Controls.Add( this.Label_ID );
            this.Name = "ItemEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ItemForm";
            this.groupBox1.ResumeLayout( false );
            this.groupBox1.PerformLayout();
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_StatToMod;
        private System.Windows.Forms.Button button_RemoveMod;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.Button button_AddMod;
        private System.Windows.Forms.ListBox listBox_StatMods;
        private System.Windows.Forms.TextBox textBox_Description;
        private System.Windows.Forms.TextBox textBox_Cost;
        private System.Windows.Forms.TextBox textBox_StatModAmount;
        private System.Windows.Forms.TextBox FIELD_Name;
        private System.Windows.Forms.Label label_Description;
        private System.Windows.Forms.Label label_StatMods;
        private System.Windows.Forms.Label label_Name;
        private System.Windows.Forms.Label label_Cost;
        private System.Windows.Forms.Label Label_ID;
        private System.Windows.Forms.Button button_Edit;
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
        private System.Windows.Forms.ComboBox comboBox_ForGroup;
        private System.Windows.Forms.Label label1;
    }
}