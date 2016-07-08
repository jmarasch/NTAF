using NTAF.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace NTAF.PlugInFramework
{
	/// <summary>
	/// This class is to be used as a base inheritance over "Form" when creating an ObjectClass editor
	/// class will also require the EditroPlugIn attribute
	/// </summary>
	public class OCEditorBase : Form
	{
		private EditorMode _Mode = EditorMode.New;

		private ObjectClassBase i_MyObject = null;

		private List<OCCBase> i_Collectors = new List<OCCBase>();

		private EditorExitCode i_ExitCode = EditorExitCode.Cancel;

		private bool i_Loading = false;

		/// <summary>
		/// Override Required
		/// Returns the type of the collection
		/// </summary>
		[ReadOnly(true)]
		public virtual Type CollectionType
		{
			get
			{
				return base.GetType();
			}
		}

		/// <summary>
		/// Gets and sets the Collectors that an editor can read from to pull needed reference information
		/// </summary>
		public OCCBase[] Collectors
		{
			get
			{
				return this.i_Collectors.ToArray();
			}
			set
			{
				if (this.i_Collectors.Count <= 0)
				{
					this.i_Collectors.Clear();
				}
				this.i_Collectors.AddRange(value);
			}
		}

		/// <summary>
		/// Checks the EditorPlugin atribute and reports if the editor is a graphical editor
		/// </summary>
		public bool Graphical
		{
			get
			{
				bool retVal = false;
				List<object> myAtts = new List<object>(base.GetType().GetCustomAttributes(typeof(EditorPlugIn), true));
				if (myAtts.Count >= 1)
				{
					retVal = ((EditorPlugIn)myAtts[0]).isGUI;
				}
				return retVal;
			}
		}

		/// <summary>
		/// Gets or sets the current mode of the editor
		/// </summary>
		public EditorMode Mode
		{
			get
			{
				return this._Mode;
			}
			set
			{
				if (this.Mode == EditorMode.ReadOnly)
				{
					throw new ReadOnlyException("Editor has been set to Read-Only and cannot be changed");
				}
				if ((value == EditorMode.New ? true : value == EditorMode.Edit))
				{
					this.editing(true);
				}
				if ((value == EditorMode.ReadOnly ? true : value == EditorMode.View))
				{
					this.editing(false);
				}
			}
		}

		/// <summary>
		/// Gets and sets the object currently contained within the editor.
		/// when an object is sent it needs to be striped of subscribed events
		/// and have just the values copied using the IMemberCopy interface
		/// </summary>
		public ObjectClassBase MyObject
		{
			get
			{
				return this.i_MyObject;
			}
			set
			{
				if (!this.IEdit(value.GetType()))
				{
					throw new InvalidParameter("Object is not of the correct type, setting of \"MyObject\" aborted");
				}
				this.i_MyObject = (ObjectClassBase)Activator.CreateInstance(value.GetType());
				((IMemberCopy)this.i_MyObject).CopyMembers(value);
			}
		}

		/// <summary>
		/// Gets or sets( proected ) the editors exits status
		/// </summary>
		public EditorExitCode MyResult
		{
			get
			{
				return this.i_ExitCode;
			}
			protected set
			{
				this.i_ExitCode = value;
			}
		}

		public OCEditorBase()
		{
		}

		/// <summary>
		/// cancels the current editing by setting the result code
		/// and closing the form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void button_Cancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.i_ExitCode = EditorExitCode.Cancel;
			base.Close();
		}

		/// <summary>
		/// sets the Mode of the form to edit
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void button_Edit_Click(object sender, EventArgs e)
		{
			this.Mode = EditorMode.Edit;
		}

		/// <summary>
		/// Calls for vaallidation of the object using IValidate sets the proper exit code
		/// and closes the form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void button_Save_Click(object sender, EventArgs e)
		{
			if ((this.Mode == EditorMode.New ? false : this.Mode != EditorMode.Edit))
			{
				base.DialogResult = System.Windows.Forms.DialogResult.Ignore;
				this.i_ExitCode = EditorExitCode.Cancel;
				base.Close();
			}
			else
			{
				try
				{
					if (this.i_MyObject != null)
					{
						((IValidate)this.i_MyObject).Valid();
					}
					this.i_ExitCode = EditorExitCode.OK;
					base.DialogResult = System.Windows.Forms.DialogResult.OK;
					base.Close();
				}
				catch (ValidationWarning validationWarning)
				{
					if (MessageBox.Show(validationWarning.Message, "Possible object validation errors...", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
					{
						this.i_ExitCode = EditorExitCode.OK;
						base.DialogResult = System.Windows.Forms.DialogResult.OK;
						base.Close();
					}
				}
				catch (ValidationException validationException)
				{
					MessageBox.Show(validationException.Message, "Object validation errors...", MessageBoxButtons.OK);
				}
				catch (Exception exception)
				{
					MessageBox.Show(exception.Message);
				}
			}
		}

		/// <summary>
		/// Requires override, basse provides basic functionallity to set TextBoxes to readonly or not-readonly
		/// base will make a button named "btnEdit" visible or not visible depending on editing setting
		/// </summary>
		/// <param name="editing"></param>
		protected virtual void editing(bool editing)
		{
			foreach (Control ctr in base.Controls)
			{
				if (ctr is TextBox)
				{
					((TextBox)ctr).ReadOnly = !editing;
				}
				if ((!(ctr is Button) ? false : ctr.Name == "btnEdit"))
				{
					ctr.Visible = !editing;
				}
			}
		}

		/// <summary>
		/// Call base.Enter_field(sender,e) then overried the rest of the implementation
		/// based on how you want it to work for what ever field your entering
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void Enter_field(object sender, EventArgs e)
		{
			if (sender is Control)
			{
				if (sender is TextBox)
				{
					((TextBox)sender).SelectAll();
				}
			}
		}

		/// <summary>
		/// will call PopulateComboboxes and PopulateFields when the form loads
		/// </summary>
		protected virtual void FormLoad()
		{
			this.i_Loading = true;
			this.PopulateComboboxes();
			this.PopulateFields();
			this.i_Loading = false;
		}

		/// <summary>
		/// Easy way to tell events not to fire without unsubscribing and then re-subscribing
		/// </summary>
		/// <returns>true - if form is loading, false - if form is not loading</returns>
		protected bool FormLoading()
		{
			return this.i_Loading;
		}

		/// <summary>
		/// Tests to see if the editor can edit a specific type
		/// </summary>
		/// <param name="thisType">Type to test for editing</param>
		/// <returns>true if the type can be edited using this editor</returns>
		public bool IEdit(Type thisType)
		{
			bool flag;
			object[] atts = base.GetType().GetCustomAttributes(typeof(EditorPlugIn), true);
			if ((int)atts.Length >= 1)
			{
				Type[] edit = ((EditorPlugIn)atts[0]).IEdit;
				int num = 0;
				while (num < (int)edit.Length)
				{
					if (!(edit[num] == thisType))
					{
						num++;
					}
					else
					{
						flag = true;
						return flag;
					}
				}
			}
			flag = false;
			return flag;
		}

		/// <summary>
		/// Call base.Leave_field(sender,e) then overried the rest of the implementation
		/// if the field being left is a text box and of one of the standard fields
		/// it will auto set the field for the oject. check current documentation for
		/// what the current standard field names are and what they are for
		/// you will need to override this if you are using custom fields and you should
		/// be using custom fields if your creating a plugin.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void Leave_field(object sender, EventArgs e)
		{
			if (sender is Control)
			{
				if (sender is TextBox)
				{
					string name = ((Control)sender).Name;
					if (name != null)
					{
						if (name != "FIELD_Name")
						{
							if (name == "FIELD_ID")
							{
								if (this.i_MyObject != null)
								{
									((INTId)this.i_MyObject).ID = ((TextBox)sender).Text;
								}
							}
						}
						else if (this.i_MyObject != null)
						{
							((INTName)this.i_MyObject).Name = ((TextBox)sender).Text;
						}
					}
				}
			}
		}

		/// <summary>
		/// Should be overridden base function will populate comboboxes or lists with
		/// proper names check documentation of current fields for this version
		/// </summary>
		protected virtual void PopulateComboboxes()
		{
		}

		/// <summary>
		/// Should be overridden base function will populate fields with proper names
		/// check documentation of current fields for this version
		/// </summary>
		protected virtual void PopulateFields()
		{
			foreach (Control ctr in base.Controls)
			{
				if (ctr.Name == "FIELD_Name" && this.i_MyObject != null)
				{
					((TextBox)ctr).Text = ((INTName)this.i_MyObject).Name;
				}
				if (ctr.Name == "FIELD_ID" && this.i_MyObject != null)
				{
					((TextBox)ctr).Text = ((INTId)this.i_MyObject).ID;
				}
			}
		}

		/// <summary>
		/// this will run the editor, using the object previously attached to it using the MyObject 
		/// </summary>
		/// <param name="mode">editing mode</param>
		/// <returns>Editors exit code</returns>
		public virtual EditorExitCode RunEditor(EditorMode mode)
		{
			this.Mode = mode;
			this.FormLoad();
			base.ShowDialog();
			return this.i_ExitCode;
		}

		/// <summary>
		/// Sets the editors object and runs the editor
		/// </summary>
		/// <param name="setObject">Object to set</param>
		/// <param name="mode">editing mode</param>
		/// <returns>Editors exit code</returns>
		public virtual EditorExitCode RunEditor(ObjectClassBase setObject, EditorMode mode)
		{
			this.MyObject = setObject;
			return this.RunEditor(mode);
		}
	}
}