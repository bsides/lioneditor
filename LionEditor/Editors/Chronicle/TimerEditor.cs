using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace LionEditor.Editors.Chronicle
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
