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
	[ObjectClassPlugIn("Item", "0.0.0.0")]
	[Serializable]
	public class Item : ObjectClassBase
	{
		private string _Description;

		private List<StatsMod> _modifyStat = new List<StatsMod>();

		private uint _Cost;

		private ArchetypeBaseEnu _UseableType = ArchetypeBaseEnu.All;

		private Race _UseableRace = null;

		private Species _RacesCanUse = 0;

		[Browsable(false)]
		[XmlIgnore]
		public override string aboutMe
		{
			get
			{
				string str = base.aboutMe;
				if ((int)this.SpeciesCanEquip != 0)
				{
					Species speciesCanEquip = this.SpeciesCanEquip;
					str = string.Concat(str, "Required Base Race(s): ", speciesCanEquip.ToString(), "\n");
				}
				else if (this.RaceCanEquip != null)
				{
					str = string.Concat(str, "Required Race: ", this.RaceCanEquip.Name, "\n");
				}
				if (this.ModifiesStats.Length != 0)
				{
					StatsMod[] modifiesStats = this.ModifiesStats;
					for (int i = 0; i < (int)modifiesStats.Length; i++)
					{
						StatsMod statsMod = modifiesStats[i];
						str = string.Concat(str, statsMod.ToString(), "\n");
					}
				}
				if (this.Description != "")
				{
					str = string.Concat(str, "Description :\n", GeneralOperations.WrapLength(this.Description, 50), "\n");
				}
				uint cost = this.Cost;
				str = string.Concat(str, "Cost: ", cost.ToString());
				return str;
			}
		}

		public override Type CollectionType
		{
			get
			{
				return typeof(Item);
			}
		}

		[Category("About")]
		[Description("")]
		[XmlAttribute]
		public uint Cost
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
		public StatsMod[] ModifiesStats
		{
			get
			{
				return this._modifyStat.ToArray();
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				this._modifyStat.Clear();
				this._modifyStat.AddRange(value);
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventStatModsChanged != null)
					{
						this.EventStatModsChanged();
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		[Category("Permission Requirements")]
		public Race RaceCanEquip
		{
			get
			{
				return this._UseableRace;
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				this._UseableRace = value;
				if (!Settings.Default.Loading)
				{
					if (this.EventRaceChange != null)
					{
						this.EventRaceChange();
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		[XmlAttribute]
		public Species SpeciesCanEquip
		{
			get
			{
				return this._RacesCanUse;
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				this._RacesCanUse = value;
				if (this.EventRaceChange != null)
				{
					this.EventRaceChange();
				}
				if (this.MyDataChanged != null)
				{
					this.MyDataChanged();
				}
			}
		}

		[Category("About")]
		[Description("")]
		[XmlAttribute]
		public ArchetypeBaseEnu TypesCanUse
		{
			get
			{
				return this._UseableType;
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				this._UseableType = value;
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventUseableTypeChanged != null)
					{
						this.EventUseableTypeChanged();
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		public Item()
		{
			base.ID = "";
			base.Name = "";
			this.SpeciesCanEquip = (Species)0;
			this.RaceCanEquip = null;
			this.Description = "";
			this.Cost = 0;
		}

		public Item(Item var)
		{
			base.ID = var.ID;
			base.Name = var.Name;
			this.SpeciesCanEquip = var.SpeciesCanEquip;
			this.RaceCanEquip = var.RaceCanEquip;
			this.Description = var.Description;
			this.ModifiesStats = var.ModifiesStats;
			this.Cost = var.Cost;
		}

		public Item(string iID, string iName, Species iBaseRacesCanEquip, Race iSubRaceCanEquip, string iDescription, StatsMod[] iStatMods, ushort iCost)
		{
			base.ID = iID;
			base.Name = iName;
			this.SpeciesCanEquip = iBaseRacesCanEquip;
			this.RaceCanEquip = iSubRaceCanEquip;
			this.Description = iDescription;
			this.ModifiesStats = iStatMods;
			this.Cost = iCost;
		}

		public void AddStatMod(StatsMod SM)
		{
			if (this.StatsModExists(SM))
			{
				throw new Exception("Modifier allreaddy exists");
			}
			this._modifyStat.Add(SM);
			if (this.EventStatModsChanged != null)
			{
				this.EventStatModsChanged();
			}
		}

		public override bool CheckForReferences(ObjectClassBase item)
		{
			return (this.RaceCanEquip != item ? false : true);
		}

		protected override void clearMyEvents()
		{
			base.clearMyEvents();
			this.EventCostChanged = null;
			this.EventDescriptionChanged = null;
			this.EventRaceChange = null;
			this.EventStatModsChanged = null;
			this.EventUseableTypeChanged = null;
		}

		public void DropStatMod(StatsMod SM)
		{
			if (!this.StatsModExists(SM))
			{
				throw new Exception("Modifier not found");
			}
			this._modifyStat.Remove(SM);
			if (this.EventStatModsChanged != null)
			{
				this.EventStatModsChanged();
			}
		}

		public override bool Equals(object obj)
		{
			bool d = true;
			if ((obj == null ? true : !(obj is Item)))
			{
				d = false;
			}
			if (d)
			{
				d = base.ID == ((Item)obj).ID;
			}
			if (d)
			{
				d = base.Name == ((Item)obj).Name;
			}
			if (d)
			{
				d = this.RaceCanEquip == ((Item)obj).RaceCanEquip;
			}
			if (d)
			{
				d = this.SpeciesCanEquip == ((Item)obj).SpeciesCanEquip;
			}
			if (d)
			{
				d = (int)this.ModifiesStats.Length == (int)((Item)obj).ModifiesStats.Length;
			}
			if (d)
			{
				d = this.Description == ((Item)obj).Description;
			}
			if (d)
			{
				d = this.Cost == ((Item)obj).Cost;
			}
			if (d)
			{
				d = this.TypesCanUse == ((Item)obj).TypesCanUse;
			}
			return d;
		}

		public override DataMember[] getDataMembers()
		{
			string str = "";
			StatsMod[] modifiesStats = this.ModifiesStats;
			for (int i = 0; i < (int)modifiesStats.Length; i++)
			{
				StatsMod statsMod = modifiesStats[i];
				str = string.Concat(str, statsMod.ToString(), ", ");
			}
			str = str.TrimEnd(new char[] { ',', ' ' });
			DataMember[] dataMember = new DataMember[] { new DataMember("Name", base.Name), new DataMember("Description", this.Description), new DataMember("Modifies Stats", str), new DataMember("Archatypes Can Use", (object)this.TypesCanUse), null, null, null };
			dataMember[4] = (this._UseableRace == null ? new DataMember("Races Can Use", "") : new DataMember("Races Can Use", this._UseableRace.Name));
			dataMember[5] = new DataMember("Species Can Use", (object)this._RacesCanUse);
			dataMember[6] = new DataMember("Cost", (object)this.Cost);
			return dataMember;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool IsOfType(object obj)
		{
			return obj is Item;
		}

		public override bool IsOfType(Type T)
		{
			return T == typeof(Item);
		}

		public override void Link(ILink DataMaster)
		{
			this.RaceCanEquip = (Race)DataMaster.FindObject(this.RaceCanEquip);
		}

		public override Type MyType()
		{
			return this.CollectionType;
		}

		public static bool operator ==(Item a, Item b)
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

		public static bool operator !=(Item a, Item b)
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
			element.AddTitleText(string.Concat("Item: ", base.Name));
			element.AddCategoryText("For Type", this.TypesCanUse.ToString());
			element.AddBlankLine();
			element.AddCategoryText("Description:");
			element.AddMText(this.Description);
			element.AddBlankLine();
			if ((int)this.ModifiesStats.Length >= 1)
			{
				element.AddCategoryText("Modifies Stats:");
				string str = "";
				StatsMod[] modifiesStats = this.ModifiesStats;
				for (int i = 0; i < (int)modifiesStats.Length; i++)
				{
					StatsMod statsMod = modifiesStats[i];
					str = string.Concat(str, statsMod.ToString(), ", ");
				}
				element.AddMText(str.TrimEnd(new char[] { ',', ' ' }));
				element.AddBlankLine();
			}
			element.AddCategoryText("Cost", this.Cost.ToString());
			element.AddBlankLine();
		}

		public override void ReplaceReferences(ObjectClassBase ObjectToReplace, ObjectClassBase ReplaceWith)
		{
			if (this.RaceCanEquip == ObjectToReplace)
			{
				this.RaceCanEquip = (Race)ReplaceWith;
			}
		}

		public static IComparer sortID_Asc()
		{
			return new Item.sortIDHelper_ASC();
		}

		public static IComparer sortID_Dsc()
		{
			return new Item.sortIDHelper_DSC();
		}

		public static IComparer sortName_Dsc()
		{
			return new Item.sortNameHelper_DSC();
		}

		public bool StatsModExists(StatsMod SM)
		{
			bool flag = false;
			if (this._modifyStat.Contains(SM))
			{
				flag = true;
			}
			return flag;
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
				if (((int)this.SpeciesCanEquip != 0 ? false : this.RaceCanEquip == null))
				{
					fieldAndMessages.Add(new FieldAndMessage("Race/Species", "A useable race or base race needs to be selected"));
				}
				if (this.Cost == 0)
				{
					fieldAndMessages.Add(new FieldAndMessage("Cost", "Item must cost something"));
				}
				if (this.Description == "")
				{
					fieldAndMessages1.Add(new FieldAndMessage("Description", "No description given"));
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

		public event NTEventHandler EventCostChanged;

		public event NTEventHandler EventDescriptionChanged;

		public event NTEventHandler EventRaceChange;

		public event NTEventHandler EventStatModsChanged;

		public event NTEventHandler EventUseableTypeChanged;

		public override event NTEventHandler MyDataChanged;

		private class sortIDHelper_ASC : IComparer
		{
			public sortIDHelper_ASC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				Item item = (Item)a;
				Item item1 = (Item)b;
				return string.Compare(item.ID, item1.ID);
			}
		}

		private class sortIDHelper_DSC : IComparer
		{
			public sortIDHelper_DSC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				Item item = (Item)a;
				return string.Compare(((Item)b).ID, item.ID);
			}
		}

		private class sortNameHelper_DSC : IComparer
		{
			public sortNameHelper_DSC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				Item item = (Item)a;
				return string.Compare(((Item)b).Name, item.Name);
			}
		}
	}
}