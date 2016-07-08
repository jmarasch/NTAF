using System;
using System.Windows.Forms;

namespace NTAF.PlugInFramework
{
	public class OCCNode : TreeNode
	{
		private OCCBase I_Collector = null;

		public OCCBase Collector
		{
			get
			{
				return this.I_Collector;
			}
			set
			{
				this.I_Collector = value;
				base.Text = this.I_Collector.CollectionName;
			}
		}

		public OCCNode()
		{
		}
	}
}