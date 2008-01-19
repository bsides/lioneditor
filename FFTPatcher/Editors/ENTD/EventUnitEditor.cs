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
using System.Windows.Forms;
using FFTPatcher.Controls;
using FFTPatcher.Datatypes;

namespace FFTPatcher.Editors
{
    public partial class EventUnitEditor : UserControl
    {
        private EventUnit eventUnit = null;
        private bool ignoreChanges = false;
        private Context ourContext = Context.Default;

        public EventUnit EventUnit
        {
            get { return eventUnit; }
            set
            {
                if( value == null )
                {
                    eventUnit = null;
                    Enabled = false;
                }
                else if( value != eventUnit )
                {
                    eventUnit = value;
                    UpdateView();
                    Enabled = true;
                }
            }
        }

        private NumericUpDownWithDefault[] spinners;
        private ComboBoxWithDefault[] comboBoxes;

        public EventUnitEditor()
        {
            InitializeComponent();
            spinners = new NumericUpDownWithDefault[] {
                daySpinner, braverySpinner, faithSpinner, levelSpinner, paletteSpinner, xSpinner, ySpinner,
                unknown10Spinner, unknown11Spinner, unknown12Spinner, unknown13Spinner, unknown14Spinner,
                unknown1Spinner, unknown2Spinner, unknown3Spinner, unknown4Spinner, unknown5Spinner, 
                unknown6Spinner, unknown7Spinner, unknown8Spinner, unknown9Spinner };
            comboBoxes = new ComboBoxWithDefault[] {
                spriteSetComboBox, specialNameComboBox, monthComboBox, jobComboBox, 
                primarySkillComboBox, secondaryActionComboBox, reactionComboBox, supportComboBox, movementComboBox,
                rightHandComboBox, leftHandComboBox, headComboBox, bodyComboBox, accessoryComboBox };

            foreach( NumericUpDownWithDefault spinner in spinners )
            {
                spinner.ValueChanged += spinner_ValueChanged;
            }
            foreach( ComboBoxWithDefault comboBox in comboBoxes )
            {
                comboBox.SelectedIndexChanged += comboBox_SelectedIndexChanged;
            }
            flags1CheckedListBox.ItemCheck += flagsCheckedListBox_ItemCheck;
            flags2CheckedListBox.ItemCheck += flagsCheckedListBox_ItemCheck;
        }

        private void flagsCheckedListBox_ItemCheck( object sender, ItemCheckEventArgs e )
        {
            if (sender == flags1CheckedListBox)
                ReflectionHelpers.SetFieldOrProperty( eventUnit, EventUnit.Flags1FieldNames[e.Index], e.NewValue == CheckState.Checked );
            else if( sender == flags2CheckedListBox )
                ReflectionHelpers.SetFieldOrProperty( eventUnit, EventUnit.Flags2FieldNames[e.Index], e.NewValue == CheckState.Checked );
        }

        private void UpdateDataSources()
        {
            foreach( ComboBoxWithDefault itemComboBox in 
                new ComboBoxWithDefault[] { rightHandComboBox, leftHandComboBox, headComboBox, bodyComboBox, accessoryComboBox } )
            {
                itemComboBox.BindingContext = new BindingContext();
                itemComboBox.DataSource = Item.EventItems;
            }

            primarySkillComboBox.BindingContext = new BindingContext();
            primarySkillComboBox.DataSource = new List<SkillSet>( SkillSet.EventSkillSets.Values );
            secondaryActionComboBox.BindingContext = new BindingContext();
            secondaryActionComboBox.DataSource = new List<SkillSet>( SkillSet.EventSkillSets.Values );
            foreach( ComboBoxWithDefault abilityComboBox in 
                new ComboBoxWithDefault[] { reactionComboBox, supportComboBox, movementComboBox } )
            {
                abilityComboBox.BindingContext = new BindingContext();
                abilityComboBox.DataSource = AllAbilities.EventAbilities;
            }
            spriteSetComboBox.DataSource = SpriteSet.SpriteSets;
            specialNameComboBox.DataSource = SpecialName.SpecialNames;
            jobComboBox.DataSource = AllJobs.DummyJobs;
            monthComboBox.DataSource = Enum.GetValues( typeof( Month ) );
        }

        public event EventHandler DataChanged;

        private void FireDataChangedEvent()
        {
            if( DataChanged != null )
            {
                DataChanged( this, EventArgs.Empty );
            }
        }

        private void comboBox_SelectedIndexChanged( object sender, System.EventArgs e )
        {
            if( !ignoreChanges )
            {
                ComboBoxWithDefault c = sender as ComboBoxWithDefault;
                ReflectionHelpers.SetFieldOrProperty( eventUnit, c.Tag.ToString(), c.SelectedItem );
                if( (c == spriteSetComboBox) || (c == specialNameComboBox) || (c == jobComboBox) )
                {
                    FireDataChangedEvent();
                }
            }
        }
 
        private void spinner_ValueChanged( object sender, System.EventArgs e )
        {
            if( !ignoreChanges )
            {
                NumericUpDownWithDefault c = sender as NumericUpDownWithDefault;
                ReflectionHelpers.SetFieldOrProperty( eventUnit, c.Tag.ToString(), (byte)c.Value );
            }
        }

        private void UpdateView()
        {
            ignoreChanges = true;

            if( ourContext != FFTPatch.Context )
            {
                ourContext = FFTPatch.Context;
                UpdateDataSources();
            }

            if( eventUnit.Default != null )
            {
                foreach( NumericUpDownWithDefault spinner in spinners )
                {
                    spinner.SetValueAndDefault(
                        ReflectionHelpers.GetFieldOrProperty<byte>( eventUnit, spinner.Tag.ToString() ),
                        ReflectionHelpers.GetFieldOrProperty<byte>( eventUnit.Default, spinner.Tag.ToString() ) );
                }

                foreach( ComboBoxWithDefault comboBox in comboBoxes )
                {
                    comboBox.SetValueAndDefault(
                        ReflectionHelpers.GetFieldOrProperty<object>( eventUnit, comboBox.Tag.ToString() ),
                        ReflectionHelpers.GetFieldOrProperty<object>( eventUnit.Default, comboBox.Tag.ToString() ) );
                }

                flags1CheckedListBox.SetValuesAndDefaults(
                    ReflectionHelpers.GetFieldsOrProperties<bool>( eventUnit, EventUnit.Flags1FieldNames ),
                    ReflectionHelpers.GetFieldsOrProperties<bool>( eventUnit.Default, EventUnit.Flags1FieldNames ) );

                flags2CheckedListBox.SetValuesAndDefaults(
                    ReflectionHelpers.GetFieldsOrProperties<bool>( eventUnit, EventUnit.Flags2FieldNames ),
                    ReflectionHelpers.GetFieldsOrProperties<bool>( eventUnit.Default, EventUnit.Flags2FieldNames ) );

            }
            ignoreChanges = false;
        }
    }
}
