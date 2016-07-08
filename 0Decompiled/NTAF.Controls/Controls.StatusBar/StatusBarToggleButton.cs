using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace NTAF.Controls.StatusBar
{
	public class StatusBarToggleButton : ToolStripItem
	{
		private bool chked = false;

		private Color checkedBackColor;

		private Color checkedForeColor;

		private Color uncheckedBackColor;

		private Color uncheckedForeColor;

		private bool showBorder = true;

		private Color borderColor = Color.Black;

		private float borderWidth = 1f;

		[Category("Border")]
		[Description("")]
		public Color BorderColor
		{
			get
			{
				return this.borderColor;
			}
			set
			{
				this.borderColor = value;
				base.Invalidate();
			}
		}

		[Category("Border")]
		[Description("")]
		public float BorderWidth
		{
			get
			{
				return this.borderWidth;
			}
			set
			{
				this.borderWidth = value;
				base.Invalidate();
			}
		}

		[Category("Appearance")]
		[Description("")]
		public bool Checked
		{
			get
			{
				return this.chked;
			}
			set
			{
				try
				{
					this.chked = value;
					this.changeState();
					if (this.CheckChanged != null)
					{
						this.CheckChanged(this, new EventArgs());
					}
				}
				catch (Exception exception)
				{
				}
			}
		}

		[Category("Appearance")]
		[Description("")]
		public Color CheckedBackColor
		{
			get
			{
				return this.checkedBackColor;
			}
			set
			{
				this.checkedBackColor = value;
				this.changeState();
			}
		}

		[Category("Appearance")]
		[Description("")]
		public Color CheckedForeColor
		{
			get
			{
				return this.checkedForeColor;
			}
			set
			{
				this.checkedForeColor = value;
				this.changeState();
			}
		}

		[Category("Appearance")]
		[Description("")]
		public System.Drawing.Image CheckedImage
		{
			get;
			set;
		}

		[Category("Border")]
		[Description("")]
		public bool ShowBorder
		{
			get
			{
				return this.showBorder;
			}
			set
			{
				this.showBorder = value;
				base.Invalidate();
			}
		}

		[Category("Appearance")]
		[Description("")]
		public Color UnCheckedBackColor
		{
			get
			{
				return this.uncheckedBackColor;
			}
			set
			{
				this.uncheckedBackColor = value;
				this.changeState();
			}
		}

		[Category("Appearance")]
		[Description("")]
		public Color UnCheckedForeColor
		{
			get
			{
				return this.uncheckedForeColor;
			}
			set
			{
				this.uncheckedForeColor = value;
				this.changeState();
			}
		}

		[Category("Appearance")]
		[Description("")]
		public System.Drawing.Image UnCheckedImage
		{
			get;
			set;
		}

		public StatusBarToggleButton() : base("", null, null, "")
		{
			this.Text = "NewStatusBarToggleButton";
			this.UnCheckedBackColor = SystemColors.Control;
			this.CheckedBackColor = SystemColors.ControlLightLight;
			this.UnCheckedForeColor = SystemColors.ControlText;
			this.CheckedForeColor = SystemColors.ControlText;
			this.UnCheckedImage = null;
			this.CheckedImage = null;
			this.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
			this.Init();
		}

		public StatusBarToggleButton(string text, System.Drawing.Image image, EventHandler onClick) : base(text, image, onClick, "")
		{
			this.Init();
		}

		public StatusBarToggleButton(string text, System.Drawing.Image image, EventHandler onClick, string name) : base(text, image, onClick, name)
		{
			this.Init();
		}

		private void changeState()
		{
			if (!this.Checked)
			{
				this.BackColor = this.UnCheckedBackColor;
				this.ForeColor = this.UnCheckedForeColor;
				this.Image = this.UnCheckedImage;
			}
			else
			{
				this.BackColor = this.CheckedBackColor;
				this.ForeColor = this.CheckedForeColor;
				this.Image = this.CheckedImage;
			}
			base.Invalidate();
		}

		private void CheckImageNullRef()
		{
			if (this.DisplayStyle == ToolStripItemDisplayStyle.ImageAndText)
			{
				if (this.UnCheckedImage == null | this.CheckedImage == null)
				{
					this.UnCheckedImage = new Bitmap(base.Height, base.Width);
					this.CheckedImage = new Bitmap(base.Height, base.Width);
					Graphics UnCheckedGraphics = Graphics.FromImage(this.UnCheckedImage);
					Graphics CheckedGraphics = Graphics.FromImage(this.CheckedImage);
					SolidBrush UnCheckedBkgBrush = new SolidBrush(this.UnCheckedBackColor);
					SolidBrush CheckedBkgBrush = new SolidBrush(this.CheckedBackColor);
					UnCheckedGraphics.FillRectangle(UnCheckedBkgBrush, UnCheckedGraphics.ClipBounds);
					CheckedGraphics.FillRectangle(UnCheckedBkgBrush, CheckedGraphics.ClipBounds);
					UnCheckedBkgBrush.Dispose();
					CheckedBkgBrush.Dispose();
				}
			}
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		private void Init()
		{
			this.changeState();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Rectangle clip;
			Rectangle clipRectangle;
			this.CheckImageNullRef();
			PointF CenterPoint = new PointF((float)(base.Width / 2), (float)(base.Height / 2));
			StringFormat sf = new StringFormat()
			{
				Alignment = StringAlignment.Center,
				LineAlignment = StringAlignment.Center
			};
			SolidBrush backBrush = new SolidBrush(this.BackColor);
			SolidBrush foreBrush = new SolidBrush(this.ForeColor);
			e.Graphics.FillRectangle(backBrush, e.ClipRectangle);
			if (this.ShowBorder)
			{
				Pen borderPen = null;
				borderPen = (!(this.BorderColor == Color.Empty) ? new Pen(this.BorderColor) : new Pen(Color.Black));
				if ((double)this.BorderWidth != 0)
				{
					borderPen.Width = this.BorderWidth;
				}
				else
				{
					borderPen.Width = 1f;
				}
				Graphics graphics = e.Graphics;
				int x = e.ClipRectangle.X;
				int y = e.ClipRectangle.Y;
				clipRectangle = e.ClipRectangle;
				int width = clipRectangle.Width - 1;
				clipRectangle = e.ClipRectangle;
				graphics.DrawRectangle(borderPen, x, y, width, clipRectangle.Height - 1);
				borderPen.Dispose();
			}
			switch (this.DisplayStyle)
			{
				case ToolStripItemDisplayStyle.Text:
				{
					e.Graphics.DrawString(this.Text, this.Font, foreBrush, CenterPoint, sf);
					break;
				}
				case ToolStripItemDisplayStyle.Image:
				{
					int num = e.ClipRectangle.X;
					int y1 = e.ClipRectangle.Y;
					int width1 = e.ClipRectangle.Width;
					clipRectangle = e.ClipRectangle;
					clip = new Rectangle(num, y1, width1, clipRectangle.Height);
					if (this.ShowBorder)
					{
						clip.Width = clip.Width - (int)this.borderWidth * 2;
						clip.X = clip.X + (int)this.borderWidth * 2;
						clip.Height = clip.Height - (int)this.borderWidth * 2;
						clip.Y = clip.Y + (int)this.borderWidth;
					}
					if (this.Image == null)
					{
						if (!this.Checked)
						{
							this.Image = this.UnCheckedImage;
						}
						else
						{
							this.Image = this.CheckedImage;
						}
					}
					if (this.Image != null)
					{
						e.Graphics.DrawImage(this.Image, StatusBarToggleButton.ScaleRectangle(clip, this.Image.Size));
					}
					break;
				}
				case ToolStripItemDisplayStyle.ImageAndText:
				{
					sf.Alignment = StringAlignment.Near;
					int x1 = e.ClipRectangle.X;
					int num1 = e.ClipRectangle.Y;
					int width2 = e.ClipRectangle.Width;
					clipRectangle = e.ClipRectangle;
					clip = new Rectangle(x1, num1, width2, clipRectangle.Height);
					if (this.ShowBorder)
					{
						clip.Width = clip.Width - (int)this.borderWidth * 2;
						clip.X = clip.X + (int)this.borderWidth * 2;
						clip.Height = clip.Height - (int)this.borderWidth * 2;
						clip.Y = clip.Y + (int)this.borderWidth;
					}
					if (this.Image == null)
					{
						if (!this.Checked)
						{
							this.Image = this.UnCheckedImage;
						}
						else
						{
							this.Image = this.CheckedImage;
						}
					}
					Rectangle rectangle = e.ClipRectangle;
					if (e != null & this.Image != null)
					{
						int x2 = e.ClipRectangle.X;
						clipRectangle = StatusBarToggleButton.ScaleRectangle(e.ClipRectangle, this.Image.Size);
						CenterPoint.X = (float)(x2 + clipRectangle.Width + 1);
						e.Graphics.DrawImage(this.Image, StatusBarToggleButton.ScaleRectangle(clip, this.Image.Size));
					}
					e.Graphics.DrawString(this.Text, this.Font, foreBrush, CenterPoint, sf);
					break;
				}
			}
			backBrush.Dispose();
			foreBrush.Dispose();
			sf.Dispose();
		}

		private static Rectangle ScaleRectangle(Rectangle Bounds, System.Drawing.Size ImageSize)
		{
			Rectangle rectangle;
			try
			{
				Rectangle retVal = new Rectangle(Bounds.X, Bounds.Y, 0, 0);
				float Wsf = (float)Bounds.Width / (float)ImageSize.Width;
				float Hsf = (float)Bounds.Height / (float)ImageSize.Height;
				if (!(Hsf < Wsf | Hsf == Wsf))
				{
					retVal.Width = (int)Math.Round((double)(Wsf * (float)ImageSize.Width), 0, MidpointRounding.AwayFromZero);
					retVal.Height = (int)Math.Round((double)(Wsf * (float)ImageSize.Height), 0, MidpointRounding.AwayFromZero);
				}
				else
				{
					retVal.Width = (int)Math.Round((double)(Hsf * (float)ImageSize.Width), 0, MidpointRounding.AwayFromZero);
					retVal.Height = (int)Math.Round((double)(Hsf * (float)ImageSize.Height), 0, MidpointRounding.AwayFromZero);
				}
				rectangle = retVal;
			}
			catch (Exception exception)
			{
				throw;
			}
			return rectangle;
		}

		public event EventHandler CheckChanged;
	}
}