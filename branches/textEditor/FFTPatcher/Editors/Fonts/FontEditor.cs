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
using System.Windows.Forms;
using FFTPatcher.Datatypes;

namespace FFTPatcher.Editors
{
    public partial class FontEditor : UserControl
    {
		#region Instance Variables (1) 

        private FFTFont font;

		#endregion Instance Variables 

		#region Public Properties (1) 

        public FFTFont FFTFont 
        {
            get { return font; }
            set
            {
                if( font != value )
                {
                    font = value;
                    numericUpDown1.Value = 0;
                    numericUpDown1_ValueChanged( numericUpDown1, EventArgs.Empty );
                }
            }
        }

		#endregion Public Properties 

		#region Constructors (1) 

        public FontEditor()
        {
            InitializeComponent();
            numericUpDown1.ValueChanged += new EventHandler( numericUpDown1_ValueChanged );
        }

		#endregion Constructors 

		#region Private Methods (1) 

        private void numericUpDown1_ValueChanged( object sender, EventArgs e )
        {
            if( FFTFont != null )
            {
                glyphEditor1.Glyph = FFTFont.Glyphs[(int)numericUpDown1.Value];
            }
        }

		#endregion Private Methods 
    }
}
