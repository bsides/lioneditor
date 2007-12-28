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
using System.Windows.Forms;

namespace LionEditor
{
    public partial class TimerEditor : UserControl
    {
        public uint Value
        {
            get { return (uint)(hoursSpinner.Value * 3600 + minutesSpinner.Value * 60 + secondsSpinner.Value); }
            set
            {
                hoursSpinner.Value = value / 3600;
                minutesSpinner.Value = (int)(value - hoursSpinner.Value * 3600) / 60;
                secondsSpinner.Value = (int)(value - hoursSpinner.Value * 3600 - minutesSpinner.Value * 60);
            }
        }

        public TimerEditor()
        {
            InitializeComponent();

            hoursSpinner.ValueChanged += valueChanged;
            secondsSpinner.ValueChanged += valueChanged;
            minutesSpinner.ValueChanged += valueChanged;
        }

        void valueChanged( object sender, EventArgs e )
        {
            FireDataChangedEvent();
        }

        public event EventHandler DataChangedEvent;
        private void FireDataChangedEvent()
        {
            if( DataChangedEvent != null )
            {
                DataChangedEvent( this, EventArgs.Empty );
            }
        }
    }
}
