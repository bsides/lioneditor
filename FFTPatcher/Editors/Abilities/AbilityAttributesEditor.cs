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
    public partial class AbilityAttributesEditor : UserControl
    {

        private List<NumericUpDownWithDefault> spinners;
        private static readonly List<string> FieldNames = new List<string>( new string[] {
            "Range", "Effect", "Vertical", "X", "Y", "InflictStatus", "CT", "MPCost" } );
        private static readonly List<string> FlagNames = new List<string>( new string[] {
            "Blank6", "Blank7", "WeaponRange", "VerticalFixed", "VerticalTolerance", "WeaponStrike", "Auto", "TargetSelf",
            "HitEnemies", "HitAllies", "Blank8", "FollowTarget", "RandomFire", "LinearAttack", "ThreeDirections", "HitCaster",
            "Reflect", "Arithmetick", "Silence", "Mimic", "NormalAttack", "Perservere", "ShowQuote", "AnimateMiss",
            "CounterFlood", "CounterMagic", "Direct", "Shirahadori", "RequiresSword", "RequiresMateriaBlade", "Evadeable", "Targeting"} );

        private AbilityAttributes attributes;
        public AbilityAttributes Attributes
        {
            get { return attributes; }
            set
            {
                if( value == null )
                {
                    this.Enabled = false;
                    attributes = null;
                }
                else if( attributes != value )
                {
                    this.Enabled = true;
                    attributes = value;
                    UpdateView();
                }
            }
        }

        private Context ourContext = Context.Default;

        private void UpdateView()
        {
            this.SuspendLayout();
            elementsEditor.SuspendLayout();

            ignoreChanges = true;

            if( FFTPatch.Context != ourContext )
            {
                ourContext = FFTPatch.Context;
                flagsCheckedListBox.Items.Clear();
                flagsCheckedListBox.Items.AddRange( ourContext == Context.US_PSP ? Resources.AbilityAttributes : PSXResources.AbilityAttributes );
                formulaComboBox.DataSource = ourContext == Context.US_PSP ? AbilityFormula.PSPAbilityFormulas : AbilityFormula.PSXAbilityFormulas;
            }

            bool[] defaults = null;
            if( attributes.Default != null )
            {
                defaults = attributes.Default.ToBoolArray();
            }

            flagsCheckedListBox.SetValuesAndDefaults( ReflectionHelpers.GetFieldsOrProperties<bool>( attributes, FlagNames.ToArray() ), defaults );

            foreach( NumericUpDownWithDefault spinner in spinners )
            {
                spinner.SetValueAndDefault(
                    ReflectionHelpers.GetFieldOrProperty<byte>( attributes, spinner.Tag.ToString() ),
                    ReflectionHelpers.GetFieldOrProperty<byte>( attributes.Default, spinner.Tag.ToString() ) );
            }

            formulaComboBox.SetValueAndDefault( attributes.Formula, attributes.Default.Formula );

            elementsEditor.SetValueAndDefaults( attributes.Elements, attributes.Default.Elements );
            ignoreChanges = false;

            elementsEditor.ResumeLayout();
            this.ResumeLayout();
        }

        public AbilityAttributesEditor()
        {
            InitializeComponent();
            spinners = new List<NumericUpDownWithDefault>( new NumericUpDownWithDefault[] { rangeSpinner, effectSpinner, verticalSpinner, xSpinner, ySpinner, statusSpinner, ctSpinner, mpSpinner } );
            foreach( NumericUpDownWithDefault spinner in spinners )
            {
                spinner.ValueChanged += spinner_ValueChanged;
            }

            formulaComboBox.SelectedIndexChanged += formulaComboBox_SelectedIndexChanged;
            flagsCheckedListBox.ItemCheck += flagsCheckedListBox_ItemCheck;
            inflictStatusLabel.Click += inflictStatusLabel_Click;
            inflictStatusLabel.TabStop = false;
        }


        private void inflictStatusLabel_Click( object sender, EventArgs e )
        {
            FireLinkClickedEvent();
        }

        public event EventHandler<LabelClickedEventArgs> LinkClicked;
        private void FireLinkClickedEvent()
        {
            if( LinkClicked != null )
            {
                LinkClicked( this, new LabelClickedEventArgs( (byte)statusSpinner.Value ) );
            }
        }

        private void spinner_ValueChanged( object sender, EventArgs e )
        {
            if( !ignoreChanges )
            {
                NumericUpDownWithDefault c = sender as NumericUpDownWithDefault;
                int i = spinners.IndexOf( c );
                ReflectionHelpers.SetFieldOrProperty( attributes, FieldNames[i], (byte)c.Value );
            }
        }

        private void formulaComboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            if( !ignoreChanges )
            {
                if( attributes != null )
                {
                    attributes.Formula = formulaComboBox.SelectedItem as AbilityFormula;
                }
            }
        }

        private bool ignoreChanges = false;

        private void flagsCheckedListBox_ItemCheck( object sender, ItemCheckEventArgs e )
        {
            if( !ignoreChanges )
            {
                ReflectionHelpers.SetFlag( attributes, FlagNames[e.Index], e.NewValue == CheckState.Checked );
            }
        }
    }

    public class LabelClickedEventArgs : EventArgs
    {
        public byte Value { get; private set; }

        public LabelClickedEventArgs( byte value )
        {
            Value = value;
        }
    }
}
