using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace FFTPatcher.Controls
{
    public class ProgressBarWithText : ProgressBar
    {
        private string currentText = null;

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
    }
}
