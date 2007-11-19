/*
	Copyright 2007, Joe Davidson <joedavidson@gmail.com>

	This file is part of LionEditor.

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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace LionEditor
{
    public partial class CharacterEditor : UserControl
    {
        private bool ignoreChanges = false;

        private Character character;
        public Character Character
        {
            get { return character; }
            set
            {
                character = value;
                UpdateView();
            }
        }

        private void UpdateView()
        {
            // HACK: find a better way to do this
            ignoreChanges = true;

            rightHandCombo.SelectedIndex = (rightHandCombo.DataSource as List<Item>).IndexOf( character.RightHand );
            rightShieldCombo.SelectedIndex = (rightShieldCombo.DataSource as List<Item>).IndexOf( character.RightShield );
            leftHandCombo.SelectedIndex = (leftHandCombo.DataSource as List<Item>).IndexOf( character.LeftHand );
            leftShieldCombo.SelectedIndex = (leftShieldCombo.DataSource as List<Item>).IndexOf( character.LeftShield );
            accessoryCombo.SelectedIndex = (accessoryCombo.DataSource as List<Item>).IndexOf( character.Accessory );
            headCombo.SelectedIndex = (headCombo.DataSource as List<Item>).IndexOf( character.Head );
            bodyCombo.SelectedIndex = (bodyCombo.DataSource as List<Item>).IndexOf( character.Body );

            this.braverySpinner.Value = character.Brave;
            this.experienceSpinner.Value = character.Experience;
            this.faithSpinner.Value = character.Faith;
            this.groupBox1.Text = character.Index.ToString();
            this.classComboBox.SelectedItem = character.Job;
            this.movementCombo.SelectedIndex = (movementCombo.DataSource as List<Ability>).IndexOf( character.MovementAbility );
            this.reactionCombo.SelectedIndex = (reactionCombo.DataSource as List<Ability>).IndexOf( character.ReactAbility );
            this.supportCombo.SelectedIndex = (supportCombo.DataSource as List<Ability>).IndexOf( character.SupportAbility );
            this.nameTextBox.Text = character.Name;

            this.secondaryCombo.SelectedItem = character.SecondaryAction;
            this.spriteSetCombo.SelectedItem = character.SpriteSet;
            this.levelSpinner.Value = character.Level;
            this.zodiacComboBox.SelectedItem = character.ZodiacSign;
            this.skillTextLabel.Text = character.Job.Command;

            UpdateMove();
            UpdateJump();

            UpdateHPValue();
            UpdateMPValue();
            UpdateSPValue();
            UpdatePAValue();
            UpdateMAValue();
            UpdateRightWeapon();
            UpdateLeftWeapon();
            UpdateCEV();
            UpdateSEV();
            UpdateAEV();

            ignoreChanges = false;
        }

        private void UpdateRightWeapon()
        {
            rightWeaponEvade.Text = string.Format( "/ {0:00}%", character.RBlockRate );
            rightWeaponPower.Text = string.Format( "{0:000}", character.RPower );
        }

        private void UpdateLeftWeapon()
        {
            leftWeaponEvade.Text = string.Format( "/ {0:00}%", character.LBlockRate );
            leftWeaponPower.Text = string.Format( "{0:000}", character.LPower );
        }

        private void UpdateCEV()
        {
            cevPhysical.Text = string.Format( "{0:00}%", character.PhysicalCEV );
            cevMagic.Text = string.Format( "{0:00}%", character.MagicCEV );
        }

        private void UpdateSEV()
        {
            sevMagic.Text = string.Format( "{0:00}%", character.MagicSEV );
            sevPhysical.Text = string.Format( "{0:00}%", character.PhysicalSEV );
        }

        private void UpdateAEV()
        {
            aevMagic.Text = "00%";
            aevPhysical.Text = "00%";
        }

        private void UpdateMove()
        {
            moveValueLabel.Text = character.Move.ToString();
        }

        private void UpdateJump()
        {
            jumpValueLabel.Text = character.Jump.ToString();
        }

        private void UpdateHPValue()
        {
            hpSpinner.Minimum = 0;
            hpSpinner.Value = (character.HP > hpSpinner.Maximum) ? hpSpinner.Maximum : character.HP;
            hpSpinner.Minimum = character.HP - character.Job.ActualHP( character.RawHP );
        }

        private void UpdateMPValue()
        {
            mpSpinner.Minimum = 0;
            mpSpinner.Value = (character.MP > mpSpinner.Maximum) ? mpSpinner.Maximum : character.MP;
            mpSpinner.Minimum = character.MP - character.Job.ActualMP( character.RawMP );
        }

        private void UpdateSPValue()
        {
            speedSpinner.Minimum = 0;
            speedSpinner.Value = (character.Speed > speedSpinner.Maximum) ? speedSpinner.Maximum : character.Speed;
            speedSpinner.Minimum = character.Speed - character.Job.ActualSP( character.RawSP );
        }

        private void UpdateMAValue()
        {
            maSpinner.Minimum = 0;
            maSpinner.Value = (character.MA > maSpinner.Maximum) ? maSpinner.Maximum : character.MA;
            maSpinner.Minimum = character.MA - character.Job.ActualMA( character.RawMA );
        }

        private void UpdatePAValue()
        {
            paSpinner.Minimum = 0;
            paSpinner.Value = (character.PA > paSpinner.Maximum) ? paSpinner.Maximum : character.PA;
            paSpinner.Minimum = character.PA - character.Job.ActualPA( character.RawPA );
        }


        public CharacterEditor()
        {
            InitializeComponent();
            AssignComboBoxItems();

            nameTextBox.Validating += new CancelEventHandler( nameTextBox_Validating );
            System.IO.FileStream stream = new System.IO.FileStream( "testCharacter.hex", System.IO.FileMode.Open );
            byte[] bytes = new byte[256];
            stream.Read( bytes, 0, 256 );
            stream.Close();
            Character = new Character( bytes );

            UpdateView();
            SetupEvents();
        }

        private void SetupEvents()
        {
            classComboBox.SelectedIndexChanged +=
                delegate( object sender, EventArgs e )
                {
                    //UpdateJob( (Class)classComboBox.SelectedItem );
                    character.Job = (Class)classComboBox.SelectedItem;
                    ignoreChanges = true;
                    UpdateMove();
                    UpdateJump();
                    UpdateSPValue();
                    UpdateMAValue();
                    UpdatePAValue();
                    UpdateCEV();
                    UpdateHPValue();
                    UpdateMPValue();
                    ignoreChanges = false;
                };
            nameTextBox.Validated +=
                delegate( object sender, EventArgs e )
                {
                    character.Name = nameTextBox.Text;
                };
            spriteSetCombo.SelectedIndexChanged +=
                delegate( object sender, EventArgs e )
                {
                    character.SpriteSet = (SpriteSet)spriteSetCombo.SelectedItem;
                };
            levelSpinner.ValueChanged +=
                delegate( object sender, EventArgs e )
                {
                    character.Level = (byte)levelSpinner.Value;
                };
            experienceSpinner.ValueChanged +=
                delegate( object sender, EventArgs e )
                {
                    character.Experience = (byte)experienceSpinner.Value;
                };
            hpSpinner.ValueChanged +=
                delegate( object sender, EventArgs e )
                {
                    if( !ignoreChanges )
                    {
                        character.HP = (uint)hpSpinner.Value;
                    }
                };
            speedSpinner.ValueChanged +=
                delegate( object sender, EventArgs e )
                {
                    if( !ignoreChanges )
                    {
                        character.Speed = (uint)speedSpinner.Value;
                    }
                };
            mpSpinner.ValueChanged +=
                delegate( object sender, EventArgs e )
                {
                    if( !ignoreChanges )
                    {
                        character.MP = (uint)mpSpinner.Value;
                    }
                };
            paSpinner.ValueChanged +=
                delegate( object sender, EventArgs e )
                {
                    if( !ignoreChanges )
                    {
                        character.PA = (uint)paSpinner.Value;
                    }
                };
            maSpinner.ValueChanged +=
                delegate( object sender, EventArgs e )
                {
                    if( !ignoreChanges )
                    {
                        character.MA = (uint)maSpinner.Value;
                    }
                };
            zodiacComboBox.SelectedValueChanged +=
                delegate( object sender, EventArgs e )
                {
                    character.ZodiacSign = (Zodiac)zodiacComboBox.SelectedValue;
                };
            braverySpinner.ValueChanged +=
                delegate( object sender, EventArgs e )
                {
                    character.Brave = (byte)braverySpinner.Value;
                };
            faithSpinner.ValueChanged +=
                delegate( object sender, EventArgs e )
                {
                    character.Faith = (byte)faithSpinner.Value;
                };

            rightHandCombo.SelectedValueChanged +=
                delegate( object sender, EventArgs e )
                {
                    character.RightHand = (Item)rightHandCombo.SelectedItem;
                    UpdateRightWeapon();
                    UpdateMove();
                    UpdateJump();
                    UpdateSPValue();
                    UpdateHPValue();
                    UpdatePAValue();
                    UpdateMAValue();
                    UpdateMPValue();
                    UpdateSEV();
                    UpdateAEV();
                };
            rightShieldCombo.SelectedValueChanged +=
                delegate( object sender, EventArgs e )
                {
                    character.RightShield = (Item)rightShieldCombo.SelectedItem;
                    UpdateMove();
                    UpdateJump();
                    UpdateSPValue();
                    UpdateHPValue();
                    UpdatePAValue();
                    UpdateMAValue();
                    UpdateMPValue();
                    UpdateSEV();
                    UpdateAEV();
                };
            leftHandCombo.SelectedValueChanged +=
                delegate( object sender, EventArgs e )
                {
                    character.LeftHand = (Item)leftHandCombo.SelectedItem;
                    UpdateLeftWeapon();
                    UpdateMove();
                    UpdateJump();
                    UpdateSPValue();
                    UpdateHPValue();
                    UpdatePAValue();
                    UpdateMAValue();
                    UpdateMPValue();
                    UpdateSEV();
                    UpdateAEV();
                };
            leftShieldCombo.SelectedValueChanged +=
                delegate( object sender, EventArgs e )
                {
                    character.LeftShield = (Item)leftShieldCombo.SelectedItem;
                    UpdateMove();
                    UpdateJump();
                    UpdateSPValue();
                    UpdateHPValue();
                    UpdatePAValue();
                    UpdateMAValue();
                    UpdateMPValue();
                    UpdateSEV();
                    UpdateAEV();
                };
            headCombo.SelectedValueChanged +=
                delegate( object sender, EventArgs e )
                {
                    character.Head = (Item)headCombo.SelectedItem;
                    UpdateMove();
                    UpdateJump();
                    UpdateSPValue();
                    UpdateHPValue();
                    UpdatePAValue();
                    UpdateMAValue();
                    UpdateMPValue();
                    UpdateSEV();
                    UpdateAEV();
                };
            bodyCombo.SelectedValueChanged +=
                delegate( object sender, EventArgs e )
                {
                    character.Body = (Item)bodyCombo.SelectedItem;
                    UpdateMove();
                    UpdateJump();
                    UpdateSPValue();
                    UpdateHPValue();
                    UpdatePAValue();
                    UpdateMAValue();
                    UpdateMPValue();
                    UpdateSEV();
                    UpdateAEV();
                };
            accessoryCombo.SelectedValueChanged +=
                delegate( object sender, EventArgs e )
                {
                    character.Accessory = (Item)accessoryCombo.SelectedItem;
                    UpdateMove();
                    UpdateJump();
                    UpdateSPValue();
                    UpdateHPValue();
                    UpdatePAValue();
                    UpdateMAValue();
                    UpdateMPValue();
                    UpdateSEV();
                    UpdateAEV();
                };
            secondaryCombo.SelectedIndexChanged +=
                delegate( object sender, EventArgs e )
                {
                    character.SecondaryAction = (SecondaryAction)secondaryCombo.SelectedItem;
                    UpdateView();
                };
            reactionCombo.SelectedValueChanged +=
                delegate( object sender, EventArgs e )
                {
                    character.ReactAbility = (Ability)reactionCombo.SelectedItem;
                    UpdateMove();
                    UpdateJump();
                };
            supportCombo.SelectedValueChanged +=
                delegate( object sender, EventArgs e )
                {
                    character.SupportAbility = (Ability)supportCombo.SelectedItem;
                    UpdateMove();
                    UpdateJump();
                };
            movementCombo.SelectedValueChanged +=
                delegate( object sender, EventArgs e )
                {
                    character.MovementAbility = (Ability)movementCombo.SelectedItem;
                    UpdateMove();
                    UpdateJump();
                };
        
        }

        void nameTextBox_Validating( object sender, CancelEventArgs e )
        {
            List<char> charList = new List<char>( Character.characterMap );
            foreach( char c in nameTextBox.Text )
            {
                if( charList.IndexOf( c ) < 0 )
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

        private int GetIdealDropDownWidth( ICollection items, ComboBox c, int minimumWidth )
        {
            int width = minimumWidth;
            Graphics g = c.CreateGraphics();
            Font f = c.Font;
            int scrollBarWidth = (items.Count > c.MaxDropDownItems) ? SystemInformation.VerticalScrollBarWidth : 0;
            int itemWidth;
            foreach( object o in items )
            {
                string s = o.ToString();
                itemWidth = (int)g.MeasureString( s, f ).Width + scrollBarWidth;
                if( width < itemWidth )
                {
                    width = itemWidth;
                }
            }

            return width;
        }

        private void AssignComboBoxItems()
        {
            ComboBox[] itemCombos = new ComboBox[] { rightHandCombo, rightShieldCombo, leftHandCombo, leftShieldCombo, headCombo, bodyCombo, accessoryCombo };
            foreach (ComboBox c in itemCombos)
            {
                c.DisplayMember = "String";
                c.ValueMember = "Offset";
                c.DataSource = Item.ItemList;
                c.Validating += ComboBoxValidating;

                //c.DropDownWidth = GetIdealDropDownWidth( Item.ItemList, c, 0 );
            }

            secondaryCombo.DisplayMember = "String";
            secondaryCombo.ValueMember = "Byte";
            secondaryCombo.DataSource = SecondaryAction.ActionList;
            //secondaryCombo.DropDownWidth = GetIdealDropDownWidth( SecondaryAction.ActionList, secondaryCombo, 0 );

            ComboBox[] abilityCombos = new ComboBox[] { movementCombo, reactionCombo, supportCombo };
            foreach( ComboBox a in abilityCombos )
            {
                a.DisplayMember = "String";
                a.ValueMember = "Value";
                a.DataSource = Ability.AbilityList;
                //a.DropDownWidth = GetIdealDropDownWidth( Ability.AbilityList, a, 0 );
            }

            classComboBox.DataSource = Class.ClassList;
            //classComboBox.DropDownWidth = GetIdealDropDownWidth( Class.ClassList, classComboBox, 0 );

            zodiacComboBox.DataSource = Enum.GetValues( typeof( Zodiac ) );
            //zodiacComboBox.DropDownWidth = GetIdealDropDownWidth( Enum.GetValues( typeof( Zodiac ) ), zodiacComboBox, 0 );

            spriteSetCombo.DataSource = SpriteSet.AllSprites;
            //spriteSetCombo.DropDownWidth = GetIdealDropDownWidth( SpriteSet.AllSprites, spriteSetCombo, 0 );
        }

        void ComboBoxValidating( object sender, CancelEventArgs e )
        {
            ComboBox c = sender as ComboBox;
            if( c.SelectedItem == null )
            {
                e.Cancel = true;
            }
        }

    }
}
