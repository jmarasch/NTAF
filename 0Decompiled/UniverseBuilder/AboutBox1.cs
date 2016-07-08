using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using UniverseBuilder.Properties;

namespace UniverseBuilder
{
	internal class AboutBox1 : Form
	{
		private IContainer components = null;

		private TableLayoutPanel tableLayoutPanel;

		private PictureBox logoPictureBox;

		private Label labelProductName;

		private Label labelCompanyName;

		private TextBox textBoxDescription;

		private Button okButton;

		private Label labelVersion;

		private Label labelCopyright;

		public string AssemblyCompany
		{
			get
			{
				string str;
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
				str = ((int)attributes.Length != 0 ? ((AssemblyCompanyAttribute)attributes[0]).Company : "");
				return str;
			}
		}

		public string AssemblyCopyright
		{
			get
			{
				string str;
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
				str = ((int)attributes.Length != 0 ? ((AssemblyCopyrightAttribute)attributes[0]).Copyright : "");
				return str;
			}
		}

		public string AssemblyDescription
		{
			get
			{
				string str;
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
				str = ((int)attributes.Length != 0 ? ((AssemblyDescriptionAttribute)attributes[0]).Description : "");
				return str;
			}
		}

		public string AssemblyProduct
		{
			get
			{
				string str;
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
				str = ((int)attributes.Length != 0 ? ((AssemblyProductAttribute)attributes[0]).Product : "");
				return str;
			}
		}

		public string AssemblyTitle
		{
			get
			{
				string title;
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
				if ((int)attributes.Length > 0)
				{
					AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
					if (titleAttribute.Title != "")
					{
						title = titleAttribute.Title;
						return title;
					}
				}
				title = Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
				return title;
			}
		}

		public string AssemblyVersion
		{
			get
			{
				string str = string.Concat(Assembly.GetExecutingAssembly().GetName().Version.ToString(), " ", Settings.Default.ReleaseStatus);
				return str;
			}
		}

		public AboutBox1()
		{
			this.InitializeComponent();
			this.Text = string.Format("About {0}", this.AssemblyTitle);
			this.labelProductName.Text = this.AssemblyProduct;
			this.labelVersion.Text = string.Format("Version {0}", this.AssemblyVersion);
			this.labelCopyright.Text = this.AssemblyCopyright;
			this.labelCompanyName.Text = this.AssemblyCompany;
			this.textBoxDescription.Text = this.AssemblyDescription;
		}

