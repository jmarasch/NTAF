namespace NTAF.ObjectClasses {
    partial class PsyEditor {
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
            this.textBox_Description = new System.Windows.Forms.TextBox();
            this.FIELD_Name = new System.Windows.Forms.TextBox();
            this.comboBox_TemplateType = new System.Windows.Forms.ComboBox();
            this.Label_SkillGroup = new System.Windows.Forms.Label();
            this.label_Description = new System.Windows.Forms.Label();
            this.label_Name = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_Effect = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_PPCost = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_TemplateSize = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_Range = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox_PsyType = new System.Windows.Forms.ComboBox();
            this.comboBox_Permission = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_Cancel
            // 
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Location = new System.Drawing.Point( 270, 519 );
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size( 102, 23 );
            this.button_Cancel.TabIndex = 20;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler( this.button_Cancel_Click );
            // 
            // button_Edit
            // 
            this.button_Edit.Location = new System.Drawing.Point( 54, 520 );
            this.button_Edit.Name = "button_Edit";
            this.button_Edit.Size = new System.Drawing.Size( 102, 23 );
            this.button_Edit.TabIndex = 18;
            this.button_Edit.Text = "Edit";
            this.button_Edit.UseVisualStyleBackColor = true;
            this.button_Edit.Visible = false;
            this.button_Edit.Click += new System.EventHandler( this.button_Edit_Click );
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point( 162, 519 );
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size( 102, 23 );
            this.button_Save.TabIndex = 19;
            this.button_Save.Text = "Finished";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler( this.button_Save_Click );
            // 
            // textBox_Description
            // 
            this.textBox_Description.Location = new System.Drawing.Point( 15, 376 );
            this.textBox_Description.Multiline = true;
            this.textBox_Description.Name = "textBox_Description";
            this.textBox_Description.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_Description.Size = new System.Drawing.Size( 355, 134 );
            this.textBox_Description.TabIndex = 17;
            this.textBox_Description.Leave += new System.EventHandler( this.Leave_field );
            this.textBox_Description.Enter += new System.EventHandler( this.Enter_field );
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
            // comboBox_TemplateType
            // 
            this.comboBox_TemplateType.FormattingEnabled = true;
            this.comboBox_TemplateType.Location = new System.Drawing.Point( 15, 143 );
            this.comboBox_TemplateType.Name = "comboBox_TemplateType";
            this.comboBox_TemplateType.Size = new System.Drawing.Size( 171, 21 );
            this.comboBox_TemplateType.TabIndex = 9;
            this.comboBox_TemplateType.Leave += new System.EventHandler( this.Leave_field );
            this.comboBox_TemplateType.Enter += new System.EventHandler( this.Enter_field );
            // 
            // Label_SkillGroup
            // 
            this.Label_SkillGroup.AutoSize = true;
            this.Label_SkillGroup.Location = new System.Drawing.Point( 12, 127 );
            this.Label_SkillGroup.Name = "Label_SkillGroup";
            this.Label_SkillGroup.Size = new System.Drawing.Size( 54, 13 );
            this.Label_SkillGroup.TabIndex = 8;
            this.Label_SkillGroup.Text = "Template:";
            // 
            // label_Description
            // 
            this.label_Description.AutoSize = true;
            this.label_Description.Location = new System.Drawing.Point( 13, 360 );
            this.label_Description.Name = "label_Description";
            this.label_Description.Size = new System.Drawing.Size( 63, 13 );
            this.label_Description.TabIndex = 16;
            this.label_Description.Text = "Description:";
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point( 12, 207 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size( 38, 13 );
            this.label1.TabIndex = 14;
            this.label1.Text = "Effect:";
            // 
            // textBox_Effect
            // 
            this.textBox_Effect.Location = new System.Drawing.Point( 14, 223 );
            this.textBox_Effect.Multiline = true;
            this.textBox_Effect.Name = "textBox_Effect";
            this.textBox_Effect.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_Effect.Size = new System.Drawing.Size( 356, 134 );
            this.textBox_Effect.TabIndex = 15;
            this.textBox_Effect.Leave += new System.EventHandler( this.Leave_field );
            this.textBox_Effect.Enter += new System.EventHandler( this.Enter_field );
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point( 12, 88 );
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size( 48, 13 );
            this.label2.TabIndex = 4;
            this.label2.Text = "PP Cost:";
            // 
            // textBox_PPCost
            // 
            this.textBox_PPCost.Location = new System.Drawing.Point( 15, 104 );
            this.textBox_PPCost.Name = "textBox_PPCost";
            this.textBox_PPCost.Size = new System.Drawing.Size( 171, 20 );
            this.textBox_PPCost.TabIndex = 5;
            this.textBox_PPCost.Leave += new System.EventHandler( this.Leave_field );
            this.textBox_PPCost.Enter += new System.EventHandler( this.Enter_field );
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point( 196, 128 );
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size( 77, 13 );
            this.label3.TabIndex = 10;
            this.label3.Text = "Template Size:";
            // 
            // textBox_TemplateSize
            // 
            this.textBox_TemplateSize.Location = new System.Drawing.Point( 199, 144 );
            this.textBox_TemplateSize.Name = "textBox_TemplateSize";
            this.textBox_TemplateSize.Size = new System.Drawing.Size( 171, 20 );
            this.textBox_TemplateSize.TabIndex = 11;
            this.textBox_TemplateSize.Leave += new System.EventHandler( this.Leave_field );
            this.textBox_TemplateSize.Enter += new System.EventHandler( this.Enter_field );
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point( 196, 88 );
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size( 42, 13 );
            this.label4.TabIndex = 6;
            this.label4.Text = "Range:";
            // 
            // textBox_Range
            // 
            this.textBox_Range.Location = new System.Drawing.Point( 199, 104 );
            this.textBox_Range.Name = "textBox_Range";
            this.textBox_Range.Size = new System.Drawing.Size( 171, 20 );
            this.textBox_Range.TabIndex = 7;
            this.textBox_Range.Leave += new System.EventHandler( this.Leave_field );
            this.textBox_Range.Enter += new System.EventHandler( this.Enter_field );
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point( 12, 48 );
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size( 54, 13 );
            this.label5.TabIndex = 2;
            this.label5.Text = "Psy Type:";
            // 
            // comboBox_PsyType
            // 
            this.comboBox_PsyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_PsyType.FormattingEnabled = true;
            this.comboBox_PsyType.Location = new System.Drawing.Point( 15, 64 );
            this.comboBox_PsyType.Name = "comboBox_PsyType";
            this.comboBox_PsyType.Size = new System.Drawing.Size( 355, 21 );
            this.comboBox_PsyType.TabIndex = 3;
            this.comboBox_PsyType.Leave += new System.EventHandler( this.Leave_field );
            this.comboBox_PsyType.Enter += new System.EventHandler( this.Enter_field );
            // 
            // comboBox_Permission
            // 
            this.comboBox_Permission.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Permission.FormattingEnabled = true;
            this.comboBox_Permission.Location = new System.Drawing.Point( 15, 183 );
            this.comboBox_Permission.Name = "comboBox_Permission";
            this.comboBox_Permission.Size = new System.Drawing.Size( 355, 21 );
            this.comboBox_Permission.TabIndex = 13;
            this.comboBox_Permission.Leave += new System.EventHandler( this.Leave_field );
            this.comboBox_Permission.Enter += new System.EventHandler( this.Enter_field );
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point( 12, 167 );
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size( 105, 13 );
            this.label6.TabIndex = 12;
            this.label6.Text = "Requires Permission:";
            // 
            // PsyEditor
            // 
            this.AcceptButton = this.button_Save;
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancel;
            this.ClientSize = new System.Drawing.Size( 389, 557 );
            this.Collectors = new NTAF.PlugInFramework.OCCBase[0];
            this.ControlBox = false;
            this.Controls.Add( this.comboBox_Permission );
            this.Controls.Add( this.label6 );
            this.Controls.Add( this.button_Cancel );
            this.Controls.Add( this.button_Edit );
            this.Controls.Add( this.button_Save );
            this.Controls.Add( this.textBox_Effect );
            this.Controls.Add( this.textBox_Description );
            this.Controls.Add( this.textBox_Range );
            this.Controls.Add( this.textBox_TemplateSize );
            this.Controls.Add( this.textBox_PPCost );
            this.Controls.Add( this.FIELD_Name );
            this.Controls.Add( this.comboBox_PsyType );
            this.Controls.Add( this.comboBox_TemplateType );
            this.Controls.Add( this.label5 );
            this.Controls.Add( this.Label_SkillGroup );
            this.Controls.Add( this.label1 );
            this.Controls.Add( this.label_Description );
            this.Controls.Add( this.label4 );
            this.Controls.Add( this.label3 );
            this.Controls.Add( this.label2 );
            this.Controls.Add( this.label_Name );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "PsyEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PsyForm";
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_Edit;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.TextBox textBox_Description;
        private System.Windows.Forms.TextBox FIELD_Name;
        private System.Windows.Forms.ComboBox comboBox_TemplateType;
        private System.Windows.Forms.Label Label_SkillGroup;
        private System.Windows.Forms.Label label_Description;
        private System.Windows.Forms.Label label_Name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_Effect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_PPCost;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_TemplateSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_Range;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox_PsyType;
        private System.Windows.Forms.ComboBox comboBox_Permission;
        private System.Windows.Forms.Label label6;
    }
}