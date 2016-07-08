using NTAF.Core;
using NTAF.Core.Properties;
using NTAF.Permissions;
using NTAF.PlugInFramework;
using NTAF.PrintEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml.Serialization;

namespace NTAF.ObjectClasses
{
	[ObjectClassPlugIn("BaseUnit", "0.0.0.0")]
	[Serializable]
	public class BaseUnit : ObjectClassBase
	{
		public const byte MAX_PERCENT_STAT = 12;

		public const byte MIN_PERCENT_STAT = 1;

		public const byte MAX_POINT_STAT = 255;

		public const byte MIN_POINT_STAT = 0;

		private byte _Movement;

		private byte _HitPoints;

		private byte _HandToHand;

		private byte _ActionPoints;

		private byte _PsyPoints;

		private byte _RangedWeapons;

		private byte _Might;

		private byte _Toughness;

		private byte _Agility;

		private byte _Willpower;

		private byte _HorrorFactor;

		private int _CostMod;

		private Race _Race;

		private Archetype _Archetype;

		private string _Description;

		private string _Special;

		internal List<WeaponPermission> _WPerms;

		internal List<SkillPermission> _SPerms;

		internal List<PsyPermission> _PPerms;

		internal List<Weapon> _Weapons;

		internal List<Skill> _Skills;

		internal List<Psy> _Psys;

		internal List<Item> _Item;

		[Browsable(false)]
		[XmlIgnore]
		public override string aboutMe
		{
			get
			{
				string str = base.aboutMe;
				str = string.Concat(str, string.Format("Race: {0}\n\n", this.BaseRace.Name));
				str = string.Concat(str, string.Format("Unit Type: {0}\n\n", this.archetype.Name));
				str = string.Concat(str, string.Format("Movement: {0}\n", this.MovementTotal));
				str = string.Concat(str, string.Format("HitPoints: {0}\n", this.HitPointsTotal));
				str = string.Concat(str, string.Format("HandToHand: {0}\n", this.HandToHandTotal));
				str = string.Concat(str, string.Format("AttackPoints: {0}\n", this.AttackPointsTotal));
				str = string.Concat(str, string.Format("PsyPoints: {0}\n", this.PsyPointsTotal));
				str = string.Concat(str, string.Format("RangedWeapons: {0}\n", this.RangedWeaponsTotal));
				str = string.Concat(str, string.Format("Might: {0}\n", this.MightTotal));
				str = string.Concat(str, string.Format("Toughness: {0}\n", this.ToughnessTotal));
				str = string.Concat(str, string.Format("Agility: {0}\n", this.AgilityTotal));
				str = string.Concat(str, string.Format("Willpower: {0}\n", this.WillpowerTotal));
				str = string.Concat(str, string.Format("HorrorFactor: {0}\n", this.HorrorFactorTotal));
				str = string.Concat(str, string.Format("\nDescription: \n{0}\n", GeneralOperations.WrapLength(this.Description, 50)));
				str = string.Concat(str, string.Format("\nCost: {0}\n", this.Cost));
				return str;
			}
		}

