using NTAF.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Threading;

namespace NTAF.PlugInFramework
{
	/// <summary>
	/// The Object Collector Base Class provides all the functionality needed to create
	/// a functioning collector for any class that inherits IObjectClass.
	/// to use this class you only need to override Collection Name, and CollectionType
	/// the only time its required to override objectLevel is when your 
	/// not creating a root or layer 0 IObjectClass  
	/// </summary>
	public class OCCBase
	{
		private List<object> i_Colector = new List<object>();

		/// <summary>
		/// Override Required
		/// Returns the collection Name
		/// </summary>
		[ReadOnly(true)]
		public string CollectionName
		{
			get
			{
				string str;
				object[] typAtts = this.GetType().GetCustomAttributes(typeof(OCCPlugIn), true);
				str = ((int)typAtts.Length < 1 ? "UnKnow Collection Type" : ((OCCPlugIn)typAtts[0]).Name);
				return str;
			}
		}

		/// <summary>
		/// Override Required
		/// Returns the type of the collection
		/// </summary>
		[ReadOnly(true)]
		public virtual Type CollectionType
		{
			get
			{
				return typeof(object);
			}
		}

		/// <summary>
		/// counts the number of objects in the collector
		/// </summary>
		/// <returns>number of objects in the collector</returns>
		[ReadOnly(true)]
		public virtual int Count
		{
			get
			{
				return this.i_Colector.Count;
			}
		}

		/// <summary>
		/// Returns the object at a specified index
		/// </summary>
		/// <param name="index">Index of the object</param>
		/// <returns>Object from the list</returns>
		public virtual object this[int index]
		{
			get
			{
				return this.i_Colector[index];
			}
			set
			{
				if (!this.IsOfType(value))
				{
					throw new InvalidParameter("Object type incorrect");
				}
				if ((index < 0 ? true : index >= this.Count))
				{
					throw new IndexOutOfRangeException("Index was out side the bounds of the collector");
				}
				this.i_Colector[index] = value;
			}
		}

		/// <summary>
		/// searches for the object by a passed object
		/// </summary>
		/// <param name="Item"></param>
		/// <returns>matching object from the Collector</returns>
		public virtual object this[object Item]
		{
			get
			{
				if (!this.IsOfType(Item))
				{
					throw new InvalidParameter("Object not of the correct type");
				}
				object retVal = null;
				if (this.Count >= 1)
				{
					int index = this.FindIndex(Item);
					if (index != -1)
					{
						retVal = this.i_Colector[index];
					}
				}
				return retVal;
			}
		}

		/// <summary>
		/// searches for the object by a key field method
		/// </summary>
		/// <param name="Key">Search perammiter</param>
		/// <param name="Field">Filed to search in</param>
		/// <returns></returns>
		public virtual object this[string Key, SearchField Field]
		{
			get
			{
				object retVal = null;
				if (this.Count >= 1)
				{
					retVal = this.i_Colector[this.FindIndex(Key, Field)];
				}
				return retVal;
			}
		}

		/// <summary>
		/// Override Required is level is above 0 or root
		/// Returns the layerd level of this object collection
		/// </summary>
		public virtual byte objectLayer
		{
			get
			{
				return (byte)0;
			}
		}

		/// <summary>
		/// Returns all the object that this Collector holds
		/// </summary>
		public virtual object[] Objects
		{
			get
			{
				return this.i_Colector.ToArray();
			}
		}

		public OCCBase()
		{
		}

		/// <summary>
		/// add an object to the collector
		/// </summary>
		/// <param name="Item">Item to add. Must be of the same type of collector or it will throw an invalid peramiter exception</param>
		public virtual void AddObject(object Item)
		{
			if (!this.IsOfType(Item))
			{
				throw new InvalidParameter("Object not of the correct type");
			}
			int newIndex = this.Count;
			this.i_Colector.Add(Item);
			if (this.CollectionUpdated != null)
			{
				this.CollectionUpdated(new ItemChangedArgs(newIndex, Item, ArgAction.Add));
			}
		}

