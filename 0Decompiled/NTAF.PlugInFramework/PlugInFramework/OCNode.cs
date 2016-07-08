using NTAF.Core;
using System;
using System.Windows.Forms;

namespace NTAF.PlugInFramework
{
	public class OCNode : TreeNode
	{
		private ObjectClassBase i_ObjectClass = null;

		public ObjectClassBase ObjectClass
		{
			get
			{
				return this.i_ObjectClass;
			}
			set
			{
				if (this.i_ObjectClass != null)
				{
					this.i_ObjectClass.EventNameChanged -= new NTEventHandler<NameChangeArgs>(this.i_ObjectClass_EventNameChanged);
				}
				this.i_ObjectClass = value;
				this.i_ObjectClass.EventNameChanged += new NTEventHandler<NameChangeArgs>(this.i_ObjectClass_EventNameChanged);
				base.Text = this.i_ObjectClass.Name;
			}
		}

		public OCNode()
		{
		}

		private void i_ObjectClass_EventNameChanged(NameChangeArgs args)
		{
			base.Text = this.i_ObjectClass.Name;
		}
	}
}