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
    public partial class AbilityAttributesEditor : UserControl
    {

        private List<NumericUpDown> spinners;
        private static readonly List<string> FieldNames = new List<string>( new string[] {
            "Range", "Effect", "Vertical", "Formula", "X", "Y", "InflictStatus", "CT", "MPCost" } );
        private static readonly List<string> FlagNames = new List<string>( new string[] {
            "Blank6", "Blank7", "WeaponRange", "VerticalFixed", "VerticalTolerance", "WeaponStrike", "Auto", "TargetSelf",
            "HitEnemies", "HitAllies", "Blank8", "FollowTarget", "RandomFire", "LinearAttack", "ThreeDirections", "HitCaster",
            "Reflect", "Arithmetick", "Silence", "Mimic", "NormalAttack", "Perservere", "ShowQuote", "Unknown5",
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
                    this.elementsEditor.Elements = null;
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

        private void UpdateView()
        {
            this.SuspendLayout();
            elementsEditor.SuspendLayout();

            ignoreChanges = true;
            for( int i = 0; i < flagsCheckedListBox.Items.Count; i++ )
            {
                flagsCheckedListBox.SetItemChecked( i, Utilities.GetFlag( attributes, FlagNames[i] ) );
            }

            rangeSpinner.Value = attributes.Range;
            effectSpinner.Value = attributes.Effect;
            verticalSpinner.Value = attributes.Vertical;
            formulaSpinner.Value = attributes.Formula;
            xSpinner.Value = attributes.X;
            ySpinner.Value = attributes.Y;
            statusSpinner.Value = attributes.InflictStatus;
            ctSpinner.Value = attributes.CT;
            mpSpinner.Value = attributes.MPCost;

            elementsEditor.Elements = attributes.Elements;
            ignoreChanges = false;

            elementsEditor.ResumeLayout();
            this.ResumeLayout();
        }

        public AbilityAttributesEditor()
        {
            InitializeComponent();
            spinners = new List<NumericUpDown>( new NumericUpDown[] { rangeSpinner, effectSpinner, verticalSpinner, formulaSpinner, xSpinner, ySpinner, statusSpinner, ctSpinner, mpSpinner } );
            foreach( NumericUpDown spinner in spinners )
            {
                spinner.ValueChanged += spinner_ValueChanged;
            }
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
                NumericUpDown c = sender as NumericUpDown;
                int i = spinners.IndexOf( c );
                Utilities.SetFieldOrProperty( attributes, FieldNames[i], (byte)c.Value );
            }
        }

        private bool ignoreChanges = false;

        private void flagsCheckedListBox_ItemCheck( object sender, ItemCheckEventArgs e )
        {
            if( !ignoreChanges )
            {
                Utilities.SetFlag( attributes, FlagNames[e.Index], e.NewValue == CheckState.Checked );
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
