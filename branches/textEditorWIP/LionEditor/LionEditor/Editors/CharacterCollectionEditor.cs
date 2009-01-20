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
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace LionEditor
{
    /// <summary>
    /// A UserControl for editing collections of characters, with drag/drop, rearranging, etc.
    /// </summary>
    public partial class CharacterCollectionEditor : UserControl
    {
        #region Fields

        private List<Character> characterCollection;
        private Character characterInVirtualClipBoard = null;
        private int lastSelectedIndex;
        private bool contextMenuEnabled = true;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets whether the CharacterEditor is enabled
        /// </summary>
        public bool CharacterEditorEnabled
        {
            get { return characterEditor.Enabled; }
            set { characterEditor.Enabled = false; }
        }

        /// <summary>
        /// Gets or sets the collection of characters being edited by this instance
        /// </summary>
        public List<Character> CharacterCollection
        {
            get { return characterCollection; }
            set
            {
                characterCollection = value;
                characterSelector.Items.Clear();
                if (characterCollection != null)
                {
                    foreach (Character c in characterCollection)
                    {
                        characterSelector.Items.Add(c, c.IsPresent);
                    }

                    characterSelector.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Gets or sets whether the user can right click on a character's name in the CheckedListBox
        /// </summary>
        public bool ContextMenuEnabled
        {
            get { return contextMenuEnabled; }
            set 
            { 
                contextMenuEnabled = value;
                characterSelector.ContextMenu = contextMenuEnabled ? characterRightClickMenu : null;
            }
        }

        #endregion

        #region Constructor

        public CharacterCollectionEditor()
        {
            InitializeComponent();

            characterSelector.SelectedIndexChanged += characterSelector_SelectedIndexChanged;
            characterSelector.CheckOnClick = false;
            characterSelector.ItemCheck += characterSelector_ItemCheck;
            
            characterSelector.ContextMenu = characterRightClickMenu;

            characterSelector.MouseDown += characterSelector_MouseDown;
            characterSelector.DragEnter += characterSelector_DragEnter;
            characterSelector.DragDrop += characterSelector_DragDrop;

            characterCopyMenuItem.Click += characterCopyMenuItem_Click;
            characterPasteMenuItem.Click += characterPasteMenuItem_Click;
            characterMoveUpMenuItem.Click += characterMoveUpMenuItem_Click;
            characterMoveDownMenuItem.Click += characterMoveDownMenuItem_Click;
            characterRightClickMenu.Popup += characterRightClickMenu_Popup;

            characterEditor.DataChangedEvent += characterEditor_DataChangedEvent;
        }

        #endregion

        #region Events

        #region Drag/Drop support

        private void characterSelector_DragDrop(object sender, DragEventArgs e)
        {
            CheckedListBox listBox = sender as CheckedListBox;
            if (e.Data.GetDataPresent(typeof(Character)))
            {
                if (e.Effect == DragDropEffects.Move)
                {
                    Character c = e.Data.GetData(typeof(Character)) as Character;
                    int index = listBox.IndexFromPoint(listBox.PointToClient(new Point(e.X, e.Y)));
                    if (index > -1)
                    {
                        listBox.Items.Insert(index, c);
                        characterCollection.Insert(index, c);

                        int oldSelectedIndex = listBox.SelectedIndex;
                        listBox.Items.RemoveAt(oldSelectedIndex);
                        characterCollection.RemoveAt(oldSelectedIndex);

                        listBox.SelectedItem = c;
                        characterEditor.Character = c;
                        FireDataChangedEvent();
                    }
                }
                else if (e.Effect == DragDropEffects.Copy)
                {
                    Character c = e.Data.GetData(typeof(Character)) as Character;
                    int index = listBox.IndexFromPoint(listBox.PointToClient(new Point(e.X, e.Y)));
                    Character d = new Character(c.ToByteArray(), index);
                    if (index > -1)
                    {
                        d.Index = (byte)index;
                        listBox.Items.Insert(index, d);
                        listBox.SetItemChecked(index, d.IsPresent);
                        characterCollection.Insert(index, d);

                        listBox.Items.RemoveAt(index + 1);
                        characterCollection.RemoveAt(index + 1);

                        listBox.SelectedItem = d;
                        characterEditor.Character = d;
                        FireDataChangedEvent();
                    }
                }
            }
        }

        private void characterSelector_DragEnter(object sender, DragEventArgs e)
        {
            if ((e.AllowedEffect == DragDropEffects.Copy) && (CharacterEditorEnabled) && (e.Data.GetDataPresent(typeof(Character))))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else if ((e.AllowedEffect == DragDropEffects.Move) && (CharacterEditorEnabled) && (e.Data.GetDataPresent(typeof(Character))))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void characterSelector_MouseDown(object sender, MouseEventArgs e)
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

                DragDropEffects dde = DoDragDrop(c, CharacterEditorEnabled ? DragDropEffects.Move : DragDropEffects.Copy);
                lastSelectedIndex = listBox.SelectedIndex;

                // Reassign everyone's index and checkstates
                for (int i = 0; i < listBox.Items.Count; i++)
                {
                    c = listBox.Items[i] as Character;
                    listBox.SetItemChecked(i, c.IsPresent);
                    c.Index = (byte)i;
                }

                characterEditor.Character = characterSelector.SelectedItem as Character;
            }
            else if (e.Button == MouseButtons.Right)
            {
                characterSelector.SelectedIndex = index;
            }
        }

        #endregion

        #region Context menu

        private void characterRightClickMenu_Popup(object sender, EventArgs e)
        {
            characterMoveDownMenuItem.Enabled = (characterSelector.SelectedIndex != (characterSelector.Items.Count - 1));
            characterMoveUpMenuItem.Enabled = (characterSelector.SelectedIndex != 0);
            characterPasteMenuItem.Enabled = (characterInVirtualClipBoard != null);
        }

        private void characterMoveDownMenuItem_Click(object sender, EventArgs e)
        {
            CharacterMove(false);
        }

        private void characterMoveUpMenuItem_Click(object sender, EventArgs e)
        {
            CharacterMove(true);
        }

        private void characterPasteMenuItem_Click(object sender, EventArgs e)
        {
            if (characterInVirtualClipBoard != null)
            {
                this.SuspendLayout();
                Character newChar = characterInVirtualClipBoard.Clone() as Character;
                newChar.Index = (byte)characterSelector.SelectedIndex;
                characterCollection[characterSelector.SelectedIndex] = newChar;
                characterSelector.Items[characterSelector.SelectedIndex] = newChar;
                characterEditor.Character = newChar;
                FireDataChangedEvent();

                this.ResumeLayout();
            }
        }

        private void characterCopyMenuItem_Click(object sender, EventArgs e)
        {
            characterPasteMenuItem.Enabled = true;
            characterInVirtualClipBoard = characterSelector.SelectedItem as Character;
        }

        #endregion

        private void characterEditor_DataChangedEvent(object sender, EventArgs e)
        {
            FireDataChangedEvent();
        }

        private void characterSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            Character c = characterSelector.SelectedItem as Character;
            characterEditor.Character = c;
            lastSelectedIndex = characterSelector.SelectedIndex;
        }

        private void characterSelector_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue != CheckState.Indeterminate)
            {
                Character c = characterSelector.Items[e.Index] as Character;
                c.IsPresent = (e.NewValue == CheckState.Checked);
                FireDataChangedEvent();
            }
        }

        public event EventHandler DataChangedEvent;

        private void FireDataChangedEvent()
        {
            if (DataChangedEvent != null)
            {
                DataChangedEvent(this, EventArgs.Empty);
            }
        }


        #endregion

        #region Utilities

        private void CharacterMove(bool up)
        {
            int add = up ? -1 : 1;
            this.SuspendLayout();
            Character moved = characterSelector.SelectedItem as Character;
            Character notMoved = characterSelector.Items[characterSelector.SelectedIndex + add] as Character;
            moved.Index = (byte)(moved.Index + add);
            notMoved.Index = (byte)(notMoved.Index - add);

            characterSelector.Items[characterSelector.SelectedIndex] = notMoved;
            characterSelector.Items[characterSelector.SelectedIndex + add] = moved;
            
            characterSelector.SelectedItem = moved;

            CharacterCollection[moved.Index] = moved;
            CharacterCollection[notMoved.Index] = notMoved;

            FireDataChangedEvent();
            this.ResumeLayout();
        }
        
        #endregion
    }
}
