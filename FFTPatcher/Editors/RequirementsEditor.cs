using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FFTPatcher.Datatypes;

namespace FFTPatcher.Editors
{
    public partial class RequirementsEditor : UserControl
    {
        private List<Requirements> requirements;
        public List<Requirements> Requirements
        {
            get { return requirements; }
            set
            {
                if( value == null )
                {
                    this.Enabled = false;
                    this.requirements = null;
                    dataGridView1.DataSource = null;
                }
                else if( value != requirements )
                {
                    this.Enabled = true;
                    this.requirements = value;
                    dataGridView1.DataSource = value;
                }
            }
        }


        public RequirementsEditor()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.CellParsing += dataGridView1_CellParsing;
            dataGridView1.CellValidating += dataGridView1_CellValidating;
        }

        private void dataGridView1_CellValidating( object sender, DataGridViewCellValidatingEventArgs e )
        {
            int result;
            if( !Int32.TryParse( e.FormattedValue.ToString(), out result ) || (result < 0) || (result > 8) )
            {
                e.Cancel = true;
            }
        }

        private void dataGridView1_CellParsing( object sender, DataGridViewCellParsingEventArgs e )
        {
            int result;
            if( Int32.TryParse( e.Value.ToString(), out result ) )
            {
                if( result > 8 )
                    result = 8;
                if( result < 0 )
                    result = 0;
                e.Value = result;

                e.ParsingApplied = true;
            }
        }
    }
}
