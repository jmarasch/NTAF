namespace NTAF.PrintEngine
{
    partial class ObjectPreview
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ObjectPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size( 582, 496 );
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ObjectPreview";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ObjectPreview";
            this.ResizeBegin += new System.EventHandler( this.ObjectPreview_ResizeBegin );
            this.Paint += new System.Windows.Forms.PaintEventHandler( this.ObjectPreview_Paint );
            this.KeyUp += new System.Windows.Forms.KeyEventHandler( this.ObjectPreview_KeyUp );
            this.ResizeEnd += new System.EventHandler( this.ObjectPreview_ResizeEnd );
            this.ResumeLayout( false );

        }

        #endregion

    }
}