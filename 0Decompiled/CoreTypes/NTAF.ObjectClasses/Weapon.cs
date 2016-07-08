using NTAF.Core;
using NTAF.Core.Properties;
using NTAF.PlugInFramework;
using NTAF.PrintEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml.Serialization;

namespace NTAF.ObjectClasses
{
	[ObjectClassPlugIn("Weapon", "0.0.0.0")]
	[Serializable]
	[XmlInclude(typeof(WeaponPermission))]
	public class Weapon : ObjectClassBase, IRequiresPermission
	{
		private string _Description;

		private string _Range;

		private string _Special;

		private WeaponPermission _RequiresPermission;

		private sbyte _MvsP;

		private sbyte _MvsA;

		private sbyte _SIOR;

		private sbyte _SvMod;

		private byte _Shots;

		private ushort _Cost;

		private WeaponBaseType _BaseType;

		[Browsable(false)]
		[XmlIgnore]
		public override string aboutMe
		{
			get
			{
				string str = base.aboutMe;
				if (this.Range != "")
				{
					str = string.Concat(str, "Range: ", this.Range.ToString(), "\n");
				}
				if (this.SvMod != 0)
				{
					str = string.Concat(str, string.Format("Save Modifier: {0}\n", this.SvMod));
				}
				str = string.Concat(str, string.Format("MvsP: {0} MvsA: {1}\n", new object[] { this.MvsP, this.MvsA }));
				str = string.Concat(str, string.Format("Shots: {0}    SIOR: {1}\n", new object[] { this.Shots, this.SIOR }));
				if (this.BaseType != WeaponBaseType.NoBase)
				{
					str = string.Concat(str, string.Format("Base Weapon Type: {0}\n", this.BaseType));
				}
				if (this.Special != "")
				{
					str = string.Concat(str, "Special:\n", GeneralOperations.WrapLength(this.Special, 50), "\n");
				}
				if (this.Description != "")
				{
					str = string.Concat(str, "Description:\n", GeneralOperations.WrapLength(this.Description, 50), "\n");
				}
				if (this.RequiresPermission != null)
				{
					str = string.Concat(str, "Required Permission: ", this.RequiresPermission.Name, "\n");
				}
				str = string.Concat(str, string.Format("Cost: {0}", this.Cost));
				return str;
			}
		}

		[Category("Stats")]
		[Description("")]
		[XmlAttribute]
		public WeaponBaseType BaseType
		{
			get
			{
				return this._BaseType;
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				this._BaseType = value;
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventBaseTypeChanged != null)
					{
						this.EventBaseTypeChanged();
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
				return typeof(Weapon);
			}
		}

