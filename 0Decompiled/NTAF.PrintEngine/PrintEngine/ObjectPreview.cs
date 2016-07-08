using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NTAF.PrintEngine
{
	public class ObjectPreview : Form
	{
		private object i_objToPreview = null;

		private IContainer components = null;

		public ObjectPreview(object objToPreview)
		{
			if (!(objToPreview is IPrintable))
			{
				throw new Exception("Object is not valid for preview");
			}
			this.i_objToPreview = objToPreview;
			this.InitializeComponent();
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
			base.SuspendLayout();
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = Color.White;
			base.ClientSize = new System.Drawing.Size(582, 496);
			this.DoubleBuffered = true;
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			base.Name = "ObjectPreview";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "ObjectPreview";
			base.ResizeBegin += new EventHandler(this.ObjectPreview_ResizeBegin);
			base.Paint += new PaintEventHandler(this.ObjectPreview_Paint);
			base.KeyUp += new KeyEventHandler(this.ObjectPreview_KeyUp);
			base.ResizeEnd += new EventHandler(this.ObjectPreview_ResizeEnd);
			base.ResumeLayout(false);
		}

		private void ObjectPreview_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				base.Close();
			}
		}

		private void ObjectPreview_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(Color.White);
			PrintElement element = new PrintElement((IPrintable)this.i_objToPreview);
			((IPrintable)this.i_objToPreview).Print(element);
			NTAF.PrintEngine.PrintEngine X = new NTAF.PrintEngine.PrintEngine("");
			System.Drawing.Size size = base.Size;
			X.pageBounds.Width = size.Width;
			SizeF sizeF = e.Graphics.MeasureString("X", X.PrintFont);
			int fontheight = (int)Math.Round((double)sizeF.Height, 0, MidpointRounding.AwayFromZero);
			int formHeight = (int)((float)Math.Round((double)element.CalculateHeight(X, e.Graphics), 0, MidpointRounding.AwayFromZero)) + fontheight;
			size = base.Size;
			base.Size = new System.Drawing.Size(size.Width, formHeight);
			Rectangle windSize = new Rectangle(new Point(0, 0), base.Size);
			X.pageBounds = windSize;
			element.Draw(X, 0f, e.Graphics, windSize);
		}

		private void ObjectPreview_ResizeBegin(object sender, EventArgs e)
		{
			base.SuspendLayout();
		}

		private void ObjectPreview_ResizeEnd(object sender, EventArgs e)
		{
			base.ResumeLayout();
		}
	}
}