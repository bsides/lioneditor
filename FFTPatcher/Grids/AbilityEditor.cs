using System;
using System.Reflection;
using System.Windows.Forms;
using FFTPatcher.Datatypes;

namespace FFTPatcher.Grids
{
    public partial class AbilityEditor : UserControl
    {
        private Ability ability;

        protected AbilityEditor()
        {
            InitializeComponent();
        }

        public AbilityEditor( Ability a )
            : this()
        {
            ability = a;

            NameLabel.Text = a.Name;
            Offset.Text = a.Offset.ToString( "0x{04X}" );

            JPCost.Value = a.JPCost;
            JPCost.ValueChanged += JPCost_ValueChanged;

            LearnRate.Value = a.LearnRate;
            LearnRate.ValueChanged += LearnRate_ValueChanged;

            AbilityType.DataSource = Enum.GetValues( a.AbilityType.GetType() );
            AbilityType.SelectedItem = a.AbilityType;
            AbilityType.SelectedIndexChanged += AbilityType_SelectedIndexChanged;

            foreach( Control c in this.Controls )
            {
                if( c is CheckBox )
                {
                    CheckBox cb = c as CheckBox;
                    if( cb.Name.IndexOf( "AI" ) == 0 )
                    {
                        FieldInfo fi = typeof( AIFlags ).GetField( cb.Name.Substring( 2 ) );
                        bool b = (bool)fi.GetValue( a.AIFlags );
                        cb.Checked = b;
                    }
                    else
                    {
                        FieldInfo fi = typeof( Ability ).GetField( cb.Name );
                        bool b = (bool)fi.GetValue( a );
                        cb.Checked = b;
                    }

                    cb.CheckedChanged += new EventHandler( cb_CheckedChanged );
                }
            }
        }

        private void AbilityType_SelectedIndexChanged( object sender, EventArgs e )
        {
            ability.AbilityType = (AbilityType)AbilityType.Items[AbilityType.SelectedIndex];
        }

        void LearnRate_ValueChanged( object sender, EventArgs e )
        {
            ability.LearnRate = (byte)LearnRate.Value;
        }

        void JPCost_ValueChanged( object sender, EventArgs e )
        {
            ability.JPCost = (UInt16)JPCost.Value;
        }

        private void cb_CheckedChanged( object sender, EventArgs e )
        {
            CheckBox cb = sender as CheckBox;
            if( cb.Name.IndexOf( "AI" ) == 0 )
            {
                FieldInfo fi = typeof( AIFlags ).GetField( cb.Name.Substring( 2 ) );
                fi.SetValue( ability.AIFlags, cb.Checked );
            }
            else
            {
                FieldInfo fi = typeof( Ability ).GetField( cb.Name );
                fi.SetValue( ability, cb.Checked );
            }
        }
    }
}
