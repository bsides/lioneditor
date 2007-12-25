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
    public partial class AllStatusAttributesEditor : UserControl
    {
        public AllStatusAttributes AllStatusAttributes { get; set; }

        public AllStatusAttributesEditor()
        {
            InitializeComponent();
            AllStatusAttributes = new AllStatusAttributes( new SubArray<byte>( new List<byte>( Resources.StatusAttributesBin ), 0 ) );
            listBox.Items.AddRange( AllStatusAttributes.StatusAttributes );
            listBox.SelectedIndexChanged += listBox_SelectedIndexChanged;
            listBox.SelectedIndex = 0;
        }

        private void listBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            StatusAttribute a = listBox.SelectedItem as StatusAttribute;
            statusAttributeEditor.StatusAttribute = a;
        }
    }
}
