using System.Windows.Forms;
using System;
using System.Drawing;
using FFTPatcher.Editors;

namespace FFTPatcher.Controls
{
    public class NumericUpDownWithDefault : NumericUpDown
    {
        public decimal DefaultValue { get; set; }
        public bool Default { get { return Value == DefaultValue; } }
        private bool borderOn = false;

        public new decimal Value
        {
            get { return base.Value; }
            private set { base.Value = value; }
        }

        public void SetValueAndDefault( decimal value, decimal defaultValue )
        {
            if( Hexadecimal )
                FFTPatchEditor.ToolTip.SetToolTip( this, string.Format( "Default: 0x{0:X2}", (int)defaultValue ) );
            else
                FFTPatchEditor.ToolTip.SetToolTip( this, string.Format( "Default: {0}", defaultValue ) );
            
            DefaultValue = defaultValue;
            Value = value;
            OnValueChanged( EventArgs.Empty );
        }
      
        protected override void OnPaint( PaintEventArgs e )
        {
            base.OnPaint( e );
            if( Enabled && (Value != DefaultValue) )
            {
                e.Graphics.DrawRectangle( new Pen( Brushes.Blue, 2 ), e.ClipRectangle );
                borderOn = true;
            }
            else
            {
                borderOn = false;
            }
        }

        protected override void OnValueChanged( EventArgs e )
        {
            if( !(Default ^ borderOn) )
            {
                Refresh();
            }

            base.OnValueChanged( e );
        }
    }
}