		protected override void Dispose(bool disposing)
		{
			if ((!disposing ? false : this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.tableLayoutPanel = new TableLayoutPanel();
			this.logoPictureBox = new PictureBox();
			this.labelProductName = new Label();
			this.labelVersion = new Label();
			this.labelCopyright = new Label();
			this.labelCompanyName = new Label();
			this.textBoxDescription = new TextBox();
			this.okButton = new Button();
			this.tableLayoutPanel.SuspendLayout();
			((ISupportInitialize)this.logoPictureBox).BeginInit();
			base.SuspendLayout();
			this.tableLayoutPanel.ColumnCount = 2;
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33f));
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 67f));
			this.tableLayoutPanel.Controls.Add(this.logoPictureBox, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.labelProductName, 1, 0);
			this.tableLayoutPanel.Controls.Add(this.labelVersion, 1, 1);
			this.tableLayoutPanel.Controls.Add(this.labelCopyright, 1, 2);
			this.tableLayoutPanel.Controls.Add(this.labelCompanyName, 1, 3);
			this.tableLayoutPanel.Controls.Add(this.textBoxDescription, 1, 4);
			this.tableLayoutPanel.Controls.Add(this.okButton, 1, 5);
			this.tableLayoutPanel.Dock = DockStyle.Fill;
			this.tableLayoutPanel.Location = new Point(9, 9);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 6;
			this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
			this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
			this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
			this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
			this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
			this.tableLayoutPanel.Size = new System.Drawing.Size(417, 265);
			this.tableLayoutPanel.TabIndex = 0;
			this.logoPictureBox.Dock = DockStyle.Fill;
			this.logoPictureBox.Image = Resources.Galaxy_AboutScreen;
			this.logoPictureBox.Location = new Point(3, 3);
			this.logoPictureBox.Name = "logoPictureBox";
			this.tableLayoutPanel.SetRowSpan(this.logoPictureBox, 6);
			this.logoPictureBox.Size = new System.Drawing.Size(131, 259);
			this.logoPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
			this.logoPictureBox.TabIndex = 12;
			this.logoPictureBox.TabStop = false;
			this.labelProductName.Dock = DockStyle.Fill;
			this.labelProductName.Location = new Point(143, 0);
			this.labelProductName.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
			this.labelProductName.MaximumSize = new System.Drawing.Size(0, 17);
			this.labelProductName.Name = "labelProductName";
			this.labelProductName.Size = new System.Drawing.Size(271, 17);
			this.labelProductName.TabIndex = 19;
			this.labelProductName.Text = "Product Name";
			this.labelProductName.TextAlign = ContentAlignment.MiddleLeft;
			this.labelVersion.Dock = DockStyle.Fill;
			this.labelVersion.Location = new Point(143, 26);
			this.labelVersion.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
			this.labelVersion.MaximumSize = new System.Drawing.Size(0, 17);
			this.labelVersion.Name = "labelVersion";
			this.labelVersion.Size = new System.Drawing.Size(271, 17);
			this.labelVersion.TabIndex = 0;
			this.labelVersion.Text = "Version";
			this.labelVersion.TextAlign = ContentAlignment.MiddleLeft;
			this.labelCopyright.Dock = DockStyle.Fill;
			this.labelCopyright.Location = new Point(143, 52);
			this.labelCopyright.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
			this.labelCopyright.MaximumSize = new System.Drawing.Size(0, 17);
			this.labelCopyright.Name = "labelCopyright";
			this.labelCopyright.Size = new System.Drawing.Size(271, 17);
			this.labelCopyright.TabIndex = 21;
			this.labelCopyright.Text = "Copyright";
			this.labelCopyright.TextAlign = ContentAlignment.MiddleLeft;
			this.labelCompanyName.Dock = DockStyle.Fill;
			this.labelCompanyName.Location = new Point(143, 78);
			this.labelCompanyName.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
			this.labelCompanyName.MaximumSize = new System.Drawing.Size(0, 17);
			this.labelCompanyName.Name = "labelCompanyName";
			this.labelCompanyName.Size = new System.Drawing.Size(271, 17);
			this.labelCompanyName.TabIndex = 22;
			this.labelCompanyName.Text = "Company Name";
			this.labelCompanyName.TextAlign = ContentAlignment.MiddleLeft;
			this.textBoxDescription.Dock = DockStyle.Fill;
			this.textBoxDescription.Location = new Point(143, 107);
			this.textBoxDescription.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
			this.textBoxDescription.Multiline = true;
			this.textBoxDescription.Name = "textBoxDescription";
			this.textBoxDescription.ReadOnly = true;
			this.textBoxDescription.ScrollBars = ScrollBars.Both;
			this.textBoxDescription.Size = new System.Drawing.Size(271, 126);
			this.textBoxDescription.TabIndex = 23;
			this.textBoxDescription.TabStop = false;
			this.textBoxDescription.Text = "Description";
			this.okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.okButton.Location = new Point(339, 239);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 24;
			this.okButton.Text = "&OK";
			base.AcceptButton = this.okButton;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(435, 283);
			base.Controls.Add(this.tableLayoutPanel);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "AboutBox1";
			base.Padding = new System.Windows.Forms.Padding(9);
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "AboutBox1";
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			((ISupportInitialize)this.logoPictureBox).EndInit();
			base.ResumeLayout(false);
		}
	}
}