using NTAF.Core;
using NTAF.Core.Properties;
using NTAF.PrintEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Threading;
using System.Xml.Serialization;

namespace NTAF.PlugInFramework
{
	/// <summary>
	/// The lowest level complex type for NewTerra applications
	/// Interfaces:
	/// IAboutMe, ICloneable, IComparable, IMemberCopy
	/// INTId, INTName, IOwner, IPrintable, 
	/// IValidate, 
	/// </summary>
	public class ObjectClassBase : INTId, INTName, IOwner, ICloneable, IComparable, IAboutMe, IValidate, IPrintable, IMemberCopy
	{
		private string _ID;

		private string _Name;

		private object owner = null;

		/// <summary>
		/// Gets a string of root basic details about the object.
		/// Override this method and first call base.aboutMe and then
		/// add your extra field information. This method is important
		/// in that if print is unavalable, this will typically get called
		/// in its place.
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public virtual string aboutMe
		{
			get
			{
				string retVal = string.Empty;
				if (this.ID != "")
				{
					retVal = string.Concat(retVal, "ID: ", this.ID, "\n");
				}
				if (this.Name != "")
				{
					retVal = string.Concat(retVal, "Name: ", this.Name, "\n");
				}
				return retVal;
			}
		}

		/// <summary>
		/// Gets the name of the class based on the ObjectClassPlugIn attribute
		/// </summary>
		public string CollectionName
		{
			get
			{
				string retval = "";
				object[] myAtts = this.GetType().GetCustomAttributes(typeof(ObjectClassPlugIn), true);
				if ((int)myAtts.Length >= 1)
				{
					retval = ((ObjectClassPlugIn)myAtts[0]).Name;
				}
				return retval;
			}
		}

		/// <summary>
		/// Gets the type of this object
		/// </summary>
		/// <returns>This objects type</returns>
		public virtual Type CollectionType
		{
			get
			{
				return this.GetType();
			}
		}

