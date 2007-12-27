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
    public partial class ItemEditor : UserControl
    {
        private string[] weaponBools = new string[] {
            "Striking", "Lunging", "Direct", "Arc",
            "TwoSwords", "TwoHands", "Blank", "Force2Hands" };
        private string[] itemBools = new string[] {
            "Weapon", "Shield", "Head", "Body",
            "Accessory", "Blank1", "Rare", "Blank2" };
        private List<NumericUpDown> spinners = new List<NumericUpDown>();
        private List<ComboBox> comboBoxes = new List<ComboBox>();

        private bool ignoreChanges = false;
        private Item item;
        public Item Item
        {
            get { return item; }
            set
            {
                if( value == null )
                {
                    this.Enabled = false;
                    this.item = null;
                }
                else if( value != item )
                {
                    this.item = value;
                    this.Enabled = true;
                    UpdateView();
                }
            }
        }

        private void UpdateView()
        {
            ignoreChanges = true;
            SuspendLayout();
            weaponPanel.SuspendLayout();
            shieldPanel.SuspendLayout();
            armorPanel.SuspendLayout();
            accessoryPanel.SuspendLayout();
            chemistItemPanel.SuspendLayout();

            weaponPanel.Visible = item is Weapon;
            weaponPanel.Enabled = item is Weapon;

            shieldPanel.Visible = item is Shield;
            shieldPanel.Enabled = item is Shield;

            armorPanel.Visible = item is Armor;
            armorPanel.Enabled = item is Armor;

            accessoryPanel.Visible = item is Accessory;
            accessoryPanel.Enabled = item is Accessory;

            chemistItemPanel.Visible = item is ChemistItem;
            chemistItemPanel.Enabled = item is ChemistItem;

            if( item is Weapon )
            {
                for( int i = 0; i < 8; i++ )
                {
                    weaponAttributesCheckedListBox.SetItemChecked( i, 
                        Utilities.GetFieldOrProperty<bool>( item as Weapon, weaponBools[i] ) );
                }
                weaponRangeSpinner.Value = (item as Weapon).Range;
                weaponFormulaSpinner.Value = (item as Weapon).Formula;
                weaponPowerSpinner.Value = (item as Weapon).WeaponPower;
                weaponEvadePercentageSpinner.Value = (item as Weapon).EvadePercentage;
                weaponSpellStatusSpinner.Value = (item as Weapon).SpellStatus;
                weaponElementsEditor.Elements = (item as Weapon).Elements;
            }
            else if( item is Shield )
            {
                shieldPhysicalBlockRateSpinner.Value = (item as Shield).PhysicalBlockRate;
                shieldMagicBlockRateSpinner.Value = (item as Shield).MagicBlockRate;
            }
            else if( item is Armor )
            {
                armorHPBonusSpinner.Value = (item as Armor).HPBonus;
                armorMPBonusSpinner.Value = (item as Armor).MPBonus;
            }
            else if( item is Accessory )
            {
                accessoryMagicEvadeRateSpinner.Value = (item as Accessory).MagicEvade;
                accessoryPhysicalEvadeRateLabel.Value = (item as Accessory).PhysicalEvade;
            }
            else if( item is ChemistItem )
            {
                chemistItemFormulaSpinner.Value = (item as ChemistItem).Formula;
                chemistItemSpellStatusSpinner.Value = (item as ChemistItem).SpellStatus;
                chemistItemSpellStatusSpinner.Value = (item as ChemistItem).X;
            }

            paletteSpinner.Value = item.Palette;
            graphicSpinner.Value = item.Graphic;
            enemyLevelSpinner.Value = item.EnemyLevel;
            itemTypeComboBox.SelectedItem = item.ItemType;
            itemAttributesSpinner.Value = item.SIA;
            priceSpinner.Value = item.Price;
            shopAvailabilityComboBox.SelectedItem = item.ShopAvailability;
            for( int i = 0; i < 8; i++ )
            {
                itemAttributesCheckedListBox.SetItemChecked( i,
                    Utilities.GetFieldOrProperty<bool>( item, itemBools[i] ) );
            }

            weaponPanel.ResumeLayout();
            shieldPanel.ResumeLayout();
            armorPanel.ResumeLayout();
            accessoryPanel.ResumeLayout();
            chemistItemPanel.ResumeLayout();
            ResumeLayout();
            ignoreChanges = false;
        }

        public ItemEditor()
        {
            InitializeComponent();
            ignoreChanges = true;

            spinners.AddRange( new NumericUpDown[] { 
                paletteSpinner, graphicSpinner, enemyLevelSpinner, itemAttributesSpinner, priceSpinner, 
                weaponRangeSpinner, weaponFormulaSpinner, weaponPowerSpinner, weaponEvadePercentageSpinner, weaponSpellStatusSpinner, 
                armorHPBonusSpinner, armorMPBonusSpinner, 
                shieldMagicBlockRateSpinner, shieldPhysicalBlockRateSpinner, 
                accessoryMagicEvadeRateSpinner, accessoryPhysicalEvadeRateLabel, 
                chemistItemFormulaSpinner, chemistItemSpellStatusSpinner, chemistItemXSpinner } );
            comboBoxes.AddRange( new ComboBox[] { itemTypeComboBox, shopAvailabilityComboBox } );
            foreach( NumericUpDown spinner in spinners )
            {
                spinner.ValueChanged += spinner_ValueChanged;
            }
            foreach( ComboBox comboBox in comboBoxes )
            {
                comboBox.SelectedIndexChanged += comboBox_SelectedIndexChanged;
            }

            itemAttributesCheckedListBox.ItemCheck += itemAttributesCheckedListBox_ItemCheck;
            weaponAttributesCheckedListBox.ItemCheck += weaponAttributesCheckedListBox_ItemCheck;

            itemTypeComboBox.DataSource = Enum.GetValues( typeof( ItemSubType ) );
            shopAvailabilityComboBox.DataSource = ShopAvailability.AllAvailabilities;

            ignoreChanges = false;
        }

        private void weaponAttributesCheckedListBox_ItemCheck( object sender, ItemCheckEventArgs e )
        {
            if( !ignoreChanges )
            {
                Weapon w = item as Weapon;
                Utilities.SetFieldOrProperty( w, weaponBools[e.Index], e.NewValue );
            }
        }

        private void itemAttributesCheckedListBox_ItemCheck( object sender, ItemCheckEventArgs e )
        {
            if( !ignoreChanges )
            {
                Utilities.SetFieldOrProperty( item, itemBools[e.Index], e.NewValue );
            }
        }

        private void comboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            if( !ignoreChanges )
            {
                ComboBox c = sender as ComboBox;
                Utilities.SetFieldOrProperty( item, c.Tag.ToString(), c.SelectedItem );
            }
        }

        private void spinner_ValueChanged( object sender, EventArgs e )
        {
            if( !ignoreChanges )
            {
                NumericUpDown spinner = sender as NumericUpDown;
                if( spinner == priceSpinner )
                {
                    Utilities.SetFieldOrProperty( item, spinner.Tag.ToString(), (UInt16)spinner.Value );
                }
                else
                {
                    Utilities.SetFieldOrProperty( item, spinner.Tag.ToString(), (byte)spinner.Value );
                }
            }
        }
    }
}
