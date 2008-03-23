/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of FFTPatcher.

    FFTPatcher is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    FFTPatcher is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with FFTPatcher.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FFTPatcher.Controls;
using FFTPatcher.Datatypes;

namespace FFTPatcher.Editors
{
    public partial class ItemEditor : UserControl
    {

		#region Static Fields (2) 

        private static List<string> itemFormulaItems;
        private static List<string> weaponFormulaItems;

		#endregion Static Fields 

		#region Fields (9) 

        private List<ComboBoxWithDefault> comboBoxes = new List<ComboBoxWithDefault>();
        private bool ignoreChanges = false;
        private Item item;
        private string[] itemBools = new string[] {
            "Weapon", "Shield", "Head", "Body",
            "Accessory", "Blank1", "Rare", "Blank2" };
        private Context ourContext = Context.Default;
        private List<ItemSubType> pspItemTypes = new List<ItemSubType>( (ItemSubType[])Enum.GetValues( typeof( ItemSubType ) ) );
        private List<ItemSubType> psxItemTypes = new List<ItemSubType>( (ItemSubType[])Enum.GetValues( typeof( ItemSubType ) ) );
        private List<NumericUpDownWithDefault> spinners = new List<NumericUpDownWithDefault>();
        private string[] weaponBools = new string[] {
            "Striking", "Lunging", "Direct", "Arc",
            "TwoSwords", "TwoHands", "Blank", "Force2Hands" };

		#endregion Fields 

		#region Properties (1) 


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


		#endregion Properties 

		#region Constructors (2) 

        static ItemEditor()
        {
            weaponFormulaItems = new List<string>( 256 );
            weaponFormulaItems.Add( "00" );
            weaponFormulaItems.AddRange( new string[] {
                "01 - Normal", "02 - Cast spell at random", "03 - Gun",
                "04 - Elemental gun", "05", "06 - Drain HP", "07 - Restore HP" } );
            for( int i = 8; i < 256; i++ )
            {
                weaponFormulaItems.Add( string.Format( "{0:X2}", i ) );
            }

            itemFormulaItems = new List<string>( 256 );
            Dictionary<int, string> t = new Dictionary<int, string>( 5 );
            t.Add( 0x38, "Remove status" );
            t.Add( 0x48, "Restore 10*X HP" );
            t.Add( 0x49, "Restore 10*X MP" );
            t.Add( 0x4A, "Restore all HP/MP" );
            t.Add( 0x4B, "Remove status and restore (1..(X-1)) HP" );
            for( int i = 0; i < 256; i++ )
            {
                if( t.ContainsKey( i ) )
                {
                    itemFormulaItems.Add( t[i] );
                }
                else
                {
                    itemFormulaItems.Add( string.Format( "{0:X2}", i ) );
                }
            }
        }

        public ItemEditor()
        {
            InitializeComponent();
            ignoreChanges = true;

            spinners.AddRange( new NumericUpDownWithDefault[] { 
                paletteSpinner, graphicSpinner, enemyLevelSpinner, itemAttributesSpinner, priceSpinner, 
                weaponRangeSpinner, weaponPowerSpinner, weaponEvadePercentageSpinner, weaponSpellStatusSpinner, 
                armorHPBonusSpinner, armorMPBonusSpinner, 
                shieldMagicBlockRateSpinner, shieldPhysicalBlockRateSpinner, 
                accessoryMagicEvadeRateSpinner, accessoryPhysicalEvadeRateLabel, 
                chemistItemSpellStatusSpinner, chemistItemXSpinner } );
            comboBoxes.AddRange( new ComboBoxWithDefault[] { itemTypeComboBox, shopAvailabilityComboBox } );
            foreach( NumericUpDownWithDefault spinner in spinners )
            {
                spinner.ValueChanged += spinner_ValueChanged;
            }
            foreach( ComboBoxWithDefault comboBox in comboBoxes )
            {
                comboBox.SelectedIndexChanged += comboBox_SelectedIndexChanged;
            }

            weaponFormulaComboBox.SelectedIndexChanged += weaponFormulaComboBox_SelectedIndexChanged;
            chemistItemFormulaComboBox.SelectedIndexChanged += chemistItemFormulaComboBox_SelectedIndexChanged;

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
            weaponFormulaComboBox.DataSource = weaponFormulaItems;
            chemistItemFormulaComboBox.DataSource = itemFormulaItems;

            ignoreChanges = false;
        }

