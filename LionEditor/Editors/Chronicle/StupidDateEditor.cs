using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace LionEditor.Editors.Chronicle
{
    public partial class StupidDateEditor : UserControl
    {
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
        
        void daySpinner_ValueChanged( object sender, EventArgs e )
        {
            FireDateChangedEvent();
        }

        void monthCombo_SelectedIndexChanged( object sender, EventArgs e )
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