		/// <summary>
		/// add multiple objects to the collector
		/// </summary>
		/// <param name="Items">Items to add. Must be of the same type of collector or it will throw an invalid peramiter exception</param>
		public virtual void AddObject(object[] Items)
		{
			object obj;
			int i;
			object[] items = Items;
			for (i = 0; i < (int)items.Length; i++)
			{
				obj = items[i];
				if (!this.IsOfType(obj))
				{
					throw new InvalidParameter("Object not of the correct type");
				}
			}
			items = Items;
			for (i = 0; i < (int)items.Length; i++)
			{
				obj = items[i];
				int newIndex = this.Count;
				this.i_Colector.Add(obj);
				if (this.CollectionUpdated != null)
				{
					this.CollectionUpdated(new ItemChangedArgs(newIndex, obj, ArgAction.Add));
				}
			}
		}

		/// <summary>
		/// add multiple objects to the collector
		/// </summary>
		/// <param name="Item">Items to add. Must be of the same type of collector or it will throw an invalid peramiter exception</param>
		/// <param name="atIndex">Index at which to insert the object</param>
		public virtual void AddObject(object Item, int atIndex)
		{
			if (!this.IsOfType(Item))
			{
				throw new InvalidParameter("Object not of the correct type");
			}
			this.i_Colector.Insert(atIndex, Item);
			if (this.CollectionUpdated != null)
			{
				this.CollectionUpdated(new ItemChangedArgs(atIndex, Item, ArgAction.Add));
			}
		}

		/// <summary>
		/// add multiple objects to the collector
		/// </summary>
		/// <param name="Items">Items to add. Must be of the same type of collector or it will throw an invalid peramiter exception</param>
		/// <param name="atIndex">Index at which to insert the objects</param>
		public virtual void AddObject(object[] Items, int atIndex)
		{
			object obj;
			int i;
			object[] items = Items;
			for (i = 0; i < (int)items.Length; i++)
			{
				obj = items[i];
				if (!this.IsOfType(obj))
				{
					throw new InvalidParameter("Object not of the correct type");
				}
			}
			items = Items;
			for (i = 0; i < (int)items.Length; i++)
			{
				obj = items[i];
				int newIndex = this.Count;
				this.i_Colector.Insert(atIndex, obj);
				if (this.CollectionUpdated != null)
				{
					this.CollectionUpdated(new ItemChangedArgs(newIndex, obj, ArgAction.Add));
				}
				atIndex++;
			}
		}

		/// <summary>
		/// clears all items from the collector.
		/// </summary>
		public virtual void Clear()
		{
			this.DropObject((new List<object>(this.i_Colector)).ToArray());
		}

		/// <summary>
		/// removes an object from the collector
		/// </summary>
		/// <param name="Item">object to remove. Must be of the same type of collector or it will throw an invalid peramiter exception</param>
		public virtual void DropObject(ObjectClassBase Item)
		{
			if (!this.IsOfType(Item))
			{
				throw new InvalidParameter("Object not of the correct type");
			}
			if (!this.Exists(Item))
			{
				throw new ItemException("Could not drop object, it doesnt exist in the list");
			}
			int oldIndex = this.i_Colector.IndexOf(Item);
			this.i_Colector.Remove(Item);
			if (this.CollectionUpdated != null)
			{
				this.CollectionUpdated(new ItemChangedArgs(oldIndex, Item, ArgAction.Remove));
			}
		}

		/// <summary>
		/// removes multiple objects from the collector
		/// </summary>
		/// <param name="Items">objects to remove. Must be of the same type of collector or it will throw an invalid peramiter exception</param>
		public virtual void DropObject(object[] Items)
		{
			object obj;
			int i;
			object[] items = Items;
			for (i = 0; i < (int)items.Length; i++)
			{
				obj = items[i];
				if (!this.IsOfType(obj))
				{
					throw new InvalidParameter("Object not of the correct type");
				}
			}
			items = Items;
			for (i = 0; i < (int)items.Length; i++)
			{
				obj = items[i];
				int oldIndex = this.FindIndex(obj);
				this.i_Colector.Remove(obj);
				if (this.CollectionUpdated != null)
				{
					this.CollectionUpdated(new ItemChangedArgs(oldIndex, obj, ArgAction.Remove));
				}
			}
		}

