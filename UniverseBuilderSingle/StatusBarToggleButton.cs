using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NTAF.Controls.StatusBar {
    public partial class StatusBarToggleButton : ToolStripItem {
        #region Events

        public event EventHandler CheckChanged;

        #endregion

        #region Properties

        [Category( "Appearance" ), Description( "" )]
        public Image CheckedImage { get; set; }

        [Category( "Appearance" ), Description( "" )]
        public Image UnCheckedImage { get; set; }

        private Boolean chked = false;
        [Category( "Appearance" ),
         Description( "" )]
        public bool Checked {
            get { return chked; }
            set {
                try {
                    chked = value;

                    changeState();

                    if ( CheckChanged != null )
                        CheckChanged( this, new EventArgs() );
                }
                catch ( Exception ex ) { }
            }
        }

        private Color checkedBackColor;
        [Category( "Appearance" ), Description( "" )]
        public Color CheckedBackColor {
            get { return checkedBackColor; }
            set {
                checkedBackColor = value;
                changeState();
            }
        }

        private Color checkedForeColor;
        [Category( "Appearance" ), Description( "" )]
        public Color CheckedForeColor {
            get { return checkedForeColor; }
            set {
                checkedForeColor = value;
                changeState();
            }
        }

        private Color uncheckedBackColor;
        [Category( "Appearance" ), Description( "" )]
        public Color UnCheckedBackColor {
            get { return uncheckedBackColor; }
            set {
                uncheckedBackColor = value;
                changeState();
            }
        }

        private Color uncheckedForeColor;
        [Category( "Appearance" ), Description( "" )]
        public Color UnCheckedForeColor {
            get { return uncheckedForeColor; }
            set {
                uncheckedForeColor = value;
                changeState();
            }
        }

        private bool showBorder = true;
        [Category( "Border" ), Description( "" )]
        public bool ShowBorder {
            get { return showBorder; }
            set {
                showBorder = value;
                this.Invalidate();
            }

        }

        private Color borderColor = Color.Black;
        [Category( "Border" ), Description( "" )]
        public Color BorderColor {
            get { return borderColor; }
            set {
                borderColor = value;
                this.Invalidate();
            }
        }

        private float borderWidth = 1.0F;
        [Category( "Border" ), Description( "" )]
        public float BorderWidth {
            get { return borderWidth; }
            set {
                borderWidth = value;
                this.Invalidate();
            }
        }

        #endregion

        #region Constructors
        public StatusBarToggleButton()
            : base( "", null, null, "" ) {
            this.Text = "NewStatusBarToggleButton";
            this.UnCheckedBackColor = SystemColors.Control;
            this.CheckedBackColor = SystemColors.ControlLightLight;
            this.UnCheckedForeColor = SystemColors.ControlText;
            this.CheckedForeColor = SystemColors.ControlText;
            this.UnCheckedImage = null;
            this.CheckedImage = null;
            this.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;

            Init();
        }

        public StatusBarToggleButton( string text, Image image, EventHandler onClick )
            : base( text, image, onClick, "" ) {
            Init();
        }

        public StatusBarToggleButton( string text, Image image, EventHandler onClick, string name )
            : base( text, image, onClick, name ) {
            Init();
        }

        private void Init() {
            this.changeState();
        }

        #endregion

        #region Methods

        protected override void OnPaint( PaintEventArgs e ) {
            //check for null values and create a temporary display style value
            CheckImageNullRef();

            PointF CenterPoint = new PointF(
                Width / 2, Height / 2 );

            StringFormat sf = new StringFormat();

            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            SolidBrush
                backBrush = new SolidBrush( BackColor ),
                foreBrush = new SolidBrush( ForeColor );

            e.Graphics.FillRectangle( backBrush, e.ClipRectangle );

            //create the border
            if ( this.ShowBorder ) {
                Pen
                    borderPen = null;

                if ( this.BorderColor == Color.Empty )
                    borderPen = new Pen( Color.Black );
                else
                    borderPen = new Pen( this.BorderColor );

                if ( this.BorderWidth == 0.0 )
                    borderPen.Width = 1.0F;
                else
                    borderPen.Width = this.BorderWidth;

                e.Graphics.DrawRectangle( borderPen, e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1 );

                borderPen.Dispose();
            }

            switch ( this.DisplayStyle ) {
                case ToolStripItemDisplayStyle.Image: {
                        Rectangle 
                            clip = new Rectangle( e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.Width, e.ClipRectangle.Height );

                        if ( this.ShowBorder ) {
                            clip.Width -= ( Int32 )this.borderWidth * 2;
                            clip.X += ( Int32 )this.borderWidth * 2;
                            clip.Height -= ( Int32 )this.borderWidth * 2;
                            clip.Y += ( Int32 )this.borderWidth;
                        }

                        if ( this.Image == null )
                            if ( this.Checked )
                                this.Image = CheckedImage;
                            else
                                this.Image = UnCheckedImage;

                        if ( this.Image != null )
                            e.Graphics.DrawImage( this.Image, ScaleRectangle( clip, Image.Size ) );

                    }
                    break;
                case ToolStripItemDisplayStyle.ImageAndText: {

                        sf.Alignment = StringAlignment.Near;

                        Rectangle 
                            clip = new Rectangle( e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.Width, e.ClipRectangle.Height );

                        if ( this.ShowBorder ) {
                            clip.Width -= ( Int32 )this.borderWidth * 2;
                            clip.X += ( Int32 )this.borderWidth * 2;
                            clip.Height -= ( Int32 )this.borderWidth * 2;
                            clip.Y += ( Int32 )this.borderWidth;
                        }

                        if ( this.Image == null )
                            if ( this.Checked )
                                this.Image = CheckedImage;
                            else
                                this.Image = UnCheckedImage;

                        if ( CenterPoint != null & e != null & e.ClipRectangle != null & Image != null ) {
                            CenterPoint.X = e.ClipRectangle.X + ScaleRectangle( e.ClipRectangle, this.Image.Size ).Width + 1;
                            e.Graphics.DrawImage( this.Image, ScaleRectangle( clip, Image.Size ) );
                        }

                        e.Graphics.DrawString( this.Text, this.Font, foreBrush, CenterPoint, sf );
                    }
                    break;
                case ToolStripItemDisplayStyle.None: { }
                    break;
                case ToolStripItemDisplayStyle.Text: {
                        e.Graphics.DrawString( this.Text, this.Font, foreBrush, CenterPoint, sf );
                    }
                    break;
                default: { }
                    break;
            }
            backBrush.Dispose();
            foreBrush.Dispose();
            sf.Dispose();


        }

        private void CheckImageNullRef() {
            switch ( this.DisplayStyle ) {
                case ToolStripItemDisplayStyle.Image | ToolStripItemDisplayStyle.ImageAndText:
                    if ( this.UnCheckedImage == null | this.CheckedImage == null ) {
                        //to avoid null references create new images in memory
                        UnCheckedImage = new Bitmap( this.Height, this.Width );
                        CheckedImage = new Bitmap( this.Height, this.Width );

                        //Get their graphics objects
                        Graphics 
                            UnCheckedGraphics = Graphics.FromImage( UnCheckedImage ),
                            CheckedGraphics = Graphics.FromImage( CheckedImage );

                        //create brushes to paint on the images
                        SolidBrush
                            UnCheckedBkgBrush = new SolidBrush( this.UnCheckedBackColor ),
                            CheckedBkgBrush = new SolidBrush( this.CheckedBackColor );

                        //fianlly fill the images with the proper BKG colors
                        UnCheckedGraphics.FillRectangle( UnCheckedBkgBrush, UnCheckedGraphics.ClipBounds );
                        CheckedGraphics.FillRectangle( UnCheckedBkgBrush, CheckedGraphics.ClipBounds );

                        //fianlly clean up after our selves
                        UnCheckedBkgBrush.Dispose();
                        CheckedBkgBrush.Dispose();
                    }

                    break;
            }
        }

        private static Rectangle ScaleRectangle( Rectangle Bounds, Size ImageSize ) {
            try {
                Rectangle
                retVal = new Rectangle( Bounds.X, Bounds.Y, 0, 0 );

                //find scale factor
                float Wsf = ( float )Bounds.Width / ( float )ImageSize.Width,
                  Hsf = ( float )Bounds.Height / ( float )ImageSize.Height;

                if ( Hsf < Wsf | Hsf == Wsf ) {
                    retVal.Width = ( int )Math.Round( Hsf * ImageSize.Width, 0, MidpointRounding.AwayFromZero );
                    retVal.Height = ( int )Math.Round( Hsf * ImageSize.Height, 0, MidpointRounding.AwayFromZero );
                }
                else {
                    retVal.Width = ( int )Math.Round( Wsf * ImageSize.Width, 0, MidpointRounding.AwayFromZero );
                    retVal.Height = ( int )Math.Round( Wsf * ImageSize.Height, 0, MidpointRounding.AwayFromZero );
                }

                return retVal;
            }
            catch ( Exception ex ) {

                throw;
            }
        }

        private void changeState() {
            if ( this.Checked ) {
                this.BackColor = CheckedBackColor;
                this.ForeColor = CheckedForeColor;
                this.Image = CheckedImage;
            }
            else {
                this.BackColor = UnCheckedBackColor;
                this.ForeColor = UnCheckedForeColor;
                this.Image = UnCheckedImage;
            }
            this.Invalidate();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing ) {
            base.Dispose( disposing );
        }

        #endregion
    }
}