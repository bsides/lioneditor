using System.Windows.Forms;
using FFTPatcher.Datatypes;

namespace FFTPatcher.Editors
{
    public partial class EquipmentEditor : UserControl
    {
        private static string[] FieldNames = new string[] {
            "Unused", "Knife", "NinjaBlade", "Sword", "KnightsSword", "Katana", "Axe", "Rod",
            "Staff", "Flail", "Gun", "Crossbow", "Bow", "Instrument", "Book", "Polearm",
            "Pole", "Bag", "Cloth", "Shield", "Helmet", "Hat", "HairAdornment", "Armor",
            "Clothing", "Robe", "Shoes", "Armguard", "Ring", "Armlet", "Cloak", "Perfume",
            "Unknown1", "Unknown2", "Unknown3", "FellSword", "LipRouge", "Unknown6", "Unknown7", "Unknown8"};

        private Equipment equipment;
        public Equipment Equipment
        {
            get { return equipment; }
            set
            {
                if (value == null)
                {
                    this.Enabled = false;
                    equipment = null;
                }
                else if (value != equipment)
                {
                    this.Enabled = true;
                    equipment = value;
                    UpdateView();
                }
            }
        }

        private bool ignoreChanges = false;

        public EquipmentEditor()
        {
            InitializeComponent();
            equipmentCheckedListBox.ItemCheck += equipmentCheckedListBox_ItemCheck;
        }

        private void equipmentCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (!ignoreChanges)
            {
                Utilities.SetFlag(equipment, FieldNames[e.Index], e.NewValue == CheckState.Checked);
            }
        }

        private void UpdateView()
        {
            this.SuspendLayout();
            equipmentCheckedListBox.SuspendLayout();

            ignoreChanges = true;
            for (int i = 0; i < equipmentCheckedListBox.Items.Count; i++)
            {
                equipmentCheckedListBox.SetItemChecked(i, Utilities.GetFlag(equipment, FieldNames[i]));
            }
            ignoreChanges = false;
            equipmentCheckedListBox.ResumeLayout();
            this.ResumeLayout();
        }
    }
}
