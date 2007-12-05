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
using System.Windows.Forms.VisualStyles;

namespace LionEditor
{
    public partial class SavegameEditor : UserControl
    {
        #region Fields

        private Savegame game;
        private bool ignoreChanges = false;
        
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the game currently being edited
        /// </summary>
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        public Savegame Game
        {
            get { return game; }
            set
            {
                game = value;
                if( value != null )
                {
                    ignoreChanges = true;

                    characterEditor.Enabled = true;
                    characterSelector.Enabled = true;
                    tabControl.Enabled = true;

                    UpdateView();

                    ignoreChanges = false;
                }
            }
        }

        #endregion

        #region Events

        void chronicleEditor_DataChangedEvent( object sender, EventArgs e )
        {
            if( !ignoreChanges )
            {
                game.Kills = chronicleEditor.Kills;
                game.Casualties = chronicleEditor.Casualties;
                game.Timer = chronicleEditor.Timer;
                game.Date = chronicleEditor.Date;
                game.WarFunds = chronicleEditor.WarFunds;
                dataChanged( sender, e );
            }
        }

        void dataChanged( object sender, EventArgs e )
        {
            FireDataChangedEvent();
        }
        bool ignoreChecks = false;
        void characterSelector_ItemCheck( object sender, ItemCheckEventArgs e )
        {
            if ((e.NewValue != CheckState.Indeterminate) && (!ignoreChecks))
            {
                Character c = characterSelector.Items[e.Index] as Character;
                c.IsPresent = (e.NewValue == CheckState.Checked);
                FireDataChangedEvent();
            }
        }

        int lastSelectedIndex;

        void characterSelector_SelectedIndexChanged( object sender, EventArgs e )
        {
            Character c = characterSelector.SelectedItem as Character;
            characterEditor.Character = c;
            lastSelectedIndex = characterSelector.SelectedIndex;
        }

        public event EventHandler DataChangedEvent;

        private void FireDataChangedEvent()
        {
            if( (DataChangedEvent != null) && (!ignoreChanges) )
            {
                DataChangedEvent( this, EventArgs.Empty );
            }
        }

        #region Drag/Drop support

        void characterSelector_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Effect == DragDropEffects.Move)
            {
                if (e.Data.GetDataPresent(typeof(Character)))
                {
                    CheckedListBox listBox = sender as CheckedListBox;
                    Character c = e.Data.GetData(typeof(Character)) as Character;
                    int index = listBox.IndexFromPoint(listBox.PointToClient(new Point(e.X, e.Y)));
                    if (index > -1)
                    {
                        listBox.Items.Insert(index, c);
                        int oldSelectedIndex = listBox.SelectedIndex;
                        //listBox.SelectedIndex = index;
                        listBox.Items.RemoveAt(oldSelectedIndex);
                        listBox.SelectedItem = c;
                        characterEditor.Character = c;
                    }
                }
            }
        }

        void characterSelector_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Character)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        void characterSelector_MouseDown(object sender, MouseEventArgs e)
        {
            // Determine if the click was on the checkbox
            Size s;
            int index = characterSelector.IndexFromPoint(e.X, e.Y);
            using (Graphics g = characterSelector.CreateGraphics())
            {
                s = CheckBoxRenderer.GetGlyphSize(g, characterSelector.CheckedIndices.Contains(index) ? CheckBoxState.CheckedNormal : CheckBoxState.UncheckedNormal);
            }
            Rectangle bounds = characterSelector.GetItemRectangle(index);
            int num2 = characterSelector.Font.Height + 2;
            int num3 = Math.Max((num2 - s.Width) / 2, 0);
            if ((num3 + s.Width) > bounds.Height)
            {
                num3 = bounds.Height - s.Width;
            }

            Rectangle rectangle = new Rectangle(bounds.X + 1, bounds.Y + num3, s.Width, s.Width);

            CheckedListBox listBox = sender as CheckedListBox;

            // If not, do whatever
            if ((e.Button == MouseButtons.Left) && (listBox.SelectedIndex == lastSelectedIndex) && (!rectangle.Contains(e.Location)))
            {
                if (listBox.Items.Count == 0)
                {
                    return;
                }

                Character c = listBox.Items[index] as Character;
                DragDropEffects dde = DoDragDrop(c, DragDropEffects.Move);
                lastSelectedIndex = listBox.SelectedIndex;

                // Reassign everyone's index and checkstates
                for (int i = 0; i < listBox.Items.Count; i++)
                {
                    c = listBox.Items[i] as Character;
                    listBox.SetItemChecked(i, c.IsPresent);
                    c.Index = (byte)i;
                }

                // TODO: "dropped" character's index displays wrong in groupbox
                characterEditor.Invalidate();
            }
        }

        #endregion

        #endregion

        #region Utilities

        private void UpdateView()
        {
            characterSelector.Items.Clear();
            foreach( Character c in game.Characters )
            {
                characterSelector.Items.Add(c, c.IsPresent);
            }

            characterSelector.SelectedIndex = 0;

            optionsEditor.Options = game.Options;

            chronicleEditor.Feats = game.Feats;
            chronicleEditor.Wonders = game.Wonders;
            chronicleEditor.Artefacts = game.Artefacts;

            chronicleEditor.Kills = (game.Kills > 9999) ? 9999 : game.Kills;
            chronicleEditor.Casualties = (game.Casualties > 9999) ? 9999 : game.Casualties;
            chronicleEditor.Timer = game.Timer;
            chronicleEditor.WarFunds = game.WarFunds;
            chronicleEditor.Date = game.Date;

            inventoryEditor.Inventory = game.Inventory;
            poachersDenEditor.Inventory = game.PoachersDen;
        }

        #endregion

        public SavegameEditor()
        {
            InitializeComponent();

            characterEditor.Enabled = false;
            characterSelector.Enabled = false;
            tabControl.Enabled = false;
            
            characterSelector.SelectedIndexChanged += characterSelector_SelectedIndexChanged;
            characterSelector.CheckOnClick = false;
            characterSelector.ItemCheck += characterSelector_ItemCheck;

            characterEditor.DataChangedEvent += dataChanged;
            optionsEditor.DataChangedEvent += dataChanged;
            chronicleEditor.DataChangedEvent += chronicleEditor_DataChangedEvent;
            inventoryEditor.DataChangedEvent += dataChanged;
            poachersDenEditor.DataChangedEvent += dataChanged;

            characterSelector.MouseDown += characterSelector_MouseDown;
            characterSelector.DragEnter += characterSelector_DragEnter;
            characterSelector.DragDrop += characterSelector_DragDrop;
        }
    }
}
