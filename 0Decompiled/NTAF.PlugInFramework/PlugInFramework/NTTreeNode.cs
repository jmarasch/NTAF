using NTAF.Core;
using System;
using System.Collections;
using System.Windows.Forms;

namespace NTAF.PlugInFramework
{
	/// <summary>
	/// TreeNode object specifically for the NTTreeView conrol
	/// </summary>
	public class NTTreeNode : TreeNode, IDictionaryEnumerator, IEnumerator
	{
		/// <summary>
		/// </summary>
		private DictionaryEntry nodeEntry;

		/// <summary>
		/// </summary>
		private IEnumerator enumerator;

		/// <summary>
		/// gets the current enumerator for the node
		/// </summary>
		public object Current
		{
			get
			{
				return this.enumerator.Current;
			}
		}

		/// <summary>
		/// gets KeyValue Pair for this node
		/// </summary>
		public DictionaryEntry Entry
		{
			get
			{
				return this.nodeEntry;
			}
		}

		/// <summary>
		/// returns the key value of the node
		/// </summary>
		public object Key
		{
			get
			{
				return this.nodeEntry.Key;
			}
		}

		/// <summary>
		/// Gets or sets the key value to enable node lookup
		/// </summary>
		public string NodeKey
		{
			get
			{
				return this.nodeEntry.Key.ToString();
			}
			set
			{
				this.nodeEntry.Key = value;
			}
		}

		/// <summary>
		/// Gets or sets the node value
		/// </summary>
		public object NodeValue
		{
			get
			{
				return this.nodeEntry.Value;
			}
			set
			{
				if (this.nodeEntry.Value is INTName)
				{
					try
					{
						((INTName)this.nodeEntry.Value).EventNameChanged -= new NTEventHandler<NameChangeArgs>(this.myNodeValueNameChanged);
					}
					catch
					{
					}
				}
				this.nodeEntry.Value = value;
				base.Text = value.ToString();
				if (this.nodeEntry.Value is INTName)
				{
					((INTName)this.nodeEntry.Value).EventNameChanged += new NTEventHandler<NameChangeArgs>(this.myNodeValueNameChanged);
				}
			}
		}

		/// <summary>
		/// returns the value of the node
		/// </summary>
		public object Value
		{
			get
			{
				return this.nodeEntry.Value;
			}
		}

		/// <summary>
		/// Creates an instance of a NewTerra TreeNode
		/// </summary>
		public NTTreeNode()
		{
			this.enumerator = base.Nodes.GetEnumerator();
		}

		/// <summary>
		/// Creates an instance of a NewTerra TreeNode and sets the NodeValue
		/// </summary>
		/// <param name="obj">Object to store in NodeValue</param>
		public NTTreeNode(object obj)
		{
			this.enumerator = base.Nodes.GetEnumerator();
			this.NodeValue = obj;
		}

		/// <summary>
		/// Advances the enumerator
		/// </summary>
		/// <returns>true if the enumerator was successfully advanced to the next control;
		/// false if the enumerator has passed the end of the collection. </returns>
		public bool MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		/// <summary>
		/// Gets fired when the name of the containing type changes
		/// </summary>
		/// <param name="args"></param>
		private void myNodeValueNameChanged(NameChangeArgs args)
		{
			base.Text = this.nodeEntry.Value.ToString();
		}

		/// <summary>
		/// resets the enumerator
		/// </summary>
		public void Reset()
		{
			this.enumerator.Reset();
		}
	}
}