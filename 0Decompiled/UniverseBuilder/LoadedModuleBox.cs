using NTAF.PlugInFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using UniverseBuilder.Properties;

namespace UniverseBuilder
{
	internal class LoadedModuleBox : Form
	{
		private IContainer components = null;

		private TableLayoutPanel tableLayoutPanelx;

		private TextBox tbLoadedMods;

		private Button okButton;

		private PictureBox logoPictureBox;

		public LoadedModuleBox()
		{
			this.InitializeComponent();
			this.tbLoadedMods.Lines = this.getAssembliesAndPlugins();
		}

		protected override void Dispose(bool disposing)
		{
			if ((!disposing ? false : this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private string[] getAssembliesAndPlugins()
		{
			object asmTypAttrib;
			int j;
			int k;
			List<string> RetVal = new List<string>();
			Assembly[] assemblyArray = PluginEngine.LoadedAssemblies();
			for (int i = 0; i < (int)assemblyArray.Length; i++)
			{
				Assembly asm = assemblyArray[i];
				object[] asmAttribs = asm.GetCustomAttributes(true);
				string Designer = string.Empty;
				string Contact = string.Empty;
				string URL = string.Empty;
				string Title = string.Empty;
				string Description = string.Empty;
				string Company = string.Empty;
				string CopyRight = string.Empty;
				string TM = string.Empty;
				string ASMVersion = string.Empty;
				string FileVersion = string.Empty;
				object[] objArray = asmAttribs;
				for (j = 0; j < (int)objArray.Length; j++)
				{
					object obj = objArray[j];
					if (obj is PluginDesigner)
					{
						Designer = obj.ToString();
					}
					if (obj is PluginDesignerContact)
					{
						Contact = obj.ToString();
					}
					if (obj is PluginDesignerWebUrl)
					{
						URL = obj.ToString();
					}
					if (obj is AssemblyTitleAttribute)
					{
						Title = ((AssemblyTitleAttribute)obj).Title;
					}
					if (obj is AssemblyDescriptionAttribute)
					{
						Description = ((AssemblyDescriptionAttribute)obj).Description;
					}
					if (obj is AssemblyCompanyAttribute)
					{
						Company = ((AssemblyCompanyAttribute)obj).Company;
					}
					if (obj is AssemblyCopyrightAttribute)
					{
						CopyRight = ((AssemblyCopyrightAttribute)obj).Copyright;
					}
					if (obj is AssemblyTrademarkAttribute)
					{
						TM = ((AssemblyTrademarkAttribute)obj).Trademark;
					}
					if (obj is AssemblyVersionAttribute)
					{
						ASMVersion = ((AssemblyVersionAttribute)obj).Version;
					}
					if (obj is AssemblyFileVersionAttribute)
					{
						FileVersion = ((AssemblyFileVersionAttribute)obj).Version;
					}
				}
				if (Title != "")
				{
					RetVal.Add(string.Concat("Assembly File: ", Title));
				}
				if (Designer != "")
				{
					RetVal.Add(string.Concat("   Designer: ", Designer));
				}
				if (Contact != "")
				{
					RetVal.Add(string.Concat("   Contact: ", Contact));
				}
				if (URL != "")
				{
					RetVal.Add(string.Concat("   Designer URL:", URL));
				}
				if (ASMVersion != "")
				{
					RetVal.Add(string.Concat("   Assembly Version: ", ASMVersion));
				}
				if (FileVersion != "")
				{
					RetVal.Add(string.Concat("   File Version: ", FileVersion));
				}
				if (Company != "")
				{
					RetVal.Add(string.Concat("   Company: ", Company));
				}
				if (CopyRight != "")
				{
					RetVal.Add(string.Concat("   CopyRight: ", CopyRight));
				}
				if (TM != "")
				{
					RetVal.Add(string.Concat("   TradeMark: ", ASMVersion));
				}
				if (Description != "")
				{
					RetVal.Add(string.Concat("   Description: ", Description));
				}
				RetVal.Add("");
				RetVal.Add("   Contained Plugins");
				RetVal.Add("");
				Type[] asmTypes = asm.GetTypes();
				List<string> PluginTypes = new List<string>();
				Type[] typeArray = asmTypes;
				for (j = 0; j < (int)typeArray.Length; j++)
				{
					Type typ = typeArray[j];
					object[] asmTypAttribs = typ.GetCustomAttributes(typeof(ObjectClassPlugIn), true);
					if ((int)asmTypAttribs.Length >= 1)
					{
						objArray = asmTypAttribs;
						for (k = 0; k < (int)objArray.Length; k++)
						{
							asmTypAttrib = objArray[k];
							PluginTypes.Add(string.Concat("      ", ((ObjectClassPlugIn)asmTypAttrib).ToString()));
						}
					}
					asmTypAttribs = typ.GetCustomAttributes(typeof(OCCPlugIn), true);
					if ((int)asmTypAttribs.Length >= 1)
					{
						objArray = asmTypAttribs;
						for (k = 0; k < (int)objArray.Length; k++)
						{
							asmTypAttrib = objArray[k];
							PluginTypes.Add(string.Concat("      ", ((OCCPlugIn)asmTypAttrib).ToString()));
						}
					}
					asmTypAttribs = typ.GetCustomAttributes(typeof(EditorPlugIn), true);
					if ((int)asmTypAttribs.Length >= 1)
					{
						objArray = asmTypAttribs;
						for (k = 0; k < (int)objArray.Length; k++)
						{
							asmTypAttrib = objArray[k];
							PluginTypes.Add(string.Concat("      ", ((EditorPlugIn)asmTypAttrib).ToString()));
						}
					}
					asmTypAttribs = typ.GetCustomAttributes(typeof(TreeNodePlugIn), true);
					if ((int)asmTypAttribs.Length >= 1)
					{
						objArray = asmTypAttribs;
						for (k = 0; k < (int)objArray.Length; k++)
						{
							asmTypAttrib = objArray[k];
							PluginTypes.Add(string.Concat("      ", ((TreeNodePlugIn)asmTypAttrib).ToString()));
						}
					}
				}
				PluginTypes.Sort();
				RetVal.AddRange(PluginTypes.ToArray());
				RetVal.Add("");
			}
			return RetVal.ToArray();
		}

		private void InitializeComponent()
		{
			this.tableLayoutPanelx = new TableLayoutPanel();
			this.okButton = new Button();
			this.tbLoadedMods = new TextBox();
			this.logoPictureBox = new PictureBox();
			this.tableLayoutPanelx.SuspendLayout();
			((ISupportInitialize)this.logoPictureBox).BeginInit();
			base.SuspendLayout();
			this.tableLayoutPanelx.ColumnCount = 2;
			this.tableLayoutPanelx.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33f));
			this.tableLayoutPanelx.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 67f));
			this.tableLayoutPanelx.Controls.Add(this.okButton, 1, 1);
			this.tableLayoutPanelx.Controls.Add(this.tbLoadedMods, 1, 0);
			this.tableLayoutPanelx.Controls.Add(this.logoPictureBox, 0, 0);
			this.tableLayoutPanelx.Dock = DockStyle.Fill;
			this.tableLayoutPanelx.Location = new Point(9, 9);
			this.tableLayoutPanelx.Name = "tableLayoutPanelx";
			this.tableLayoutPanelx.RowCount = 2;
			this.tableLayoutPanelx.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanelx.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
			this.tableLayoutPanelx.Size = new System.Drawing.Size(417, 265);
			this.tableLayoutPanelx.TabIndex = 0;
			this.okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.okButton.Location = new Point(339, 239);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 24;
			this.okButton.Text = "&OK";
			this.tbLoadedMods.Dock = DockStyle.Fill;
			this.tbLoadedMods.Location = new Point(143, 3);
			this.tbLoadedMods.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
			this.tbLoadedMods.Multiline = true;
			this.tbLoadedMods.Name = "tbLoadedMods";
			this.tbLoadedMods.ReadOnly = true;
			this.tbLoadedMods.ScrollBars = ScrollBars.Both;
			this.tbLoadedMods.Size = new System.Drawing.Size(271, 229);
			this.tbLoadedMods.TabIndex = 23;
			this.tbLoadedMods.TabStop = false;
			this.tbLoadedMods.Text = "LoadedMods";
			this.logoPictureBox.Dock = DockStyle.Fill;
			this.logoPictureBox.Image = Resources.Galaxy_AboutScreen;
			this.logoPictureBox.Location = new Point(3, 3);
			this.logoPictureBox.Name = "logoPictureBox";
			this.tableLayoutPanelx.SetRowSpan(this.logoPictureBox, 2);
			this.logoPictureBox.Size = new System.Drawing.Size(131, 259);
			this.logoPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
			this.logoPictureBox.TabIndex = 12;
			this.logoPictureBox.TabStop = false;
			base.AcceptButton = this.okButton;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(435, 283);
			base.Controls.Add(this.tableLayoutPanelx);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "LoadedModuleBox";
			base.Padding = new System.Windows.Forms.Padding(9);
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Loaded Modules...";
			this.tableLayoutPanelx.ResumeLayout(false);
			this.tableLayoutPanelx.PerformLayout();
			((ISupportInitialize)this.logoPictureBox).EndInit();
			base.ResumeLayout(false);
		}
	}
}