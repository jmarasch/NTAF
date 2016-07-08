using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

internal class InputBoxForm : Form
{
	private Button btnOK;

	private Button btnCancel;

	private Label lblText;

	private TextBox txtResult;

	private string strReturnValue;

	private Point pntStartLocation;

	private System.ComponentModel.Container components = null;

	public bool CapturePassword
	{
		set
		{
			if (!value)
			{
				this.txtResult.PasswordChar = '\0';
			}
			else
			{
				this.txtResult.PasswordChar = '*';
			}
		}
	}

	public string DefaultResponse
	{
		set
		{
			this.txtResult.Text = value;
			this.txtResult.SelectAll();
		}
	}

	public string Prompt
	{
		set
		{
			this.lblText.Text = value;
		}
	}

	public string ReturnValue
	{
		get
		{
			return this.strReturnValue;
		}
	}

	public Point StartLocation
	{
		set
		{
			this.pntStartLocation = value;
		}
	}

	public string Title
	{
		set
		{
			this.Text = value;
		}
	}

	public InputBoxForm()
	{
		this.InitializeComponent();
		this.strReturnValue = "";
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		base.Close();
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		this.strReturnValue = this.txtResult.Text;
		base.Close();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			if (this.components != null)
			{
				this.components.Dispose();
			}
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.btnOK = new Button();
		this.txtResult = new TextBox();
		this.lblText = new Label();
		this.btnCancel = new Button();
		base.SuspendLayout();
		this.btnOK.Location = new Point(288, 8);
		this.btnOK.Name = "btnOK";
		this.btnOK.Size = new System.Drawing.Size(64, 24);
		this.btnOK.TabIndex = 1;
		this.btnOK.Text = "OK";
		this.btnOK.Click += new EventHandler(this.btnOK_Click);
		this.txtResult.Location = new Point(8, 80);
		this.txtResult.Name = "txtResult";
		this.txtResult.Size = new System.Drawing.Size(344, 20);
		this.txtResult.TabIndex = 0;
		this.lblText.Location = new Point(16, 8);
		this.lblText.Name = "lblText";
		this.lblText.Size = new System.Drawing.Size(264, 64);
		this.lblText.TabIndex = 3;
		this.lblText.Text = "InputBox";
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.Location = new Point(288, 40);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(64, 24);
		this.btnCancel.TabIndex = 2;
		this.btnCancel.Text = "Cancel";
		this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
		base.AcceptButton = this.btnOK;
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
		base.CancelButton = this.btnCancel;
		base.ClientSize = new System.Drawing.Size(362, 111);
		base.Controls.Add(this.btnCancel);
		base.Controls.Add(this.lblText);
		base.Controls.Add(this.txtResult);
		base.Controls.Add(this.btnOK);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "InputBoxForm";
		this.Text = "InputBox";
		base.Load += new EventHandler(this.InputBoxForm_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	private void InputBoxForm_Load(object sender, EventArgs e)
	{
		if (this.pntStartLocation.IsEmpty)
		{
			base.CenterToParent();
		}
		else if (!(this.pntStartLocation.X == -1 | this.pntStartLocation.Y == -1))
		{
			base.Top = this.pntStartLocation.X;
			base.Left = this.pntStartLocation.Y;
		}
		else
		{
			base.CenterToParent();
		}
	}
}