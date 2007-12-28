using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FFTPatcher.Datatypes;
using FFTPatcher.Properties;

namespace FFTPatcher.Editors
{
    public partial class AllItemAttributesEditor : UserControl
    {
        public AllItemAttributes AllItemAttributes { get; private set; }

        public AllItemAttributesEditor()
        {
            InitializeComponent();
            AllItemAttributes = new AllItemAttributes(
                new SubArray<byte>( new List<byte>( Resources.OldItemAttributesBin ), 0 ),
                new SubArray<byte>( new List<byte>( Resources.NewItemAttributesBin ), 0 ) );
            offsetListBox.DataSource = AllItemAttributes.ItemAttributes;
            offsetListBox.SelectedIndexChanged += offsetListBox_SelectedIndexChanged;
            offsetListBox.SelectedIndex = 0;
            offsetListBox_SelectedIndexChanged( offsetListBox, EventArgs.Empty );
        }

        private void offsetListBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            itemAttributeEditor.ItemAttributes = offsetListBox.SelectedItem as ItemAttributes;
        }
    }
}
