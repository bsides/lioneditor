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
    public partial class AllItemsEditor : UserControl
    {
        public AllItemsEditor()
        {
            InitializeComponent();
            itemEditor.InflictStatusClicked += itemEditor_InflictStatusClicked;
            itemEditor.ItemAttributesClicked += itemEditor_ItemAttributesClicked;
        }

        public event EventHandler<LabelClickedEventArgs> ItemAttributesClicked;
        private void itemEditor_ItemAttributesClicked( object sender, LabelClickedEventArgs e )
        {
            if( ItemAttributesClicked != null )
            {
                ItemAttributesClicked( this, e );
            }
        }

        public event EventHandler<LabelClickedEventArgs> InflictStatusClicked;
        private void itemEditor_InflictStatusClicked( object sender, LabelClickedEventArgs e )
        {
            if( InflictStatusClicked != null )
            {
                InflictStatusClicked( this, e );
            }
        }
        
        public void UpdateView( AllItems items )
        {
            itemListBox.SelectedIndexChanged -= itemListBox_SelectedIndexChanged;
            itemListBox.Items.Clear();
            itemListBox.Items.AddRange( items.Items.ToArray() );
            itemListBox.SelectedIndexChanged += itemListBox_SelectedIndexChanged;
            itemListBox.SelectedIndex = 0;
            itemEditor.Item = itemListBox.SelectedItem as Item;
        }

        private void itemListBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            itemEditor.Item = itemListBox.SelectedItem as Item;
        }
    }
}
