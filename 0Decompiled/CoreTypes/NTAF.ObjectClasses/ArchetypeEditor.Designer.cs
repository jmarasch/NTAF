using NTAF.Core;
using NTAF.PlugInFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NTAF.ObjectClasses
{
	[EditorPlugIn("GUI Archetype Editor", "0.0.0.0", true, typeof(Archetype))]
	public class ArchetypeEditor : OCEditorBase
	{
		private IContainer components = null;

		private Button button_Cancel;

		private Button button_Edit;

		private Button button_Save;

		private TextBox FIELD_Name;

		private ComboBox comboBox_BaseUnitType;

		private Label Label_BaseRace;

		private Label label_Name;

		private Label Label_ID;

		public override Type CollectionType
		{
			get
			{
				return typeof(Archetype);
			}
		}

		public ArchetypeEditor()
		{
			base.MyObject = (Archetype)Activator.CreateInstance(typeof(Archetype));
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
			this.comboBox_BaseUnitType.Enabled = editing;
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
			this.FIELD_Name = new TextBox();
			this.comboBox_BaseUnitType = new ComboBox();
			this.Label_BaseRace = new Label();
			this.label_Name = new Label();
			this.Label_ID = new Label();
			base.SuspendLayout();
			this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button_Cancel.Location = new Point(119, 120);
			this.button_Cancel.Name = "button_Cancel";
			this.button_Cancel.Size = new System.Drawing.Size(102, 23);
			this.button_Cancel.TabIndex = 6;
			this.button_Cancel.Text = "&Cancel";
			this.button_Cancel.UseVisualStyleBackColor = true;
			ArchetypeEditor archetypeEditor = this;
			this.button_Cancel.Click += new EventHandler(archetypeEditor.button_Cancel_Click);
			this.button_Edit.Location = new Point(11, 91);
			this.button_Edit.Name = "button_Edit";
			this.button_Edit.Size = new System.Drawing.Size(102, 23);
			this.button_Edit.TabIndex = 4;
			this.button_Edit.Text = "&Edit";
			this.button_Edit.UseVisualStyleBackColor = true;
			this.button_Edit.Visible = false;
			ArchetypeEditor archetypeEditor1 = this;
			this.button_Edit.Click += new EventHandler(archetypeEditor1.button_Edit_Click);
			this.button_Save.Location = new Point(11, 120);
			this.button_Save.Name = "button_Save";
			this.button_Save.Size = new System.Drawing.Size(102, 23);
			this.button_Save.TabIndex = 5;
			this.button_Save.Text = "&Finished";
			this.button_Save.UseVisualStyleBackColor = true;
			ArchetypeEditor archetypeEditor2 = this;
			this.button_Save.Click += new EventHandler(archetypeEditor2.button_Save_Click);
			this.FIELD_Name.Location = new Point(11, 25);
			this.FIELD_Name.Name = "FIELD_Name";
			this.FIELD_Name.Size = new System.Drawing.Size(210, 20);
			this.FIELD_Name.TabIndex = 1;
			ArchetypeEditor archetypeEditor3 = this;
			this.FIELD_Name.Leave += new EventHandler(archetypeEditor3.Leave_field);
			ArchetypeEditor archetypeEditor4 = this;
			this.FIELD_Name.Enter += new EventHandler(archetypeEditor4.Enter_field);
			this.comboBox_BaseUnitType.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_BaseUnitType.FormattingEnabled = true;
			this.comboBox_BaseUnitType.Location = new Point(11, 64);
			this.comboBox_BaseUnitType.Name = "comboBox_BaseUnitType";
			this.comboBox_BaseUnitType.Size = new System.Drawing.Size(210, 21);
			this.comboBox_BaseUnitType.TabIndex = 3;
			ArchetypeEditor archetypeEditor5 = this;
			this.comboBox_BaseUnitType.Leave += new EventHandler(archetypeEditor5.Leave_field);
			this.Label_BaseRace.AutoSize = true;
			this.Label_BaseRace.Location = new Point(8, 48);
			this.Label_BaseRace.Name = "Label_BaseRace";
			this.Label_BaseRace.Size = new System.Drawing.Size(83, 13);
			this.Label_BaseRace.TabIndex = 2;
			this.Label_BaseRace.Text = "&Base Unit Type:";
			this.label_Name.AutoSize = true;
			this.label_Name.Location = new Point(8, 9);
			this.label_Name.Name = "label_Name";
			this.label_Name.Size = new System.Drawing.Size(38, 13);
			this.label_Name.TabIndex = 0;
			this.label_Name.Text = "&Name:";
			this.Label_ID.AutoSize = true;
			this.Label_ID.Location = new Point(8, 7);
			this.Label_ID.Name = "Label_ID";
			this.Label_ID.Size = new System.Drawing.Size(21, 13);
			this.Label_ID.TabIndex = 21;
			this.Label_ID.Text = "ID:";
			this.Label_ID.Visible = false;
			base.AcceptButton = this.button_Save;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.button_Cancel;
			base.ClientSize = new System.Drawing.Size(232, 149);
			base.Collectors = new OCCBase[0];
			base.ControlBox = false;
			base.Controls.Add(this.button_Cancel);
			base.Controls.Add(this.button_Edit);
			base.Controls.Add(this.button_Save);
			base.Controls.Add(this.FIELD_Name);
			base.Controls.Add(this.comboBox_BaseUnitType);
			base.Controls.Add(this.Label_BaseRace);
			base.Controls.Add(this.label_Name);
			base.Controls.Add(this.Label_ID);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Name = "ArchetypeEditor";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "UnitTypeForm";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		protected override void Leave_field(object sender, EventArgs e)
		{
			base.Leave_field(sender, e);
			if (sender is Control)
			{
				if ((((Control)sender).Name == this.comboBox_BaseUnitType.Name) & this.comboBox_BaseUnitType.SelectedItem != null)
				{
					((Archetype)base.MyObject).BaseType = (ArchetypeBaseEnu)Enum.Parse(typeof(ArchetypeBaseEnu), this.comboBox_BaseUnitType.SelectedItem.ToString());
				}
			}
		}

		protected override void PopulateComboboxes()
		{
			foreach (ArchetypeBaseEnu list in GeneralOperations.EnumToList<ArchetypeBaseEnu>())
			{
				if ((list == ArchetypeBaseEnu.All ? true : list == ArchetypeBaseEnu.New))
				{
					continue;
				}
				this.comboBox_BaseUnitType.Items.Add(GeneralOperations.GetDescription<ArchetypeBaseEnu>(list).Split(new char[] { ',' })[0]);
			}
			this.comboBox_BaseUnitType.Sorted = true;
		}

		protected override void PopulateFields()
		{
			this.FIELD_Name.Text = base.MyObject.Name;
			this.comboBox_BaseUnitType.SelectedItem = GeneralOperations.GetDescription<ArchetypeBaseEnu>(((Archetype)base.MyObject).BaseType).Split(new char[] { ',' })[0];
		}

		public override EditorExitCode RunEditor(EditorMode mode)
		{
			this.InitializeComponent();
			return base.RunEditor(mode);
		}
	}
}