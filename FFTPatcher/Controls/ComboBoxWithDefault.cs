/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of FFTPatcher.

    LionEditor is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    LionEditor is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with LionEditor.  If not, see <http://www.gnu.org/licenses/>.
*/

using System.Drawing;
using System.Windows.Forms;
using FFTPatcher.Editors;

namespace FFTPatcher.Controls
{
    public class ComboBoxWithDefault : ComboBox
    {
        public object DefaultValue { get; private set; }

        public new object SelectedItem
        {
            get { return base.SelectedItem; }
            private set { base.SelectedItem = value; }
        }

        public ComboBoxWithDefault()
            : base()
        {
            this.DrawMode = DrawMode.OwnerDrawFixed;
        }

        public void SetValueAndDefault( object value, object defaultValue )
        {
            FFTPatchEditor.ToolTip.SetToolTip( this, "Default: " + defaultValue.ToString() );

            DefaultValue = defaultValue;
            SelectedItem = value;
            Invalidate();
        }

        protected override void OnDrawItem( DrawItemEventArgs e )
        {
            if( Enabled && !DroppedDown && (SelectedItem != null) && !SelectedItem.Equals( DefaultValue ) )
            {
                using( Brush backBrush = new SolidBrush( BackColor ) )
                using( Brush textBrush = new SolidBrush( ForeColor ) )
                using( Pen borderPen = new Pen( Brushes.Blue, 2 ) )
                {
                    if( (e.State & DrawItemState.Focus) == DrawItemState.Focus )
                    {
                        e.Graphics.FillRectangle( SystemBrushes.Highlight, e.Bounds );
                        e.Graphics.DrawString( SelectedItem == null ? string.Empty : SelectedItem.ToString(), Font, SystemBrushes.HighlightText, e.Bounds.Location );
                    }
                    else
                    {
                        e.Graphics.FillRectangle( backBrush, e.Bounds );
                        e.Graphics.DrawString( SelectedItem == null ? string.Empty : SelectedItem.ToString(), Font, textBrush, e.Bounds.Location );
                    }
                    e.Graphics.DrawRectangle( borderPen, e.Bounds );
                }
            }
            else if( Enabled && !DroppedDown && ((e.State & DrawItemState.Focus) == DrawItemState.Focus) )
            {
                e.Graphics.FillRectangle( SystemBrushes.Highlight, e.Bounds );
                e.Graphics.DrawString( SelectedItem == null ? string.Empty : SelectedItem.ToString(), Font, SystemBrushes.HighlightText, e.Bounds.Location );
            }
            else if( Enabled && (e.Index != -1) )
            {
                if( ((e.State & DrawItemState.Focus) == DrawItemState.Focus) &&
                    ((e.State & DrawItemState.ComboBoxEdit) == 0) )
                {
                    using( Brush highlightBrush = new SolidBrush( SystemColors.Highlight ) )
                    using( Brush textBrush = new SolidBrush( SystemColors.HighlightText ) )
                    {
                        e.Graphics.FillRectangle( highlightBrush, e.Bounds );
                        e.Graphics.DrawString( Items[e.Index].ToString(), Font, textBrush, e.Bounds.Location );
                    }
                }
                else
                {
                    using( Brush highlightBrush = new SolidBrush( BackColor ) )
                    using( Brush textBrush = new SolidBrush( ForeColor ) )
                    {
                        e.Graphics.FillRectangle( highlightBrush, e.Bounds );
                        e.Graphics.DrawString( Items[e.Index].ToString(), Font, textBrush, e.Bounds.Location );
                    }
                }
            }
        }
    }
}
