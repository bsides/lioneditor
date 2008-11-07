using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace FFTPatcher.Controls
{
    public class ProgressBarWithText : ProgressBar
    {
		#region Instance Variables (1) 

        private string currentText = null;

		#endregion Instance Variables 

		#region Public Methods (2) 

        public void SetValue( int value )
        {
            SetValue( value, null );
        }

        public void SetValue( int value, string text )
        {
            currentText = text;
            Value = value;
            Invalidate();
        }

		#endregion Public Methods 

		#region Protected Methods (1) 

        protected override void OnPaint( PaintEventArgs e )
        {
            base.OnPaint( e );

            if (currentText != null)
            {
                TextRenderer.DrawText(
                    e.Graphics,
                    currentText,
                    this.Font,
                    new Rectangle( Point.Empty, Size ),
                    Color.White,
                    Color.Black,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPrefix );
            }
        }

		#endregion Protected Methods 
    }
}
