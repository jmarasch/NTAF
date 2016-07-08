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
	[EditorPlugIn("GUI Skill Editor", "0.0.0.0", true, new Type[] { typeof(Item) })]
	public class ItemEditor : OCEditorBase
	{
		private IContainer components = null;

		private ComboBox comboBox_StatToMod;

		private Button button_RemoveMod;

		private Button button_Cancel;

		private Button button_Save;

		private Button button_AddMod;

		private ListBox listBox_StatMods;

		private TextBox textBox_Description;

		private TextBox textBox_Cost;

		private TextBox textBox_StatModAmount;

		private TextBox FIELD_Name;

		private Label label_Description;

		private Label label_StatMods;

		private Label label_Name;

		private Label label_Cost;

		private Label Label_ID;

		private Button button_Edit;

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

		private ComboBox comboBox_ForGroup;

		private Label label1;

		public ItemEditor()
		{
			base.MyObject = (Item)Activator.CreateInstance(typeof(Item));
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
			List<StatsMod> statsMods = new List<StatsMod>();
			foreach (StatsMod item in this.listBox_StatMods.Items)
			{
				statsMods.Add(item);
			}
			((Item)base.MyObject).ModifiesStats = statsMods.ToArray();
			base.button_Save_Click(sender, e);
		}

		private void CheckForChecks()
		{
			if (!base.FormLoading())
			{
				Item myObject = (Item)base.MyObject;
				myObject.SpeciesCanEquip = (Species)0;
				foreach (Control control in this.groupBox1.Controls)
				{
					if (((CheckBox)control).Checked)
					{
						if (control.Name == this.rcAll.Name)
						{
							myObject.SpeciesCanEquip = Species.All;
						}
						if (control.Name == this.rcHuman.Name)
						{
							Item speciesCanEquip = myObject;
							speciesCanEquip.SpeciesCanEquip = speciesCanEquip.SpeciesCanEquip | Species.Human;
						}
						if (control.Name == this.rcMutant.Name)
						{
							Item item = myObject;
							item.SpeciesCanEquip = item.SpeciesCanEquip | Species.Mutant;
						}
						if (control.Name == this.rcUndead.Name)
						{
							Item speciesCanEquip1 = myObject;
							speciesCanEquip1.SpeciesCanEquip = speciesCanEquip1.SpeciesCanEquip | Species.Undead;
						}
						if (control.Name == this.rcAquatic.Name)
						{
							Item item1 = myObject;
							item1.SpeciesCanEquip = item1.SpeciesCanEquip | Species.Aquatic;
						}
						if (control.Name == this.rcGenicA.Name)
						{
							Item speciesCanEquip2 = myObject;
							speciesCanEquip2.SpeciesCanEquip = speciesCanEquip2.SpeciesCanEquip | Species.Genics_A;
						}
						if (control.Name == this.rcGenicB.Name)
						{
							Item item2 = myObject;
							item2.SpeciesCanEquip = item2.SpeciesCanEquip | Species.Genics_B;
						}
						if (control.Name == this.rcAngelic.Name)
						{
							Item speciesCanEquip3 = myObject;
							speciesCanEquip3.SpeciesCanEquip = speciesCanEquip3.SpeciesCanEquip | Species.Angelic;
						}
						if (control.Name == this.rcDemonic.Name)
						{
							Item item3 = myObject;
							item3.SpeciesCanEquip = item3.SpeciesCanEquip | Species.Demonic;
						}
						if (control.Name == this.rcFun.Name)
						{
							Item speciesCanEquip4 = myObject;
							speciesCanEquip4.SpeciesCanEquip = speciesCanEquip4.SpeciesCanEquip | Species.Fun;
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
			this.comboBox_StatToMod = new ComboBox();
			this.button_RemoveMod = new Button();
			this.button_Cancel = new Button();
			this.button_Save = new Button();
			this.button_AddMod = new Button();
			this.listBox_StatMods = new ListBox();
			this.textBox_Description = new TextBox();
			this.textBox_Cost = new TextBox();
			this.textBox_StatModAmount = new TextBox();
			this.FIELD_Name = new TextBox();
			this.label_Description = new Label();
			this.label_StatMods = new Label();
			this.label_Name = new Label();
			this.label_Cost = new Label();
			this.Label_ID = new Label();
			this.button_Edit = new Button();
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
			this.comboBox_ForGroup = new ComboBox();
			this.label1 = new Label();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.comboBox_StatToMod.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_StatToMod.FormattingEnabled = true;
			this.comboBox_StatToMod.Location = new Point(15, 174);
			this.comboBox_StatToMod.Name = "comboBox_StatToMod";
			this.comboBox_StatToMod.Size = new System.Drawing.Size(129, 21);
			this.comboBox_StatToMod.Sorted = true;
			this.comboBox_StatToMod.TabIndex = 3;
			ItemEditor itemEditor = this;
			this.comboBox_StatToMod.Enter += new EventHandler(itemEditor.Enter_field);
			this.button_RemoveMod.Location = new Point(150, 201);
			this.button_RemoveMod.Name = "button_RemoveMod";
			this.button_RemoveMod.Size = new System.Drawing.Size(129, 23);
			this.button_RemoveMod.TabIndex = 6;
			this.button_RemoveMod.Text = "&Remove Selected Mod";
			this.button_RemoveMod.UseVisualStyleBackColor = true;
			this.button_RemoveMod.Click += new EventHandler(this.button_RemoveMod_Click);
			this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button_Cancel.Location = new Point(385, 386);
			this.button_Cancel.Name = "button_Cancel";
			this.button_Cancel.Size = new System.Drawing.Size(102, 23);
			this.button_Cancel.TabIndex = 15;
			this.button_Cancel.Text = "Cancel";
			this.button_Cancel.UseVisualStyleBackColor = true;
			ItemEditor itemEditor1 = this;
			this.button_Cancel.Click += new EventHandler(itemEditor1.button_Cancel_Click);
			this.button_Save.Location = new Point(277, 386);
			this.button_Save.Name = "button_Save";
			this.button_Save.Size = new System.Drawing.Size(102, 23);
			this.button_Save.TabIndex = 14;
			this.button_Save.Text = "&Finished";
			this.button_Save.UseVisualStyleBackColor = true;
			ItemEditor itemEditor2 = this;
			this.button_Save.Click += new EventHandler(itemEditor2.button_Save_Click);
			this.button_AddMod.Location = new Point(15, 201);
			this.button_AddMod.Name = "button_AddMod";
			this.button_AddMod.Size = new System.Drawing.Size(129, 23);
			this.button_AddMod.TabIndex = 5;
			this.button_AddMod.Text = "Add &Mod";
			this.button_AddMod.UseVisualStyleBackColor = true;
			this.button_AddMod.Click += new EventHandler(this.button_AddMod_Click);
			this.listBox_StatMods.FormattingEnabled = true;
			this.listBox_StatMods.Location = new Point(15, 73);
			this.listBox_StatMods.Name = "listBox_StatMods";
			this.listBox_StatMods.Size = new System.Drawing.Size(264, 95);
			this.listBox_StatMods.TabIndex = 29;
			this.textBox_Description.Location = new Point(14, 243);
			this.textBox_Description.Multiline = true;
			this.textBox_Description.Name = "textBox_Description";
			this.textBox_Description.ScrollBars = ScrollBars.Both;
			this.textBox_Description.Size = new System.Drawing.Size(473, 134);
			this.textBox_Description.TabIndex = 10;
			ItemEditor itemEditor3 = this;
			this.textBox_Description.Leave += new EventHandler(itemEditor3.Leave_field);
			ItemEditor itemEditor4 = this;
			this.textBox_Description.Enter += new EventHandler(itemEditor4.Enter_field);
			this.textBox_Cost.Location = new Point(49, 388);
			this.textBox_Cost.Name = "textBox_Cost";
			this.textBox_Cost.Size = new System.Drawing.Size(68, 20);
			this.textBox_Cost.TabIndex = 12;
			ItemEditor itemEditor5 = this;
			this.textBox_Cost.Leave += new EventHandler(itemEditor5.Leave_field);
			this.textBox_Cost.KeyPress += new KeyPressEventHandler(this.NumOnly_KeyPress);
			ItemEditor itemEditor6 = this;
			this.textBox_Cost.Enter += new EventHandler(itemEditor6.Enter_field);
			this.textBox_StatModAmount.Location = new Point(150, 175);
			this.textBox_StatModAmount.Name = "textBox_StatModAmount";
			this.textBox_StatModAmount.Size = new System.Drawing.Size(129, 20);
			this.textBox_StatModAmount.TabIndex = 4;
			this.textBox_StatModAmount.KeyPress += new KeyPressEventHandler(this.NumOnly_KeyPress);
			ItemEditor itemEditor7 = this;
			this.textBox_StatModAmount.Enter += new EventHandler(itemEditor7.Enter_field);
			this.FIELD_Name.Location = new Point(15, 25);
			this.FIELD_Name.Name = "FIELD_Name";
			this.FIELD_Name.Size = new System.Drawing.Size(264, 20);
			this.FIELD_Name.TabIndex = 1;
			ItemEditor itemEditor8 = this;
			this.FIELD_Name.Leave += new EventHandler(itemEditor8.Leave_field);
			ItemEditor itemEditor9 = this;
			this.FIELD_Name.Enter += new EventHandler(itemEditor9.Enter_field);
			this.label_Description.AutoSize = true;
			this.label_Description.Location = new Point(12, 227);
			this.label_Description.Name = "label_Description";
			this.label_Description.Size = new System.Drawing.Size(63, 13);
			this.label_Description.TabIndex = 9;
			this.label_Description.Text = "&Description:";
			this.label_StatMods.AutoSize = true;
			this.label_StatMods.Location = new Point(12, 54);
			this.label_StatMods.Name = "label_StatMods";
			this.label_StatMods.Size = new System.Drawing.Size(58, 13);
			this.label_StatMods.TabIndex = 23;
			this.label_StatMods.Text = "Stat Mods:";
			this.label_Name.AutoSize = true;
			this.label_Name.Location = new Point(12, 9);
			this.label_Name.Name = "label_Name";
			this.label_Name.Size = new System.Drawing.Size(38, 13);
			this.label_Name.TabIndex = 0;
			this.label_Name.Text = "&Name:";
			this.label_Cost.AutoSize = true;
			this.label_Cost.Location = new Point(12, 391);
			this.label_Cost.Name = "label_Cost";
			this.label_Cost.Size = new System.Drawing.Size(31, 13);
			this.label_Cost.TabIndex = 11;
			this.label_Cost.Text = "&Cost:";
			this.Label_ID.AutoSize = true;
			this.Label_ID.Location = new Point(12, 9);
			this.Label_ID.Name = "Label_ID";
			this.Label_ID.Size = new System.Drawing.Size(21, 13);
			this.Label_ID.TabIndex = 27;
			this.Label_ID.Text = "ID:";
			this.Label_ID.Visible = false;
			this.button_Edit.Location = new Point(169, 386);
			this.button_Edit.Name = "button_Edit";
			this.button_Edit.Size = new System.Drawing.Size(102, 23);
			this.button_Edit.TabIndex = 13;
			this.button_Edit.Text = "&Edit";
			this.button_Edit.UseVisualStyleBackColor = true;
			this.button_Edit.Visible = false;
			ItemEditor itemEditor10 = this;
			this.button_Edit.Click += new EventHandler(itemEditor10.button_Edit_Click);
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
			this.groupBox1.Location = new Point(290, 25);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(197, 142);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Species that can use this Permission";
			this.rcFun.AutoSize = true;
			this.rcFun.Location = new Point(81, 111);
			this.rcFun.Name = "rcFun";
			this.rcFun.Size = new System.Drawing.Size(44, 17);
			this.rcFun.TabIndex = 9;
			this.rcFun.Text = "F&un";
			this.rcFun.UseVisualStyleBackColor = true;
			this.rcFun.CheckedChanged += new EventHandler(this.rcAll_CheckedChanged);
			this.rcAquatic.AutoSize = true;
			this.rcAquatic.Location = new Point(6, 111);
			this.rcAquatic.Name = "rcAquatic";
			this.rcAquatic.Size = new System.Drawing.Size(62, 17);
			this.rcAquatic.TabIndex = 4;
			this.rcAquatic.Text = "&Aquatic";
			this.rcAquatic.UseVisualStyleBackColor = true;
			this.rcAquatic.CheckedChanged += new EventHandler(this.rcAll_CheckedChanged);
			this.rcDemonic.AutoSize = true;
			this.rcDemonic.Location = new Point(81, 88);
			this.rcDemonic.Name = "rcDemonic";
			this.rcDemonic.Size = new System.Drawing.Size(68, 17);
			this.rcDemonic.TabIndex = 8;
			this.rcDemonic.Text = "Dem&onic";
			this.rcDemonic.UseVisualStyleBackColor = true;
			this.rcDemonic.CheckedChanged += new EventHandler(this.rcAll_CheckedChanged);
			this.rcUndead.AutoSize = true;
			this.rcUndead.Location = new Point(6, 88);
			this.rcUndead.Name = "rcUndead";
			this.rcUndead.Size = new System.Drawing.Size(64, 17);
			this.rcUndead.TabIndex = 3;
			this.rcUndead.Text = "&Undead";
			this.rcUndead.UseVisualStyleBackColor = true;
			this.rcUndead.CheckedChanged += new EventHandler(this.rcAll_CheckedChanged);
			this.rcAngelic.AutoSize = true;
			this.rcAngelic.Location = new Point(81, 65);
			this.rcAngelic.Name = "rcAngelic";
			this.rcAngelic.Size = new System.Drawing.Size(61, 17);
			this.rcAngelic.TabIndex = 7;
			this.rcAngelic.Text = "Angel&ic";
			this.rcAngelic.UseVisualStyleBackColor = true;
			this.rcAngelic.CheckedChanged += new EventHandler(this.rcAll_CheckedChanged);
			this.rcMutant.AutoSize = true;
			this.rcMutant.Location = new Point(6, 65);
			this.rcMutant.Name = "rcMutant";
			this.rcMutant.Size = new System.Drawing.Size(59, 17);
			this.rcMutant.TabIndex = 2;
			this.rcMutant.Text = "&Mutant";
			this.rcMutant.UseVisualStyleBackColor = true;
			this.rcMutant.CheckedChanged += new EventHandler(this.rcAll_CheckedChanged);
			this.rcGenicB.AutoSize = true;
			this.rcGenicB.Location = new Point(81, 42);
			this.rcGenicB.Name = "rcGenicB";
			this.rcGenicB.Size = new System.Drawing.Size(91, 17);
			this.rcGenicB.TabIndex = 6;
			this.rcGenicB.Text = "Genic &Type B";
			this.rcGenicB.UseVisualStyleBackColor = true;
			this.rcGenicB.CheckedChanged += new EventHandler(this.rcAll_CheckedChanged);
			this.rcHuman.AutoSize = true;
			this.rcHuman.Location = new Point(6, 42);
			this.rcHuman.Name = "rcHuman";
			this.rcHuman.Size = new System.Drawing.Size(60, 17);
			this.rcHuman.TabIndex = 1;
			this.rcHuman.Text = "&Human";
			this.rcHuman.UseVisualStyleBackColor = true;
			this.rcHuman.CheckedChanged += new EventHandler(this.rcAll_CheckedChanged);
			this.rcGenicA.AutoSize = true;
			this.rcGenicA.Location = new Point(81, 19);
			this.rcGenicA.Name = "rcGenicA";
			this.rcGenicA.Size = new System.Drawing.Size(91, 17);
			this.rcGenicA.TabIndex = 5;
			this.rcGenicA.Text = "&Genic Type A";
			this.rcGenicA.UseVisualStyleBackColor = true;
			this.rcGenicA.CheckedChanged += new EventHandler(this.rcAll_CheckedChanged);
			this.rcAll.AutoSize = true;
			this.rcAll.Location = new Point(6, 19);
			this.rcAll.Name = "rcAll";
			this.rcAll.Size = new System.Drawing.Size(37, 17);
			this.rcAll.TabIndex = 0;
			this.rcAll.Text = "A&ll";
			this.rcAll.UseVisualStyleBackColor = true;
			this.rcAll.CheckedChanged += new EventHandler(this.rcAll_CheckedChanged);
			this.comboBox_ForGroup.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_ForGroup.FormattingEnabled = true;
			this.comboBox_ForGroup.Location = new Point(290, 201);
			this.comboBox_ForGroup.Name = "comboBox_ForGroup";
			this.comboBox_ForGroup.Size = new System.Drawing.Size(197, 21);
			this.comboBox_ForGroup.TabIndex = 8;
			ItemEditor itemEditor11 = this;
			this.comboBox_ForGroup.Leave += new EventHandler(itemEditor11.Leave_field);
			ItemEditor itemEditor12 = this;
			this.comboBox_ForGroup.Enter += new EventHandler(itemEditor12.Enter_field);
			this.label1.AutoSize = true;
			this.label1.Location = new Point(287, 185);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(95, 13);
			this.label1.TabIndex = 7;
			this.label1.Text = "For &Specific Race:";
			base.AcceptButton = this.button_Save;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.button_Cancel;
			base.ClientSize = new System.Drawing.Size(503, 419);
			base.Collectors = new OCCBase[0];
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.comboBox_ForGroup);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.button_Edit);
			base.Controls.Add(this.comboBox_StatToMod);
			base.Controls.Add(this.button_RemoveMod);
			base.Controls.Add(this.button_Cancel);
			base.Controls.Add(this.button_Save);
			base.Controls.Add(this.button_AddMod);
			base.Controls.Add(this.listBox_StatMods);
			base.Controls.Add(this.textBox_Description);
			base.Controls.Add(this.textBox_Cost);
			base.Controls.Add(this.textBox_StatModAmount);
			base.Controls.Add(this.FIELD_Name);
			base.Controls.Add(this.label_Description);
			base.Controls.Add(this.label_StatMods);
			base.Controls.Add(this.label_Name);
			base.Controls.Add(this.label_Cost);
			base.Controls.Add(this.Label_ID);
			base.Name = "ItemEditor";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "ItemForm";
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
						((Item)base.MyObject).Description = this.textBox_Description.Text;
					}
					if (((Control)sender).Name == this.textBox_Cost.Name)
					{
						ushort.TryParse(this.textBox_Cost.Text, out num);
						((Item)base.MyObject).Cost = num;
					}
					if (((Control)sender).Name == this.comboBox_ForGroup.Name)
					{
						((Item)base.MyObject).RaceCanEquip = (Race)this.comboBox_ForGroup.SelectedItem;
					}
				}
				catch (Exception exception)
				{
					MessageBox.Show(exception.Message);
				}
			}
		}

		private void NumOnly_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (((e.KeyChar > '9' || e.KeyChar < '0') && e.KeyChar != '\b' ? e.KeyChar != '-' : false))
			{
				e.Handled = true;
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
			foreach (StatsMod.Stats list in GeneralOperations.EnumToList<StatsMod.Stats>())
			{
				this.comboBox_StatToMod.Items.Add(GeneralOperations.GetDescription<StatsMod.Stats>(list));
			}
			this.comboBox_ForGroup.Sorted = true;
		}

		protected override void PopulateFields()
		{
			base.PopulateFields();
			Item myObject = (Item)base.MyObject;
			if (myObject.RaceCanEquip != null)
			{
				foreach (Race item in this.comboBox_ForGroup.Items)
				{
					if (item.ID != myObject.RaceCanEquip.ID)
					{
						continue;
					}
					this.comboBox_ForGroup.SelectedItem = item;
				}
			}
			this.textBox_Description.Text = myObject.Description;
			this.textBox_Cost.Text = myObject.Cost.ToString();
			StatsMod[] modifiesStats = myObject.ModifiesStats;
			for (int i = 0; i < (int)modifiesStats.Length; i++)
			{
				StatsMod statsMod = modifiesStats[i];
				this.listBox_StatMods.Items.Add(statsMod);
			}
			if (!((Item)base.MyObject).SpeciesCanEquip.Is<Species>(Species.All))
			{
				this.rcAngelic.Checked = ((Item)base.MyObject).SpeciesCanEquip.Is<Species>(Species.Angelic);
				this.rcAquatic.Checked = ((Item)base.MyObject).SpeciesCanEquip.Is<Species>(Species.Aquatic);
				this.rcDemonic.Checked = ((Item)base.MyObject).SpeciesCanEquip.Is<Species>(Species.Demonic);
				this.rcFun.Checked = ((Item)base.MyObject).SpeciesCanEquip.Is<Species>(Species.Fun);
				this.rcGenicA.Checked = ((Item)base.MyObject).SpeciesCanEquip.Is<Species>(Species.Genics_A);
				this.rcGenicB.Checked = ((Item)base.MyObject).SpeciesCanEquip.Is<Species>(Species.Genics_B);
				this.rcHuman.Checked = ((Item)base.MyObject).SpeciesCanEquip.Is<Species>(Species.Human);
				this.rcMutant.Checked = ((Item)base.MyObject).SpeciesCanEquip.Is<Species>(Species.Mutant);
				this.rcUndead.Checked = ((Item)base.MyObject).SpeciesCanEquip.Is<Species>(Species.Undead);
			}
			else
			{
				this.rcAll.Checked = true;
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