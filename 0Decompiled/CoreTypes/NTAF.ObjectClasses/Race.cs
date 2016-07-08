using NTAF.Core;
using NTAF.Core.Properties;
using NTAF.PlugInFramework;
using NTAF.PrintEngine;
using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml.Serialization;

namespace NTAF.ObjectClasses
{
	[ObjectClassPlugIn("Race", "0.9.0.0")]
	[Serializable]
	public class Race : ObjectClassBase
	{
		private Species _species;

		public override string aboutMe
		{
			get
			{
				string str = base.aboutMe;
				str = string.Concat(str, string.Format("Base Race: {0}", this.species));
				return str;
			}
		}

		public override Type CollectionType
		{
			get
			{
				return typeof(Race);
			}
		}

		[Category("Base")]
		[Description("")]
		[XmlAttribute]
		public Species species
		{
			get
			{
				return this._species;
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				this._species = value;
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

		[Browsable(false)]
		[XmlIgnore]
		public string SpeciesToString
		{
			get
			{
				return this._species.ToString();
			}
		}

		public Race()
		{
			base.ID = "";
			base.Name = "";
			this.species = Species.All;
		}

		public Race(Race var)
		{
			base.ID = var.ID;
			base.Name = var.Name;
			this.species = var.species;
		}

		public Race(string rID, string rName, Species rBaseRace)
		{
			base.ID = rID;
			base.Name = rName;
			this.species = rBaseRace;
		}

		protected override void clearMyEvents()
		{
			base.clearMyEvents();
			this.EventSpeciesChange = null;
		}

		public override bool Equals(object obj)
		{
			bool d = true;
			if ((obj == null ? true : !(obj is Race)))
			{
				d = false;
			}
			if (d)
			{
				d = base.ID == ((Race)obj).ID;
			}
			if (d)
			{
				d = base.Name == ((Race)obj).Name;
			}
			if (d)
			{
				d = this.species == ((Race)obj).species;
			}
			return d;
		}

		public override DataMember[] getDataMembers()
		{
			DataMember[] dataMember = new DataMember[] { new DataMember("Name", base.Name), null };
			Species species = this.species;
			dataMember[1] = new DataMember("Species", species.ToString());
			return dataMember;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool IsOfType(object obj)
		{
			return obj is Race;
		}

		public override bool IsOfType(Type T)
		{
			return T == typeof(Race);
		}

		public override Type MyType()
		{
			return this.CollectionType;
		}

		public static bool operator ==(Race a, Race b)
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

		public static bool operator !=(Race a, Race b)
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
			element.AddTitleText(string.Concat("Race: ", base.Name));
			element.AddCategoryText("Species: ", this.species.ToString());
			element.AddBlankLine();
		}

		public static IComparer sortID_Asc()
		{
			return new Race.sortIDHelper_ASC();
		}

		public static IComparer sortID_Dsc()
		{
			return new Race.sortIDHelper_DSC();
		}

		public static IComparer sortName_Dsc()
		{
			return new Race.sortNameHelper_DSC();
		}

		public override string ToString()
		{
			string str = string.Concat(this.species, ":", base.Name);
			if (Settings.Default.VerboseToString)
			{
				str = string.Concat(new object[] { base.ID, ", ", this.species, ":", base.Name });
			}
			return str;
		}

		public event NTEventHandler EventSpeciesChange;

		public override event NTEventHandler MyDataChanged;

		private class sortIDHelper_ASC : IComparer
		{
			public sortIDHelper_ASC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				Race race = (Race)a;
				Race race1 = (Race)b;
				return string.Compare(race.ID, race1.ID);
			}
		}

		private class sortIDHelper_DSC : IComparer
		{
			public sortIDHelper_DSC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				Race race = (Race)a;
				return string.Compare(((Race)b).ID, race.ID);
			}
		}

		private class sortNameHelper_DSC : IComparer
		{
			public sortNameHelper_DSC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				Race race = (Race)a;
				return string.Compare(((Race)b).Name, race.Name);
			}
		}
	}
}