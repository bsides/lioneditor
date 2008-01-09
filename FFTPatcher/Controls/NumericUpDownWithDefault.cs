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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FFTPatcher.Editors;

namespace FFTPatcher.Controls
{
    public class NumericUpDownWithDefault : NumericUpDown
    {
        public decimal DefaultValue { get; private set; }
        public bool Default { get { return Value == DefaultValue; } }
        private bool borderOn = false;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [ReadOnly(true)]
        public new decimal Value
        {
            get { return base.Value; }
            private set { base.Value = value; }
        }

        public void SetValueAndDefault( decimal value, decimal defaultValue )
        {
            if( Hexadecimal )
                FFTPatchEditor.ToolTip.SetToolTip( this, string.Format( "Default: 0x{0:X2}", (int)defaultValue ) );
            else
                FFTPatchEditor.ToolTip.SetToolTip( this, string.Format( "Default: {0}", defaultValue ) );
            
            DefaultValue = defaultValue;
            Value = value;
            OnValueChanged( EventArgs.Empty );
        }
      
        protected override void OnPaint( PaintEventArgs e )
        {
            base.OnPaint( e );
            if( Enabled && (Value != DefaultValue) )
            {
                e.Graphics.DrawRectangle( new Pen( Brushes.Blue, 2 ), e.ClipRectangle );
                borderOn = true;
            }
            else
            {
                borderOn = false;
            }
        }

        protected override void OnValueChanged( EventArgs e )
        {
            if( !(Default ^ borderOn) )
            {
                Refresh();
            }

            base.OnValueChanged( e );
        }
    }
}
