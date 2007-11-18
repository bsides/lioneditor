/*
	Copyright 2007, Joe Davidson <joedavidson@gmail.com>

	This file is part of LionEditor.

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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace LionEditor
{
    public partial class CharacterEditor : UserControl
    {
        public CharacterEditor()
        {
            InitializeComponent();
            AssignComboBoxItems();
        }

        private int GetIdealDropDownWidth( ICollection items, ComboBox c, int minimumWidth )
        {
            int width = c.DropDownWidth;
            Graphics g = c.CreateGraphics();
            Font f = c.Font;
            int scrollBarWidth = (items.Count > c.MaxDropDownItems) ? SystemInformation.VerticalScrollBarWidth : 0;
            int itemWidth;
            foreach( object o in items )
            {
                string s = o.ToString();
                itemWidth = (int)g.MeasureString( s, f ).Width + scrollBarWidth;
                if( width < itemWidth )
                {
                    width = itemWidth;
                }
            }

            return width;
        }

        private void AssignComboBoxItems()
        {
            ComboBox[] itemCombos = new ComboBox[] { rightHandCombo, rightShieldCombo, leftHandCombo, leftShieldCombo, headCombo, bodyCombo, accessoryCombo };
            foreach (ComboBox c in itemCombos)
            {
                //c.DisplayMember = "String";
                c.ValueMember = "Offset";
                c.DataSource = Item.ItemList;
                c.Validating += ComboBoxValidating;

                c.DropDownWidth = GetIdealDropDownWidth( Item.ItemList, c, 0 );
            }

            secondaryCombo.DisplayMember = "String";
            secondaryCombo.ValueMember = "Byte";
            secondaryCombo.DataSource = SecondaryAction.ActionList;
            secondaryCombo.DropDownWidth = GetIdealDropDownWidth( SecondaryAction.ActionList, secondaryCombo, 0 );

            ComboBox[] abilityCombos = new ComboBox[] { movementCombo, reactionCombo, supportCombo };
            foreach( ComboBox a in abilityCombos )
            {
                //a.DisplayMember = "String";
                a.ValueMember = "Value";
                a.DataSource = Ability.AbilityList;
                a.DropDownWidth = GetIdealDropDownWidth( Ability.AbilityList, a, 0 );
            }

            classComboBox.DataSource = Class.ClassList;
            classComboBox.DropDownWidth = GetIdealDropDownWidth( Class.ClassList, classComboBox, 0 );

            zodiacComboBox.DataSource = Enum.GetValues( typeof( Zodiac ) );
        }

        void ComboBoxValidating( object sender, CancelEventArgs e )
        {
            ComboBox c = sender as ComboBox;
            if( c.SelectedItem == null )
            {
                e.Cancel = true;
            }
        }
    }
}
