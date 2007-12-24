using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FFTPatcher.Datatypes;
using FFTPatcher.Properties;

namespace FFTPatcher.Grids
{
    public partial class AbilitiesDataGridView : UserControl
    {
        private Ability[] allAbilities = new AllAbilities( new SubArray<byte>( new List<byte>( bytes ), 0, 4095 ) ).Abilities;
        private static byte[] bytes = Resources.AbilitiesBin;

        public AbilitiesDataGridView()
        {
            InitializeComponent();
            AbilityType.DataSource = Enum.GetValues( typeof( AbilityType ) );
            AbilityType.ValueType = typeof( AbilityType );
            dataGridView1.AutoGenerateColumns = false;
            Index.ValueType = typeof( UInt16 );
            JPCost.ValueType = typeof( UInt16 );
            LearnRate.ValueType = typeof( byte );
            dataGridView1.DataSource = allAbilities;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.DataError += dataGridView1_DataError;
            dataGridView1.EditingControlShowing += dataGridView1_EditingControlShowing;
            dataGridView1.CellParsing += dataGridView1_CellParsing;
        }

        private void dataGridView1_CellParsing( object sender, DataGridViewCellParsingEventArgs e )
        {
            Int64 i;
            if( e.ColumnIndex == JPCost.Index )
            {
                bool success = Int64.TryParse( e.Value.ToString(), out i );
                i = success ? ((i < 0) ? 0 : ((i > 9999) ? 9999 : i)) : 0;
                e.Value = (UInt16)i;
                e.ParsingApplied = true;
            }
            else if( e.ColumnIndex == LearnRate.Index )
            {
                bool success = Int64.TryParse( e.Value.ToString(), out i );
                i = success ? ((i < 0) ? 0 : ((i > 100) ? 100 : i)) : 0;
                e.Value = (byte)i;
                e.ParsingApplied = true;
            }
        }

        private void dataGridView1_EditingControlShowing( object sender, DataGridViewEditingControlShowingEventArgs e )
        {
            if( dataGridView1.CurrentCell.ColumnIndex == AbilityType.Index )
            {
                DataGridViewComboBoxEditingControl c = e.Control as DataGridViewComboBoxEditingControl;
                c.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }

        void dataGridView1_DataError( object sender, DataGridViewDataErrorEventArgs e )
        {
            e.ThrowException = false;
        }
    }
}
