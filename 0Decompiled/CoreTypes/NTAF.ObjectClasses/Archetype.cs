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
	[ObjectClassPlugIn("Archetype", "0.0.0.0")]
	[Serializable]
	public class Archetype : ObjectClassBase
	{
		private ArchetypeBaseEnu _BaseType;

		[Browsable(false)]
		[XmlIgnore]
		public override string aboutMe
		{
			get
			{
				string str = base.aboutMe;
				str = string.Concat(str, string.Format("Base Type: {0}", this.BaseType));
				return str;
			}
		}

		[Browsable(false)]
		[XmlIgnore]
		public string BaseToStirng
		{
			get
			{
				return GeneralOperations.GetDescription<ArchetypeBaseEnu>(this.BaseType).Split(new char[] { ',' })[0];
			}
		}

		[Category("Base")]
		[Description("The major type that defines the Archetype")]
		[XmlAttribute]
		public ArchetypeBaseEnu BaseType
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

		[Category("Base")]
		[Description("This is what it costs to be this archetype")]
		[XmlIgnore]
		public int CostModifier
		{
			get
			{
				int num = short.Parse(GeneralOperations.GetDescription<ArchetypeBaseEnu>(this.BaseType).Split(new char[] { ',' })[1]);
				return num;
			}
		}

		[Category("Base")]
		[Description("The archetypes starting experience value")]
		[XmlIgnore]
		public string StartingEXP
		{
			get
			{
				return GeneralOperations.GetDescription<ArchetypeBaseEnu>(this.BaseType).Split(new char[] { ',' })[2];
			}
		}

		public Archetype()
		{
			base.ID = "";
			base.Name = "";
			this.BaseType = ArchetypeBaseEnu.New;
		}

		public Archetype(Archetype var)
		{
			base.ID = var.ID;
			base.Name = var.Name;
			this.BaseType = var.BaseType;
		}

		public Archetype(string utID, string utName, ArchetypeBaseEnu utBaseType)
		{
			base.ID = utID;
			base.Name = utName;
			this.BaseType = utBaseType;
		}

		protected override void clearMyEvents()
		{
			base.clearMyEvents();
			this.EventBaseTypeChanged = null;
		}

		public override object Deserialize(string PathOfSavedObject)
		{
			throw new NotImplementedException();
		}

		public override bool Equals(object obj)
		{
			bool d = true;
			if ((obj == null ? true : !(obj is Archetype)))
			{
				d = false;
			}
			if (d)
			{
				d = base.ID == ((Archetype)obj).ID;
			}
			if (d)
			{
				d = base.Name == ((Archetype)obj).Name;
			}
			if (d)
			{
				d = this.BaseType == ((Archetype)obj).BaseType;
			}
			return d;
		}

		public override DataMember[] getDataMembers()
		{
			return new DataMember[] { new DataMember("Name", base.Name), new DataMember("Type", this.BaseToStirng) };
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool IsOfType(object obj)
		{
			return obj is Archetype;
		}

		public override bool IsOfType(Type T)
		{
			return T == typeof(Archetype);
		}

		public override void Link(ILink DataMaster)
		{
		}

		public override Type MyType()
		{
			return this.CollectionType;
		}

		public static bool operator ==(Archetype a, Archetype b)
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

		public static bool operator !=(Archetype a, Archetype b)
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
			element.AddTitleText(string.Concat("Archetype: ", base.Name));
			element.AddCategoryText("Base Type: ", this.BaseToStirng);
			element.AddCategoryText("Cost Modifier: ", this.CostModifier.ToString());
			element.AddCategoryText("Starting EXP: ", this.StartingEXP);
			element.AddBlankLine();
		}

		public override string Serialize(object toSerial, string tempPath)
		{
			throw new NotImplementedException();
		}

		public static IComparer sortID_Asc()
		{
			return new Archetype.sortIDHelper_ASC();
		}

		public static IComparer sortID_Dsc()
		{
			return new Archetype.sortIDHelper_DSC();
		}

		public static IComparer sortName_Dsc()
		{
			return new Archetype.sortNameHelper_DSC();
		}

		public override string ToString()
		{
			ArchetypeBaseEnu baseType = this.BaseType;
			string str = string.Concat(baseType.ToString(), ":", base.Name);
			if (Settings.Default.VerboseToString)
			{
				str = string.Concat(base.ID, ": ", base.Name);
			}
			return str;
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
				if (this._BaseType.Is<ArchetypeBaseEnu>(ArchetypeBaseEnu.New))
				{
					fieldAndMessages.Add(new FieldAndMessage("Base Type", "Has not been set"));
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

		public override event NTEventHandler MyDataChanged;

		private class sortIDHelper_ASC : IComparer
		{
			public sortIDHelper_ASC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				Archetype archetype = (Archetype)a;
				Archetype archetype1 = (Archetype)b;
				return string.Compare(archetype.ID, archetype1.ID);
			}
		}

		private class sortIDHelper_DSC : IComparer
		{
			public sortIDHelper_DSC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				Archetype archetype = (Archetype)a;
				return string.Compare(((Archetype)b).ID, archetype.ID);
			}
		}

		private class sortNameHelper_DSC : IComparer
		{
			public sortNameHelper_DSC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				Archetype archetype = (Archetype)a;
				return string.Compare(((Archetype)b).Name, archetype.Name);
			}
		}
	}
}