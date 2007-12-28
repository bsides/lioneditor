using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FFTPatcher.Datatypes;
using FFTPatcher.Properties;

namespace FFTPatcher.Editors
{
    public partial class AllInflictStatusesEditor : UserControl
    {
        public AllInflictStatuses AllInflictStatuses { get; private set; }

        public AllInflictStatusesEditor()
        {
            InitializeComponent();
            AllInflictStatuses = new AllInflictStatuses( 
                new SubArray<byte>( new List<byte>( Resources.InflictStatusesBin ), 0 ) );

            offsetListBox.DataSource = AllInflictStatuses.InflictStatuses;

            offsetListBox.SelectedIndexChanged += offsetListBox_SelectedIndexChanged;
            offsetListBox.SelectedIndex = 0;
            offsetListBox_SelectedIndexChanged( offsetListBox, EventArgs.Empty );
        }

        private void offsetListBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            inflictStatusEditor.InflictStatus = offsetListBox.SelectedItem as InflictStatus;
        }
    }
}
