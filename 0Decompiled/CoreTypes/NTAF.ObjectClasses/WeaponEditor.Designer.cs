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
	[EditorPlugIn("GUI Psy Editor", "0.0.0.0", true, new Type[] { typeof(Weapon) })]
	public class WeaponEditor : OCEditorBase
	{
		private IContainer components = null;

		private Button button_Cancel;

		private Button button_Edit;

		private Button button_Save;

		private TextBox FIELD_Name;

		private ComboBox comboBox_BaseWeaponType;

		private Label Label_BaseRace;

		private Label label_Name;

		private TextBox textBox_Description;

		private TextBox textBox_Cost;

		private Label label_Description;

		private Label label_Cost;

		private TextBox textBox_Range;

		private Label label2;

		private TextBox textBox_MvsP;

		private Label label3;

		private TextBox textBox_MvsA;

		private Label label4;

		private TextBox textBox_SIOR;

		private Label label5;

		private TextBox textBox_Special;

		private Label label6;

		private TextBox textBox_Shots;

		private Label label7;

		private TextBox textBox_SaveMod;

		private Label label8;

		private Label label1;

		private ComboBox comboBox_Permission;

		public WeaponEditor()
		{
			base.MyObject = (Weapon)Activator.CreateInstance(typeof(Weapon));
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
				if (control is ComboBox)
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
			this.FIELD_Name = new TextBox();
			this.comboBox_BaseWeaponType = new ComboBox();
			this.Label_BaseRace = new Label();
			this.label_Name = new Label();
			this.textBox_Description = new TextBox();
			this.textBox_Cost = new TextBox();
			this.label_Description = new Label();
			this.label_Cost = new Label();
			this.textBox_Range = new TextBox();
			this.label2 = new Label();
			this.textBox_MvsP = new TextBox();
			this.label3 = new Label();
			this.textBox_MvsA = new TextBox();
			this.label4 = new Label();
			this.textBox_SIOR = new TextBox();
			this.label5 = new Label();
			this.textBox_Special = new TextBox();
			this.label6 = new Label();
			this.textBox_Shots = new TextBox();
			this.label7 = new Label();
			this.textBox_SaveMod = new TextBox();
			this.label8 = new Label();
			this.label1 = new Label();
			this.comboBox_Permission = new ComboBox();
			base.SuspendLayout();
			this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button_Cancel.Location = new Point(370, 449);
			this.button_Cancel.Name = "button_Cancel";
			this.button_Cancel.Size = new System.Drawing.Size(102, 23);
			this.button_Cancel.TabIndex = 26;
			this.button_Cancel.Text = "Cancel";
			this.button_Cancel.UseVisualStyleBackColor = true;
			WeaponEditor weaponEditor = this;
			this.button_Cancel.Click += new EventHandler(weaponEditor.button_Cancel_Click);
			this.button_Edit.Location = new Point(262, 129);
			this.button_Edit.Name = "button_Edit";
			this.button_Edit.Size = new System.Drawing.Size(210, 23);
			this.button_Edit.TabIndex = 18;
			this.button_Edit.Text = "Edit";
			this.button_Edit.UseVisualStyleBackColor = true;
			this.button_Edit.Visible = false;
			WeaponEditor weaponEditor1 = this;
			this.button_Edit.Click += new EventHandler(weaponEditor1.button_Edit_Click);
			this.button_Save.Location = new Point(262, 449);
			this.button_Save.Name = "button_Save";
			this.button_Save.Size = new System.Drawing.Size(102, 23);
			this.button_Save.TabIndex = 25;
			this.button_Save.Text = "Finished";
			this.button_Save.UseVisualStyleBackColor = true;
			WeaponEditor weaponEditor2 = this;
			this.button_Save.Click += new EventHandler(weaponEditor2.button_Save_Click);
			this.FIELD_Name.Location = new Point(15, 25);
			this.FIELD_Name.Name = "FIELD_Name";
			this.FIELD_Name.Size = new System.Drawing.Size(210, 20);
			this.FIELD_Name.TabIndex = 1;
			WeaponEditor weaponEditor3 = this;
			this.FIELD_Name.Leave += new EventHandler(weaponEditor3.Leave_field);
			this.comboBox_BaseWeaponType.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_BaseWeaponType.FormattingEnabled = true;
			this.comboBox_BaseWeaponType.Location = new Point(15, 64);
			this.comboBox_BaseWeaponType.Name = "comboBox_BaseWeaponType";
			this.comboBox_BaseWeaponType.Size = new System.Drawing.Size(210, 21);
			this.comboBox_BaseWeaponType.TabIndex = 7;
			WeaponEditor weaponEditor4 = this;
			this.comboBox_BaseWeaponType.Leave += new EventHandler(weaponEditor4.Leave_field);
			this.Label_BaseRace.AutoSize = true;
			this.Label_BaseRace.Location = new Point(12, 48);
			this.Label_BaseRace.Name = "Label_BaseRace";
			this.Label_BaseRace.Size = new System.Drawing.Size(105, 13);
			this.Label_BaseRace.TabIndex = 6;
			this.Label_BaseRace.Text = "Base Weapon Type:";
			this.label_Name.AutoSize = true;
			this.label_Name.Location = new Point(12, 9);
			this.label_Name.Name = "label_Name";
			this.label_Name.Size = new System.Drawing.Size(38, 13);
			this.label_Name.TabIndex = 0;
			this.label_Name.Text = "Name:";
			this.textBox_Description.Location = new Point(15, 310);
			this.textBox_Description.Multiline = true;
			this.textBox_Description.Name = "textBox_Description";
			this.textBox_Description.ScrollBars = ScrollBars.Both;
			this.textBox_Description.Size = new System.Drawing.Size(457, 134);
			this.textBox_Description.TabIndex = 22;
			WeaponEditor weaponEditor5 = this;
			this.textBox_Description.Leave += new EventHandler(weaponEditor5.Leave_field);
			this.textBox_Cost.Location = new Point(50, 452);
			this.textBox_Cost.Name = "textBox_Cost";
			this.textBox_Cost.Size = new System.Drawing.Size(68, 20);
			this.textBox_Cost.TabIndex = 24;
			WeaponEditor weaponEditor6 = this;
			this.textBox_Cost.Leave += new EventHandler(weaponEditor6.Leave_field);
			this.label_Description.AutoSize = true;
			this.label_Description.Location = new Point(13, 294);
			this.label_Description.Name = "label_Description";
			this.label_Description.Size = new System.Drawing.Size(63, 13);
			this.label_Description.TabIndex = 21;
			this.label_Description.Text = "Description:";
			this.label_Cost.AutoSize = true;
			this.label_Cost.Location = new Point(13, 455);
			this.label_Cost.Name = "label_Cost";
			this.label_Cost.Size = new System.Drawing.Size(31, 13);
			this.label_Cost.TabIndex = 23;
			this.label_Cost.Text = "Cost:";
			this.textBox_Range.Location = new Point(231, 25);
			this.textBox_Range.Name = "textBox_Range";
			this.textBox_Range.Size = new System.Drawing.Size(117, 20);
			this.textBox_Range.TabIndex = 3;
			WeaponEditor weaponEditor7 = this;
			this.textBox_Range.Leave += new EventHandler(weaponEditor7.Leave_field);
			this.label2.AutoSize = true;
			this.label2.Location = new Point(228, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(42, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Range:";
			this.textBox_MvsP.Location = new Point(231, 64);
			this.textBox_MvsP.Name = "textBox_MvsP";
			this.textBox_MvsP.Size = new System.Drawing.Size(117, 20);
			this.textBox_MvsP.TabIndex = 9;
			WeaponEditor weaponEditor8 = this;
			this.textBox_MvsP.Leave += new EventHandler(weaponEditor8.Leave_field);
			this.label3.AutoSize = true;
			this.label3.Location = new Point(228, 48);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(37, 13);
			this.label3.TabIndex = 8;
			this.label3.Text = "MvsP:";
			this.textBox_MvsA.Location = new Point(231, 103);
			this.textBox_MvsA.Name = "textBox_MvsA";
			this.textBox_MvsA.Size = new System.Drawing.Size(117, 20);
			this.textBox_MvsA.TabIndex = 15;
			WeaponEditor weaponEditor9 = this;
			this.textBox_MvsA.Leave += new EventHandler(weaponEditor9.Leave_field);
			this.label4.AutoSize = true;
			this.label4.Location = new Point(228, 87);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(37, 13);
			this.label4.TabIndex = 14;
			this.label4.Text = "MvsA:";
			this.textBox_SIOR.Location = new Point(354, 25);
			this.textBox_SIOR.Name = "textBox_SIOR";
			this.textBox_SIOR.Size = new System.Drawing.Size(117, 20);
			this.textBox_SIOR.TabIndex = 5;
			WeaponEditor weaponEditor10 = this;
			this.textBox_SIOR.Leave += new EventHandler(weaponEditor10.Leave_field);
			this.label5.AutoSize = true;
			this.label5.Location = new Point(351, 9);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(36, 13);
			this.label5.TabIndex = 4;
			this.label5.Text = "SIOR:";
			this.textBox_Special.Location = new Point(15, 158);
			this.textBox_Special.Multiline = true;
			this.textBox_Special.Name = "textBox_Special";
			this.textBox_Special.ScrollBars = ScrollBars.Both;
			this.textBox_Special.Size = new System.Drawing.Size(457, 134);
			this.textBox_Special.TabIndex = 20;
			WeaponEditor weaponEditor11 = this;
			this.textBox_Special.Leave += new EventHandler(weaponEditor11.Leave_field);
			this.label6.AutoSize = true;
			this.label6.Location = new Point(13, 142);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(45, 13);
			this.label6.TabIndex = 19;
			this.label6.Text = "Special:";
			this.textBox_Shots.Location = new Point(354, 64);
			this.textBox_Shots.Name = "textBox_Shots";
			this.textBox_Shots.Size = new System.Drawing.Size(117, 20);
			this.textBox_Shots.TabIndex = 11;
			WeaponEditor weaponEditor12 = this;
			this.textBox_Shots.Leave += new EventHandler(weaponEditor12.Leave_field);
			this.label7.AutoSize = true;
			this.label7.Location = new Point(351, 48);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(37, 13);
			this.label7.TabIndex = 10;
			this.label7.Text = "Shots:";
			this.textBox_SaveMod.Location = new Point(354, 103);
			this.textBox_SaveMod.Name = "textBox_SaveMod";
			this.textBox_SaveMod.Size = new System.Drawing.Size(117, 20);
			this.textBox_SaveMod.TabIndex = 17;
			WeaponEditor weaponEditor13 = this;
			this.textBox_SaveMod.Leave += new EventHandler(weaponEditor13.Leave_field);
			this.label8.AutoSize = true;
			this.label8.Location = new Point(351, 87);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(75, 13);
			this.label8.TabIndex = 16;
			this.label8.Text = "Save Modifier:";
			this.label1.AutoSize = true;
			this.label1.Location = new Point(13, 87);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(105, 13);
			this.label1.TabIndex = 12;
			this.label1.Text = "Requires Permission:";
			this.comboBox_Permission.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_Permission.FormattingEnabled = true;
			this.comboBox_Permission.Location = new Point(16, 103);
			this.comboBox_Permission.Name = "comboBox_Permission";
			this.comboBox_Permission.Size = new System.Drawing.Size(210, 21);
			this.comboBox_Permission.TabIndex = 13;
			WeaponEditor weaponEditor14 = this;
			this.comboBox_Permission.Leave += new EventHandler(weaponEditor14.Leave_field);
			base.AcceptButton = this.button_Save;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.button_Cancel;
			base.ClientSize = new System.Drawing.Size(481, 481);
			base.Collectors = new OCCBase[0];
			base.ControlBox = false;
			base.Controls.Add(this.textBox_SaveMod);
			base.Controls.Add(this.label8);
			base.Controls.Add(this.textBox_Shots);
			base.Controls.Add(this.label7);
			base.Controls.Add(this.textBox_Special);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.textBox_SIOR);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.textBox_MvsA);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.textBox_MvsP);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.textBox_Range);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.textBox_Description);
			base.Controls.Add(this.textBox_Cost);
			base.Controls.Add(this.label_Description);
			base.Controls.Add(this.label_Cost);
			base.Controls.Add(this.button_Cancel);
			base.Controls.Add(this.button_Edit);
			base.Controls.Add(this.button_Save);
			base.Controls.Add(this.FIELD_Name);
			base.Controls.Add(this.comboBox_Permission);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.comboBox_BaseWeaponType);
			base.Controls.Add(this.Label_BaseRace);
			base.Controls.Add(this.label_Name);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Name = "WeaponEditor";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "WeaponForm";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		protected override void Leave_field(object sender, EventArgs e)
		{
			base.Leave_field(sender, e);
			if (sender is Control)
			{
				Control control = sender as Control;
				try
				{
					Weapon myObject = base.MyObject as Weapon;
					if (control.Name == this.comboBox_Permission.Name)
					{
						myObject.RequiresPermission = (WeaponPermission)this.comboBox_Permission.SelectedItem;
					}
					if (control.Name == this.comboBox_BaseWeaponType.Name)
					{
						myObject.BaseType = (WeaponBaseType)Enum.Parse(typeof(WeaponBaseType), this.comboBox_BaseWeaponType.SelectedItem.ToString());
					}
					if (control.Name == this.textBox_Cost.Name)
					{
						ushort num = 0;
						ushort.TryParse(this.textBox_Cost.Text, out num);
						myObject.Cost = num;
					}
					if (control.Name == this.textBox_Description.Name)
					{
						myObject.Description = this.textBox_Description.Text;
					}
					if (control.Name == this.textBox_Special.Name)
					{
						myObject.Special = this.textBox_Special.Text;
					}
					if (control.Name == this.textBox_Range.Name)
					{
						myObject.Range = this.textBox_Range.Text;
					}
					if (control.Name == this.textBox_MvsP.Name)
					{
						sbyte num1 = 0;
						sbyte.TryParse(this.textBox_MvsP.Text, out num1);
						myObject.MvsP = num1;
					}
					if (control.Name == this.textBox_MvsA.Name)
					{
						sbyte num2 = 0;
						sbyte.TryParse(this.textBox_MvsA.Text, out num2);
						myObject.MvsA = num2;
					}
					if (control.Name == this.textBox_SIOR.Name)
					{
						sbyte num3 = 0;
						sbyte.TryParse(this.textBox_SIOR.Text, out num3);
						myObject.SIOR = num3;
					}
					if (control.Name == this.textBox_Shots.Name)
					{
						byte num4 = 0;
						byte.TryParse(this.textBox_Shots.Text, out num4);
						myObject.Shots = num4;
					}
					if (control.Name == this.textBox_SaveMod.Name)
					{
						sbyte num5 = 0;
						sbyte.TryParse(this.textBox_SaveMod.Text, out num5);
						myObject.SvMod = num5;
					}
				}
				catch (Exception exception)
				{
					throw;
				}
			}
		}

		protected override void PopulateComboboxes()
		{
			foreach (WeaponBaseType list in GeneralOperations.EnumToList<WeaponBaseType>())
			{
				this.comboBox_BaseWeaponType.Items.Add(GeneralOperations.GetDescription<WeaponBaseType>(list));
			}
			OCCBase[] collectors = base.Collectors;
			for (int i = 0; i < (int)collectors.Length; i++)
			{
				OCCBase oCCBase = collectors[i];
				if (oCCBase.IsOfType(typeof(WeaponPermission)))
				{
					this.comboBox_Permission.Items.AddRange(oCCBase.Objects);
				}
			}
			this.comboBox_BaseWeaponType.Sorted = true;
			this.comboBox_Permission.Sorted = true;
		}

		protected override void PopulateFields()
		{
			base.PopulateFields();
			try
			{
				Weapon myObject = base.MyObject as Weapon;
				this.comboBox_BaseWeaponType.SelectedItem = myObject.BaseType.ToString();
				this.textBox_Range.Text = myObject.Range;
				this.textBox_MvsP.Text = myObject.MvsP.ToString();
				this.textBox_MvsA.Text = myObject.MvsA.ToString();
				this.textBox_SIOR.Text = myObject.SIOR.ToString();
				this.textBox_Shots.Text = myObject.Shots.ToString();
				this.textBox_SaveMod.Text = myObject.SvMod.ToString();
				this.textBox_Special.Text = myObject.Special;
				this.textBox_Description.Text = myObject.Description;
				if (myObject.RequiresPermission != null)
				{
					foreach (WeaponPermission item in this.comboBox_Permission.Items)
					{
						if (item.ID != myObject.RequiresPermission.ID)
						{
							continue;
						}
						this.comboBox_Permission.SelectedItem = item;
					}
				}
				this.textBox_Cost.Text = myObject.Cost.ToString();
			}
			catch (Exception exception)
			{
				throw;
			}
		}

		public override EditorExitCode RunEditor(EditorMode mode)
		{
			this.InitializeComponent();
			return base.RunEditor(mode);
		}
	}
}