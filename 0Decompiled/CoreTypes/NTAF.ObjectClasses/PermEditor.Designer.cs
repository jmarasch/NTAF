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
	[EditorPlugIn("GUI WPSPermission Editor", "0.0.0.0", true, new Type[] { typeof(WeaponPermission), typeof(PsyPermission), typeof(SkillPermission) })]
	public class PermEditor : OCEditorBase
	{
		private IContainer components = null;

		private Button button_Cancel;

		private Button button_Edit;

		private Button button_Save;

		private TextBox FIELD_Name;

		private Label label_Name;

		private Label Label_ID;

		private Label label1;

		private ComboBox comboBox_ForGroup;

		private GroupBox groupBox1;

		private CheckBox rcFun;

		private CheckBox rcAquatic;

		private CheckBox rcDemonic;

		private CheckBox rcUndead;

		private CheckBox rcAngelic;

		private CheckBox rcMutant;

		private CheckBox rcGenicB;

		private CheckBox rcHuman;

		private CheckBox rcGenicA;

		private CheckBox rcAll;

		private Label label2;

		public ComboBox comboBox_PermType;

		public override Type CollectionType
		{
			get
			{
				return typeof(WSPPermission);
			}
		}

		public PermEditor()
		{
			base.MyObject = (WeaponPermission)Activator.CreateInstance(typeof(WeaponPermission));
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
			base.MyObject = PermissionFormatter.changeType((WSPPermission)base.MyObject, (PermissionType)Enum.Parse(typeof(PermissionType), this.comboBox_PermType.SelectedItem.ToString()));
			base.button_Save_Click(sender, e);
		}

		private void CheckForChecks()
		{
			if (!base.FormLoading())
			{
				((WSPPermission)base.MyObject).SpeciesCanEquip = (Species)0;
				bool flag = false;
				foreach (Control control in this.groupBox1.Controls)
				{
					if (((CheckBox)control).Checked)
					{
						flag = true;
						if (control.Name == this.rcAll.Name && this.rcAll.Checked)
						{
							((WSPPermission)base.MyObject).SpeciesCanEquip = Species.All;
						}
						if (control.Name == this.rcHuman.Name && this.rcHuman.Checked)
						{
							WSPPermission myObject = (WSPPermission)base.MyObject;
							myObject.SpeciesCanEquip = myObject.SpeciesCanEquip | Species.Human;
						}
						if (control.Name == this.rcMutant.Name && this.rcMutant.Checked)
						{
							WSPPermission speciesCanEquip = (WSPPermission)base.MyObject;
							speciesCanEquip.SpeciesCanEquip = speciesCanEquip.SpeciesCanEquip | Species.Mutant;
						}
						if (control.Name == this.rcUndead.Name && this.rcUndead.Checked)
						{
							WSPPermission wSPPermission = (WSPPermission)base.MyObject;
							wSPPermission.SpeciesCanEquip = wSPPermission.SpeciesCanEquip | Species.Undead;
						}
						if (control.Name == this.rcAquatic.Name && this.rcAquatic.Checked)
						{
							WSPPermission myObject1 = (WSPPermission)base.MyObject;
							myObject1.SpeciesCanEquip = myObject1.SpeciesCanEquip | Species.Aquatic;
						}
						if (control.Name == this.rcGenicA.Name && this.rcGenicA.Checked)
						{
							WSPPermission speciesCanEquip1 = (WSPPermission)base.MyObject;
							speciesCanEquip1.SpeciesCanEquip = speciesCanEquip1.SpeciesCanEquip | Species.Genics_A;
						}
						if (control.Name == this.rcGenicB.Name && this.rcGenicB.Checked)
						{
							WSPPermission wSPPermission1 = (WSPPermission)base.MyObject;
							wSPPermission1.SpeciesCanEquip = wSPPermission1.SpeciesCanEquip | Species.Genics_B;
						}
						if (control.Name == this.rcAngelic.Name && this.rcAngelic.Checked)
						{
							WSPPermission myObject2 = (WSPPermission)base.MyObject;
							myObject2.SpeciesCanEquip = myObject2.SpeciesCanEquip | Species.Angelic;
						}
						if (control.Name == this.rcDemonic.Name && this.rcDemonic.Checked)
						{
							WSPPermission speciesCanEquip2 = (WSPPermission)base.MyObject;
							speciesCanEquip2.SpeciesCanEquip = speciesCanEquip2.SpeciesCanEquip | Species.Demonic;
						}
						if (control.Name == this.rcFun.Name && this.rcFun.Checked)
						{
							WSPPermission wSPPermission2 = (WSPPermission)base.MyObject;
							wSPPermission2.SpeciesCanEquip = wSPPermission2.SpeciesCanEquip | Species.Fun;
						}
					}
				}
				if ((base.Mode == EditorMode.Edit ? true : base.Mode == EditorMode.New))
				{
					this.comboBox_ForGroup.Enabled = !flag;
					if (flag)
					{
						this.comboBox_ForGroup.SelectedItem = null;
					}
				}
			}
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
			this.comboBox_ForGroup.Enabled = editing;
			this.comboBox_PermType.Enabled = editing;
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
			this.label_Name = new Label();
			this.Label_ID = new Label();
			this.label1 = new Label();
			this.comboBox_ForGroup = new ComboBox();
			this.groupBox1 = new GroupBox();
			this.rcFun = new CheckBox();
			this.rcAquatic = new CheckBox();
			this.rcDemonic = new CheckBox();
			this.rcUndead = new CheckBox();
			this.rcAngelic = new CheckBox();
			this.rcMutant = new CheckBox();
			this.rcGenicB = new CheckBox();
			this.rcHuman = new CheckBox();
			this.rcGenicA = new CheckBox();
			this.rcAll = new CheckBox();
			this.label2 = new Label();
			this.comboBox_PermType = new ComboBox();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button_Cancel.Location = new Point(473, 165);
			this.button_Cancel.Name = "button_Cancel";
			this.button_Cancel.Size = new System.Drawing.Size(102, 23);
			this.button_Cancel.TabIndex = 9;
			this.button_Cancel.Text = "Cancel";
			this.button_Cancel.UseVisualStyleBackColor = true;
			PermEditor permEditor = this;
			this.button_Cancel.Click += new EventHandler(permEditor.button_Cancel_Click);
			this.button_Edit.Location = new Point(257, 166);
			this.button_Edit.Name = "button_Edit";
			this.button_Edit.Size = new System.Drawing.Size(102, 23);
			this.button_Edit.TabIndex = 7;
			this.button_Edit.Text = "Edit";
			this.button_Edit.UseVisualStyleBackColor = true;
			this.button_Edit.Visible = false;
			PermEditor permEditor1 = this;
			this.button_Edit.Click += new EventHandler(permEditor1.button_Edit_Click);
			this.button_Save.Location = new Point(365, 165);
			this.button_Save.Name = "button_Save";
			this.button_Save.Size = new System.Drawing.Size(102, 23);
			this.button_Save.TabIndex = 8;
			this.button_Save.Text = "Finished";
			this.button_Save.UseVisualStyleBackColor = true;
			PermEditor permEditor2 = this;
			this.button_Save.Click += new EventHandler(permEditor2.button_Save_Click);
			this.FIELD_Name.Location = new Point(15, 25);
			this.FIELD_Name.Name = "FIELD_Name";
			this.FIELD_Name.Size = new System.Drawing.Size(355, 20);
			this.FIELD_Name.TabIndex = 1;
			PermEditor permEditor3 = this;
			this.FIELD_Name.Leave += new EventHandler(permEditor3.Leave_field);
			PermEditor permEditor4 = this;
			this.FIELD_Name.Enter += new EventHandler(permEditor4.Enter_field);
			this.label_Name.AutoSize = true;
			this.label_Name.Location = new Point(12, 9);
			this.label_Name.Name = "label_Name";
			this.label_Name.Size = new System.Drawing.Size(38, 13);
			this.label_Name.TabIndex = 0;
			this.label_Name.Text = "Name:";
			this.Label_ID.AutoSize = true;
			this.Label_ID.Location = new Point(12, 9);
			this.Label_ID.Name = "Label_ID";
			this.Label_ID.Size = new System.Drawing.Size(21, 13);
			this.Label_ID.TabIndex = 35;
			this.Label_ID.Text = "ID:";
			this.Label_ID.Visible = false;
			this.label1.AutoSize = true;
			this.label1.Location = new Point(12, 60);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(95, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "For Specific Race:";
			this.comboBox_ForGroup.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_ForGroup.FormattingEnabled = true;
			this.comboBox_ForGroup.Location = new Point(15, 76);
			this.comboBox_ForGroup.Name = "comboBox_ForGroup";
			this.comboBox_ForGroup.Size = new System.Drawing.Size(355, 21);
			this.comboBox_ForGroup.TabIndex = 3;
			PermEditor permEditor5 = this;
			this.comboBox_ForGroup.Leave += new EventHandler(permEditor5.Leave_field);
			this.groupBox1.Controls.Add(this.rcFun);
			this.groupBox1.Controls.Add(this.rcAquatic);
			this.groupBox1.Controls.Add(this.rcDemonic);
			this.groupBox1.Controls.Add(this.rcUndead);
			this.groupBox1.Controls.Add(this.rcAngelic);
			this.groupBox1.Controls.Add(this.rcMutant);
			this.groupBox1.Controls.Add(this.rcGenicB);
			this.groupBox1.Controls.Add(this.rcHuman);
			this.groupBox1.Controls.Add(this.rcGenicA);
			this.groupBox1.Controls.Add(this.rcAll);
			this.groupBox1.Location = new Point(387, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(188, 142);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "For General Species";
			this.rcFun.AutoSize = true;
			this.rcFun.Location = new Point(81, 111);
			this.rcFun.Name = "rcFun";
			this.rcFun.Size = new System.Drawing.Size(44, 17);
			this.rcFun.TabIndex = 9;
			this.rcFun.Text = "Fun";
			this.rcFun.UseVisualStyleBackColor = true;
			this.rcFun.CheckedChanged += new EventHandler(this.rcAll_CheckedChanged);
			this.rcAquatic.AutoSize = true;
			this.rcAquatic.Location = new Point(6, 111);
			this.rcAquatic.Name = "rcAquatic";
			this.rcAquatic.Size = new System.Drawing.Size(62, 17);
			this.rcAquatic.TabIndex = 4;
			this.rcAquatic.Text = "Aquatic";
			this.rcAquatic.UseVisualStyleBackColor = true;
			this.rcAquatic.CheckedChanged += new EventHandler(this.rcAll_CheckedChanged);
			this.rcDemonic.AutoSize = true;
			this.rcDemonic.Location = new Point(81, 88);
			this.rcDemonic.Name = "rcDemonic";
			this.rcDemonic.Size = new System.Drawing.Size(68, 17);
			this.rcDemonic.TabIndex = 8;
			this.rcDemonic.Text = "Demonic";
			this.rcDemonic.UseVisualStyleBackColor = true;
			this.rcDemonic.CheckedChanged += new EventHandler(this.rcAll_CheckedChanged);
			this.rcUndead.AutoSize = true;
			this.rcUndead.Location = new Point(6, 88);
			this.rcUndead.Name = "rcUndead";
			this.rcUndead.Size = new System.Drawing.Size(64, 17);
			this.rcUndead.TabIndex = 3;
			this.rcUndead.Text = "Undead";
			this.rcUndead.UseVisualStyleBackColor = true;
			this.rcUndead.CheckedChanged += new EventHandler(this.rcAll_CheckedChanged);
			this.rcAngelic.AutoSize = true;
			this.rcAngelic.Location = new Point(81, 65);
			this.rcAngelic.Name = "rcAngelic";
			this.rcAngelic.Size = new System.Drawing.Size(61, 17);
			this.rcAngelic.TabIndex = 7;
			this.rcAngelic.Text = "Angelic";
			this.rcAngelic.UseVisualStyleBackColor = true;
			this.rcAngelic.CheckedChanged += new EventHandler(this.rcAll_CheckedChanged);
			this.rcMutant.AutoSize = true;
			this.rcMutant.Location = new Point(6, 65);
			this.rcMutant.Name = "rcMutant";
			this.rcMutant.Size = new System.Drawing.Size(59, 17);
			this.rcMutant.TabIndex = 2;
			this.rcMutant.Text = "Mutant";
			this.rcMutant.UseVisualStyleBackColor = true;
			this.rcMutant.CheckedChanged += new EventHandler(this.rcAll_CheckedChanged);
			this.rcGenicB.AutoSize = true;
			this.rcGenicB.Location = new Point(81, 42);
			this.rcGenicB.Name = "rcGenicB";
			this.rcGenicB.Size = new System.Drawing.Size(91, 17);
			this.rcGenicB.TabIndex = 6;
			this.rcGenicB.Text = "Genic Type B";
			this.rcGenicB.UseVisualStyleBackColor = true;
			this.rcGenicB.CheckedChanged += new EventHandler(this.rcAll_CheckedChanged);
			this.rcHuman.AutoSize = true;
			this.rcHuman.Location = new Point(6, 42);
			this.rcHuman.Name = "rcHuman";
			this.rcHuman.Size = new System.Drawing.Size(60, 17);
			this.rcHuman.TabIndex = 1;
			this.rcHuman.Text = "Human";
			this.rcHuman.UseVisualStyleBackColor = true;
			this.rcHuman.CheckedChanged += new EventHandler(this.rcAll_CheckedChanged);
			this.rcGenicA.AutoSize = true;
			this.rcGenicA.Location = new Point(81, 19);
			this.rcGenicA.Name = "rcGenicA";
			this.rcGenicA.Size = new System.Drawing.Size(91, 17);
			this.rcGenicA.TabIndex = 5;
			this.rcGenicA.Text = "Genic Type A";
			this.rcGenicA.UseVisualStyleBackColor = true;
			this.rcGenicA.CheckedChanged += new EventHandler(this.rcAll_CheckedChanged);
			this.rcAll.AutoSize = true;
			this.rcAll.Location = new Point(6, 19);
			this.rcAll.Name = "rcAll";
			this.rcAll.Size = new System.Drawing.Size(37, 17);
			this.rcAll.TabIndex = 0;
			this.rcAll.Text = "All";
			this.rcAll.UseVisualStyleBackColor = true;
			this.rcAll.CheckedChanged += new EventHandler(this.rcAll_CheckedChanged);
			this.label2.AutoSize = true;
			this.label2.Location = new Point(12, 104);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(87, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Permission Type:";
			this.comboBox_PermType.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_PermType.FormattingEnabled = true;
			this.comboBox_PermType.Location = new Point(15, 120);
			this.comboBox_PermType.Name = "comboBox_PermType";
			this.comboBox_PermType.Size = new System.Drawing.Size(355, 21);
			this.comboBox_PermType.TabIndex = 5;
			PermEditor permEditor6 = this;
			this.comboBox_PermType.Leave += new EventHandler(permEditor6.Leave_field);
			base.AcceptButton = this.button_Save;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.button_Cancel;
			base.ClientSize = new System.Drawing.Size(588, 198);
			base.Collectors = new OCCBase[0];
			base.ControlBox = false;
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.button_Cancel);
			base.Controls.Add(this.button_Edit);
			base.Controls.Add(this.button_Save);
			base.Controls.Add(this.FIELD_Name);
			base.Controls.Add(this.comboBox_PermType);
			base.Controls.Add(this.comboBox_ForGroup);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.label_Name);
			base.Controls.Add(this.Label_ID);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Name = "PermEditor";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "PsyPerm";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		protected override void Leave_field(object sender, EventArgs e)
		{
			base.Leave_field(sender, e);
			if (sender is Control)
			{
				if (((Control)sender).Name == this.comboBox_ForGroup.Name)
				{
					((WSPPermission)base.MyObject).RaceCanEquip = (Race)this.comboBox_ForGroup.SelectedItem;
				}
			}
		}

		protected override void PopulateComboboxes()
		{
			OCCBase[] collectors = base.Collectors;
			for (int i = 0; i < (int)collectors.Length; i++)
			{
				OCCBase oCCBase = collectors[i];
				if (oCCBase.IsOfType(typeof(Race)))
				{
					this.comboBox_ForGroup.Items.AddRange(oCCBase.Objects);
				}
			}
			this.comboBox_ForGroup.Sorted = true;
			foreach (PermissionType list in GeneralOperations.EnumToList<PermissionType>())
			{
				this.comboBox_PermType.Items.Add(GeneralOperations.GetDescription<PermissionType>(list));
			}
		}

		protected override void PopulateFields()
		{
			base.PopulateFields();
			if (base.MyObject is WeaponPermission)
			{
				this.comboBox_PermType.SelectedItem = PermissionType.Weapon.ToString();
			}
			if (base.MyObject is SkillPermission)
			{
				this.comboBox_PermType.SelectedItem = PermissionType.Skill.ToString();
			}
			if (base.MyObject is PsyPermission)
			{
				this.comboBox_PermType.SelectedItem = PermissionType.Psy.ToString();
			}
			if (this.comboBox_PermType.SelectedItem == null)
			{
				this.comboBox_PermType.SelectedItem = PermissionType.Weapon.ToString();
			}
			if (base.MyObject != null)
			{
				if (((WSPPermission)base.MyObject).RaceCanEquip != null)
				{
					try
					{
						foreach (Race item in this.comboBox_ForGroup.Items)
						{
							if (item.ID != ((WSPPermission)base.MyObject).RaceCanEquip.ID)
							{
								continue;
							}
							this.comboBox_ForGroup.SelectedItem = item;
							break;
						}
						if (this.comboBox_ForGroup.SelectedItem == null)
						{
							this.comboBox_ForGroup.Items.Add(((WSPPermission)base.MyObject).RaceCanEquip);
							this.comboBox_ForGroup.SelectedItem = ((WSPPermission)base.MyObject).RaceCanEquip;
						}
					}
					catch
					{
						this.comboBox_ForGroup.SelectedItem = null;
						MessageBox.Show("Race not found possible data error...", "Error...", MessageBoxButtons.OK);
					}
				}
				if (!((WSPPermission)base.MyObject).SpeciesCanEquip.Is<Species>(Species.All))
				{
					if (!((WSPPermission)base.MyObject).SpeciesCanEquip.Is<Species>(Species.Angelic))
					{
						this.rcAngelic.Checked = false;
					}
					else
					{
						this.rcAngelic.Checked = true;
					}
					if (!((WSPPermission)base.MyObject).SpeciesCanEquip.Is<Species>(Species.Aquatic))
					{
						this.rcAquatic.Checked = false;
					}
					else
					{
						this.rcAquatic.Checked = true;
					}
					if (!((WSPPermission)base.MyObject).SpeciesCanEquip.Is<Species>(Species.Demonic))
					{
						this.rcDemonic.Checked = false;
					}
					else
					{
						this.rcDemonic.Checked = true;
					}
					if (!((WSPPermission)base.MyObject).SpeciesCanEquip.Is<Species>(Species.Fun))
					{
						this.rcFun.Checked = false;
					}
					else
					{
						this.rcFun.Checked = true;
					}
					if (!((WSPPermission)base.MyObject).SpeciesCanEquip.Is<Species>(Species.Genics_A))
					{
						this.rcGenicA.Checked = false;
					}
					else
					{
						this.rcGenicA.Checked = true;
					}
					if (!((WSPPermission)base.MyObject).SpeciesCanEquip.Is<Species>(Species.Genics_B))
					{
						this.rcGenicB.Checked = false;
					}
					else
					{
						this.rcGenicB.Checked = true;
					}
					if (!((WSPPermission)base.MyObject).SpeciesCanEquip.Is<Species>(Species.Human))
					{
						this.rcHuman.Checked = false;
					}
					else
					{
						this.rcHuman.Checked = true;
					}
					if (!((WSPPermission)base.MyObject).SpeciesCanEquip.Is<Species>(Species.Mutant))
					{
						this.rcMutant.Checked = false;
					}
					else
					{
						this.rcMutant.Checked = true;
					}
					if (!((WSPPermission)base.MyObject).SpeciesCanEquip.Is<Species>(Species.Undead))
					{
						this.rcUndead.Checked = false;
					}
					else
					{
						this.rcUndead.Checked = true;
					}
				}
				else
				{
					this.rcAll.Checked = true;
				}
			}
		}

		private void rcAll_CheckedChanged(object sender, EventArgs e)
		{
			if (!base.FormLoading())
			{
				if (!this.rcAll.Checked)
				{
					bool flag = true;
					this.rcHuman.Enabled = flag;
					this.rcMutant.Enabled = flag;
					this.rcUndead.Enabled = flag;
					this.rcAquatic.Enabled = flag;
					this.rcGenicA.Enabled = flag;
					this.rcGenicB.Enabled = flag;
					this.rcAngelic.Enabled = flag;
					this.rcDemonic.Enabled = flag;
					this.rcFun.Enabled = flag;
				}
				else
				{
					bool flag1 = false;
					this.rcHuman.Enabled = flag1;
					this.rcMutant.Enabled = flag1;
					this.rcUndead.Enabled = flag1;
					this.rcAquatic.Enabled = flag1;
					this.rcGenicA.Enabled = flag1;
					this.rcGenicB.Enabled = flag1;
					this.rcAngelic.Enabled = flag1;
					this.rcDemonic.Enabled = flag1;
					this.rcFun.Enabled = flag1;
					this.rcHuman.Checked = flag1;
					this.rcMutant.Checked = flag1;
					this.rcUndead.Checked = flag1;
					this.rcAquatic.Checked = flag1;
					this.rcGenicA.Checked = flag1;
					this.rcGenicB.Checked = flag1;
					this.rcAngelic.Checked = flag1;
					this.rcDemonic.Checked = flag1;
					this.rcFun.Checked = flag1;
				}
				this.CheckForChecks();
			}
		}

		public override EditorExitCode RunEditor(EditorMode mode)
		{
			this.InitializeComponent();
			return base.RunEditor(mode);
		}
	}
}