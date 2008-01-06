using System;
using System.ComponentModel;
using System.Drawing;

namespace FFTPatcher.Controls
{
    /// 
    /// VerticalLabel is a component that displays a label, but instead of the
    /// text being horizontal it is displayed vertically.
    ///
    /// This is achieved by drawing using the specified Font and overriding
    /// the OnPaint method to draw with the font rotated.
    ///
    /// Windows designer support is also included, the default text and alignment
    /// can be set in the designer
    /// 
    ///
    public class VerticalLabel : System.Windows.Forms.Control
    {

        /// 
        /// internal variable for the current label text
        /// 
        private string labelText;

        /// 
        /// internal variable for the alignment of the vertical text
        /// 
        private System.Drawing.ContentAlignment labelTextAlign;

        #region default constructor, destructor and initialisation code
        public VerticalLabel()
        {
            //base.New();
            InitializeComponent();
        }

        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                if( !((components == null)) )
                {
                    components.Dispose();
                }
            }
            base.Dispose( disposing );
        }



        private System.ComponentModel.Container components = null;

        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
            this.Size = new System.Drawing.Size( 24, 100 );
        }

        #endregion

        /// 
        /// Override the onPaint method to draw a string vertically on the screen
        /// 
        /// default PaintEventArgs parameter
        protected override void OnPaint( System.Windows.Forms.PaintEventArgs e )
        {
            float sngControlWidth;
            float sngControlHeight;
            float sngTransformX;
            float sngTransformY;
            Color labelColor = this.BackColor;
            Pen labelBorderPen = new Pen( labelColor, 0 );
            SolidBrush labelBackColorBrush = new SolidBrush( labelColor );
            SolidBrush labelForeColorBrush = new SolidBrush( base.ForeColor );
            base.OnPaint( e );
            sngControlWidth = this.Size.Width;
            sngControlHeight = this.Size.Height;
            e.Graphics.DrawRectangle( labelBorderPen, 0, 0, sngControlWidth, sngControlHeight );
            e.Graphics.FillRectangle( labelBackColorBrush, 0, 0, sngControlWidth, sngControlHeight );
            sngTransformX = 0;
            sngTransformY = sngControlHeight;
            e.Graphics.TranslateTransform( sngTransformX, sngTransformY );
            e.Graphics.RotateTransform( 270 );

            #region figure out offset to achieve the desired alignment
            //default to left alignment
            float leftOffset = 0;


            //handle center alignment
            if( (this.labelTextAlign == System.Drawing.ContentAlignment.BottomCenter) ||
                (this.labelTextAlign == System.Drawing.ContentAlignment.MiddleCenter) ||
                (this.labelTextAlign == System.Drawing.ContentAlignment.TopCenter) )
            {
                System.Drawing.SizeF sf = e.Graphics.MeasureString( this.labelText, Font );
                leftOffset = (this.Size.Height - sf.Width) / 2;
            }
            //handle right alignment
            if( (this.labelTextAlign == System.Drawing.ContentAlignment.BottomRight) ||
                (this.labelTextAlign == System.Drawing.ContentAlignment.MiddleRight) ||
                (this.labelTextAlign == System.Drawing.ContentAlignment.TopRight) )
            {
                System.Drawing.SizeF sf = e.Graphics.MeasureString( this.labelText, Font );
                leftOffset = this.Size.Height - sf.Width;
            }
            #endregion

            e.Graphics.DrawString( labelText, Font, labelForeColorBrush, leftOffset, 0 );
        }


        /// 
        /// Invalidate on resize event
        /// 
        /// 
        protected override void OnResize( EventArgs e )
        {
            Invalidate();
            base.OnResize( e );
        }



        #region windows form designer support
        /// 
        /// Windows designer Text setting
        /// 
        [Category( "Verticallabel" ), Description( "Text is displayed vertiaclly in container" )]
        public override string Text
        {
            get
            {
                return labelText;
            }
            set
            {
                labelText = value;
                Invalidate();
            }
        }
        [Category( "Verticallabel" ), Description( "Text Alignment" )]
        public System.Drawing.ContentAlignment TextAlign
        {
            get
            {
                return labelTextAlign;
            }
            set
            {
                labelTextAlign = value;
                Invalidate();
            }
        }



        #endregion

    }
}