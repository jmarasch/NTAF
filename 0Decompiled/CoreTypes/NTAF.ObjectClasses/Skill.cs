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
	[ObjectClassPlugIn("Skill", "0.0.0.0")]
	[Serializable]
	[XmlInclude(typeof(SkillPermission))]
	public class Skill : ObjectClassBase, IRequiresPermission
	{
		private string _Description;

		private SkillPermission _RequiresPermission;

		private ushort _Cost;

		private Species _SpeciesCanUse;

		private SkillGroupFlag _Group;

		private List<StatsMod> _modifyStat = new List<StatsMod>();

		[Browsable(false)]
		[XmlIgnore]
		public override string aboutMe
		{
			get
			{
				string str = base.aboutMe;
				if (this.ModifiesStats.Count != 0)
				{
					str = string.Concat(str, "\n");
				}
				foreach (StatsMod modifiesStat in this.ModifiesStats)
				{
					str = string.Concat(str, string.Format("{0}\n", modifiesStat));
				}
				if (this.Description != "")
				{
					str = string.Concat(str, string.Format("\nDescription: \n{0}\n\n", GeneralOperations.WrapLength(this.Description, 50)));
				}
				str = string.Concat(str, string.Format("Skill Group: {0}\n\n", this.Group));
				str = string.Concat(str, string.Format("Avalible to: {0}\n\n", this.SpeciesCanUseSkill));
				if (this.RequiresPermission != null)
				{
					str = string.Concat(str, string.Format("Requires Permission: {0}\n\n", this.RequiresPermission.Name));
				}
				str = string.Concat(str, string.Format("Cost: {0}\n\n", this.Cost));
				return str;
			}
		}

		public override Type CollectionType
		{
			get
			{
				return typeof(Skill);
			}
		}

		[Category("Cost")]
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
					if (this.EventCostChange != null)
					{
						this.EventCostChange();
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
					if (this.EventDescriptionChange != null)
					{
						this.EventDescriptionChange();
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		[Category("Base")]
		[Description("")]
		[XmlAttribute]
		public SkillGroupFlag Group
		{
			get
			{
				return this._Group;
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				this._Group = value;
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventGroupChange != null)
					{
						this.EventGroupChange();
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
		public List<StatsMod> ModifiesStats
		{
			get
			{
				return this._modifyStat;
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				this._modifyStat = value;
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventModifyStatChange != null)
					{
						this.EventModifyStatChange();
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		[Category("Base")]
		[Description("")]
		public Permission RequiresPermission
		{
			get
			{
				return this._RequiresPermission;
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				if ((value is SkillPermission ? false : value != null))
				{
					throw new ArgumentException("When setting this permission it must be of the PsyPermission Type");
				}
				this._RequiresPermission = (SkillPermission)value;
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

		[Category("Base")]
		[Description("")]
		[XmlAttribute]
		public Species SpeciesCanUseSkill
		{
			get
			{
				return this._SpeciesCanUse;
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				this._SpeciesCanUse = value;
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventSpeciesChange != null)
					{
						this.EventSpeciesChange();
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		public Skill()
		{
			base.ID = "";
			base.Name = "";
			this.Cost = 0;
			this.Description = "";
			this.SpeciesCanUseSkill = (Species)0;
			this.Group = SkillGroupFlag.Generic;
			this.ModifiesStats = new List<StatsMod>();
			this.RequiresPermission = null;
		}

		public Skill(Skill var)
		{
			base.ID = var.ID;
			base.Name = var.Name;
			this.Cost = var.Cost;
			this.Description = var.Description;
			this.SpeciesCanUseSkill = var.SpeciesCanUseSkill;
			this.Group = var.Group;
			this.ModifiesStats = var.ModifiesStats;
			this.RequiresPermission = var.RequiresPermission;
		}

		public Skill(string skID, string skName, ushort skCost, string skDescription, Species skRacesCanUse, SkillGroupFlag skGroup, List<StatsMod> skModifysStat, SkillPermission skRequiresPermission)
		{
			base.ID = skID;
			base.Name = skName;
			this.Cost = skCost;
			this.Description = skDescription;
			this.SpeciesCanUseSkill = skRacesCanUse;
			this.Group = skGroup;
			this.ModifiesStats = skModifysStat;
			this.RequiresPermission = skRequiresPermission;
		}

		public override bool CheckForReferences(ObjectClassBase Item)
		{
			return (this.RequiresPermission != Item ? false : true);
		}

		protected override void clearMyEvents()
		{
			base.clearMyEvents();
			this.EventCostChange = null;
			this.EventDescriptionChange = null;
			this.EventGroupChange = null;
			this.EventModifyStatChange = null;
			this.EventSpeciesChange = null;
			this.EventRequiredPermissionChange = null;
		}

		public override bool Equals(object obj)
		{
			bool requiresPermission = true;
			if ((obj == null ? true : !(obj is Skill)))
			{
				requiresPermission = false;
			}
			if (requiresPermission)
			{
				requiresPermission = this.RequiresPermission == ((Skill)obj).RequiresPermission;
			}
			if (requiresPermission)
			{
				requiresPermission = base.Name == ((Skill)obj).Name;
			}
			if (requiresPermission)
			{
				requiresPermission = base.ID == ((Skill)obj).ID;
			}
			if (requiresPermission)
			{
				requiresPermission = this.Description == ((Skill)obj).Description;
			}
			if (requiresPermission)
			{
				requiresPermission = this.Cost == ((Skill)obj).Cost;
			}
			if (requiresPermission)
			{
				requiresPermission = this.SpeciesCanUseSkill == ((Skill)obj).SpeciesCanUseSkill;
			}
			if (requiresPermission)
			{
				requiresPermission = this.Group == ((Skill)obj).Group;
			}
			if (requiresPermission)
			{
				requiresPermission = this.ModifiesStats == ((Skill)obj).ModifiesStats;
			}
			return requiresPermission;
		}

		public override DataMember[] getDataMembers()
		{
			string str = "";
			foreach (StatsMod modifiesStat in this.ModifiesStats)
			{
				str = string.Concat(str, modifiesStat.ToString(), ", ");
			}
			str = str.TrimEnd(new char[] { ',', ' ' });
			DataMember[] dataMember = new DataMember[] { new DataMember("Name", base.Name), new DataMember("Description", this.Description), new DataMember("Modifies Stats", str), null, null, null, null };
			SkillGroupFlag group = this.Group;
			dataMember[3] = new DataMember("Skill Group", group.ToString());
			Species speciesCanUseSkill = this.SpeciesCanUseSkill;
			dataMember[4] = new DataMember("Avalable to", speciesCanUseSkill.ToString());
			dataMember[5] = new DataMember("Permission", this.RequiresPermission.Name);
			dataMember[6] = new DataMember("Cost", (object)this.Cost);
			return dataMember;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool IsOfType(object obj)
		{
			return obj is Skill;
		}

		public override bool IsOfType(Type T)
		{
			return T == typeof(Skill);
		}

		public override void Link(ILink DataMaster)
		{
			this.RequiresPermission = (Permission)DataMaster.FindObject(this.RequiresPermission);
		}

		public override Type MyType()
		{
			return this.CollectionType;
		}

		public static bool operator ==(Skill a, Skill b)
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

		public static bool operator !=(Skill a, Skill b)
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
			string empty = string.Empty;
			element.AddTitleText(string.Concat("Skill: ", base.Name));
			element.AddCategoryText("Modifies Stats:");
			foreach (StatsMod modifiesStat in this.ModifiesStats)
			{
				empty = string.Concat(empty, modifiesStat.ToString(), ", ");
			}
			element.AddMText(empty.TrimEnd(new char[] { ',', ' ' }));
			element.AddBlankLine();
			element.AddCategoryText("Description:");
			element.AddMText(this.Description);
			element.AddBlankLine();
			element.AddCategoryText("Requires Permission: ", this.RequiresPermission.Name);
			element.AddBlankLine();
			element.AddCategoryText("Species avalibility: ", this.SpeciesCanUseSkill.ToString());
			element.AddBlankLine();
			element.AddCategoryText("Skill Group: ", this.Group.ToString());
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

		public static IComparer sortID_Asc()
		{
			return new Skill.sortIDHelper_ASC();
		}

		public static IComparer sortID_Dsc()
		{
			return new Skill.sortIDHelper_DSC();
		}

		public static IComparer sortName_Dsc()
		{
			return new Skill.sortNameHelper_DSC();
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
					name = string.Concat(name, base.Name, ", ");
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
					name = string.Concat(name, str);
				}
				if (Settings.Default.VerboseOther)
				{
					string[] strArrays = new string[] { name, "( ( ", null, null, null, null, null, null, null, null };
					strArrays[2] = this.SpeciesCanUseSkill.ToString();
					strArrays[3] = " ) ";
					strArrays[4] = this.Group.ToString();
					strArrays[5] = " ";
					strArrays[6] = this.Cost.ToString();
					strArrays[7] = " ";
					strArrays[8] = this.RequiresPermission.Name;
					strArrays[9] = " )";
					name = string.Concat(strArrays);
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
				if ((int)this.Group == 0)
				{
					fieldAndMessages.Add(new FieldAndMessage("Skill Group", "Skill must be associated with a group"));
				}
				if ((int)this.SpeciesCanUseSkill == 0)
				{
					fieldAndMessages.Add(new FieldAndMessage("Species", "At least one species need to be able to use the skill"));
				}
				if (this.RequiresPermission == null)
				{
					fieldAndMessages.Add(new FieldAndMessage("Required Permission", "Requires a value"));
				}
				if (this.Description == "")
				{
					fieldAndMessages1.Add(new FieldAndMessage("Description", "Should not be blank"));
				}
				if (this.Cost == 0)
				{
					fieldAndMessages1.Add(new FieldAndMessage("Cost", "Cost is 0"));
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

		public event NTEventHandler EventCostChange;

		public event NTEventHandler EventDescriptionChange;

		public event NTEventHandler EventGroupChange;

		public event NTEventHandler EventModifyStatChange;

		public event NTEventHandler EventRequiredPermissionChange;

		public event NTEventHandler EventRequiredPermissionChanged;

		public event NTEventHandler EventSpeciesChange;

		public override event NTEventHandler MyDataChanged;

		private class sortIDHelper_ASC : IComparer
		{
			public sortIDHelper_ASC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				Skill skill = (Skill)a;
				Skill skill1 = (Skill)b;
				return string.Compare(skill.ID, skill1.ID);
			}
		}

		private class sortIDHelper_DSC : IComparer
		{
			public sortIDHelper_DSC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				Skill skill = (Skill)a;
				return string.Compare(((Skill)b).ID, skill.ID);
			}
		}

		private class sortNameHelper_DSC : IComparer
		{
			public sortNameHelper_DSC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				Skill skill = (Skill)a;
				return string.Compare(((Skill)b).Name, skill.Name);
			}
		}
	}
}