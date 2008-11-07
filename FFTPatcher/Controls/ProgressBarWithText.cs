/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of FFTPatcher.

    FFTPatcher is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    FFTPatcher is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with FFTPatcher.  If not, see <http://www.gnu.org/licenses/>.
*/

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
