using NTAF.Core;
using NTAF.Permissions;
using NTAF.PlugInFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NTAF.ObjectClasses
{
	[EditorPlugIn("GUI Psy Editor", "0.0.0.0", true, new Type[] { typeof(Psy) })]
	public class PsyEditor : OCEditorBase
	{
		private IContainer components = null;

		private Button button_Cancel;

		private Button button_Edit;

		private Button button_Save;

		private TextBox textBox_Description;

		private TextBox FIELD_Name;

		private ComboBox comboBox_TemplateType;

		private Label Label_SkillGroup;

		private Label label_Description;

		private Label label_Name;

		private Label label1;

		private TextBox textBox_Effect;

		private Label label2;

		private TextBox textBox_PPCost;

		private Label label3;

		private TextBox textBox_TemplateSize;

		private Label label4;

		private TextBox textBox_Range;

		private Label label5;

		private ComboBox comboBox_PsyType;

		private ComboBox comboBox_Permission;

		private Label label6;

		public PsyEditor()
		{
			base.MyObject = (Psy)Activator.CreateInstance(typeof(Psy));
		}

		protected override void button_Cancel_Click(object sender, EventArgs e)
		{
			base.button_Cancel_Click(sender, e);
		}

		protected override void button_Edit_Click(object sender, EventArgs e)
		{
			base.button_Edit_Click(sender, e);
		}

		protected override void button_Save_Click(object sender, EventArgs e)
		{
			base.button_Save_Click(sender, e);
		}

		protected override void Dispose(bool disposing)
		{
			if ((!disposing ? false : this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		protected override void editing(bool editing)
		{
			base.editing(editing);
			foreach (Control control in base.Controls)
			{
				if (control.Name.Split(new char[] { '\u005F' })[0] == "comboBox")
				{
					((ComboBox)control).Enabled = editing;
				}
			}
		}

		protected override void Enter_field(object sender, EventArgs e)
		{
			base.Enter_field(sender, e);
		}

		private void InitializeComponent()
		{
			this.button_Cancel = new Button();
			this.button_Edit = new Button();
			this.button_Save = new Button();
			this.textBox_Description = new TextBox();
			this.FIELD_Name = new TextBox();
			this.comboBox_TemplateType = new ComboBox();
			this.Label_SkillGroup = new Label();
			this.label_Description = new Label();
			this.label_Name = new Label();
			this.label1 = new Label();
			this.textBox_Effect = new TextBox();
			this.label2 = new Label();
			this.textBox_PPCost = new TextBox();
			this.label3 = new Label();
			this.textBox_TemplateSize = new TextBox();
			this.label4 = new Label();
			this.textBox_Range = new TextBox();
			this.label5 = new Label();
			this.comboBox_PsyType = new ComboBox();
			this.comboBox_Permission = new ComboBox();
			this.label6 = new Label();
			base.SuspendLayout();
			this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button_Cancel.Location = new Point(270, 519);
			this.button_Cancel.Name = "button_Cancel";
			this.button_Cancel.Size = new System.Drawing.Size(102, 23);
			this.button_Cancel.TabIndex = 20;
			this.button_Cancel.Text = "Cancel";
			this.button_Cancel.UseVisualStyleBackColor = true;
			PsyEditor psyEditor = this;
			this.button_Cancel.Click += new EventHandler(psyEditor.button_Cancel_Click);
			this.button_Edit.Location = new Point(54, 520);
			this.button_Edit.Name = "button_Edit";
			this.button_Edit.Size = new System.Drawing.Size(102, 23);
			this.button_Edit.TabIndex = 18;
			this.button_Edit.Text = "Edit";
			this.button_Edit.UseVisualStyleBackColor = true;
			this.button_Edit.Visible = false;
			PsyEditor psyEditor1 = this;
			this.button_Edit.Click += new EventHandler(psyEditor1.button_Edit_Click);
			this.button_Save.Location = new Point(162, 519);
			this.button_Save.Name = "button_Save";
			this.button_Save.Size = new System.Drawing.Size(102, 23);
			this.button_Save.TabIndex = 19;
			this.button_Save.Text = "Finished";
			this.button_Save.UseVisualStyleBackColor = true;
			PsyEditor psyEditor2 = this;
			this.button_Save.Click += new EventHandler(psyEditor2.button_Save_Click);
			this.textBox_Description.Location = new Point(15, 376);
			this.textBox_Description.Multiline = true;
			this.textBox_Description.Name = "textBox_Description";
			this.textBox_Description.ScrollBars = ScrollBars.Both;
			this.textBox_Description.Size = new System.Drawing.Size(355, 134);
			this.textBox_Description.TabIndex = 17;
			PsyEditor psyEditor3 = this;
			this.textBox_Description.Leave += new EventHandler(psyEditor3.Leave_field);
			PsyEditor psyEditor4 = this;
			this.textBox_Description.Enter += new EventHandler(psyEditor4.Enter_field);
			this.FIELD_Name.Location = new Point(15, 25);
			this.FIELD_Name.Name = "FIELD_Name";
			this.FIELD_Name.Size = new System.Drawing.Size(355, 20);
			this.FIELD_Name.TabIndex = 1;
			PsyEditor psyEditor5 = this;
			this.FIELD_Name.Leave += new EventHandler(psyEditor5.Leave_field);
			PsyEditor psyEditor6 = this;
			this.FIELD_Name.Enter += new EventHandler(psyEditor6.Enter_field);
			this.comboBox_TemplateType.FormattingEnabled = true;
			this.comboBox_TemplateType.Location = new Point(15, 143);
			this.comboBox_TemplateType.Name = "comboBox_TemplateType";
			this.comboBox_TemplateType.Size = new System.Drawing.Size(171, 21);
			this.comboBox_TemplateType.TabIndex = 9;
			PsyEditor psyEditor7 = this;
			this.comboBox_TemplateType.Leave += new EventHandler(psyEditor7.Leave_field);
			PsyEditor psyEditor8 = this;
			this.comboBox_TemplateType.Enter += new EventHandler(psyEditor8.Enter_field);
			this.Label_SkillGroup.AutoSize = true;
			this.Label_SkillGroup.Location = new Point(12, 127);
			this.Label_SkillGroup.Name = "Label_SkillGroup";
			this.Label_SkillGroup.Size = new System.Drawing.Size(54, 13);
			this.Label_SkillGroup.TabIndex = 8;
			this.Label_SkillGroup.Text = "Template:";
			this.label_Description.AutoSize = true;
			this.label_Description.Location = new Point(13, 360);
			this.label_Description.Name = "label_Description";
			this.label_Description.Size = new System.Drawing.Size(63, 13);
			this.label_Description.TabIndex = 16;
			this.label_Description.Text = "Description:";
			this.label_Name.AutoSize = true;
			this.label_Name.Location = new Point(12, 9);
			this.label_Name.Name = "label_Name";
			this.label_Name.Size = new System.Drawing.Size(38, 13);
			this.label_Name.TabIndex = 0;
			this.label_Name.Text = "Name:";
			this.label1.AutoSize = true;
			this.label1.Location = new Point(12, 207);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(38, 13);
			this.label1.TabIndex = 14;
			this.label1.Text = "Effect:";
			this.textBox_Effect.Location = new Point(14, 223);
			this.textBox_Effect.Multiline = true;
			this.textBox_Effect.Name = "textBox_Effect";
			this.textBox_Effect.ScrollBars = ScrollBars.Both;
			this.textBox_Effect.Size = new System.Drawing.Size(356, 134);
			this.textBox_Effect.TabIndex = 15;
			PsyEditor psyEditor9 = this;
			this.textBox_Effect.Leave += new EventHandler(psyEditor9.Leave_field);
			PsyEditor psyEditor10 = this;
			this.textBox_Effect.Enter += new EventHandler(psyEditor10.Enter_field);
			this.label2.AutoSize = true;
			this.label2.Location = new Point(12, 88);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "PP Cost:";
			this.textBox_PPCost.Location = new Point(15, 104);
			this.textBox_PPCost.Name = "textBox_PPCost";
			this.textBox_PPCost.Size = new System.Drawing.Size(171, 20);
			this.textBox_PPCost.TabIndex = 5;
			PsyEditor psyEditor11 = this;
			this.textBox_PPCost.Leave += new EventHandler(psyEditor11.Leave_field);
			PsyEditor psyEditor12 = this;
			this.textBox_PPCost.Enter += new EventHandler(psyEditor12.Enter_field);
			this.label3.AutoSize = true;
			this.label3.Location = new Point(196, 128);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(77, 13);
			this.label3.TabIndex = 10;
			this.label3.Text = "Template Size:";
			this.textBox_TemplateSize.Location = new Point(199, 144);
			this.textBox_TemplateSize.Name = "textBox_TemplateSize";
			this.textBox_TemplateSize.Size = new System.Drawing.Size(171, 20);
			this.textBox_TemplateSize.TabIndex = 11;
			PsyEditor psyEditor13 = this;
			this.textBox_TemplateSize.Leave += new EventHandler(psyEditor13.Leave_field);
			PsyEditor psyEditor14 = this;
			this.textBox_TemplateSize.Enter += new EventHandler(psyEditor14.Enter_field);
			this.label4.AutoSize = true;
			this.label4.Location = new Point(196, 88);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(42, 13);
			this.label4.TabIndex = 6;
			this.label4.Text = "Range:";
			this.textBox_Range.Location = new Point(199, 104);
			this.textBox_Range.Name = "textBox_Range";
			this.textBox_Range.Size = new System.Drawing.Size(171, 20);
			this.textBox_Range.TabIndex = 7;
			PsyEditor psyEditor15 = this;
			this.textBox_Range.Leave += new EventHandler(psyEditor15.Leave_field);
			PsyEditor psyEditor16 = this;
			this.textBox_Range.Enter += new EventHandler(psyEditor16.Enter_field);
			this.label5.AutoSize = true;
			this.label5.Location = new Point(12, 48);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(54, 13);
			this.label5.TabIndex = 2;
			this.label5.Text = "Psy Type:";
			this.comboBox_PsyType.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_PsyType.FormattingEnabled = true;
			this.comboBox_PsyType.Location = new Point(15, 64);
			this.comboBox_PsyType.Name = "comboBox_PsyType";
			this.comboBox_PsyType.Size = new System.Drawing.Size(355, 21);
			this.comboBox_PsyType.TabIndex = 3;
			PsyEditor psyEditor17 = this;
			this.comboBox_PsyType.Leave += new EventHandler(psyEditor17.Leave_field);
			PsyEditor psyEditor18 = this;
			this.comboBox_PsyType.Enter += new EventHandler(psyEditor18.Enter_field);
			this.comboBox_Permission.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_Permission.FormattingEnabled = true;
			this.comboBox_Permission.Location = new Point(15, 183);
			this.comboBox_Permission.Name = "comboBox_Permission";
			this.comboBox_Permission.Size = new System.Drawing.Size(355, 21);
			this.comboBox_Permission.TabIndex = 13;
			PsyEditor psyEditor19 = this;
			this.comboBox_Permission.Leave += new EventHandler(psyEditor19.Leave_field);
			PsyEditor psyEditor20 = this;
			this.comboBox_Permission.Enter += new EventHandler(psyEditor20.Enter_field);
			this.label6.AutoSize = true;
			this.label6.Location = new Point(12, 167);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(105, 13);
			this.label6.TabIndex = 12;
			this.label6.Text = "Requires Permission:";
			base.AcceptButton = this.button_Save;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.button_Cancel;
			base.ClientSize = new System.Drawing.Size(389, 557);
			base.Collectors = new OCCBase[0];
			base.ControlBox = false;
			base.Controls.Add(this.comboBox_Permission);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.button_Cancel);
			base.Controls.Add(this.button_Edit);
			base.Controls.Add(this.button_Save);
			base.Controls.Add(this.textBox_Effect);
			base.Controls.Add(this.textBox_Description);
			base.Controls.Add(this.textBox_Range);
			base.Controls.Add(this.textBox_TemplateSize);
			base.Controls.Add(this.textBox_PPCost);
			base.Controls.Add(this.FIELD_Name);
			base.Controls.Add(this.comboBox_PsyType);
			base.Controls.Add(this.comboBox_TemplateType);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.Label_SkillGroup);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.label_Description);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label_Name);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Name = "PsyEditor";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "PsyForm";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		protected override void Leave_field(object sender, EventArgs e)
		{
			base.Leave_field(sender, e);
			if (sender is Control)
			{
				try
				{
					ushort num = 0;
					if (((Control)sender).Name == "textBox_Description")
					{
						((Psy)base.MyObject).Description = this.textBox_Description.Text;
					}
					if (((Control)sender).Name == "textBox_Effect")
					{
						((Psy)base.MyObject).Effect = this.textBox_Effect.Text;
					}
					if (((Control)sender).Name == "textBox_PPCost")
					{
						ushort.TryParse(this.textBox_PPCost.Text, out num);
						((Psy)base.MyObject).ppCost = num;
					}
					if (((Control)sender).Name == "textBox_Range")
					{
						ushort.TryParse(this.textBox_Range.Text, out num);
						((Psy)base.MyObject).Range = num;
					}
					if (((Control)sender).Name == "textBox_TemplateSize")
					{
						((Psy)base.MyObject).TemplateSize = this.textBox_TemplateSize.Text;
					}
					if (((Control)sender).Name == "comboBox_Permission")
					{
						((Psy)base.MyObject).RequiresPermission = (PsyPermission)this.comboBox_Permission.SelectedItem;
					}
					if (((Control)sender).Name == "comboBox_PsyType")
					{
						((Psy)base.MyObject).PsyTypes = (PsyGroup)Enum.Parse(typeof(PsyGroup), this.comboBox_PsyType.SelectedItem.ToString());
					}
					if (((Control)sender).Name == "comboBox_TemplateType")
					{
						((Psy)base.MyObject).Template = (template)Enum.Parse(typeof(template), this.comboBox_TemplateType.SelectedItem.ToString());
					}
				}
				catch (Exception exception)
				{
					MessageBox.Show(exception.Message);
				}
			}
		}

		protected override void PopulateComboboxes()
		{
			foreach (PsyGroup list in GeneralOperations.EnumToList<PsyGroup>())
			{
				this.comboBox_PsyType.Items.Add(GeneralOperations.GetDescription<PsyGroup>(list));
			}
			foreach (template _template in GeneralOperations.EnumToList<template>())
			{
				this.comboBox_TemplateType.Items.Add(GeneralOperations.GetDescription<template>(_template));
			}
			OCCBase[] collectors = base.Collectors;
			for (int i = 0; i < (int)collectors.Length; i++)
			{
				OCCBase oCCBase = collectors[i];
				if (oCCBase.IsOfType(typeof(PsyPermission)))
				{
					this.comboBox_Permission.Items.AddRange(oCCBase.Objects);
				}
			}
			this.comboBox_PsyType.Sorted = true;
			this.comboBox_TemplateType.Sorted = true;
			this.comboBox_Permission.Sorted = true;
		}

		protected override void PopulateFields()
		{
			base.PopulateFields();
			if (((Psy)base.MyObject).RequiresPermission != null)
			{
				bool flag = false;
				foreach (object item in this.comboBox_Permission.Items)
				{
					if (!(item.GetType() == typeof(WSPPermission)) || !(((WSPPermission)item).ID == ((Psy)base.MyObject).RequiresPermission.ID))
					{
						continue;
					}
					flag = true;
				}
				if ((flag ? false : ((Psy)base.MyObject).RequiresPermission.Name != ""))
				{
					this.comboBox_Permission.Items.Add(((Psy)base.MyObject).RequiresPermission);
				}
				if (((Psy)base.MyObject).RequiresPermission.Name != "")
				{
					this.comboBox_Permission.SelectedItem = ((Psy)base.MyObject).RequiresPermission;
				}
			}
			this.comboBox_PsyType.SelectedItem = ((Psy)base.MyObject).PsyTypes.ToString();
			TextBox textBoxPPCost = this.textBox_PPCost;
			ushort myObject = ((Psy)base.MyObject).ppCost;
			textBoxPPCost.Text = myObject.ToString();
			TextBox textBoxRange = this.textBox_Range;
			myObject = ((Psy)base.MyObject).Range;
			textBoxRange.Text = myObject.ToString();
			this.comboBox_TemplateType.SelectedItem = ((Psy)base.MyObject).Template.ToString();
			this.textBox_TemplateSize.Text = ((Psy)base.MyObject).TemplateSize.ToString();
			this.textBox_Effect.Text = ((Psy)base.MyObject).Effect;
			this.textBox_Description.Text = ((Psy)base.MyObject).Description;
		}

		public override EditorExitCode RunEditor(EditorMode mode)
		{
			this.InitializeComponent();
			return base.RunEditor(mode);
		}
	}
}