		#endregion Constructors 

		#region Events (2) 

        public event EventHandler<LabelClickedEventArgs> InflictStatusClicked;

        public event EventHandler<LabelClickedEventArgs> ItemAttributesClicked;

		#endregion Events 

		#region Methods (10) 


        private void chemistItemFormulaComboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            if( !ignoreChanges && item is ChemistItem )
            {
                (item as ChemistItem).Formula = (byte)chemistItemFormulaComboBox.SelectedIndex;
            }
        }

        private void chemistItemSpellStatusLabel_Click( object sender, EventArgs e )
        {
            if( InflictStatusClicked != null )
            {
                InflictStatusClicked( this, new LabelClickedEventArgs( (byte)chemistItemSpellStatusSpinner.Value ) );
            }
        }

        private void comboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            if( !ignoreChanges )
            {
                ComboBoxWithDefault c = sender as ComboBoxWithDefault;
                ReflectionHelpers.SetFieldOrProperty( item, c.Tag.ToString(), c.SelectedItem );
            }
        }

        private void itemAttributesCheckedListBox_ItemCheck( object sender, ItemCheckEventArgs e )
        {
            if( !ignoreChanges )
            {
                ReflectionHelpers.SetFieldOrProperty( item, itemBools[e.Index], e.NewValue == CheckState.Checked );
            }
        }

        private void itemAttributesLabel_Click( object sender, EventArgs e )
        {
            if( ItemAttributesClicked != null )
            {
                ItemAttributesClicked( this, new LabelClickedEventArgs( (byte)itemAttributesSpinner.Value ) );
            }
        }

        private void spinner_ValueChanged( object sender, EventArgs e )
        {
            if( !ignoreChanges )
            {
                NumericUpDownWithDefault spinner = sender as NumericUpDownWithDefault;
                if( spinner == priceSpinner )
                {
                    ReflectionHelpers.SetFieldOrProperty( item, spinner.Tag.ToString(), (UInt16)spinner.Value );
                }
                else
                {
                    ReflectionHelpers.SetFieldOrProperty( item, spinner.Tag.ToString(), (byte)spinner.Value );
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
                Weapon w = item as Weapon;

                if( w.WeaponDefault != null )
                {
                    weaponAttributesCheckedListBox.SetValuesAndDefaults( ReflectionHelpers.GetFieldsOrProperties<bool>( w, weaponBools ), w.WeaponDefault.ToWeaponBoolArray() );
                }

                weaponRangeSpinner.SetValueAndDefault(
                    w.Range,
                    w.WeaponDefault.Range );
                weaponFormulaComboBox.SetValueAndDefault(
                    weaponFormulaComboBox.Items[w.Formula],
                    weaponFormulaComboBox.Items[w.WeaponDefault.Formula] );
                weaponPowerSpinner.SetValueAndDefault( 
                    w.WeaponPower, 
                    w.WeaponDefault.WeaponPower );
                weaponEvadePercentageSpinner.SetValueAndDefault(
                    w.EvadePercentage,
                    w.WeaponDefault.EvadePercentage );
                weaponSpellStatusSpinner.SetValueAndDefault(
                    w.InflictStatus,
                    w.WeaponDefault.InflictStatus );

                weaponElementsEditor.SetValueAndDefaults( w.Elements, w.WeaponDefault.Elements );
            }
            else if( item is Shield )
            {
                shieldPhysicalBlockRateSpinner.SetValueAndDefault(
                    (item as Shield).PhysicalBlockRate,
                    (item as Shield).ShieldDefault.PhysicalBlockRate );
                shieldMagicBlockRateSpinner.SetValueAndDefault( 
                    (item as Shield).MagicBlockRate, 
                    (item as Shield).ShieldDefault.MagicBlockRate );
            }
            else if( item is Armor )
            {
                armorHPBonusSpinner.SetValueAndDefault(
                    (item as Armor).HPBonus,
                    (item as Armor).ArmorDefault.HPBonus );
                armorMPBonusSpinner.SetValueAndDefault( 
                    (item as Armor).MPBonus,
                    (item as Armor).ArmorDefault.MPBonus );
            }
            else if( item is Accessory )
            {
                accessoryMagicEvadeRateSpinner.SetValueAndDefault( 
                    (item as Accessory).MagicEvade,
                    (item as Accessory).AccessoryDefault.MagicEvade );
                accessoryPhysicalEvadeRateLabel.SetValueAndDefault( 
                    (item as Accessory).PhysicalEvade,
                    (item as Accessory).AccessoryDefault.PhysicalEvade );
            }
            else if( item is ChemistItem )
            {
                chemistItemFormulaComboBox.SetValueAndDefault(
                    chemistItemFormulaComboBox.Items[(item as ChemistItem).Formula],
                    chemistItemFormulaComboBox.Items[(item as ChemistItem).ChemistItemDefault.Formula] );
                chemistItemSpellStatusSpinner.SetValueAndDefault( 
                    (item as ChemistItem).InflictStatus,
                    (item as ChemistItem).ChemistItemDefault.InflictStatus );
                chemistItemXSpinner.SetValueAndDefault( 
                    (item as ChemistItem).X,
                    (item as ChemistItem).ChemistItemDefault.X );
            }

            paletteSpinner.SetValueAndDefault( item.Palette, item.Default.Palette );
            graphicSpinner.SetValueAndDefault( item.Graphic, item.Default.Graphic );
            enemyLevelSpinner.SetValueAndDefault( item.EnemyLevel, item.Default.EnemyLevel );

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
            itemTypeComboBox.SetValueAndDefault( item.ItemType, item.Default.ItemType );

            itemAttributesSpinner.SetValueAndDefault( item.SIA, item.Default.SIA );
            itemAttributesSpinner.Maximum = FFTPatch.Context == Context.US_PSP ? 0x64 : 0x4F;
            priceSpinner.SetValueAndDefault( item.Price, item.Default.Price );
            shopAvailabilityComboBox.SetValueAndDefault( item.ShopAvailability, item.Default.ShopAvailability );

            if( item.Default != null )
            {
                itemAttributesCheckedListBox.SetValuesAndDefaults( ReflectionHelpers.GetFieldsOrProperties<bool>( item, itemBools ), item.Default.ToBoolArray() );
            }

            weaponPanel.ResumeLayout();
            shieldPanel.ResumeLayout();
            armorPanel.ResumeLayout();
            accessoryPanel.ResumeLayout();
            chemistItemPanel.ResumeLayout();
            ResumeLayout();
            ignoreChanges = false;
        }

        private void weaponAttributesCheckedListBox_ItemCheck( object sender, ItemCheckEventArgs e )
        {
            if( !ignoreChanges )
            {
                Weapon w = item as Weapon;
                ReflectionHelpers.SetFieldOrProperty( w, weaponBools[e.Index], e.NewValue == CheckState.Checked );
            }
        }

        private void weaponFormulaComboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            if( !ignoreChanges && item is Weapon )
            {
                (item as Weapon).Formula = (byte)weaponFormulaComboBox.SelectedIndex;
            }
        }

        private void weaponSpellStatusLabel_Click( object sender, EventArgs e )
        {
            if( InflictStatusClicked != null )
            {
                InflictStatusClicked( this, new LabelClickedEventArgs( (byte)weaponSpellStatusSpinner.Value ) );
            }
        }


		#endregion Methods 

    }
}