		[Category("Stats")]
		[Description("")]
		[XmlAttribute]
		public ushort Cost
		{
			get
			{
				return this._Cost;
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				this._Cost = value;
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

		[Category("Stats")]
		[Description("")]
		[XmlAttribute]
		public sbyte MvsA
		{
			get
			{
				return this._MvsA;
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				this._MvsA = value;
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventMvsAChanged != null)
					{
						this.EventMvsAChanged();
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		[Category("Stats")]
		[Description("")]
		[XmlAttribute]
		public sbyte MvsP
		{
			get
			{
				return this._MvsP;
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				this._MvsP = value;
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventMvsPChanged != null)
					{
						this.EventMvsPChanged();
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		[Category("Stats")]
		[Description("")]
		[XmlAttribute]
		public string Range
		{
			get
			{
				return this._Range;
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				this._Range = value;
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventRangeChanged != null)
					{
						this.EventRangeChanged();
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		[Category("Requires Permission")]
		[Description("")]
		public Permission RequiresPermission
		{
			get
			{
				return this._RequiresPermission;
			}
			set
			{
				if (!Settings.Default.Loading && !Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				if ((value is WeaponPermission ? false : value != null))
				{
					throw new ItemException("Permission is not of the proper type, only Weapon Permissions can be assigned");
				}
				this._RequiresPermission = (WeaponPermission)value;
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventRequiredPermissionChanged != null)
					{
						this.EventRequiredPermissionChanged();
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		[Category("Stats")]
		[Description("")]
		[XmlAttribute]
		public byte Shots
		{
			get
			{
				return this._Shots;
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				this._Shots = value;
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventShotsChanged != null)
					{
						this.EventShotsChanged();
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		[Category("Stats")]
		[Description("")]
		[XmlAttribute]
		public sbyte SIOR
		{
			get
			{
				return this._SIOR;
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				this._SIOR = value;
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventSIORChanged != null)
					{
						this.EventSIORChanged();
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
					if (this.EventSpecialChanged != null)
					{
						this.EventSpecialChanged();
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		[Category("Stats")]
		[Description("")]
		[XmlAttribute]
		public sbyte SvMod
		{
			get
			{
				return this._SvMod;
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				this._SvMod = value;
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventSvModChanged != null)
					{
						this.EventSvModChanged();
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		public Weapon()
		{
			base.ID = "";
			base.Name = "";
			this.Description = "";
			this.Range = "";
			this.Special = "";
			this.MvsP = 0;
			this.MvsA = 0;
			this.SIOR = 0;
			this.Shots = 0;
			this.Cost = 0;
			this.SvMod = 0;
			this.BaseType = WeaponBaseType.NoBase;
			this.RequiresPermission = null;
		}

		public Weapon(Weapon weapon)
		{
			base.ID = weapon.ID;
			base.Name = weapon.Name;
			this.Description = weapon.Description;
			this.Range = weapon.Range;
			this.Special = weapon.Special;
			this.MvsP = weapon.MvsP;
			this.MvsA = weapon.MvsA;
			this.SIOR = weapon.SIOR;
			this.Shots = weapon.Shots;
			this.Cost = weapon.Cost;
			this.SvMod = weapon.SvMod;
			this.BaseType = weapon.BaseType;
			this.RequiresPermission = weapon.RequiresPermission;
		}

		public Weapon(string wpID, string wpName, string wpDescription, string wpRange, string wpSpecial, sbyte wpMvsP, sbyte wpMvsA, sbyte wpSIOR, byte wpShots, ushort wpCost, sbyte wpSvMod, WeaponBaseType wpBaseType, WeaponPermission wpRequiresPermission)
		{
			base.ID = wpID;
			base.Name = wpName;
			this.Description = wpDescription;
			this.Range = wpRange;
			this.Special = wpSpecial;
			this.MvsP = wpMvsP;
			this.MvsA = wpMvsA;
			this.SIOR = wpSIOR;
			this.Shots = wpShots;
			this.Cost = wpCost;
			this.SvMod = wpSvMod;
			this.BaseType = wpBaseType;
			this.RequiresPermission = wpRequiresPermission;
		}

		public override bool CheckForReferences(ObjectClassBase Item)
		{
			return (this.RequiresPermission != Item ? false : true);
		}

		protected override void clearMyEvents()
		{
			base.clearMyEvents();
			this.EventBaseTypeChanged = null;
			this.EventCostChanged = null;
			this.EventDescriptionChanged = null;
			this.EventMvsAChanged = null;
			this.EventMvsPChanged = null;
			this.EventRangeChanged = null;
			this.EventRequiredPermissionChanged = null;
			this.EventShotsChanged = null;
			this.EventSIORChanged = null;
			this.EventSpecialChanged = null;
			this.EventSvModChanged = null;
		}

		public override bool Equals(object obj)
		{
			bool requiresPermission = true;
			if ((obj == null ? true : !(obj is Weapon)))
			{
				requiresPermission = false;
			}
			if (requiresPermission)
			{
				requiresPermission = this.RequiresPermission == ((Weapon)obj).RequiresPermission;
			}
			if (requiresPermission)
			{
				requiresPermission = base.ID == ((Weapon)obj).ID;
			}
			if (requiresPermission)
			{
				requiresPermission = base.Name == ((Weapon)obj).Name;
			}
			if (requiresPermission)
			{
				requiresPermission = this.Description == ((Weapon)obj).Description;
			}
			if (requiresPermission)
			{
				requiresPermission = this.Range == ((Weapon)obj).Range;
			}
			if (requiresPermission)
			{
				requiresPermission = this.Special == ((Weapon)obj).Special;
			}
			if (requiresPermission)
			{
				requiresPermission = this.MvsP == ((Weapon)obj).MvsP;
			}
			if (requiresPermission)
			{
				requiresPermission = this.MvsA == ((Weapon)obj).MvsA;
			}
			if (requiresPermission)
			{
				requiresPermission = this.SvMod == ((Weapon)obj).SvMod;
			}
			if (requiresPermission)
			{
				requiresPermission = this.SIOR == ((Weapon)obj).SIOR;
			}
			if (requiresPermission)
			{
				requiresPermission = this.Shots == ((Weapon)obj).Shots;
			}
			if (requiresPermission)
			{
				requiresPermission = this.Cost == ((Weapon)obj).Cost;
			}
			if (requiresPermission)
			{
				requiresPermission = this.BaseType == ((Weapon)obj).BaseType;
			}
			return requiresPermission;
		}

		public override DataMember[] getDataMembers()
		{
			return new DataMember[] { new DataMember("Name", base.Name), new DataMember("Range", this.Range), new DataMember("Description", this.Description), new DataMember("Special", this.Special), new DataMember("MvsP", (object)this.MvsP), new DataMember("MvsA", (object)this.MvsA), new DataMember("SIOR", (object)this.SIOR), new DataMember("SvMod", (object)this.SvMod), new DataMember("Shots", (object)this.Shots), new DataMember("Permission", this.RequiresPermission.Name), new DataMember("Cost", (object)this.Cost) };
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool IsOfType(object obj)
		{
			return obj is Weapon;
		}

		public override bool IsOfType(Type T)
		{
			return T == this.CollectionType;
		}

		public override void Link(ILink DataMaster)
		{
			this.RequiresPermission = (Permission)DataMaster.FindObject(this.RequiresPermission);
		}

		public override Type MyType()
		{
			return this.CollectionType;
		}

		public static bool operator ==(Weapon a, Weapon b)
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

		public static bool operator !=(Weapon a, Weapon b)
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

		public override void Print(PrintElement element)
		{
			element.AddTitleText(string.Concat("Weapon: ", base.Name));
			element.AddCategoryText("Range: ", (this.Range != "0" ? string.Concat(this.Range, "\"") : "Direct Contact"));
			element.AddText(string.Format("Save Modifier: {0}    MvsP/MvsA: {1}/{2}", this.SvMod, this.MvsP, this.MvsA));
			element.AddText(string.Format("SIOR/Shots: {0}/{1}", this.SIOR, this.Shots));
			element.AddBlankLine();
			element.AddCategoryText("Description:");
			element.AddMText(this.Description);
			element.AddCategoryText("Base Type: ", this.BaseType.ToString());
			element.AddBlankLine();
			element.AddCategoryText("Requires Permission: ", this.RequiresPermission.Name);
			element.AddBlankLine();
			element.AddCategoryText("Special:");
			element.AddMText(this.Special);
			element.AddBlankLine();
			element.AddCategoryText("Cost: ", this.Cost.ToString());
			element.AddBlankLine();
		}

		public override void ReplaceReferences(ObjectClassBase ObjectToReplace, ObjectClassBase ReplaceWith)
		{
			if (this.RequiresPermission == ObjectToReplace)
			{
				this.RequiresPermission = (Permission)ReplaceWith;
			}
		}

		public static IComparer sortByIDAsc()
		{
			return new Weapon.sortByIDHelper_Asc();
		}

		public static IComparer sortByIDDsc()
		{
			return new Weapon.sortByIDHelper_Dsc();
		}

		public static IComparer sortByNameDsc()
		{
			return new Weapon.sortByNameHelper_Dec();
		}

		public override string ToString()
		{
			string name = base.Name;
			if (Settings.Default.VerboseToString)
			{
				name = string.Concat(base.ID, ": ", base.Name);
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
				if (this._Range == "")
				{
					fieldAndMessages.Add(new FieldAndMessage("Range", "Requires a value"));
				}
				if (this._RequiresPermission == null)
				{
					fieldAndMessages.Add(new FieldAndMessage("Required Permission", "Requires a value"));
				}
				if (this._MvsP <= -1)
				{
					fieldAndMessages.Add(new FieldAndMessage("MvsP", "To Small"));
				}
				if (this._MvsA <= -1)
				{
					fieldAndMessages.Add(new FieldAndMessage("MvsA", "To Small"));
				}
				if (this._SIOR <= -1)
				{
					fieldAndMessages.Add(new FieldAndMessage("SIOR", "Needs to be a positive number"));
				}
				if (this._Cost == 0)
				{
					fieldAndMessages.Add(new FieldAndMessage("Cost", "Weapon must have a cost"));
				}
				if (this._BaseType.Is<WeaponBaseType>(WeaponBaseType.NoBase))
				{
					fieldAndMessages.Add(new FieldAndMessage("Base Type", "All weapons have a base type"));
				}
				if ((!(this._BaseType.Is<WeaponBaseType>(WeaponBaseType.Projectile) | this._BaseType.Is<WeaponBaseType>(WeaponBaseType.Gun) | this._BaseType.Is<WeaponBaseType>(WeaponBaseType.Thrown)) ? false : this._Shots == 0))
				{
					fieldAndMessages.Add(new FieldAndMessage("Shots", string.Concat(this._BaseType.GetDescription<WeaponBaseType>(this._BaseType), " requires atleast 1 shot")));
				}
				if (this._Description == "")
				{
					fieldAndMessages1.Add(new FieldAndMessage("Description", "Should not be blank"));
				}
				if (this._Special == "")
				{
					fieldAndMessages1.Add(new FieldAndMessage("Special", "Should not be blank"));
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

		public event NTEventHandler EventBaseTypeChanged;

		public event NTEventHandler EventCostChanged;

		public event NTEventHandler EventDescriptionChanged;

		public event NTEventHandler EventMvsAChanged;

		public event NTEventHandler EventMvsPChanged;

		public event NTEventHandler EventRangeChanged;

		public event NTEventHandler EventRequiredPermissionChanged;

		public event NTEventHandler EventShotsChanged;

		public event NTEventHandler EventSIORChanged;

		public event NTEventHandler EventSpecialChanged;

		public event NTEventHandler EventSvModChanged;

		public override event NTEventHandler MyDataChanged;

		private class sortByIDHelper_Asc : IComparer
		{
			public sortByIDHelper_Asc()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				Weapon weapon = (Weapon)a;
				Weapon weapon1 = (Weapon)b;
				return string.Compare(weapon.ID, weapon1.ID);
			}
		}

		private class sortByIDHelper_Dsc : IComparer
		{
			public sortByIDHelper_Dsc()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				Weapon weapon = (Weapon)a;
				return string.Compare(((Weapon)b).ID, weapon.ID);
			}
		}

		private class sortByNameHelper_Dec : IComparer
		{
			public sortByNameHelper_Dec()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				Weapon weapon = (Weapon)a;
				return string.Compare(((Weapon)b).Name, weapon.Name);
			}
		}
	}
}