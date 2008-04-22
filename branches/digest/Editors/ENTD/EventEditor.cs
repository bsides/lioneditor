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
using System.Drawing;
using System.Windows.Forms;
using FFTPatcher.Datatypes;

namespace FFTPatcher.Editors
{
    public partial class EventEditor : UserControl
    {

		#region Fields (2) 

        private int[] columnWidths = new int[3] { 50, 50, 50 };
        private Event evt;

		#endregion Fields 

		#region Properties (1) 


        public Event Event
        {
            get { return evt; }
            set
            {
                if( value == null )
                {
                    evt = null;
                    Enabled = false;
                }
                else if( value != evt )
                {
                    evt = value;
                    UpdateView();
                    Enabled = true;
                }
            }
        }


		#endregion Properties 

		#region Constructors (1) 

        public EventEditor()
        {
            InitializeComponent();
            unitSelectorListBox.SelectedIndexChanged += unitSelectorComboBox_SelectedIndexChanged;
            eventUnitEditor.DataChanged += eventUnitEditor_DataChanged;
            unitSelectorListBox.DrawItem += unitSelectorListBox_DrawItem;
            unitSelectorListBox.DrawMode = DrawMode.OwnerDrawFixed;
        }

		#endregion Constructors 

		#region Methods (5) 


        private void DetermineColumnWidths()
        {
            int maxSpriteWidth = 50;
            int maxNameWidth = 50;
            int maxJobWidth = 50;

            foreach( EventUnit unit in evt.Units )
            {
                string sprite = unit.SpriteSet.Name;
                string name = unit.SpecialName.Name;
                string job = unit.Job.Name;
                maxSpriteWidth = Math.Max( maxSpriteWidth, TextRenderer.MeasureText( sprite, unitSelectorListBox.Font ).Width );
                maxNameWidth = Math.Max( maxNameWidth, TextRenderer.MeasureText( name, unitSelectorListBox.Font ).Width );
                maxJobWidth = Math.Max( maxJobWidth, TextRenderer.MeasureText( job, unitSelectorListBox.Font ).Width );
            }

            columnWidths[0] = maxSpriteWidth + 10;
            columnWidths[1] = maxNameWidth + 10;
            columnWidths[2] = maxJobWidth + 10;
        }

        private void eventUnitEditor_DataChanged( object sender, System.EventArgs e )
        {
            CurrencyManager cm = (CurrencyManager)BindingContext[evt.Units];
            cm.Refresh();
        }

        private void unitSelectorComboBox_SelectedIndexChanged( object sender, System.EventArgs e )
        {
            eventUnitEditor.EventUnit = unitSelectorListBox.SelectedItem as EventUnit;
        }

        private void unitSelectorListBox_DrawItem( object sender, DrawItemEventArgs e )
        {
            if( (e.Index > -1) && (e.Index < unitSelectorListBox.Items.Count) )
            {
                EventUnit unit = unitSelectorListBox.Items[e.Index] as EventUnit;
                using( Brush textBrush = new SolidBrush( e.ForeColor ) )
                using( Brush backBrush = new SolidBrush( e.BackColor ) )
                {
                    e.Graphics.FillRectangle( backBrush, e.Bounds );
                    e.Graphics.DrawString( unit.SpriteSet.Name, e.Font, textBrush, e.Bounds.X + 0, e.Bounds.Y + 0 );
                    e.Graphics.DrawString( unit.SpecialName.Name, e.Font, textBrush, e.Bounds.X + columnWidths[0], e.Bounds.Y + 0 );
                    e.Graphics.DrawString( unit.Job.Name, e.Font, textBrush, e.Bounds.X + columnWidths[0] + columnWidths[1], e.Bounds.Y + 0 );
                    if( (e.State & DrawItemState.Focus) == DrawItemState.Focus )
                    {
                        e.DrawFocusRectangle();
                    }
                }
            }
        }

        private void UpdateView()
        {
            eventUnitEditor.SuspendLayout();
            DetermineColumnWidths();
            unitSelectorListBox.DataSource = evt.Units;
            unitSelectorListBox.SelectedIndex = 0;
            eventUnitEditor.EventUnit = unitSelectorListBox.SelectedItem as EventUnit;
            eventUnitEditor.ResumeLayout();
        }


		#endregion Methods 

    }
}
