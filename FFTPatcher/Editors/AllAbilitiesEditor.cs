using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FFTPatcher.Datatypes;
using FFTPatcher.Properties;

namespace FFTPatcher.Editors
{
    public partial class AllAbilitiesEditor : UserControl
    {
        public AllAbilities AllAbilities { get; set; }

        public AllAbilitiesEditor()
        {
            InitializeComponent();
            AllAbilities = new AllAbilities( new SubArray<byte>( new List<byte>( Resources.AbilitiesBin ), 0 ) );
            abilitiesListBox.Items.AddRange( AllAbilities.Abilities );
            abilitiesListBox.SelectedIndexChanged += abilitiesListBox_SelectedIndexChanged;
            abilitiesListBox.SelectedIndex = 0;
        }

        private void abilitiesListBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            Ability a = abilitiesListBox.SelectedItem as Ability;
            abilityEditor.Ability = a;
        }
    }
}
