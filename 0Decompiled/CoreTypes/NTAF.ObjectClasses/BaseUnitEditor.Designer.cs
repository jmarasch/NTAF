using NTAF.Core;
using NTAF.PlugInFramework;
using NTAF.UniverseBuilder.WinGui.MessageBoxes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace NTAF.ObjectClasses
{
	[EditorPlugIn("GUI BaseUnit Editor", "0.0.0.0", true, new Type[] { typeof(BaseUnit) })]
	public class BaseUnitEditor : OCEditorBase
	{
		private Race _PreviouslySelectedRace = null;

		private char[] trimChars = new char[] { '\'', ' ', '\"' };

		private IContainer components = null;

		internal ComboBox cbxRace;

		internal ComboBox cbxArchatype;

		internal Label lblStartingBaseStats;

		internal Label lblHdrEXP;

		internal Label lblCostHead;

		internal Label lblHF;

		internal Label lblWP;

		internal Label lblAG;

		internal Label lblT;

		internal Label lblMT;

		internal Label lblRW;

		internal Label lblPP;

		internal Label lblHH;

		internal Label lblAP;

		internal Label lblHP;

		internal Label Label19;

		internal Label Label18;

		internal Label lblM;

		internal Label lblEXP;

		internal TextBox txtSpec;

		internal TextBox txtDes;

		internal Label lblRace;

		internal Label lblUnitType;

		internal Label lblName;

		internal Button btnRmvPsyRest;

		internal Button btnRmvSkillRest;

		internal Button btnRmvWpnRest;

		internal Button btnAddPsyRest;

		internal Button btnAddSkillRest;

		internal Button btnAddWpnRest;

		internal Label lblPPerm;

		internal Label lblSPerm;

		internal Label lblWPerm;

		internal Label lblWeapons;

		internal Label lblSkills;

		internal Label lblPsys;

		internal Button btnAddWeapon;

		internal Button btnAddSkills;

		internal Button btnAddPsy;

		internal Button btnRemWeapon;

		internal Button btnRemSkill;

		internal Button btnRemPsy;

		internal Label lblItems;

		internal ListBox lstItems;

		internal Button btnAddItem;

		internal Button btnRemItem;

		private Button ButtonCancel;

		private Button ButtonEdit;

		private Button ButtonSave;

		private Label lblID;

		internal TextBox FIELD_Name;

		internal TextBox txtCostMod;

		internal Label lblConstMod;

		private ListBox lstWepRes;

		private ListBox lstPsyRes;

		private ListBox lstSkillRes;

		private ListBox lstWeapons;

		private ListBox lstSkils;

		private ListBox lstPsys;

		internal Label MMod;

		internal Label HPMod;

		internal Label APMod;

		internal Label HHMod;

		internal Label PPMod;

		internal Label RWMod;

		internal Label MTMod;

		internal Label TMod;

		internal Label AGMod;

		internal Label WPMod;

		internal Label HFMod;

		internal Label MTot;

		internal Label HPTot;

		internal Label APTot;

		internal Label HHTot;

		internal Label PPTot;

		internal Label RWTot;

		internal Label MTTot;

		internal Label TTot;

		internal Label AGTot;

		internal Label WPTot;

		internal Label HFTot;

		internal Label lblCost;

		private NumericUpDown nrM;

		private NumericUpDown nrHP;

		private NumericUpDown nrAP;

		private NumericUpDown nrHH;

		private NumericUpDown nrPP;

		private NumericUpDown nrRW;

		private NumericUpDown nrMGT;

		private NumericUpDown nrT;

		private NumericUpDown nrAGIL;

		private NumericUpDown nrWP;

		private NumericUpDown nrHF;

		public BaseUnitEditor()
		{
			base.MyObject = (BaseUnit)Activator.CreateInstance(typeof(BaseUnit));
		}

		private void btnAddItem_Click(object sender, EventArgs e)
		{
			try
			{
				List<Item> items = new List<Item>();
				OCCBase[] collectors = base.Collectors;
				for (int i = 0; i < (int)collectors.Length; i++)
				{
					OCCBase oCCBase = collectors[i];
					if (oCCBase.IsOfType(typeof(Item)))
					{
						items.AddRange(Array.ConvertAll<object, Item>(oCCBase.Objects, (object Obj) => (Obj is Item ? (Item)Obj : null)));
					}
				}
				SelectorForm selectorForm = new SelectorForm("Add", "Cancel", "Select an Item to add...", "Add Item", ((BaseUnit)base.MyObject).getAvalablItems(items.ToArray()));
				if (selectorForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					((BaseUnit)base.MyObject).AddItem((Item)selectorForm.SelectedObject);
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void btnAddPsy_Click(object sender, EventArgs e)
		{
			try
			{
				List<Psy> psies = new List<Psy>();
				OCCBase[] collectors = base.Collectors;
				for (int i = 0; i < (int)collectors.Length; i++)
				{
					OCCBase oCCBase = collectors[i];
					if (oCCBase.IsOfType(typeof(Psy)))
					{
						psies.AddRange(oCCBase.Objects as Psy[]);
					}
				}
				SelectorForm selectorForm = new SelectorForm("Add", "Cancel", "Select a Psy to add...", "Add Psy", ((BaseUnit)base.MyObject).getAvalablPsys(psies.ToArray()));
				if (selectorForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					((BaseUnit)base.MyObject).AddPsy((Psy)selectorForm.SelectedObject);
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void btnAddPsyRest_Click(object sender, EventArgs e)
		{
			try
			{
				List<PsyPermission> psyPermissions = new List<PsyPermission>();
				OCCBase[] collectors = base.Collectors;
				for (int i = 0; i < (int)collectors.Length; i++)
				{
					OCCBase oCCBase = collectors[i];
					if (oCCBase.IsOfType(typeof(PsyPermission)))
					{
						psyPermissions.AddRange(Array.ConvertAll<object, PsyPermission>(oCCBase.Objects, (object Obj) => (Obj is PsyPermission ? (PsyPermission)Obj : null)));
					}
					psyPermissions.AddRange(oCCBase.Objects as PsyPermission[]);
				}
				SelectorForm selectorForm = new SelectorForm("Add", "Cancel", "Select a Psy Permission to add...", "Add Psy Permission", ((BaseUnit)base.MyObject).getAllowedPermissions<PsyPermission>(psyPermissions.ToArray()));
				if (selectorForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					((BaseUnit)base.MyObject).AddPsyPermission((PsyPermission)selectorForm.SelectedObject);
				}
			}
			catch (RaceException raceException)
			{
			}
			catch (Exception exception)
			{
			}
		}

		private void btnAddSkill_Click(object sender, EventArgs e)
		{
			try
			{
				List<Skill> skills = new List<Skill>();
				OCCBase[] collectors = base.Collectors;
				for (int i = 0; i < (int)collectors.Length; i++)
				{
					OCCBase oCCBase = collectors[i];
					if (oCCBase.IsOfType(typeof(Skill)))
					{
						skills.AddRange(oCCBase.Objects as Skill[]);
					}
				}
				SelectorForm selectorForm = new SelectorForm("Add", "Cancel", "Select a Skill to add...", "Add Skill", ((BaseUnit)base.MyObject).getAvalableSkills(skills.ToArray()));
				if (selectorForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					((BaseUnit)base.MyObject).AddSkill((Skill)selectorForm.SelectedObject);
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void btnAddSkillRest_Click(object sender, EventArgs e)
		{
			try
			{
				List<SkillPermission> skillPermissions = new List<SkillPermission>();
				OCCBase[] collectors = base.Collectors;
				for (int i = 0; i < (int)collectors.Length; i++)
				{
					OCCBase oCCBase = collectors[i];
					if (oCCBase.IsOfType(typeof(SkillPermission)))
					{
						skillPermissions.AddRange(Array.ConvertAll<object, SkillPermission>(oCCBase.Objects, (object Obj) => (Obj is SkillPermission ? (SkillPermission)Obj : null)));
					}
				}
				SelectorForm selectorForm = new SelectorForm("Add", "Cancel", "Select a Skill Permission to add...", "Add Skill Permission", ((BaseUnit)base.MyObject).getAllowedPermissions<SkillPermission>(skillPermissions.ToArray()));
				if (selectorForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					((BaseUnit)base.MyObject).AddSkillPermission((SkillPermission)selectorForm.SelectedObject);
				}
			}
			catch (RaceException raceException)
			{
			}
			catch (Exception exception)
			{
			}
		}

		private void btnAddWeapon_Click(object sender, EventArgs e)
		{
			try
			{
				List<Weapon> weapons = new List<Weapon>();
				OCCBase[] collectors = base.Collectors;
				for (int i = 0; i < (int)collectors.Length; i++)
				{
					OCCBase oCCBase = collectors[i];
					if (oCCBase.IsOfType(typeof(Weapon)))
					{
						weapons.AddRange(oCCBase.Objects as Weapon[]);
					}
				}
				SelectorForm selectorForm = new SelectorForm("Add", "Cancel", "Select a Weapon to add...", "Add Weapon", ((BaseUnit)base.MyObject).getAvalableWeapons(weapons.ToArray()));
				if (selectorForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					((BaseUnit)base.MyObject).AddWeapon((Weapon)selectorForm.SelectedObject);
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void btnAddWpnRest_Click(object sender, EventArgs e)
		{
			try
			{
				List<WeaponPermission> weaponPermissions = new List<WeaponPermission>();
				OCCBase[] collectors = base.Collectors;
				for (int i = 0; i < (int)collectors.Length; i++)
				{
					OCCBase oCCBase = collectors[i];
					if ((!oCCBase.IsOfType(typeof(WeaponPermission)) ? false : oCCBase.Objects.Length != 0))
					{
						weaponPermissions.AddRange(Array.ConvertAll<object, WeaponPermission>(oCCBase.Objects, (object Obj) => (Obj is WeaponPermission ? (WeaponPermission)Obj : null)));
					}
				}
				SelectorForm selectorForm = new SelectorForm("Add", "Cancel", "Select a Weapon Permission to add...", "Add Weapon Permission", ((BaseUnit)base.MyObject).getAllowedPermissions<WeaponPermission>(weaponPermissions.ToArray()));
				if (selectorForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					((BaseUnit)base.MyObject).AddWeaponPermission((WeaponPermission)selectorForm.SelectedObject);
				}
			}
			catch (RaceException raceException)
			{
			}
			catch (Exception exception)
			{
			}
		}

		private void btnRemPsy_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.lstPsys.SelectedItem != null)
				{
					((BaseUnit)base.MyObject).DropPsy((Psy)this.lstPsys.SelectedItem);
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void btnRemSkill_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.lstSkils.SelectedItem != null)
				{
					((BaseUnit)base.MyObject).DropSkill((Skill)this.lstSkils.SelectedItem);
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void btnRemWeapon_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.lstWeapons.SelectedItem != null)
				{
					((BaseUnit)base.MyObject).DropWeapon((Weapon)this.lstWepRes.SelectedItem);
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void btnRmvItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.lstItems.SelectedItem != null)
				{
					((BaseUnit)base.MyObject).DropItem((Item)this.lstItems.SelectedItem);
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void btnRmvPsyRest_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.lstPsyRes.SelectedItem != null)
				{
					((BaseUnit)base.MyObject).DropPsyPermission((PsyPermission)this.lstPsyRes.SelectedItem);
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void btnRmvSkillRest_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.lstSkillRes.SelectedItem != null)
				{
					((BaseUnit)base.MyObject).DropSkillPermission((SkillPermission)this.lstSkillRes.SelectedItem);
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void btnRmvWpnRest_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.lstWepRes.SelectedItem != null)
				{
					((BaseUnit)base.MyObject).DropWeaponPermission((WeaponPermission)this.lstWepRes.SelectedItem);
				}
			}
			catch (Exception exception)
			{
			}
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

		private void cbxUnitType_SelectedIndexChanged(object sender, EventArgs e)
		{
			((BaseUnit)base.MyObject).archetype = (Archetype)this.cbxArchatype.SelectedItem;
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
				if (control.Name.Substring(0, 3) == "txt")
				{
					((TextBox)control).ReadOnly = !editing;
				}
				if (control.Name.Substring(0, 3) == "cbx")
				{
					((ComboBox)control).Enabled = editing;
				}
				if (control.Name.Substring(0, 3) == "lst")
				{
					((ListBox)control).Enabled = editing;
				}
				if (control.Name.Substring(0, 3) == "btn")
				{
					((Button)control).Visible = editing;
				}
				if (control.Name == "ButtonEdit")
				{
					((Button)control).Visible = !editing;
				}
			}
		}

		protected override void Enter_field(object sender, EventArgs e)
		{
			base.Enter_field(sender, e);
		}

		private void EventCostChanged()
		{
			Console.WriteLine("Cost changed!");
			this.lblCost.Text = ((BaseUnit)base.MyObject).Cost.ToString();
		}

		private void EventStatChanged(StatsMod.Stats Stat)
		{
			int movementMod;
			StatsMod.Stats stat = Stat;
			if (stat <= StatsMod.Stats.RangedWeapons)
			{
				if (stat <= StatsMod.Stats.AttackPoints)
				{
					switch (stat)
					{
						case StatsMod.Stats.Movement:
						{
							Label mMod = this.MMod;
							movementMod = ((BaseUnit)base.MyObject).MovementMod;
							mMod.Text = movementMod.ToString();
							Label mTot = this.MTot;
							movementMod = ((BaseUnit)base.MyObject).MovementTotal;
							mTot.Text = movementMod.ToString();
							break;
						}
						case StatsMod.Stats.HitPoints:
						{
							Label hPMod = this.HPMod;
							movementMod = ((BaseUnit)base.MyObject).HitPointsMod;
							hPMod.Text = movementMod.ToString();
							Label hPTot = this.HPTot;
							movementMod = ((BaseUnit)base.MyObject).HitPointsTotal;
							hPTot.Text = movementMod.ToString();
							break;
						}
						case StatsMod.Stats.Movement | StatsMod.Stats.HitPoints:
						{
							break;
						}
						case StatsMod.Stats.HandToHand:
						{
							Label hHMod = this.HHMod;
							movementMod = ((BaseUnit)base.MyObject).HandToHandMod;
							hHMod.Text = movementMod.ToString();
							Label hHTot = this.HHTot;
							movementMod = ((BaseUnit)base.MyObject).HandToHandTotal;
							hHTot.Text = movementMod.ToString();
							break;
						}
						default:
						{
							if (stat == StatsMod.Stats.AttackPoints)
							{
								Label aPMod = this.APMod;
								movementMod = ((BaseUnit)base.MyObject).AttackPointsMod;
								aPMod.Text = movementMod.ToString();
								Label aPTot = this.APTot;
								movementMod = ((BaseUnit)base.MyObject).AttackPointsTotal;
								aPTot.Text = movementMod.ToString();
								break;
							}
							else
							{
								break;
							}
						}
					}
				}
				else if (stat == StatsMod.Stats.PsyPoints)
				{
					Label pPMod = this.PPMod;
					movementMod = ((BaseUnit)base.MyObject).PsyPointsMod;
					pPMod.Text = movementMod.ToString();
					Label pPTot = this.PPTot;
					movementMod = ((BaseUnit)base.MyObject).PsyPointsTotal;
					pPTot.Text = movementMod.ToString();
				}
				else if (stat == StatsMod.Stats.RangedWeapons)
				{
					Label rWMod = this.RWMod;
					movementMod = ((BaseUnit)base.MyObject).RangedWeaponsMod;
					rWMod.Text = movementMod.ToString();
					Label rWTot = this.RWTot;
					movementMod = ((BaseUnit)base.MyObject).RangedWeaponsTotal;
					rWTot.Text = movementMod.ToString();
				}
			}
			else if (stat <= StatsMod.Stats.Toughness)
			{
				if (stat == StatsMod.Stats.Might)
				{
					Label mTMod = this.MTMod;
					movementMod = ((BaseUnit)base.MyObject).MightMod;
					mTMod.Text = movementMod.ToString();
					Label mTTot = this.MTTot;
					movementMod = ((BaseUnit)base.MyObject).MightTotal;
					mTTot.Text = movementMod.ToString();
				}
				else if (stat == StatsMod.Stats.Toughness)
				{
					Label tMod = this.TMod;
					movementMod = ((BaseUnit)base.MyObject).ToughnessMod;
					tMod.Text = movementMod.ToString();
					Label tTot = this.TTot;
					movementMod = ((BaseUnit)base.MyObject).ToughnessTotal;
					tTot.Text = movementMod.ToString();
				}
			}
			else if (stat == StatsMod.Stats.Agility)
			{
				Label aGMod = this.AGMod;
				movementMod = ((BaseUnit)base.MyObject).AgilityMod;
				aGMod.Text = movementMod.ToString();
				Label aGTot = this.AGTot;
				movementMod = ((BaseUnit)base.MyObject).AgilityTotal;
				aGTot.Text = movementMod.ToString();
			}
			else if (stat == StatsMod.Stats.Willpower)
			{
				Label wPMod = this.WPMod;
				movementMod = ((BaseUnit)base.MyObject).WillpowerMod;
				wPMod.Text = movementMod.ToString();
				Label wPTot = this.WPTot;
				movementMod = ((BaseUnit)base.MyObject).WillpowerTotal;
				wPTot.Text = movementMod.ToString();
			}
			else if (stat == StatsMod.Stats.HorrorFactor)
			{
				Label hFMod = this.HFMod;
				movementMod = ((BaseUnit)base.MyObject).HorrorFactorMod;
				hFMod.Text = movementMod.ToString();
				Label hFTot = this.HFTot;
				movementMod = ((BaseUnit)base.MyObject).HorrorFactorTotal;
				hFTot.Text = movementMod.ToString();
			}
			Console.WriteLine(string.Concat(Stat.ToString(), " Updated!"));
		}

		private void EventUnitTypeChanged()
		{
			this.lblEXP.Text = ((BaseUnit)base.MyObject).StartingEXP;
		}

		private void InitializeComponent()
		{
			this.cbxRace = new ComboBox();
			this.cbxArchatype = new ComboBox();
			this.lblStartingBaseStats = new Label();
			this.lblHdrEXP = new Label();
			this.lblCostHead = new Label();
			this.lblHF = new Label();
			this.lblWP = new Label();
			this.lblAG = new Label();
			this.lblT = new Label();
			this.lblMT = new Label();
			this.lblRW = new Label();
			this.lblPP = new Label();
			this.lblHH = new Label();
			this.lblAP = new Label();
			this.lblHP = new Label();
			this.Label19 = new Label();
			this.Label18 = new Label();
			this.lblM = new Label();
			this.lblEXP = new Label();
			this.lblCost = new Label();
			this.txtSpec = new TextBox();
			this.txtDes = new TextBox();
			this.lblRace = new Label();
			this.lblUnitType = new Label();
			this.lblName = new Label();
			this.btnRmvPsyRest = new Button();
			this.btnRmvSkillRest = new Button();
			this.btnRmvWpnRest = new Button();
			this.btnAddPsyRest = new Button();
			this.btnAddSkillRest = new Button();
			this.btnAddWpnRest = new Button();
			this.lstPsyRes = new ListBox();
			this.lstSkillRes = new ListBox();
			this.lstWepRes = new ListBox();
			this.lblPPerm = new Label();
			this.lblSPerm = new Label();
			this.lblWPerm = new Label();
			this.lblWeapons = new Label();
			this.lblSkills = new Label();
			this.lblPsys = new Label();
			this.lstWeapons = new ListBox();
			this.lstSkils = new ListBox();
			this.lstPsys = new ListBox();
			this.btnAddWeapon = new Button();
			this.btnAddSkills = new Button();
			this.btnAddPsy = new Button();
			this.btnRemWeapon = new Button();
			this.btnRemSkill = new Button();
			this.btnRemPsy = new Button();
			this.lblItems = new Label();
			this.lstItems = new ListBox();
			this.btnAddItem = new Button();
			this.btnRemItem = new Button();
			this.ButtonCancel = new Button();
			this.ButtonEdit = new Button();
			this.ButtonSave = new Button();
			this.lblID = new Label();
			this.FIELD_Name = new TextBox();
			this.txtCostMod = new TextBox();
			this.lblConstMod = new Label();
			this.MMod = new Label();
			this.HPMod = new Label();
			this.APMod = new Label();
			this.HHMod = new Label();
			this.PPMod = new Label();
			this.RWMod = new Label();
			this.MTMod = new Label();
			this.TMod = new Label();
			this.AGMod = new Label();
			this.WPMod = new Label();
			this.HFMod = new Label();
			this.MTot = new Label();
			this.HPTot = new Label();
			this.APTot = new Label();
			this.HHTot = new Label();
			this.PPTot = new Label();
			this.RWTot = new Label();
			this.MTTot = new Label();
			this.TTot = new Label();
			this.AGTot = new Label();
			this.WPTot = new Label();
			this.HFTot = new Label();
			this.nrM = new NumericUpDown();
			this.nrHP = new NumericUpDown();
			this.nrAP = new NumericUpDown();
			this.nrHH = new NumericUpDown();
			this.nrPP = new NumericUpDown();
			this.nrRW = new NumericUpDown();
			this.nrMGT = new NumericUpDown();
			this.nrT = new NumericUpDown();
			this.nrAGIL = new NumericUpDown();
			this.nrWP = new NumericUpDown();
			this.nrHF = new NumericUpDown();
			((ISupportInitialize)this.nrM).BeginInit();
			((ISupportInitialize)this.nrHP).BeginInit();
			((ISupportInitialize)this.nrAP).BeginInit();
			((ISupportInitialize)this.nrHH).BeginInit();
			((ISupportInitialize)this.nrPP).BeginInit();
			((ISupportInitialize)this.nrRW).BeginInit();
			((ISupportInitialize)this.nrMGT).BeginInit();
			((ISupportInitialize)this.nrT).BeginInit();
			((ISupportInitialize)this.nrAGIL).BeginInit();
			((ISupportInitialize)this.nrWP).BeginInit();
			((ISupportInitialize)this.nrHF).BeginInit();
			base.SuspendLayout();
			this.cbxRace.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbxRace.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.cbxRace.FormattingEnabled = true;
			this.cbxRace.Location = new Point(495, 28);
			this.cbxRace.Name = "cbxRace";
			this.cbxRace.Size = new System.Drawing.Size(186, 24);
			this.cbxRace.Sorted = true;
			this.cbxRace.TabIndex = 5;
			this.cbxRace.SelectedValueChanged += new EventHandler(this.Race_Changed);
			this.cbxArchatype.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbxArchatype.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.cbxArchatype.FormattingEnabled = true;
			this.cbxArchatype.Location = new Point(303, 28);
			this.cbxArchatype.Name = "cbxArchatype";
			this.cbxArchatype.Size = new System.Drawing.Size(186, 24);
			this.cbxArchatype.Sorted = true;
			this.cbxArchatype.TabIndex = 3;
			this.cbxArchatype.SelectedIndexChanged += new EventHandler(this.cbxUnitType_SelectedIndexChanged);
			this.lblStartingBaseStats.AutoSize = true;
			this.lblStartingBaseStats.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblStartingBaseStats.Location = new Point(12, 55);
			this.lblStartingBaseStats.Name = "lblStartingBaseStats";
			this.lblStartingBaseStats.Size = new System.Drawing.Size(117, 16);
			this.lblStartingBaseStats.TabIndex = 92;
			this.lblStartingBaseStats.Text = "Starting Base Stats";
			this.lblStartingBaseStats.TextAlign = ContentAlignment.MiddleCenter;
			this.lblHdrEXP.AutoSize = true;
			this.lblHdrEXP.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblHdrEXP.Location = new Point(623, 71);
			this.lblHdrEXP.Name = "lblHdrEXP";
			this.lblHdrEXP.Size = new System.Drawing.Size(30, 16);
			this.lblHdrEXP.TabIndex = 30;
			this.lblHdrEXP.Text = "&EXP";
			this.lblCostHead.AutoSize = true;
			this.lblCostHead.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblCostHead.Location = new Point(545, 71);
			this.lblCostHead.Name = "lblCostHead";
			this.lblCostHead.Size = new System.Drawing.Size(41, 16);
			this.lblCostHead.TabIndex = 28;
			this.lblCostHead.Text = "COST";
			this.lblHF.AutoSize = true;
			this.lblHF.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblHF.Location = new Point(506, 71);
			this.lblHF.Name = "lblHF";
			this.lblHF.Size = new System.Drawing.Size(23, 16);
			this.lblHF.TabIndex = 26;
			this.lblHF.Text = "&HF";
			this.lblWP.AutoSize = true;
			this.lblWP.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblWP.Location = new Point(455, 71);
			this.lblWP.Name = "lblWP";
			this.lblWP.Size = new System.Drawing.Size(27, 16);
			this.lblWP.TabIndex = 24;
			this.lblWP.Text = "&WP";
			this.lblAG.AutoSize = true;
			this.lblAG.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblAG.Location = new Point(403, 71);
			this.lblAG.Name = "lblAG";
			this.lblAG.Size = new System.Drawing.Size(34, 16);
			this.lblAG.TabIndex = 22;
			this.lblAG.Text = "AG&IL";
			this.lblT.AutoSize = true;
			this.lblT.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblT.Location = new Point(366, 71);
			this.lblT.Name = "lblT";
			this.lblT.Size = new System.Drawing.Size(16, 16);
			this.lblT.TabIndex = 20;
			this.lblT.Text = "&T";
			this.lblMT.AutoSize = true;
			this.lblMT.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblMT.Location = new Point(308, 71);
			this.lblMT.Name = "lblMT";
			this.lblMT.Size = new System.Drawing.Size(34, 16);
			this.lblMT.TabIndex = 18;
			this.lblMT.Text = "M&GT";
			this.lblRW.AutoSize = true;
			this.lblRW.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblRW.Location = new Point(266, 71);
			this.lblRW.Name = "lblRW";
			this.lblRW.Size = new System.Drawing.Size(28, 16);
			this.lblRW.TabIndex = 16;
			this.lblRW.Text = "&RW";
			this.lblPP.AutoSize = true;
			this.lblPP.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblPP.Location = new Point(175, 72);
			this.lblPP.Name = "lblPP";
			this.lblPP.Size = new System.Drawing.Size(22, 16);
			this.lblPP.TabIndex = 12;
			this.lblPP.Text = "&PP";
			this.lblHH.AutoSize = true;
			this.lblHH.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblHH.Location = new Point(217, 71);
			this.lblHH.Name = "lblHH";
			this.lblHH.Size = new System.Drawing.Size(32, 16);
			this.lblHH.TabIndex = 14;
			this.lblHH.Text = "H&+H";
			this.lblAP.AutoSize = true;
			this.lblAP.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblAP.Location = new Point(125, 71);
			this.lblAP.Name = "lblAP";
			this.lblAP.Size = new System.Drawing.Size(23, 16);
			this.lblAP.TabIndex = 10;
			this.lblAP.Text = "&AP";
			this.lblHP.AutoSize = true;
			this.lblHP.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblHP.Location = new Point(75, 71);
			this.lblHP.Name = "lblHP";
			this.lblHP.Size = new System.Drawing.Size(23, 16);
			this.lblHP.TabIndex = 8;
			this.lblHP.Text = "&HP";
			this.Label19.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.Label19.AutoSize = true;
			this.Label19.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.Label19.Location = new Point(352, 179);
			this.Label19.Name = "Label19";
			this.Label19.Size = new System.Drawing.Size(49, 16);
			this.Label19.TabIndex = 58;
			this.Label19.Text = "&Special";
			this.Label19.TextAlign = ContentAlignment.MiddleCenter;
			this.Label18.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.Label18.AutoSize = true;
			this.Label18.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.Label18.Location = new Point(9, 179);
			this.Label18.Name = "Label18";
			this.Label18.Size = new System.Drawing.Size(71, 16);
			this.Label18.TabIndex = 56;
			this.Label18.Text = "&Description";
			this.Label18.TextAlign = ContentAlignment.MiddleCenter;
			this.lblM.AutoSize = true;
			this.lblM.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblM.Location = new Point(27, 71);
			this.lblM.Name = "lblM";
			this.lblM.Size = new System.Drawing.Size(18, 16);
			this.lblM.TabIndex = 6;
			this.lblM.Text = "&M";
			this.lblM.TextAlign = ContentAlignment.MiddleCenter;
			this.lblEXP.BorderStyle = BorderStyle.FixedSingle;
			this.lblEXP.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblEXP.Location = new Point(591, 87);
			this.lblEXP.Name = "lblEXP";
			this.lblEXP.Size = new System.Drawing.Size(94, 23);
			this.lblEXP.TabIndex = 31;
			this.lblEXP.Text = "0";
			this.lblEXP.TextAlign = ContentAlignment.TopCenter;
			this.lblCost.BorderStyle = BorderStyle.FixedSingle;
			this.lblCost.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblCost.Location = new Point(543, 87);
			this.lblCost.Name = "lblCost";
			this.lblCost.Size = new System.Drawing.Size(42, 23);
			this.lblCost.TabIndex = 29;
			this.lblCost.Text = "21";
			this.lblCost.TextAlign = ContentAlignment.MiddleCenter;
			this.txtSpec.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.txtSpec.BorderStyle = BorderStyle.FixedSingle;
			this.txtSpec.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.txtSpec.Location = new Point(355, 198);
			this.txtSpec.Multiline = true;
			this.txtSpec.Name = "txtSpec";
			this.txtSpec.ReadOnly = true;
			this.txtSpec.ScrollBars = ScrollBars.Both;
			this.txtSpec.Size = new System.Drawing.Size(330, 118);
			this.txtSpec.TabIndex = 59;
			BaseUnitEditor baseUnitEditor = this;
			this.txtSpec.Leave += new EventHandler(baseUnitEditor.Leave_field);
			this.txtDes.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.txtDes.BorderStyle = BorderStyle.FixedSingle;
			this.txtDes.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.txtDes.Location = new Point(12, 198);
			this.txtDes.Multiline = true;
			this.txtDes.Name = "txtDes";
			this.txtDes.ReadOnly = true;
			this.txtDes.ScrollBars = ScrollBars.Both;
			this.txtDes.Size = new System.Drawing.Size(330, 118);
			this.txtDes.TabIndex = 57;
			BaseUnitEditor baseUnitEditor1 = this;
			this.txtDes.Leave += new EventHandler(baseUnitEditor1.Leave_field);
			this.lblRace.AutoSize = true;
			this.lblRace.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblRace.Location = new Point(492, 9);
			this.lblRace.Name = "lblRace";
			this.lblRace.Size = new System.Drawing.Size(36, 16);
			this.lblRace.TabIndex = 4;
			this.lblRace.Text = "Race";
			this.lblUnitType.AutoSize = true;
			this.lblUnitType.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblUnitType.Location = new Point(300, 9);
			this.lblUnitType.Name = "lblUnitType";
			this.lblUnitType.Size = new System.Drawing.Size(65, 16);
			this.lblUnitType.TabIndex = 2;
			this.lblUnitType.Text = "Ar&chetype";
			this.lblName.AutoSize = true;
			this.lblName.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblName.Location = new Point(12, 9);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(41, 16);
			this.lblName.TabIndex = 0;
			this.lblName.Text = "&Name";
			this.btnRmvPsyRest.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.btnRmvPsyRest.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btnRmvPsyRest.Location = new Point(435, 360);
			this.btnRmvPsyRest.Name = "btnRmvPsyRest";
			this.btnRmvPsyRest.Size = new System.Drawing.Size(23, 23);
			this.btnRmvPsyRest.TabIndex = 67;
			this.btnRmvPsyRest.Text = "-";
			this.btnRmvPsyRest.UseVisualStyleBackColor = true;
			this.btnRmvPsyRest.Visible = false;
			this.btnRmvPsyRest.Click += new EventHandler(this.btnRmvPsyRest_Click);
			this.btnRmvSkillRest.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.btnRmvSkillRest.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btnRmvSkillRest.Location = new Point(659, 360);
			this.btnRmvSkillRest.Name = "btnRmvSkillRest";
			this.btnRmvSkillRest.Size = new System.Drawing.Size(23, 23);
			this.btnRmvSkillRest.TabIndex = 71;
			this.btnRmvSkillRest.Text = "-";
			this.btnRmvSkillRest.UseVisualStyleBackColor = true;
			this.btnRmvSkillRest.Visible = false;
			this.btnRmvSkillRest.Click += new EventHandler(this.btnRmvSkillRest_Click);
			this.btnRmvWpnRest.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.btnRmvWpnRest.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btnRmvWpnRest.Location = new Point(211, 360);
			this.btnRmvWpnRest.Name = "btnRmvWpnRest";
			this.btnRmvWpnRest.Size = new System.Drawing.Size(23, 23);
			this.btnRmvWpnRest.TabIndex = 63;
			this.btnRmvWpnRest.Text = "-";
			this.btnRmvWpnRest.UseVisualStyleBackColor = true;
			this.btnRmvWpnRest.Visible = false;
			this.btnRmvWpnRest.Click += new EventHandler(this.btnRmvWpnRest_Click);
			this.btnAddPsyRest.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.btnAddPsyRest.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btnAddPsyRest.Location = new Point(435, 338);
			this.btnAddPsyRest.Name = "btnAddPsyRest";
			this.btnAddPsyRest.Size = new System.Drawing.Size(23, 23);
			this.btnAddPsyRest.TabIndex = 66;
			this.btnAddPsyRest.Text = "+";
			this.btnAddPsyRest.UseVisualStyleBackColor = true;
			this.btnAddPsyRest.Visible = false;
			this.btnAddPsyRest.Click += new EventHandler(this.btnAddPsyRest_Click);
			this.btnAddSkillRest.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.btnAddSkillRest.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btnAddSkillRest.Location = new Point(659, 338);
			this.btnAddSkillRest.Name = "btnAddSkillRest";
			this.btnAddSkillRest.Size = new System.Drawing.Size(23, 23);
			this.btnAddSkillRest.TabIndex = 70;
			this.btnAddSkillRest.Text = "+";
			this.btnAddSkillRest.UseVisualStyleBackColor = true;
			this.btnAddSkillRest.Visible = false;
			this.btnAddSkillRest.Click += new EventHandler(this.btnAddSkillRest_Click);
			this.btnAddWpnRest.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.btnAddWpnRest.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btnAddWpnRest.Location = new Point(211, 338);
			this.btnAddWpnRest.Name = "btnAddWpnRest";
			this.btnAddWpnRest.Size = new System.Drawing.Size(23, 23);
			this.btnAddWpnRest.TabIndex = 62;
			this.btnAddWpnRest.Text = "+";
			this.btnAddWpnRest.UseVisualStyleBackColor = true;
			this.btnAddWpnRest.Visible = false;
			this.btnAddWpnRest.Click += new EventHandler(this.btnAddWpnRest_Click);
			this.lstPsyRes.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.lstPsyRes.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lstPsyRes.FormattingEnabled = true;
			this.lstPsyRes.ItemHeight = 16;
			this.lstPsyRes.Location = new Point(239, 338);
			this.lstPsyRes.Name = "lstPsyRes";
			this.lstPsyRes.Size = new System.Drawing.Size(194, 68);
			this.lstPsyRes.Sorted = true;
			this.lstPsyRes.TabIndex = 65;
			this.lstSkillRes.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.lstSkillRes.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lstSkillRes.FormattingEnabled = true;
			this.lstSkillRes.ItemHeight = 16;
			this.lstSkillRes.Location = new Point(463, 338);
			this.lstSkillRes.Name = "lstSkillRes";
			this.lstSkillRes.Size = new System.Drawing.Size(194, 68);
			this.lstSkillRes.Sorted = true;
			this.lstSkillRes.TabIndex = 69;
			this.lstWepRes.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.lstWepRes.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lstWepRes.FormattingEnabled = true;
			this.lstWepRes.ItemHeight = 16;
			this.lstWepRes.Location = new Point(15, 338);
			this.lstWepRes.Name = "lstWepRes";
			this.lstWepRes.Size = new System.Drawing.Size(194, 68);
			this.lstWepRes.Sorted = true;
			this.lstWepRes.TabIndex = 61;
			this.lblPPerm.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.lblPPerm.AutoSize = true;
			this.lblPPerm.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblPPerm.Location = new Point(236, 319);
			this.lblPPerm.Name = "lblPPerm";
			this.lblPPerm.Size = new System.Drawing.Size(93, 16);
			this.lblPPerm.TabIndex = 64;
			this.lblPPerm.Text = "Psy Permission";
			this.lblSPerm.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.lblSPerm.AutoSize = true;
			this.lblSPerm.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblSPerm.Location = new Point(460, 319);
			this.lblSPerm.Name = "lblSPerm";
			this.lblSPerm.Size = new System.Drawing.Size(97, 16);
			this.lblSPerm.TabIndex = 68;
			this.lblSPerm.Text = "Skill Permission";
			this.lblWPerm.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.lblWPerm.AutoSize = true;
			this.lblWPerm.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblWPerm.Location = new Point(12, 319);
			this.lblWPerm.Name = "lblWPerm";
			this.lblWPerm.Size = new System.Drawing.Size(121, 16);
			this.lblWPerm.TabIndex = 60;
			this.lblWPerm.Text = "Weapon Permission";
			this.lblWeapons.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.lblWeapons.AutoSize = true;
			this.lblWeapons.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblWeapons.Location = new Point(12, 409);
			this.lblWeapons.Name = "lblWeapons";
			this.lblWeapons.Size = new System.Drawing.Size(61, 16);
			this.lblWeapons.TabIndex = 72;
			this.lblWeapons.Text = "Weapons";
			this.lblSkills.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.lblSkills.AutoSize = true;
			this.lblSkills.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblSkills.Location = new Point(460, 409);
			this.lblSkills.Name = "lblSkills";
			this.lblSkills.Size = new System.Drawing.Size(37, 16);
			this.lblSkills.TabIndex = 80;
			this.lblSkills.Text = "Skills";
			this.lblPsys.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.lblPsys.AutoSize = true;
			this.lblPsys.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblPsys.Location = new Point(236, 409);
			this.lblPsys.Name = "lblPsys";
			this.lblPsys.Size = new System.Drawing.Size(33, 16);
			this.lblPsys.TabIndex = 76;
			this.lblPsys.Text = "Psys";
			this.lstWeapons.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.lstWeapons.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lstWeapons.FormattingEnabled = true;
			this.lstWeapons.ItemHeight = 16;
			this.lstWeapons.Location = new Point(15, 428);
			this.lstWeapons.Name = "lstWeapons";
			this.lstWeapons.Size = new System.Drawing.Size(194, 68);
			this.lstWeapons.Sorted = true;
			this.lstWeapons.TabIndex = 73;
			this.lstSkils.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.lstSkils.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lstSkils.FormattingEnabled = true;
			this.lstSkils.ItemHeight = 16;
			this.lstSkils.Location = new Point(463, 428);
			this.lstSkils.Name = "lstSkils";
			this.lstSkils.Size = new System.Drawing.Size(194, 68);
			this.lstSkils.Sorted = true;
			this.lstSkils.TabIndex = 81;
			this.lstPsys.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.lstPsys.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lstPsys.FormattingEnabled = true;
			this.lstPsys.ItemHeight = 16;
			this.lstPsys.Location = new Point(239, 428);
			this.lstPsys.Name = "lstPsys";
			this.lstPsys.Size = new System.Drawing.Size(194, 68);
			this.lstPsys.Sorted = true;
			this.lstPsys.TabIndex = 77;
			this.btnAddWeapon.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.btnAddWeapon.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btnAddWeapon.Location = new Point(211, 428);
			this.btnAddWeapon.Name = "btnAddWeapon";
			this.btnAddWeapon.Size = new System.Drawing.Size(23, 23);
			this.btnAddWeapon.TabIndex = 74;
			this.btnAddWeapon.Text = "+";
			this.btnAddWeapon.UseVisualStyleBackColor = true;
			this.btnAddWeapon.Visible = false;
			this.btnAddWeapon.Click += new EventHandler(this.btnAddWeapon_Click);
			this.btnAddSkills.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.btnAddSkills.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btnAddSkills.Location = new Point(659, 428);
			this.btnAddSkills.Name = "btnAddSkills";
			this.btnAddSkills.Size = new System.Drawing.Size(23, 23);
			this.btnAddSkills.TabIndex = 82;
			this.btnAddSkills.Text = "+";
			this.btnAddSkills.UseVisualStyleBackColor = true;
			this.btnAddSkills.Visible = false;
			this.btnAddSkills.Click += new EventHandler(this.btnAddSkill_Click);
			this.btnAddPsy.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.btnAddPsy.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btnAddPsy.Location = new Point(435, 428);
			this.btnAddPsy.Name = "btnAddPsy";
			this.btnAddPsy.Size = new System.Drawing.Size(23, 23);
			this.btnAddPsy.TabIndex = 78;
			this.btnAddPsy.Text = "+";
			this.btnAddPsy.UseVisualStyleBackColor = true;
			this.btnAddPsy.Visible = false;
			this.btnAddPsy.Click += new EventHandler(this.btnAddPsy_Click);
			this.btnRemWeapon.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.btnRemWeapon.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btnRemWeapon.Location = new Point(211, 450);
			this.btnRemWeapon.Name = "btnRemWeapon";
			this.btnRemWeapon.Size = new System.Drawing.Size(23, 23);
			this.btnRemWeapon.TabIndex = 75;
			this.btnRemWeapon.Text = "-";
			this.btnRemWeapon.UseVisualStyleBackColor = true;
			this.btnRemWeapon.Visible = false;
			this.btnRemWeapon.Click += new EventHandler(this.btnRemWeapon_Click);
			this.btnRemSkill.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.btnRemSkill.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btnRemSkill.Location = new Point(659, 450);
			this.btnRemSkill.Name = "btnRemSkill";
			this.btnRemSkill.Size = new System.Drawing.Size(23, 23);
			this.btnRemSkill.TabIndex = 83;
			this.btnRemSkill.Text = "-";
			this.btnRemSkill.UseVisualStyleBackColor = true;
			this.btnRemSkill.Visible = false;
			this.btnRemSkill.Click += new EventHandler(this.btnRemSkill_Click);
			this.btnRemPsy.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.btnRemPsy.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btnRemPsy.Location = new Point(435, 450);
			this.btnRemPsy.Name = "btnRemPsy";
			this.btnRemPsy.Size = new System.Drawing.Size(23, 23);
			this.btnRemPsy.TabIndex = 79;
			this.btnRemPsy.Text = "-";
			this.btnRemPsy.UseVisualStyleBackColor = true;
			this.btnRemPsy.Visible = false;
			this.btnRemPsy.Click += new EventHandler(this.btnRemPsy_Click);
			this.lblItems.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
			this.lblItems.AutoSize = true;
			this.lblItems.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblItems.Location = new Point(688, 9);
			this.lblItems.Name = "lblItems";
			this.lblItems.Size = new System.Drawing.Size(40, 16);
			this.lblItems.TabIndex = 84;
			this.lblItems.Text = "It&ems";
			this.lstItems.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
			this.lstItems.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lstItems.FormattingEnabled = true;
			this.lstItems.ItemHeight = 16;
			this.lstItems.Location = new Point(691, 28);
			this.lstItems.Name = "lstItems";
			this.lstItems.Size = new System.Drawing.Size(210, 356);
			this.lstItems.Sorted = true;
			this.lstItems.TabIndex = 85;
			this.btnAddItem.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.btnAddItem.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btnAddItem.Location = new Point(907, 28);
			this.btnAddItem.Name = "btnAddItem";
			this.btnAddItem.Size = new System.Drawing.Size(23, 23);
			this.btnAddItem.TabIndex = 86;
			this.btnAddItem.Text = "+";
			this.btnAddItem.UseVisualStyleBackColor = true;
			this.btnAddItem.Visible = false;
			this.btnAddItem.Click += new EventHandler(this.btnAddItem_Click);
			this.btnRemItem.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.btnRemItem.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btnRemItem.Location = new Point(907, 50);
			this.btnRemItem.Name = "btnRemItem";
			this.btnRemItem.Size = new System.Drawing.Size(23, 23);
			this.btnRemItem.TabIndex = 87;
			this.btnRemItem.Text = "-";
			this.btnRemItem.UseVisualStyleBackColor = true;
			this.btnRemItem.Visible = false;
			this.btnRemItem.Click += new EventHandler(this.btnRmvItem_Click);
			this.ButtonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.ButtonCancel.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.ButtonCancel.Location = new Point(715, 470);
			this.ButtonCancel.Name = "ButtonCancel";
			this.ButtonCancel.Size = new System.Drawing.Size(186, 23);
			this.ButtonCancel.TabIndex = 91;
			this.ButtonCancel.Text = "Cancel";
			this.ButtonCancel.UseVisualStyleBackColor = true;
			BaseUnitEditor baseUnitEditor2 = this;
			this.ButtonCancel.Click += new EventHandler(baseUnitEditor2.button_Cancel_Click);
			this.ButtonEdit.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.ButtonEdit.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.ButtonEdit.Location = new Point(715, 412);
			this.ButtonEdit.Name = "ButtonEdit";
			this.ButtonEdit.Size = new System.Drawing.Size(186, 23);
			this.ButtonEdit.TabIndex = 89;
			this.ButtonEdit.Text = "Edit";
			this.ButtonEdit.UseVisualStyleBackColor = true;
			this.ButtonEdit.Visible = false;
			BaseUnitEditor baseUnitEditor3 = this;
			this.ButtonEdit.Click += new EventHandler(baseUnitEditor3.button_Edit_Click);
			this.ButtonSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.ButtonSave.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.ButtonSave.Location = new Point(715, 441);
			this.ButtonSave.Name = "ButtonSave";
			this.ButtonSave.Size = new System.Drawing.Size(186, 23);
			this.ButtonSave.TabIndex = 90;
			this.ButtonSave.Text = "Finished";
			this.ButtonSave.UseVisualStyleBackColor = true;
			BaseUnitEditor baseUnitEditor4 = this;
			this.ButtonSave.Click += new EventHandler(baseUnitEditor4.button_Save_Click);
			this.lblID.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.lblID.AutoSize = true;
			this.lblID.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblID.Location = new Point(712, 396);
			this.lblID.Name = "lblID";
			this.lblID.Size = new System.Drawing.Size(25, 16);
			this.lblID.TabIndex = 88;
			this.lblID.Text = "ID:";
			this.lblID.Visible = false;
			this.FIELD_Name.BorderStyle = BorderStyle.FixedSingle;
			this.FIELD_Name.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.FIELD_Name.Location = new Point(12, 29);
			this.FIELD_Name.Name = "FIELD_Name";
			this.FIELD_Name.ReadOnly = true;
			this.FIELD_Name.Size = new System.Drawing.Size(282, 23);
			this.FIELD_Name.TabIndex = 1;
			this.FIELD_Name.TextAlign = HorizontalAlignment.Center;
			BaseUnitEditor baseUnitEditor5 = this;
			this.FIELD_Name.Leave += new EventHandler(baseUnitEditor5.Leave_field);
			this.txtCostMod.BorderStyle = BorderStyle.FixedSingle;
			this.txtCostMod.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.txtCostMod.Location = new Point(543, 138);
			this.txtCostMod.Name = "txtCostMod";
			this.txtCostMod.ReadOnly = true;
			this.txtCostMod.Size = new System.Drawing.Size(142, 23);
			this.txtCostMod.TabIndex = 33;
			this.txtCostMod.Text = "0";
			this.txtCostMod.TextAlign = HorizontalAlignment.Center;
			BaseUnitEditor baseUnitEditor6 = this;
			this.txtCostMod.Leave += new EventHandler(baseUnitEditor6.Leave_field);
			this.txtCostMod.KeyPress += new KeyPressEventHandler(this.txt_KeyPress);
			this.lblConstMod.AutoSize = true;
			this.lblConstMod.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblConstMod.Location = new Point(545, 122);
			this.lblConstMod.Name = "lblConstMod";
			this.lblConstMod.Size = new System.Drawing.Size(83, 16);
			this.lblConstMod.TabIndex = 32;
			this.lblConstMod.Text = "Cost Modifier";
			this.MMod.AutoSize = true;
			this.MMod.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.MMod.Location = new Point(27, 122);
			this.MMod.Name = "MMod";
			this.MMod.Size = new System.Drawing.Size(15, 16);
			this.MMod.TabIndex = 34;
			this.MMod.Text = "0";
			this.MMod.TextAlign = ContentAlignment.MiddleCenter;
			this.HPMod.AutoSize = true;
			this.HPMod.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.HPMod.Location = new Point(75, 122);
			this.HPMod.Name = "HPMod";
			this.HPMod.Size = new System.Drawing.Size(15, 16);
			this.HPMod.TabIndex = 35;
			this.HPMod.Text = "0";
			this.APMod.AutoSize = true;
			this.APMod.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.APMod.Location = new Point(125, 122);
			this.APMod.Name = "APMod";
			this.APMod.Size = new System.Drawing.Size(15, 16);
			this.APMod.TabIndex = 36;
			this.APMod.Text = "0";
			this.HHMod.AutoSize = true;
			this.HHMod.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.HHMod.Location = new Point(217, 122);
			this.HHMod.Name = "HHMod";
			this.HHMod.Size = new System.Drawing.Size(15, 16);
			this.HHMod.TabIndex = 38;
			this.HHMod.Text = "0";
			this.PPMod.AutoSize = true;
			this.PPMod.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.PPMod.Location = new Point(175, 123);
			this.PPMod.Name = "PPMod";
			this.PPMod.Size = new System.Drawing.Size(15, 16);
			this.PPMod.TabIndex = 37;
			this.PPMod.Text = "0";
			this.RWMod.AutoSize = true;
			this.RWMod.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.RWMod.Location = new Point(266, 122);
			this.RWMod.Name = "RWMod";
			this.RWMod.Size = new System.Drawing.Size(15, 16);
			this.RWMod.TabIndex = 39;
			this.RWMod.Text = "0";
			this.MTMod.AutoSize = true;
			this.MTMod.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.MTMod.Location = new Point(316, 122);
			this.MTMod.Name = "MTMod";
			this.MTMod.Size = new System.Drawing.Size(15, 16);
			this.MTMod.TabIndex = 40;
			this.MTMod.Text = "0";
			this.TMod.AutoSize = true;
			this.TMod.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.TMod.Location = new Point(366, 122);
			this.TMod.Name = "TMod";
			this.TMod.Size = new System.Drawing.Size(15, 16);
			this.TMod.TabIndex = 41;
			this.TMod.Text = "0";
			this.AGMod.AutoSize = true;
			this.AGMod.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.AGMod.Location = new Point(409, 122);
			this.AGMod.Name = "AGMod";
			this.AGMod.Size = new System.Drawing.Size(15, 16);
			this.AGMod.TabIndex = 42;
			this.AGMod.Text = "0";
			this.WPMod.AutoSize = true;
			this.WPMod.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.WPMod.Location = new Point(455, 122);
			this.WPMod.Name = "WPMod";
			this.WPMod.Size = new System.Drawing.Size(15, 16);
			this.WPMod.TabIndex = 43;
			this.WPMod.Text = "0";
			this.HFMod.AutoSize = true;
			this.HFMod.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.HFMod.Location = new Point(506, 122);
			this.HFMod.Name = "HFMod";
			this.HFMod.Size = new System.Drawing.Size(15, 16);
			this.HFMod.TabIndex = 44;
			this.HFMod.Text = "0";
			this.MTot.AutoSize = true;
			this.MTot.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.MTot.Location = new Point(27, 150);
			this.MTot.Name = "MTot";
			this.MTot.Size = new System.Drawing.Size(15, 16);
			this.MTot.TabIndex = 45;
			this.MTot.Text = "0";
			this.MTot.TextAlign = ContentAlignment.MiddleCenter;
			this.HPTot.AutoSize = true;
			this.HPTot.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.HPTot.Location = new Point(75, 150);
			this.HPTot.Name = "HPTot";
			this.HPTot.Size = new System.Drawing.Size(15, 16);
			this.HPTot.TabIndex = 46;
			this.HPTot.Text = "0";
			this.APTot.AutoSize = true;
			this.APTot.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.APTot.Location = new Point(125, 150);
			this.APTot.Name = "APTot";
			this.APTot.Size = new System.Drawing.Size(15, 16);
			this.APTot.TabIndex = 47;
			this.APTot.Text = "0";
			this.HHTot.AutoSize = true;
			this.HHTot.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.HHTot.Location = new Point(217, 150);
			this.HHTot.Name = "HHTot";
			this.HHTot.Size = new System.Drawing.Size(15, 16);
			this.HHTot.TabIndex = 49;
			this.HHTot.Text = "0";
			this.PPTot.AutoSize = true;
			this.PPTot.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.PPTot.Location = new Point(175, 151);
			this.PPTot.Name = "PPTot";
			this.PPTot.Size = new System.Drawing.Size(15, 16);
			this.PPTot.TabIndex = 48;
			this.PPTot.Text = "0";
			this.RWTot.AutoSize = true;
			this.RWTot.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.RWTot.Location = new Point(266, 150);
			this.RWTot.Name = "RWTot";
			this.RWTot.Size = new System.Drawing.Size(15, 16);
			this.RWTot.TabIndex = 50;
			this.RWTot.Text = "0";
			this.MTTot.AutoSize = true;
			this.MTTot.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.MTTot.Location = new Point(316, 150);
			this.MTTot.Name = "MTTot";
			this.MTTot.Size = new System.Drawing.Size(15, 16);
			this.MTTot.TabIndex = 51;
			this.MTTot.Text = "0";
			this.TTot.AutoSize = true;
			this.TTot.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.TTot.Location = new Point(366, 150);
			this.TTot.Name = "TTot";
			this.TTot.Size = new System.Drawing.Size(15, 16);
			this.TTot.TabIndex = 52;
			this.TTot.Text = "0";
			this.AGTot.AutoSize = true;
			this.AGTot.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.AGTot.Location = new Point(409, 150);
			this.AGTot.Name = "AGTot";
			this.AGTot.Size = new System.Drawing.Size(15, 16);
			this.AGTot.TabIndex = 53;
			this.AGTot.Text = "0";
			this.WPTot.AutoSize = true;
			this.WPTot.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.WPTot.Location = new Point(455, 150);
			this.WPTot.Name = "WPTot";
			this.WPTot.Size = new System.Drawing.Size(15, 16);
			this.WPTot.TabIndex = 54;
			this.WPTot.Text = "0";
			this.HFTot.AutoSize = true;
			this.HFTot.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.HFTot.Location = new Point(506, 150);
			this.HFTot.Name = "HFTot";
			this.HFTot.Size = new System.Drawing.Size(15, 16);
			this.HFTot.TabIndex = 55;
			this.HFTot.Text = "0";
			this.nrM.Location = new Point(15, 89);
			this.nrM.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
			this.nrM.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			this.nrM.Name = "nrM";
			this.nrM.Size = new System.Drawing.Size(42, 20);
			this.nrM.TabIndex = 7;
			this.nrM.Value = new decimal(new int[] { 1, 0, 0, 0 });
			this.nrM.ValueChanged += new EventHandler(this.StatNR_ValueChanged);
			this.nrHP.Location = new Point(63, 89);
			this.nrHP.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
			this.nrHP.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			this.nrHP.Name = "nrHP";
			this.nrHP.Size = new System.Drawing.Size(42, 20);
			this.nrHP.TabIndex = 9;
			this.nrHP.Value = new decimal(new int[] { 1, 0, 0, 0 });
			this.nrHP.ValueChanged += new EventHandler(this.StatNR_ValueChanged);
			this.nrAP.Location = new Point(111, 89);
			this.nrAP.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
			this.nrAP.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			this.nrAP.Name = "nrAP";
			this.nrAP.Size = new System.Drawing.Size(42, 20);
			this.nrAP.TabIndex = 11;
			this.nrAP.Value = new decimal(new int[] { 1, 0, 0, 0 });
			this.nrAP.ValueChanged += new EventHandler(this.StatNR_ValueChanged);
			this.nrHH.Location = new Point(208, 89);
			this.nrHH.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
			this.nrHH.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			this.nrHH.Name = "nrHH";
			this.nrHH.Size = new System.Drawing.Size(42, 20);
			this.nrHH.TabIndex = 15;
			this.nrHH.Value = new decimal(new int[] { 1, 0, 0, 0 });
			this.nrHH.ValueChanged += new EventHandler(this.StatNR_ValueChanged);
			this.nrPP.Location = new Point(159, 90);
			this.nrPP.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
			this.nrPP.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			this.nrPP.Name = "nrPP";
			this.nrPP.Size = new System.Drawing.Size(42, 20);
			this.nrPP.TabIndex = 13;
			this.nrPP.Value = new decimal(new int[] { 1, 0, 0, 0 });
			this.nrPP.ValueChanged += new EventHandler(this.StatNR_ValueChanged);
			this.nrRW.Location = new Point(255, 89);
			this.nrRW.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
			this.nrRW.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			this.nrRW.Name = "nrRW";
			this.nrRW.Size = new System.Drawing.Size(42, 20);
			this.nrRW.TabIndex = 17;
			this.nrRW.Value = new decimal(new int[] { 1, 0, 0, 0 });
			this.nrRW.ValueChanged += new EventHandler(this.StatNR_ValueChanged);
			this.nrMGT.Location = new Point(303, 89);
			this.nrMGT.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
			this.nrMGT.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			this.nrMGT.Name = "nrMGT";
			this.nrMGT.Size = new System.Drawing.Size(42, 20);
			this.nrMGT.TabIndex = 19;
			this.nrMGT.Value = new decimal(new int[] { 1, 0, 0, 0 });
			this.nrMGT.ValueChanged += new EventHandler(this.StatNR_ValueChanged);
			this.nrT.Location = new Point(351, 89);
			this.nrT.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
			this.nrT.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			this.nrT.Name = "nrT";
			this.nrT.Size = new System.Drawing.Size(42, 20);
			this.nrT.TabIndex = 21;
			this.nrT.Value = new decimal(new int[] { 1, 0, 0, 0 });
			this.nrT.ValueChanged += new EventHandler(this.StatNR_ValueChanged);
			this.nrAGIL.Location = new Point(398, 89);
			this.nrAGIL.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
			this.nrAGIL.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			this.nrAGIL.Name = "nrAGIL";
			this.nrAGIL.Size = new System.Drawing.Size(42, 20);
			this.nrAGIL.TabIndex = 23;
			this.nrAGIL.Value = new decimal(new int[] { 1, 0, 0, 0 });
			this.nrAGIL.ValueChanged += new EventHandler(this.StatNR_ValueChanged);
			this.nrWP.Location = new Point(447, 89);
			this.nrWP.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
			this.nrWP.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			this.nrWP.Name = "nrWP";
			this.nrWP.Size = new System.Drawing.Size(42, 20);
			this.nrWP.TabIndex = 25;
			this.nrWP.Value = new decimal(new int[] { 1, 0, 0, 0 });
			this.nrWP.ValueChanged += new EventHandler(this.StatNR_ValueChanged);
			this.nrHF.Location = new Point(494, 89);
			this.nrHF.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
			this.nrHF.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			this.nrHF.Name = "nrHF";
			this.nrHF.Size = new System.Drawing.Size(42, 20);
			this.nrHF.TabIndex = 27;
			this.nrHF.Value = new decimal(new int[] { 1, 0, 0, 0 });
			this.nrHF.ValueChanged += new EventHandler(this.StatNR_ValueChanged);
			base.AcceptButton = this.ButtonSave;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.ButtonCancel;
			base.ClientSize = new System.Drawing.Size(936, 508);
			base.Collectors = new OCCBase[0];
			base.Controls.Add(this.nrHF);
			base.Controls.Add(this.nrAGIL);
			base.Controls.Add(this.nrPP);
			base.Controls.Add(this.nrWP);
			base.Controls.Add(this.nrT);
			base.Controls.Add(this.nrHH);
			base.Controls.Add(this.nrMGT);
			base.Controls.Add(this.nrAP);
			base.Controls.Add(this.nrRW);
			base.Controls.Add(this.nrHP);
			base.Controls.Add(this.nrM);
			base.Controls.Add(this.lblID);
			base.Controls.Add(this.ButtonCancel);
			base.Controls.Add(this.ButtonEdit);
			base.Controls.Add(this.ButtonSave);
			base.Controls.Add(this.cbxRace);
			base.Controls.Add(this.cbxArchatype);
			base.Controls.Add(this.lblStartingBaseStats);
			base.Controls.Add(this.lblHdrEXP);
			base.Controls.Add(this.HFTot);
			base.Controls.Add(this.lblConstMod);
			base.Controls.Add(this.HFMod);
			base.Controls.Add(this.WPTot);
			base.Controls.Add(this.lblCostHead);
			base.Controls.Add(this.WPMod);
			base.Controls.Add(this.AGTot);
			base.Controls.Add(this.lblHF);
			base.Controls.Add(this.AGMod);
			base.Controls.Add(this.TTot);
			base.Controls.Add(this.lblWP);
			base.Controls.Add(this.TMod);
			base.Controls.Add(this.MTTot);
			base.Controls.Add(this.lblAG);
			base.Controls.Add(this.MTMod);
			base.Controls.Add(this.RWTot);
			base.Controls.Add(this.lblT);
			base.Controls.Add(this.RWMod);
			base.Controls.Add(this.PPTot);
			base.Controls.Add(this.lblMT);
			base.Controls.Add(this.PPMod);
			base.Controls.Add(this.HHTot);
			base.Controls.Add(this.lblRW);
			base.Controls.Add(this.HHMod);
			base.Controls.Add(this.APTot);
			base.Controls.Add(this.lblPP);
			base.Controls.Add(this.APMod);
			base.Controls.Add(this.HPTot);
			base.Controls.Add(this.lblHH);
			base.Controls.Add(this.HPMod);
			base.Controls.Add(this.lblAP);
			base.Controls.Add(this.lblHP);
			base.Controls.Add(this.MTot);
			base.Controls.Add(this.Label19);
			base.Controls.Add(this.MMod);
			base.Controls.Add(this.Label18);
			base.Controls.Add(this.lblM);
			base.Controls.Add(this.txtCostMod);
			base.Controls.Add(this.lblEXP);
			base.Controls.Add(this.lblCost);
			base.Controls.Add(this.txtSpec);
			base.Controls.Add(this.txtDes);
			base.Controls.Add(this.FIELD_Name);
			base.Controls.Add(this.lblRace);
			base.Controls.Add(this.lblUnitType);
			base.Controls.Add(this.lblName);
			base.Controls.Add(this.btnRemPsy);
			base.Controls.Add(this.btnRmvPsyRest);
			base.Controls.Add(this.btnRemSkill);
			base.Controls.Add(this.btnRemItem);
			base.Controls.Add(this.btnRmvSkillRest);
			base.Controls.Add(this.btnRemWeapon);
			base.Controls.Add(this.btnRmvWpnRest);
			base.Controls.Add(this.btnAddPsy);
			base.Controls.Add(this.btnAddPsyRest);
			base.Controls.Add(this.btnAddSkills);
			base.Controls.Add(this.btnAddItem);
			base.Controls.Add(this.btnAddSkillRest);
			base.Controls.Add(this.btnAddWeapon);
			base.Controls.Add(this.btnAddWpnRest);
			base.Controls.Add(this.lstPsys);
			base.Controls.Add(this.lstPsyRes);
			base.Controls.Add(this.lstSkils);
			base.Controls.Add(this.lstItems);
			base.Controls.Add(this.lstSkillRes);
			base.Controls.Add(this.lstWeapons);
			base.Controls.Add(this.lstWepRes);
			base.Controls.Add(this.lblPsys);
			base.Controls.Add(this.lblPPerm);
			base.Controls.Add(this.lblSkills);
			base.Controls.Add(this.lblItems);
			base.Controls.Add(this.lblSPerm);
			base.Controls.Add(this.lblWeapons);
			base.Controls.Add(this.lblWPerm);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.Name = "BaseUnitEditor";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "BaseUnitForm";
			BaseUnitEditor baseUnitEditor7 = this;
			base.Leave += new EventHandler(baseUnitEditor7.Leave_field);
			((ISupportInitialize)this.nrM).EndInit();
			((ISupportInitialize)this.nrHP).EndInit();
			((ISupportInitialize)this.nrAP).EndInit();
			((ISupportInitialize)this.nrHH).EndInit();
			((ISupportInitialize)this.nrPP).EndInit();
			((ISupportInitialize)this.nrRW).EndInit();
			((ISupportInitialize)this.nrMGT).EndInit();
			((ISupportInitialize)this.nrT).EndInit();
			((ISupportInitialize)this.nrAGIL).EndInit();
			((ISupportInitialize)this.nrWP).EndInit();
			((ISupportInitialize)this.nrHF).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void ItemsChanged(Item item)
		{
			Console.WriteLine("Items changed, updating list");
			this.lstItems.Items.Clear();
			this.lstItems.Items.AddRange(((BaseUnit)base.MyObject).Items);
			this.UpdateStats();
		}

		protected override void Leave_field(object sender, EventArgs e)
		{
			base.Leave_field(sender, e);
			if (sender is Control)
			{
				try
				{
					byte num = 1;
					byte.TryParse(((TextBox)sender).Text, out num);
					int num1 = 0;
					int.TryParse(((TextBox)sender).Text, out num1);
					if (sender is TextBox)
					{
						string name = ((TextBox)sender).Name;
						uint num2 = <PrivateImplementationDetails>.ComputeStringHash(name);
						if (num2 <= 1077617751)
						{
							if (num2 <= 190062685)
							{
								if (num2 == 6366064)
								{
									if (name == "txtRW")
									{
										((BaseUnit)base.MyObject).RangedWeapons = num;
									}
								}
								else if (num2 != 18759446)
								{
									if (num2 == 190062685)
									{
										if (name == "txtDes")
										{
											((BaseUnit)base.MyObject).Description = ((TextBox)sender).Text;
										}
									}
								}
								else if (name == "txtMT")
								{
									((BaseUnit)base.MyObject).Might = num;
								}
							}
							else if (num2 <= 824820633)
							{
								if (num2 != 482872266)
								{
									if (num2 == 824820633)
									{
										if (name == "txtHF")
										{
											((BaseUnit)base.MyObject).HorrorFactor = num;
										}
									}
								}
								else if (name == "txtCostMod")
								{
									((BaseUnit)base.MyObject).CostMod = num1;
								}
							}
							else if (num2 != 992596823)
							{
								if (num2 == 1077617751)
								{
									if (name == "txtAG")
									{
										((BaseUnit)base.MyObject).Agility = num;
									}
								}
							}
							else if (name == "txtHH")
							{
								((BaseUnit)base.MyObject).HandToHand = num;
							}
						}
						else if (num2 <= 1399202631)
						{
							if (num2 <= 1230293608)
							{
								if (num2 != 1126817775)
								{
									if (num2 == 1230293608)
									{
										if (name == "txtWP")
										{
											((BaseUnit)base.MyObject).Willpower = num;
										}
									}
								}
								else if (name == "txtHP")
								{
									((BaseUnit)base.MyObject).HitPoints = num;
								}
							}
							else if (num2 != 1295726798)
							{
								if (num2 == 1399202631)
								{
									if (name == "txtPP")
									{
										((BaseUnit)base.MyObject).PsyPoints = num;
									}
								}
							}
							else if (name == "txtAP")
							{
								((BaseUnit)base.MyObject).ActionPoints = num;
							}
						}
						else if (num2 <= -1250293242)
						{
							if (num2 != 1884125132)
							{
								if (num2 == -1250293242)
								{
									if (name == "txtM")
									{
										((BaseUnit)base.MyObject).Movement = num;
									}
								}
							}
							else if (name == "txtName")
							{
								((BaseUnit)base.MyObject).Name = ((TextBox)sender).Text;
							}
						}
						else if (num2 != -1084166472)
						{
							if (num2 == -830852767)
							{
								if (name == "txtT")
								{
									((BaseUnit)base.MyObject).Toughness = num;
								}
							}
						}
						else if (name == "txtSpec")
						{
							((BaseUnit)base.MyObject).Special = ((TextBox)sender).Text;
						}
					}
				}
				catch (Exception exception)
				{
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
					this.cbxRace.Items.AddRange(oCCBase.Objects);
				}
			}
			OCCBase[] oCCBaseArray = base.Collectors;
			for (int j = 0; j < (int)oCCBaseArray.Length; j++)
			{
				OCCBase oCCBase1 = oCCBaseArray[j];
				if (oCCBase1.IsOfType(typeof(Archetype)))
				{
					this.cbxArchatype.Items.AddRange(oCCBase1.Objects);
				}
			}
			this.cbxRace.Sorted = true;
			this.cbxArchatype.Sorted = true;
		}

		protected override void PopulateFields()
		{
			base.PopulateFields();
			if (((BaseUnit)base.MyObject).archetype != null)
			{
				bool flag = false;
				foreach (object item in this.cbxArchatype.Items)
				{
					if (!(item.GetType() == typeof(Archetype)) || !(((Archetype)item).ID == ((BaseUnit)base.MyObject).archetype.ID))
					{
						continue;
					}
					flag = true;
				}
				if ((flag ? false : ((BaseUnit)base.MyObject).archetype.Name != ""))
				{
					this.cbxArchatype.Items.Add(((BaseUnit)base.MyObject).archetype);
				}
			}
			if (((BaseUnit)base.MyObject).archetype.Name != "")
			{
				this.cbxArchatype.SelectedItem = ((BaseUnit)base.MyObject).archetype;
			}
			if (((BaseUnit)base.MyObject).BaseRace != null)
			{
				bool flag1 = false;
				foreach (object obj in this.cbxRace.Items)
				{
					if (!(obj.GetType() == typeof(Race)) || !(((Race)obj).ID == ((BaseUnit)base.MyObject).BaseRace.ID))
					{
						continue;
					}
					flag1 = true;
				}
				if ((flag1 ? false : ((BaseUnit)base.MyObject).BaseRace.Name != ""))
				{
					this.cbxRace.Items.Add(((BaseUnit)base.MyObject).BaseRace);
				}
			}
			if (((BaseUnit)base.MyObject).BaseRace.Name != "")
			{
				this.cbxRace.SelectedItem = ((BaseUnit)base.MyObject).BaseRace;
			}
			if (((BaseUnit)base.MyObject).WPermissions != null)
			{
				this.lstWepRes.Items.AddRange(((BaseUnit)base.MyObject).WPermissions);
			}
			if (((BaseUnit)base.MyObject).SPermissions != null)
			{
				this.lstSkillRes.Items.AddRange(((BaseUnit)base.MyObject).SPermissions);
			}
			if (((BaseUnit)base.MyObject).PPermissions != null)
			{
				this.lstPsyRes.Items.AddRange(((BaseUnit)base.MyObject).PPermissions);
			}
			if (((BaseUnit)base.MyObject).Weapons != null)
			{
				this.lstWeapons.Items.AddRange(((BaseUnit)base.MyObject).Weapons);
			}
			if (((BaseUnit)base.MyObject).SkillsAndAbilities != null)
			{
				this.lstSkils.Items.AddRange(((BaseUnit)base.MyObject).SkillsAndAbilities);
			}
			if (((BaseUnit)base.MyObject).Psys != null)
			{
				this.lstPsys.Items.AddRange(((BaseUnit)base.MyObject).Psys);
			}
			if (((BaseUnit)base.MyObject).Items != null)
			{
				this.lstItems.Items.AddRange(((BaseUnit)base.MyObject).Items);
			}
			this.lblCost.Text = ((BaseUnit)base.MyObject).Cost.ToString();
		}

		private void PsyPermissionsChanged(PsyPermission PPerm)
		{
			Console.WriteLine("Psy Permission changed, Updating List");
			this.lstPsyRes.Items.Clear();
			this.lstPsyRes.Items.AddRange(((BaseUnit)base.MyObject).PPermissions);
		}

		private void PsysChanged(Psy psy)
		{
			Console.WriteLine("Psys changed, Updating List");
			this.lstPsys.Items.Clear();
			this.lstPsys.Items.AddRange(((BaseUnit)base.MyObject).Psys);
		}

		private void Race_Changed(object sender, EventArgs e)
		{
			if ((this._PreviouslySelectedRace == null ? true : this._PreviouslySelectedRace == (Race)this.cbxRace.SelectedItem))
			{
				this._PreviouslySelectedRace = (Race)this.cbxRace.SelectedItem;
				((BaseUnit)base.MyObject).BaseRace = (Race)this.cbxRace.SelectedItem;
			}
			else
			{
				System.Windows.Forms.DialogResult dialogResult = MessageBox.Show(string.Concat(new string[] { "Changing your race will change your allowable permissions and could result in ", Environment.NewLine, "permissions, weapons, skills, and psy abilities to be removed from the current base unit", Environment.NewLine, "Do you wish to continue?" }), "Change Race?", MessageBoxButtons.YesNo);
				if (dialogResult == System.Windows.Forms.DialogResult.Yes)
				{
					try
					{
						((BaseUnit)base.MyObject).BaseRace = (Race)this.cbxRace.SelectedItem;
						this._PreviouslySelectedRace = (Race)this.cbxRace.SelectedItem;
					}
					catch (Exception exception)
					{
					}
				}
				else if (dialogResult == System.Windows.Forms.DialogResult.No)
				{
					this.cbxRace.SelectedItem = this._PreviouslySelectedRace;
				}
			}
		}

		public override EditorExitCode RunEditor(EditorMode mode)
		{
			this.InitializeComponent();
			this.nrM.Minimum = decimal.Zero;
			this.nrM.Maximum = new decimal(255);
			this.nrAP.Minimum = decimal.Zero;
			this.nrAP.Maximum = new decimal(255);
			this.nrHP.Minimum = decimal.Zero;
			this.nrHP.Maximum = new decimal(255);
			this.nrPP.Minimum = decimal.Zero;
			this.nrPP.Maximum = new decimal(255);
			this.nrAGIL.Minimum = decimal.One;
			this.nrAGIL.Maximum = new decimal(12);
			this.nrHF.Minimum = decimal.One;
			this.nrHF.Maximum = new decimal(12);
			this.nrHH.Minimum = decimal.One;
			this.nrHH.Maximum = new decimal(12);
			this.nrMGT.Minimum = decimal.One;
			this.nrMGT.Maximum = new decimal(12);
			this.nrRW.Minimum = decimal.One;
			this.nrRW.Maximum = new decimal(12);
			this.nrT.Minimum = decimal.One;
			this.nrT.Maximum = new decimal(12);
			this.nrWP.Minimum = decimal.One;
			this.nrWP.Maximum = new decimal(12);
			((BaseUnit)base.MyObject).EventWeaponPermissionsChanged += new BaseUnit.WeaponPermissionsChanged(this.WeaponPermissionsChanged);
			((BaseUnit)base.MyObject).EventSkillPermissionsChanged += new BaseUnit.SkillPermissionsChanged(this.SkillPermissionsChanged);
			((BaseUnit)base.MyObject).EventPsyPermissionsChanged += new BaseUnit.PsyPermissionsChanged(this.PsyPermissionsChanged);
			((BaseUnit)base.MyObject).EventItemsChanged += new BaseUnit.ItemsChanged(this.ItemsChanged);
			((BaseUnit)base.MyObject).EventWeaponsChanged += new BaseUnit.WeaponsChanged(this.WeaponsChanged);
			((BaseUnit)base.MyObject).EventSkillsChanged += new BaseUnit.SkillsChanged(this.SkillsChanged);
			((BaseUnit)base.MyObject).EventPsysChanged += new BaseUnit.PsysChanged(this.PsysChanged);
			((BaseUnit)base.MyObject).EventStatChanged += new BaseUnit.StatChanged(this.EventStatChanged);
			((BaseUnit)base.MyObject).EventCostChanged += new NTEventHandler(this.EventCostChanged);
			((BaseUnit)base.MyObject).EventUnitTypeChanged += new NTEventHandler(this.EventUnitTypeChanged);
			return base.RunEditor(mode);
		}

		private void SkillPermissionsChanged(SkillPermission PPerm)
		{
			Console.WriteLine("Skill Permission changed, Updating List");
			this.lstSkillRes.Items.Clear();
			this.lstSkillRes.Items.AddRange(((BaseUnit)base.MyObject).SPermissions);
		}

		private void SkillsChanged(Skill Skil)
		{
			Console.WriteLine("Skills changed, Updating List");
			this.lstSkils.Items.Clear();
			this.lstSkils.Items.AddRange(((BaseUnit)base.MyObject).SkillsAndAbilities);
			this.UpdateStats();
		}

		private void StatNR_ValueChanged(object sender, EventArgs e)
		{
			if (sender is NumericUpDown)
			{
				byte num = (byte)Math.Round(((NumericUpDown)sender).Value, 0, MidpointRounding.AwayFromZero);
				string name = ((NumericUpDown)sender).Name;
				uint num1 = <PrivateImplementationDetails>.ComputeStringHash(name);
				if (num1 <= 865021970)
				{
					if (num1 <= 614343423)
					{
						if (num1 != 42624449)
						{
							if (num1 == 614343423)
							{
								if (name == "nrHF")
								{
									((BaseUnit)base.MyObject).HorrorFactor = num;
								}
							}
						}
						else if (name == "nrPP")
						{
							((BaseUnit)base.MyObject).PsyPoints = num;
						}
					}
					else if (num1 == 715009137)
					{
						if (name == "nrHH")
						{
							((BaseUnit)base.MyObject).HandToHand = num;
						}
					}
					else if (num1 != 849230089)
					{
						if (num1 == 865021970)
						{
							if (name == "nrRW")
							{
								((BaseUnit)base.MyObject).RangedWeapons = num;
							}
						}
					}
					else if (name == "nrHP")
					{
						((BaseUnit)base.MyObject).HitPoints = num;
					}
				}
				else if (num1 <= 1096880591)
				{
					if (num1 == 1018139112)
					{
						if (name == "nrAP")
						{
							((BaseUnit)base.MyObject).ActionPoints = num;
						}
					}
					else if (num1 != 1083572302)
					{
						if (num1 == 1096880591)
						{
							if (name == "nrMGT")
							{
								((BaseUnit)base.MyObject).Might = num;
							}
						}
					}
					else if (name == "nrWP")
					{
						((BaseUnit)base.MyObject).Willpower = num;
					}
				}
				else if (num1 == 1198450135)
				{
					if (name == "nrT")
					{
						((BaseUnit)base.MyObject).Toughness = num;
					}
				}
				else if (num1 != 1271380208)
				{
					if (num1 == 1315893468)
					{
						if (name == "nrM")
						{
							((BaseUnit)base.MyObject).Movement = num;
						}
					}
				}
				else if (name == "nrAGIL")
				{
					((BaseUnit)base.MyObject).Agility = num;
				}
			}
		}

		private void txt_KeyPress(object sender, KeyPressEventArgs e)
		{
			bool flag;
			if (e.KeyChar > '9')
			{
				flag = true;
			}
			else
			{
				flag = (e.KeyChar >= '0' ? false : e.KeyChar != '\b');
			}
			if (flag)
			{
				e.Handled = true;
			}
		}

		private void UpdateStats()
		{
			Label mMod = this.MMod;
			int movementMod = ((BaseUnit)base.MyObject).MovementMod;
			mMod.Text = movementMod.ToString();
			Label mTot = this.MTot;
			movementMod = ((BaseUnit)base.MyObject).MovementTotal;
			mTot.Text = movementMod.ToString();
			Label hPMod = this.HPMod;
			movementMod = ((BaseUnit)base.MyObject).HitPointsMod;
			hPMod.Text = movementMod.ToString();
			Label hPTot = this.HPTot;
			movementMod = ((BaseUnit)base.MyObject).HitPointsTotal;
			hPTot.Text = movementMod.ToString();
			Label aPMod = this.APMod;
			movementMod = ((BaseUnit)base.MyObject).AttackPointsMod;
			aPMod.Text = movementMod.ToString();
			Label aPTot = this.APTot;
			movementMod = ((BaseUnit)base.MyObject).AttackPointsTotal;
			aPTot.Text = movementMod.ToString();
			Label hHMod = this.HHMod;
			movementMod = ((BaseUnit)base.MyObject).HandToHandMod;
			hHMod.Text = movementMod.ToString();
			Label hHTot = this.HHTot;
			movementMod = ((BaseUnit)base.MyObject).HandToHandTotal;
			hHTot.Text = movementMod.ToString();
			Label pPMod = this.PPMod;
			movementMod = ((BaseUnit)base.MyObject).PsyPointsMod;
			pPMod.Text = movementMod.ToString();
			Label pPTot = this.PPTot;
			movementMod = ((BaseUnit)base.MyObject).PsyPointsTotal;
			pPTot.Text = movementMod.ToString();
			Label rWMod = this.RWMod;
			movementMod = ((BaseUnit)base.MyObject).RangedWeaponsMod;
			rWMod.Text = movementMod.ToString();
			Label rWTot = this.RWTot;
			movementMod = ((BaseUnit)base.MyObject).RangedWeaponsTotal;
			rWTot.Text = movementMod.ToString();
			Label mTMod = this.MTMod;
			movementMod = ((BaseUnit)base.MyObject).MightMod;
			mTMod.Text = movementMod.ToString();
			Label mTTot = this.MTTot;
			movementMod = ((BaseUnit)base.MyObject).MightTotal;
			mTTot.Text = movementMod.ToString();
			Label tMod = this.TMod;
			movementMod = ((BaseUnit)base.MyObject).ToughnessMod;
			tMod.Text = movementMod.ToString();
			Label tTot = this.TTot;
			movementMod = ((BaseUnit)base.MyObject).ToughnessTotal;
			tTot.Text = movementMod.ToString();
			Label aGMod = this.AGMod;
			movementMod = ((BaseUnit)base.MyObject).AgilityMod;
			aGMod.Text = movementMod.ToString();
			Label aGTot = this.AGTot;
			movementMod = ((BaseUnit)base.MyObject).AgilityTotal;
			aGTot.Text = movementMod.ToString();
			Label wPMod = this.WPMod;
			movementMod = ((BaseUnit)base.MyObject).WillpowerMod;
			wPMod.Text = movementMod.ToString();
			Label wPTot = this.WPTot;
			movementMod = ((BaseUnit)base.MyObject).WillpowerTotal;
			wPTot.Text = movementMod.ToString();
			Label hFMod = this.HFMod;
			movementMod = ((BaseUnit)base.MyObject).HorrorFactorMod;
			hFMod.Text = movementMod.ToString();
			Label hFTot = this.HFTot;
			movementMod = ((BaseUnit)base.MyObject).HorrorFactorTotal;
			hFTot.Text = movementMod.ToString();
		}

		private void WeaponPermissionsChanged(WeaponPermission WPerm)
		{
			Console.WriteLine("Weapon Permissions changed, updating lists");
			this.lstWepRes.Items.Clear();
			this.lstWepRes.Items.AddRange(((BaseUnit)base.MyObject).WPermissions);
		}

		private void WeaponsChanged(Weapon weapon)
		{
			Console.WriteLine("Weapons changed, Updating List");
			this.lstWeapons.Items.Clear();
			this.lstWeapons.Items.AddRange(((BaseUnit)base.MyObject).Weapons);
		}
	}
}