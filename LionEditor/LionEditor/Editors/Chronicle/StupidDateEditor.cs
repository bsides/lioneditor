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
using System.ComponentModel;
using System.Windows.Forms;

namespace LionEditor
{
    public partial class StupidDateEditor : UserControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public StupidDate CurrentDate
        {
            get { return new StupidDate( (int)daySpinner.Value, (Zodiac)monthCombo.SelectedItem ); }
            set
            {
                monthCombo.SelectedItem = value.Month;
                daySpinner.Value = value.Day;
            }
        }

        public StupidDateEditor( StupidDate initialDate )
            : this()
        {
            monthCombo.SelectedItem = initialDate.Month;
            daySpinner.Value = initialDate.Day;
        }

        public StupidDateEditor()
        {
            InitializeComponent();
            monthCombo.Items.Add( Zodiac.Aries );
            monthCombo.Items.Add( Zodiac.Taurus );
            monthCombo.Items.Add( Zodiac.Gemini );
            monthCombo.Items.Add( Zodiac.Cancer );
            monthCombo.Items.Add( Zodiac.Leo );
            monthCombo.Items.Add( Zodiac.Virgo );
            monthCombo.Items.Add( Zodiac.Libra );
            monthCombo.Items.Add( Zodiac.Scorpio );
            monthCombo.Items.Add( Zodiac.Sagittarius );
            monthCombo.Items.Add( Zodiac.Capricorn );
            monthCombo.Items.Add( Zodiac.Aquarius );
            monthCombo.Items.Add( Zodiac.Pisces );
            monthCombo.SelectedIndexChanged += monthCombo_SelectedIndexChanged;
            daySpinner.ValueChanged += daySpinner_ValueChanged;
        }

        public event EventHandler DateChangedEvent;

        private void FireDateChangedEvent()
        {
            if( DateChangedEvent != null )
            {
                DateChangedEvent( this, EventArgs.Empty );
            }
        }

        private void daySpinner_ValueChanged(object sender, EventArgs e)
        {
            FireDateChangedEvent();
        }

        private void monthCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch( (Zodiac)monthCombo.SelectedItem )
            {
                case Zodiac.Gemini:
                    daySpinner.Maximum = 32;
                    break;

                case Zodiac.Taurus:
                case Zodiac.Cancer:
                case Zodiac.Leo:
                case Zodiac.Virgo:
                case Zodiac.Libra:
                    daySpinner.Value = (daySpinner.Value > 31) ? 31 : daySpinner.Value;
                    daySpinner.Maximum = 31;
                    break;

                case Zodiac.Aries:
                case Zodiac.Scorpio:
                case Zodiac.Sagittarius:
                case Zodiac.Aquarius:
                case Zodiac.Pisces:
                    daySpinner.Value = (daySpinner.Value > 30) ? 30 : daySpinner.Value;
                    daySpinner.Maximum = 30;
                    break;

                case Zodiac.Capricorn:
                    daySpinner.Value = (daySpinner.Value > 28) ? 28 : daySpinner.Value;
                    daySpinner.Maximum = 28;
                    break;
            }

            FireDateChangedEvent();
        }
    }
}
