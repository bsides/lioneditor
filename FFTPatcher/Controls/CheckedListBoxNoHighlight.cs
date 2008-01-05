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
using System.Windows.Forms.VisualStyles;

namespace FFTPatcher.Controls
{
    public partial class CheckedListBoxNoHighlight : CheckedListBox
    {
        public bool[] Defaults { get; set; }

        public CheckedListBoxNoHighlight()
            : base()
        {
            this.CheckOnClick = true;
        }

        protected override void OnItemCheck( ItemCheckEventArgs e )
        {
            RefreshItem( e.Index );
            base.OnItemCheck( e );
        }

        protected override void OnDrawItem( DrawItemEventArgs e )
        {
            Brush backColorBrush = new SolidBrush( this.BackColor );
            Brush foreColorBrush = new SolidBrush( this.ForeColor );

            e.Graphics.FillRectangle( backColorBrush, e.Bounds );
            CheckBoxState state = this.GetItemChecked( e.Index ) ? CheckBoxState.CheckedNormal : CheckBoxState.UncheckedNormal;
            Size checkBoxSize = CheckBoxRenderer.GetGlyphSize( e.Graphics, state );
            Point loc = new Point( 1, (e.Bounds.Height - (checkBoxSize.Height + 1)) / 2 + 1 );
            CheckBoxRenderer.DrawCheckBox( e.Graphics, new Point( loc.X + e.Bounds.X, loc.Y + e.Bounds.Y ), state );
            e.Graphics.DrawString( this.Items[e.Index].ToString(), e.Font, foreColorBrush, new PointF( loc.X + checkBoxSize.Width + 1 + e.Bounds.X, loc.Y + e.Bounds.Y ) );

            if( (Defaults != null) && (Defaults.Length > e.Index) && (Defaults[e.Index] != GetItemChecked( e.Index )) )
            {
                using( Pen p = new Pen( Color.Blue, 1 ) )
                {
                    e.Graphics.DrawRectangle( p, new Rectangle( e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1 ) );
                }
            }
        }
    }
}
