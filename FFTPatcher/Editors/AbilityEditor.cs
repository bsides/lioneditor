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

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FFTPatcher.Datatypes;

namespace FFTPatcher.Editors
{
    public partial class AbilityEditor : UserControl
    {
        private Ability ability;
        public Ability Ability
        {
            get { return ability; }
            set
            {
                if( ability != value )
                {
                    ability = value;
                    UpdateView();
                }
            }
        }

        private bool ignoreChanges = false;
        private Context ourContext = Context.Default;

        private void UpdateView()
        {
            ignoreChanges = true;

            commonAbilitiesEditor.Ability = ability;

            abilityAttributesEditor.Visible = ability.IsNormal;
            abilityAttributesEditor.Attributes = ability.Attributes;

            arithmeticksLabel.Visible = ability.IsArithmetick;
            arithmeticksSpinner.Visible = ability.IsArithmetick;
            arithmeticksSpinner.Value = ability.ArithmetickSkill;

            hLabel.Visible = ability.IsArithmetick || ability.IsOther;

            chargingLabel.Visible = ability.IsCharging;
            ctLabel.Visible = ability.IsCharging;
            powerLabel.Visible = ability.IsCharging;
            ctSpinner.Visible = ability.IsCharging;
            powerSpinner.Visible = ability.IsCharging;
            ctSpinner.Value = ability.ChargeCT;
            powerSpinner.Value = ability.ChargeBonus;

            jumpingLabel.Visible = ability.IsJumping;
            horizontalLabel.Visible = ability.IsJumping;
            verticalLabel.Visible = ability.IsJumping;
            horizontalSpinner.Visible = ability.IsJumping;
            verticalSpinner.Visible = ability.IsJumping;
            horizontalSpinner.Value = ability.JumpHorizontal;
            verticalSpinner.Value = ability.JumpVertical;

            idLabel.Visible = ability.IsOther;
            idSpinner.Visible = ability.IsOther;
            idSpinner.Value = ability.OtherID;

            itemUseLabel.Visible = ability.IsItem;

            if( FFTPatch.Context == Context.US_PSP && ourContext != Context.US_PSP)
            {
                ourContext = Context.US_PSP;
                itemUseComboBox.Items.Clear();
                itemUseComboBox.Items.AddRange( pspItems.ToArray() );
                throwingComboBox.DataSource = pspItemTypes;
            }
            else if( FFTPatch.Context == Context.US_PSX && ourContext != Context.US_PSX )
            {
                ourContext = Context.US_PSX;
                itemUseComboBox.Items.Clear();
                itemUseComboBox.Items.AddRange( psxItems.ToArray() );
                throwingComboBox.DataSource = psxItemTypes;
            }
            itemUseComboBox.Visible = ability.IsItem;
            itemUseComboBox.SelectedItem = ability.Item;

            throwingLabel.Visible = ability.IsThrowing;

            throwingComboBox.Visible = ability.IsThrowing;
            throwingComboBox.SelectedItem = ability.Throwing;

            ignoreChanges = false;
        }

        private List<NumericUpDown> spinners;
        private List<ComboBox> comboBoxes;
        private List<Item> psxItems = new List<Item>( 256 );
        private List<Item> pspItems = new List<Item>( 256 );
        private List<ItemSubType> pspItemTypes = new List<ItemSubType>( (ItemSubType[])Enum.GetValues( typeof( ItemSubType ) ) );
        private List<ItemSubType> psxItemTypes = new List<ItemSubType>( (ItemSubType[])Enum.GetValues( typeof( ItemSubType ) ) );

        public AbilityEditor()
        {
            InitializeComponent();
            spinners = new List<NumericUpDown>( new NumericUpDown[] { 
                arithmeticksSpinner, ctSpinner, powerSpinner, horizontalSpinner, verticalSpinner, idSpinner } );
            comboBoxes = new List<ComboBox>( new ComboBox[] { itemUseComboBox, throwingComboBox } );

            arithmeticksSpinner.Tag = "ArithmetickSkill";
            ctSpinner.Tag = "ChargeCT";
            powerSpinner.Tag = "ChargeBonus";
            horizontalSpinner.Tag = "JumpHorizontal";
            verticalSpinner.Tag = "JumpVertical";
            itemUseComboBox.Tag = "Item";
            throwingComboBox.Tag = "Throwing";
            idSpinner.Tag = "OtherID";

            foreach( NumericUpDown spinner in spinners )
            {
                spinner.ValueChanged += spinner_ValueChanged;
            }
            foreach( ComboBox combo in comboBoxes )
            {
                combo.SelectedIndexChanged += combo_SelectedIndexChanged;
            }

            foreach( Item i in Item.PSPDummies )
            {
                if( i.Offset <= 0xFF )
                {
                    pspItems.Add( i );
                }
            }
            foreach( Item i in Item.PSXDummies )
            {
                if( i.Offset <= 0xFF )
                {
                    psxItems.Add( i );
                }
            }
            psxItemTypes.Remove( ItemSubType.LipRouge );
            psxItemTypes.Remove( ItemSubType.FellSword );
        }

        private void combo_SelectedIndexChanged( object sender, EventArgs e )
        {
            if( !ignoreChanges )
            {
                ComboBox c = sender as ComboBox;
                Utilities.SetFieldOrProperty( ability, c.Tag as string, c.SelectedItem );
            }
        }

        private void spinner_ValueChanged( object sender, EventArgs e )
        {
            if( !ignoreChanges )
            {
                NumericUpDown c = sender as NumericUpDown;
                Utilities.SetFieldOrProperty( ability, c.Tag as string, (byte)c.Value );
            }
        }
    }
}
