using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FFTPatcher.Datatypes;
using FFTPatcher.Properties;

namespace FFTPatcher.Editors
{
    public partial class AllItemsEditor : UserControl
    {
        public AllItems AllItems { get; private set; }
        public AllItemsEditor()
        {
            InitializeComponent();
            AllItems = new AllItems(
                new SubArray<byte>( new List<byte>( Resources.OldItemsBin ), 0 ),
                new SubArray<byte>( new List<byte>( Resources.NewItemsBin ), 0 ) );
            foreach( Item i in AllItems.Items )
            {
                itemListBox.Items.Add( i );
            }
            itemListBox.SelectedIndexChanged += itemListBox_SelectedIndexChanged;
            itemListBox.SelectedIndex = 0;
        }

        private void itemListBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            itemEditor.Item = itemListBox.SelectedItem as Item;
        }
    }
}