		/// <summary>
		/// remove an object from the collector
		/// </summary>
		/// <param name="atIndex">Index at which to insert the object</param>
		public virtual void DropObject(int atIndex)
		{
			if ((atIndex >= this.Count ? true : atIndex < 0))
			{
				throw new IndexOutOfRangeException("Index was outside the bounds of the collector");
			}
			object Removed = this.i_Colector[atIndex];
			this.i_Colector.RemoveAt(atIndex);
			if (this.CollectionUpdated != null)
			{
				this.CollectionUpdated(new ItemChangedArgs(atIndex, Removed, ArgAction.Remove));
			}
		}

		/// <summary>
		/// Edits the object in the collector with the the passed in new values
		/// </summary>
		/// <param name="toEdit">Item to edit</param>
		/// <param name="newValues">Items New Values</param>
		public virtual void EditObject(object toEdit, object newValues)
		{
			if (!this.IsOfType(toEdit))
			{
				throw new InvalidParameter("editing object not of the correct type");
			}
			if (!this.IsOfType(newValues))
			{
				throw new InvalidParameter("New Values object must be of the same type");
			}
			if (!(this[toEdit] is IMemberCopy))
			{
				throw new InvalidOperationException("Plugin doesnot support editing, IMemberCopy functionallity not found");
			}
			((IMemberCopy)this[toEdit]).CopyMembers(newValues);
			if (this.CollectionUpdated != null)
			{
				this.CollectionUpdated(new ItemChangedArgs(0, newValues, ArgAction.Edit));
			}
		}

		/// <summary>
		/// Checks to see of the collector allreaddy holds atleast one of the specified objects
		/// </summary>
		/// <param name="Item">Item to check for</param>
		/// <returns>true/false</returns>
		public virtual bool Exists(object Item)
		{
			if (!this.IsOfType(Item))
			{
				throw new InvalidParameter("Object not of the correct type");
			}
			return this.i_Colector.Contains(Item);
		}

		/// <summary>
		/// Finds the index of the specified item
		/// </summary>
		/// <param name="Item">item to find index for</param>
		/// <returns>int index of the found item or -1 if it cannot find the item</returns>
		public virtual int FindIndex(object Item)
		{
			if (!this.IsOfType(Item))
			{
				throw new InvalidParameter("Item is not of the correct Type");
			}
			return this.i_Colector.IndexOf(Item);
		}

		/// <summary>
		/// Searched the collector based on a key and field.
		/// </summary>
		/// <param name="Key">Data to match</param>
		/// <param name="Field">Field to search</param>
		/// <returns>int index of the found item or -1 if it cannot find the item</returns>
		public virtual int FindIndex(string Key, SearchField Field)
		{
			bool flag;
			int retVal = -1;
			if (this.Count < 1)
			{
				flag = true;
			}
			else
			{
				flag = (!(this.i_Colector[0] is INTId) ? true : !(this.i_Colector[0] is INTName));
			}
			if (!flag)
			{
				for (int i = 0; i < this.Count - 1; i++)
				{
					if ((((INTId)this.i_Colector[i]).ID != Key ? false : Field == SearchField.ID) | (((INTName)this.i_Colector[i]).Name != Key ? false : Field == SearchField.Name))
					{
						retVal = i;
					}
				}
			}
			return retVal;
		}

		/// <summary>
		/// Returns an enumerator that iterates over the collection
		/// </summary>
		/// <returns>Returns an enumerator that iterates over the collection</returns>
		public List<object>.Enumerator GetEnumerator()
		{
			return this.i_Colector.GetEnumerator();
		}

		/// <summary>
		/// Checks a passed in obj and determins if it matches the type of this collector
		/// </summary>
		/// <param name="obj">Type of the class to check</param>
		/// <returns>true/false</returns>
		public virtual bool IsOfType(object obj)
		{
			return obj.GetType() == this.CollectionType;
		}

		/// <summary>
		/// Checks a passed in obj and determins if it matches the type of this collector
		/// </summary>
		/// <param name="T">Type of the class to check</param>
		/// <returns>true/false</returns>
		public virtual bool IsOfType(Type T)
		{
			return T == this.CollectionType;
		}

		/// <summary>
		/// fires when the collection is updated
		/// </summary>
		public event NTEventHandler<ItemChangedArgs> CollectionUpdated;
	}
}