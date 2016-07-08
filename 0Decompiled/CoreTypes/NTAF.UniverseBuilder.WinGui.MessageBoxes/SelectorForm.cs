using NTAF.Core;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace NTAF.UniverseBuilder.WinGui.MessageBoxes
{
	public class SelectorForm : Form
	{
		private IContainer components = null;

		private Button btnOK;

		private Button btnCancle;

		private Label lblMessage;

		private ComboBox cbxSelected;

		private Button btnInfo;

		private Label label1;

		public string btnCancleval
		{
			set
			{
				this.btnCancle.Text = value;
			}
		}

		public string btnOKval
		{
			set
			{
				this.btnOK.Text = value;
			}
		}

		public string Message
		{
			set
			{
				this.lblMessage.Text = value;
			}
		}

		public object Selected
		{
			get
			{
				return this.cbxSelected.SelectedItem;
			}
		}

		public object SelectedObject
		{
			get
			{
				return this.cbxSelected.SelectedItem;
			}
		}

		public object[] SelectionObjArray
		{
			set
			{
				this.cbxSelected.Items.AddRange(value.ToArray<object>());
			}
		}

		public string Title
		{
			set
			{
				this.Text = value;
			}
		}

		public SelectorForm()
		{
			this.InitializeComponent();
			this.btnOKval = "OK";
			this.btnCancleval = "Cancle";
			this.Message = "Select one...";
			this.Title = "Please make a selection";
		}

		public SelectorForm(string OKVal, string CancleVal, string message, string title, object[] selectionVals)
		{
			System.Drawing.Size minimumSize;
			this.InitializeComponent();
			this.btnOKval = OKVal;
			this.btnCancleval = CancleVal;
			this.Message = message;
			this.Title = title;
			this.SelectionObjArray = selectionVals;
			System.Drawing.Size top = new System.Drawing.Size();
			int height = this.lblMessage.Height + this.lblMessage.Top + 10;
			this.cbxSelected.Top = height;
			height = height + this.cbxSelected.Height + 10;
			this.btnCancle.Top = height;
			this.btnOK.Top = height;
			this.btnInfo.Top = height;
			base.Top = base.Top + this.btnInfo.Height + 10;
			top.Width = (this.lblMessage.Width + 30 <= 197 ? 197 : this.lblMessage.Width + 30);
			top.Height = base.Top;
			if (top.Height < this.MinimumSize.Height)
			{
				minimumSize = this.MinimumSize;
				top.Height = minimumSize.Height;
			}
			if (top.Width < this.MinimumSize.Width)
			{
				minimumSize = this.MinimumSize;
				top.Width = minimumSize.Width;
			}
			base.Size = top;
			this.MinimumSize = top;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			string text = ((Button)sender).Text;
			if (text == ">>")
			{
				((Button)sender).Text = "<<";
				base.Size = new System.Drawing.Size(330, 390);
			}
			else if (text == "<<")
			{
				((Button)sender).Text = ">>";
				base.Size = this.MinimumSize;
			}
		}

		private void cbxSelected_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.btnOK.Enabled = true;
			this.label1.Text = ExIAboutMe.getIAboutMe(((ComboBox)sender).SelectedItem);
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
			this.btnOK = new Button();
			this.btnCancle = new Button();
			this.lblMessage = new Label();
			this.cbxSelected = new ComboBox();
			this.btnInfo = new Button();
			this.label1 = new Label();
			base.SuspendLayout();
			this.btnOK.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Enabled = false;
			this.btnOK.Location = new Point(121, 60);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "button1";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnCancle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.btnCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancle.Location = new Point(202, 60);
			this.btnCancle.Name = "btnCancle";
			this.btnCancle.Size = new System.Drawing.Size(75, 23);
			this.btnCancle.TabIndex = 2;
			this.btnCancle.Text = "button2";
			this.btnCancle.UseVisualStyleBackColor = true;
			this.lblMessage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.lblMessage.AutoSize = true;
			this.lblMessage.Location = new Point(12, 9);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(35, 13);
			this.lblMessage.TabIndex = 2;
			this.lblMessage.Text = "label1";
			this.cbxSelected.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.cbxSelected.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbxSelected.FormattingEnabled = true;
			this.cbxSelected.Location = new Point(12, 33);
			this.cbxSelected.Name = "cbxSelected";
			this.cbxSelected.Size = new System.Drawing.Size(303, 21);
			this.cbxSelected.TabIndex = 0;
			this.cbxSelected.SelectedIndexChanged += new EventHandler(this.cbxSelected_SelectedIndexChanged);
			this.btnInfo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.btnInfo.Font = new System.Drawing.Font("Times New Roman", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btnInfo.Location = new Point(283, 60);
			this.btnInfo.Name = "btnInfo";
			this.btnInfo.Size = new System.Drawing.Size(32, 23);
			this.btnInfo.TabIndex = 3;
			this.btnInfo.Text = ">>";
			this.btnInfo.UseVisualStyleBackColor = true;
			this.btnInfo.Click += new EventHandler(this.button1_Click);
			this.label1.Location = new Point(12, 108);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(303, 248);
			this.label1.TabIndex = 4;
			this.label1.Text = "Nothing Selected...";
			base.AcceptButton = this.btnOK;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.btnCancle;
			base.ClientSize = new System.Drawing.Size(328, 118);
			base.ControlBox = false;
			base.Controls.Add(this.label1);
			base.Controls.Add(this.cbxSelected);
			base.Controls.Add(this.lblMessage);
			base.Controls.Add(this.btnCancle);
			base.Controls.Add(this.btnInfo);
			base.Controls.Add(this.btnOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MinimumSize = new System.Drawing.Size(330, 120);
			base.Name = "SelectorForm";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "ComboSelectorForm";
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}