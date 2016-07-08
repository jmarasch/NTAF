using NTAF.Core;
using NTAF.PlugInFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NTAF.ObjectClasses
{
	[EditorPlugIn("GUI Race Editor", "0.8.0.0", true, typeof(Race))]
	public class RaceEditor : OCEditorBase
	{
		private IContainer components = null;

		private Button button_Cancel;

		private Button button_Edit;

		private Button button_Save;

		private TextBox FIELD_Name;

		private ComboBox comboBox_BaseRace;

		private Label Label_BaseRace;

		private Label label_Name;

		public override Type CollectionType
		{
			get
			{
				return typeof(Race);
			}
		}

		public RaceEditor()
		{
			base.MyObject = (Race)Activator.CreateInstance(typeof(Race));
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
			this.comboBox_BaseRace.Enabled = editing;
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
			this.comboBox_BaseRace = new ComboBox();
			this.Label_BaseRace = new Label();
			this.label_Name = new Label();
			base.SuspendLayout();
			this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button_Cancel.Location = new Point(123, 120);
			this.button_Cancel.Name = "button_Cancel";
			this.button_Cancel.Size = new System.Drawing.Size(102, 23);
			this.button_Cancel.TabIndex = 6;
			this.button_Cancel.Text = "Cancel";
			this.button_Cancel.UseVisualStyleBackColor = true;
			RaceEditor raceEditor = this;
			this.button_Cancel.Click += new EventHandler(raceEditor.button_Cancel_Click);
			this.button_Edit.Location = new Point(15, 91);
			this.button_Edit.Name = "button_Edit";
			this.button_Edit.Size = new System.Drawing.Size(102, 23);
			this.button_Edit.TabIndex = 4;
			this.button_Edit.Text = "Edit";
			this.button_Edit.UseVisualStyleBackColor = true;
			this.button_Edit.Visible = false;
			RaceEditor raceEditor1 = this;
			this.button_Edit.Click += new EventHandler(raceEditor1.button_Edit_Click);
			this.button_Save.Location = new Point(15, 120);
			this.button_Save.Name = "button_Save";
			this.button_Save.Size = new System.Drawing.Size(102, 23);
			this.button_Save.TabIndex = 5;
			this.button_Save.Text = "Finished";
			this.button_Save.UseVisualStyleBackColor = true;
			RaceEditor raceEditor2 = this;
			this.button_Save.Click += new EventHandler(raceEditor2.button_Save_Click);
			this.FIELD_Name.Location = new Point(15, 25);
			this.FIELD_Name.Name = "FIELD_Name";
			this.FIELD_Name.Size = new System.Drawing.Size(210, 20);
			this.FIELD_Name.TabIndex = 1;
			RaceEditor raceEditor3 = this;
			this.FIELD_Name.Leave += new EventHandler(raceEditor3.Leave_field);
			RaceEditor raceEditor4 = this;
			this.FIELD_Name.Enter += new EventHandler(raceEditor4.Enter_field);
			this.comboBox_BaseRace.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_BaseRace.FormattingEnabled = true;
			this.comboBox_BaseRace.Location = new Point(15, 64);
			this.comboBox_BaseRace.Name = "comboBox_BaseRace";
			this.comboBox_BaseRace.Size = new System.Drawing.Size(210, 21);
			this.comboBox_BaseRace.TabIndex = 3;
			RaceEditor raceEditor5 = this;
			this.comboBox_BaseRace.Leave += new EventHandler(raceEditor5.Leave_field);
			this.Label_BaseRace.AutoSize = true;
			this.Label_BaseRace.Location = new Point(12, 48);
			this.Label_BaseRace.Name = "Label_BaseRace";
			this.Label_BaseRace.Size = new System.Drawing.Size(48, 13);
			this.Label_BaseRace.TabIndex = 2;
			this.Label_BaseRace.Text = "Species:";
			this.label_Name.AutoSize = true;
			this.label_Name.Location = new Point(12, 9);
			this.label_Name.Name = "label_Name";
			this.label_Name.Size = new System.Drawing.Size(67, 13);
			this.label_Name.TabIndex = 0;
			this.label_Name.Text = "Race Name:";
			base.AcceptButton = this.button_Save;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.button_Cancel;
			base.ClientSize = new System.Drawing.Size(232, 152);
			base.Collectors = new OCCBase[0];
			base.ControlBox = false;
			base.Controls.Add(this.button_Cancel);
			base.Controls.Add(this.button_Edit);
			base.Controls.Add(this.button_Save);
			base.Controls.Add(this.FIELD_Name);
			base.Controls.Add(this.comboBox_BaseRace);
			base.Controls.Add(this.Label_BaseRace);
			base.Controls.Add(this.label_Name);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Name = "RaceEditor";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "RaceForm";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		protected override void Leave_field(object sender, EventArgs e)
		{
			base.Leave_field(sender, e);
			if (sender is Control)
			{
				if (((Control)sender).Name == this.comboBox_BaseRace.Name)
				{
					((Race)base.MyObject).species = (Species)Enum.Parse(typeof(Species), this.comboBox_BaseRace.SelectedItem.ToString());
				}
			}
		}

		protected override void PopulateComboboxes()
		{
			foreach (Species list in GeneralOperations.EnumToList<Species>())
			{
				this.comboBox_BaseRace.Items.Add(GeneralOperations.GetDescription<Species>(list));
			}
			this.comboBox_BaseRace.Sorted = true;
		}

		protected override void PopulateFields()
		{
			this.FIELD_Name.Text = base.MyObject.Name;
			this.comboBox_BaseRace.SelectedItem = ((Race)base.MyObject).species.ToString();
		}

		public override EditorExitCode RunEditor(EditorMode mode)
		{
			this.InitializeComponent();
			return base.RunEditor(mode);
		}
	}
}