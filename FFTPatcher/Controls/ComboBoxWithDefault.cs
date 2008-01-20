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

using System.Drawing;
using System.Windows.Forms;
using FFTPatcher.Editors;

namespace FFTPatcher.Controls
{
    /// <summary>
    /// Represents a <see cref="ComboBox"/> that allows a default value to be specified.
    /// </summary>
    public class ComboBoxWithDefault : ComboBox
    {
        public object DefaultValue { get; private set; }
        public new ComboBoxStyle DropDownStyle { get { return base.DropDownStyle; } private set { base.DropDownStyle = value; } }
        public new DrawMode DrawMode { get { return base.DrawMode; } private set { base.DrawMode = value; } }

        /// <summary>
        /// Gets the currently selected item.
        /// </summary>
        public new object SelectedItem
        {
            get { return base.SelectedItem; }
            private set { base.SelectedItem = value; }
        }

        public ComboBoxWithDefault()
            : base()
        {
            DropDownStyle = ComboBoxStyle.DropDownList;
        }

        /// <summary>
        /// Sets the SelectedItem and its default value.
        /// </summary>
        public void SetValueAndDefault( object value, object defaultValue )
        {
            FFTPatchEditor.ToolTip.SetToolTip( this, "Default: " + defaultValue.ToString() );

            DefaultValue = defaultValue;
            SelectedItem = value;
            Invalidate();
        }

        protected override void OnKeyDown( KeyEventArgs e )
        {
            if( e.KeyData == Keys.F12 )
            {
                SetValueAndDefault( DefaultValue, DefaultValue );
            }
            base.OnKeyDown( e );
        }

        private void SetColors()
        {
            if( Enabled && !DroppedDown && (SelectedItem != null) && !SelectedItem.Equals( DefaultValue ) )
            {
                BackColor = Color.Blue;
                ForeColor = Color.White;
            }
            else if( Enabled && !DroppedDown )
            {
                BackColor = SystemColors.Window;
                ForeColor = SystemColors.WindowText;
            }
            else if( Enabled && DroppedDown )
            {
                BackColor = SystemColors.Window;
                ForeColor = SystemColors.WindowText;
            }
        }

        protected override void OnInvalidated( InvalidateEventArgs e )
        {
            SetColors();
            base.OnInvalidated( e );
        }

        protected override void OnSelectedIndexChanged( System.EventArgs e )
        {
            base.OnSelectedIndexChanged( e );
            SetColors();
        }

        protected override void OnMouseClick( MouseEventArgs e )
        {
            if( (e.Button & MouseButtons.Right) == MouseButtons.Right )
            {
                SetValueAndDefault( DefaultValue, DefaultValue );
            }
            base.OnMouseClick( e );
        }
    }
}