		[Category("Base Stats")]
		[Description("")]
		[XmlAttribute]
		public byte ActionPoints
		{
			get
			{
				return this._ActionPoints;
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				if (value < 0)
				{
					throw new StatException(StatsMod.Stats.AttackPoints, StatException.StatExceptionType.ToSmall, 0, 255);
				}
				this._ActionPoints = value;
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventStatChanged != null)
					{
						this.EventStatChanged(8);
					}
					if (this.EventCostChanged != null)
					{
						this.EventCostChanged();
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		[Category("Base Stats")]
		[Description("")]
		[XmlAttribute]
		public byte Agility
		{
			get
			{
				return this._Agility;
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				if (value > 12)
				{
					throw new StatException(StatsMod.Stats.Agility, StatException.StatExceptionType.ToLarge, 1, 12);
				}
				if (value < 1)
				{
					throw new StatException(StatsMod.Stats.Agility, StatException.StatExceptionType.ToSmall, 1, 12);
				}
				this._Agility = value;
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventStatChanged != null)
					{
						this.EventStatChanged(256);
					}
					if (this.EventCostChanged != null)
					{
						this.EventCostChanged();
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		[Category("Stat Modifiers")]
		[Description("")]
		[XmlIgnore]
		public int AgilityMod
		{
			get
			{
				return this.getStatMod(StatsMod.Stats.Agility);
			}
		}

		[Category("Stat Totals")]
		[Description("")]
		[XmlIgnore]
		public int AgilityTotal
		{
			get
			{
				return this.Agility + this.AgilityMod;
			}
		}

		[Category("Base")]
		[Description("")]
		public Archetype archetype
		{
			get
			{
				return this._Archetype;
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				this._Archetype = value;
				if (this.EventUnitTypeChanged != null)
				{
					this.EventUnitTypeChanged();
				}
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventCostChanged != null)
					{
						this.EventCostChanged();
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		[Category("Stat Modifiers")]
		[Description("")]
		[XmlIgnore]
		public int AttackPointsMod
		{
			get
			{
				return this.getStatMod(StatsMod.Stats.AttackPoints);
			}
		}

		[Category("Stat Totals")]
		[Description("")]
		[XmlIgnore]
		public int AttackPointsTotal
		{
			get
			{
				return this.ActionPoints + this.AttackPointsMod;
			}
		}

		[Category("Base")]
		[Description("")]
		public Race BaseRace
		{
			get
			{
				return this._Race;
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				if (this._Race != null)
				{
					this._Race = value;
					if (!Settings.Default.Loading)
					{
						this.ValidatePermAndItems();
					}
				}
				else
				{
					this._Race = value;
				}
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventRaceChanged != null)
					{
						this.EventRaceChanged();
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		public override Type CollectionType
		{
			get
			{
				return typeof(BaseUnit);
			}
		}

		[Category("Cost")]
		[Description("")]
		[XmlIgnore]
		public int Cost
		{
			get
			{
				int movement = 0;
				movement = movement + this.Movement * 2;
				movement = movement + this.HandToHand * 2;
				movement = movement + this.RangedWeapons * 3;
				movement = movement + this.Might * 3;
				movement = movement + this.Toughness * 4;
				movement = movement + this.HitPoints * 5;
				movement = movement + this.Agility;
				movement = movement + this.HorrorFactor * 10;
				movement = movement + this.PsyPoints * 10;
				movement = movement + this.Willpower;
				movement = movement + this.ActionPoints * 5;
				movement = movement + this.CostMod;
				Weapon[] weapons = this.Weapons;
				for (int i = 0; i < (int)weapons.Length; i++)
				{
					movement = movement + weapons[i].Cost;
				}
				Skill[] skillsAndAbilities = this.SkillsAndAbilities;
				for (int j = 0; j < (int)skillsAndAbilities.Length; j++)
				{
					movement = movement + skillsAndAbilities[j].Cost;
				}
				Item[] items = this.Items;
				for (int k = 0; k < (int)items.Length; k++)
				{
					movement = (int)(movement + items[k].Cost);
				}
				if (this.archetype != null)
				{
					movement = movement + this.archetype.CostModifier;
				}
				return movement;
			}
		}

		[Category("Cost")]
		[Description("")]
		[XmlAttribute]
		public int CostMod
		{
			get
			{
				return this._CostMod;
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				this._CostMod = value;
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventCostChanged != null)
					{
						this.EventCostChanged();
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		[Category("About")]
		[Description("")]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				this._Description = value;
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventDescriptionChanged != null)
					{
						this.EventDescriptionChanged();
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		[Category("Base Stats")]
		[Description("")]
		[XmlAttribute]
		public byte HandToHand
		{
			get
			{
				return this._HandToHand;
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				if (value > 12)
				{
					throw new StatException(StatsMod.Stats.HandToHand, StatException.StatExceptionType.ToLarge, 1, 12);
				}
				if (value < 1)
				{
					throw new StatException(StatsMod.Stats.HandToHand, StatException.StatExceptionType.ToSmall, 1, 12);
				}
				this._HandToHand = value;
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventStatChanged != null)
					{
						this.EventStatChanged(4);
					}
					if (this.EventCostChanged != null)
					{
						this.EventCostChanged();
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		[Category("Stat Modifiers")]
		[Description("")]
		[XmlIgnore]
		public int HandToHandMod
		{
			get
			{
				return this.getStatMod(StatsMod.Stats.HandToHand);
			}
		}

		[Category("Stat Totals")]
		[Description("")]
		[XmlIgnore]
		public int HandToHandTotal
		{
			get
			{
				return this.HandToHand + this.HandToHandMod;
			}
		}

		[Category("Base Stats")]
		[Description("")]
		[XmlAttribute]
		public byte HitPoints
		{
			get
			{
				return this._HitPoints;
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				if (value < 0)
				{
					throw new StatException(StatsMod.Stats.HitPoints, StatException.StatExceptionType.ToSmall, 0, 255);
				}
				this._HitPoints = value;
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventStatChanged != null)
					{
						this.EventStatChanged(2);
					}
					if (this.EventCostChanged != null)
					{
						this.EventCostChanged();
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		[Category("Stat Modifiers")]
		[Description("")]
		[XmlIgnore]
		public int HitPointsMod
		{
			get
			{
				return this.getStatMod(StatsMod.Stats.HitPoints);
			}
		}

		[Category("Stat Totals")]
		[Description("")]
		[XmlIgnore]
		public int HitPointsTotal
		{
			get
			{
				return this.HitPoints + this.HitPointsMod;
			}
		}

		[Category("Base Stats")]
		[Description("")]
		[XmlAttribute]
		public byte HorrorFactor
		{
			get
			{
				return this._HorrorFactor;
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				this._HorrorFactor = value;
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventStatChanged != null)
					{
						this.EventStatChanged(1024);
					}
					if (this.EventCostChanged != null)
					{
						this.EventCostChanged();
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		[Category("Stat Modifiers")]
		[Description("")]
		[XmlIgnore]
		public int HorrorFactorMod
		{
			get
			{
				return this.getStatMod(StatsMod.Stats.HorrorFactor);
			}
		}

		[Category("Stat Totals")]
		[Description("")]
		[XmlIgnore]
		public int HorrorFactorTotal
		{
			get
			{
				return this.HorrorFactor + this.HorrorFactorMod;
			}
		}

		[Category("Items/Abilities")]
		[Description("")]
		public Item[] Items
		{
			get
			{
				return this._Item.ToArray();
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				if (this._Item == null)
				{
					this._Item = new List<Item>();
				}
				this._Item.Clear();
				this._Item.AddRange(value);
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventItemsChanged != null)
					{
						this.EventItemsChanged(null);
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		[Category("Base Stats")]
		[Description("")]
		[XmlAttribute]
		public byte Might
		{
			get
			{
				return this._Might;
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				if (value > 12)
				{
					throw new StatException(StatsMod.Stats.Might, StatException.StatExceptionType.ToLarge, 1, 12);
				}
				if (value < 1)
				{
					throw new StatException(StatsMod.Stats.Might, StatException.StatExceptionType.ToSmall, 1, 12);
				}
				this._Might = value;
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventStatChanged != null)
					{
						this.EventStatChanged(64);
					}
					if (this.EventCostChanged != null)
					{
						this.EventCostChanged();
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		[Category("Stat Modifiers")]
		[Description("")]
		[XmlIgnore]
		public int MightMod
		{
			get
			{
				return this.getStatMod(StatsMod.Stats.Might);
			}
		}

		[Category("Stat Totals")]
		[Description("")]
		[XmlIgnore]
		public int MightTotal
		{
			get
			{
				return this.Might + this.MightMod;
			}
		}

		[Category("Base Stats")]
		[Description("")]
		[XmlAttribute]
		public byte Movement
		{
			get
			{
				return this._Movement;
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				if (value < 0)
				{
					throw new StatException(StatsMod.Stats.Movement, StatException.StatExceptionType.ToSmall, 0, 255);
				}
				this._Movement = value;
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventStatChanged != null)
					{
						this.EventStatChanged(1);
					}
					if (this.EventCostChanged != null)
					{
						this.EventCostChanged();
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		[Category("Stat Modifiers")]
		[Description("")]
		[XmlIgnore]
		public int MovementMod
		{
			get
			{
				return this.getStatMod(StatsMod.Stats.Movement);
			}
		}

		[Category("Stat Totals")]
		[Description("")]
		[XmlIgnore]
		public int MovementTotal
		{
			get
			{
				return this.Movement + this.MovementMod;
			}
		}

		[Browsable(false)]
		[XmlIgnore]
		public WSPPermission[] permissions
		{
			get
			{
				List<WSPPermission> wSPPermissions = new List<WSPPermission>();
				wSPPermissions.AddRange(this.WPermissions);
				wSPPermissions.AddRange(this.SPermissions);
				wSPPermissions.AddRange(this.PPermissions);
				return wSPPermissions.ToArray();
			}
		}

		[Category("Permissions")]
		[Description("")]
		public PsyPermission[] PPermissions
		{
			get
			{
				return this._PPerms.ToArray();
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				if (this._PPerms == null)
				{
					this._PPerms = new List<PsyPermission>();
				}
				this._PPerms.Clear();
				this._PPerms.AddRange(value);
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventPsyPermissionsChanged != null)
					{
						this.EventPsyPermissionsChanged(null);
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		[Category("Base Stats")]
		[Description("")]
		[XmlAttribute]
		public byte PsyPoints
		{
			get
			{
				return this._PsyPoints;
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				if (value < 0)
				{
					throw new StatException(StatsMod.Stats.PsyPoints, StatException.StatExceptionType.ToSmall, 0, 255);
				}
				this._PsyPoints = value;
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventStatChanged != null)
					{
						this.EventStatChanged(16);
					}
					if (this.EventCostChanged != null)
					{
						this.EventCostChanged();
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		[Category("Stat Modifiers")]
		[Description("")]
		[XmlIgnore]
		public int PsyPointsMod
		{
			get
			{
				return this.getStatMod(StatsMod.Stats.PsyPoints);
			}
		}

		[Category("Stat Totals")]
		[Description("")]
		[XmlIgnore]
		public int PsyPointsTotal
		{
			get
			{
				return this.PsyPoints + this.PsyPointsMod;
			}
		}

		[Category("Items/Abilities")]
		[Description("")]
		public Psy[] Psys
		{
			get
			{
				return this._Psys.ToArray();
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				if (this._Psys == null)
				{
					this._Psys = new List<Psy>();
				}
				this._Psys.Clear();
				this._Psys.AddRange(value);
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventPsysChanged != null)
					{
						this.EventPsysChanged(null);
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		[Category("Base Stats")]
		[Description("")]
		[XmlAttribute]
		public byte RangedWeapons
		{
			get
			{
				return this._RangedWeapons;
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				if (value > 12)
				{
					throw new StatException(StatsMod.Stats.RangedWeapons, StatException.StatExceptionType.ToLarge, 1, 12);
				}
				if (value < 1)
				{
					throw new StatException(StatsMod.Stats.RangedWeapons, StatException.StatExceptionType.ToSmall, 1, 12);
				}
				this._RangedWeapons = value;
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventStatChanged != null)
					{
						this.EventStatChanged(32);
					}
					if (this.EventCostChanged != null)
					{
						this.EventCostChanged();
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		[Category("Stat Modifiers")]
		[Description("")]
		[XmlIgnore]
		public int RangedWeaponsMod
		{
			get
			{
				return this.getStatMod(StatsMod.Stats.RangedWeapons);
			}
		}

		[Category("Stat Totals")]
		[Description("")]
		[XmlIgnore]
		public int RangedWeaponsTotal
		{
			get
			{
				return this.RangedWeapons + this.RangedWeaponsMod;
			}
		}

		[Category("Items/Abilities")]
		[Description("")]
		public Skill[] SkillsAndAbilities
		{
			get
			{
				return this._Skills.ToArray();
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				if (this._Skills == null)
				{
					this._Skills = new List<Skill>();
				}
				this._Skills.Clear();
				this._Skills.AddRange(value);
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventSkillsChanged != null)
					{
						this.EventSkillsChanged(null);
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		[Category("About")]
		[Description("")]
		public string Special
		{
			get
			{
				return this._Special;
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				this._Special = value;
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventSpaecialChanged != null)
					{
						this.EventSpaecialChanged();
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		[Category("Permissions")]
		[Description("")]
		public SkillPermission[] SPermissions
		{
			get
			{
				return this._SPerms.ToArray();
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				if (this._SPerms == null)
				{
					this._SPerms = new List<SkillPermission>();
				}
				this._SPerms.Clear();
				this._SPerms.AddRange(value);
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventSkillPermissionsChanged != null)
					{
						this.EventSkillPermissionsChanged(null);
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		[Category("Experience")]
		[Description("")]
		[XmlAttribute]
		public string StartingEXP
		{
			get
			{
				string startingEXP = "";
				if (this.archetype != null)
				{
					startingEXP = this.archetype.StartingEXP;
				}
				return startingEXP;
			}
		}

		[Category("Base Stats")]
		[Description("")]
		[XmlAttribute]
		public byte Toughness
		{
			get
			{
				return this._Toughness;
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				if (value > 12)
				{
					throw new StatException(StatsMod.Stats.Toughness, StatException.StatExceptionType.ToLarge, 1, 12);
				}
				if (value < 1)
				{
					throw new StatException(StatsMod.Stats.Toughness, StatException.StatExceptionType.ToSmall, 1, 12);
				}
				this._Toughness = value;
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventStatChanged != null)
					{
						this.EventStatChanged(128);
					}
					if (this.EventCostChanged != null)
					{
						this.EventCostChanged();
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		[Category("Stat Modifiers")]
		[Description("")]
		[XmlIgnore]
		public int ToughnessMod
		{
			get
			{
				return this.getStatMod(StatsMod.Stats.Toughness);
			}
		}

		[Category("Stat Totals")]
		[Description("")]
		[XmlIgnore]
		public int ToughnessTotal
		{
			get
			{
				return this.Toughness + this.ToughnessMod;
			}
		}

		[Category("Items/Abilities")]
		[Description("")]
		public Weapon[] Weapons
		{
			get
			{
				return this._Weapons.ToArray();
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				if (this._Weapons == null)
				{
					this._Weapons = new List<Weapon>();
				}
				this._Weapons.Clear();
				this._Weapons.AddRange(value);
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventWeaponsChanged != null)
					{
						this.EventWeaponsChanged(null);
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		[Category("Base Stats")]
		[Description("")]
		[XmlAttribute]
		public byte Willpower
		{
			get
			{
				return this._Willpower;
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				if (value > 12)
				{
					throw new StatException(StatsMod.Stats.Willpower, StatException.StatExceptionType.ToLarge, 1, 12);
				}
				if (value < 1)
				{
					throw new StatException(StatsMod.Stats.Willpower, StatException.StatExceptionType.ToSmall, 1, 12);
				}
				this._Willpower = value;
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventStatChanged != null)
					{
						this.EventStatChanged(512);
					}
					if (this.EventCostChanged != null)
					{
						this.EventCostChanged();
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		[Category("Stat Modifiers")]
		[Description("")]
		[XmlIgnore]
		public int WillpowerMod
		{
			get
			{
				return this.getStatMod(StatsMod.Stats.Willpower);
			}
		}

		[Category("Stat Totals")]
		[Description("")]
		[XmlIgnore]
		public int WillpowerTotal
		{
			get
			{
				return this.Willpower + this.WillpowerMod;
			}
		}

		[Category("Permissions")]
		[Description("")]
		public WeaponPermission[] WPermissions
		{
			get
			{
				return this._WPerms.ToArray();
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				if (this._WPerms == null)
				{
					this._WPerms = new List<WeaponPermission>();
				}
				this._WPerms.Clear();
				this._WPerms.AddRange(value);
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventWeaponPermissionsChanged != null)
					{
						this.EventWeaponPermissionsChanged(null);
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		public BaseUnit()
		{
			base.Name = "";
			base.ID = "";
			this.archetype = new Archetype();
			this.BaseRace = new Race();
			this.Movement = 1;
			this.HitPoints = 1;
			this.HandToHand = 1;
			this.ActionPoints = 1;
			this.PsyPoints = 0;
			this.RangedWeapons = 1;
			this.Might = 1;
			this.Toughness = 1;
			this.Agility = 1;
			this.Willpower = 1;
			this.HorrorFactor = 0;
			this.CostMod = 0;
			this.Description = "";
			this.Special = "";
			this._WPerms = new List<WeaponPermission>();
			this._SPerms = new List<SkillPermission>();
			this._PPerms = new List<PsyPermission>();
			this._Weapons = new List<Weapon>();
			this._Skills = new List<Skill>();
			this._Psys = new List<Psy>();
			this._Item = new List<Item>();
		}

		public BaseUnit(BaseUnit var)
		{
			base.Name = var.Name;
			base.ID = var.ID;
			this.archetype = var.archetype;
			this.BaseRace = var.BaseRace;
			this.Movement = var.Movement;
			this.HitPoints = var.HitPoints;
			this.HandToHand = var.HandToHand;
			this.ActionPoints = var.ActionPoints;
			this.PsyPoints = var.PsyPoints;
			this.RangedWeapons = var.RangedWeapons;
			this.Might = var.Might;
			this.Toughness = var.Toughness;
			this.Agility = var.Agility;
			this.Willpower = var.Willpower;
			this.HorrorFactor = var.HorrorFactor;
			this.CostMod = var.CostMod;
			this.Description = var.Description;
			this.Special = var.Special;
			this.WPermissions = var.WPermissions;
			this.SPermissions = var.SPermissions;
			this.PPermissions = var.PPermissions;
			this.Weapons = var.Weapons;
			this.SkillsAndAbilities = var.SkillsAndAbilities;
			this.Psys = var.Psys;
			this.Items = var.Items;
		}

		public BaseUnit(string buName, string buBaseID, Archetype buBaseType, Race buBaseRace, byte buMovement, byte buHitPoints, byte buHandToHand, byte buAttackPoints, byte buPsyPoints, byte buRangedWeapons, byte buMight, byte buToughness, byte buAgility, byte buWillpower, int buCostMod, byte buHorrorFactor, string buStartingEXP, string buDescription, string buSpecial, WeaponPermission[] buWPermissions, SkillPermission[] buSPermissions, PsyPermission[] buPPermissions, Weapon[] buWeapons, Skill[] buSkillsAndAbilities, Psy[] buPsys, Item[] buItems)
		{
			base.Name = buName;
			base.ID = buBaseID;
			this.archetype = buBaseType;
			this.BaseRace = buBaseRace;
			this.Movement = buMovement;
			this.HitPoints = buHitPoints;
			this.HandToHand = buHandToHand;
			this.ActionPoints = buAttackPoints;
			this.PsyPoints = buPsyPoints;
			this.RangedWeapons = buRangedWeapons;
			this.Might = buMight;
			this.Toughness = buToughness;
			this.Agility = buAgility;
			this.Willpower = buWillpower;
			this.HorrorFactor = buHorrorFactor;
			this.CostMod = buCostMod;
			this.Description = buDescription;
			this.Special = buSpecial;
			this.WPermissions = buWPermissions;
			this.SPermissions = buSPermissions;
			this.PPermissions = buPPermissions;
			this.Weapons = buWeapons;
			this.SkillsAndAbilities = buSkillsAndAbilities;
			this.Psys = buPsys;
			this.Items = buItems;
		}

		public void AddItem(Item item)
		{
			this._Item.Add(item);
			if (!Settings.Default.Loading)
			{
				if (this.EventItemsChanged != null)
				{
					this.EventItemsChanged(item);
				}
				if (this.EventCostChanged != null)
				{
					this.EventCostChanged();
				}
			}
		}

		public void AddPsy(Psy psy)
		{
			this._Psys.Add(psy);
			if (!Settings.Default.Loading)
			{
				if (this.EventPsysChanged != null)
				{
					this.EventPsysChanged(psy);
				}
				if (this.EventCostChanged != null)
				{
					this.EventCostChanged();
				}
			}
		}

		public void AddPsyPermission(PsyPermission PPerm)
		{
			if (this.PsyPermissionExists(PPerm))
			{
				throw new PermissionException("This permission already exists in the current list, no need to add it twice...");
			}
			this._PPerms.Add(PPerm);
			if (!Settings.Default.Loading && this.EventPsyPermissionsChanged != null)
			{
				this.EventPsyPermissionsChanged(PPerm);
			}
		}

		internal void AddRangeItems(Item[] items)
		{
			Item[] itemArray = items;
			for (int i = 0; i < (int)itemArray.Length; i++)
			{
				this.AddItem(itemArray[i]);
			}
		}

		internal void AddRangePPerms(PsyPermission[] perms)
		{
			PsyPermission[] psyPermissionArray = perms;
			for (int i = 0; i < (int)psyPermissionArray.Length; i++)
			{
				this.AddPsyPermission(psyPermissionArray[i]);
			}
		}

		internal void AddRangePsys(Psy[] items)
		{
			Psy[] psyArray = items;
			for (int i = 0; i < (int)psyArray.Length; i++)
			{
				this.AddPsy(psyArray[i]);
			}
		}

		internal void AddRangeSkills(Skill[] items)
		{
			Skill[] skillArray = items;
			for (int i = 0; i < (int)skillArray.Length; i++)
			{
				this.AddSkill(skillArray[i]);
			}
		}

		internal void AddRangeSPerms(SkillPermission[] perms)
		{
			SkillPermission[] skillPermissionArray = perms;
			for (int i = 0; i < (int)skillPermissionArray.Length; i++)
			{
				this.AddSkillPermission(skillPermissionArray[i]);
			}
		}

		internal void AddRangeWeapons(Weapon[] items)
		{
			Weapon[] weaponArray = items;
			for (int i = 0; i < (int)weaponArray.Length; i++)
			{
				this.AddWeapon(weaponArray[i]);
			}
		}

		internal void AddRangeWPerms(WeaponPermission[] perms)
		{
			WeaponPermission[] weaponPermissionArray = perms;
			for (int i = 0; i < (int)weaponPermissionArray.Length; i++)
			{
				this.AddWeaponPermission(weaponPermissionArray[i]);
			}
		}

		public void AddSkill(Skill Skil)
		{
			if (this.SkillExists(Skil))
			{
				throw new ItemException("that item already exists in the lists");
			}
			this._Skills.Add(Skil);
			if (!Settings.Default.Loading)
			{
				if (this.EventSkillsChanged != null)
				{
					this.EventSkillsChanged(Skil);
				}
				if (this.EventCostChanged != null)
				{
					this.EventCostChanged();
				}
			}
		}

		public void AddSkillPermission(SkillPermission SPerm)
		{
			if (this.SkillPermissionExists(SPerm))
			{
				throw new PermissionException("This permission already exists in the current list, no need to add it twice...");
			}
			this._SPerms.Add(SPerm);
			if (!Settings.Default.Loading && this.EventSkillPermissionsChanged != null)
			{
				this.EventSkillPermissionsChanged(SPerm);
			}
		}

		public void AddWeapon(Weapon Wepon)
		{
			this._Weapons.Add(Wepon);
			if (!Settings.Default.Loading)
			{
				if (this.EventWeaponsChanged != null)
				{
					this.EventWeaponsChanged(Wepon);
				}
				if (this.EventCostChanged != null)
				{
					this.EventCostChanged();
				}
			}
		}

		public void AddWeaponPermission(WeaponPermission WPerm)
		{
			if (this.WeaponPermissionExists(WPerm))
			{
				throw new PermissionException("This item already exists in the current list, no need to add it twice...");
			}
			this._WPerms.Add(WPerm);
			if (!Settings.Default.Loading && this.EventWeaponPermissionsChanged != null)
			{
				this.EventWeaponPermissionsChanged(WPerm);
			}
		}

		public override bool CheckForReferences(ObjectClassBase item)
		{
			bool flag;
			if (this.archetype == item)
			{
				flag = true;
			}
			else if (this.BaseRace == item)
			{
				flag = true;
			}
			else if (item is Item && this._Item.Contains((Item)item))
			{
				flag = true;
			}
			else if (item is SkillPermission && this._SPerms.Contains((SkillPermission)item))
			{
				flag = true;
			}
			else if (!(item is WeaponPermission) || !this._WPerms.Contains((WeaponPermission)item))
			{
				flag = (!(item is PsyPermission) || !this._PPerms.Contains((PsyPermission)item) ? false : true);
			}
			else
			{
				flag = true;
			}
			return flag;
		}

		private bool checkItem(Item item)
		{
			bool flag = false;
			bool flag1 = false;
			if (item.TypesCanUse == ArchetypeBaseEnu.All)
			{
				flag = true;
			}
			else if (this.CheckUnitTypeFlag(item.TypesCanUse, this.archetype.BaseType))
			{
				flag = true;
			}
			if ((int)item.SpeciesCanEquip != 0)
			{
				if (this.CheckRaceFlag(item.SpeciesCanEquip, this.BaseRace.species))
				{
					flag1 = true;
				}
				else if (item.RaceCanEquip == this.BaseRace)
				{
					flag1 = true;
				}
			}
			return flag & flag1;
		}

		private bool CheckRaceFlag(Species FlagsToLookFor, Species MyFlag)
		{
			bool flag = false;
			List<Species> species = new List<Species>();
			foreach (Species list in GeneralOperations.EnumToList<Species>())
			{
				if (!(list.Is<Species>(FlagsToLookFor) & list != Species.All))
				{
					continue;
				}
				species.Add(list);
			}
			foreach (Species species1 in species)
			{
				if (!species1.Is<Species>(MyFlag))
				{
					continue;
				}
				flag = true;
			}
			return flag;
		}

		private bool CheckUnitTypeFlag(ArchetypeBaseEnu FlagsToLookFor, ArchetypeBaseEnu MyFlag)
		{
			bool flag = false;
			List<ArchetypeBaseEnu> archetypeBaseEnus = new List<ArchetypeBaseEnu>();
			foreach (ArchetypeBaseEnu list in GeneralOperations.EnumToList<ArchetypeBaseEnu>())
			{
				if (!list.Is<ArchetypeBaseEnu>(FlagsToLookFor))
				{
					continue;
				}
				archetypeBaseEnus.Add(list);
			}
			foreach (ArchetypeBaseEnu archetypeBaseEnu in archetypeBaseEnus)
			{
				if (!archetypeBaseEnu.Is<ArchetypeBaseEnu>(MyFlag))
				{
					continue;
				}
				flag = true;
			}
			return flag;
		}

		internal void clearItems()
		{
			this._Item.Clear();
		}

		protected override void clearMyEvents()
		{
			base.clearMyEvents();
			this.EventCostChanged = null;
			this.EventDescriptionChanged = null;
			this.EventItemsChanged = null;
			this.MyDataChanged = null;
			this.EventPsyPermissionsChanged = null;
			this.EventPsysChanged = null;
			this.EventRaceChanged = null;
			this.EventSkillPermissionsChanged = null;
			this.EventSkillsChanged = null;
			this.EventSpaecialChanged = null;
			this.EventStatChanged = null;
			this.EventUnitTypeChanged = null;
			this.EventWeaponPermissionsChanged = null;
			this.EventWeaponsChanged = null;
		}

		internal void clearPPermissions()
		{
			this._PPerms.Clear();
		}

		internal void clearPsys()
		{
			this._Psys.Clear();
		}

		internal void clearSkills()
		{
			this._Skills.Clear();
		}

		internal void clearSPermissions()
		{
			this._SPerms.Clear();
		}

		internal void clearWeapons()
		{
			this._Weapons.Clear();
		}

		internal void clearWPermissions()
		{
			this._WPerms.Clear();
		}

		public void DropItem(Item item)
		{
			if (!this.ItemExists(item))
			{
				throw new ItemException("Could not find the item in the current list");
			}
			this._Item.Remove(item);
			if (this.EventItemsChanged != null)
			{
				this.EventItemsChanged(item);
			}
			if (this.EventCostChanged != null)
			{
				this.EventCostChanged();
			}
		}

		public void DropPsy(Psy psy)
		{
			if (!this.PsyExists(psy))
			{
				throw new ItemException("Could not find the item in the current list");
			}
			this._Psys.Remove(psy);
			if (this.EventPsysChanged != null)
			{
				this.EventPsysChanged(psy);
			}
			if (this.EventCostChanged != null)
			{
				this.EventCostChanged();
			}
		}

		public void DropPsyPermission(PsyPermission PPerm)
		{
			if (!this.PsyPermissionExists(PPerm))
			{
				throw new PermissionException("Could not find the item in the current list");
			}
			this._PPerms.Remove(PPerm);
			Console.WriteLine("Perm removed in class");
			this.ValidatePermAndItems();
			if (this.EventPsyPermissionsChanged != null)
			{
				this.EventPsyPermissionsChanged(PPerm);
			}
		}

		public void DropSkill(Skill Skil)
		{
			if (!this.SkillExists(Skil))
			{
				throw new ItemException("Could not find the item in the current list");
			}
			this._Skills.Remove(Skil);
			if (this.EventWeaponsChanged != null)
			{
				this.EventSkillsChanged(Skil);
			}
			if (this.EventCostChanged != null)
			{
				this.EventCostChanged();
			}
		}

		public void DropSkillPermission(SkillPermission SPerm)
		{
			if (!this.SkillPermissionExists(SPerm))
			{
				throw new PermissionException("Could not find the item in the current list");
			}
			this._SPerms.Remove(SPerm);
			Console.WriteLine("Perm removed in class");
			this.ValidatePermAndItems();
			if (this.EventSkillPermissionsChanged != null)
			{
				this.EventSkillPermissionsChanged(SPerm);
			}
		}

		public void DropWeapon(Weapon Wepon)
		{
			if (!this.WeaponExists(Wepon))
			{
				throw new ItemException("Could not find the item in the current list");
			}
			this._Weapons.Remove(Wepon);
			if (this.EventWeaponsChanged != null)
			{
				this.EventWeaponsChanged(Wepon);
			}
			if (this.EventCostChanged != null)
			{
				this.EventCostChanged();
			}
		}

		public void DropWeaponPermission(WeaponPermission WPerm)
		{
			if (!this.WeaponPermissionExists(WPerm))
			{
				throw new PermissionException("Could not find the item in the current list");
			}
			this._WPerms.Remove(WPerm);
			Console.WriteLine("Perm removed in class");
			this.ValidatePermAndItems();
			if (this.EventWeaponPermissionsChanged != null)
			{
				this.EventWeaponPermissionsChanged(WPerm);
			}
		}

		public override bool Equals(object obj)
		{
			bool d = true;
			if ((obj == null ? true : !(obj is BaseUnit)))
			{
				d = false;
			}
			if (d)
			{
				d = base.ID == ((BaseUnit)obj).ID;
			}
			if (d)
			{
				d = base.Name == ((BaseUnit)obj).Name;
			}
			if (d)
			{
				d = this.MovementTotal == ((BaseUnit)obj).MovementTotal;
			}
			if (d)
			{
				d = this.HitPointsTotal == ((BaseUnit)obj).HitPointsTotal;
			}
			if (d)
			{
				d = this.AttackPointsTotal == ((BaseUnit)obj).AttackPointsTotal;
			}
			if (d)
			{
				d = this.HandToHandTotal == ((BaseUnit)obj).HandToHandTotal;
			}
			if (d)
			{
				d = this.PsyPointsTotal == ((BaseUnit)obj).PsyPointsTotal;
			}
			if (d)
			{
				d = this.RangedWeaponsTotal == ((BaseUnit)obj).RangedWeaponsTotal;
			}
			if (d)
			{
				d = this.MightTotal == ((BaseUnit)obj).MightTotal;
			}
			if (d)
			{
				d = this.ToughnessTotal == ((BaseUnit)obj).ToughnessTotal;
			}
			if (d)
			{
				d = this.AgilityTotal == ((BaseUnit)obj).AgilityTotal;
			}
			if (d)
			{
				d = this.WillpowerTotal == ((BaseUnit)obj).WillpowerTotal;
			}
			if (d)
			{
				d = this.HorrorFactorTotal == ((BaseUnit)obj).HorrorFactorTotal;
			}
			if (d)
			{
				d = this.BaseRace == ((BaseUnit)obj).BaseRace;
			}
			if (d)
			{
				d = this.Description == ((BaseUnit)obj).Description;
			}
			if (d)
			{
				d = this.Special == ((BaseUnit)obj).Special;
			}
			if (d)
			{
				if (this.WPermissions.Count<WeaponPermission>() != ((BaseUnit)obj).WPermissions.Count<WeaponPermission>())
				{
					d = false;
				}
				else
				{
					WeaponPermission[] wPermissions = ((BaseUnit)obj).WPermissions;
					int num = 0;
					while (num < (int)wPermissions.Length)
					{
						WeaponPermission weaponPermission = wPermissions[num];
						if (this.WPermissions.Contains<WeaponPermission>(weaponPermission))
						{
							num++;
						}
						else
						{
							d = false;
							break;
						}
					}
				}
			}
			if (d)
			{
				if (this.SPermissions.Count<SkillPermission>() != ((BaseUnit)obj).SPermissions.Count<SkillPermission>())
				{
					d = false;
				}
				else
				{
					SkillPermission[] sPermissions = ((BaseUnit)obj).SPermissions;
					int num1 = 0;
					while (num1 < (int)sPermissions.Length)
					{
						SkillPermission skillPermission = sPermissions[num1];
						if (this.SPermissions.Contains<SkillPermission>(skillPermission))
						{
							num1++;
						}
						else
						{
							d = false;
							break;
						}
					}
				}
			}
			if (d)
			{
				if (this.PPermissions.Count<PsyPermission>() != ((BaseUnit)obj).PPermissions.Count<PsyPermission>())
				{
					d = false;
				}
				else
				{
					PsyPermission[] pPermissions = ((BaseUnit)obj).PPermissions;
					int num2 = 0;
					while (num2 < (int)pPermissions.Length)
					{
						PsyPermission psyPermission = pPermissions[num2];
						if (this.PPermissions.Contains<PsyPermission>(psyPermission))
						{
							num2++;
						}
						else
						{
							d = false;
							break;
						}
					}
				}
			}
			if (d)
			{
				if (this.Weapons.Count<Weapon>() != ((BaseUnit)obj).Weapons.Count<Weapon>())
				{
					d = false;
				}
				else
				{
					Weapon[] weapons = ((BaseUnit)obj).Weapons;
					int num3 = 0;
					while (num3 < (int)weapons.Length)
					{
						Weapon weapon = weapons[num3];
						if (this.Weapons.Contains<Weapon>(weapon))
						{
							num3++;
						}
						else
						{
							d = false;
							break;
						}
					}
				}
			}
			if (d)
			{
				if (this.SkillsAndAbilities.Count<Skill>() != ((BaseUnit)obj).SkillsAndAbilities.Count<Skill>())
				{
					d = false;
				}
				else
				{
					Skill[] skillsAndAbilities = ((BaseUnit)obj).SkillsAndAbilities;
					int num4 = 0;
					while (num4 < (int)skillsAndAbilities.Length)
					{
						Skill skill = skillsAndAbilities[num4];
						if (this.SkillsAndAbilities.Contains<Skill>(skill))
						{
							num4++;
						}
						else
						{
							d = false;
							break;
						}
					}
				}
			}
			if (d)
			{
				if (this.Psys.Count<Psy>() != ((BaseUnit)obj).Psys.Count<Psy>())
				{
					d = false;
				}
				else
				{
					Psy[] psys = ((BaseUnit)obj).Psys;
					int num5 = 0;
					while (num5 < (int)psys.Length)
					{
						Psy psy = psys[num5];
						if (this.Psys.Contains<Psy>(psy))
						{
							num5++;
						}
						else
						{
							d = false;
							break;
						}
					}
				}
			}
			if (d)
			{
				if (this.Items.Count<Item>() != ((BaseUnit)obj).Items.Count<Item>())
				{
					d = false;
				}
				else
				{
					Item[] items = ((BaseUnit)obj).Items;
					int num6 = 0;
					while (num6 < (int)items.Length)
					{
						Item item = items[num6];
						if (this.Items.Contains<Item>(item))
						{
							num6++;
						}
						else
						{
							d = false;
							break;
						}
					}
				}
			}
			return d;
		}

		public T[] getAllowedPermissions<T>(T[] LoadedPermissions)
		{
			if (this.BaseRace.ID == "")
			{
				throw new RaceException("Race hasnot ben set");
			}
			List<T> ts = new List<T>();
			T[] loadedPermissions = LoadedPermissions;
			for (int i = 0; i < (int)loadedPermissions.Length; i++)
			{
				T t = loadedPermissions[i];
				if (this.PermissionFilter(t))
				{
					ts.Add(t);
				}
			}
			return ts.ToArray();
		}

		public Skill[] getAvalableSkills(Skill[] skills)
		{
			if (this._SPerms.Count <= 0)
			{
				throw new PermissionException("You have not selected any skillAbility permissions");
			}
			List<Skill> skills1 = new List<Skill>();
			Skill[] skillArray = skills;
			for (int i = 0; i < (int)skillArray.Length; i++)
			{
				Skill skill = skillArray[i];
				foreach (SkillPermission _SPerm in this._SPerms)
				{
					if (_SPerm != skill.RequiresPermission)
					{
						continue;
					}
					skills1.Add(skill);
				}
			}
			return skills1.ToArray();
		}

		public Weapon[] getAvalableWeapons(Weapon[] weapons)
		{
			if (this._WPerms.Count <= 0)
			{
				throw new PermissionException("You have not selected any weapon permissions");
			}
			List<Weapon> weapons1 = new List<Weapon>();
			Weapon[] weaponArray = weapons;
			for (int i = 0; i < (int)weaponArray.Length; i++)
			{
				Weapon weapon = weaponArray[i];
				foreach (WeaponPermission _WPerm in this._WPerms)
				{
					if (_WPerm.ID != weapon.RequiresPermission.ID)
					{
						continue;
					}
					weapons1.Add(weapon);
				}
			}
			return weapons1.ToArray();
		}

		public Item[] getAvalablItems(Item[] items)
		{
			if (this.BaseRace.ID == "")
			{
				throw new RaceException("You have not selected a race");
			}
			if (this.archetype.ID == "")
			{
				throw new RaceException("You have not selected a Unit Type");
			}
			List<Item> items1 = new List<Item>();
			Item[] itemArray = items;
			for (int i = 0; i < (int)itemArray.Length; i++)
			{
				Item item = itemArray[i];
				if (this.checkItem(item))
				{
					items1.Add(item);
				}
			}
			return items1.ToArray();
		}

		public Psy[] getAvalablPsys(Psy[] psys)
		{
			if (this._PPerms.Count <= 0)
			{
				throw new PermissionException("You have not selected any psy permissions");
			}
			List<Psy> psies = new List<Psy>();
			Psy[] psyArray = psys;
			for (int i = 0; i < (int)psyArray.Length; i++)
			{
				Psy psy = psyArray[i];
				foreach (PsyPermission _PPerm in this._PPerms)
				{
					if (_PPerm.ID != psy.RequiresPermission.ID)
					{
						continue;
					}
					psies.Add(psy);
				}
			}
			return psies.ToArray();
		}

		public override DataMember[] getDataMembers()
		{
			string str = "";
			foreach (WeaponPermission _WPerm in this._WPerms)
			{
				str = string.Concat(str, _WPerm.Name, ", ");
			}
			str = str.TrimEnd(new char[] { ',', ' ' });
			string str1 = "";
			foreach (SkillPermission _SPerm in this._SPerms)
			{
				str1 = string.Concat(str1, _SPerm.Name, ", ");
			}
			str1 = str1.TrimEnd(new char[] { ',', ' ' });
			string str2 = "";
			foreach (PsyPermission _PPerm in this._PPerms)
			{
				str2 = string.Concat(str2, _PPerm.Name, ", ");
			}
			str2 = str2.TrimEnd(new char[] { ',', ' ' });
			string str3 = "";
			foreach (Weapon _Weapon in this._Weapons)
			{
				str3 = string.Concat(str3, _Weapon.Name, ", ");
			}
			str3 = str3.TrimEnd(new char[] { ',', ' ' });
			string str4 = "";
			foreach (Skill _Skill in this._Skills)
			{
				str4 = string.Concat(str4, _Skill.Name, ", ");
			}
			str4 = str4.TrimEnd(new char[] { ',', ' ' });
			string str5 = "";
			foreach (Psy _Psy in this._Psys)
			{
				str5 = string.Concat(str5, _Psy.Name, ", ");
			}
			str5 = str5.TrimEnd(new char[] { ',', ' ' });
			string str6 = "";
			foreach (Item item in this._Item)
			{
				str6 = string.Concat(str6, item.Name, ", ");
			}
			str6 = str6.TrimEnd(new char[] { ',', ' ' });
			return new DataMember[] { new DataMember("Name", base.Name), new DataMember("Description", this._Race.Name), new DataMember("Description", this._Archetype.Name), new DataMember("Description", this.Description), new DataMember("Movement", (object)this.Movement), new DataMember("HitPoints", (object)this.HitPoints), new DataMember("HandToHand", (object)this.HandToHand), new DataMember("ActionPoints", (object)this.ActionPoints), new DataMember("PsyPoints", (object)this.PsyPoints), new DataMember("RangedWeapons", (object)this.RangedWeapons), new DataMember("Might", (object)this.Might), new DataMember("Toughness", (object)this.Toughness), new DataMember("Agility", (object)this.Agility), new DataMember("Willpower", (object)this.Willpower), new DataMember("HorrorFactor", (object)this.HorrorFactor), new DataMember("Weapon Permissions", str), new DataMember("Skill Permissions", str1), new DataMember("Psy Permissions", str2), new DataMember("Weapons", str3), new DataMember("Skills", str4), new DataMember("Psy Ablilites", str5), new DataMember("Items", str6), new DataMember("Special", this.Special), new DataMember("Cost Modifier", (object)this.CostMod), new DataMember("Cost", (object)this.Cost) };
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		private int getStatMod(StatsMod.Stats stat)
		{
			int modifier = 0;
			List<StatsMod> statsMods = new List<StatsMod>();
			Skill[] skillsAndAbilities = this.SkillsAndAbilities;
			for (int i = 0; i < (int)skillsAndAbilities.Length; i++)
			{
				statsMods.AddRange(skillsAndAbilities[i].ModifiesStats);
			}
			Item[] items = this.Items;
			for (int j = 0; j < (int)items.Length; j++)
			{
				statsMods.AddRange(items[j].ModifiesStats);
			}
			foreach (StatsMod statsMod in statsMods)
			{
				if (statsMod.StatToMod != stat)
				{
					continue;
				}
				modifier = modifier + statsMod.Modifier;
			}
			return modifier;
		}

		public override bool IsOfType(object obj)
		{
			return obj is BaseUnit;
		}

		public override bool IsOfType(Type T)
		{
			return T == typeof(BaseUnit);
		}

		public bool ItemExists(Item item)
		{
			return this._Item.Contains(item);
		}

		public override void Link(ILink DataMaster)
		{
			this.archetype = (Archetype)DataMaster.FindObject(this.archetype);
			this.BaseRace = (Race)DataMaster.FindObject(this.BaseRace);
			List<Item> items = new List<Item>(this.Items);
			this.clearItems();
			foreach (Item item in items)
			{
				this.AddItem((Item)DataMaster.FindObject(item));
			}
			List<WSPPermission> wSPPermissions = new List<WSPPermission>(this.permissions);
			this.clearWPermissions();
			this.clearSPermissions();
			this.clearPPermissions();
			foreach (WSPPermission wSPPermission in wSPPermissions)
			{
				if (wSPPermission is WeaponPermission)
				{
					this.AddWeaponPermission((WeaponPermission)DataMaster.FindObject(wSPPermission));
				}
				if (wSPPermission is SkillPermission)
				{
					this.AddSkillPermission((SkillPermission)DataMaster.FindObject(wSPPermission));
				}
				if (wSPPermission is PsyPermission)
				{
					this.AddPsyPermission((PsyPermission)DataMaster.FindObject(wSPPermission));
				}
			}
		}

		public override Type MyType()
		{
			return this.CollectionType;
		}

		public static bool operator ==(BaseUnit a, BaseUnit b)
		{
			bool flag = false;
			try
			{
				flag = a.Equals(b);
			}
			catch (NullReferenceException nullReferenceException)
			{
				flag = ((a != null ? true : b != null) ? false : true);
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return flag;
		}

		public static bool operator !=(BaseUnit a, BaseUnit b)
		{
			bool flag = false;
			try
			{
				flag = a.Equals(b);
			}
			catch (NullReferenceException nullReferenceException)
			{
				flag = ((a != null ? true : b != null) ? false : true);
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return !flag;
		}

		private bool PermissionFilter(object PERM)
		{
			if (this.BaseRace.ID == "")
			{
				throw new RaceException("Race hasnot ben set");
			}
			bool flag = false;
			Race race = new Race();
			Species speciesCanEquip = (Species)0;
			if (PERM is WSPPermission)
			{
				race = ((WSPPermission)PERM).RaceCanEquip;
				speciesCanEquip = ((WSPPermission)PERM).SpeciesCanEquip;
			}
			if (speciesCanEquip == Species.All)
			{
				flag = true;
			}
			else if (race == null)
			{
				if (this.CheckRaceFlag(speciesCanEquip, this.BaseRace.species))
				{
					flag = true;
				}
			}
			else if (race.ID != "")
			{
				if (race.ID == this.BaseRace.ID)
				{
					flag = true;
				}
			}
			else if (this.CheckRaceFlag(speciesCanEquip, this.BaseRace.species))
			{
				flag = true;
			}
			return flag;
		}

		public override void Print(PrintElement element)
		{
			string str = "";
			element.AddTitleText(string.Concat("Base Unit Type: ", base.Name));
			element.AddText(string.Format("Archetype: {0}  Race: {1}", this.archetype.Name, this.BaseRace.Name));
			element.AddPrimitive(new BaseUnit.PrintStatGrid(this.MovementTotal, this.HitPointsTotal, this.HandToHandTotal, this.AttackPointsTotal, this.PsyPointsTotal, this.RangedWeaponsTotal, this.MightTotal, this.ToughnessTotal, this.AgilityTotal, this.WillpowerTotal, this.HorrorFactorTotal));
			element.AddText(string.Format("Cost: {0}   Starting EXP: {1}", this.Cost, this.StartingEXP));
			WSPPermission[] wSPPermissionArray = this.permissions;
			for (int i = 0; i < (int)wSPPermissionArray.Length; i++)
			{
				Permission permission = wSPPermissionArray[i];
				str = string.Concat(str, permission.Name, ", ");
			}
			str = str.TrimEnd(new char[] { ',' });
			element.AddBlankLine();
			element.AddCategoryText("Permissions:");
			element.AddMText(str);
			str = "";
			Weapon[] weapons = this.Weapons;
			for (int j = 0; j < (int)weapons.Length; j++)
			{
				Weapon weapon = weapons[j];
				str = string.Concat(str, weapon.Name, ", ");
			}
			str = str.TrimEnd(new char[] { ',' });
			element.AddBlankLine();
			element.AddCategoryText("Weapons:");
			element.AddMText(str);
			str = "";
			Psy[] psys = this.Psys;
			for (int k = 0; k < (int)psys.Length; k++)
			{
				Psy psy = psys[k];
				str = string.Concat(str, psy.Name, ", ");
			}
			str = str.TrimEnd(new char[] { ',' });
			element.AddBlankLine();
			element.AddCategoryText("Psys:");
			element.AddMText(str);
			str = "";
			Skill[] skillsAndAbilities = this.SkillsAndAbilities;
			for (int l = 0; l < (int)skillsAndAbilities.Length; l++)
			{
				Skill skill = skillsAndAbilities[l];
				str = string.Concat(str, skill.Name, ", ");
			}
			str = str.TrimEnd(new char[] { ',' });
			element.AddBlankLine();
			element.AddCategoryText("Skills And Abilities:");
			element.AddMText(str);
			str = "";
			Item[] items = this.Items;
			for (int m = 0; m < (int)items.Length; m++)
			{
				Item item = items[m];
				str = string.Concat(str, item.Name, ", ");
			}
			str = str.TrimEnd(new char[] { ',' });
			element.AddBlankLine();
			element.AddCategoryText("Items:");
			element.AddMText(str);
			str = "";
			element.AddBlankLine();
			element.AddCategoryText("Description:");
			element.AddMText(this.Description);
			element.AddBlankLine();
			element.AddCategoryText("Special notes about unit:");
			element.AddMText(this.Special);
			element.AddBlankLine();
		}

		public bool PsyExists(Psy psy)
		{
			return this._Psys.Contains(psy);
		}

		public bool PsyPermissionExists(PsyPermission PPerm)
		{
			bool flag = false;
			flag = (this._PPerms != null ? this._PPerms.Contains(PPerm) : false);
			return flag;
		}

		public override void ReplaceReferences(ObjectClassBase ObjectToReplace, ObjectClassBase ReplaceWith)
		{
			if (ObjectToReplace is Archetype)
			{
				this.archetype = (Archetype)ReplaceWith;
			}
			if (ObjectToReplace is Race)
			{
				this.BaseRace = (Race)ReplaceWith;
			}
			if (ObjectToReplace is Item && this._Item.Contains((Item)ObjectToReplace))
			{
				this._Item.Remove((Item)ObjectToReplace);
				this._Item.Add((Item)ReplaceWith);
			}
			if (ObjectToReplace is SkillPermission && this._SPerms.Contains((SkillPermission)ObjectToReplace))
			{
				this._SPerms.Remove((SkillPermission)ObjectToReplace);
				this._SPerms.Add((SkillPermission)ReplaceWith);
			}
			if (ObjectToReplace is WeaponPermission && this._WPerms.Contains((WeaponPermission)ObjectToReplace))
			{
				this._WPerms.Remove((WeaponPermission)ObjectToReplace);
				this._WPerms.Add((WeaponPermission)ReplaceWith);
			}
			if (ObjectToReplace is PsyPermission && this._PPerms.Contains((PsyPermission)ObjectToReplace))
			{
				this._PPerms.Remove((PsyPermission)ObjectToReplace);
				this._PPerms.Add((PsyPermission)ReplaceWith);
			}
		}

		public bool SkillExists(Skill Skil)
		{
			return this._Skills.Contains(Skil);
		}

		public bool SkillPermissionExists(SkillPermission SPerm)
		{
			bool flag = false;
			flag = (this._SPerms != null ? this._SPerms.Contains(SPerm) : false);
			return flag;
		}

		public static IComparer sortID_Asc()
		{
			return new BaseUnit.sortIDHelper_ASC();
		}

		public static IComparer sortID_Dsc()
		{
			return new BaseUnit.sortIDHelper_DSC();
		}

		public static IComparer sortName_Dsc()
		{
			return new BaseUnit.sortNameHelper_DSC();
		}

		public override string ToString()
		{
			string str;
			string name = base.Name;
			if (Settings.Default.VerboseToString)
			{
				name = "";
				if (Settings.Default.VerboseID)
				{
					name = string.Concat(name, base.ID, ": ");
				}
				if (Settings.Default.VerboseName)
				{
					name = string.Concat(name, base.Name);
				}
				if (Settings.Default.VerboseOther)
				{
					name = string.Concat(new string[] { name, "( ", this.BaseRace.Name, ", ", this.archetype.Name, " )" });
				}
				if (Settings.Default.VerboseDescription)
				{
					if (this.Description.Length < 120)
					{
						str = (this.Description.Length > 0 ? this.Description.Substring(0, this.Description.Length) : "");
					}
					else
					{
						str = string.Concat(this.Description.Substring(0, 120), "...");
					}
					name = string.Concat(name, ", ", str);
				}
			}
			return name;
		}

		public override void Valid()
		{
			List<FieldAndMessage> fieldAndMessages = new List<FieldAndMessage>();
			List<FieldAndMessage> fieldAndMessages1 = new List<FieldAndMessage>();
			try
			{
				try
				{
					base.Valid();
				}
				catch (ValidationException validationException)
				{
					fieldAndMessages.AddRange(validationException.Errors.ToArray());
				}
			}
			finally
			{
				if (this.archetype == null)
				{
					fieldAndMessages.Add(new FieldAndMessage("Archetype", "Base unit must have an Archietype associated with it."));
				}
				if (this.BaseRace == null)
				{
					fieldAndMessages.Add(new FieldAndMessage("Race", "Base unit must have a race associated with it."));
				}
				if (fieldAndMessages.Count >= 1)
				{
					throw new ValidationException(fieldAndMessages.ToArray());
				}
				if (fieldAndMessages1.Count >= 1)
				{
					throw new ValidationWarning(fieldAndMessages1.ToArray());
				}
			}
		}

		private void ValidatePermAndItems()
		{
			List<Item> items = new List<Item>();
			if (this._Item.Count > 0)
			{
				Item[] itemArray = this.Items;
				for (int i = 0; i < (int)itemArray.Length; i++)
				{
					Item item = itemArray[i];
					if (!this.checkItem(item))
					{
						items.Add(item);
					}
				}
				foreach (Item item1 in items)
				{
					this.DropItem(item1);
				}
			}
			List<WeaponPermission> weaponPermissions = new List<WeaponPermission>();
			if (this._WPerms.Count > 0)
			{
				WeaponPermission[] wPermissions = this.WPermissions;
				for (int j = 0; j < (int)wPermissions.Length; j++)
				{
					WeaponPermission weaponPermission = wPermissions[j];
					if (!this.PermissionFilter(weaponPermission))
					{
						weaponPermissions.Add(weaponPermission);
					}
				}
				foreach (WeaponPermission weaponPermission1 in weaponPermissions)
				{
					this.DropWeaponPermission(weaponPermission1);
				}
			}
			if (this._Weapons.Count > 0)
			{
				List<Weapon> weapons = new List<Weapon>();
				Weapon[] weaponArray = this.Weapons;
				for (int k = 0; k < (int)weaponArray.Length; k++)
				{
					Weapon weapon = weaponArray[k];
					if (!this.WPermissions.Contains<Permission>(weapon.RequiresPermission))
					{
						weapons.Add(weapon);
					}
				}
				foreach (Weapon weapon1 in weapons)
				{
					this.DropWeapon(weapon1);
				}
			}
			else
			{
				this._Weapons.Clear();
				if (this.EventWeaponsChanged != null)
				{
					this.EventWeaponsChanged(null);
				}
			}
			List<SkillPermission> skillPermissions = new List<SkillPermission>();
			if (this._SPerms.Count > 0)
			{
				SkillPermission[] sPermissions = this.SPermissions;
				for (int l = 0; l < (int)sPermissions.Length; l++)
				{
					SkillPermission skillPermission = sPermissions[l];
					if (!this.PermissionFilter(skillPermission))
					{
						skillPermissions.Add(skillPermission);
					}
				}
				foreach (SkillPermission skillPermission1 in skillPermissions)
				{
					this.DropSkillPermission(skillPermission1);
				}
			}
			if (this._SPerms.Count > 0)
			{
				List<Skill> skills = new List<Skill>();
				Skill[] skillsAndAbilities = this.SkillsAndAbilities;
				for (int m = 0; m < (int)skillsAndAbilities.Length; m++)
				{
					Skill skill = skillsAndAbilities[m];
					if (!this.SPermissions.Contains<Permission>(skill.RequiresPermission))
					{
						skills.Add(skill);
					}
				}
				foreach (Skill skill1 in skills)
				{
					this.DropSkill(skill1);
				}
			}
			else
			{
				this._Skills.Clear();
				if (this.EventSkillsChanged != null)
				{
					this.EventSkillsChanged(null);
				}
			}
			List<PsyPermission> psyPermissions = new List<PsyPermission>();
			if (this._PPerms.Count > 0)
			{
				PsyPermission[] pPermissions = this.PPermissions;
				for (int n = 0; n < (int)pPermissions.Length; n++)
				{
					PsyPermission psyPermission = pPermissions[n];
					if (!this.PermissionFilter(psyPermission))
					{
						psyPermissions.Add(psyPermission);
					}
				}
				foreach (PsyPermission psyPermission1 in psyPermissions)
				{
					this.DropPsyPermission(psyPermission1);
				}
			}
			if (this._PPerms.Count > 0)
			{
				List<Psy> psies = new List<Psy>();
				Psy[] psys = this.Psys;
				for (int o = 0; o < (int)psys.Length; o++)
				{
					Psy psy = psys[o];
					if (!this.PPermissions.Contains<Permission>(psy.RequiresPermission))
					{
						psies.Add(psy);
					}
				}
				foreach (Psy psy1 in psies)
				{
					this.DropPsy(psy1);
				}
			}
			else
			{
				this._Psys.Clear();
				if (this.EventPsysChanged != null)
				{
					this.EventPsysChanged(null);
				}
			}
			if (this.EventCostChanged != null)
			{
				this.EventCostChanged();
			}
		}

		public bool WeaponExists(Weapon Wepon)
		{
			return this._Weapons.Contains(Wepon);
		}

		public bool WeaponPermissionExists(WeaponPermission WPerm)
		{
			bool flag = false;
			flag = (this._WPerms != null ? this._WPerms.Contains(WPerm) : false);
			return flag;
		}

		public event NTEventHandler EventCostChanged;

		public event NTEventHandler EventDescriptionChanged;

		public event BaseUnit.ItemsChanged EventItemsChanged;

		public event BaseUnit.PsyPermissionsChanged EventPsyPermissionsChanged;

		public event BaseUnit.PsysChanged EventPsysChanged;

		public event NTEventHandler EventRaceChanged;

		public event BaseUnit.SkillPermissionsChanged EventSkillPermissionsChanged;

		public event BaseUnit.SkillsChanged EventSkillsChanged;

		public event NTEventHandler EventSpaecialChanged;

		public event BaseUnit.StatChanged EventStatChanged;

		public event NTEventHandler EventUnitTypeChanged;

		public event BaseUnit.WeaponPermissionsChanged EventWeaponPermissionsChanged;

		public event BaseUnit.WeaponsChanged EventWeaponsChanged;

		public override event NTEventHandler MyDataChanged;

		public delegate void ItemsChanged(Item item);

		public class PrintStatGrid : IPrintPrimitive
		{
			private const float buffer = 4f;

			private string[] StatValues;

			private string[] StatHeaderText;

			public PrintStatGrid(int M, int HP, int HH, int AP, int PP, int RW, int MT, int T, int AG, int WP, int HF)
			{
				this.StatValues[0] = M.ToString();
				this.StatValues[1] = HP.ToString();
				this.StatValues[2] = HH.ToString();
				this.StatValues[3] = AP.ToString();
				this.StatValues[4] = PP.ToString();
				this.StatValues[5] = RW.ToString();
				this.StatValues[6] = MT.ToString();
				this.StatValues[7] = T.ToString();
				this.StatValues[8] = AG.ToString();
				this.StatValues[9] = WP.ToString();
				this.StatValues[10] = HF.ToString();
			}

			public float CalculateHeight(NTAF.PrintEngine.PrintEngine engine, Graphics graphics)
			{
				float height = 0f;
				height = height + engine.PrintFont.GetHeight(graphics) * 2f;
				return height + 16f;
			}

			public void Draw(NTAF.PrintEngine.PrintEngine engine, float yPos, Graphics graphics, Rectangle elementBounds)
			{
				SizeF sizeF;
				List<float> singles = new List<float>();
				List<float> singles1 = new List<float>();
				float[] singleArray = new float[11];
				singles.Add((float)elementBounds.Left);
				for (int i = 0; i <= 10; i++)
				{
					if (graphics.MeasureString(this.StatHeaderText[i], engine.PrintFont).Width < graphics.MeasureString(this.StatValues[i], engine.PrintFont).Width)
					{
						float item = 8f + singles[i];
						sizeF = graphics.MeasureString(this.StatValues[i], engine.PrintFont);
						singles.Add(item + sizeF.Width);
					}
					else
					{
						float single = 8f + singles[i];
						sizeF = graphics.MeasureString(this.StatHeaderText[i], engine.PrintFont);
						singles.Add(single + sizeF.Width);
					}
				}
				for (int j = 0; j <= 10; j++)
				{
					singles1.Add((singles[j + 1] - singles[j]) / 2f + singles[j]);
				}
				Pen pen = new Pen(engine.PrintBrush, 1f);
				foreach (float single1 in singles)
				{
					graphics.DrawLine(pen, single1, yPos, single1, yPos + this.CalculateHeight(engine, graphics));
				}
				graphics.DrawLine(pen, singles[0], yPos, singles[11], yPos);
				graphics.DrawLine(pen, singles[0], yPos + this.CalculateHeight(engine, graphics) / 2f, singles[11], yPos + this.CalculateHeight(engine, graphics) / 2f);
				graphics.DrawLine(pen, singles[0], yPos + this.CalculateHeight(engine, graphics), singles[11], yPos + this.CalculateHeight(engine, graphics));
				for (int k = 0; k <= 10; k++)
				{
					graphics.DrawString(this.StatHeaderText[k], engine.PrintFont, engine.PrintBrush, singles1[k], yPos + 4f + (float)((double)((float)engine.PrintFont.Height) * 0.5), StringHelper.AlignTC());
					graphics.DrawString(this.StatValues[k], engine.PrintFont, engine.PrintBrush, singles1[k], yPos + 12f + pen.Width + (float)((double)((float)engine.PrintFont.Height) * 1.5), StringHelper.AlignTC());
				}
			}
		}

		public delegate void PsyPermissionsChanged(PsyPermission PPerm);

		public delegate void PsysChanged(Psy psy);

		public delegate void SkillPermissionsChanged(SkillPermission PPerm);

		public delegate void SkillsChanged(Skill Skil);

		private class sortAgilityHelper_ASC : IComparer
		{
			public sortAgilityHelper_ASC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				BaseUnit baseUnit = (BaseUnit)a;
				BaseUnit baseUnit1 = (BaseUnit)b;
				int num = 0;
				if (baseUnit.Agility > baseUnit1.Agility)
				{
					num = 1;
				}
				if (baseUnit.Agility < baseUnit1.Agility)
				{
					num = -1;
				}
				return num;
			}
		}

		private class sortAgilityHelper_DSC : IComparer
		{
			public sortAgilityHelper_DSC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				BaseUnit baseUnit = (BaseUnit)a;
				BaseUnit baseUnit1 = (BaseUnit)b;
				int num = 0;
				if (baseUnit.Agility < baseUnit1.Agility)
				{
					num = 1;
				}
				if (baseUnit.Agility > baseUnit1.Agility)
				{
					num = -1;
				}
				return num;
			}
		}

		private class sortAttackPointsHelper_ASC : IComparer
		{
			public sortAttackPointsHelper_ASC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				BaseUnit baseUnit = (BaseUnit)a;
				BaseUnit baseUnit1 = (BaseUnit)b;
				int num = 0;
				if (baseUnit.ActionPoints > baseUnit1.ActionPoints)
				{
					num = 1;
				}
				if (baseUnit.ActionPoints < baseUnit1.ActionPoints)
				{
					num = -1;
				}
				return num;
			}
		}

		private class sortAttackPointsHelper_DSC : IComparer
		{
			public sortAttackPointsHelper_DSC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				BaseUnit baseUnit = (BaseUnit)a;
				BaseUnit baseUnit1 = (BaseUnit)b;
				int num = 0;
				if (baseUnit.ActionPoints < baseUnit1.ActionPoints)
				{
					num = 1;
				}
				if (baseUnit.ActionPoints > baseUnit1.ActionPoints)
				{
					num = -1;
				}
				return num;
			}
		}

		private class sortHandToHandHelper_ASC : IComparer
		{
			public sortHandToHandHelper_ASC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				BaseUnit baseUnit = (BaseUnit)a;
				BaseUnit baseUnit1 = (BaseUnit)b;
				int num = 0;
				if (baseUnit.HandToHand > baseUnit1.HandToHand)
				{
					num = 1;
				}
				if (baseUnit.HandToHand < baseUnit1.HandToHand)
				{
					num = -1;
				}
				return num;
			}
		}

		private class sortHandToHandHelper_DSC : IComparer
		{
			public sortHandToHandHelper_DSC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				BaseUnit baseUnit = (BaseUnit)a;
				BaseUnit baseUnit1 = (BaseUnit)b;
				int num = 0;
				if (baseUnit.HandToHand < baseUnit1.HandToHand)
				{
					num = 1;
				}
				if (baseUnit.HandToHand > baseUnit1.HandToHand)
				{
					num = -1;
				}
				return num;
			}
		}

		private class sortHitPointsHelper_ASC : IComparer
		{
			public sortHitPointsHelper_ASC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				BaseUnit baseUnit = (BaseUnit)a;
				BaseUnit baseUnit1 = (BaseUnit)b;
				int num = 0;
				if (baseUnit.HitPoints > baseUnit1.HitPoints)
				{
					num = 1;
				}
				if (baseUnit.HitPoints < baseUnit1.HitPoints)
				{
					num = -1;
				}
				return num;
			}
		}

		private class sortHitPointsHelper_DSC : IComparer
		{
			public sortHitPointsHelper_DSC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				BaseUnit baseUnit = (BaseUnit)a;
				BaseUnit baseUnit1 = (BaseUnit)b;
				int num = 0;
				if (baseUnit.HitPoints < baseUnit1.HitPoints)
				{
					num = 1;
				}
				if (baseUnit.HitPoints > baseUnit1.HitPoints)
				{
					num = -1;
				}
				return num;
			}
		}

		private class sortHorrorFactorHelper_ASC : IComparer
		{
			public sortHorrorFactorHelper_ASC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				BaseUnit baseUnit = (BaseUnit)a;
				BaseUnit baseUnit1 = (BaseUnit)b;
				int num = 0;
				if (baseUnit.HorrorFactor > baseUnit1.HorrorFactor)
				{
					num = 1;
				}
				if (baseUnit.HorrorFactor < baseUnit1.HorrorFactor)
				{
					num = -1;
				}
				return num;
			}
		}

		private class sortHorrorFactorHelper_DSC : IComparer
		{
			public sortHorrorFactorHelper_DSC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				BaseUnit baseUnit = (BaseUnit)a;
				BaseUnit baseUnit1 = (BaseUnit)b;
				int num = 0;
				if (baseUnit.HorrorFactor < baseUnit1.HorrorFactor)
				{
					num = 1;
				}
				if (baseUnit.HorrorFactor > baseUnit1.HorrorFactor)
				{
					num = -1;
				}
				return num;
			}
		}

		private class sortIDHelper_ASC : IComparer
		{
			public sortIDHelper_ASC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				BaseUnit baseUnit = (BaseUnit)a;
				BaseUnit baseUnit1 = (BaseUnit)b;
				return string.Compare(baseUnit.ID, baseUnit1.ID);
			}
		}

		private class sortIDHelper_DSC : IComparer
		{
			public sortIDHelper_DSC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				BaseUnit baseUnit = (BaseUnit)a;
				return string.Compare(((BaseUnit)b).ID, baseUnit.ID);
			}
		}

		private class sortMightHelper_ASC : IComparer
		{
			public sortMightHelper_ASC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				BaseUnit baseUnit = (BaseUnit)a;
				BaseUnit baseUnit1 = (BaseUnit)b;
				int num = 0;
				if (baseUnit.Might > baseUnit1.Might)
				{
					num = 1;
				}
				if (baseUnit.Might < baseUnit1.Might)
				{
					num = -1;
				}
				return num;
			}
		}

		private class sortMightHelper_DSC : IComparer
		{
			public sortMightHelper_DSC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				BaseUnit baseUnit = (BaseUnit)a;
				BaseUnit baseUnit1 = (BaseUnit)b;
				int num = 0;
				if (baseUnit.Might < baseUnit1.Might)
				{
					num = 1;
				}
				if (baseUnit.Might > baseUnit1.Might)
				{
					num = -1;
				}
				return num;
			}
		}

		private class sortMovementHelper_ASC : IComparer
		{
			public sortMovementHelper_ASC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				BaseUnit baseUnit = (BaseUnit)a;
				BaseUnit baseUnit1 = (BaseUnit)b;
				int num = 0;
				if (baseUnit.Movement > baseUnit1.Movement)
				{
					num = 1;
				}
				if (baseUnit.Movement < baseUnit1.Movement)
				{
					num = -1;
				}
				return num;
			}
		}

		private class sortMovementHelper_DSC : IComparer
		{
			public sortMovementHelper_DSC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				BaseUnit baseUnit = (BaseUnit)a;
				BaseUnit baseUnit1 = (BaseUnit)b;
				int num = 0;
				if (baseUnit.Movement < baseUnit1.Movement)
				{
					num = 1;
				}
				if (baseUnit.Movement > baseUnit1.Movement)
				{
					num = -1;
				}
				return num;
			}
		}

		private class sortNameHelper_DSC : IComparer
		{
			public sortNameHelper_DSC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				BaseUnit baseUnit = (BaseUnit)a;
				return string.Compare(((BaseUnit)b).Name, baseUnit.Name);
			}
		}

		private class sortPsyPointsHelper_ASC : IComparer
		{
			public sortPsyPointsHelper_ASC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				BaseUnit baseUnit = (BaseUnit)a;
				BaseUnit baseUnit1 = (BaseUnit)b;
				int num = 0;
				if (baseUnit.PsyPoints > baseUnit1.PsyPoints)
				{
					num = 1;
				}
				if (baseUnit.PsyPoints < baseUnit1.PsyPoints)
				{
					num = -1;
				}
				return num;
			}
		}

		private class sortPsyPointsHelper_DSC : IComparer
		{
			public sortPsyPointsHelper_DSC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				BaseUnit baseUnit = (BaseUnit)a;
				BaseUnit baseUnit1 = (BaseUnit)b;
				int num = 0;
				if (baseUnit.PsyPoints < baseUnit1.PsyPoints)
				{
					num = 1;
				}
				if (baseUnit.PsyPoints > baseUnit1.PsyPoints)
				{
					num = -1;
				}
				return num;
			}
		}

		private class sortRangedWeaponsHelper_ASC : IComparer
		{
			public sortRangedWeaponsHelper_ASC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				BaseUnit baseUnit = (BaseUnit)a;
				BaseUnit baseUnit1 = (BaseUnit)b;
				int num = 0;
				if (baseUnit.RangedWeapons > baseUnit1.RangedWeapons)
				{
					num = 1;
				}
				if (baseUnit.RangedWeapons < baseUnit1.RangedWeapons)
				{
					num = -1;
				}
				return num;
			}
		}

		private class sortRangedWeaponsHelper_DSC : IComparer
		{
			public sortRangedWeaponsHelper_DSC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				BaseUnit baseUnit = (BaseUnit)a;
				BaseUnit baseUnit1 = (BaseUnit)b;
				int num = 0;
				if (baseUnit.RangedWeapons < baseUnit1.RangedWeapons)
				{
					num = 1;
				}
				if (baseUnit.RangedWeapons > baseUnit1.RangedWeapons)
				{
					num = -1;
				}
				return num;
			}
		}

		private class sortToughnessHelper_ASC : IComparer
		{
			public sortToughnessHelper_ASC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				BaseUnit baseUnit = (BaseUnit)a;
				BaseUnit baseUnit1 = (BaseUnit)b;
				int num = 0;
				if (baseUnit.Toughness > baseUnit1.Toughness)
				{
					num = 1;
				}
				if (baseUnit.Toughness < baseUnit1.Toughness)
				{
					num = -1;
				}
				return num;
			}
		}

		private class sortToughnessHelper_DSC : IComparer
		{
			public sortToughnessHelper_DSC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				BaseUnit baseUnit = (BaseUnit)a;
				BaseUnit baseUnit1 = (BaseUnit)b;
				int num = 0;
				if (baseUnit.Toughness < baseUnit1.Toughness)
				{
					num = 1;
				}
				if (baseUnit.Toughness > baseUnit1.Toughness)
				{
					num = -1;
				}
				return num;
			}
		}

		private class sortWillpowerHelper_ASC : IComparer
		{
			public sortWillpowerHelper_ASC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				BaseUnit baseUnit = (BaseUnit)a;
				BaseUnit baseUnit1 = (BaseUnit)b;
				int num = 0;
				if (baseUnit.Willpower > baseUnit1.Willpower)
				{
					num = 1;
				}
				if (baseUnit.Willpower < baseUnit1.Willpower)
				{
					num = -1;
				}
				return num;
			}
		}

		private class sortWillpowerHelper_DSC : IComparer
		{
			public sortWillpowerHelper_DSC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				BaseUnit baseUnit = (BaseUnit)a;
				BaseUnit baseUnit1 = (BaseUnit)b;
				int num = 0;
				if (baseUnit.Willpower < baseUnit1.Willpower)
				{
					num = 1;
				}
				if (baseUnit.Willpower > baseUnit1.Willpower)
				{
					num = -1;
				}
				return num;
			}
		}

		public delegate void StatChanged(StatsMod.Stats Stat);

		public delegate void WeaponPermissionsChanged(WeaponPermission WPerm);

		public delegate void WeaponsChanged(Weapon Weapon);
	}
}