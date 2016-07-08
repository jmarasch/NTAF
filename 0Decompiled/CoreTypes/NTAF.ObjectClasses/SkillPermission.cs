using NTAF.Core;
using NTAF.Core.Properties;
using NTAF.Permissions;
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
	[ObjectClassPlugIn("SkillPermission", "0.0.0.0")]
	[Serializable]
	public class SkillPermission : WSPPermission
	{
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
				return str;
			}
		}

		public override Type CollectionType
		{
			get
			{
				return typeof(SkillPermission);
			}
		}

		public SkillPermission()
		{
		}

		public SkillPermission(string pID, string pName, Species MyBaseRace)
		{
			base.ID = pID;
			base.Name = pName;
			this.SpeciesCanEquip = MyBaseRace;
		}

		public SkillPermission(string pID, string pName, Race MyRace)
		{
			base.ID = pID;
			base.Name = pName;
			this.RaceCanEquip = MyRace;
		}

		public SkillPermission(SkillPermission permission)
		{
			base.ID = permission.ID;
			base.Name = permission.Name;
			this.SpeciesCanEquip = permission.SpeciesCanEquip;
			this.RaceCanEquip = permission.RaceCanEquip;
		}

		public SkillPermission(string pID, string pName, Species MyBaseRace, Race MyRace)
		{
			base.ID = pID;
			base.Name = pName;
			this.SpeciesCanEquip = MyBaseRace;
			this.RaceCanEquip = MyRace;
		}

		public override bool CheckForReferences(ObjectClassBase Item)
		{
			return (this.RaceCanEquip != Item ? false : true);
		}

		protected override void clearMyEvents()
		{
			base.clearMyEvents();
			this.EventRaceChange = null;
		}

		public override bool Equals(object obj)
		{
			bool d = true;
			if ((obj == null ? true : !(obj is SkillPermission)))
			{
				d = false;
			}
			if (d)
			{
				d = base.ID == ((SkillPermission)obj).ID;
			}
			if (d)
			{
				d = base.Name == ((SkillPermission)obj).Name;
			}
			if (d)
			{
				d = this.RaceCanEquip == ((SkillPermission)obj).RaceCanEquip;
			}
			if (d)
			{
				d = this.SpeciesCanEquip == ((SkillPermission)obj).SpeciesCanEquip;
			}
			return d;
		}

		public override DataMember[] getDataMembers()
		{
			return new DataMember[] { new DataMember("Name", base.Name), new DataMember("Type", "Skill Permission"), new DataMember("Race Can Equip", this.RaceCanEquip), new DataMember("Species Can Equip", (object)this.SpeciesCanEquip) };
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool IsOfType(object obj)
		{
			return obj is SkillPermission;
		}

		public override bool IsOfType(Type T)
		{
			return T == typeof(SkillPermission);
		}

		public override void Link(ILink DataMaster)
		{
			if (this.RaceCanEquip != null)
			{
				this.RaceCanEquip = (Race)DataMaster.FindObject(this.RaceCanEquip);
			}
		}

		public override Type MyType()
		{
			return this.CollectionType;
		}

		public static bool operator ==(SkillPermission a, SkillPermission b)
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

		public static bool operator !=(SkillPermission a, SkillPermission b)
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
			element.AddTitleText(string.Concat("Skill Permission: ", base.Name));
			if (this.RaceCanEquip == null)
			{
				element.AddCategoryText("For Species", this.SpeciesCanEquip.ToString());
			}
			else
			{
				element.AddCategoryText("For race", this.RaceCanEquip.Name);
			}
			element.AddBlankLine();
		}

		public override void ReplaceReferences(ObjectClassBase ObjectToReplace, ObjectClassBase ReplaceWith)
		{
			if (this.RaceCanEquip == ObjectToReplace)
			{
				this.RaceCanEquip = (Race)ReplaceWith;
			}
		}

		public static IComparer sortIDAsc()
		{
			return new SkillPermission.sortIDHelper_ASC();
		}

		public static IComparer sortIDDsc()
		{
			return new SkillPermission.sortIDHelper_DSC();
		}

		public static IComparer sortNameDsc()
		{
			return new SkillPermission.sortNameHelpe_DSC();
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

		public override event NTEventHandler EventRaceChange;

		private class sortIDHelper_ASC : IComparer
		{
			public sortIDHelper_ASC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				Permission permission = (Permission)a;
				Permission permission1 = (Permission)b;
				return string.Compare(permission.ID, permission1.ID);
			}
		}

		private class sortIDHelper_DSC : IComparer
		{
			public sortIDHelper_DSC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				Permission permission = (Permission)a;
				return string.Compare(((Permission)b).ID, permission.ID);
			}
		}

		private class sortNameHelpe_DSC : IComparer
		{
			public sortNameHelpe_DSC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				Permission permission = (Permission)a;
				return string.Compare(((Permission)b).Name, permission.Name);
			}
		}
	}
}