using NTAF.Core;
using NTAF.PlugInFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NTAF.ObjectClasses
{
	[EditorPlugIn("GUI Skill Editor", "0.0.0.0", true, new Type[] { typeof(Skill) })]
	public class SkillEditor : OCEditorBase
	{
		private IContainer components = null;

		private GroupBox groupBox1;

		private CheckBox rcAll;

		private CheckBox rcFun;

		private CheckBox rcAquatic;

		private CheckBox rcDemonic;

		private CheckBox rcUndead;

		private CheckBox rcAngelic;

		private CheckBox rcMutant;

		private CheckBox rcGenicB;

		private CheckBox rcHuman;

		private CheckBox rcGenicA;

		private ComboBox comboBox_SkillGroup;

		private Label Label_SkillGroup;

		private Label label_Name;

		private TextBox FIELD_Name;

		private ListBox listBox_StatMods;

		private Label label_StatMods;

		private Button button_AddMod;

		private Button button_RemoveMod;

		private Label label_Description;

		private TextBox textBox_Description;

		private Label label_Cost;

		private TextBox textBox_Cost;

		private Button button_Save;

		private Button button_Cancel;

		private ComboBox comboBox_StatToMod;

		private TextBox textBox_StatModAmount;

		private Button button_Edit;

		private ComboBox comboBox_Permission;

		private Label label1;

		public SkillEditor()
		{
			base.MyObject = (Skill)Activator.CreateInstance(typeof(Skill));
		}

		private void button_AddMod_Click(object sender, EventArgs e)
		{
			int num = 0;
			int.TryParse(this.textBox_StatModAmount.Text, out num);
			this.listBox_StatMods.Items.Add(new StatsMod((StatsMod.Stats)Enum.Parse(typeof(StatsMod.Stats), this.comboBox_StatToMod.SelectedItem.ToString()), num));
		}

		protected override void button_Cancel_Click(object sender, EventArgs e)
		{
			base.button_Cancel_Click(sender, e);
		}

		protected override void button_Edit_Click(object sender, EventArgs e)
		{
			base.button_Edit_Click(sender, e);
		}

		private void button_RemoveMod_Click(object sender, EventArgs e)
		{
			this.listBox_StatMods.Items.Remove(this.listBox_StatMods.SelectedItem);
		}

		protected override void button_Save_Click(object sender, EventArgs e)
		{
			((Skill)base.MyObject).ModifiesStats.Clear();
			foreach (StatsMod item in this.listBox_StatMods.Items)
			{
				((Skill)base.MyObject).ModifiesStats.Add(item);
			}
			base.button_Save_Click(sender, e);
		}

		private void CheckForChecks()
		{
			if (!base.FormLoading())
			{
				Skill myObject = (Skill)base.MyObject;
				myObject.SpeciesCanUseSkill = (Species)0;
				foreach (Control control in this.groupBox1.Controls)
				{
					if (((CheckBox)control).Checked)
					{
						if (control.Name == this.rcAll.Name)
						{
							myObject.SpeciesCanUseSkill = Species.All;
						}
						if (control.Name == this.rcHuman.Name)
						{
							Skill speciesCanUseSkill = myObject;
							speciesCanUseSkill.SpeciesCanUseSkill = speciesCanUseSkill.SpeciesCanUseSkill | Species.Human;
						}
						if (control.Name == this.rcMutant.Name)
						{
							Skill skill = myObject;
							skill.SpeciesCanUseSkill = skill.SpeciesCanUseSkill | Species.Mutant;
						}
						if (control.Name == this.rcUndead.Name)
						{
							Skill speciesCanUseSkill1 = myObject;
							speciesCanUseSkill1.SpeciesCanUseSkill = speciesCanUseSkill1.SpeciesCanUseSkill | Species.Undead;
						}
						if (control.Name == this.rcAquatic.Name)
						{
							Skill skill1 = myObject;
							skill1.SpeciesCanUseSkill = skill1.SpeciesCanUseSkill | Species.Aquatic;
						}
						if (control.Name == this.rcGenicA.Name)
						{
							Skill speciesCanUseSkill2 = myObject;
							speciesCanUseSkill2.SpeciesCanUseSkill = speciesCanUseSkill2.SpeciesCanUseSkill | Species.Genics_A;
						}
						if (control.Name == this.rcGenicB.Name)
						{
							Skill skill2 = myObject;
							skill2.SpeciesCanUseSkill = skill2.SpeciesCanUseSkill | Species.Genics_B;
						}
						if (control.Name == this.rcAngelic.Name)
						{
							Skill speciesCanUseSkill3 = myObject;
							speciesCanUseSkill3.SpeciesCanUseSkill = speciesCanUseSkill3.SpeciesCanUseSkill | Species.Angelic;
						}
						if (control.Name == this.rcDemonic.Name)
						{
							Skill skill3 = myObject;
							skill3.SpeciesCanUseSkill = skill3.SpeciesCanUseSkill | Species.Demonic;
						}
						if (control.Name == this.rcFun.Name)
						{
							Skill speciesCanUseSkill4 = myObject;
							speciesCanUseSkill4.SpeciesCanUseSkill = speciesCanUseSkill4.SpeciesCanUseSkill | Species.Fun;
						}
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
			this.comboBox_SkillGroup = new ComboBox();
			this.Label_SkillGroup = new Label();
			this.label_Name = new Label();
			this.FIELD_Name = new TextBox();
			this.listBox_StatMods = new ListBox();
			this.label_StatMods = new Label();
			this.button_AddMod = new Button();
			this.button_RemoveMod = new Button();
			this.label_Description = new Label();
			this.textBox_Description = new TextBox();
			this.label_Cost = new Label();
			this.textBox_Cost = new TextBox();
			this.button_Save = new Button();
			this.button_Cancel = new Button();
			this.comboBox_StatToMod = new ComboBox();
			this.textBox_StatModAmount = new TextBox();
			this.button_Edit = new Button();
			this.comboBox_Permission = new ComboBox();
			this.label1 = new Label();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
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
			this.groupBox1.Location = new Point(286, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(201, 142);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Species that can use this skillAbility";
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
			this.comboBox_SkillGroup.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_SkillGroup.FormattingEnabled = true;
			this.comboBox_SkillGroup.Location = new Point(286, 173);
			this.comboBox_SkillGroup.Name = "comboBox_SkillGroup";
			this.comboBox_SkillGroup.Size = new System.Drawing.Size(184, 21);
			this.comboBox_SkillGroup.TabIndex = 10;
			SkillEditor skillEditor = this;
			this.comboBox_SkillGroup.Leave += new EventHandler(skillEditor.Leave_field);
			this.Label_SkillGroup.AutoSize = true;
			this.Label_SkillGroup.Location = new Point(289, 157);
			this.Label_SkillGroup.Name = "Label_SkillGroup";
			this.Label_SkillGroup.Size = new System.Drawing.Size(61, 13);
			this.Label_SkillGroup.TabIndex = 9;
			this.Label_SkillGroup.Text = "Skill Group:";
			this.label_Name.AutoSize = true;
			this.label_Name.Location = new Point(4, 9);
			this.label_Name.Name = "label_Name";
			this.label_Name.Size = new System.Drawing.Size(38, 13);
			this.label_Name.TabIndex = 0;
			this.label_Name.Text = "Name:";
			this.FIELD_Name.Location = new Point(7, 25);
			this.FIELD_Name.Name = "FIELD_Name";
			this.FIELD_Name.Size = new System.Drawing.Size(254, 20);
			this.FIELD_Name.TabIndex = 1;
			SkillEditor skillEditor1 = this;
			this.FIELD_Name.Leave += new EventHandler(skillEditor1.Leave_field);
			this.listBox_StatMods.FormattingEnabled = true;
			this.listBox_StatMods.Location = new Point(7, 73);
			this.listBox_StatMods.Name = "listBox_StatMods";
			this.listBox_StatMods.Size = new System.Drawing.Size(253, 95);
			this.listBox_StatMods.TabIndex = 6;
			this.label_StatMods.AutoSize = true;
			this.label_StatMods.Location = new Point(4, 54);
			this.label_StatMods.Name = "label_StatMods";
			this.label_StatMods.Size = new System.Drawing.Size(58, 13);
			this.label_StatMods.TabIndex = 5;
			this.label_StatMods.Text = "Stat Mods:";
			this.button_AddMod.Location = new Point(7, 230);
			this.button_AddMod.Name = "button_AddMod";
			this.button_AddMod.Size = new System.Drawing.Size(102, 23);
			this.button_AddMod.TabIndex = 4;
			this.button_AddMod.Text = "Add Mod";
			this.button_AddMod.UseVisualStyleBackColor = true;
			this.button_AddMod.Click += new EventHandler(this.button_AddMod_Click);
			this.button_RemoveMod.Location = new Point(7, 174);
			this.button_RemoveMod.Name = "button_RemoveMod";
			this.button_RemoveMod.Size = new System.Drawing.Size(146, 23);
			this.button_RemoveMod.TabIndex = 7;
			this.button_RemoveMod.Text = "Remove Selected Mod";
			this.button_RemoveMod.UseVisualStyleBackColor = true;
			this.button_RemoveMod.Click += new EventHandler(this.button_RemoveMod_Click);
			this.label_Description.AutoSize = true;
			this.label_Description.Location = new Point(4, 268);
			this.label_Description.Name = "label_Description";
			this.label_Description.Size = new System.Drawing.Size(63, 13);
			this.label_Description.TabIndex = 14;
			this.label_Description.Text = "Description:";
			this.textBox_Description.Location = new Point(6, 284);
			this.textBox_Description.Multiline = true;
			this.textBox_Description.Name = "textBox_Description";
			this.textBox_Description.ScrollBars = ScrollBars.Both;
			this.textBox_Description.Size = new System.Drawing.Size(464, 134);
			this.textBox_Description.TabIndex = 15;
			SkillEditor skillEditor2 = this;
			this.textBox_Description.Leave += new EventHandler(skillEditor2.Leave_field);
			this.label_Cost.AutoSize = true;
			this.label_Cost.Location = new Point(4, 432);
			this.label_Cost.Name = "label_Cost";
			this.label_Cost.Size = new System.Drawing.Size(31, 13);
			this.label_Cost.TabIndex = 16;
			this.label_Cost.Text = "Cost:";
			this.textBox_Cost.Location = new Point(41, 429);
			this.textBox_Cost.Name = "textBox_Cost";
			this.textBox_Cost.Size = new System.Drawing.Size(68, 20);
			this.textBox_Cost.TabIndex = 17;
			SkillEditor skillEditor3 = this;
			this.textBox_Cost.Leave += new EventHandler(skillEditor3.Leave_field);
			this.textBox_Cost.KeyPress += new KeyPressEventHandler(this.textBox_Cost_KeyPress);
			this.button_Save.Location = new Point(260, 427);
			this.button_Save.Name = "button_Save";
			this.button_Save.Size = new System.Drawing.Size(102, 23);
			this.button_Save.TabIndex = 18;
			this.button_Save.Text = "Finished";
			this.button_Save.UseVisualStyleBackColor = true;
			SkillEditor skillEditor4 = this;
			this.button_Save.Click += new EventHandler(skillEditor4.button_Save_Click);
			this.button_Cancel.Location = new Point(368, 427);
			this.button_Cancel.Name = "button_Cancel";
			this.button_Cancel.Size = new System.Drawing.Size(102, 23);
			this.button_Cancel.TabIndex = 19;
			this.button_Cancel.Text = "Cancel";
			this.button_Cancel.UseVisualStyleBackColor = true;
			SkillEditor skillEditor5 = this;
			this.button_Cancel.Click += new EventHandler(skillEditor5.button_Cancel_Click);
			this.comboBox_StatToMod.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_StatToMod.FormattingEnabled = true;
			this.comboBox_StatToMod.Location = new Point(7, 203);
			this.comboBox_StatToMod.Name = "comboBox_StatToMod";
			this.comboBox_StatToMod.Size = new System.Drawing.Size(121, 21);
			this.comboBox_StatToMod.TabIndex = 2;
			this.textBox_StatModAmount.Location = new Point(134, 203);
			this.textBox_StatModAmount.Name = "textBox_StatModAmount";
			this.textBox_StatModAmount.Size = new System.Drawing.Size(126, 20);
			this.textBox_StatModAmount.TabIndex = 3;
			this.button_Edit.Location = new Point(286, 240);
			this.button_Edit.Name = "button_Edit";
			this.button_Edit.Size = new System.Drawing.Size(184, 23);
			this.button_Edit.TabIndex = 13;
			this.button_Edit.Text = "Edit";
			this.button_Edit.UseVisualStyleBackColor = true;
			this.button_Edit.Visible = false;
			SkillEditor skillEditor6 = this;
			this.button_Edit.Click += new EventHandler(skillEditor6.button_Edit_Click);
			this.comboBox_Permission.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_Permission.FormattingEnabled = true;
			this.comboBox_Permission.Location = new Point(286, 213);
			this.comboBox_Permission.Name = "comboBox_Permission";
			this.comboBox_Permission.Size = new System.Drawing.Size(184, 21);
			this.comboBox_Permission.TabIndex = 12;
			SkillEditor skillEditor7 = this;
			this.comboBox_Permission.Leave += new EventHandler(skillEditor7.Leave_field);
			this.label1.AutoSize = true;
			this.label1.Location = new Point(283, 197);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(105, 13);
			this.label1.TabIndex = 11;
			this.label1.Text = "Requires Permission:";
			base.AcceptButton = this.button_Save;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.button_Cancel;
			base.ClientSize = new System.Drawing.Size(499, 461);
			base.Collectors = new OCCBase[0];
			base.ControlBox = false;
			base.Controls.Add(this.comboBox_Permission);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.comboBox_StatToMod);
			base.Controls.Add(this.button_RemoveMod);
			base.Controls.Add(this.button_Cancel);
			base.Controls.Add(this.button_Edit);
			base.Controls.Add(this.button_Save);
			base.Controls.Add(this.button_AddMod);
			base.Controls.Add(this.listBox_StatMods);
			base.Controls.Add(this.textBox_Description);
			base.Controls.Add(this.textBox_Cost);
			base.Controls.Add(this.textBox_StatModAmount);
			base.Controls.Add(this.FIELD_Name);
			base.Controls.Add(this.comboBox_SkillGroup);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.Label_SkillGroup);
			base.Controls.Add(this.label_Description);
			base.Controls.Add(this.label_StatMods);
			base.Controls.Add(this.label_Name);
			base.Controls.Add(this.label_Cost);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Name = "SkillEditor";
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "SkillForm";
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
				try
				{
					ushort num = 0;
					if (((Control)sender).Name == this.textBox_Description.Name)
					{
						((Skill)base.MyObject).Description = this.textBox_Description.Text;
					}
					if (((Control)sender).Name == this.textBox_Cost.Name)
					{
						ushort.TryParse(this.textBox_Cost.Text, out num);
						((Skill)base.MyObject).Cost = num;
					}
					if (((Control)sender).Name == this.comboBox_Permission.Name)
					{
						((Skill)base.MyObject).RequiresPermission = (Permission)this.comboBox_Permission.SelectedItem;
					}
					if (((Control)sender).Name == this.comboBox_SkillGroup.Name)
					{
						((Skill)base.MyObject).Group = (SkillGroupFlag)Enum.Parse(typeof(SkillGroupFlag), this.comboBox_SkillGroup.SelectedItem.ToString());
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
			foreach (SkillGroupFlag list in GeneralOperations.EnumToList<SkillGroupFlag>())
			{
				this.comboBox_SkillGroup.Items.Add(GeneralOperations.GetDescription<SkillGroupFlag>(list));
			}
			foreach (StatsMod.Stats stat in GeneralOperations.EnumToList<StatsMod.Stats>())
			{
				this.comboBox_StatToMod.Items.Add(GeneralOperations.GetDescription<StatsMod.Stats>(stat));
			}
			OCCBase[] collectors = base.Collectors;
			for (int i = 0; i < (int)collectors.Length; i++)
			{
				OCCBase oCCBase = collectors[i];
				if (oCCBase.IsOfType(typeof(SkillPermission)))
				{
					this.comboBox_Permission.Items.AddRange(oCCBase.Objects);
				}
			}
			this.comboBox_SkillGroup.Sorted = true;
			this.comboBox_StatToMod.Sorted = true;
			this.comboBox_Permission.Sorted = true;
		}

		protected override void PopulateFields()
		{
			base.PopulateFields();
			Skill myObject = (Skill)base.MyObject;
			if (myObject.RequiresPermission != null)
			{
				foreach (SkillPermission item in this.comboBox_Permission.Items)
				{
					if (item.ID != myObject.RequiresPermission.ID)
					{
						continue;
					}
					this.comboBox_Permission.SelectedItem = item;
				}
			}
			if ((this.comboBox_Permission.SelectedItem != null ? false : myObject.RequiresPermission != null))
			{
				this.comboBox_Permission.Items.Add(myObject.RequiresPermission);
				this.comboBox_Permission.SelectedItem = myObject.RequiresPermission;
			}
			foreach (StatsMod modifiesStat in myObject.ModifiesStats)
			{
				this.listBox_StatMods.Items.Add(modifiesStat);
			}
			if ((int)myObject.Group != 0)
			{
				this.comboBox_SkillGroup.SelectedItem = myObject.Group.ToString();
			}
			if (!((Skill)base.MyObject).SpeciesCanUseSkill.Is<Species>(Species.All))
			{
				this.rcAngelic.Checked = ((Skill)base.MyObject).SpeciesCanUseSkill.Is<Species>(Species.Angelic);
				this.rcAquatic.Checked = ((Skill)base.MyObject).SpeciesCanUseSkill.Is<Species>(Species.Aquatic);
				this.rcDemonic.Checked = ((Skill)base.MyObject).SpeciesCanUseSkill.Is<Species>(Species.Demonic);
				this.rcFun.Checked = ((Skill)base.MyObject).SpeciesCanUseSkill.Is<Species>(Species.Fun);
				this.rcGenicA.Checked = ((Skill)base.MyObject).SpeciesCanUseSkill.Is<Species>(Species.Genics_A);
				this.rcGenicB.Checked = ((Skill)base.MyObject).SpeciesCanUseSkill.Is<Species>(Species.Genics_B);
				this.rcHuman.Checked = ((Skill)base.MyObject).SpeciesCanUseSkill.Is<Species>(Species.Human);
				this.rcMutant.Checked = ((Skill)base.MyObject).SpeciesCanUseSkill.Is<Species>(Species.Mutant);
				this.rcUndead.Checked = ((Skill)base.MyObject).SpeciesCanUseSkill.Is<Species>(Species.Undead);
			}
			else
			{
				this.rcAll.Checked = true;
			}
			this.textBox_Description.Text = myObject.Description;
			this.textBox_Cost.Text = myObject.Cost.ToString();
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

		private void textBox_Cost_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar > '9' || e.KeyChar < '0' ? e.KeyChar != '\b' : false))
			{
				e.Handled = true;
			}
		}
	}
}