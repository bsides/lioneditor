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
    public partial class AllMoveFindItemsEditor : UserControl
    {
        public AllMoveFindItemsEditor()
        {
            InitializeComponent();
            mapMoveFindItemEditor1.DataChanged += new EventHandler( mapMoveFindItemEditor1_DataChanged );
        }

        void mapListBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            MapMoveFindItems map = mapListBox.SelectedItem as MapMoveFindItems;
            mapMoveFindItemEditor1.MapMoveFindItems = map;
        }

        void mapMoveFindItemEditor1_DataChanged( object sender, EventArgs e )
        {
            CurrencyManager cm = (CurrencyManager)BindingContext[mapListBox.DataSource];
            cm.Refresh();
        }

        public void UpdateView( AllMoveFindItems items )
        {
            mapListBox.SelectedIndexChanged -= mapListBox_SelectedIndexChanged;
            mapListBox.DataSource = items.MoveFindItems;
            mapListBox.SelectedIndexChanged += mapListBox_SelectedIndexChanged;
            mapListBox.SelectedIndex = 0;
            mapMoveFindItemEditor1.MapMoveFindItems = mapListBox.SelectedItem as MapMoveFindItems;
        }
    }
}