		/// <summary>
		/// Gets or sets the objects Unque ID. All ids need to consist of a 4 alpha numeric prefix
		/// controlled by its file data prefix id and typically an 8 Hex ID the prefix/id
		/// is used to makesure that even if two items should ever share the same id in different files
		/// they will never be cross confused due to the prefix.
		/// <example>
		/// BSDS54D457A6
		/// BSDS is the id prefix and 54D457A6 is the id inside the file
		/// </example>
		/// </summary>
		[Category("ID")]
		[Description("Unique ID used to distiguish this items from others like it, Must contain the 4 digit Prefix and an 8 digit hex number after it")]
		[XmlAttribute]
		public string ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				this._ID = value;
			}
		}

		/// <summary>
		/// Gets the objects prefix identifier, see ID for more information
		/// </summary>
		[Category("ID")]
		[Description("4 digit alpha numeric identifier so the objecs origen can be traced")]
		[XmlIgnore]
		public string IDPreFix
		{
			get
			{
				return this._ID.Substring(0, 4);
			}
		}

		/// <summary>
		/// The data file that this object belongs to
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public object myOwner
		{
			get
			{
				return this.owner;
			}
			set
			{
				this.owner = value;
			}
		}

		/// <summary>
		/// Gets or sets the name of the object
		/// </summary>
		[Category("ID")]
		[Description("Common Name given to this object")]
		[XmlAttribute]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if (!Settings.Default.Loading && this.myOwner is ILockable && ((ILockable)this.myOwner).FileLocked)
				{
					throw new FileLockedException("File is locked, and cannot be edited.");
				}
				string oldName = this._Name;
				this._Name = value;
				if ((Settings.Default.Loading ? false : !Settings.Default.Updating))
				{
					if (this.EventNameChanged != null)
					{
						this.EventNameChanged(new NameChangeArgs(this._Name, oldName, this._ID));
					}
					if (this.MyDataChanged != null)
					{
						this.MyDataChanged();
					}
				}
			}
		}

		public ObjectClassBase()
		{
		}

		/// <summary>
		/// Must be overridden if the object level is 1 or higher for the ClassCollector,
		/// Checks to find out if a reference to the passed object exists in this object
		/// </summary>
		/// <param name="Item">Object were looking for references for</param>
		/// <returns>True if a reference exists, False if no reference is found</returns>
		public virtual bool CheckForReferences(ObjectClassBase Item)
		{
			return false;
		}

		/// <summary>
		/// Clears all the basic events this object could have, you must override
		/// is baseic operation if you plan on adding cusom events, when overriding
		/// this method you should call base.clearMyEvents() prior to any event clearing
		/// your method will do.
		/// </summary>
		protected virtual void clearMyEvents()
		{
			this.MyDataChanged = null;
			this.EventNameChanged = null;
		}

		/// <summary>
		/// creates a shallow clone of this object
		/// </summary>
		/// <returns>An object identical to this one</returns>
		public object Clone()
		{
			object retVal = this.MemberwiseClone();
			((ObjectClassBase)retVal).clearMyEvents();
			return retVal;
		}

		/// <summary>
		/// copys all read/write fields from one object to this one
		/// </summary>
		/// <param name="members">The object thats having its properties copied</param>
		public void CopyMembers(object members)
		{
			bool flag;
			List<PropertyInfo> MyProps = new List<PropertyInfo>(this.GetType().GetProperties());
			foreach (PropertyInfo PI in new List<PropertyInfo>(members.GetType().GetProperties()))
			{
				if (!MyProps.Contains(PI))
				{
					flag = true;
				}
				else
				{
					flag = (!PI.CanRead ? true : !PI.CanWrite);
				}
				if (!flag)
				{
					MyProps[MyProps.IndexOf(PI)].SetValue(this, PI.GetValue(members, null), null);
				}
			}
			List<FieldInfo> MyFields = new List<FieldInfo>(this.GetType().GetFields());
			foreach (FieldInfo FI in new List<FieldInfo>(members.GetType().GetFields()))
			{
				if ((!MyFields.Contains(FI) || !FI.IsPublic ? false : !FI.IsStatic))
				{
					try
					{
						MyFields[MyFields.IndexOf(FI)].SetValue(this, FI.GetValue(members));
					}
					catch (ReadOnlyException readOnlyException)
					{
					}
				}
			}
			this.clearMyEvents();
		}

		/// <summary>
		/// not implimented not used, may be used in the future
		/// </summary>
		/// <param name="PathOfSavedObject"></param>
		/// <returns></returns>
		public virtual object Deserialize(string PathOfSavedObject)
		{
			throw new NotImplementedException();
		}

		public virtual DataMember[] getDataMembers()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// checks to see if this object is of testing type
		/// </summary>
		/// <param name="obj">Object to test</param>
		/// <returns>true if they match false if the dont match</returns>
		public virtual bool IsOfType(object obj)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// checks to see if this object is of testing type
		/// </summary>
		/// <param name="T">Type to test</param>
		/// <returns>true if they match false if the dont match</returns>
		public virtual bool IsOfType(Type T)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Must be overridden if the object level is 1 or higher for the ClassCollector
		/// this instructs the object on how to find complex reference objects and create
		/// a link to them
		/// </summary>
		/// <param name="DataMaster"></param>
		public virtual void Link(ILink DataMaster)
		{
		}

		/// <summary>
		/// Gets the type of this object
		/// </summary>
		/// <returns>This objects type</returns>
		public virtual Type MyType()
		{
			return this.GetType();
		}

		/// <summary>
		/// Needs to be overriden if you want the object to print anything
		/// see NewTerra Print Framework documentation for details
		/// </summary>
		/// <param name="element"></param>
		public virtual void Print(PrintElement element)
		{
		}

		/// <summary>
		/// Must be overridden if the object level is 1 or higher for the ClassCollector
		/// this instructs the object on how to find complex reference objects and create
		/// a link to them
		/// </summary>
		/// <param name="ObjectToReplace"></param>
		/// <param name="ReplaceWith"></param>
		public virtual void ReplaceReferences(ObjectClassBase ObjectToReplace, ObjectClassBase ReplaceWith)
		{
		}

		/// <summary>
		/// not implimented not used, may be used in the future
		/// </summary>
		/// <param name="toSerial"></param>
		/// <param name="tempPath"></param>
		/// <returns></returns>
		public virtual string Serialize(object toSerial, string tempPath)
		{
			throw new NotImplementedException();
		}

		int System.IComparable.CompareTo(object obj)
		{
			ObjectClassBase chk = (ObjectClassBase)obj;
			return string.Compare(this.Name, chk.Name);
		}

		/// <summary>
		/// Override this method call the base version then simply finish validating the
		/// item by checking for null referenced fields, when a field/property
		/// thats required for the object to work properly is found blank/empty/null
		/// or what ever state that would not be good or the object do a
		/// throw new ValidationExecption("this is whats wrong");
		/// </summary>
		/// <exception cref="T:NTAF.Core.ValidationException">Sends back informaton on whats
		/// wrong with the object</exception>
		public virtual void Valid()
		{
			List<FieldAndMessage> ThrowList = new List<FieldAndMessage>();
			if (this._ID == "")
			{
				ThrowList.Add(new FieldAndMessage("ID", "Cannot be blank"));
			}
			if (this._Name == "")
			{
				ThrowList.Add(new FieldAndMessage("Name", "Cannot be blank"));
			}
			if (ThrowList.Count >= 1)
			{
				throw new ValidationException(ThrowList.ToArray());
			}
		}

		/// <summary>
		/// Fires when a name change occurs
		/// </summary>
		public event NTEventHandler<NameChangeArgs> EventNameChanged;

		/// <summary>
		/// Occurs when data has changed
		/// </summary>
		public virtual event NTEventHandler MyDataChanged;
	}
}