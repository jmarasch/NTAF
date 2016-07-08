using NTAF.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace NTAF.PlugInFramework
{
	/// <summary>
	/// The Object Class Tree Node Base provides the ability to create a TreeVew
	/// plugin class for the NTTreView Control.
	/// The only required override is CollectionType
	/// This class will require an apropriate OCCBase class to read and write to
	/// </summary>
	public class OCTreeNodeBase
	{
		private ContextMenuStrip i_RootMenu;

		private ContextMenuStrip i_NodeMenu;

		private List<OCCBase> i_ObjectCollector = new List<OCCBase>();

		private TreeNode i_Branch = new OCCNode();

		public string TreeName
		{
			get
			{
				string str;
				object[] atts = this.GetType().GetCustomAttributes(typeof(TreeNodePlugIn), true);
				str = ((int)atts.Length < 1 ? "Branch name is unable to be set" : ((TreeNodePlugIn)atts[0]).DisplayName);
				return str;
			}
		}

		public OCTreeNodeBase()
		{
		}

		/// <summary>
		/// Attaches a collector to this object so it can create the branch and leaves
		/// </summary>
		/// <param name="collector">Collector object to reference</param>
		public void AttachOCC(OCCBase collector)
		{
			if (!this.CanDisplay(collector.CollectionType))
			{
				throw new ArgumentException("Collector is not of the proper type and cannot be attached to this branch");
			}
			collector.CollectionUpdated += new NTEventHandler<ItemChangedArgs>(this.i_ObjectCollector_CollectionUpdated);
			this.i_ObjectCollector.Add(collector);
		}

		public bool CanDisplay(Type thisType)
		{
			bool flag;
			object[] atts = this.GetType().GetCustomAttributes(typeof(TreeNodePlugIn), true);
			if ((int)atts.Length >= 1)
			{
				Type[] display = ((TreeNodePlugIn)atts[0]).IDisplay;
				int num = 0;
				while (num < (int)display.Length)
				{
					if (!(display[num] == thisType))
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
		/// Internally creates a main branch to return to the tree
		/// </summary>
		public virtual void GrowBranch()
		{
			this.i_Branch.Nodes.Clear();
			if (this.i_ObjectCollector.Count != 0)
			{
				if (this.Updating != null)
				{
					this.Updating(new UpdaterEventArgs(this.i_ObjectCollector.Count));
				}
				int currentCount = this.i_ObjectCollector.Count;
				if (this.i_ObjectCollector.Count < 2)
				{
					this.i_Branch = new OCCNode();
					this.i_Branch.Nodes.AddRange(this.PopulateNode(this.i_ObjectCollector[0], currentCount, out currentCount));
					((OCCNode)this.i_Branch).Collector = this.i_ObjectCollector[0];
				}
				else
				{
					this.i_Branch = new TreeNode();
					foreach (OCCBase occ in this.i_ObjectCollector)
					{
						OCCNode SubRoot = new OCCNode()
						{
							Collector = occ
						};
						SubRoot.Nodes.AddRange(this.PopulateNode(occ, currentCount, out currentCount));
						if (this.i_RootMenu != null)
						{
							SubRoot.ContextMenuStrip = this.i_RootMenu;
						}
						this.i_Branch.Nodes.Add(SubRoot);
					}
				}
				this.i_Branch.Text = this.TreeName;
				if (this.Updated != null)
				{
					this.Updated();
				}
			}
			else
			{
				this.i_Branch = new TreeNode("Collector Error: no collector has been assigned to node");
			}
		}

		/// <summary>
		/// event that occurs when the collection has been changed
		/// </summary>
		/// <param name="args">ItemChangedArgs defines what happend</param>
		private void i_ObjectCollector_CollectionUpdated(ItemChangedArgs args)
		{
			int x;
			TreeNode tn = null;
			if (args.Action == ArgAction.Add)
			{
				OCNode newNode = new OCNode();
				bool added = false;
				if (this.i_NodeMenu != null)
				{
					newNode.ContextMenuStrip = this.i_NodeMenu;
				}
				newNode.ObjectClass = (ObjectClassBase)args.Item;
				if (this.i_ObjectCollector.Count >= 2)
				{
					foreach (OCCNode TN in this.i_Branch.Nodes)
					{
						if (!(TN.Collector.CollectionType == newNode.ObjectClass.CollectionType))
						{
							continue;
						}
						if (TN.Nodes.Count < 1)
						{
							TN.Nodes.Add(newNode);
						}
						else
						{
							x = 0;
							while (x <= TN.Nodes.Count - 1)
							{
								if (newNode.ObjectClass.Name.CompareTo(((OCNode)TN.Nodes[x]).ObjectClass.Name) > 0)
								{
									x++;
								}
								else
								{
									TN.Nodes.Insert(x, newNode);
									added = true;
									break;
								}
							}
							if (!added)
							{
								TN.Nodes.Add(newNode);
							}
						}
					}
				}
				else if (this.i_Branch.Nodes.Count < 1)
				{
					this.i_Branch.Nodes.Add(newNode);
				}
				else
				{
					x = 0;
					while (x <= this.i_Branch.Nodes.Count - 1)
					{
						if (newNode.ObjectClass.Name.CompareTo(((OCNode)this.i_Branch.Nodes[x]).ObjectClass.Name) > 0)
						{
							x++;
						}
						else
						{
							this.i_Branch.Nodes.Insert(x, newNode);
							added = true;
							break;
						}
					}
					if (!added)
					{
						this.i_Branch.Nodes.Add(newNode);
					}
				}
			}
			if (args.Action == ArgAction.Remove)
			{
				OCNode remove = null;
				if (this.i_ObjectCollector.Count < 2)
				{
					foreach (TreeNode tn in this.i_Branch.Nodes)
					{
						if (!(tn is OCNode) || ((OCNode)tn).ObjectClass != args.Item)
						{
							continue;
						}
						remove = (OCNode)tn;
					}
				}
				else
				{
					foreach (TreeNode TN in this.i_Branch.Nodes)
					{
						foreach (TreeNode node in TN.Nodes)
						{
							if (!(node is OCNode) || ((OCNode)node).ObjectClass != args.Item)
							{
								continue;
							}
							remove = (OCNode)node;
						}
					}
				}
				if (remove == null)
				{
					throw new Exception(string.Concat("Could not find the leaf in the branch ", this.i_Branch.Text, " for the object being removed ", (args.Item is INTName ? ((INTName)args.Item).Name : args.Item.ToString())));
				}
				this.i_Branch.Nodes.Remove(remove);
			}
			if (args.Action == ArgAction.Edit)
			{
			}
		}

		/// <summary>
		/// Creates and returns the main branch and all of its leaflings from the objects in the collector.
		/// </summary>
		/// <returns>A root node for the NTTreeView control</returns>
		/// <exception cref="T:System.Exception">Thrown when the ObjectClassCollector has not been set</exception>
		public TreeNode MainBranch()
		{
			if (this.i_ObjectCollector.Count <= 0)
			{
				throw new Exception("ObjectCalssCollector(s) has(have) not been set");
			}
			if (this.i_Branch.Nodes.Count <= 0)
			{
				this.GrowBranch();
			}
			if ((this.i_ObjectCollector.Count != 1 ? true : this.i_RootMenu == null))
			{
				this.i_Branch.ContextMenuStrip = null;
			}
			else
			{
				this.i_Branch.ContextMenuStrip = this.i_RootMenu;
			}
			return this.i_Branch;
		}

		private TreeNode[] PopulateNode(OCCBase occ, int InCount, out int OutCount)
		{
			List<TreeNode> retVal = new List<TreeNode>();
			object[] objects = occ.Objects;
			for (int i = 0; i < (int)objects.Length; i++)
			{
				ObjectClassBase obj = (ObjectClassBase)objects[i];
				OCNode newNode = new OCNode()
				{
					NodeFont = SystemFonts.DefaultFont
				};
				if (this.i_NodeMenu != null)
				{
					newNode.ContextMenuStrip = this.i_NodeMenu;
				}
				newNode.ObjectClass = obj;
				retVal.Add(newNode);
				if (this.Update != null)
				{
					int inCount = InCount;
					InCount = inCount + 1;
					this.Update(new UpdateProgressEventArgs(string.Concat("Updating Leaves In ", occ.CollectionName), "Updated", newNode.Text, inCount, this.i_ObjectCollector.Count));
				}
			}
			OutCount = InCount;
			return retVal.ToArray();
		}

		/// <summary>
		/// Use this to set the optional context menues for when a node is right clicked
		/// </summary>
		/// <param name="RootMenu"></param>
		/// <param name="NodeMenu"></param>
		public void SetMenus(ContextMenuStrip RootMenu, ContextMenuStrip NodeMenu)
		{
			this.i_RootMenu = RootMenu;
			this.i_NodeMenu = NodeMenu;
		}

		/// <summary>
		/// triggered when a step in the update process is compleeted
		/// </summary>
		public event NTEventHandler<UpdateProgressEventArgs> Update;

		/// <summary>
		/// trigered when update compleetes
		/// </summary>
		public event NTEventHandler Updated;

		/// <summary>
		/// triggered when the tree goes in to update mode
		/// </summary>
		public event NTEventHandler<UpdaterEventArgs> Updating;
	}
}