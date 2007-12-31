/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of FFTPatcher.

    LionEditor is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    LionEditor is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with LionEditor.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
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
        private List<ItemSubType> pspItemTypes = new List<ItemSubType>( (ItemSubType[])Enum.GetValues( typeof( ItemSubType ) ) );
        private List<ItemSubType> psxItemTypes = new List<ItemSubType>( (ItemSubType[])Enum.GetValues( typeof( ItemSubType ) ) );
        private Context ourContext = Context.Default;

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
                weaponSpellStatusSpinner.Value = (item as Weapon).InflictStatus;
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
                chemistItemSpellStatusSpinner.Value = (item as ChemistItem).InflictStatus;
                chemistItemXSpinner.Value = (item as ChemistItem).X;
            }

            paletteSpinner.Value = item.Palette;
            graphicSpinner.Value = item.Graphic;
            enemyLevelSpinner.Value = item.EnemyLevel;

            if( FFTPatch.Context == Context.US_PSP && ourContext != Context.US_PSP )
            {
                itemTypeComboBox.DataSource = pspItemTypes;
                ourContext = Context.US_PSP;
            }
            else if( FFTPatch.Context == Context.US_PSX && ourContext != Context.US_PSX )
            {
                itemTypeComboBox.DataSource = psxItemTypes;
                ourContext = Context.US_PSX;
            }
            itemTypeComboBox.SelectedItem = item.ItemType;

            itemAttributesSpinner.Value = item.SIA;
            itemAttributesSpinner.Maximum = FFTPatch.Context == Context.US_PSP ? 0x64 : 0x4F;
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

            shopAvailabilityComboBox.Items.Clear();
            shopAvailabilityComboBox.Items.AddRange( ShopAvailability.AllAvailabilities.ToArray() );
            psxItemTypes.Remove( ItemSubType.LipRouge );
            psxItemTypes.Remove( ItemSubType.FellSword );

            chemistItemSpellStatusLabel.Click += chemistItemSpellStatusLabel_Click;
            chemistItemSpellStatusLabel.TabStop = false;
            weaponSpellStatusLabel.Click += weaponSpellStatusLabel_Click;
            weaponSpellStatusLabel.TabStop = false;
            itemAttributesLabel.Click += itemAttributesLabel_Click;
            itemAttributesLabel.TabStop = false;
            ignoreChanges = false;
        }

        public event EventHandler<LabelClickedEventArgs> ItemAttributesClicked;
        private void itemAttributesLabel_Click( object sender, EventArgs e )
        {
            if( ItemAttributesClicked != null )
            {
                ItemAttributesClicked( this, new LabelClickedEventArgs( (byte)itemAttributesSpinner.Value ) );
            }
        }

        private void weaponSpellStatusLabel_Click( object sender, EventArgs e )
        {
            if( InflictStatusClicked != null )
            {
                InflictStatusClicked( this, new LabelClickedEventArgs( (byte)weaponSpellStatusSpinner.Value ) );
            }
        }

        public event EventHandler<LabelClickedEventArgs> InflictStatusClicked;
        private void chemistItemSpellStatusLabel_Click( object sender, EventArgs e )
        {
            if( InflictStatusClicked != null )
            {
                InflictStatusClicked( this, new LabelClickedEventArgs( (byte)chemistItemSpellStatusSpinner.Value ) );
            }
        }

        private void weaponAttributesCheckedListBox_ItemCheck( object sender, ItemCheckEventArgs e )
        {
            if( !ignoreChanges )
            {
                Weapon w = item as Weapon;
                Utilities.SetFieldOrProperty( w, weaponBools[e.Index], e.NewValue == CheckState.Checked );
            }
        }

        private void itemAttributesCheckedListBox_ItemCheck( object sender, ItemCheckEventArgs e )
        {
            if( !ignoreChanges )
            {
                Utilities.SetFieldOrProperty( item, itemBools[e.Index], e.NewValue == CheckState.Checked );
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
