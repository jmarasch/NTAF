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
	[ObjectClassPlugIn("Psy", "0.0.0.0")]
	[Serializable]
	[XmlInclude(typeof(PsyPermission))]
	public class Psy : ObjectClassBase, IRequiresPermission
	{
		private string _Description;

		private string _Effect;

		private string _TemplateSize;

		private PsyPermission _RequiresPermission;

		private ushort _ppCost;

		private ushort _Range;

		private template _Template;

		private PsyGroup _PsyType;

		[Browsable(false)]
		[XmlIgnore]
		public override string aboutMe
		{
			get
			{
				ushort num;
				string str = base.aboutMe;
				if (this.Template != template.None)
				{
					string[] templateSize = new string[] { str, null, null, null, null };
					templateSize[1] = this.Template.ToString();
					templateSize[2] = "(";
					templateSize[3] = this.TemplateSize;
					templateSize[4] = ")\n";
					str = string.Concat(templateSize);
				}
				if ((int)this.PsyTypes == 0)
				{
					num = this.ppCost;
					str = string.Concat(str, "PP: ", num.ToString(), "\n");
				}
				else
				{
					string[] strArrays = new string[] { str, "PP: ", null, null, null, null };
					num = this.ppCost;
					strArrays[2] = num.ToString();
					strArrays[3] = "    Type: ";
					strArrays[4] = this.PsyTypes.ToString();
					strArrays[5] = "\n";
					str = string.Concat(strArrays);
				}
				str = string.Concat(str, "Range: ", (this.Range == 0 ? "Base to Base" : this.Range.ToString()), "\n");
				if (this.Effect != "")
				{
					str = string.Concat(str, "Effect:\n", GeneralOperations.WrapLength(this.Effect, 50), "\n\n");
				}
				if (this.Description != "")
				{
					str = string.Concat(str, "Description:\n", GeneralOperations.WrapLength(this.Description, 50), "\n");
				}
				if (this.RequiresPermission != null)
				{
					str = string.Concat(str, "Required Permission: ", this.RequiresPermission.Name, "\n");
				}
				return str;
			}
		}

		public override Type CollectionType
		{
			get
			{
				return typeof(Psy);
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

		[Category("Stats")]
		[Description("")]
		[XmlIgnore]
		public string Distance
		{
			get
			{
				string str;
				str = (this._Range > 0 ? string.Concat("Ranged Attack(", this._Range.ToString(), "\")") : "Touch Attack");
				return str;
			}
		}

		[Category("About")]
		[Description("")]
		public string Effect
		{
			get
			{
				return this._Effect;
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				this._Effect = value;
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventEffectChange != null)
					{
						this.EventEffectChange();
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
		public ushort ppCost
		{
			get
			{
				return this._ppCost;
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				this._ppCost = value;
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventPPCostChange != null)
					{
						this.EventPPCostChange();
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
		public PsyGroup PsyTypes
		{
			get
			{
				return this._PsyType;
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				this._PsyType = value;
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventPsyTypeChange != null)
					{
						this.EventPsyTypeChange();
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
		public ushort Range
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
					if (this.EventRangeChange != null)
					{
						this.EventRangeChange();
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
				if ((value is PsyPermission ? false : value != null))
				{
					throw new ArgumentException("When setting this permission it must be of the PsyPermission Type");
				}
				this._RequiresPermission = (PsyPermission)value;
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

		[Category("Type")]
		[Description("")]
		[XmlAttribute]
		public template Template
		{
			get
			{
				return this._Template;
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				this._Template = value;
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventTemplateChange != null)
					{
						this.EventTemplateChange();
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		[Category("Size")]
		[Description("")]
		[XmlAttribute]
		public string TemplateSize
		{
			get
			{
				return this._TemplateSize;
			}
			set
			{
				if (!Settings.Default.Loading && base.myOwner is ILockable && ((ILockable)base.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				this._TemplateSize = value;
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventTemplateSizeChange != null)
					{
						this.EventTemplateSizeChange();
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		public Psy()
		{
			base.ID = "";
			base.Name = "";
			this.ppCost = 0;
			this.Range = 0;
			this.Effect = "";
			this.Description = "";
			this.Template = template.None;
			this.TemplateSize = "";
			this.PsyTypes = PsyGroup.Generic;
			this.RequiresPermission = null;
		}

		public Psy(Psy var)
		{
			base.ID = var.ID;
			base.Name = var.Name;
			this.ppCost = var.ppCost;
			this.Range = var.Range;
			this.Effect = var.Effect;
			this.Description = var.Description;
			this.Template = var.Template;
			this.TemplateSize = var.TemplateSize;
			this.PsyTypes = var.PsyTypes;
			this.RequiresPermission = var.RequiresPermission;
		}

		public Psy(string PsyID, string PsyDescription, string PsyEffect, string PsyName, ushort PsyppCost, string PsyTemplateSize, ushort PsyRange, template PsyTemplate, PsyGroup PsyPsyType, PsyPermission PsyRequiredPermission)
		{
			base.ID = PsyID;
			base.Name = PsyName;
			this.ppCost = PsyppCost;
			this.Range = PsyRange;
			this.Effect = PsyEffect;
			this.Description = PsyDescription;
			this.Template = PsyTemplate;
			this.TemplateSize = PsyTemplateSize;
			this.PsyTypes = PsyPsyType;
			this.RequiresPermission = PsyRequiredPermission;
		}

		public override bool CheckForReferences(ObjectClassBase Item)
		{
			return (this.RequiresPermission != Item ? false : true);
		}

		protected override void clearMyEvents()
		{
			base.clearMyEvents();
			this.EventDescriptionChange = null;
			this.EventEffectChange = null;
			this.EventPPCostChange = null;
			this.EventPsyTypeChange = null;
			this.EventRangeChange = null;
			this.EventRequiredPermissionChanged = null;
			this.EventTemplateChange = null;
			this.EventTemplateSizeChange = null;
		}

		public override bool Equals(object obj)
		{
			bool d = true;
			if ((obj == null ? true : !(obj is Psy)))
			{
				d = false;
			}
			if (d)
			{
				d = base.ID == ((Psy)obj).ID;
			}
			if (d)
			{
				d = base.Name == ((Psy)obj).Name;
			}
			if (d)
			{
				d = this.Description == ((Psy)obj).Description;
			}
			if (d)
			{
				d = this.RequiresPermission == ((Psy)obj).RequiresPermission;
			}
			if (d)
			{
				d = this.Effect == ((Psy)obj).Effect;
			}
			if (d)
			{
				d = this.ppCost == ((Psy)obj).ppCost;
			}
			if (d)
			{
				d = this.Range == ((Psy)obj).Range;
			}
			if (d)
			{
				d = this.Template == ((Psy)obj).Template;
			}
			if (d)
			{
				d = this.TemplateSize == ((Psy)obj).TemplateSize;
			}
			if (d)
			{
				d = this.PsyTypes == ((Psy)obj).PsyTypes;
			}
			return d;
		}

		public override DataMember[] getDataMembers()
		{
			return new DataMember[] { new DataMember("Name", base.Name), new DataMember("PP Cost", (object)this.ppCost), new DataMember("Range", (object)this.Range), new DataMember("Template", (object)this.Template), new DataMember("Template Size", this.TemplateSize), new DataMember("Psy Type", (object)this.PsyTypes), new DataMember("Discription", this.Description), new DataMember("Effect", this.Effect), new DataMember("Requires Permission", this.RequiresPermission) };
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool IsOfType(object obj)
		{
			return obj is Psy;
		}

		public override bool IsOfType(Type T)
		{
			return T == typeof(Psy);
		}

		public override void Link(ILink DataMaster)
		{
			this.RequiresPermission = (Permission)DataMaster.FindObject(this.RequiresPermission);
		}

		public override Type MyType()
		{
			return this.CollectionType;
		}

		public static bool operator ==(Psy a, Psy b)
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

		public static bool operator !=(Psy a, Psy b)
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
			string str;
			string empty = string.Empty;
			element.AddTitleText(string.Concat("Psy: ", base.Name));
			PrintElement printElement = element;
			if (this.Range != 0)
			{
				ushort range = this.Range;
				str = string.Concat(range.ToString(), "\"");
			}
			else
			{
				str = "Direct Contact";
			}
			printElement.AddText(string.Concat("Range: ", str));
			element.AddText(string.Format("Template(Size): {0}{1}", this.Template, string.Concat(this.TemplateSize.ToString(), "\"")));
			element.AddText(string.Format("Psy Type: {0}    P.P. Cost: {1}", this.PsyTypes, this.ppCost));
			element.AddBlankLine();
			element.AddCategoryText("Description:");
			element.AddMText(this.Description);
			element.AddBlankLine();
			element.AddCategoryText("Effect:");
			element.AddMText(this.Effect);
			element.AddBlankLine();
			element.AddCategoryText("Requires Permission: ", this.RequiresPermission.Name);
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
			return new Psy.sortIDHelper_ASC();
		}

		public static IComparer sortID_Dsc()
		{
			return new Psy.sortIDHelper_DSC();
		}

		public static IComparer sortName_Dsc()
		{
			return new Psy.sortNameHelper_DSC();
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
				if (this._ppCost == 0)
				{
					fieldAndMessages.Add(new FieldAndMessage("PP Cost", "All Psy abilities use some PP"));
				}
				if (this._RequiresPermission == null)
				{
					fieldAndMessages.Add(new FieldAndMessage("Required Permission", "Requires a value"));
				}
				if (!this._Template.Is<template>(template.None) & (this.TemplateSize != ""))
				{
					fieldAndMessages.Add(new FieldAndMessage("Template Size", "Cannot have a value if a template is not used"));
				}
				if (this._Effect == "")
				{
					fieldAndMessages1.Add(new FieldAndMessage("Effect", "Should not be blank"));
				}
				if (this._Description == "")
				{
					fieldAndMessages1.Add(new FieldAndMessage("Description", "Should not be blank"));
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

		public event NTEventHandler EventDescriptionChange;

		public event NTEventHandler EventEffectChange;

		public event NTEventHandler EventPPCostChange;

		public event NTEventHandler EventPsyTypeChange;

		public event NTEventHandler EventRangeChange;

		public event NTEventHandler EventRequiredPermissionChanged;

		public event NTEventHandler EventTemplateChange;

		public event NTEventHandler EventTemplateSizeChange;

		public override event NTEventHandler MyDataChanged;

		private class sortIDHelper_ASC : IComparer
		{
			public sortIDHelper_ASC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				Psy psy = (Psy)a;
				Psy psy1 = (Psy)b;
				return string.Compare(psy.ID, psy1.ID);
			}
		}

		private class sortIDHelper_DSC : IComparer
		{
			public sortIDHelper_DSC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				Psy psy = (Psy)a;
				return string.Compare(((Psy)b).ID, psy.ID);
			}
		}

		private class sortNameHelper_DSC : IComparer
		{
			public sortNameHelper_DSC()
			{
			}

			int System.Collections.IComparer.Compare(object a, object b)
			{
				Psy psy = (Psy)a;
				return string.Compare(((Psy)b).Name, psy.Name);
			}
		}
	}
}